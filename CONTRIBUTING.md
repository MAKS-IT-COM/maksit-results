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

Orchestration lives in **`utils/`** (from [maksit-repoutils](https://github.com/MAKS-IT-COM/maksit-repoutils)).

### Prerequisites

- .NET SDK, PowerShell 7+, Git, GitHub CLI (`gh`)
- Environment variables (names match logical secrets in `scriptSettings.json`):
  - **`GitHub`** — GitHub token (`repo` scope)
  - **`NuGet`** — NuGet.org API key

| Entry | Purpose |
|-------|---------|
| `utils\Invoke-TestEngine.bat` | Tests and coverage badges |
| `utils\Invoke-ReleasePackage-Single.bat` | Release (build, test, pack, publish) |
| `utils\Update-RepoUtils.bat` | Sync engines from maksit-repoutils |
| `utils\Force-AmendTaggedCommit.bat` | Amend last tagged commit |

### Workflow

1. Bump `<Version>` in `src/MaksIT.Results/MaksIT.Results.csproj` and **CHANGELOG.md**
2. Commit, tag `vX.Y.Z` on `main`
3. Set `$env:GitHub`, `$env:NuGet`, run `utils\Invoke-ReleasePackage-Single.bat`

Dry-run: `pwsh -File utils\engines\release\Invoke-ReleasePackage.ps1 -DryRun`

Configuration: `utils/engines/release/scriptSettings.json`, `utils/engines/test/scriptSettings.json`

## License

By contributing, you agree that your contributions are licensed under the terms in `LICENSE.md`.
