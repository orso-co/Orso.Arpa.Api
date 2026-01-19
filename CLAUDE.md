# CLAUDE.md - Projekt-Kontext für Claude Code

## Projekt-Übersicht

**Orso.Arpa.Api** - Backend-API für ARPA (Artistic Resource Planning Application)
- .NET 10 Web API
- PostgreSQL Datenbank
- GraphQL (HotChocolate) + REST
- Entity Framework Core

## Branch-Struktur

| Branch | Zweck | Deploy-Ziel |
|--------|-------|-------------|
| `develop` | Hauptentwicklung | Azure Dev, GitHub Actions CI |
| `master` | Produktion | Azure Prod |
| `raspi-prod` | Raspberry Pi Produktion | raspi3:/home/wolf/arpa-prod |
| `raspi-dev` | Raspberry Pi Development | raspi3:/home/wolf/arpa |

## Deployment-Umgebungen

### Azure (Produktion)
- **Prod:** orso-arpa-prod-infra-pgs (PostgreSQL 16)
- **Dev:** orso-arpa-dev-infra-pgs (PostgreSQL 16)
- Pipelines: `build-prod.yml`, `build-dev.yml`, `build-staging.yml`

### Raspberry Pi 3 (raspi3)
- **Host:** r3.loopus.it:2222 (SSH)
- **Lokal:** 192.168.1.59 (SSH Port 22 deaktiviert)
- **Deployments:**
  - `/home/wolf/arpa` → raspi-dev Branch
  - `/home/wolf/arpa-prod` → raspi-prod Branch
- **PostgreSQL:** 16-alpine (Docker)
- **Workflow:** `.github/workflows/raspi-deploy.yml`

### Lokal (Entwicklung)
- `docker-compose.yml` im Repo
- PostgreSQL 18-alpine (Container) oder 17.x (manuell)

## Wichtige Dateien

- `Orso.Arpa.Api/Startup.cs` - App-Konfiguration
- `Orso.Arpa.Api/appsettings.*.json` - Umgebungs-Konfiguration
- `Dockerfile` - Multi-Stage Build für Alpine
- `.github/workflows/` - CI/CD Pipelines

## Bekannte Eigenheiten

### Dockerfile (Alpine)
Das Alpine-basierte .NET Image benötigt zusätzliche Pakete:
```dockerfile
RUN apk add --no-cache curl krb5-libs icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
```

### Tests
- `UserAccessorTests`: ConcurrencyStamp muss auf allen Ebenen excluded werden:
  ```csharp
  .Excluding(ctx => ctx.Path.EndsWith("ConcurrencyStamp"))
  ```
- `ExportAppointmentsToIcsHandlerTests`: ical.net Version im erwarteten Output anpassen bei Updates

### Azurite (Azure Storage Emulator)
Für neuere Azure.Storage.Blobs Versionen:
```bash
azurite --skipApiVersionCheck
```

## Häufige Aufgaben

### Renovate PRs bearbeiten
1. PR prüfen und CI-Status checken
2. Bei Test-Fehlern: Version-Strings oder Mocks anpassen
3. Merge wenn grün

### Deploy nach raspi3
```bash
git checkout raspi-prod
git merge develop
git push origin raspi-prod
# Workflow läuft automatisch
```

### Manueller Container-Neustart auf raspi3
```bash
ssh -p 2222 wolf@r3.loopus.it
cd /home/wolf/arpa  # oder arpa-prod
docker pull ghcr.io/orso-co/orso-arpa-api:raspi-prod
docker compose up -d --force-recreate api
```
