# CLAUDE.md - Projekt-Kontext für Claude Code

## Projekt-Übersicht

**Orso.Arpa.Api** - Backend-API für ARPA (Artistic Resource Planning Application)
- .NET 10 Web API
- PostgreSQL Datenbank
- GraphQL (HotChocolate) + REST
- Entity Framework Core

## Branch-Struktur (seit 19.02.2026)

| Branch | Zweck | Deploy-Ziel |
|--------|-------|-------------|
| `develop` | Hauptentwicklung | raspi3 DEV (arpa.loopus.it:8080) + Azure Dev |
| `main` | Produktion | raspi3 PROD (arpax.loopus.it:8082) + Azure Prod |

**Alte Branches (`raspi-dev`, `raspi-prod`, `master`) sind obsolet.**

## Deployment-Umgebungen

### Azure (Produktion)
- **Prod:** orso-arpa-prod-infra-pgs (PostgreSQL 16)
- **Dev:** orso-arpa-dev-infra-pgs (PostgreSQL 16)
- Pipelines: `build-prod.yml`, `build-dev.yml`, `build-staging.yml`

### Raspberry Pi 5 (raspi3)
- **Host:** r3.loopus.it:2222 (SSH)
- **Lokal:** 192.168.1.59 (SSH Port 22 deaktiviert)
- **Deployments:**
  - `/home/wolf/arpa` → develop Branch (DEV)
  - `/home/wolf/arpa-prod` → main Branch (PROD)
- **PostgreSQL:** 16-alpine (Docker)
- **CI/CD:**
  - **GitHub:** `.github/workflows/raspi-deploy.yml` (baut auf GitHub, pusht zu ghcr.io)
  - **GitLab:** `.gitlab-ci.yml` (baut nativ auf raspi3, schneller)

### GitLab CI/CD (git.loopus.it)
Eingerichtet 01.02.2026. Alternative zu GitHub Actions, baut direkt auf raspi3.

**GitLab Repo:** https://git.loopus.it/arpa/arpa-api

**Pipeline Stages:**
1. `security` - Snyk Vulnerability Scanning
2. `quality` - SonarCloud Code Analysis
3. `build-and-deploy` - Docker Build + Deployment auf raspi3

**CI/CD Variablen (Settings → CI/CD → Variables):**
| Variable | Beschreibung |
|----------|--------------|
| `SNYK_TOKEN` | Snyk API Token (von app.snyk.io) |
| `SONAR_TOKEN` | SonarCloud Token (von sonarcloud.io) |
| `SSH_PRIVATE_KEY_B64` | Deploy-Key (base64) |
| `SSH_HOST` | r3.loopus.it |
| `SSH_USERNAME` | wolf |

**Remote hinzufügen:**
```bash
git remote add gitlab "https://Wolf:TOKEN@git.loopus.it/arpa/arpa-api.git"
```

**Deployment via GitLab:**
```bash
git push gitlab develop  # → Deploy auf arpa.loopus.it (DEV)

# PROD:
git checkout main && git merge develop && git push gitlab main && git checkout develop
```

**Vorteile gegenüber GitHub Actions:**
- Baut nativ auf ARM64 (kein QEMU Cross-Compile)
- Schneller (~3 Min statt ~8 Min)
- Keine GitHub-Abhängigkeit
- Lokale Kontrolle

**Pipeline-Logs:** https://git.loopus.it/arpa/arpa-api/-/pipelines

### Lokal (Entwicklung)
- `docker-compose.yml` im Repo (PostgreSQL 18-alpine, MailHog, Azurite)
- Backend: `dotnet run` auf Port **5080** (siehe `launchSettings.json`)
- Frontend: `ng serve` auf Port **4200** (proxy auf 5080)
- **WICHTIG:** Homebrew PostgreSQL muss gestoppt sein (`brew services stop postgresql@14`), sonst blockiert sie Port 5432 und der Docker-Container ist nicht erreichbar.

#### Lokale Umgebungen

| Umgebung | Backend | Frontend | DB | Zweck |
|----------|---------|----------|----|-------|
| **Localhost** | `dotnet run` (Port 5080) | `ng serve` (Port 4200) | Docker PostgreSQL (Port 5432) | Entwicklung, offline-fähig |
| **DEV** | arpa.loopus.it:8080 | arpa.loopus.it | raspi3 PostgreSQL | Integration-Test nach Deploy |
| **PROD** | arpax.loopus.it:8082 | arpax.loopus.it | raspi3 PostgreSQL | Live für echte User |

#### Hilfs-Skripte (`scripts/`)

| Skript | Beschreibung |
|--------|-------------|
| `local-backend.sh` | Backend starten mit allen Env-Vars (ConnectionString, JWT, Seed-Passwort) |
| `local-db-reset.sh` | Lokale DB droppen + neu erstellen (Backend-Start wendet Migrationen + Seed an) |
| `local-db-sync-dev.sh` | DEV-Dump von raspi3 holen und lokal importieren |
| `local-reset-passwords.sh` | Lockout der Seed-User zurücksetzen |

#### Seed-User (Development)

| Username | Passwort | Rollen |
|----------|----------|--------|
| `admin` | `Pa$$w0rd` | Admin |
| `performer` | `Pa$$w0rd` | Performer |
| `staff` | `Pa$$w0rd` | Staff |
| `testwolf` | `Pa$$w0rd` | Admin + Staff + Performer |

`testwolf` wird durch `SeedTestWolf: true` in `appsettings.Development.json` aktiviert.

#### Schnellstart (frische DB)

```bash
brew services stop postgresql@14        # Homebrew-PostgreSQL stoppen!
docker compose up -d postgres mail       # DB + Mail starten
./scripts/local-db-reset.sh              # DB droppen + neu erstellen
./scripts/local-backend.sh               # Backend (Migrationen + Seed automatisch)
cd ../orso-arpa-web-extended && ng serve  # Frontend
# Login: testwolf / Pa$$w0rd (alle Rollen)
```

#### Alternativ: Mit DEV-Daten

```bash
brew services stop postgresql@14
docker compose up -d postgres mail
./scripts/local-db-sync-dev.sh           # DEV-Dump holen (echte Daten)
./scripts/local-backend.sh               # Migrationen + Seed laufen automatisch
cd ../orso-arpa-web-extended && ng serve
# Login: TestWolf / Pa$$w0rd (alle Rollen, DEV-Username mit Großbuchstaben)
```

## Entwicklungs-Workflow (WICHTIG!)

**Reihenfolge einhalten - keine Abkürzungen!**

### 1. Lokal entwickeln und testen
```
1. Homebrew PostgreSQL stoppen: brew services stop postgresql@14
2. Docker starten: docker compose up -d postgres mail
3. DB befüllen: ./scripts/local-db-reset.sh (frisch) oder ./scripts/local-db-sync-dev.sh (DEV-Daten)
4. Backend starten: ./scripts/local-backend.sh
5. Frontend starten: cd ../orso-arpa-web-extended && ng serve
6. Alle Änderungen committen
```

### 2. Nach DEV deployen
```
git push gitlab develop
# Pipeline baut und deployed automatisch
# Andere Mitarbeiter testen auf arpa.loopus.it
```

### 3. Nach PROD deployen (erst nach erfolgreichem Test!)
```
git checkout main
git merge develop
git push gitlab main
git checkout develop
```

**Niemals direkt nach PROD pushen ohne Test auf DEV!**
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
# 2. Migration erstellen (IMMER mit dotnet ef — NIEMALS .cs manuell schreiben!)
dotnet ef migrations add FeatureName -p Orso.Arpa.Persistence -s Orso.Arpa.Api --context ArpaContext
# → Erstellt 3 Dateien: Migration.cs, Migration.Designer.cs, ArpaContextModelSnapshot.cs
# → ALLE 3 DATEIEN COMMITTEN! Ohne Designer.cs erkennt EF Core die Migration nicht.

# 3. Lokal testen mit frischer DB
./scripts/local-db-reset.sh
./scripts/local-backend.sh

# 4. Code + Migration committen und pushen
git add . && git commit -m "Add FeatureName" && git push gitlab develop

# 5. API startet neu → Migration wird automatisch angewendet
# 6. Testen auf arpa.loopus.it
# 7. Nach PROD mergen wenn OK
```

### Bekanntes Problem: Migrations ohne Designer.cs (behoben 21.02.2026)

3 Migrationen für PersonMembership wurden manuell (per Write-Tool) erstellt statt mit `dotnet ef migrations add`. Dadurch fehlte die Designer.cs und EF Core ignorierte sie. Frische DBs konnten nicht aufgebaut werden.

**Fix:** Konsolidiert zu `20260122100000_AddPersonMembershipConsolidated` (mit auto-generierter Designer.cs). DEV+PROD Migrations-History per SQL aktualisiert.

**Lektion:** Migrationen **IMMER** mit `dotnet ef migrations add` erstellen. Nie `.cs`-Dateien manuell schreiben — die Designer.cs (15.000+ Zeilen Model-Snapshot) kann nicht von Hand erstellt werden.

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
# DEV
git push gitlab develop

# PROD
git checkout main && git merge develop && git push gitlab main && git checkout develop
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

## Bekannte Fixes (Februar 2026)

### JWT Claims Mapping - 403 Forbidden nach Login (03.02.2026)

**Problem:** Nach erfolgreichem Login kamen 403-Fehler auf allen API-Endpoints die Rollen erfordern.

**Ursache:** .NET mappt standardmäßig JWT-Claims auf lange URIs:
- `"role"` → `"http://schemas.microsoft.com/ws/2008/06/identity/claims/role"`
- `"sub"` → `"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"`

Die Autorisierungs-Policies fanden die Rollen nicht mehr, weil sie nach dem kurzen Namen suchten.

**Fix in `Orso.Arpa.Api/Extensions/JwtBearerConfiguration.cs`:**
```csharp
return builder.AddJwtBearer(opt =>
{
    opt.SaveToken = true;
    opt.MapInboundClaims = false;  // ← WICHTIG: Kein Claim-Mapping!
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        // ... andere Settings ...

        // Mit MapInboundClaims = false explizit setzen:
        NameClaimType = "sub",
        RoleClaimType = "role",
    };
});
```

**Commits:**
- `fix(auth): Handle JWT claims with MapInboundClaims=false` (TokenAccessor)
- `fix(auth): Add MapInboundClaims=false to fix 403 errors` (JwtBearerConfiguration)

**WICHTIG:** Beide Fixes sind nötig:
1. `TokenAccessor.cs` prüft "nameid" UND ClaimTypes.NameIdentifier
2. `JwtBearerConfiguration.cs` setzt MapInboundClaims=false

### JWT Email Claim fehlte (19.02.2026)

**Problem:** Frontend Avatar-Dropdown zeigte Vornamen statt E-Mail-Adresse.

**Ursache:** `JwtGenerator.cs` hatte keinen `email`-Claim im Token.

**Fix in `Orso.Arpa.Infrastructure/Authentication/JwtGenerator.cs`:**
```csharp
var claims = new List<Claim>
{
    new Claim(JwtRegisteredClaimNames.NameId, user.UserName),
    new Claim(JwtRegisteredClaimNames.Name, user.DisplayName),
    new Claim(JwtRegisteredClaimNames.Email, user.Email),  // ← NEU
    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
    new Claim($"{_jwtConfiguration.Issuer}/person_id", user.PersonId.ToString())
};
```

**WICHTIG:** Nach Deploy müssen User sich neu einloggen — alte Tokens haben keinen Email-Claim.

### Account-Entsperrung nach fehlgeschlagenen Login-Versuchen

Bei zu vielen falschen Passwort-Eingaben wird der Account gesperrt.

**Entsperren via PostgreSQL:**
```bash
ssh -p 2222 wolf@r3.loopus.it
docker exec arpa-prod-postgres psql -U postgres -d orso-arpa -c \
  "UPDATE \"AspNetUsers\" SET lockout_end = NULL, access_failed_count = 0 WHERE email = 'user@example.com';"
```

### GraphQL war Staff-only → Chat-403 für Performer (21.02.2026)

**Problem:** Alle Nicht-Staff-User bekamen 403 → Redirect auf `/error/access-denied` beim Chat.

**Ursache:** `Startup.cs:713` — `endpoints.MapGraphQL().RequireAuthorization(Roles = RoleNames.Staff)`
- Chat-Dialoge (Personensuche) nutzten Apollo/GraphQL → 403 für Performer
- `auth.interceptor.ts` im Frontend redirectete bei 403 auf `/error/access-denied`

**Fix:** Neuer REST-Endpoint für Personensuche (ersetzt GraphQL in Chat-Dialogen):
- `GET /api/persons/search?query=xxx&take=50&hasAccount=true|false`
- Controller: `PersonsController.Search()` mit `[Authorize]` (kein Rollen-Check)
- Service: `PersonService.SearchAsync()` — LINQ-Query auf `_arpaContext.Persons`
- DTO: `PersonSearchResultDto` (id, givenName, surname, displayName, hasUser, userId)

**Wichtig:** `DisplayName` ist kein DB-Feld auf Person → im Select manuell berechnen:
```csharp
DisplayName = (p.GivenName + " " + p.Surname).Trim()
```

## Bekannte Fixes (Februar 2026, Fortsetzung)

### Chat: Gruppenchats als Direct gespeichert → 1:1-Chat öffnet Gruppe (24.02.2026)

**Problem:** Klick auf Chat-Icon bei Online-Users öffnete einen Gruppenchat (z.B. 280 Mitglieder) statt 1:1-Direktchat.

**Ursache:** `CreateGroupChatAsync` in `ChatService.cs` erstellte Gruppenräume mit `ChatRoomType.Direct` (es gab keinen Group-Typ). `CreateDirectChatAsync` suchte nach `Type==Direct` + beide User als Member → fand den großen Gruppenchat.

**Fix:**
1. Neuer Enum-Wert `ChatRoomType.Group = 5` in `Orso.Arpa.Domain/ChatDomain/Enums/ChatRoomType.cs`
2. `ChatRoomTypeDto.Group = 5` in `Orso.Arpa.Application/ChatApplication/Model/ChatDtos.cs`
3. `CreateGroupChatAsync` nutzt `ChatRoomType.Group` statt `Direct`
4. `CreateDirectChatAsync` prüft zusätzlich `Members.Count == 2`
5. `GetRoomDisplayNameAsync` behandelt `Group`-Typ separat

**PROD-Datenfix (nach Deploy nötig!):**
```sql
-- Bestehende Gruppenchats (>2 Mitglieder) von Direct (0) auf Group (5) umstellen
UPDATE chat_rooms SET type = 5
WHERE type = 0 AND NOT deleted
AND id IN (
  SELECT chat_room_id FROM chat_room_members
  WHERE NOT deleted GROUP BY chat_room_id HAVING COUNT(*) > 2
);
```

### ChatRoomType Enum (vollständig, Stand 24.02.2026)

| Wert | Name | Beschreibung |
|------|------|-------------|
| 0 | Direct | 1:1 Privatnachricht (genau 2 Mitglieder) |
| 1 | Project | Projekt-Gruppenchat |
| 2 | Global | Globaler Chat (alle User) |
| 3 | Todo | Chat für TODO-Kommentare |
| 4 | Entity | Chat verknüpft mit Entity (Ticket, Person, etc.) |
| 5 | Group | Manuell erstellter Gruppenchat |

**JSON-Serialisierung:** `JsonStringEnumConverter(SnakeCaseUpper)` → `"DIRECT"`, `"GROUP"`, etc.

### Chat Read-Receipts / Gelesen-Anzeige (24.02.2026)

**Feature:** Blaue/graue Häkchen (✓/✓✓) an eigenen Chat-Nachrichten.

**Konzept:** Raum-Level Tracking via `ChatRoomMember.LastReadAt` (kein per-Nachricht-Tracking, keine neue Migration).
Eine Nachricht gilt als "gelesen" wenn `message.SentAt <= otherMember.LastReadAt`.

**Backend-Änderungen:**
- `ChatMemberDto` hat neues Feld `LastReadAt` (gemappt aus `ChatRoomMember.LastReadAt`)
- `ChatController.MarkAsRead()` sendet jetzt SignalR `MessageRead` Event an alle Raum-Mitglieder
  ```csharp
  await _chatHubContext.Clients.Group($"chat_{roomId}")
      .SendAsync("MessageRead", new { RoomId = roomId, UserId = userId, ReadAt = DateTime.UtcNow });
  ```

**Frontend-Änderungen:**
- `ReadReceiptEvent` Interface in `chat.model.ts`
- `MessageRead` SignalR-Handler in `chat.service.ts` → `updateMemberLastReadAt()`
- Häkchen-Anzeige in `chat-messages.component.ts`:
  - ✓ grau = gesendet (default)
  - ✓✓ blau = gelesen (mindestens ein anderer Member hat `lastReadAt >= sentAt`)

**Status:** Auf develop gepusht, noch nicht auf PROD getestet.

## Features (Februar 2026)

### Mediathek Bridge-Endpoint (21.02.2026)

**Endpoint:** `GET /api/mediathek/check/{username}`
- Validiert `X-Bridge-Key` Header gegen `MediathekBridgeKey` in appsettings
- Ruft `MediathekService.CheckAccessByUsernameAsync()` auf
- Gibt `true`/`false` zurück (JSON)

**Konfiguration (appsettings.Production.json auf raspi3):**
```json
"MediathekBridgeKey": "-ifMAG-cFLOrhLryDtSBf19MYWdU62VOQkf7Ot7Ze6k"
```

**Genutzt von:** Mediathek AuthBridge auf Zelos neu (Freiburg) — prüft ob User Mediathek-Zugang hat bevor Jellyfin-Autologin durchgeführt wird.
