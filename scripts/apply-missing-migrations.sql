-- ============================================================================
-- SQL Script: Apply 11 missing EF Core migrations (without Designer.cs)
-- Generated: 2026-02-24
-- Target: PostgreSQL (ARPA database)
-- Idempotent: Uses IF NOT EXISTS / DO $$ blocks where possible
-- ============================================================================

-- No BEGIN/COMMIT to allow partial execution

-- ============================================================================
-- 1. 20260121100000_AddUniqueIndexAppointmentParticipation
-- ============================================================================
CREATE UNIQUE INDEX IF NOT EXISTS ix_appointment_participations_person_id_appointment_id
    ON appointment_participations (person_id, appointment_id);

-- ============================================================================
-- 2. 20260121130000_AddCommentByStaffInnerToAppointmentParticipation
-- ============================================================================
DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM information_schema.columns
        WHERE table_name = 'appointment_participations' AND column_name = 'comment_by_staff_inner'
    ) THEN
        ALTER TABLE appointment_participations
            ADD COLUMN comment_by_staff_inner character varying(500);
    END IF;
END $$;

-- ============================================================================
-- 3. 20260124090000_AddMusicLibrary
-- ============================================================================

-- music_pieces table
CREATE TABLE IF NOT EXISTS music_pieces (
    id uuid NOT NULL,
    title character varying(500) NOT NULL,
    composer character varying(200),
    arranger character varying(200),
    subtitle character varying(500),
    duration integer,
    year_composed integer,
    opus character varying(100),
    instrumentation character varying(2000),
    epoch_id uuid,
    genre_id uuid,
    difficulty_level_id uuid,
    performance_notes character varying(5000),
    internal_notes character varying(5000),
    is_archived boolean NOT NULL DEFAULT false,
    created_by character varying(110),
    created_at timestamp without time zone NOT NULL,
    modified_by character varying(110),
    modified_at timestamp without time zone,
    deleted boolean NOT NULL,
    CONSTRAINT pk_music_pieces PRIMARY KEY (id),
    CONSTRAINT fk_music_pieces_select_value_mappings_epoch_id
        FOREIGN KEY (epoch_id) REFERENCES select_value_mappings (id),
    CONSTRAINT fk_music_pieces_select_value_mappings_genre_id
        FOREIGN KEY (genre_id) REFERENCES select_value_mappings (id),
    CONSTRAINT fk_music_pieces_select_value_mappings_difficulty_level_id
        FOREIGN KEY (difficulty_level_id) REFERENCES select_value_mappings (id)
);

CREATE INDEX IF NOT EXISTS ix_music_pieces_title ON music_pieces (title);
CREATE INDEX IF NOT EXISTS ix_music_pieces_composer ON music_pieces (composer);
CREATE INDEX IF NOT EXISTS ix_music_pieces_is_archived ON music_pieces (is_archived);
CREATE INDEX IF NOT EXISTS ix_music_pieces_epoch_id ON music_pieces (epoch_id);
CREATE INDEX IF NOT EXISTS ix_music_pieces_genre_id ON music_pieces (genre_id);
CREATE INDEX IF NOT EXISTS ix_music_pieces_difficulty_level_id ON music_pieces (difficulty_level_id);

-- music_piece_parts table
CREATE TABLE IF NOT EXISTS music_piece_parts (
    id uuid NOT NULL,
    music_piece_id uuid NOT NULL,
    section_id uuid,
    part_name character varying(200) NOT NULL,
    sort_order integer NOT NULL,
    created_by character varying(110),
    created_at timestamp without time zone NOT NULL,
    modified_by character varying(110),
    modified_at timestamp without time zone,
    deleted boolean NOT NULL,
    CONSTRAINT pk_music_piece_parts PRIMARY KEY (id),
    CONSTRAINT fk_music_piece_parts_music_pieces_music_piece_id
        FOREIGN KEY (music_piece_id) REFERENCES music_pieces (id) ON DELETE CASCADE,
    CONSTRAINT fk_music_piece_parts_sections_section_id
        FOREIGN KEY (section_id) REFERENCES sections (id)
);

CREATE INDEX IF NOT EXISTS ix_music_piece_parts_music_piece_id_sort_order
    ON music_piece_parts (music_piece_id, sort_order);
CREATE INDEX IF NOT EXISTS ix_music_piece_parts_section_id
    ON music_piece_parts (section_id);

-- music_piece_files table
CREATE TABLE IF NOT EXISTS music_piece_files (
    id uuid NOT NULL,
    music_piece_id uuid NOT NULL,
    music_piece_part_id uuid,
    file_name character varying(500) NOT NULL,
    storage_file_name character varying(500) NOT NULL,
    content_type character varying(100) NOT NULL,
    file_size bigint NOT NULL,
    description character varying(500),
    created_by character varying(110),
    created_at timestamp without time zone NOT NULL,
    modified_by character varying(110),
    modified_at timestamp without time zone,
    deleted boolean NOT NULL,
    CONSTRAINT pk_music_piece_files PRIMARY KEY (id),
    CONSTRAINT fk_music_piece_files_music_pieces_music_piece_id
        FOREIGN KEY (music_piece_id) REFERENCES music_pieces (id) ON DELETE CASCADE,
    CONSTRAINT fk_music_piece_files_music_piece_parts_music_piece_part_id
        FOREIGN KEY (music_piece_part_id) REFERENCES music_piece_parts (id) ON DELETE SET NULL
);

CREATE INDEX IF NOT EXISTS ix_music_piece_files_music_piece_id
    ON music_piece_files (music_piece_id);
CREATE INDEX IF NOT EXISTS ix_music_piece_files_music_piece_part_id
    ON music_piece_files (music_piece_part_id);

-- music_piece_file_roles table
CREATE TABLE IF NOT EXISTS music_piece_file_roles (
    id uuid NOT NULL,
    music_piece_file_id uuid NOT NULL,
    role_id uuid NOT NULL,
    created_by character varying(110),
    created_at timestamp without time zone NOT NULL,
    modified_by character varying(110),
    modified_at timestamp without time zone,
    deleted boolean NOT NULL,
    CONSTRAINT pk_music_piece_file_roles PRIMARY KEY (id),
    CONSTRAINT fk_music_piece_file_roles_music_piece_files_music_piece_file_id
        FOREIGN KEY (music_piece_file_id) REFERENCES music_piece_files (id) ON DELETE CASCADE,
    CONSTRAINT fk_music_piece_file_roles_roles_role_id
        FOREIGN KEY (role_id) REFERENCES "AspNetRoles" (id)
);

CREATE UNIQUE INDEX IF NOT EXISTS ix_music_piece_file_roles_music_piece_file_id_role_id
    ON music_piece_file_roles (music_piece_file_id, role_id);
CREATE INDEX IF NOT EXISTS ix_music_piece_file_roles_role_id
    ON music_piece_file_roles (role_id);

-- ============================================================================
-- 4. 20260124100000_AddMusicPieceSelectValues (seed data)
-- ============================================================================

-- SelectValueCategories
INSERT INTO select_value_categories (id, created_at, created_by, deleted, modified_at, modified_by, name, property, "table")
VALUES
    ('a1b2c3d4-0001-4000-8000-000000000001', '0001-01-01 00:00:00', NULL, false, NULL, NULL, 'Music Epoch', 'Epoch', 'MusicPiece'),
    ('a1b2c3d4-0002-4000-8000-000000000002', '0001-01-01 00:00:00', NULL, false, NULL, NULL, 'Music Genre', 'Genre', 'MusicPiece'),
    ('a1b2c3d4-0003-4000-8000-000000000003', '0001-01-01 00:00:00', NULL, false, NULL, NULL, 'Difficulty Level', 'DifficultyLevel', 'MusicPiece')
ON CONFLICT (id) DO NOTHING;

-- SelectValues
INSERT INTO select_values (id, created_at, created_by, deleted, description, modified_at, modified_by, name)
VALUES
    -- Epochs
    ('b0000001-0001-4000-8000-000000000001', '0001-01-01 00:00:00', NULL, false, 'ca. 1600-1750', NULL, NULL, 'Baroque'),
    ('b0000001-0002-4000-8000-000000000002', '0001-01-01 00:00:00', NULL, false, 'ca. 1750-1820', NULL, NULL, 'Classical'),
    ('b0000001-0003-4000-8000-000000000003', '0001-01-01 00:00:00', NULL, false, 'ca. 1820-1910', NULL, NULL, 'Romantic'),
    ('b0000001-0004-4000-8000-000000000004', '0001-01-01 00:00:00', NULL, false, 'ca. 1910-1975', NULL, NULL, 'Modern'),
    ('b0000001-0005-4000-8000-000000000005', '0001-01-01 00:00:00', NULL, false, 'from 1975', NULL, NULL, 'Contemporary'),
    -- Genres
    ('b0000002-0001-4000-8000-000000000001', '0001-01-01 00:00:00', NULL, false, '', NULL, NULL, 'Symphonic Music'),
    ('b0000002-0002-4000-8000-000000000002', '0001-01-01 00:00:00', NULL, false, '', NULL, NULL, 'Concerto'),
    ('b0000002-0003-4000-8000-000000000003', '0001-01-01 00:00:00', NULL, false, '', NULL, NULL, 'Chamber Music'),
    ('b0000002-0004-4000-8000-000000000004', '0001-01-01 00:00:00', NULL, false, '', NULL, NULL, 'Opera / Musical Theatre'),
    ('b0000002-0005-4000-8000-000000000005', '0001-01-01 00:00:00', NULL, false, '', NULL, NULL, 'Sacred Music'),
    ('b0000002-0006-4000-8000-000000000006', '0001-01-01 00:00:00', NULL, false, '', NULL, NULL, 'Choral Music'),
    -- Difficulty Levels
    ('b0000003-0001-4000-8000-000000000001', '0001-01-01 00:00:00', NULL, false, '', NULL, NULL, 'Easy'),
    ('b0000003-0002-4000-8000-000000000002', '0001-01-01 00:00:00', NULL, false, '', NULL, NULL, 'Medium'),
    ('b0000003-0003-4000-8000-000000000003', '0001-01-01 00:00:00', NULL, false, '', NULL, NULL, 'Advanced'),
    ('b0000003-0004-4000-8000-000000000004', '0001-01-01 00:00:00', NULL, false, '', NULL, NULL, 'Expert')
ON CONFLICT (id) DO NOTHING;

-- SelectValueMappings
INSERT INTO select_value_mappings (id, created_at, created_by, deleted, modified_at, modified_by, select_value_category_id, select_value_id, sort_order)
VALUES
    -- Epoch mappings
    ('c0000001-0001-4000-8000-000000000001', '0001-01-01 00:00:00', NULL, false, NULL, NULL, 'a1b2c3d4-0001-4000-8000-000000000001', 'b0000001-0001-4000-8000-000000000001', 10),
    ('c0000001-0002-4000-8000-000000000002', '0001-01-01 00:00:00', NULL, false, NULL, NULL, 'a1b2c3d4-0001-4000-8000-000000000001', 'b0000001-0002-4000-8000-000000000002', 20),
    ('c0000001-0003-4000-8000-000000000003', '0001-01-01 00:00:00', NULL, false, NULL, NULL, 'a1b2c3d4-0001-4000-8000-000000000001', 'b0000001-0003-4000-8000-000000000003', 30),
    ('c0000001-0004-4000-8000-000000000004', '0001-01-01 00:00:00', NULL, false, NULL, NULL, 'a1b2c3d4-0001-4000-8000-000000000001', 'b0000001-0004-4000-8000-000000000004', 40),
    ('c0000001-0005-4000-8000-000000000005', '0001-01-01 00:00:00', NULL, false, NULL, NULL, 'a1b2c3d4-0001-4000-8000-000000000001', 'b0000001-0005-4000-8000-000000000005', 50),
    -- Genre mappings
    ('c0000002-0001-4000-8000-000000000001', '0001-01-01 00:00:00', NULL, false, NULL, NULL, 'a1b2c3d4-0002-4000-8000-000000000002', 'b0000002-0001-4000-8000-000000000001', 10),
    ('c0000002-0002-4000-8000-000000000002', '0001-01-01 00:00:00', NULL, false, NULL, NULL, 'a1b2c3d4-0002-4000-8000-000000000002', 'b0000002-0002-4000-8000-000000000002', 20),
    ('c0000002-0003-4000-8000-000000000003', '0001-01-01 00:00:00', NULL, false, NULL, NULL, 'a1b2c3d4-0002-4000-8000-000000000002', 'b0000002-0003-4000-8000-000000000003', 30),
    ('c0000002-0004-4000-8000-000000000004', '0001-01-01 00:00:00', NULL, false, NULL, NULL, 'a1b2c3d4-0002-4000-8000-000000000002', 'b0000002-0004-4000-8000-000000000004', 40),
    ('c0000002-0005-4000-8000-000000000005', '0001-01-01 00:00:00', NULL, false, NULL, NULL, 'a1b2c3d4-0002-4000-8000-000000000002', 'b0000002-0005-4000-8000-000000000005', 50),
    ('c0000002-0006-4000-8000-000000000006', '0001-01-01 00:00:00', NULL, false, NULL, NULL, 'a1b2c3d4-0002-4000-8000-000000000002', 'b0000002-0006-4000-8000-000000000006', 60),
    -- Difficulty Level mappings
    ('c0000003-0001-4000-8000-000000000001', '0001-01-01 00:00:00', NULL, false, NULL, NULL, 'a1b2c3d4-0003-4000-8000-000000000003', 'b0000003-0001-4000-8000-000000000001', 10),
    ('c0000003-0002-4000-8000-000000000002', '0001-01-01 00:00:00', NULL, false, NULL, NULL, 'a1b2c3d4-0003-4000-8000-000000000003', 'b0000003-0002-4000-8000-000000000002', 20),
    ('c0000003-0003-4000-8000-000000000003', '0001-01-01 00:00:00', NULL, false, NULL, NULL, 'a1b2c3d4-0003-4000-8000-000000000003', 'b0000003-0003-4000-8000-000000000003', 30),
    ('c0000003-0004-4000-8000-000000000004', '0001-01-01 00:00:00', NULL, false, NULL, NULL, 'a1b2c3d4-0003-4000-8000-000000000003', 'b0000003-0004-4000-8000-000000000004', 40)
ON CONFLICT (id) DO NOTHING;

-- ============================================================================
-- 5. 20260124120000_AddMusicPieceFileSections
-- ============================================================================
CREATE TABLE IF NOT EXISTS music_piece_file_sections (
    id uuid NOT NULL,
    music_piece_file_id uuid NOT NULL,
    section_id uuid NOT NULL,
    created_by character varying(110),
    created_at timestamp with time zone,
    modified_by character varying(110),
    modified_at timestamp with time zone,
    deleted boolean NOT NULL DEFAULT false,
    CONSTRAINT pk_music_piece_file_sections PRIMARY KEY (id),
    CONSTRAINT fk_music_piece_file_sections_music_piece_files_music_piece_file_id
        FOREIGN KEY (music_piece_file_id) REFERENCES music_piece_files (id) ON DELETE CASCADE,
    CONSTRAINT fk_music_piece_file_sections_sections_section_id
        FOREIGN KEY (section_id) REFERENCES sections (id) ON DELETE CASCADE
);

CREATE UNIQUE INDEX IF NOT EXISTS ix_music_piece_file_sections_music_piece_file_id_section_id
    ON music_piece_file_sections (music_piece_file_id, section_id);
CREATE INDEX IF NOT EXISTS ix_music_piece_file_sections_section_id
    ON music_piece_file_sections (section_id);

-- ============================================================================
-- 6. 20260124130000_AddSetlists
-- ============================================================================

-- setlists table
CREATE TABLE IF NOT EXISTS setlists (
    id uuid NOT NULL,
    name character varying(200) NOT NULL,
    description character varying(2000),
    is_template boolean NOT NULL DEFAULT false,
    created_by character varying(110),
    created_at timestamp without time zone NOT NULL,
    modified_by character varying(110),
    modified_at timestamp without time zone,
    deleted boolean NOT NULL,
    CONSTRAINT pk_setlists PRIMARY KEY (id)
);

CREATE INDEX IF NOT EXISTS ix_setlists_name ON setlists (name);
CREATE INDEX IF NOT EXISTS ix_setlists_is_template ON setlists (is_template);

-- setlist_pieces table
CREATE TABLE IF NOT EXISTS setlist_pieces (
    id uuid NOT NULL,
    setlist_id uuid NOT NULL,
    music_piece_id uuid NOT NULL,
    sort_order integer NOT NULL,
    notes character varying(1000),
    created_by character varying(110),
    created_at timestamp without time zone NOT NULL,
    modified_by character varying(110),
    modified_at timestamp without time zone,
    deleted boolean NOT NULL,
    CONSTRAINT pk_setlist_pieces PRIMARY KEY (id),
    CONSTRAINT fk_setlist_pieces_setlists_setlist_id
        FOREIGN KEY (setlist_id) REFERENCES setlists (id) ON DELETE CASCADE,
    CONSTRAINT fk_setlist_pieces_music_pieces_music_piece_id
        FOREIGN KEY (music_piece_id) REFERENCES music_pieces (id) ON DELETE CASCADE
);

CREATE INDEX IF NOT EXISTS ix_setlist_pieces_setlist_id_sort_order
    ON setlist_pieces (setlist_id, sort_order);
CREATE INDEX IF NOT EXISTS ix_setlist_pieces_music_piece_id
    ON setlist_pieces (music_piece_id);

-- ============================================================================
-- 7. 20260125100000_AddSetlistToProject
-- ============================================================================

-- Add setlist_id column to projects
DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM information_schema.columns
        WHERE table_name = 'projects' AND column_name = 'setlist_id'
    ) THEN
        ALTER TABLE projects ADD COLUMN setlist_id uuid;
    END IF;
END $$;

CREATE INDEX IF NOT EXISTS ix_projects_setlist_id ON projects (setlist_id);

DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM information_schema.table_constraints
        WHERE constraint_name = 'fk_projects_setlists_setlist_id'
    ) THEN
        ALTER TABLE projects
            ADD CONSTRAINT fk_projects_setlists_setlist_id
            FOREIGN KEY (setlist_id) REFERENCES setlists (id) ON DELETE SET NULL;
    END IF;
END $$;

-- appointment_setlist_pieces table
CREATE TABLE IF NOT EXISTS appointment_setlist_pieces (
    id uuid NOT NULL,
    appointment_id uuid NOT NULL,
    setlist_piece_id uuid NOT NULL,
    created_by character varying(110),
    created_at timestamp without time zone NOT NULL,
    modified_by character varying(110),
    modified_at timestamp without time zone,
    deleted boolean NOT NULL,
    CONSTRAINT pk_appointment_setlist_pieces PRIMARY KEY (id),
    CONSTRAINT fk_appointment_setlist_pieces_appointments_appointment_id
        FOREIGN KEY (appointment_id) REFERENCES appointments (id) ON DELETE CASCADE,
    CONSTRAINT fk_appointment_setlist_pieces_setlist_pieces_setlist_piece_id
        FOREIGN KEY (setlist_piece_id) REFERENCES setlist_pieces (id) ON DELETE CASCADE
);

CREATE UNIQUE INDEX IF NOT EXISTS ix_appointment_setlist_pieces_appointment_id_setlist_piece_id
    ON appointment_setlist_pieces (appointment_id, setlist_piece_id);
CREATE INDEX IF NOT EXISTS ix_appointment_setlist_pieces_setlist_piece_id
    ON appointment_setlist_pieces (setlist_piece_id);

-- ============================================================================
-- 8. 20260127100000_AddStageSetup
-- ============================================================================

-- stage_setups table
CREATE TABLE IF NOT EXISTS stage_setups (
    id uuid NOT NULL,
    name character varying(200) NOT NULL,
    file_name character varying(500) NOT NULL,
    storage_path character varying(1000) NOT NULL,
    content_type character varying(100) NOT NULL,
    file_size bigint NOT NULL,
    canvas_width integer NOT NULL,
    canvas_height integer NOT NULL,
    is_active boolean NOT NULL DEFAULT false,
    is_visible_to_performers boolean NOT NULL DEFAULT false,
    project_id uuid NOT NULL,
    created_by character varying(110),
    created_at timestamp without time zone NOT NULL,
    modified_by character varying(110),
    modified_at timestamp without time zone,
    deleted boolean NOT NULL,
    CONSTRAINT pk_stage_setups PRIMARY KEY (id),
    CONSTRAINT fk_stage_setups_projects_project_id
        FOREIGN KEY (project_id) REFERENCES projects (id) ON DELETE CASCADE
);

CREATE INDEX IF NOT EXISTS ix_stage_setups_project_id ON stage_setups (project_id);
CREATE INDEX IF NOT EXISTS ix_stage_setups_project_id_is_active ON stage_setups (project_id, is_active);

-- stage_setup_positions table
CREATE TABLE IF NOT EXISTS stage_setup_positions (
    id uuid NOT NULL,
    position_x double precision NOT NULL,
    position_y double precision NOT NULL,
    "row" integer,
    stand integer,
    stage_setup_id uuid NOT NULL,
    musician_profile_id uuid NOT NULL,
    created_by character varying(110),
    created_at timestamp without time zone NOT NULL,
    modified_by character varying(110),
    modified_at timestamp without time zone,
    deleted boolean NOT NULL,
    CONSTRAINT pk_stage_setup_positions PRIMARY KEY (id),
    CONSTRAINT fk_stage_setup_positions_stage_setups_stage_setup_id
        FOREIGN KEY (stage_setup_id) REFERENCES stage_setups (id) ON DELETE CASCADE,
    CONSTRAINT fk_stage_setup_positions_musician_profiles_musician_profile_id
        FOREIGN KEY (musician_profile_id) REFERENCES musician_profiles (id) ON DELETE CASCADE
);

CREATE INDEX IF NOT EXISTS ix_stage_setup_positions_stage_setup_id
    ON stage_setup_positions (stage_setup_id);
CREATE INDEX IF NOT EXISTS ix_stage_setup_positions_musician_profile_id
    ON stage_setup_positions (musician_profile_id);
CREATE UNIQUE INDEX IF NOT EXISTS ix_stage_setup_positions_stage_setup_id_musician_profile_id
    ON stage_setup_positions (stage_setup_id, musician_profile_id);

-- ============================================================================
-- 9. 20260129020000_AddStageSetupEquipment
-- ============================================================================
CREATE TABLE IF NOT EXISTS stage_setup_equipment (
    id uuid NOT NULL,
    equipment_id character varying(100) NOT NULL,
    position_x double precision NOT NULL,
    position_y double precision NOT NULL,
    rotation double precision NOT NULL,
    stage_setup_id uuid NOT NULL,
    created_by character varying(110),
    created_at timestamp without time zone NOT NULL,
    modified_by character varying(110),
    modified_at timestamp without time zone,
    deleted boolean NOT NULL,
    CONSTRAINT pk_stage_setup_equipment PRIMARY KEY (id),
    CONSTRAINT fk_stage_setup_equipment_stage_setups_stage_setup_id
        FOREIGN KEY (stage_setup_id) REFERENCES stage_setups (id) ON DELETE CASCADE
);

CREATE INDEX IF NOT EXISTS ix_stage_setup_equipment_stage_setup_id
    ON stage_setup_equipment (stage_setup_id);

-- ============================================================================
-- 10. 20260203140000_AddMusicPieceUrlAndSelectValues
-- ============================================================================

-- music_piece_urls table
CREATE TABLE IF NOT EXISTS music_piece_urls (
    id uuid NOT NULL,
    music_piece_id uuid NOT NULL,
    href character varying(1000) NOT NULL,
    anchor_text character varying(200),
    url_type_id uuid,
    created_by character varying(110),
    created_at timestamp without time zone NOT NULL,
    modified_by character varying(110),
    modified_at timestamp without time zone,
    deleted boolean NOT NULL,
    CONSTRAINT pk_music_piece_urls PRIMARY KEY (id),
    CONSTRAINT fk_music_piece_urls_music_pieces_music_piece_id
        FOREIGN KEY (music_piece_id) REFERENCES music_pieces (id) ON DELETE CASCADE,
    CONSTRAINT fk_music_piece_urls_select_value_mappings_url_type_id
        FOREIGN KEY (url_type_id) REFERENCES select_value_mappings (id)
);

CREATE INDEX IF NOT EXISTS ix_music_piece_urls_music_piece_id
    ON music_piece_urls (music_piece_id);
CREATE INDEX IF NOT EXISTS ix_music_piece_urls_url_type_id
    ON music_piece_urls (url_type_id);

-- Seed: URL Type category
INSERT INTO select_value_categories (id, created_at, created_by, deleted, modified_at, modified_by, name, property, "table")
VALUES ('a1b2c3d4-0004-4000-8000-000000000004', '0001-01-01 00:00:00', NULL, false, NULL, NULL, 'URL Type', 'UrlType', 'MusicPieceUrl')
ON CONFLICT (id) DO NOTHING;

-- Seed: New SelectValues (Virtuoso difficulty + URL types)
INSERT INTO select_values (id, created_at, created_by, deleted, description, modified_at, modified_by, name)
VALUES
    ('b0000003-0005-4000-8000-000000000005', '0001-01-01 00:00:00', NULL, false, 'Highest difficulty level', NULL, NULL, 'Virtuoso'),
    ('b0000004-0001-4000-8000-000000000001', '0001-01-01 00:00:00', NULL, false, '', NULL, NULL, 'YouTube Video'),
    ('b0000004-0002-4000-8000-000000000002', '0001-01-01 00:00:00', NULL, false, '', NULL, NULL, 'Publisher'),
    ('b0000004-0003-4000-8000-000000000003', '0001-01-01 00:00:00', NULL, false, '', NULL, NULL, 'IMDB'),
    ('b0000004-0004-4000-8000-000000000004', '0001-01-01 00:00:00', NULL, false, '', NULL, NULL, 'Wikipedia'),
    ('b0000004-0005-4000-8000-000000000005', '0001-01-01 00:00:00', NULL, false, '', NULL, NULL, 'Other')
ON CONFLICT (id) DO NOTHING;

-- Seed: New SelectValueMappings
INSERT INTO select_value_mappings (id, created_at, created_by, deleted, modified_at, modified_by, select_value_category_id, select_value_id, sort_order)
VALUES
    -- Film Music genre mapping (uses existing SelectValue a3be7b91-7548-492e-99dc-2788497f2930)
    ('c0000002-0007-4000-8000-000000000007', '0001-01-01 00:00:00', NULL, false, NULL, NULL, 'a1b2c3d4-0002-4000-8000-000000000002', 'a3be7b91-7548-492e-99dc-2788497f2930', 70),
    -- Virtuoso difficulty level mapping
    ('c0000003-0005-4000-8000-000000000005', '0001-01-01 00:00:00', NULL, false, NULL, NULL, 'a1b2c3d4-0003-4000-8000-000000000003', 'b0000003-0005-4000-8000-000000000005', 50),
    -- URL Type mappings
    ('c0000004-0001-4000-8000-000000000001', '0001-01-01 00:00:00', NULL, false, NULL, NULL, 'a1b2c3d4-0004-4000-8000-000000000004', 'b0000004-0001-4000-8000-000000000001', 10),
    ('c0000004-0002-4000-8000-000000000002', '0001-01-01 00:00:00', NULL, false, NULL, NULL, 'a1b2c3d4-0004-4000-8000-000000000004', 'b0000004-0002-4000-8000-000000000002', 20),
    ('c0000004-0003-4000-8000-000000000003', '0001-01-01 00:00:00', NULL, false, NULL, NULL, 'a1b2c3d4-0004-4000-8000-000000000004', 'b0000004-0003-4000-8000-000000000003', 30),
    ('c0000004-0004-4000-8000-000000000004', '0001-01-01 00:00:00', NULL, false, NULL, NULL, 'a1b2c3d4-0004-4000-8000-000000000004', 'b0000004-0004-4000-8000-000000000004', 40),
    ('c0000004-0005-4000-8000-000000000005', '0001-01-01 00:00:00', NULL, false, NULL, NULL, 'a1b2c3d4-0004-4000-8000-000000000004', 'b0000004-0005-4000-8000-000000000005', 50)
ON CONFLICT (id) DO NOTHING;

-- ============================================================================
-- 11. 20260203220000_AddMusicPieceTodo
-- WARNING: The C# migration file uses PascalCase names (MusicPieceTodos, etc.)
-- which is a bug — it was written without the Npgsql snake_case naming convention.
-- The actual DB uses snake_case for all non-Identity tables.
-- We translate to snake_case here so FKs to music_pieces(id) work correctly.
-- AspNetUsers stays PascalCase (Identity framework convention, PK column = "id").
-- ============================================================================

-- music_piece_todos table (translated from PascalCase migration to snake_case)
CREATE TABLE IF NOT EXISTS music_piece_todos (
    id uuid NOT NULL,
    music_piece_id uuid NOT NULL,
    title character varying(500) NOT NULL,
    due_date timestamp without time zone,
    is_completed boolean NOT NULL,
    completed_at timestamp without time zone,
    created_by text,
    created_at timestamp without time zone NOT NULL,
    modified_by text,
    modified_at timestamp without time zone,
    deleted boolean NOT NULL,
    CONSTRAINT pk_music_piece_todos PRIMARY KEY (id),
    CONSTRAINT fk_music_piece_todos_music_pieces_music_piece_id
        FOREIGN KEY (music_piece_id) REFERENCES music_pieces (id) ON DELETE CASCADE
);

-- music_piece_todo_assignee table (translated to snake_case)
CREATE TABLE IF NOT EXISTS music_piece_todo_assignee (
    music_piece_todo_id uuid NOT NULL,
    user_id uuid NOT NULL,
    CONSTRAINT pk_music_piece_todo_assignee PRIMARY KEY (music_piece_todo_id, user_id),
    CONSTRAINT fk_music_piece_todo_assignee_users_user_id
        FOREIGN KEY (user_id) REFERENCES "AspNetUsers" (id) ON DELETE CASCADE,
    CONSTRAINT fk_music_piece_todo_assignee_music_piece_todos_music_piece_todo_id
        FOREIGN KEY (music_piece_todo_id) REFERENCES music_piece_todos (id) ON DELETE CASCADE
);

CREATE INDEX IF NOT EXISTS ix_music_piece_todos_due_date
    ON music_piece_todos (due_date);
CREATE INDEX IF NOT EXISTS ix_music_piece_todos_is_completed
    ON music_piece_todos (is_completed);
CREATE INDEX IF NOT EXISTS ix_music_piece_todos_music_piece_id
    ON music_piece_todos (music_piece_id);
CREATE INDEX IF NOT EXISTS ix_music_piece_todo_assignee_user_id
    ON music_piece_todo_assignee (user_id);

-- ============================================================================
-- Register all 11 migrations in __EFMigrationsHistory
-- ============================================================================
INSERT INTO "__EFMigrationsHistory" (migration_id, product_version)
VALUES
    ('20260121100000_AddUniqueIndexAppointmentParticipation', '10.0.2'),
    ('20260121130000_AddCommentByStaffInnerToAppointmentParticipation', '10.0.2'),
    ('20260124090000_AddMusicLibrary', '10.0.2'),
    ('20260124100000_AddMusicPieceSelectValues', '10.0.2'),
    ('20260124120000_AddMusicPieceFileSections', '10.0.2'),
    ('20260124130000_AddSetlists', '10.0.2'),
    ('20260125100000_AddSetlistToProject', '10.0.2'),
    ('20260127100000_AddStageSetup', '10.0.2'),
    ('20260129020000_AddStageSetupEquipment', '10.0.2'),
    ('20260203140000_AddMusicPieceUrlAndSelectValues', '10.0.2'),
    ('20260203220000_AddMusicPieceTodo', '10.0.2')
ON CONFLICT (migration_id) DO NOTHING;

-- End of script
