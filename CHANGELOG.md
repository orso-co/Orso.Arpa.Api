# Changelog

Alle wichtigen Änderungen an diesem Projekt werden in dieser Datei dokumentiert.

Das Format basiert auf [Keep a Changelog](https://keepachangelog.com/de/1.0.0/).

## [Unreleased]

### Changed
- Upgrade auf .NET 10
- PostgreSQL Docker Image auf 18-alpine (nur lokale Entwicklung)
- Azure DevOps Pipelines auf .NET 10 SDK
- GitHub Actions auf .NET 10 SDK

### Updated Dependencies
- Azure.Storage.Blobs 12.26.0 → 12.27.0
- ical.net 5.1.1 → 5.2.0
- MicroElements.Swashbuckle.FluentValidation → 7.0.3
- NUnit3TestAdapter → 6.x
- HotChocolate Monorepo → 15.1.12
- NLog → 6.0.7

### Fixed
- Flaky Test `Should_Get_Current_User` - ConcurrencyStamp Exclusion auf alle Ebenen erweitert
- Dockerfile Entrypoint-Script für Alpine-Kompatibilität (printf statt echo)
- Alpine: krb5-libs für PostgreSQL Kerberos-Support hinzugefügt
- Alpine: icu-libs für Globalisierung/Lokalisierung hinzugefügt
- Azurite `--skipApiVersionCheck` Flag für neuere Azure.Storage.Blobs Versionen
- ical.net Versionsstring in Tests aktualisiert

### Infrastructure
- Merge von raspi-prod Features nach develop:
  - File validation for profile picture uploads
  - LocalStorageProfilePictureAccessor
  - LocalProfilePictureProvider
  - WarmupService
  - Remove hardcoded credentials from appsettings
  - Bulk appointments optimization mit parallel execution
