# .NET 10 Upgrade - 19. Januar 2026

## Übersicht

Upgrade von .NET 9 auf .NET 10 mit allen abhängigen Änderungen und Fixes.

## Durchgeführte Änderungen

### 1. Framework-Upgrade

Alle `.csproj` Dateien aktualisiert:
```xml
<TargetFramework>net10.0</TargetFramework>
```

Betroffene Projekte:
- Orso.Arpa.Api
- Orso.Arpa.Application
- Orso.Arpa.Domain
- Orso.Arpa.Infrastructure
- Orso.Arpa.Mail
- Orso.Arpa.Misc
- Orso.Arpa.Persistence
- Alle Test-Projekte

### 2. Package-Updates (via Renovate)

| Package | Alt | Neu |
|---------|-----|-----|
| Azure.Storage.Blobs | 12.26.0 | 12.27.0 |
| ical.net | 5.1.1 | 5.2.0 |
| MicroElements.Swashbuckle.FluentValidation | 6.x | 7.0.3 |
| Microsoft.AspNetCore.* | 9.x | 10.0.2 |
| Microsoft.EntityFrameworkCore.* | 9.x | 10.0.2 |
| HotChocolate.* | 15.1.11 | 15.1.12 |
| NLog | 6.0.6 | 6.0.7 |

### 3. CI/CD Pipeline Updates

#### GitHub Actions (`.github/workflows/dotnet.yml`)
```yaml
- name: Setup .NET 10
  uses: actions/setup-dotnet@v5
  with:
    dotnet-version: 10.0.x
```

#### Azure DevOps (`build-*.yml`)
```yaml
targetFramework: '10.0'
```

### 4. Dockerfile Fixes für Alpine

Das Alpine-basierte .NET Runtime-Image benötigt zusätzliche Pakete:

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:10.0-alpine

# Erforderliche Bibliotheken
RUN apk add --no-cache curl krb5-libs icu-libs

# Globalisierung aktivieren
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
```

**Probleme die behoben wurden:**
- `exec /publish/entrypoint.sh: no such file or directory` → printf statt echo für Entrypoint-Script
- `Cannot load library libgssapi_krb5.so.2` → krb5-libs hinzugefügt
- `Only the invariant culture is supported` → icu-libs + ENV Variable

### 5. Test-Fixes

#### UserAccessorTests (Flaky Test)
```csharp
// Vorher - excluded nur auf Root-Ebene
.Excluding(u => u.ConcurrencyStamp)

// Nachher - excluded auf allen Ebenen (inkl. nested UserRoles[].Role.ConcurrencyStamp)
.Excluding(ctx => ctx.Path.EndsWith("ConcurrencyStamp"))
```

#### ExportAppointmentsToIcsHandlerTests
```csharp
// ical.net Version im erwarteten Output aktualisiert
"PRODID:-//github.com/ical-org/ical.net//NONSGML ical.net 5.2.0//EN"
```

#### Azurite Kompatibilität
```yaml
# GitHub Actions
- name: Run Azurite in Background
  run: azurite --skipApiVersionCheck &
```

### 6. Branch-Synchronisation

```
develop ← raspi-prod (Merge der raspi-spezifischen Features)
raspi-prod ← develop (Sync der Renovate-Updates + Fixes)
```

**Features von raspi-prod nach develop übernommen:**
- File validation for profile picture uploads
- LocalStorageProfilePictureAccessor
- LocalProfilePictureProvider
- WarmupService
- Remove hardcoded credentials from appsettings
- Bulk appointments optimization
- Health check curl in Dockerfile

## Deployment-Status

| Umgebung | Status | Version |
|----------|--------|---------|
| GitHub Actions CI | ✅ Grün | .NET 10 |
| Azure DevOps | ✅ Konfiguriert | .NET 10 |
| raspi3 (arpa) | ✅ Deployed | .NET 10 |
| raspi3 (arpa-prod) | ⚠️ Image verfügbar | .NET 10 |

## Offene Punkte

- [ ] raspi-dev Branch auf develop synchronisieren
- [ ] PostgreSQL Upgrade (16 → 18) für Produktionsumgebungen evaluieren
- [ ] Azure Prod Deployment verifizieren

## Lessons Learned

1. **Alpine Images** brauchen explizite Pakete für Features die in Debian-basierten Images enthalten sind
2. **Entrypoint-Scripts** sollten mit `printf` statt `echo` erstellt werden für Shell-Kompatibilität
3. **FluentAssertions** `Excluding()` mit Lambda arbeitet nur auf Root-Ebene - für nested Properties `Path.EndsWith()` verwenden
4. **Azurite** muss mit `--skipApiVersionCheck` gestartet werden für neuere Azure SDK Versionen
