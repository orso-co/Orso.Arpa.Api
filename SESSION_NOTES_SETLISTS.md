# Session Notes: Music Library Phase 2 - Setlists

**Datum:** 2026-01-24
**Status:** Komplett funktionsfähig inkl. PDF-Viewer

---

## Erledigte Arbeiten

### Backend (komplett)

**Domain Layer (`Orso.Arpa.Domain/MusicLibraryDomain/`)**
- `Setlist.cs`, `SetlistPiece.cs` - Models
- Commands: Create, Modify, Delete, AddPiece, RemovePiece, ReorderPieces

**Application Layer (`Orso.Arpa.Application/SetlistApplication/`)**
- DTOs: SetlistDto, SetlistPieceDto, SetlistCreateDto, SetlistModifyBodyDto, AddPieceToSetlistDto
- SetlistService mit allen CRUD-Operationen

**API Layer**
- `SetlistsController.cs` mit allen Endpoints

**Persistence**
- EntityConfigurations für Setlist und SetlistPiece
- Datenbank-Tabellen manuell erstellt

### Frontend (komplett)

**DTOs (`src/app/domains/staff/data/model/`)**
- `setlist.dto.ts`
- `setlist-piece.dto.ts`
- `setlist-create.dto.ts`

**Service + Resolvers (`src/app/domains/staff/data/services/`)**
- `setlist.service.ts`
- `setlist-list.resolver.ts`
- `setlist-detail.resolver.ts`

**Components (`src/app/domains/staff/feature-setlist/`)**
- `setlist-list.component.ts/html` - Liste aller Setlists
- `setlist.component.ts/html` - Wrapper für Edit/New + PDF-Viewer
- `setlist-form/` - Formular für Name, Description, IsTemplate
- `setlist-pieces/` - Liste mit Drag-Drop (OrderList) + Stück-Auswahl

**Konfiguration**
- `tsconfig.json` - Path Alias hinzugefügt
- `staff-routes.ts` - Routes hinzugefügt
- `arpa-routes.ts` - Route-Konstanten hinzugefügt
- `app.menu.component.ts` - Navigation hinzugefügt

**Übersetzungen**
- `de.json` - Deutsche Texte (inkl. PDF-Viewer)
- `en.json` - Englische Texte (inkl. PDF-Viewer)

---

## Neues Feature: PDF-Viewer (Session 2)

**Layout geändert:**
- Oben: Setlist-Formular (Name, Beschreibung, Vorlage)
- Unten links: Stücke-Liste mit Drag & Drop
- Unten rechts: PDF-Viewer

**Funktionen:**
- Klick auf Stück zeigt verfügbare PDFs
- PDF wird als Blob über HttpClient geladen (mit JWT-Auth)
- Download-Button für einzelne Dateien
- Lade-Spinner während PDF lädt

**Geänderte Dateien:**
- `setlist.component.ts` - PDF-Logik mit MusicPieceService.downloadFile()
- `setlist.component.html` - Neues Layout mit iframe für PDF
- `setlist-pieces.component.ts` - pieceSelectedEvent Output
- `setlist-pieces.component.html` - Klick-Handler + Datei-Anzahl-Anzeige

---

## Kurt Weill Import (Session 2)

**20 MusicPieces erstellt:**
| Titel | ID |
|-------|-----|
| Alabama-Song | 4036a132-03e4-4ee2-818e-81e0a1c3d755 |
| Ballade von der sexuellen Hoerigkeit | 7fcdb281-4947-46ff-a4c8-236063e3236e |
| Berlin im Licht | 90265f17-5eb5-4978-86c1-7b45ff2d9ce8 |
| Die Legende vom toten Soldaten | 09b13393-7291-4ff7-b72a-c4720d0ce2bf |
| Dreigroschen-Overture | dbc4f7c6-dfa4-43fc-b301-b3fb2e8e1299 |
| I'm A Stranger Here Myself | f33d8254-60c4-4510-bc0a-f4f097cf76ff |
| J'attends un navire | a4d63e4f-f56e-4b5d-bf6a-5dfb34d6f1d4 |
| Kanonensong | 0125071b-ee33-43b4-b867-a7e98201aa53 |
| Kiddush | 03747987-5259-4339-bfaa-8150c49f015c |
| Lonely House | 1c9ea002-598a-4dc9-8b10-2b18947ee449 |
| Mack The Knife | f64499da-2dd3-4ffd-867c-fde8a4757ead |
| Moritat | 4e8037cf-6702-490d-a1b8-db820b53c2af |
| Nanas Lied | 5764c118-57df-488a-8e6b-661b03e9a2e6 |
| Schindlers Liste | 5b349bfa-d30d-4021-b9af-fadb4c67ed37 |
| Seeraeuber-Jenny | fb6f9eb9-563f-457d-8656-93f9b567aef6 |
| September Song | 5887a5f2-9d1c-4354-9a05-7222d8263bd0 |
| Speak Low | ac74288d-9a14-46bb-93ec-458126916c24 |
| Surabaya Johnny | 699b3657-f44c-41e5-9963-aef671e5a468 |
| Westwind | 549b0f36-a2cc-4293-8be7-49acea05bbc0 |
| Youkali | d1b291bb-74f4-41c5-a418-715569d0afa2 |

**113 PDFs hochgeladen**

**Setlist "Kurt Weill - Chornoten" erstellt (ID: 879ea22c-2101-4e5b-b55c-a7ad49b79d3b):**
1. Westwind (SATB + Einzelstimmen)
2. Surabaya Johnny (Solo, Sopran, Alt, Tenor, Bass)
3. Die Legende vom toten Soldaten (SATB Bibelformat)
4. Kanonensong (Klavierauszug)
5. Alabama-Song (Klavierauszug + ORSOrefrain)
6. Moritat
7. Mack The Knife
8. Kiddush

**Hinweise zum Import:**
- 2 Titel mussten angepasst werden (ö → oe): "Ballade von der sexuellen Hoerigkeit", "Seeraeuber-Jenny"
- MP3, M4A, SIB, Pages Dateien werden von der API nicht akzeptiert (nur PDF, JPG, PNG, XML, MusicXML, MXL)
- Quelle: `/Users/wolf/ORSO Zelos Sync/Bibliothek/Kurt Weill Material/`

---

## Getestet

✅ Setlist-Liste wird angezeigt
✅ Neue Setlist erstellen funktioniert
✅ Setlist bearbeiten funktioniert
✅ Navigation im Menü funktioniert
✅ Stücke hinzufügen/entfernen funktioniert
✅ Drag & Drop Reihenfolge funktioniert
✅ PDF-Viewer zeigt PDFs korrekt an
✅ PDF-Download funktioniert

---

## Gelöste Probleme

1. **MusicPieces-Dropdown leer** - War ein Proxy-Problem, funktioniert jetzt
2. **PDF-Viewer 401 Unauthorized** - Gelöst durch Blob-Download über HttpClient mit JWT-Auth

---

## Neues Feature: Datei-Berechtigungen nach Sections (Session 3)

**Automatische Zuweisung von Sections (Instrumenten) zu Dateien basierend auf Dateinamen.**

### Implementierte Funktionen

**Backend:**
- `MusicPieceFileSection` Entity (neue Join-Tabelle)
- `MusicPieceFileSectionConfiguration` - Entity Configuration
- `AddSectionToMusicPieceFile` Command - Section manuell hinzufügen
- `RemoveSectionFromMusicPieceFile` Command - Section entfernen
- `AutoAssignSectionsToFiles` Command - Automatische Zuweisung
- `MusicPieceFileSectionDto` - DTO mit Section-Name
- `MusicPieceFileDto` erweitert um `Sections` Collection
- `AutoAssignSectionsResultDto` - Ergebnis der Auto-Zuweisung

**API Endpoints:**
- `POST /api/musicpieces/{id}/files/{fileId}/sections/{sectionId}` - Section hinzufügen
- `DELETE /api/musicpieces/{id}/files/{fileId}/sections/{sectionId}` - Section entfernen
- `POST /api/musicpieces/{id}/files/auto-assign-sections?dryRun=true|false` - Auto-Assign für ein Stück
- `POST /api/musicpieces/files/auto-assign-sections?dryRun=true|false` - Auto-Assign für ALLE Dateien (Admin)

### Mapping (Dateiname → Section)

| Pattern | Section |
|---------|---------|
| Altsaxophon, Sopransaxophon, Tenorsaxophon | Saxophone |
| Trompete | Trumpet |
| Posaune | Trombone |
| Horn in F | Horn |
| Tenorhorn, Baritonhorn | Euphonium |
| Tenortuba, Tuba | Tuba |
| Violine, Violin | Violin |
| Viola, Bratsche | Viola |
| Violoncello, Cello | Violoncello |
| Kontrabass | Double Bass |
| Piccolo, Flöte | Flute |
| Klarinette | Clarinet |
| Oboe | Oboe |
| Fagott | Bassoon |
| Trommel, Schlagzeug, Becken | Drum Set (Orchestra) |
| Piano, Klavier, Organ | Keyboards |
| SATB, Chor | Soprano + Alto + Tenor + Bass |
| Partitur, Full Score, Klavierauszug | Conductor |

**Unklare Dateien werden automatisch dem Conductor zugewiesen.**

### Ergebnis der Auto-Zuweisung

- **118 Dateien** verarbeitet
- **124 Section-Zuweisungen** erstellt
- Beispiele:
  - "Alabama wolfgang - 1. Tenorhorn in B.pdf" → Euphonium
  - "Die Legende vom toten Soldaten - Sopran Bibelformat.pdf" → Soprano
  - "Westwind_SATB Pno.pdf" → Soprano, Alto, Tenor, Bass
  - "Moritat.pdf" → Conductor

### Neue Datenbank-Tabelle

```sql
CREATE TABLE music_piece_file_sections (
    id uuid PRIMARY KEY,
    music_piece_file_id uuid NOT NULL REFERENCES music_piece_files(id) ON DELETE CASCADE,
    section_id uuid NOT NULL REFERENCES sections(id) ON DELETE CASCADE,
    created_by varchar(110),
    created_at timestamp with time zone,
    modified_by varchar(110),
    modified_at timestamp with time zone,
    deleted boolean NOT NULL DEFAULT false,
    UNIQUE (music_piece_file_id, section_id)
);
```

### Neue Dateien (Backend)

- `Orso.Arpa.Domain/MusicLibraryDomain/Model/MusicPieceFileSection.cs`
- `Orso.Arpa.Domain/MusicLibraryDomain/Commands/AddSectionToMusicPieceFile.cs`
- `Orso.Arpa.Domain/MusicLibraryDomain/Commands/RemoveSectionFromMusicPieceFile.cs`
- `Orso.Arpa.Domain/MusicLibraryDomain/Commands/AutoAssignSectionsToFiles.cs`
- `Orso.Arpa.Persistence/EntityConfigurations/MusicPieceFileSectionConfiguration.cs`
- `Orso.Arpa.Persistence/Migrations/20260124120000_AddMusicPieceFileSections.cs`
- `Orso.Arpa.Application/MusicPieceApplication/Model/MusicPieceFileSectionDto.cs`
- `Orso.Arpa.Application/MusicPieceApplication/Model/AutoAssignSectionsResultDto.cs`

### Geänderte Dateien (Backend)

- `Orso.Arpa.Domain/MusicLibraryDomain/Model/MusicPieceFile.cs` - Sections Collection
- `Orso.Arpa.Domain/_General/Interfaces/IArpaContext.cs` - DbSet hinzugefügt
- `Orso.Arpa.Persistence/DataAccess/ArpaContext.cs` - DbSet hinzugefügt
- `Orso.Arpa.Application/MusicPieceApplication/Model/MusicPieceFileDto.cs` - Sections Property
- `Orso.Arpa.Application/MusicPieceApplication/Interfaces/IMusicPieceService.cs` - Neue Methoden
- `Orso.Arpa.Application/MusicPieceApplication/Services/MusicPieceService.cs` - Implementierung
- `Orso.Arpa.Api/Controllers/MusicPiecesController.cs` - Neue Endpoints

---

## Nächste Schritte

1. **Frontend für File Sections** - UI zum Anzeigen/Bearbeiten von Section-Zuweisungen
2. **Phase 3: Projekt-Integration** - Setlists zu Projekten zuordnen
3. **Phase 4: Termin-Integration** - Werke für Proben auswählen
4. **Erweiterte Dateitypen** - MP3/Audio-Support für Übedateien

---

## Datenbank-Tabellen

```sql
-- Bereits erstellt in lokaler DB:
CREATE TABLE setlists (
    id uuid PRIMARY KEY,
    name varchar(200) NOT NULL,
    description varchar(2000),
    is_template boolean NOT NULL DEFAULT false,
    created_by varchar(110),
    created_at timestamp with time zone,
    modified_by varchar(110),
    modified_at timestamp with time zone,
    deleted boolean NOT NULL DEFAULT false
);

CREATE TABLE setlist_pieces (
    id uuid PRIMARY KEY,
    setlist_id uuid NOT NULL REFERENCES setlists(id) ON DELETE CASCADE,
    music_piece_id uuid NOT NULL REFERENCES music_pieces(id) ON DELETE CASCADE,
    sort_order integer NOT NULL DEFAULT 0,
    notes varchar(1000),
    created_by varchar(110),
    created_at timestamp with time zone,
    modified_by varchar(110),
    modified_at timestamp with time zone,
    deleted boolean NOT NULL DEFAULT false
);
```

---

## Erstellte/Geänderte Dateien

### Backend
- (bereits in vorheriger Session erstellt)

### Frontend
- `src/app/domains/staff/data/model/setlist.dto.ts`
- `src/app/domains/staff/data/model/setlist-piece.dto.ts`
- `src/app/domains/staff/data/model/setlist-create.dto.ts`
- `src/app/domains/staff/data/services/setlist.service.ts`
- `src/app/domains/staff/data/services/setlist-list.resolver.ts`
- `src/app/domains/staff/data/services/setlist-detail.resolver.ts`
- `src/app/domains/staff/feature-setlist/index.ts`
- `src/app/domains/staff/feature-setlist/setlist-list.component.ts`
- `src/app/domains/staff/feature-setlist/setlist-list.component.html`
- `src/app/domains/staff/feature-setlist/setlist.component.ts` *(mit PDF-Viewer)*
- `src/app/domains/staff/feature-setlist/setlist.component.html` *(neues Layout)*
- `src/app/domains/staff/feature-setlist/setlist-form/setlist-form.component.ts`
- `src/app/domains/staff/feature-setlist/setlist-form/setlist-form.component.html`
- `src/app/domains/staff/feature-setlist/setlist-pieces/setlist-pieces.component.ts` *(mit pieceSelectedEvent)*
- `src/app/domains/staff/feature-setlist/setlist-pieces/setlist-pieces.component.html` *(mit Klick-Handler)*
- `public/i18n/de.json` *(PDF-Viewer Texte)*
- `public/i18n/en.json` *(PDF-Viewer Texte)*
