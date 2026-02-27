<#
.SYNOPSIS
    Release script for MaksIT.Core NuGet package and GitHub release.

.DESCRIPTION
    This script automates the release process for MaksIT.Core library.
    The script is IDEMPOTENT - you can safely re-run it if any step fails.
    It will skip already-completed steps (NuGet and GitHub) and only create what's missing.
    
    Features:
    - Validates environment and prerequisites
    - Checks if version already exists on NuGet.org (skips if released)
    - Checks if GitHub release exists (skips if released)
    - Scans for vulnerable packages (security check)
    - Builds and tests the project (Windows + Linux via Docker)
    - Collects code coverage with Coverlet (threshold enforcement optional)
    - Generates test result artifacts (TRX format) and coverage reports
    - Displays test results with pass/fail counts and coverage percentage
    - Publishes to NuGet.org
    - Creates a GitHub release with changelog and NuGet package assets
    - Shows timing summary for all steps

.REQUIREMENTS
    Environment Variables:
    - NUGET_MAKS_IT        : NuGet.org API key for publishing packages
    - GITHUB_MAKS_IT_COM   : GitHub Personal Access Token (needs 'repo' scope)

    Tools (Required):
    - dotnet CLI           : For building, testing, and packing
    - git                  : For version control operations
    - gh (GitHub CLI)      : For creating GitHub releases
    - docker               : For cross-platform Linux testing

.WORKFLOW
    1. VALIDATION PHASE
       - Check required environment variables (NuGet key, GitHub token)
       - Check required tools are installed (dotnet, git, gh, docker)
       - Verify no uncommitted changes in working directory
       - Authenticate GitHub CLI

    2. VERSION & RELEASE CHECK PHASE (Idempotent)
       - Read latest version from CHANGELOG.md
       - Find commit with matching version tag
       - Validate tag is on configured release branch (from scriptsettings.json)
       - Check if already released on NuGet.org (mark for skip if yes)
       - Check if GitHub release exists (mark for skip if yes)
       - Read target framework from MaksIT.Core.csproj
       - Extract release notes from CHANGELOG.md for current version

    3. SECURITY SCAN
       - Check for vulnerable packages (dotnet list package --vulnerable)
       - Fail or warn based on $failOnVulnerabilities setting

    4. BUILD & TEST PHASE
       - Clean previous builds (delete bin/obj folders)
       - Restore NuGet packages
       - Windows: Build main project -> Build test project -> Run tests with coverage
       - Analyze code coverage (fail if below threshold when configured)
       - Linux (Docker): Build main project -> Build test project -> Run tests (TRX report)
       - Rebuild for Windows (Docker may overwrite bin/obj)
       - Create NuGet package (.nupkg) and symbols (.snupkg)
       - All steps are timed for performance tracking

    5. CONFIRMATION PHASE
       - Display release summary
       - Prompt user for confirmation before proceeding

    6. NUGET RELEASE PHASE (Idempotent)
       - Skip if version already exists on NuGet.org
       - Otherwise, push package to NuGet.org

    7. GITHUB RELEASE PHASE (Idempotent)
       - Skip if release already exists
       - Push tag to remote if not already there
       - Create GitHub release with:
         * Release notes from CHANGELOG.md
         * .nupkg and .snupkg as downloadable assets

    8. COMPLETION PHASE
       - Display timing summary for all steps
       - Display test results summary
       - Display success summary with links
       - Open NuGet and GitHub release pages in browser
       - TODO: Email notification (template provided)
       - TODO: Package signing (template provided)

.USAGE
    Before running:
    1. Ensure Docker Desktop is running (for Linux tests)
    2. Update version in MaksIT.Core.csproj
    3. Run .\Generate-Changelog.ps1 to update CHANGELOG.md and LICENSE.md
    4. Review and commit all changes
    5. Create version tag: git tag v1.x.x
    6. Run: .\Release-NuGetPackage.ps1
    
    Note: The script finds the commit with the tag matching CHANGELOG.md version.
    You can run it from any branch/commit - it releases the tagged commit.

    Re-run release (idempotent - skips NuGet/GitHub if already released):
        .\Release-NuGetPackage.ps1

    Generate changelog and update LICENSE year:
        .\Generate-Changelog.ps1

.CONFIGURATION
    All settings are stored in scriptsettings.json:
    - qualityGates: Coverage threshold, vulnerability checks
    - packageSigning: Code signing certificate configuration
    - emailNotification: SMTP settings for release notifications

.NOTES
    Author: Maksym Sadovnychyy (MAKS-IT)
    Repository: https://github.com/MAKS-IT-COM/maksit-core
#>

# No parameters - behavior is controlled by current branch (configured in scriptsettings.json):
# - dev branch     -> Local build only (no tag required, uncommitted changes allowed)
# - release branch -> Full release to GitHub (tag required, clean working directory)

# Get the directory of the current script (for loading settings and relative paths)
$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path

#region Import Modules

# Import TestRunner module
$utilsDir = Split-Path $scriptDir -Parent

$testRunnerModulePath = Join-Path $utilsDir "TestRunner.psm1"
if (-not (Test-Path $testRunnerModulePath)) {
    Write-Error "TestRunner module not found at: $testRunnerModulePath"
    exit 1
}

Import-Module $testRunnerModulePath -Force

# Import ScriptConfig module
$scriptConfigModulePath = Join-Path $utilsDir "ScriptConfig.psm1"
if (-not (Test-Path $scriptConfigModulePath)) {
    Write-Error "ScriptConfig module not found at: $scriptConfigModulePath"
    exit 1
}

Import-Module $scriptConfigModulePath -Force

# Import Logging module
$loggingModulePath = Join-Path $utilsDir "Logging.psm1"
if (-not (Test-Path $loggingModulePath)) {
    Write-Error "Logging module not found at: $loggingModulePath"
    exit 1
}

Import-Module $loggingModulePath -Force


# Import GitTools module
$gitToolsModulePath = Join-Path $utilsDir "GitTools.psm1"
if (-not (Test-Path $gitToolsModulePath)) {
    Write-Error "GitTools module not found at: $gitToolsModulePath"
    exit 1
}

Import-Module $gitToolsModulePath -Force

#endregion

#region Load Settings
$settings = Get-ScriptSettings -ScriptDir $scriptDir

#endregion

#region Configuration

# GitHub configuration
$githubReleseEnabled = $settings.github.enabled
$githubTokenEnvVar = $settings.github.githubToken
$githubToken = [System.Environment]::GetEnvironmentVariable($githubTokenEnvVar)

# NuGet configuration
$nugetReleseEnabled = $settings.nuget.enabled
$nugetApiKeyEnvVar = $settings.nuget.nugetApiKey
$nugetApiKey = [System.Environment]::GetEnvironmentVariable($nugetApiKeyEnvVar)
$nugetSource = if ($settings.nuget.source) { $settings.nuget.source } else { "https://api.nuget.org/v3/index.json" }

# Paths from settings (resolve relative to script directory)
$csprojPaths = @()
$rawCsprojPaths = @()

if ($settings.paths.csprojPaths) {
    if ($settings.paths.csprojPaths -is [System.Collections.IEnumerable] -and -not ($settings.paths.csprojPaths -is [string])) {
        $rawCsprojPaths += $settings.paths.csprojPaths
    }
    else {
        $rawCsprojPaths += $settings.paths.csprojPaths
    }
}
else {
    Write-Error "No csproj path configured. Set 'paths.csprojPaths' (preferred) or 'paths.csprojPath' in scriptsettings.json."
    exit 1
}

foreach ($path in $rawCsprojPaths) {
    if ([string]::IsNullOrWhiteSpace($path)) {
        continue
    }

    $resolvedPath = [System.IO.Path]::GetFullPath((Join-Path $scriptDir $path))
    $csprojPaths += $resolvedPath
}

if ($csprojPaths.Count -eq 0) {
    Write-Error "No valid csproj paths configured in scriptsettings.json."
    exit 1
}

$testResultsDir = [System.IO.Path]::GetFullPath((Join-Path $scriptDir $settings.paths.testResultsDir))
$releaseDir = [System.IO.Path]::GetFullPath((Join-Path $scriptDir $settings.paths.releaseDir))
$changelogPath = [System.IO.Path]::GetFullPath((Join-Path $scriptDir $settings.paths.changelogPath))
$testProjectPath = [System.IO.Path]::GetFullPath((Join-Path $scriptDir $settings.paths.testProject))

# Release naming pattern
$zipNamePattern = $settings.release.zipNamePattern
$releaseTitlePattern = $settings.release.releaseTitlePattern

# Branch configuration
$releaseBranch = $settings.branches.release
$devBranch = $settings.branches.dev

#endregion

#region Helpers

# Helper: extract a csproj property (first match)
function Get-CsprojPropertyValue {
    param(
        [Parameter(Mandatory=$true)][xml]$csproj,
        [Parameter(Mandatory=$true)][string]$propertyName
    )

    $propNode = $csproj.Project.PropertyGroup |
        Where-Object { $_.$propertyName } |
        Select-Object -First 1

    if ($propNode) {
        return $propNode.$propertyName
    }

    return $null
}

# Helper: check for uncommitted changes
function Assert-WorkingTreeClean {
    param(
        [Parameter(Mandatory = $true)]
        [bool]$IsReleaseBranch
    )

    $gitStatus = Get-GitStatusShort
    if ($gitStatus) {
        if ($IsReleaseBranch) {
            Write-Error "Working directory has uncommitted changes. Commit or stash them before releasing."
            Write-Log -Level "WARN" -Message "Uncommitted files:"
            $gitStatus | ForEach-Object { Write-Log -Level "WARN" -Message "  $_" }
            exit 1
        }
        else {
            Write-Log -Level "WARN" -Message "  Uncommitted changes detected (allowed on dev branch)."
        }
    }
    else {
        Write-Log -Level "OK" -Message "  Working directory is clean."
    }
}

# Helper: read versions from csproj files
function Get-CsprojVersions {
    param(
        [Parameter(Mandatory = $true)]
        [string[]]$CsprojPaths
    )

    Write-Log -Level "INFO" -Message "Reading version(s) from csproj(s)..."
    $projectVersions = @{}

    foreach ($projPath in $CsprojPaths) {
        if (-not (Test-Path $projPath -PathType Leaf)) {
            Write-Error "Csproj file not found at: $projPath"
            exit 1
        }

        if ([System.IO.Path]::GetExtension($projPath) -ne ".csproj") {
            Write-Error "Configured path is not a .csproj file: $projPath"
            exit 1
        }

        [xml]$csproj = Get-Content $projPath
        $version = Get-CsprojPropertyValue -csproj $csproj -propertyName "Version"

        if (-not $version) {
            Write-Error "Version not found in $projPath"
            exit 1
        }

        $projectVersions[$projPath] = $version
        Write-Log -Level "OK" -Message "  $([System.IO.Path]::GetFileName($projPath)): $version"
    }

    return $projectVersions
}

#endregion

#region Validate CLI Dependencies

Assert-Command dotnet
Assert-Command git
Assert-Command docker
# gh command check deferred until after branch detection (only needed on release branch)

#endregion

#region Main

Write-Log -Level "STEP" -Message "=================================================="
Write-Log -Level "STEP" -Message "RELEASE BUILD"
Write-Log -Level "STEP" -Message "=================================================="

#region Preflight

$isDevBranch = $false
$isReleaseBranch = $false

# 1. Detect current branch and determine release mode
$currentBranch = Get-CurrentBranch

$isDevBranch = $currentBranch -eq $devBranch
$isReleaseBranch = $currentBranch -eq $releaseBranch

if (-not $isDevBranch -and -not $isReleaseBranch) {
    Write-Error "Releases can only be created from '$releaseBranch' or '$devBranch' branches. Current branch: $currentBranch"
    exit 1
}

# 2. Check for uncommitted changes (required on release branch, allowed on dev)
Assert-WorkingTreeClean -IsReleaseBranch:$isReleaseBranch

# 3. Get version from csproj (source of truth)
$projectVersions = Get-CsprojVersions -CsprojPaths $csprojPaths

# Use the first project's version as the release version
$version = $projectVersions[$csprojPaths[0]]

# 4. Handle tag based on branch
if ($isReleaseBranch) {
    # Release branch: tag is required and must match version
    $tag = Get-CurrentCommitTag -Version $version
    
    if ($tag -notmatch '^v(\d+\.\d+\.\d+)$') {
        Write-Error "Tag '$tag' does not match expected format 'vX.Y.Z' (e.g., v$version)."
        exit 1
    }

    $tagVersion = $Matches[1]
    
    if ($tagVersion -ne $version) {
        Write-Error "Tag version ($tagVersion) does not match csproj version ($version)."
        Write-Log -Level "WARN" -Message "  Either update the tag or the csproj version."
        exit 1
    }

    Write-Log -Level "OK" -Message "  Tag found: $tag (matches csproj)"
}
else {
    # Dev branch: no tag required, use version from csproj
    $tag = "v$version"
    Write-Log -Level "INFO" -Message "  Using version from csproj (no tag required on dev)."
}

# 5. Verify CHANGELOG.md has matching version entry
Write-Log -Level "INFO" -Message "Verifying CHANGELOG.md..."
if (-not (Test-Path $changelogPath)) {
    Write-Error "CHANGELOG.md not found at: $changelogPath"
    exit 1
}

$changelog = Get-Content $changelogPath -Raw

if ($changelog -notmatch '##\s+v(\d+\.\d+\.\d+)') {
    Write-Error "No version entry found in CHANGELOG.md"
    exit 1
}

$changelogVersion = $Matches[1]

if ($changelogVersion -ne $version) {
    Write-Error "Csproj version ($version) does not match latest CHANGELOG.md version ($changelogVersion)."
    Write-Log -Level "WARN" -Message "  Update CHANGELOG.md or the csproj version."
    exit 1
}

Write-Log -Level "OK" -Message "  CHANGELOG.md version matches: v$changelogVersion"



Write-Log -Level "OK" -Message "All pre-flight checks passed!"

#endregion

#region Test

Write-Log -Level "STEP" -Message "Running tests..."

# Run tests using TestRunner module
$testResult = Invoke-TestsWithCoverage -TestProjectPath $testProjectPath -ResultsDirectory $testResultsDir -Silent

if (-not $testResult.Success) {
    Write-Error "Tests failed. Release aborted."
    Write-Log -Level "ERROR" -Message "  Error: $($testResult.Error)"
    exit 1
}

Write-Log -Level "OK" -Message "  All tests passed!"
Write-Log -Level "INFO" -Message "  Line Coverage:   $($testResult.LineRate)%"
Write-Log -Level "INFO" -Message "  Branch Coverage: $($testResult.BranchRate)%"
Write-Log -Level "INFO" -Message "  Method Coverage: $($testResult.MethodRate)%"

#endregion

#region Build And Publish

# 7. Prepare release directory
if (!(Test-Path $releaseDir)) {
    New-Item -ItemType Directory -Path $releaseDir | Out-Null
}


# 8. Pack NuGet package and resolve produced .nupkg/.snupkg files
$packageProjectPath = $csprojPaths[0]
Write-Log -Level "STEP" -Message "Packing NuGet package..."
dotnet pack $packageProjectPath -c Release -o $releaseDir --nologo `
    -p:IncludeSymbols=true `
    -p:SymbolPackageFormat=snupkg
if ($LASTEXITCODE -ne 0) {
    Write-Error "dotnet pack failed for $packageProjectPath."
    exit 1
}

$packageFile = Get-ChildItem -Path $releaseDir -Filter "*.nupkg" |
    Where-Object {
        $_.Name -like "*$version*.nupkg" -and
        $_.Name -notlike "*.symbols.nupkg" -and
        $_.Name -notlike "*.snupkg"
    } |
    Sort-Object LastWriteTime -Descending |
    Select-Object -First 1

if (-not $packageFile) {
    Write-Error "Could not locate generated NuGet package for version $version in: $releaseDir"
    exit 1
}

Write-Log -Level "OK" -Message "  Package ready: $($packageFile.FullName)"

# Find the symbols package if available
$symbolsPackageFile = Get-ChildItem -Path $releaseDir -Filter "*.snupkg" |
    Where-Object { $_.Name -like "*$version*.snupkg" } |
    Sort-Object LastWriteTime -Descending |
    Select-Object -First 1

if ($symbolsPackageFile) {
    Write-Log -Level "OK" -Message "  Symbols package ready: $($symbolsPackageFile.FullName)"
}
else {
    Write-Log -Level "WARN" -Message "  Symbols package (.snupkg) not found for version $version."
}

# 9. Create release archive with NuGet package artifacts
Write-Log -Level "STEP" -Message "Creating release archive..."
$resolvedZipNamePattern = if ([string]::IsNullOrWhiteSpace($zipNamePattern)) { "release-{version}.zip" } else { $zipNamePattern }
$zipFileName = $resolvedZipNamePattern -replace '\{version\}', $version
$zipPath = Join-Path $releaseDir $zipFileName

if (Test-Path $zipPath) {
    Remove-Item $zipPath -Force
}

$archiveArtifacts = @($packageFile.FullName)
if ($symbolsPackageFile) {
    $archiveArtifacts += $symbolsPackageFile.FullName
}

Compress-Archive -Path $archiveArtifacts -DestinationPath $zipPath -CompressionLevel Optimal -Force

if (-not (Test-Path $zipPath)) {
    Write-Error "Failed to create release archive at: $zipPath"
    exit 1
}

Write-Log -Level "OK" -Message "  Release archive ready: $zipPath"

# 10. Extract release notes from CHANGELOG.md
Write-Log -Level "STEP" -Message "Extracting release notes..."
$pattern = "(?ms)^##\s+v$([regex]::Escape($version))\b.*?(?=^##\s+v\d+\.\d+\.\d+|\Z)"
$match = [regex]::Match($changelog, $pattern)

if (-not $match.Success) {
    Write-Error "Changelog entry for version $version not found."
    exit 1
}

$releaseNotes = $match.Value.Trim()
Write-Log -Level "OK" -Message "  Release notes extracted."

# 11. Get repository info
$configuredGithubRepository = $settings.github.repository
$repoSource = $null

if (-not [string]::IsNullOrWhiteSpace($configuredGithubRepository)) {
    $repoSource = $configuredGithubRepository.Trim()
}
else {
    $remoteUrl = git config --get remote.origin.url
    if ($LASTEXITCODE -ne 0 -or -not $remoteUrl) {
        Write-Error "Could not determine git remote origin URL."
        exit 1
    }

    $repoSource = $remoteUrl
}

if ($repoSource -match "(?i)github\.com[:/](?<owner>[^/]+)/(?<repo>[^/.]+)(\.git)?$") {
    $owner = $matches['owner']
    $repoName = $matches['repo']
    $repo = "$owner/$repoName"
}
elseif ($repoSource -match "^(?<owner>[^/]+)/(?<repo>[^/]+)$") {
    $owner = $matches['owner']
    $repoName = $matches['repo']
    $repo = "$owner/$repoName"
}
else {
    Write-Error "Could not parse GitHub repo from source: $repoSource. Use 'github.repository' in scriptsettings.json (owner/repo or github URL)."
    exit 1
}

$releaseName = $releaseTitlePattern -replace '\{version\}', $version

Write-Log -Level "STEP" -Message "Release Summary:"
Write-Log -Level "INFO" -Message "  Repository: $repo"
Write-Log -Level "INFO" -Message "  Tag: $tag"
Write-Log -Level "INFO" -Message "  Title: $releaseName"

# 12. Check if tag is pushed to remote (skip on dev branch)

if (-not $isDevBranch) {

    Write-Log -Level "STEP" -Message "Verifying tag is pushed to remote..."
    $remoteTagExists = Test-RemoteTagExists -Tag $tag -Remote "origin"
    if (-not $remoteTagExists) {
        Write-Log -Level "WARN" -Message "  Tag $tag not found on remote. Pushing..."
        Push-TagToRemote -Tag $tag -Remote "origin"
    }
    else {
        Write-Log -Level "OK" -Message "  Tag exists on remote."
    }



    # Release to GitHub
    if ($githubReleseEnabled) {

        Write-Log -Level "STEP" -Message "  Release branch ($releaseBranch) - will publish to GitHub."
        Assert-Command gh

        # 6. Check GitHub authentication
       
        Write-Log -Level "INFO" -Message "Checking GitHub authentication..."
        if (-not $githubToken) {
            Write-Error "GitHub token is not set. Set '$githubTokenEnvVar' and rerun."
            exit 1
        }

        # gh release subcommands do not support custom auth headers.
        # Scope GH_TOKEN to this block so commands authenticate with the configured token.
        $previousGhToken = $env:GH_TOKEN
        $env:GH_TOKEN = $githubToken

        try {
            $ghVersion = & gh --version 2>&1
            if ($ghVersion) {
                Write-Log -Level "INFO" -Message "  gh version: $($ghVersion[0])"
            }

            Write-Log -Level "INFO" -Message "  Auth env var: $githubTokenEnvVar (set)"
            Write-Log -Level "INFO" -Message "  Target repository: $repo"

            # Validate that the provided token can access the target repository.
            $authArgs = @("api", "repos/$repo", "--jq", ".full_name")
            Write-Log -Level "INFO" -Message "  Running: gh $($authArgs -join ' ')"
            $authOutput = & gh @authArgs 2>&1
            $authExitCode = $LASTEXITCODE

            if ($authExitCode -ne 0 -or [string]::IsNullOrWhiteSpace(($authOutput | Out-String))) {
                Write-Log -Level "WARN" -Message "  gh auth check failed (exit code: $authExitCode)."
                if ($authOutput) {
                    Write-Log -Level "WARN" -Message "  gh api output:"
                    $authOutput | ForEach-Object { Write-Log -Level "WARN" -Message "    $_" }
                }

                $authStatus = & gh auth status --hostname github.com 2>&1
                if ($authStatus) {
                    Write-Log -Level "WARN" -Message "  gh auth status output:"
                    $authStatus | ForEach-Object { Write-Log -Level "WARN" -Message "    $_" }
                }

                Write-Error "GitHub CLI authentication failed for repository '$repo'. Ensure '$githubTokenEnvVar' is valid and has access to this repository."
                exit 1
            }

            Write-Log -Level "OK" -Message "  GitHub token validated for repository: $($authOutput | Select-Object -First 1)"

            # 13. Create or update GitHub release
            Write-Log -Level "STEP" -Message "Creating GitHub release..."

            # Check if release already exists
            $releaseViewArgs = @(
                "release", "view", $tag,
                "--repo", $repo
            )
            & gh @releaseViewArgs 2>$null

            if ($LASTEXITCODE -eq 0) {
                Write-Log -Level "WARN" -Message "  Release $tag already exists. Deleting..."
                $releaseDeleteArgs = @("release", "delete", $tag, "--repo", $repo, "--yes")
                & gh @releaseDeleteArgs
                if ($LASTEXITCODE -ne 0) {
                    Write-Error "Failed to delete existing release $tag."
                    exit 1
                }
            }

            # Create release using the existing tag
            # Write release notes to a temp file to avoid shell interpretation issues with special characters
            $notesFilePath = Join-Path $releaseDir "release-notes-temp.md"
            [System.IO.File]::WriteAllText($notesFilePath, $releaseNotes, [System.Text.UTF8Encoding]::new($false))

            $releaseAssets = @($packageFile.FullName)
            if ($symbolsPackageFile) {
                $releaseAssets += $symbolsPackageFile.FullName
            }

            $createReleaseArgs = @("release", "create", $tag) + $releaseAssets + @(
                "--repo", $repo
                "--title", $releaseName
                "--notes-file", $notesFilePath
            )
            & gh @createReleaseArgs

            $ghExitCode = $LASTEXITCODE

            # Cleanup temp notes file
            if (Test-Path $notesFilePath) {
                Remove-Item $notesFilePath -Force
            }

            if ($ghExitCode -ne 0) {
                Write-Error "Failed to create GitHub release for tag $tag."
                exit 1
            }
        }
        finally {
            if ($null -ne $previousGhToken) {
                $env:GH_TOKEN = $previousGhToken
            }
            else {
                Remove-Item Env:GH_TOKEN -ErrorAction SilentlyContinue
            }
        }

        Write-Log -Level "OK" -Message "  GitHub release created successfully."
    }
    else {
        Write-Log -Level "WARN" -Message "Skipping GitHub release (disabled)."
    }


    # Release to NuGet

    if ($nugetReleseEnabled) {
        Write-Log -Level "STEP" -Message "Pushing to NuGet.org..."
        dotnet nuget push $packageFile.FullName -k $nugetApiKey -s $nugetSource --skip-duplicate

        if ($LASTEXITCODE -ne 0) {
            Write-Error "Failed to push the package to NuGet."
            exit 1
        }

        Write-Log -Level "OK" -Message "  NuGet push completed."
    }
    else {
        Write-Log -Level "WARN" -Message "Skipping NuGet publish (disabled)."
    }

}
else {
    Write-Log -Level "WARN" -Message "Skipping remote tag verification and GitHub release (dev branch)."
}

#endregion

#region Cleanup
if (Test-Path $testResultsDir) {
    Remove-Item $testResultsDir -Recurse -Force
    Write-Log -Level "INFO" -Message "  Cleaned up test results directory."
}

Get-ChildItem -Path $releaseDir -File |
    Where-Object { $_.Name -like "*$version*.nupkg" -or $_.Name -like "*$version*.snupkg" } |
    Remove-Item -Force -ErrorAction SilentlyContinue
#endregion

#region Summary
Write-Log -Level "OK" -Message "=================================================="
if ($isDevBranch) {
    Write-Log -Level "OK" -Message "DEV BUILD COMPLETE"
}
else {
    Write-Log -Level "OK" -Message "RELEASE COMPLETE"
}
Write-Log -Level "OK" -Message "=================================================="

if (-not $isDevBranch) {
    Write-Log -Level "STEP" -Message "Release URL: https://github.com/$repo/releases/tag/$tag"
}

Write-Log -Level "INFO" -Message "Artifacts location: $releaseDir"

if ($isDevBranch) {
    Write-Log -Level "WARN" -Message "To publish to GitHub, switch to '$releaseBranch', merge dev, tag, and run this script again:"
    Write-Log -Level "WARN" -Message "  git checkout $releaseBranch"
    Write-Log -Level "WARN" -Message "  git merge dev"
    Write-Log -Level "WARN" -Message "  git tag v$version"
    Write-Log -Level "WARN" -Message "  .\Release-NuGetPackage.ps1"
}

#endregion

#endregion
