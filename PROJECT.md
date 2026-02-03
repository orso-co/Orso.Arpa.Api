# ARPA - Artists Relations & Projectmanagement Application

> Session-übergreifende Projektdokumentation für Claude Code

## Projektübersicht

**ARPA** (auch ital. für "Harfe") ist eine Web-App für Chor- & Orchestermanagement.
- Open-Source-Projekt
- Seit 2+ Jahren produktiv im Einsatz für ORSO
- Ziel: Tools wie Google Sheets, Dropbox, WhatsApp, Doodle ersetzen

**Links:**
- Projektseite: https://arpa.orso.berlin
- Produktiv (Azure): https://arpa.orso.co
- Test (raspi3): https://arpa.loopus.it

**GitHub Repositories:**
| Repo | Beschreibung | Status |
|------|--------------|--------|
| [Orso.Arpa.Api](https://github.com/orso-co/Orso.Arpa.Api) | Backend API (.NET 9) | Public, aktiv |
| [orso-arpa-web-extended](https://github.com/orso-co/orso-arpa-web-extended) | Frontend (Angular 19, Nx) | **Private**, aktiv |
| [Orso.Arpa.Web](https://github.com/orso-co/Orso.Arpa.Web) | Frontend (alt) | Public, veraltet |
| [orso-arpa-web](https://github.com/orso-co/orso-arpa-web) | Frontend (David da Silva) | Public, veraltet |
| [Orso.Arpa.Terraform](https://github.com/orso-co/Orso.Arpa.Terraform) | Azure IaC | Public |

**Lokale Klone:**
- API: `/Users/wolf/Projects/ARPA/Orso.Arpa.Api`
- Frontend: `/Users/wolf/WebstormProjects/orso-arpa-web-extended`

Lizenz: **EUPL-1.2** (European Union Public License)

---

## TechStack

### Backend (Orso.Arpa.Api)
| Komponente | Version | Notizen |
|------------|---------|---------|
| .NET | 9.x | Entwickelt von Mira Gutmann |
| PostgreSQL | - | Datenbank |
| GraphQL | - | API-Abfragesprache |
| MailHog | - | E-Mail-Testing (dev) |

### Frontend (orso-arpa-web-extended)
| Komponente | Version | Notizen |
|------------|---------|---------|
| Angular | 19.2.8 | Nx Monorepo |
| TypeScript | 5.8.x | |
| PrimeNG | 19.0.8 | UI-Library |
| Tailwind CSS | 4.1.4 | Styling |
| Apollo Client | 3.13.5 | GraphQL |
| FullCalendar | 6.x | Kalender |
| @auth0/angular-jwt | 5.2.0 | JWT-Auth |

### Infrastruktur
| Komponente | Technologie | Notizen |
|------------|-------------|---------|
| **Code-Qualität** | SonarCloud, Snyk, GitGuardian | Siehe unten |
| **Hosting (Prod)** | Azure | Migration geplant |
| **Hosting (Test)** | raspi3 (192.168.1.59) | Docker |
| **Doku/Tickets** | Atlassian (Jira/Confluence) | orso.atlassian.net |

---

## Repository-Struktur & Code-Qualität

### Zwei getrennte Repositories (Entscheidung 2026-01-16)

| Repository | Sichtbarkeit | LOC | Begründung |
|------------|--------------|-----|------------|
| **[Orso.Arpa.Api](https://github.com/orso-co/Orso.Arpa.Api)** | Public | ~27.000 | Open Source (EUPL-1.2), Community-Beiträge möglich |
| **[orso-arpa-web-extended](https://github.com/orso-co/orso-arpa-web-extended)** | Private | ~24.000 | Frontend-Code geschützt |

**Warum kein Monorepo?**
- SonarCloud Free-Tier: max 50.000 LOC für private Repos
- Zusammen wären es ~51.000 LOC → knapp über dem Limit
- Getrennt: API public (unbegrenzt kostenlos) + Frontend private (unter 50k)

### Code-Qualität Tools (alle kostenlos)

| Tool | Zweck | API (Public) | Frontend (Private) |
|------|-------|--------------|-------------------|
| **[SonarCloud](https://sonarcloud.io)** | Code Quality, Bugs, Smells | ✅ Unbegrenzt | ✅ < 50k LOC |
| **[Snyk](https://snyk.io)** | Dependency Security | ✅ Unbegrenzt | ✅ 400 Tests/Monat |
| **[GitGuardian](https://gitguardian.com)** | Secret Scanning | ✅ Kostenlos | ✅ < 25 Devs |

### LOC-Übersicht (Stand 2026-01-16)

**API Backend:**
| Kategorie | Lines of Code |
|-----------|---------------|
| Produktivcode | 32.177 |
| Tests | 24.941 |
| Migrations (generiert) | 486.899 |
| **SonarCloud zählt** | **~27.000** |

**Frontend:**
| Kategorie | Lines of Code |
|-----------|---------------|
| TypeScript | 15.010 |
| HTML | 6.666 |
| SCSS | 2.353 |
| **Gesamt** | **~24.000** |

---

## Team

| Rolle | Person | Bereich |
|-------|--------|---------|
| Product Owner / Frontend | Wolfgang Roese | Angular, TypeScript |
| Backend-Entwicklung | Mira Gutmann | C#, .NET (ehrenamtlich) |

---

## Development Workflow

### Branching-Strategie

```
master          ─────────────────────────────────────────────► (Releases/Produktion)
                        ↑
develop         ────●───●───●───●───●───●───●───●───●────────► (Hauptentwicklung)
                   ↑       ↑       ↑
feature/xyz     ──●───────●       │
                                  │
feature/abc     ─────────────────●
```

| Branch | Zweck | Schutz |
|--------|-------|--------|
| `master` | Produktionsreleases | Protected, nur via PR |
| `develop` | Hauptentwicklungsbranch | Protected, PR + Checks required |
| `feature/*` | Feature-Entwicklung | Kein Schutz |
| `renovate/*` | Automatische Dependency-Updates | Auto-PRs von Renovate Bot |

### CI/CD Pipeline

#### Bei Pull Request zu `develop`:

```
┌─────────────────┐     ┌─────────────────┐     ┌─────────────────┐
│  Dotnet Build   │     │   SonarCloud    │     │      Snyk       │
│  + Unit Tests   │     │  Code Quality   │     │    Security     │
│  + Integration  │     │                 │     │                 │
└────────┬────────┘     └────────┬────────┘     └────────┬────────┘
         │                       │                       │
         └───────────────────────┼───────────────────────┘
                                 │
                    ┌────────────▼────────────┐
                    │     GitGuardian         │
                    │   Secret Scanning       │
                    └────────────┬────────────┘
                                 │
                    ┌────────────▼────────────┐
                    │   PR Merge möglich      │
                    │  (wenn alle grün)       │
                    └─────────────────────────┘
```

| Workflow | Datei | Trigger | Runner | Dauer |
|----------|-------|---------|--------|-------|
| **Dotnet** | `dotnet.yml` | PR → develop | Windows | ~10 min |
| **SonarCloud** | `sonar.yml` | PR/Push → develop | Windows | ~5 min |
| **Snyk** | `snyk-security.yml` | PR/Push → develop | Ubuntu | ~1 min |
| **GitGuardian** | (GitHub App) | Alle Commits | - | ~5 sec |

#### Bei Push zu `feature/raspi-workflow`:

```
┌─────────────────┐     ┌─────────────────┐     ┌─────────────────┐
│  Docker Build   │────►│   Push to GHCR  │────►│  Deploy raspi3  │
│  (ARM64+AMD64)  │     │                 │     │  (SSH + Docker) │
└─────────────────┘     └─────────────────┘     └─────────────────┘
        ~15 min                ~2 min                  ~1 min
```

| Workflow | Datei | Trigger | Aktion |
|----------|-------|---------|--------|
| **Raspberry Pi Deploy** | `raspi-deploy.yml` | Push → feature/raspi-workflow | Multi-Arch Image → GHCR → raspi3 |

### GitLab CI/CD (seit 2026-02-03)

**Primäres CI/CD für Raspberry Pi Deployments.** GitHub Actions sind für das Frontend deaktiviert.

| Projekt | GitLab URL | GitHub Status |
|---------|------------|---------------|
| arpa-api | https://git.loopus.it/arpa/arpa-api | GitHub Actions aktiv (Azure) |
| arpa-web-extended | https://git.loopus.it/arpa/arpa-web-extended | **GitHub Actions deaktiviert** |

**Pipeline-Stages (Frontend):**
```
┌──────────┐   ┌──────────┐   ┌──────────┐   ┌──────────┐
│ security │──►│ quality  │──►│  build   │──►│  deploy  │
│  (Snyk)  │   │ (Sonar)  │   │ (ng build)│  │ (raspi3) │
└──────────┘   └──────────┘   └──────────┘   └──────────┘
```

| Branch | Deploy-Ziel | URL |
|--------|-------------|-----|
| `main` / `raspi-dev` | raspi3 Dev | https://arpa.loopus.it |
| `raspi-prod` | raspi3 Prod | https://arpax.loopus.it |

**Wichtig:** Der Angular Build läuft im GitLab Runner (node:22), nicht auf dem Raspberry Pi.
Tailwind CSS v4 funktioniert nicht zuverlässig in Alpine Docker Containern.

### Quality Gates

Alle Checks müssen grün sein bevor ein PR gemerged werden kann:

| Check | Tool | Kriterien |
|-------|------|-----------|
| **Build & Tests** | Dotnet | Kompiliert + alle Tests bestehen |
| **Code Quality** | SonarCloud | Keine neuen Bugs, Code Smells ≤ 0 |
| **Security** | Snyk | Keine neuen Vulnerabilities (medium+) |
| **Secrets** | GitGuardian | Keine Secrets im Code |

### Entwicklungs-Ablauf

#### 1. Feature entwickeln

```bash
# Neuen Branch von develop erstellen
git checkout develop
git pull
git checkout -b feature/mein-feature

# Entwickeln...
# Commits machen...

# Pushen
git push -u origin feature/mein-feature
```

#### 2. Pull Request erstellen

```bash
# Via CLI
gh pr create --base develop --title "feat: Mein neues Feature"

# Oder via GitHub Web UI
```

#### 3. Checks abwarten

- Dotnet Build + Tests (~10 min)
- SonarCloud Analysis (~5 min)
- Snyk Security Scan (~1 min)
- GitGuardian Secret Scan (~5 sec)

#### 4. Code Review

- Mindestens 1 Approving Review erforderlich
- Alle Checks müssen grün sein

#### 5. Merge

- Squash & Merge bevorzugt (saubere History)
- Branch nach Merge löschen

### Lokale Qualitätsprüfung (vor PR)

```bash
# Build testen
dotnet build

# Tests lokal ausführen
dotnet test

# Code formatieren (optional)
dotnet format
```

### Secrets & Konfiguration

| Secret | Verwendung | Gespeichert in |
|--------|------------|----------------|
| `SONAR_TOKEN` | SonarCloud API | GitHub Secrets |
| `SNYK_TOKEN` | Snyk API | GitHub Secrets |
| `SSH_HOST` | raspi3 Hostname | GitHub Secrets |
| `SSH_USERNAME` | raspi3 User | GitHub Secrets |
| `SSH_PRIVATE_KEY` | raspi3 SSH Key | GitHub Secrets |
| `JWT_TOKEN_KEY` | JWT Signing | GitHub Secrets |
| `POSTGRES_PASSWORD` | DB Passwort | GitHub Secrets |
| `ADMIN_PASSWORD` | Initial Admin | GitHub Secrets |
| `GHCR_PAT` | Container Registry | GitHub Secrets |

### Deployment-Ziele

| Umgebung | Trigger | URL | Datenbank |
|----------|---------|-----|-----------|
| **Test (raspi3)** | Push → feature/raspi-workflow | https://arpa.loopus.it | Testdaten |
| **Produktion (Azure)** | Manuell (Terraform) | https://arpa.orso.co | Produktionsdaten |
| **Produktion (raspi3)** | *Geplant* | https://arpa.orso.co | Migriert von Azure |

---

## Aktuelle Funktionen (ARPA 2.0)

- [x] Termin- & Raumplanung
- [x] Instrumente & Stimmverteilung (Musikerprofile)
- [x] Nachrichtenboard
- [x] Kalender
- [x] Teilnehmerstatus für Projekte/Proben
- [x] Rudimentäres CRM
- [x] Usermanagement
- [x] Casting-Funktionen für Leitungsteams

---

## Geplante Module

| Modul | Beschreibung | Priorität | Status |
|-------|--------------|-----------|--------|
| **Casting** | Besetzungsplaner für 200+ Orchester- & Chorpositionen | Prio 1 | TODO |
| **Bibliothek** | Automatische Notenverteilung basierend auf Profilen | Prio 2 | TODO |
| **Auditions** | Probespiel-Organisation, Video-Upload, Referenzen | Prio 3 | TODO |
| **Taskmanagement** | Todos an Personen/Termine anhängen, Microtasks | Prio 4 | TODO |
| **Controlling** | Einfache Budgetplanung pro Konzertprojekt | Prio 5 | TODO |

---

## Aktueller Stand: raspi3 (Test)

**Status:** Testversion läuft seit 2+ Wochen

### Docker Container (Test-Instanz)

| Container | Image | Port | Status |
|-----------|-------|------|--------|
| `arpa-frontend-1` | `ghcr.io/orso-co/arpa-frontend:latest` | 8080→80 | healthy |
| `arpa-api-1` | `orso-arpa-api:local` ¹ | 5000 | healthy |
| `arpa-postgres-1` | `postgres:16-alpine` | 5432 (intern) | running |
| `arpa-mail-1` | `jcalonso/mailhog:latest` | 8025 | running |

¹ Lokales Image mit curl für Health Checks (GHCR ist privat, daher manueller Build)

### Verzeichnisstruktur auf raspi3

```
/home/wolf/
├── arpa/                      # Docker-Compose Deployment
│   ├── docker-compose.yml     # API + Postgres + MailHog
│   ├── .env                   # JWT_TOKEN_KEY
│   ├── appsettings.RaspberryPi.json
│   ├── nginx.conf             # Frontend-Config (gemountet)
│   └── arpa_storage/          # Persistenter Storage
├── arpa-src/                  # Geklonter API-Quellcode
└── monitor_arpa*.sh           # Health-Check-Skripte
```

### Konfiguration

**API (.env / docker-compose.yml):**
- `ASPNETCORE_ENVIRONMENT=RaspberryPi`
- PostgreSQL: `host=postgres;database=orso-arpa`
- JWT Issuer/Audience: `https://arpa.loopus.it`
- SMTP: MailHog (Port 1025)
- Initial Admin: `admin` / `Admin_Test_2024`

**Frontend (nginx.conf):**
- Statische Files: `/usr/share/nginx/html/Orso.Arpa.Web.Extended`
- API-Proxy: `/api/` → `http://api:5000/api/`
- GraphQL-Proxy: `/graphql` → `http://api:5000/graphql`
- Profilbild-Workaround: `/api/persons/*/profilepicture` → 204 (API-Bug)

**CORS erlaubt:**
- `http://localhost:4200`
- `https://arpa.loopus.it`

### Zugriff

- **URL:** https://arpa.loopus.it (via nginx auf raspi1)
- **Swagger:** https://arpa.loopus.it/swagger
- **SSH:** `ssh wolf@r3.loopus.it -p 2222`
- **MailHog:** http://192.168.1.59:8025 (nur intern)

### Test-User

| User | Passwort | Rolle | E-Mail |
|------|----------|-------|--------|
| `admin` | `Admin_Test_2024` | Admin | arpa@loopus.it |
| `staff` | `Staff_Test1` | Staff | staff@test.local |
| `performer` | `Performer_Test1` | Performer | performer@test.local |

---

## Migration Azure → raspi3

### Status: ✅ DATENBANK & BILDER ERFOLGREICH MIGRIERT (2026-01-16)

### Zwei Instanzen auf raspi3

| Instanz | Zweck | Erreichbar | Frontend | API | Swagger |
|---------|-------|------------|----------|-----|---------|
| **Test** (`arpa`) | Entwicklung & Tests mit Dummy-Daten | **https://arpa.loopus.it** | Port 8080 | Port 5000 | https://arpa.loopus.it/swagger |
| **Prod** (`arpa-prod`) | Migrierte Azure-Produktionsdaten | **http://192.168.1.59:8082** (nur lokal) | Port 8082 | Port 5001 | http://192.168.1.59:5001/swagger |

**Beide Instanzen nutzen `orso-arpa-api:local`** (lokales Image mit curl für Health Checks)

### Aktuelle Erreichbarkeit

| URL | Zeigt auf | Status |
|-----|-----------|--------|
| **https://arpa.orso.co** | Azure (Produktion) | ✅ Aktiv - bleibt so bis raspi3 produktionsbereit |
| **https://arpa.loopus.it** | raspi3 Test-Instanz | ✅ Aktiv - für Entwicklung & Tests |
| **http://192.168.1.59:8082** | raspi3 Prod-Instanz | ⚠️ Nur lokal erreichbar - nginx Proxy kommt später |

> **Wichtig:** `arpa.orso.co` zeigt weiterhin auf Azure und soll erst umgestellt werden, wenn das Produktionssystem auf raspi3 vollständig verifiziert und bereit ist.

### Migrierte Daten (Prod-Instanz)

| Tabelle | Anzahl |
|---------|--------|
| Audit-Logs | 185.663 |
| Termin-Teilnahmen | 66.688 |
| Projektbeteiligungen | 23.397 |
| Kontaktdaten | 14.505 |
| Personen | 8.952 |
| Musikerprofile | 4.280 |
| Benutzer | 1.246 |
| Termine | 1.142 |
| Adressen | 554 |
| Bankkonten | 332 |
| URLs | 173 |
| Projekte | 121 |
| Sektionen | 117 |
| Veranstaltungsorte | 70 |

**Bilder:**
- 362 Profilbilder (~347 MB)
- 1.103 Cache-Bilder (~25 MB)

### Verzeichnisstruktur Prod-Instanz

```
/home/wolf/arpa-prod/
├── docker-compose.yml
├── .env                          # JWT_TOKEN_KEY, ADMIN_PASSWORD
├── appsettings.Production.json
├── nginx.conf
└── arpa_storage/
    ├── profile-pictures/         # 362 Bilder
    ├── image-sharp-cache/        # 1.103 Bilder
    └── imagecache/azurestorage/profile-pictures/  # API erwartet hier die Bilder
```

### Was bereits funktioniert
- [x] Docker-Container laufen stabil (Test + Prod)
- [x] API + Frontend + Datenbank verbunden
- [x] HTTPS via Let's Encrypt (nginx auf raspi1)
- [x] Health-Monitor-Skripte vorhanden
- [x] **Datenbank-Migration von Azure-Produktiv** ✅
- [x] **Bilder-Migration von Azure Blob Storage** ✅
- [x] Login mit Azure-Credentials funktioniert

### Offene Punkte

- [x] **E-Mail-Versand für Produktion** ✅ Konfiguriert (2026-01-16) - DomainFactory SMTP (sslout.de), noch nicht getestet
- [x] **Backup-Strategie für PostgreSQL** ✅ Erledigt (2026-01-16)
- [ ] DNS-Umstellung `arpa.orso.co` → raspi3
- [ ] Performance-Tests mit echten Daten
- [ ] Authentik-Integration (SSO)?
- [x] **CI/CD Pipeline für automatische Deployments** ✅ Erledigt (2026-02-03) - GitLab CI/CD auf git.loopus.it
- [ ] nginx Reverse Proxy für `arpa-prod.loopus.it` einrichten
- [ ] **Frontend Personen-Standardansicht anpassen** (siehe unten)

### Bekannte Eigenheiten

**Frontend Personen-Liste:**
- Die Standardansicht zeigt nur Personen der letzten ~30 Tage (`createdAfter` Filter in GraphQL)
- Dies ist kein Bug, sondern die aktuelle Standardansicht
- Die **Suche funktioniert** und findet alle Personen unabhängig vom Erstelldatum
- **TODO:** Über Anpassung nachdenken - z.B. Toggle "Neue Personen" vs "Alle Personen"

### Offene Fragen

1. ~~Wie werden Azure-Produktivdaten migriert? (pg_dump?)~~ ✅ Erledigt
2. ~~Welcher SMTP-Server für Produktion?~~ ✅ DomainFactory (sslout.de) - wie auf Azure
3. Soll Authentik für SSO integriert werden?
4. ~~Backup-Strategie: Lokal + NAS (Nike/Ulysses)?~~ ✅ Erledigt - siehe Backup-System
5. Wann soll die DNS-Umstellung erfolgen?
6. Finale Migration: Wann soll ein frischer Dump zum Cutover gemacht werden?

---

## Backup-System (raspi3 → Ulysses)

**Status:** ✅ Aktiv seit 2026-01-16

### Übersicht

| Backup | Zeitplan | Ziel | Retention |
|--------|----------|------|-----------|
| **ARPA Datenbank** | täglich 3:00 | `/volume1/Backups Raspi3/arpa-db/` | 30 Tage |
| **System (Home + Docker)** | täglich 4:00 | `/volume1/Backups Raspi3/system/` | 30 Tage |

### Skripte auf raspi3

| Skript | Beschreibung |
|--------|--------------|
| `/home/wolf/scripts/backup-arpa-db.sh` | PostgreSQL-Dump der Prod-Datenbank + rsync zu Ulysses |
| `/home/wolf/scripts/backup-raspi3-full.sh` | Home-Verzeichnis + Docker-Volumes als tar.gz |

### Cron-Jobs

```bash
# ARPA Datenbank-Backup täglich um 3:00
0 3 * * * /home/wolf/scripts/backup-arpa-db.sh >> /home/wolf/backups/backup-db.log 2>&1

# System-Backup täglich um 4:00
0 4 * * * /home/wolf/scripts/backup-raspi3-full.sh >> /home/wolf/backups/backup-system.log 2>&1
```

### Technische Details

**Ziel-NAS:** Ulysses (192.168.1.121)
- SSH-Port: 28
- User: wolf
- SSH-Key: `/home/wolf/.ssh/id_ed25519` (passwordless)

**Synology-spezifische Anpassungen:**
- `--rsync-path=/usr/bin/rsync` erforderlich (rsync-Pfad nicht automatisch gefunden)
- Pfade mit Leerzeichen: `"host:/path with spaces/"` Format

**Logs:**
- `/home/wolf/backups/backup-db.log`
- `/home/wolf/backups/backup-system.log`

### Manuelles Backup ausführen

```bash
# Datenbank-Backup
ssh wolf@r3.loopus.it -p 2222 '/home/wolf/scripts/backup-arpa-db.sh'

# System-Backup
ssh wolf@r3.loopus.it -p 2222 '/home/wolf/scripts/backup-raspi3-full.sh'

# Backup-Status auf Ulysses prüfen
ssh wolf@r3.loopus.it -p 2222 'ssh -p 28 wolf@192.168.1.121 "du -sh /volume1/Backups\ Raspi3/*"'
```

### Restore-Anleitung

**Datenbank wiederherstellen:**
```bash
# Auf raspi3:
gunzip -c /volume1/Backups\ Raspi3/arpa-db/arpa-prod-YYYY-MM-DD_HH-MM.sql.gz | \
  docker exec -i arpa-prod-postgres psql -U postgres -d orso-arpa
```

**Docker-Volumes wiederherstellen:**
```bash
# tar.gz von Ulysses kopieren und entpacken
sudo tar xzf docker-volumes-backup.tar.gz -C /var/lib/docker/volumes/
```

---

## Deployment-Anleitungen

### raspi3 (Test)

```bash
# SSH-Zugang (extern)
ssh wolf@r3.loopus.it -p 2222

# Docker-Container Status
docker ps | grep arpa

# Logs ansehen
docker logs -f arpa-api-1
docker logs -f arpa-frontend-1

# Container neustarten
cd /home/wolf/arpa
docker compose down && docker compose up -d
```

### API-Image neu bauen & deployen (GHCR ist privat)

```bash
# Auf dem Mac (im API-Repo):
cd /Users/wolf/Projects/ARPA/Orso.Arpa.Api

# 1. Image für ARM64 bauen
docker buildx build --platform linux/arm64 -t orso-arpa-api:local --load .

# 2. Als tarball exportieren
docker save orso-arpa-api:local -o /tmp/orso-arpa-api.tar
gzip -f /tmp/orso-arpa-api.tar

# 3. Auf raspi3 kopieren
scp -P 2222 /tmp/orso-arpa-api.tar.gz wolf@r3.loopus.it:/tmp/

# 4. Auf raspi3 laden und deployen
ssh wolf@r3.loopus.it -p 2222 "gunzip -c /tmp/orso-arpa-api.tar.gz | docker load"
ssh wolf@r3.loopus.it -p 2222 "cd ~/arpa && docker compose down api && docker compose up -d api"
```

### Frontend deployen

```bash
# Frontend separat (läuft nicht über docker-compose!)
docker stop arpa-frontend-1
docker pull ghcr.io/orso-co/arpa-frontend:latest
docker run -d --name arpa-frontend-1 \
  --network arpa_default \
  -p 8080:80 \
  -v /home/wolf/arpa/nginx.conf:/etc/nginx/conf.d/default.conf:ro \
  ghcr.io/orso-co/arpa-frontend:latest
```

### Lokale Entwicklung

**Backend (API):**
```bash
cd /Users/wolf/Projects/ARPA/Orso.Arpa.Api
# Mit Docker
docker-compose up -d
# API läuft auf http://localhost:5000
# MailHog auf http://localhost:8025
```

**Frontend:**
```bash
cd /Users/wolf/WebstormProjects/orso-arpa-web-extended
npm install
npm start
# oder: npx nx serve Orso.Arpa.Web.Extended
# Läuft auf http://localhost:4200
```

---

## Session-Log

### 2026-01-16 (Spät) - Swagger aktiviert & Health Checks finalisiert

**Durchgeführte Arbeiten:**

1. **Swagger UI aktiviert**
   - Problem: Swagger RoutePrefix war `string.Empty`, kollidierte mit SPA-Fallback
   - Fix: `Startup.cs` geändert - RoutePrefix auf `"swagger"` gesetzt
   - nginx auf raspi1: Location `/swagger` hinzugefügt → Port 5000 Backend
   - Commit: `Enable Swagger UI at /swagger route`

2. **Health Check Probleme behoben**
   - Beide Instanzen nutzten noch GHCR-Image ohne curl
   - docker-compose.yml auf `orso-arpa-api:local` umgestellt
   - LocalStorageConfiguration zu beiden appsettings hinzugefügt
   - Alle Container jetzt **healthy**

3. **SMTP für Prod-Instanz konfiguriert**
   - DomainFactory SMTP wie auf Azure: `sslout.de:465`
   - User: `noreply@arpa.orso.co`
   - Config in `~/arpa-prod/appsettings.Production.json`

**Ergebnis:**
- ✅ Swagger UI: https://arpa.loopus.it/swagger
- ✅ Beide Container (Test + Prod) zeigen `healthy`
- ✅ SMTP für Prod konfiguriert (DomainFactory)

**Geänderte Dateien:**
- `Orso.Arpa.Api/Startup.cs` - Swagger RoutePrefix
- `/etc/nginx/sites-enabled/loopus` auf raspi1 - Swagger Location
- `~/arpa/docker-compose.yml` auf raspi3 - lokales Image
- `~/arpa-prod/docker-compose.yml` auf raspi3 - lokales Image
- `~/arpa/appsettings.RaspberryPi.json` - LocalStorageConfiguration
- `~/arpa-prod/appsettings.Production.json` - LocalStorageConfiguration + SMTP

---

### 2026-01-16 (Abend) - Backup-System eingerichtet

**Durchgeführte Arbeiten:**

1. **Backup-Strategie definiert**
   - 30 Tage Retention
   - Ziel: Ulysses NAS (`/volume1/Backups Raspi3/`)
   - Ursprünglich borgbackup geplant, aber nicht auf Synology installiert → rsync gewählt

2. **SSH-Key für passwordless Backup eingerichtet**
   - `ssh-keygen -t ed25519` auf raspi3
   - `ssh-copy-id -p 28 wolf@192.168.1.121`
   - Synology-spezifische Berechtigungen gesetzt (`chmod 700 ~/.ssh`, `chmod 600 ~/.ssh/authorized_keys`)

3. **Backup-Skripte erstellt**
   - `/home/wolf/scripts/backup-arpa-db.sh` - PostgreSQL pg_dump + rsync
   - `/home/wolf/scripts/backup-raspi3-full.sh` - Home + Docker-Volumes (als tar.gz)

4. **Synology-spezifische Probleme gelöst**
   - rsync brauchte `--rsync-path=/usr/bin/rsync` (Pfad nicht automatisch gefunden)
   - Pfade mit Leerzeichen: Spezielle Quotierung erforderlich
   - sudo-rsync für Docker-Volumes: Statt direktem rsync → tar.gz + normales rsync

5. **Cron-Jobs aktiviert**
   - DB-Backup: täglich 3:00 Uhr
   - System-Backup: täglich 4:00 Uhr

**Ergebnis:**
- ✅ Datenbank-Backup: 236 MB auf Ulysses
- ✅ System-Backup: 2.6 GB auf Ulysses
- ✅ Automatische tägliche Backups via Cron

---

### 2026-01-16 (Nachmittag) - Test-Instanz: Profilbilder & Health Checks

**Durchgeführte Arbeiten:**

1. **50 Test-Performer mit Profilbildern erstellt**
   - Generierte Personen mit deutschen Namen (Anna Müller, Emma Schmidt, etc.)
   - Profilbilder von xsgames.co heruntergeladen (gender-spezifisch)
   - SQL-Skript erstellt für: persons, AspNetUsers, AspNetUserRoles, musician_profiles
   - `created_at` auf aktuelles Datum gesetzt (Frontend filtert nach letztem Monat)

2. **LocalStorageProfilePictureAccessor implementiert**
   - Neue Klasse für lokale Dateisystem-Speicherung statt Azure Blob Storage
   - Config-Key: `LocalStorageConfiguration:StorageType = "Local"`
   - Pfad: `/publish/storage/imagecache/azurestorage/`
   - Code committed: `feat: add local storage support for profile pictures`

3. **Docker-Image neu gebaut**
   - Problem: Health Check nutzte `curl`, das im minimalen aspnet-Image fehlte
   - Fix: `apt-get install curl` im Dockerfile hinzugefügt
   - Code committed: `fix: add curl to Docker image for health checks`
   - Image manuell gebaut und per tarball auf raspi3 deployt (GHCR privat)

4. **Konfiguration Test-Instanz (`/home/wolf/arpa/`)**
   ```json
   "LocalStorageConfiguration": {
     "StorageType": "Local",
     "ProfilePicturesPath": "/publish/storage/imagecache/azurestorage",
     "ImageCachePath": "/publish/storage/imagecache"
   }
   ```

**Ergebnis:**
- ✅ 52 Personen mit Profilbildern sichtbar auf https://arpa.loopus.it
- ✅ Container zeigt `healthy` statt `unhealthy`
- ✅ Health Check funktioniert: `curl http://localhost:5000/health`

**Git Branch:** `feature/raspi-workflow` (gepusht auf GitHub)

---

### 2026-01-16 (Vormittag) - Azure → raspi3 Migration erfolgreich

**Durchgeführte Arbeiten:**

1. **Zweite Prod-Instanz auf raspi3 erstellt**
   - Separater Docker-Compose Stack in `/home/wolf/arpa-prod/`
   - Eigene Ports: Frontend 8082, API 5001, Mail 8026
   - Eigene Volumes: `arpa_prod_pgdata`, `arpa_prod_mailhog`
   - Test-Instanz (`arpa`) bleibt für Entwicklung erhalten

2. **Datenbank von Azure PostgreSQL migriert**
   - Azure Key Vault Firewall für Berlin-IP (217.92.43.85) freigeschaltet
   - pg_dump über raspi3 Docker-Container (Version 16.9 kompatibel mit Azure 16.10)
   - ~40 MB Dump erstellt und importiert
   - `SeedInitialAdmin=false` gesetzt (Admin existiert bereits in migrierten Daten)

3. **Bilder von Azure Blob Storage migriert**
   - Storage Account: `orsoarpaimagesprod`
   - 362 Profilbilder + 1.103 Cache-Bilder
   - Kopiert nach `/home/wolf/arpa-prod/arpa_storage/imagecache/azurestorage/profile-pictures/`

4. **Problem analysiert: Nur 7 Personen sichtbar**
   - API gibt alle 8.857 Personen zurück (verifiziert via curl)
   - Frontend GraphQL-Query hat `createdAfter` Filter (letzte ~30 Tage)
   - Ist Standardansicht, kein Bug - Suche findet alle Personen
   - TODO: Über Anpassung der Standardansicht nachdenken

**Azure-Zugangsdaten (für spätere finale Migration):**
- PostgreSQL: `orso-arpa-prod-infra-pgs.postgres.database.azure.com`
- User: `orsoarpaadminprod`
- Storage: `orsoarpaimagesprod`
- (Passwörter im Azure Key Vault)

**Nächste Schritte:**
- nginx Reverse Proxy für externe Erreichbarkeit
- Finale Migration mit frischem Dump zum Cutover-Zeitpunkt
- DNS-Umstellung erst nach vollständiger Verifikation

---

### 2026-01-13 - ARPA auf raspi3 zum Laufen gebracht
- API-Repo geklont nach `/Users/wolf/Projects/ARPA/Orso.Arpa.Api`
- Frontend-Repo gefunden: `/Users/wolf/WebstormProjects/orso-arpa-web-extended` (privat)
- raspi3 analysiert und mehrere Probleme behoben:

**Fixes durchgeführt:**
1. **nginx.conf:** GraphQL-Route `/graphql` hinzugefügt (fehlte komplett)
2. **Admin-Passwort:** `Pa$$w0rd` → `Admin_Test_2024` (Docker Compose interpretierte `$` als Variable)
3. **FallbackController:** Dummy `/publish/wwwroot/index.html` erstellt (API suchte nach nicht existierender Datei)
4. **Profilbild-Bug:** nginx Workaround für `/api/persons/*/profilepicture` → 204 (API-Handler nicht registriert)
5. **Datenbank:** Komplett zurückgesetzt für frisches Seeding mit neuem Passwort

**Bekannte Einschränkungen:**
- Profilbilder werden nicht angezeigt (API-Bug, benötigt neues Image)
- E-Mail nur über MailHog (nicht produktionsreif)
- Datenbank ist leer (nur Admin-User)
- GHCR-Images sind privat (kein `docker pull` ohne Auth)

**Ergebnis:** Login funktioniert unter https://arpa.loopus.it

### 2026-01-12 - Projektstart mit Claude Code
- PROJECT.md angelegt
- Bestandsaufnahme: TechStack, Team, Features
- Migration Azure → raspi3 als Hauptziel definiert

---

## Bekannte Bugs / Workarounds

| Problem | Ursache | Status |
|---------|---------|--------|
| ~~Profilbilder laden nicht~~ | ~~Handler nicht registriert~~ | ✅ **Behoben** - LocalStorageProfilePictureAccessor implementiert |
| `$` in Passwörtern | Docker Compose interpretiert `$` als Variable | Passwörter ohne `$` verwenden oder `$$` escapen |
| API 500 bei unbekannten Routen | FallbackController sucht `/publish/wwwroot/index.html` | Dummy-Datei im Container erstellen |
| Container "unhealthy" | curl fehlte im Image | ✅ **Behoben** - curl im Dockerfile installiert |

---

## Aktuelle nginx.conf (raspi3 Test-Frontend)

```nginx
server {
    listen 80;
    server_name localhost;

    root /usr/share/nginx/html/Orso.Arpa.Web.Extended;
    index index.html;

    location / {
        try_files $uri $uri/ /index.html;
    }

    # API proxy
    location /api/ {
        proxy_pass http://api:5000/api/;
        proxy_set_header Host $host;
        proxy_set_header X-Real-IP $remote_addr;
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto $scheme;
    }

    # GraphQL endpoint
    location /graphql {
        proxy_pass http://api:5000/graphql;
        proxy_set_header Host $host;
        proxy_set_header X-Real-IP $remote_addr;
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto $scheme;
    }
}
```

> **Hinweis:** Der Profilbild-Workaround (`return 204`) wurde entfernt - Bilder funktionieren jetzt nativ über die API.

---

## nginx Reverse Proxy (raspi1)

**Datei:** `/etc/nginx/sites-enabled/loopus` auf raspi1 (192.168.1.57)

Die Test-Instanz ist über https://arpa.loopus.it erreichbar. Die Konfiguration routet:

| Pfad | Ziel | Beschreibung |
|------|------|--------------|
| `/api/` | `192.168.1.59:5000/api/` | REST API |
| `/graphql` | `192.168.1.59:5000/graphql` | GraphQL Endpoint |
| `/health` | `192.168.1.59:5000/health` | Health Check |
| `/swagger` | `192.168.1.59:5000/swagger` | Swagger UI ✨ NEU |
| `/` | `192.168.1.59:8080` | Frontend (Angular SPA) |

**SSL:** Let's Encrypt Zertifikat via certbot

---

## Session-Log

### 2026-01-16 (Nacht) - SQL Performance Fix & Pie-Chart Bug

**Durchgeführte Arbeiten:**

1. **SQL-Funktion `fn_mupro_for_appointments` optimiert**
   - **Problem:** Funktion rief für jeden der 4227 musician_profiles zwei Sub-Funktionen auf (~8500 Queries pro Appointment!)
   - **Symptom:** Projekt-Detail-Seite lud 10-15 Sekunden, CPU bei 200%+
   - **Fix:** Set-basierte Query mit CTEs statt Row-by-Row
   - **Ergebnis:** 2-3 Sekunden statt 10-15 Sekunden
   - **Status:** ✅ Live auf beiden Instanzen (Test + Prod)
   - **Branch:** `fix/optimize-mupro-function`
   - **Datei:** `Orso.Arpa.Persistence/SqlStatements/fn_mupro_for_appointments_optimized.sql`

2. **Pie-Chart Endlos-Reload Bug gefunden & gefixt**
   - **Problem:** `chart-widget.component.html` hatte `[values]="[data | async]"` - Array-Literal im Template
   - **Ursache:** Bei jeder Change Detection wurde ein neues Array erstellt → Chart re-renderte ständig
   - **Fix:** Eckige Klammern entfernt: `[values]="data | async"`
   - **Status:** ✅ Datei geändert, Commit noch offen (Bash-Session kaputt)
   - **Datei:** `/Users/wolf/Orso.Arpa.Web/src/app/features/dashboard/chart-widget/chart-widget.component.html`

3. **Frontend-Repo Situation geklärt**
   - Mehrere Kopien existieren:
     - `/Users/wolf/Orso.Arpa.Web` - Hauptarbeitsverzeichnis
     - `/Users/wolf/WebstormProjects/orso-arpa-web` - veraltet
     - `/Users/wolf/WebstormProjects/orso-arpa-web-extended` - Nx Monorepo (privat)
   - Versehentlich nach `/Users/wolf/Projects/ARPA/orso-arpa-web` geklont → wieder gelöscht

**Offene Punkte:**
- [ ] Frontend-Fix committen (Bash war kaputt): `cd /Users/wolf/Orso.Arpa.Web && git checkout -b fix/chart-reloading && git add . && git commit -m "fix: prevent pie-chart reloading"`
- [ ] Lokale Instanzen starten zum Testen

**Test-Umgebungen:**

| Instanz | URL | SQL-Fix |
|---------|-----|---------|
| Test (raspi3) | https://arpa.loopus.it | ✅ Live |
| Prod (raspi3) | http://192.168.1.59:8082 | ✅ Live |
| Lokal | http://localhost:4200 | Noch starten |

---

## GraphQL Filtering (HotChocolate) - WICHTIG!

Das Backend verwendet HotChocolate für GraphQL. Bei Filtern gibt es spezielle Syntax-Anforderungen:

### UUID-Felder: `in` statt `eq`

```graphql
# FALSCH - "eq does not exist" Fehler:
where: { instrumentId: { eq: $instrumentId } }

# RICHTIG - verwende 'in' Operator mit Array:
where: { instrumentId: { in: [$instrumentId] } }
```

### Collection Filtering mit `some`

Für Navigation durch Collections (z.B. musicianProfiles):

```graphql
where: {
  musicianProfiles: {
    some: {
      instrumentId: { in: [$instrumentId] }
    }
  }
}
```

### String Filtering

String-Felder unterstützen `contains` (case-insensitive):

```graphql
where: {
  or: [
    { givenName: { contains: $searchQuery } }
    { surname: { contains: $searchQuery } }
    { displayName: { contains: $searchQuery } }
  ]
}
```

### Wichtige GraphQL-Dateien (Frontend)

- `/src/app/domains/staff/data/graphql/person-list.query.ts` - Personen-Liste Queries

---

## Frontend Deployment (orso-arpa-web-extended)

### Branches & Deployment

| Branch | Ziel | Container | URL |
|--------|------|-----------|-----|
| `raspi-dev` | Test | arpa-frontend | https://arpa.loopus.it |
| `raspi-prod` | Produktion | arpa-prod-frontend | (noch nicht live) |

### Deployment-Workflow

```bash
# Nach raspi-dev deployen (Test):
git push origin raspi-dev

# Nach raspi-prod deployen (Produktion):
git checkout raspi-prod
git merge raspi-dev
git push origin raspi-prod

# Workflow beobachten:
gh run watch $(gh run list --branch raspi-dev --limit 1 --json databaseId --jq '.[0].databaseId')
```

### Avatar Component

`arpa-avatar` unterstützt:
- `[skipTooltip]="true"` - Deaktiviert Tooltip
- `[skipInitials]="true"` - Zeigt Platzhalter statt Initialen

---

## Notizen

- raspi3 hat auch Ollama (llama3.2:3b), aber zu langsam für produktiven Einsatz
- n8n läuft ebenfalls auf raspi3 - könnte für Automatisierungen genutzt werden
- Authentik auf raspi2 könnte für SSO genutzt werden
- Nach Container-Neustart: `docker exec arpa-api-1 mkdir -p /publish/wwwroot && docker exec arpa-api-1 sh -c 'echo ARPA > /publish/wwwroot/index.html'`
