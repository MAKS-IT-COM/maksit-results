# Contributing to MaksIT.Results

Thank you for your interest in contributing to `MaksIT.Results`.

## Getting Started

1. Fork the repository.
2. Clone your fork locally.
3. Create a feature branch.
4. Implement and test your changes.
5. Submit a pull request to `main`.

## Development Setup

### Prerequisites

- .NET 8 SDK or later
- Git
- PowerShell 7+ (recommended for utility scripts)

### Build

```bash
cd src
dotnet build MaksIT.Results.sln
```

### Test

```bash
cd src
dotnet test MaksIT.Results.Tests
```

## Commit Message Format

Use:

```text
(type): description
```

### Commit Types

| Type | Description |
|------|-------------|
| `(feature):` | New feature or enhancement |
| `(bugfix):` | Bug fix |
| `(refactor):` | Refactoring without behavior change |
| `(chore):` | Maintenance tasks (dependencies, tooling, docs) |

### Guidelines

- Use lowercase in the description.
- Keep it concise and specific.
- Do not end with a period.

## Pull Request Checklist

1. Ensure build and tests pass.
2. Update `README.md` if behavior or usage changed.
3. Update `CHANGELOG.md` under the target version.
4. Keep changes scoped and explain rationale in the PR description.

## Versioning

This project follows [Semantic Versioning](https://semver.org/):

- **MAJOR**: breaking API changes
- **MINOR**: backward-compatible features
- **PATCH**: backward-compatible fixes

## Utility Scripts

Scripts are located under `utils/`.

### Generate Coverage Badges

Runs tests with coverage and generates SVG badges in `assets/badges/`.

```powershell
.\utils\Generate-CoverageBadges\Generate-CoverageBadges.ps1
```

Configuration: `utils/Generate-CoverageBadges/scriptsettings.json`

### Release NuGet Package

Builds, tests, packs, and publishes to NuGet and GitHub release flows.

```powershell
.\utils\Release-NuGetPackage\Release-NuGetPackage.ps1
```

Prerequisites:

- Docker Desktop (for Linux test validation)
- GitHub CLI (`gh`)
- environment variable `NUGET_MAKS_IT`
- environment variable `GITHUB_MAKS_IT_COM`

Configuration: `utils/Release-NuGetPackage/scriptsettings.json`

### Force Amend Tagged Commit

Amends the latest tagged commit and force-pushes updated branch and tag.

```powershell
.\utils\Force-AmendTaggedCommit\Force-AmendTaggedCommit.ps1
.\utils\Force-AmendTaggedCommit\Force-AmendTaggedCommit.ps1 -DryRun
```

Warning: this rewrites git history.

## License

By contributing, you agree that your contributions are licensed under the terms in `LICENSE.md`.
