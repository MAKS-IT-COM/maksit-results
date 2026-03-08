# Contributing to MaksIT.Results

Thank you for your interest in contributing to MaksIT.Results! This document provides guidelines for contributing to the project.

## Getting Started

1. Fork the repository
2. Clone your fork locally
3. Create a new branch for your changes
4. Make your changes
5. Submit a pull request

## Development Setup

### Prerequisites

- .NET 10 SDK or later
- Git

### Building the Project

```bash
cd src
dotnet build MaksIT.Results.slnx
```

### Running Tests

```bash
cd src
dotnet test MaksIT.Results.Tests
```

## Commit Message Format

This project uses the following commit message format:

```
(type): description
```

### Commit Types

| Type | Description |
|------|-------------|
| `(feature):` | New feature or enhancement |
| `(bugfix):` | Bug fix |
| `(refactor):` | Code refactoring without functional changes |
| `(perf):` | Performance improvement without changing behavior |
| `(test):` | Add or update tests |
| `(docs):` | Documentation-only changes |
| `(build):` | Build system, dependencies, packaging, or project file changes |
| `(ci):` | CI/CD pipeline or automation changes |
| `(style):` | Formatting or non-functional code style changes |
| `(revert):` | Revert a previous commit |
| `(chore):` | General maintenance tasks that do not fit the types above |

### Examples

```
(feature): add support for custom json options in object result
(bugfix): fix objectresult using app json options when request services null
(refactor): simplify result to action result conversion
(perf): reduce allocations in problem details serialization
(test): add coverage for addjsonoptions whenwritingnull
(docs): clarify json options in readme
(build): update package metadata in MaksIT.Results.csproj
(ci): update GitHub Actions workflow for .NET 10
(style): normalize using directives in mvc tests
(revert): revert breaking change in toactionresult behavior
(chore): update copyright year to 2026
```

### Guidelines

- Use lowercase for the description
- Keep the description concise but descriptive
- No period at the end of the description

## Code Style

- Follow standard C# naming conventions
- Use XML documentation comments for public APIs
- Keep methods focused and single-purpose
- Write unit tests for new functionality

## Pull Request Process

1. Ensure all tests pass
2. Update documentation if needed
3. Update CHANGELOG.md with your changes under the appropriate version section
4. Submit your pull request against the `main` branch

## Versioning

This project follows [Semantic Versioning](https://semver.org/):

- **MAJOR** - Breaking changes
- **MINOR** - New features (backward compatible)
- **PATCH** - Bug fixes (backward compatible)

## Release Process

The release process is automated via PowerShell scripts in the `utils/` directory.

### Prerequisites

- Docker Desktop running (for Linux tests)
- GitHub CLI (`gh`) installed
- Environment variables configured:
  - `NUGET_MAKS_IT` - NuGet.org API key
  - `GITHUB_MAKS_IT_COM` - GitHub Personal Access Token (needs `repo` scope)

### Release Scripts Overview

| Script | Purpose |
|--------|---------|
| `Generate-CoverageBadges.ps1` | Runs tests with coverage and generates SVG badges in `assets/badges/` |
| `Release-Package.ps1` | Build, test, and publish to NuGet.org and GitHub |
| `Force-AmendTaggedCommit.ps1` | Fix mistakes in tagged commits |

### Release Workflow

1. **Update version** in `MaksIT.Results/MaksIT.Results.csproj`

2. **Update CHANGELOG.md** with your changes under the target version

3. **Review and commit** all changes:
   ```bash
   git add -A
   git commit -m "(chore): release v2.x.x"
   ```

4. **Create version tag**:
   ```bash
   git tag v2.x.x
   ```

5. **Run release script**:
   ```powershell
   .\utils\Release-Package\Release-Package.ps1          # Full release
   .\utils\Release-Package\Release-Package.ps1 -DryRun   # Test without publishing
   ```

---

### Generate-CoverageBadges.ps1

Runs tests with coverage and generates SVG badges in `assets/badges/`.

**Usage:**
```powershell
.\utils\Generate-CoverageBadges\Generate-CoverageBadges.ps1
```

**Configuration:** `utils/Generate-CoverageBadges/scriptsettings.json`

---

### Release-Package.ps1

Builds, tests, packs, and publishes the package to NuGet.org and GitHub.

**What it does:**
1. Validates prerequisites and environment
2. Builds and tests the project
3. Creates NuGet package (.nupkg and .snupkg)
4. Pushes to NuGet.org
5. Creates GitHub release with assets

**Usage:**
```powershell
.\utils\Release-Package\Release-Package.ps1          # Full release
.\utils\Release-Package\Release-Package.ps1 -DryRun  # Test without publishing
```

**Configuration:** `utils/Release-Package/scriptsettings.json`

---

### Force-AmendTaggedCommit.ps1

Fixes mistakes in the last tagged commit by amending it and force-pushing.

**When to use:**
- You noticed an error after committing and tagging
- Need to add forgotten files to the release commit
- Need to fix a typo in the release

**What it does:**
1. Verifies the last commit has an associated tag
2. Stages all pending changes
3. Amends the latest commit (keeps existing message)
4. Deletes and recreates the tag on the amended commit
5. Force pushes the branch and tag to origin

**Usage:**
```powershell
.\utils\Force-AmendTaggedCommit\Force-AmendTaggedCommit.ps1          # Amend and force push
.\utils\Force-AmendTaggedCommit\Force-AmendTaggedCommit.ps1 -DryRun   # Preview without changes
```

**Warning:** This rewrites history. Only use on commits that haven't been pulled by others.

---

### Fixing a Failed Release

If the release partially failed (e.g., NuGet succeeded but GitHub failed):

1. **Re-run the release script** if it supports skipping already-completed steps
2. **If you need to fix the commit content:**
   ```powershell
   # Make your fixes, then:
   .\utils\Force-AmendTaggedCommit\Force-AmendTaggedCommit.ps1
   .\utils\Release-Package\Release-Package.ps1
   ```

## License

By contributing, you agree that your contributions are licensed under the terms in `LICENSE.md`.
