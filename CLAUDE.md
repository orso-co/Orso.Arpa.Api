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

### Raspberry Pi 5 (raspi3)
- **Host:** r3.loopus.it:2222 (SSH)
- **Lokal:** 192.168.1.59 (SSH Port 22 deaktiviert)
- **Deployments:**
  - `/home/wolf/arpa` → raspi-dev Branch
  - `/home/wolf/arpa-prod` → raspi-prod Branch
- **PostgreSQL:** 16-alpine (Docker)
- **CI/CD:**
  - **GitHub:** `.github/workflows/raspi-deploy.yml` (baut auf GitHub, pusht zu ghcr.io)
  - **GitLab:** `.gitlab-ci.yml` (baut nativ auf raspi3, schneller)

### GitLab CI/CD (git.loopus.it)
Eingerichtet 01.02.2026. Alternative zu GitHub Actions, baut direkt auf raspi3.

**GitLab Repo:** https://git.loopus.it/arpa/arpa-api

**Remote hinzufügen:**
```bash
git remote add gitlab "https://Wolf:TOKEN@git.loopus.it/arpa/arpa-api.git"
```

**Deployment via GitLab:**
```bash
git push gitlab raspi-dev   # → Deploy auf arpa.loopus.it
git push gitlab raspi-prod  # → Deploy auf arpax.loopus.it
```

**Vorteile gegenüber GitHub Actions:**
- Baut nativ auf ARM64 (kein QEMU Cross-Compile)
- Schneller (~3 Min statt ~8 Min)
- Keine GitHub-Abhängigkeit
- Lokale Kontrolle

**Pipeline-Logs:** https://git.loopus.it/arpa/arpa-api/-/pipelines

### Lokal (Entwicklung)
- `docker-compose.yml` im Repo
- PostgreSQL 18-alpine (Container) oder 17.x (manuell)
- **WICHTIG: Port 5000 ist durch macOS AirPlay belegt!**
  - Backend läuft auf Port **5001** (HTTPS) oder **5002** (HTTP)
  - Niemals Port 5000 für lokale Entwicklung verwenden

## Entwicklungs-Workflow (WICHTIG!)

**Reihenfolge einhalten - keine Abkürzungen!**

### 1. Lokal entwickeln und testen
```
1. Datenbank starten (Docker oder lokal)
2. Migrationen anwenden: dotnet ef database update
3. Backend starten und testen
4. Frontend starten und testen
5. Alle Änderungen committen
```

### 2. Nach raspi-dev deployen
```
git push origin raspi-dev
# Workflow baut und deployed automatisch
# Andere Mitarbeiter testen auf arpa.loopus.it
```

### 3. Nach raspi-prod deployen (erst nach erfolgreichem Test!)
```
git checkout raspi-prod
git merge raspi-dev
git push origin raspi-prod
```

**Niemals direkt nach raspi-dev/prod pushen ohne lokalen Test!**
**Niemals Datenbank-Änderungen manuell machen ohne Migration im Code!**

## Datenbank-Migrationen (KRITISCH!)

### Goldene Regel
> **NIEMALS Datenbank-Tabellen manuell erstellen oder ändern.**
> Immer nur über EF Core Migrationen.

Die API führt `Database.Migrate()` beim Start automatisch aus (siehe `Startup.cs:678`).

### Warum das wichtig ist
Wenn Tabellen manuell erstellt werden:
1. EF Core sieht "Tabellen existieren" und markiert Migration als erledigt
2. Aber das manuelle Schema kann **falsch** sein (andere Spalten, Typen, etc.)
3. AutoMapper-Fehler und 500er zur Laufzeit, extrem schwer zu debuggen

### Korrekter Ablauf bei neuem Feature mit DB-Änderungen
```bash
# 1. Lokal: Entity/DbContext ändern
# 2. Migration erstellen
dotnet ef migrations add FeatureName -p Orso.Arpa.Persistence -s Orso.Arpa.Api

# 3. Lokal testen
dotnet ef database update -p Orso.Arpa.Persistence -s Orso.Arpa.Api

# 4. Code + Migration committen und pushen
git add . && git commit -m "Add FeatureName" && git push origin raspi-dev

# 5. API startet neu → Migration wird automatisch angewendet
# 6. Testen auf arpa.loopus.it
# 7. Nach raspi-prod mergen wenn OK
```

### Bei Azure-Dump-Import auf Raspi
```bash
# 1. Dump importieren (überschreibt alles)
pg_restore ...

# 2. API Container neustarten - fehlende Migrationen werden automatisch angewendet
docker restart arpa-prod-api

# 3. Logs prüfen ob Migrationen durchgelaufen sind
docker logs arpa-prod-api 2>&1 | head -50
```

### Schema-Probleme debuggen
Wenn unerklärliche 500er auftreten, Schema zwischen Umgebungen vergleichen:
```bash
# Spalten einer Tabelle anzeigen
docker exec <postgres-container> psql -U postgres -d orso-arpa -c \
  "SELECT column_name, data_type FROM information_schema.columns WHERE table_name = 'tabelle' ORDER BY ordinal_position;"

# Alle Tabellen auflisten
docker exec <postgres-container> psql -U postgres -d orso-arpa -c \
  "SELECT table_name FROM information_schema.tables WHERE table_schema = 'public' ORDER BY table_name;"
```

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

## Features

### Support-Anfrage (Januar 2026)

Ermöglicht Nutzern, die Probleme beim Login haben, eine Support-Anfrage zu senden.

**Frontend:**
- Route: `/auth/support`
- Zugang über "Hilfe anfordern" Button auf der Login-Seite
- Formular mit: Vorname, Nachname, E-Mail (Pflicht), Benutzername (optional), Nachricht, Themen-Checkboxen
- Themen: Login fehlgeschlagen, Passwort-Mail kommt nicht an, Benutzername vergessen, Sonstiges

**Backend:**
- Endpoint: `POST /api/auth/support` (ohne Authentifizierung)
- DTO: `SupportRequestDto` (Orso.Arpa.Application/AuthApplication/Model/)
- Command: `SendSupportRequest` (Orso.Arpa.Domain/UserDomain/Commands/)
- E-Mail-Template: `Support_Request.html` (Orso.Arpa.Mail/Templates/Html/)

**Konfiguration:**
- `ClubConfiguration.SupportEmail` in appsettings.json definiert den Empfänger
- Aktuell: `support@arpa.orso.co`

**Dateien:**
- Frontend: `src/app/domains/auth/feature-support/`
- Backend Controller: `AuthController.cs` → `SendSupportRequest()`
- Backend Service: `AuthService.cs` → `SendSupportRequestAsync()`
