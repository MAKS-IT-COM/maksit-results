#requires -Version 7.0
#requires -PSEdition Core

<#
.SYNOPSIS
    Refreshes a local maksit-repoutils copy from GitHub.

.DESCRIPTION
    This script clones the configured repository into a temporary directory,
    refreshes the parent directory of this script, preserves existing
    scriptSettings.json files in subfolders, and copies the cloned source contents into that parent directory.

    All configuration is stored in scriptSettings.json.

.EXAMPLE
    pwsh -File .\Update-RepoUtils.ps1

.NOTES
    CONFIGURATION (scriptSettings.json):
    - dryRun: If true, logs the planned update without modifying files
    - repository.url: Git repository to clone
    - repository.sourceSubdirectory: Folder copied into the target directory
    - repository.preserveFileName: Existing file name to preserve in subfolders
    - repository.cloneDepth: Depth used for git clone
    - repository.skippedRelativeDirectories: Relative directories to exclude from phase-two refresh (preserve dest)
    - repository.omittedRelativeDirectories: Relative directories to delete from dest and never copy from source
      (product repos: ["tests"] — RepoUtils self-tests stay only in maksit-repoutils / enterprise)
#>

[CmdletBinding()]
param(
    [switch]$ContinueAfterSelfUpdate,
    [string]$TargetDirectoryOverride,
    [string]$ClonedSourceDirectoryOverride,
    [string]$TemporaryRootOverride
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$srcDir = Split-Path (Split-Path $scriptDir -Parent) -Parent
$modulesDir = Join-Path $srcDir 'modules'

# Refresh the src directory that contains modules, engines, plugins, and tools.
$targetDirectory = if ([string]::IsNullOrWhiteSpace($TargetDirectoryOverride)) {
    $srcDir
}
else {
    [System.IO.Path]::GetFullPath($TargetDirectoryOverride)
}
$currentScriptPath = [System.IO.Path]::GetFullPath($MyInvocation.MyCommand.Path)
$selfUpdateDirectory = [System.IO.Path]::Combine('tools', 'Update-RepoUtils')

function ConvertTo-NormalizedRelativePath {
    param(
        [Parameter(Mandatory = $true)]
        [string]$Path
    )

    $normalizedPath = $Path.Replace('/', [System.IO.Path]::DirectorySeparatorChar).Replace('\', [System.IO.Path]::DirectorySeparatorChar)
    return $normalizedPath.TrimStart('.', [System.IO.Path]::DirectorySeparatorChar).TrimEnd([System.IO.Path]::DirectorySeparatorChar)
}

function Test-IsInRelativeDirectory {
    param(
        [Parameter(Mandatory = $true)]
        [string]$RelativePath,

        [Parameter(Mandatory = $true)]
        [string[]]$Directories
    )

    $normalizedRelativePath = ConvertTo-NormalizedRelativePath -Path $RelativePath
    foreach ($directory in $Directories) {
        $normalizedDirectory = ConvertTo-NormalizedRelativePath -Path $directory
        if ([string]::IsNullOrWhiteSpace($normalizedDirectory)) {
            continue
        }

        if (
            $normalizedRelativePath.Equals($normalizedDirectory, [System.StringComparison]::OrdinalIgnoreCase) -or
            $normalizedRelativePath.StartsWith($normalizedDirectory + [System.IO.Path]::DirectorySeparatorChar, [System.StringComparison]::OrdinalIgnoreCase)
        ) {
            return $true
        }
    }

    return $false
}

function Get-ReleaseDeployPreserveFiles {
    param(
        [Parameter(Mandatory = $true)]
        [string]$TargetDirectory
    )

    return @()
}

function Add-PreservedFileBackup {
    param(
        [Parameter(Mandatory = $true)]
        [System.Collections.IList]$PreservedFiles,

        [Parameter(Mandatory = $true)]
        [System.IO.FileInfo]$File,

        [Parameter(Mandatory = $true)]
        [string]$TargetDirectory,

        [Parameter(Mandatory = $true)]
        [string]$TemporaryRoot,

        [Parameter(Mandatory = $true)]
        [bool]$DryRun
    )

    $relativePath = [System.IO.Path]::GetRelativePath($TargetDirectory, $File.FullName)
    foreach ($existing in $PreservedFiles) {
        if ($existing.RelativePath.Equals($relativePath, [System.StringComparison]::OrdinalIgnoreCase)) {
            return
        }
    }

    $backupPath = Join-Path $TemporaryRoot ("preserved-" + ($relativePath -replace '[\\/:*?""<>|]', '_'))
    $PreservedFiles.Add([pscustomobject]@{
            RelativePath = $relativePath
            BackupPath   = $backupPath
        }) | Out-Null

    if (-not $DryRun) {
        $backupDirectory = Split-Path -Parent $backupPath
        if (-not (Test-Path -Path $backupDirectory -PathType Container)) {
            New-Item -ItemType Directory -Path $backupDirectory -Force | Out-Null
        }

        Copy-Item -Path $File.FullName -Destination $backupPath -Force
    }
}

#region Import Modules

$scriptConfigModulePath = Join-Path $modulesDir "ScriptConfig.psm1"
if (-not (Test-Path $scriptConfigModulePath)) {
    Write-Error "ScriptConfig module not found at: $scriptConfigModulePath"
    exit 1
}

$loggingModulePath = Join-Path $modulesDir "Logging.psm1"
if (-not (Test-Path $loggingModulePath)) {
    Write-Error "Logging module not found at: $loggingModulePath"
    exit 1
}

Import-Module $scriptConfigModulePath -Force
Import-Module $loggingModulePath -Force

#endregion

#region Load Settings

$settings = Get-ScriptSettings -ScriptDir $scriptDir

#endregion

#region Configuration

$repositoryUrl = $settings.repository.url
$dryRun = if ($null -ne $settings.dryRun) { [bool]$settings.dryRun } else { $false }
$sourceSubdirectory = if ($settings.repository.sourceSubdirectory) { $settings.repository.sourceSubdirectory } else { 'src' }
$preserveFileName = if ($settings.repository.preserveFileName) { $settings.repository.preserveFileName } else { 'scriptSettings.json' }
$cloneDepth = if ($settings.repository.cloneDepth) { [int]$settings.repository.cloneDepth } else { 1 }
[string[]]$skippedRelativeDirectories = if ($settings.repository.skippedRelativeDirectories) {
    @(
        $settings.repository.skippedRelativeDirectories |
            ForEach-Object {
                ConvertTo-NormalizedRelativePath -Path ([string]$_)
            }
    )
}
else {
    @(
        [System.IO.Path]::Combine('engines', 'release', 'custom'),
        [System.IO.Path]::Combine('engines', 'test', 'custom')
    )
}
[string[]]$omittedRelativeDirectories = if ($settings.repository.omittedRelativeDirectories) {
    @(
        $settings.repository.omittedRelativeDirectories |
            ForEach-Object {
                ConvertTo-NormalizedRelativePath -Path ([string]$_)
            } |
            Where-Object { -not [string]::IsNullOrWhiteSpace($_) }
    )
}
else {
    @()
}

#endregion

#region Validate CLI Dependencies

Assert-Command git
Assert-Command pwsh

if ([string]::IsNullOrWhiteSpace($repositoryUrl)) {
    Write-Error "repository.url is required in scriptSettings.json."
    exit 1
}

#endregion

#region Main

Write-Log -Level "INFO" -Message "========================================"
Write-Log -Level "INFO" -Message "Update RepoUtils Script"
Write-Log -Level "INFO" -Message "========================================"
Write-Log -Level "INFO" -Message "Target directory: $targetDirectory"
Write-Log -Level "INFO" -Message "Dry run: $dryRun"

$ownsTemporaryRoot = [string]::IsNullOrWhiteSpace($TemporaryRootOverride)
$temporaryRoot = if ($ownsTemporaryRoot) {
    Join-Path ([System.IO.Path]::GetTempPath()) ("maksit-repoutils-update-" + [System.Guid]::NewGuid().ToString('N'))
}
else {
    [System.IO.Path]::GetFullPath($TemporaryRootOverride)
}

try {
    $clonedSourceDirectory = if ([string]::IsNullOrWhiteSpace($ClonedSourceDirectoryOverride)) {
        Write-LogStep "Cloning latest repository snapshot..."
        & git clone --depth $cloneDepth $repositoryUrl $temporaryRoot
        if ($LASTEXITCODE -ne 0) {
            throw "git clone failed with exit code $LASTEXITCODE."
        }
        Write-Log -Level "OK" -Message "Repository cloned"

        Join-Path $temporaryRoot $sourceSubdirectory
    }
    else {
        [System.IO.Path]::GetFullPath($ClonedSourceDirectoryOverride)
    }

    if (-not (Test-Path -Path $clonedSourceDirectory -PathType Container)) {
        throw "The cloned repository does not contain the expected source directory: $clonedSourceDirectory"
    }

    if (-not $ContinueAfterSelfUpdate) {
        if ($dryRun) {
            Write-LogStep "Dry run self-update summary"
            Write-Log -Level "INFO" -Message "Would refresh shared modules and $selfUpdateDirectory before relaunching the updater"
        }
        else {
            Write-LogStep "Refreshing updater files..."
            $selfUpdateFiles = Get-ChildItem -Path $clonedSourceDirectory -Recurse -Force -File |
                Where-Object {
                    $relativePath = [System.IO.Path]::GetRelativePath($clonedSourceDirectory, $_.FullName)
                    $isRootFile = -not $relativePath.Contains([System.IO.Path]::DirectorySeparatorChar)
                    $isUpdaterFile = $relativePath.StartsWith($selfUpdateDirectory + [System.IO.Path]::DirectorySeparatorChar, [System.StringComparison]::OrdinalIgnoreCase)

                    $_.Name -ne $preserveFileName -and
                    ($isRootFile -or $isUpdaterFile)
                }

            foreach ($sourceFile in $selfUpdateFiles) {
                $relativePath = [System.IO.Path]::GetRelativePath($clonedSourceDirectory, $sourceFile.FullName)
                $destinationPath = Join-Path $targetDirectory $relativePath
                $destinationDirectory = Split-Path -Parent $destinationPath
                if (-not (Test-Path -Path $destinationDirectory -PathType Container)) {
                    New-Item -ItemType Directory -Path $destinationDirectory -Force | Out-Null
                }

                Copy-Item -Path $sourceFile.FullName -Destination $destinationPath -Force
            }

            Write-Log -Level "OK" -Message "Updater files refreshed"
        }

        if ($dryRun) {
            Write-LogStep "Dry run bootstrap completed"
            Write-Log -Level "INFO" -Message "Continuing with phase two in the current process because no files were changed"
        }
        else {
            Write-LogStep "Relaunching the updated updater..."
            & pwsh -File $currentScriptPath `
                -ContinueAfterSelfUpdate `
                -TargetDirectoryOverride $targetDirectory `
                -ClonedSourceDirectoryOverride $clonedSourceDirectory `
                -TemporaryRootOverride $temporaryRoot
            if ($LASTEXITCODE -ne 0) {
                throw "Relaunched updater failed with exit code $LASTEXITCODE."
            }

            Write-Log -Level "OK" -Message "Bootstrap phase completed"
            return
        }
    }

    $preservedFiles = [System.Collections.ArrayList]@()
    [string[]]$updatePhaseSkippedDirectories = @($skippedRelativeDirectories) + $selfUpdateDirectory
    $existingPreservedFiles = Get-ChildItem -Path $targetDirectory -Recurse -File -Filter $preserveFileName -ErrorAction SilentlyContinue
    if ($existingPreservedFiles) {
        foreach ($file in $existingPreservedFiles) {
            Add-PreservedFileBackup -PreservedFiles $preservedFiles -File $file -TargetDirectory $targetDirectory -TemporaryRoot $temporaryRoot -DryRun $dryRun
        }
        Write-Log -Level "OK" -Message "Preserved $($existingPreservedFiles.Count) existing $preserveFileName file(s)"
    }
    else {
        Write-Log -Level "WARN" -Message "No existing $preserveFileName files found in subfolders"
    }

    $releaseDeployPreserveFiles = @(Get-ReleaseDeployPreserveFiles -TargetDirectory $targetDirectory)
    if ($releaseDeployPreserveFiles.Count -gt 0) {
        foreach ($file in $releaseDeployPreserveFiles) {
            Add-PreservedFileBackup -PreservedFiles $preservedFiles -File $file -TargetDirectory $targetDirectory -TemporaryRoot $temporaryRoot -DryRun $dryRun
        }
        Write-Log -Level "OK" -Message "Preserved $($releaseDeployPreserveFiles.Count) release deploy file(s)"
    }

    $preservedRelativePaths = [System.Collections.Generic.HashSet[string]]::new([StringComparer]::OrdinalIgnoreCase)
    foreach ($preservedFile in $preservedFiles) {
        [void]$preservedRelativePaths.Add($preservedFile.RelativePath)
    }

    if ($dryRun) {
        Write-LogStep "Dry run summary"
        Write-Log -Level "INFO" -Message "Would remove all files under target except preserved $preserveFileName and release deploy files"
        Write-Log -Level "INFO" -Message "Would skip phase-two refresh for: $($updatePhaseSkippedDirectories -join ', ')"
        if ($omittedRelativeDirectories.Count -gt 0) {
            Write-Log -Level "INFO" -Message "Would omit (delete dest + skip copy): $($omittedRelativeDirectories -join ', ')"
        }
        Write-Log -Level "INFO" -Message "Would copy refreshed files from: $clonedSourceDirectory"
        if ($preservedFiles.Count -gt 0) {
            $preservedList = ($preservedFiles | ForEach-Object { $_.RelativePath }) -join ", "
            Write-Log -Level "INFO" -Message "Would restore preserved files: $preservedList"
        }
        Write-Log -Level "OK" -Message "Dry run completed. No files were modified."
        return
    }

    Write-LogStep "Cleaning target directory..."
    $filesToRemove = Get-ChildItem -Path $targetDirectory -Recurse -Force -File |
        Where-Object {
            $relativePath = [System.IO.Path]::GetRelativePath($targetDirectory, $_.FullName)
            $isInSkippedDirectory = Test-IsInRelativeDirectory -RelativePath $relativePath -Directories $updatePhaseSkippedDirectories
            $isInOmittedDirectory = ($omittedRelativeDirectories.Count -gt 0) -and
                (Test-IsInRelativeDirectory -RelativePath $relativePath -Directories $omittedRelativeDirectories)

            $_.Name -ne $preserveFileName -and
            -not $preservedRelativePaths.Contains($relativePath) -and
            (-not $isInSkippedDirectory -or $isInOmittedDirectory)
        }

    foreach ($file in $filesToRemove) {
        Remove-Item -Path $file.FullName -Force
    }

    $directoriesToRemove = Get-ChildItem -Path $targetDirectory -Recurse -Force -Directory |
        Sort-Object { $_.FullName.Length } -Descending

    foreach ($directory in $directoriesToRemove) {
        $relativePath = [System.IO.Path]::GetRelativePath($targetDirectory, $directory.FullName)
        $isInOmittedDirectory = ($omittedRelativeDirectories.Count -gt 0) -and
            (Test-IsInRelativeDirectory -RelativePath $relativePath -Directories $omittedRelativeDirectories)
        if ((Test-IsInRelativeDirectory -RelativePath $relativePath -Directories $updatePhaseSkippedDirectories) -and
            -not $isInOmittedDirectory) {
            continue
        }

        $remainingItems = Get-ChildItem -Path $directory.FullName -Force -ErrorAction SilentlyContinue
        if (-not $remainingItems) {
            Remove-Item -Path $directory.FullName -Force
        }
    }
    Write-Log -Level "OK" -Message "Target directory cleaned"

    Write-LogStep "Copying refreshed source files..."
    $sourceFilesToCopy = Get-ChildItem -Path $clonedSourceDirectory -Recurse -Force -File |
        Where-Object {
            $relativePath = [System.IO.Path]::GetRelativePath($clonedSourceDirectory, $_.FullName)
            $isInSkippedDirectory = Test-IsInRelativeDirectory -RelativePath $relativePath -Directories $updatePhaseSkippedDirectories
            $isInOmittedDirectory = ($omittedRelativeDirectories.Count -gt 0) -and
                (Test-IsInRelativeDirectory -RelativePath $relativePath -Directories $omittedRelativeDirectories)

            -not $isInSkippedDirectory -and -not $isInOmittedDirectory
        }

    foreach ($sourceFile in $sourceFilesToCopy) {
        $relativePath = [System.IO.Path]::GetRelativePath($clonedSourceDirectory, $sourceFile.FullName)
        $destinationPath = Join-Path $targetDirectory $relativePath
        $destinationDirectory = Split-Path -Parent $destinationPath
        if (-not (Test-Path -Path $destinationDirectory -PathType Container)) {
            New-Item -ItemType Directory -Path $destinationDirectory -Force | Out-Null
        }

        Copy-Item -Path $sourceFile.FullName -Destination $destinationPath -Force
    }

    foreach ($skippedDirectory in $updatePhaseSkippedDirectories) {
        $skippedSourcePath = Join-Path $clonedSourceDirectory $skippedDirectory
        if (Test-Path -Path $skippedSourcePath) {
            Write-Log -Level "INFO" -Message "Skipped refresh for $skippedDirectory"
        }
    }
    foreach ($omittedDirectory in $omittedRelativeDirectories) {
        Write-Log -Level "INFO" -Message "Omitted (not shipped): $omittedDirectory"
    }
    Write-Log -Level "OK" -Message "Source files copied"

    if ($preservedFiles.Count -gt 0) {
        foreach ($preservedFile in $preservedFiles) {
            if (-not (Test-Path -Path $preservedFile.BackupPath -PathType Leaf)) {
                continue
            }

            $restorePath = Join-Path $targetDirectory $preservedFile.RelativePath
            $restoreDirectory = Split-Path -Parent $restorePath
            if (-not (Test-Path -Path $restoreDirectory -PathType Container)) {
                New-Item -ItemType Directory -Path $restoreDirectory -Force | Out-Null
            }

            Copy-Item -Path $preservedFile.BackupPath -Destination $restorePath -Force
        }
        Write-Log -Level "OK" -Message "Preserved files restored ($($preservedFiles.Count))"
    }

    Write-Log -Level "OK" -Message "========================================"
    Write-Log -Level "OK" -Message "Update completed successfully!"
    Write-Log -Level "OK" -Message "========================================"
}
finally {
    if ($ownsTemporaryRoot -and (Test-Path -Path $temporaryRoot)) {
        Remove-Item -Path $temporaryRoot -Recurse -Force -ErrorAction SilentlyContinue
    }
}

#endregion
