# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## v2.0.0 - 2026-02-22

### Added
- Added contribution guidelines and documented release workflow.
- Added shared PowerShell utility modules for configuration, logging, git operations, and test execution.
- Added automation scripts for release publishing, force-amending tagged commits, and coverage badge generation.
- Added generated coverage badge assets.
- Added missing client-error factory methods for standard 4xx status codes in `Result` and `Result<T>` APIs.

### Changed
- Updated core and test dependencies, including migration to `xunit.v3`.
- Included `README.md`, `LICENSE.md`, and `CHANGELOG.md` in NuGet package content.
- Added coverage badges to the README and linked badge assets from `assets/badges/`.
- Updated solution metadata for newer Visual Studio format.
- Organized HTTP status factory methods into `Common` and `Extended Or Less Common` regions across informational, success, client-error, and server-error result files for improved readability.
- Updated package target framework to `.NET 10` (`net10.0`).

### Fixed
- Improved RFC 7807 `ProblemDetails` JSON serialization with explicit field names, deterministic output order, null omission for optional fields, and extension-data serialization.

### Removed
- Removed `.NET 8` target support; package now targets `.NET 10` only.

<!-- 
Template for new releases:

## v1.x.x

### Added
- New features

### Changed
- Changes in existing functionality

### Deprecated
- Soon-to-be removed features

### Removed
- Removed features

### Fixed
- Bug fixes

### Security
- Security improvements
-->
