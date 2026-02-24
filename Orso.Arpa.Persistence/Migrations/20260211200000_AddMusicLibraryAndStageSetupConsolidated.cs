using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orso.Arpa.Persistence.Migrations
{
    /// <summary>
    /// Consolidation of 11 orphaned migrations (20260121-20260203) that lacked Designer.cs files
    /// and were therefore invisible to EF Core. Creates all missing tables/columns/indexes
    /// using IF NOT EXISTS / DO $$ blocks so it's safe on environments where they already exist.
    /// </summary>
    public partial class AddMusicLibraryAndStageSetupConsolidated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // ================================================================
            // From: 20260121100000_AddUniqueIndexAppointmentParticipation
            // ================================================================
            migrationBuilder.Sql(@"
                CREATE UNIQUE INDEX IF NOT EXISTS ix_appointment_participations_person_id_appointment_id
                ON appointment_participations (person_id, appointment_id);
            ");

            // ================================================================
            // From: 20260121130000_AddCommentByStaffInnerToAppointmentParticipation
            // ================================================================
            migrationBuilder.Sql(@"
                DO $$ BEGIN
                    IF NOT EXISTS (SELECT 1 FROM information_schema.columns
                        WHERE table_name='appointment_participations' AND column_name='comment_by_staff_inner') THEN
                        ALTER TABLE appointment_participations ADD COLUMN comment_by_staff_inner character varying(500);
                    END IF;
                END $$;
            ");

            // ================================================================
            // From: 20260124090000_AddMusicLibrary
            // ================================================================
            migrationBuilder.Sql(@"
                CREATE TABLE IF NOT EXISTS music_pieces (
                    id uuid NOT NULL PRIMARY KEY,
                    title character varying(500) NOT NULL,
                    composer character varying(200),
                    arranger character varying(200),
                    subtitle character varying(500),
                    duration integer,
                    year_composed integer,
                    opus character varying(100),
                    instrumentation character varying(2000),
                    epoch_id uuid REFERENCES select_value_mappings(id),
                    genre_id uuid REFERENCES select_value_mappings(id),
                    difficulty_level_id uuid REFERENCES select_value_mappings(id),
                    performance_notes character varying(5000),
                    internal_notes character varying(5000),
                    is_archived boolean NOT NULL DEFAULT false,
                    created_by character varying(110),
                    created_at timestamp without time zone NOT NULL,
                    modified_by character varying(110),
                    modified_at timestamp without time zone,
                    deleted boolean NOT NULL
                );
                CREATE INDEX IF NOT EXISTS ix_music_pieces_title ON music_pieces (title);
                CREATE INDEX IF NOT EXISTS ix_music_pieces_composer ON music_pieces (composer);
                CREATE INDEX IF NOT EXISTS ix_music_pieces_is_archived ON music_pieces (is_archived);
                CREATE INDEX IF NOT EXISTS ix_music_pieces_epoch_id ON music_pieces (epoch_id);
                CREATE INDEX IF NOT EXISTS ix_music_pieces_genre_id ON music_pieces (genre_id);
                CREATE INDEX IF NOT EXISTS ix_music_pieces_difficulty_level_id ON music_pieces (difficulty_level_id);

                CREATE TABLE IF NOT EXISTS music_piece_parts (
                    id uuid NOT NULL PRIMARY KEY,
                    music_piece_id uuid NOT NULL REFERENCES music_pieces(id) ON DELETE CASCADE,
                    section_id uuid REFERENCES sections(id),
                    part_name character varying(200) NOT NULL,
                    sort_order integer NOT NULL,
                    created_by character varying(110),
                    created_at timestamp without time zone NOT NULL,
                    modified_by character varying(110),
                    modified_at timestamp without time zone,
                    deleted boolean NOT NULL
                );
                CREATE INDEX IF NOT EXISTS ix_music_piece_parts_music_piece_id_sort_order ON music_piece_parts (music_piece_id, sort_order);
                CREATE INDEX IF NOT EXISTS ix_music_piece_parts_section_id ON music_piece_parts (section_id);

                CREATE TABLE IF NOT EXISTS music_piece_files (
                    id uuid NOT NULL PRIMARY KEY,
                    music_piece_id uuid NOT NULL REFERENCES music_pieces(id) ON DELETE CASCADE,
                    music_piece_part_id uuid REFERENCES music_piece_parts(id) ON DELETE SET NULL,
                    file_name character varying(500) NOT NULL,
                    storage_file_name character varying(500) NOT NULL,
                    content_type character varying(100) NOT NULL,
                    file_size bigint NOT NULL,
                    description character varying(500),
                    created_by character varying(110),
                    created_at timestamp without time zone NOT NULL,
                    modified_by character varying(110),
                    modified_at timestamp without time zone,
                    deleted boolean NOT NULL
                );
                CREATE INDEX IF NOT EXISTS ix_music_piece_files_music_piece_id ON music_piece_files (music_piece_id);
                CREATE INDEX IF NOT EXISTS ix_music_piece_files_music_piece_part_id ON music_piece_files (music_piece_part_id);

                CREATE TABLE IF NOT EXISTS music_piece_file_roles (
                    id uuid NOT NULL PRIMARY KEY,
                    music_piece_file_id uuid NOT NULL REFERENCES music_piece_files(id) ON DELETE CASCADE,
                    role_id uuid NOT NULL REFERENCES ""AspNetRoles""(id),
                    created_by character varying(110),
                    created_at timestamp without time zone NOT NULL,
                    modified_by character varying(110),
                    modified_at timestamp without time zone,
                    deleted boolean NOT NULL
                );
                CREATE UNIQUE INDEX IF NOT EXISTS ix_music_piece_file_roles_music_piece_file_id_role_id ON music_piece_file_roles (music_piece_file_id, role_id);
                CREATE INDEX IF NOT EXISTS ix_music_piece_file_roles_role_id ON music_piece_file_roles (role_id);
            ");

            // ================================================================
            // From: 20260124100000_AddMusicPieceSelectValues (seed data)
            // ================================================================
            migrationBuilder.Sql(@"
                INSERT INTO select_value_categories (id, created_at, deleted, name, property, ""table"")
                VALUES
                    ('a1b2c3d4-0001-4000-8000-000000000001', '0001-01-01', false, 'Music Epoch', 'Epoch', 'MusicPiece'),
                    ('a1b2c3d4-0002-4000-8000-000000000002', '0001-01-01', false, 'Music Genre', 'Genre', 'MusicPiece'),
                    ('a1b2c3d4-0003-4000-8000-000000000003', '0001-01-01', false, 'Difficulty Level', 'DifficultyLevel', 'MusicPiece')
                ON CONFLICT (id) DO NOTHING;

                INSERT INTO select_values (id, created_at, deleted, description, name)
                VALUES
                    ('b0000001-0001-4000-8000-000000000001', '0001-01-01', false, 'ca. 1600-1750', 'Baroque'),
                    ('b0000001-0002-4000-8000-000000000002', '0001-01-01', false, 'ca. 1750-1820', 'Classical'),
                    ('b0000001-0003-4000-8000-000000000003', '0001-01-01', false, 'ca. 1820-1910', 'Romantic'),
                    ('b0000001-0004-4000-8000-000000000004', '0001-01-01', false, 'ca. 1910-1975', 'Modern'),
                    ('b0000001-0005-4000-8000-000000000005', '0001-01-01', false, 'from 1975', 'Contemporary'),
                    ('b0000002-0001-4000-8000-000000000001', '0001-01-01', false, '', 'Symphonic Music'),
                    ('b0000002-0002-4000-8000-000000000002', '0001-01-01', false, '', 'Concerto'),
                    ('b0000002-0003-4000-8000-000000000003', '0001-01-01', false, '', 'Chamber Music'),
                    ('b0000002-0004-4000-8000-000000000004', '0001-01-01', false, '', 'Opera / Musical Theatre'),
                    ('b0000002-0005-4000-8000-000000000005', '0001-01-01', false, '', 'Sacred Music'),
                    ('b0000002-0006-4000-8000-000000000006', '0001-01-01', false, '', 'Choral Music'),
                    ('b0000003-0001-4000-8000-000000000001', '0001-01-01', false, '', 'Easy'),
                    ('b0000003-0002-4000-8000-000000000002', '0001-01-01', false, '', 'Medium'),
                    ('b0000003-0003-4000-8000-000000000003', '0001-01-01', false, '', 'Advanced'),
                    ('b0000003-0004-4000-8000-000000000004', '0001-01-01', false, '', 'Expert')
                ON CONFLICT (id) DO NOTHING;

                INSERT INTO select_value_mappings (id, created_at, deleted, select_value_category_id, select_value_id, sort_order)
                VALUES
                    ('c0000001-0001-4000-8000-000000000001', '0001-01-01', false, 'a1b2c3d4-0001-4000-8000-000000000001', 'b0000001-0001-4000-8000-000000000001', 10),
                    ('c0000001-0002-4000-8000-000000000002', '0001-01-01', false, 'a1b2c3d4-0001-4000-8000-000000000001', 'b0000001-0002-4000-8000-000000000002', 20),
                    ('c0000001-0003-4000-8000-000000000003', '0001-01-01', false, 'a1b2c3d4-0001-4000-8000-000000000001', 'b0000001-0003-4000-8000-000000000003', 30),
                    ('c0000001-0004-4000-8000-000000000004', '0001-01-01', false, 'a1b2c3d4-0001-4000-8000-000000000001', 'b0000001-0004-4000-8000-000000000004', 40),
                    ('c0000001-0005-4000-8000-000000000005', '0001-01-01', false, 'a1b2c3d4-0001-4000-8000-000000000001', 'b0000001-0005-4000-8000-000000000005', 50),
                    ('c0000002-0001-4000-8000-000000000001', '0001-01-01', false, 'a1b2c3d4-0002-4000-8000-000000000002', 'b0000002-0001-4000-8000-000000000001', 10),
                    ('c0000002-0002-4000-8000-000000000002', '0001-01-01', false, 'a1b2c3d4-0002-4000-8000-000000000002', 'b0000002-0002-4000-8000-000000000002', 20),
                    ('c0000002-0003-4000-8000-000000000003', '0001-01-01', false, 'a1b2c3d4-0002-4000-8000-000000000002', 'b0000002-0003-4000-8000-000000000003', 30),
                    ('c0000002-0004-4000-8000-000000000004', '0001-01-01', false, 'a1b2c3d4-0002-4000-8000-000000000002', 'b0000002-0004-4000-8000-000000000004', 40),
                    ('c0000002-0005-4000-8000-000000000005', '0001-01-01', false, 'a1b2c3d4-0002-4000-8000-000000000002', 'b0000002-0005-4000-8000-000000000005', 50),
                    ('c0000002-0006-4000-8000-000000000006', '0001-01-01', false, 'a1b2c3d4-0002-4000-8000-000000000002', 'b0000002-0006-4000-8000-000000000006', 60),
                    ('c0000003-0001-4000-8000-000000000001', '0001-01-01', false, 'a1b2c3d4-0003-4000-8000-000000000003', 'b0000003-0001-4000-8000-000000000001', 10),
                    ('c0000003-0002-4000-8000-000000000002', '0001-01-01', false, 'a1b2c3d4-0003-4000-8000-000000000003', 'b0000003-0002-4000-8000-000000000002', 20),
                    ('c0000003-0003-4000-8000-000000000003', '0001-01-01', false, 'a1b2c3d4-0003-4000-8000-000000000003', 'b0000003-0003-4000-8000-000000000003', 30),
                    ('c0000003-0004-4000-8000-000000000004', '0001-01-01', false, 'a1b2c3d4-0003-4000-8000-000000000003', 'b0000003-0004-4000-8000-000000000004', 40)
                ON CONFLICT (id) DO NOTHING;
            ");

            // ================================================================
            // From: 20260124120000_AddMusicPieceFileSections
            // ================================================================
            migrationBuilder.Sql(@"
                CREATE TABLE IF NOT EXISTS music_piece_file_sections (
                    id uuid NOT NULL PRIMARY KEY,
                    music_piece_file_id uuid NOT NULL REFERENCES music_piece_files(id) ON DELETE CASCADE,
                    section_id uuid NOT NULL REFERENCES sections(id) ON DELETE CASCADE,
                    created_by character varying(110),
                    created_at timestamp without time zone,
                    modified_by character varying(110),
                    modified_at timestamp without time zone,
                    deleted boolean NOT NULL DEFAULT false
                );
                CREATE UNIQUE INDEX IF NOT EXISTS ix_music_piece_file_sections_music_piece_file_id_section_id ON music_piece_file_sections (music_piece_file_id, section_id);
                CREATE INDEX IF NOT EXISTS ix_music_piece_file_sections_section_id ON music_piece_file_sections (section_id);
            ");

            // ================================================================
            // From: 20260124130000_AddSetlists
            // ================================================================
            migrationBuilder.Sql(@"
                CREATE TABLE IF NOT EXISTS setlists (
                    id uuid NOT NULL PRIMARY KEY,
                    name character varying(200) NOT NULL,
                    description character varying(2000),
                    is_template boolean NOT NULL DEFAULT false,
                    created_by character varying(110),
                    created_at timestamp without time zone NOT NULL,
                    modified_by character varying(110),
                    modified_at timestamp without time zone,
                    deleted boolean NOT NULL
                );
                CREATE INDEX IF NOT EXISTS ix_setlists_name ON setlists (name);
                CREATE INDEX IF NOT EXISTS ix_setlists_is_template ON setlists (is_template);

                CREATE TABLE IF NOT EXISTS setlist_pieces (
                    id uuid NOT NULL PRIMARY KEY,
                    setlist_id uuid NOT NULL REFERENCES setlists(id) ON DELETE CASCADE,
                    music_piece_id uuid NOT NULL REFERENCES music_pieces(id) ON DELETE CASCADE,
                    sort_order integer NOT NULL,
                    notes character varying(1000),
                    created_by character varying(110),
                    created_at timestamp without time zone NOT NULL,
                    modified_by character varying(110),
                    modified_at timestamp without time zone,
                    deleted boolean NOT NULL
                );
                CREATE INDEX IF NOT EXISTS ix_setlist_pieces_setlist_id_sort_order ON setlist_pieces (setlist_id, sort_order);
                CREATE INDEX IF NOT EXISTS ix_setlist_pieces_music_piece_id ON setlist_pieces (music_piece_id);
            ");

            // ================================================================
            // From: 20260125100000_AddSetlistToProject
            // ================================================================
            migrationBuilder.Sql(@"
                DO $$ BEGIN
                    IF NOT EXISTS (SELECT 1 FROM information_schema.columns
                        WHERE table_name='projects' AND column_name='setlist_id') THEN
                        ALTER TABLE projects ADD COLUMN setlist_id uuid REFERENCES setlists(id) ON DELETE SET NULL;
                        CREATE INDEX ix_projects_setlist_id ON projects (setlist_id);
                    END IF;
                END $$;

                CREATE TABLE IF NOT EXISTS appointment_setlist_pieces (
                    id uuid NOT NULL PRIMARY KEY,
                    appointment_id uuid NOT NULL REFERENCES appointments(id) ON DELETE CASCADE,
                    setlist_piece_id uuid NOT NULL REFERENCES setlist_pieces(id) ON DELETE CASCADE,
                    created_by character varying(110),
                    created_at timestamp without time zone NOT NULL,
                    modified_by character varying(110),
                    modified_at timestamp without time zone,
                    deleted boolean NOT NULL
                );
                CREATE UNIQUE INDEX IF NOT EXISTS ix_appointment_setlist_pieces_appointment_id_setlist_piece_id ON appointment_setlist_pieces (appointment_id, setlist_piece_id);
                CREATE INDEX IF NOT EXISTS ix_appointment_setlist_pieces_setlist_piece_id ON appointment_setlist_pieces (setlist_piece_id);
            ");

            // ================================================================
            // From: 20260127100000_AddStageSetup
            // ================================================================
            migrationBuilder.Sql(@"
                CREATE TABLE IF NOT EXISTS stage_setups (
                    id uuid NOT NULL PRIMARY KEY,
                    name character varying(200) NOT NULL,
                    file_name character varying(500) NOT NULL,
                    storage_path character varying(1000) NOT NULL,
                    content_type character varying(100) NOT NULL,
                    file_size bigint NOT NULL,
                    canvas_width integer NOT NULL,
                    canvas_height integer NOT NULL,
                    is_active boolean NOT NULL DEFAULT false,
                    is_visible_to_performers boolean NOT NULL DEFAULT false,
                    project_id uuid NOT NULL REFERENCES projects(id) ON DELETE CASCADE,
                    created_by character varying(110),
                    created_at timestamp without time zone NOT NULL,
                    modified_by character varying(110),
                    modified_at timestamp without time zone,
                    deleted boolean NOT NULL
                );
                CREATE INDEX IF NOT EXISTS ix_stage_setups_project_id ON stage_setups (project_id);
                CREATE INDEX IF NOT EXISTS ix_stage_setups_project_id_is_active ON stage_setups (project_id, is_active);

                CREATE TABLE IF NOT EXISTS stage_setup_positions (
                    id uuid NOT NULL PRIMARY KEY,
                    position_x double precision NOT NULL,
                    position_y double precision NOT NULL,
                    row integer,
                    stand integer,
                    stage_setup_id uuid NOT NULL REFERENCES stage_setups(id) ON DELETE CASCADE,
                    musician_profile_id uuid NOT NULL REFERENCES musician_profiles(id) ON DELETE CASCADE,
                    created_by character varying(110),
                    created_at timestamp without time zone NOT NULL,
                    modified_by character varying(110),
                    modified_at timestamp without time zone,
                    deleted boolean NOT NULL
                );
                CREATE INDEX IF NOT EXISTS ix_stage_setup_positions_stage_setup_id ON stage_setup_positions (stage_setup_id);
                CREATE INDEX IF NOT EXISTS ix_stage_setup_positions_musician_profile_id ON stage_setup_positions (musician_profile_id);
                CREATE UNIQUE INDEX IF NOT EXISTS ix_stage_setup_positions_stage_setup_id_musician_profile_id ON stage_setup_positions (stage_setup_id, musician_profile_id);
            ");

            // ================================================================
            // From: 20260129020000_AddStageSetupEquipment
            // ================================================================
            migrationBuilder.Sql(@"
                CREATE TABLE IF NOT EXISTS stage_setup_equipment (
                    id uuid NOT NULL PRIMARY KEY,
                    equipment_id character varying(100) NOT NULL,
                    position_x double precision NOT NULL,
                    position_y double precision NOT NULL,
                    rotation double precision NOT NULL,
                    stage_setup_id uuid NOT NULL REFERENCES stage_setups(id) ON DELETE CASCADE,
                    created_by character varying(110),
                    created_at timestamp without time zone NOT NULL,
                    modified_by character varying(110),
                    modified_at timestamp without time zone,
                    deleted boolean NOT NULL
                );
                CREATE INDEX IF NOT EXISTS ix_stage_setup_equipment_stage_setup_id ON stage_setup_equipment (stage_setup_id);
            ");

            // ================================================================
            // From: 20260203140000_AddMusicPieceUrlAndSelectValues
            // ================================================================
            migrationBuilder.Sql(@"
                CREATE TABLE IF NOT EXISTS music_piece_urls (
                    id uuid NOT NULL PRIMARY KEY,
                    music_piece_id uuid NOT NULL REFERENCES music_pieces(id) ON DELETE CASCADE,
                    href character varying(1000) NOT NULL,
                    anchor_text character varying(200),
                    url_type_id uuid REFERENCES select_value_mappings(id),
                    created_by character varying(110),
                    created_at timestamp without time zone NOT NULL,
                    modified_by character varying(110),
                    modified_at timestamp without time zone,
                    deleted boolean NOT NULL
                );
                CREATE INDEX IF NOT EXISTS ix_music_piece_urls_music_piece_id ON music_piece_urls (music_piece_id);
                CREATE INDEX IF NOT EXISTS ix_music_piece_urls_url_type_id ON music_piece_urls (url_type_id);

                INSERT INTO select_value_categories (id, created_at, deleted, name, property, ""table"")
                VALUES ('a1b2c3d4-0004-4000-8000-000000000004', '0001-01-01', false, 'URL Type', 'UrlType', 'MusicPieceUrl')
                ON CONFLICT (id) DO NOTHING;

                INSERT INTO select_values (id, created_at, deleted, description, name)
                VALUES
                    ('b0000003-0005-4000-8000-000000000005', '0001-01-01', false, 'Highest difficulty level', 'Virtuoso'),
                    ('b0000004-0001-4000-8000-000000000001', '0001-01-01', false, '', 'YouTube Video'),
                    ('b0000004-0002-4000-8000-000000000002', '0001-01-01', false, '', 'Publisher'),
                    ('b0000004-0003-4000-8000-000000000003', '0001-01-01', false, '', 'IMDB'),
                    ('b0000004-0004-4000-8000-000000000004', '0001-01-01', false, '', 'Wikipedia'),
                    ('b0000004-0005-4000-8000-000000000005', '0001-01-01', false, '', 'Other')
                ON CONFLICT (id) DO NOTHING;

                INSERT INTO select_value_mappings (id, created_at, deleted, select_value_category_id, select_value_id, sort_order)
                VALUES
                    ('c0000002-0007-4000-8000-000000000007', '0001-01-01', false, 'a1b2c3d4-0002-4000-8000-000000000002', 'a3be7b91-7548-492e-99dc-2788497f2930', 70),
                    ('c0000003-0005-4000-8000-000000000005', '0001-01-01', false, 'a1b2c3d4-0003-4000-8000-000000000003', 'b0000003-0005-4000-8000-000000000005', 50),
                    ('c0000004-0001-4000-8000-000000000001', '0001-01-01', false, 'a1b2c3d4-0004-4000-8000-000000000004', 'b0000004-0001-4000-8000-000000000001', 10),
                    ('c0000004-0002-4000-8000-000000000002', '0001-01-01', false, 'a1b2c3d4-0004-4000-8000-000000000004', 'b0000004-0002-4000-8000-000000000002', 20),
                    ('c0000004-0003-4000-8000-000000000003', '0001-01-01', false, 'a1b2c3d4-0004-4000-8000-000000000004', 'b0000004-0003-4000-8000-000000000003', 30),
                    ('c0000004-0004-4000-8000-000000000004', '0001-01-01', false, 'a1b2c3d4-0004-4000-8000-000000000004', 'b0000004-0004-4000-8000-000000000004', 40),
                    ('c0000004-0005-4000-8000-000000000005', '0001-01-01', false, 'a1b2c3d4-0004-4000-8000-000000000004', 'b0000004-0005-4000-8000-000000000005', 50)
                ON CONFLICT (id) DO NOTHING;
            ");

            // ================================================================
            // From: 20260203220000_AddMusicPieceTodo
            // Note: Original used PascalCase table names (non-standard).
            // The ModelSnapshot uses snake_case. Using snake_case here.
            // ================================================================
            migrationBuilder.Sql(@"
                CREATE TABLE IF NOT EXISTS music_piece_todos (
                    id uuid NOT NULL PRIMARY KEY,
                    music_piece_id uuid NOT NULL REFERENCES music_pieces(id) ON DELETE CASCADE,
                    title character varying(500) NOT NULL,
                    due_date timestamp without time zone,
                    is_completed boolean NOT NULL,
                    completed_at timestamp without time zone,
                    created_by character varying(110),
                    created_at timestamp without time zone NOT NULL,
                    modified_by character varying(110),
                    modified_at timestamp without time zone,
                    deleted boolean NOT NULL
                );
                CREATE INDEX IF NOT EXISTS ix_music_piece_todos_due_date ON music_piece_todos (due_date);
                CREATE INDEX IF NOT EXISTS ix_music_piece_todos_is_completed ON music_piece_todos (is_completed);
                CREATE INDEX IF NOT EXISTS ix_music_piece_todos_music_piece_id ON music_piece_todos (music_piece_id);

                CREATE TABLE IF NOT EXISTS music_piece_todo_assignee (
                    music_piece_todo_id uuid NOT NULL REFERENCES music_piece_todos(id) ON DELETE CASCADE,
                    user_id uuid NOT NULL REFERENCES ""AspNetUsers""(id) ON DELETE CASCADE,
                    PRIMARY KEY (music_piece_todo_id, user_id)
                );
                CREATE INDEX IF NOT EXISTS ix_music_piece_todo_assignee_music_piece_todo_id_user_id ON music_piece_todo_assignee (music_piece_todo_id, user_id);
                CREATE INDEX IF NOT EXISTS ix_music_piece_todo_assignee_user_id ON music_piece_todo_assignee (user_id);
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP TABLE IF EXISTS music_piece_todo_assignee;");
            migrationBuilder.Sql("DROP TABLE IF EXISTS music_piece_todos;");
            migrationBuilder.Sql("DROP TABLE IF EXISTS music_piece_urls;");
            migrationBuilder.Sql("DROP TABLE IF EXISTS stage_setup_equipment;");
            migrationBuilder.Sql("DROP TABLE IF EXISTS stage_setup_positions;");
            migrationBuilder.Sql("DROP TABLE IF EXISTS stage_setups;");
            migrationBuilder.Sql("DROP TABLE IF EXISTS appointment_setlist_pieces;");
            migrationBuilder.Sql("DROP TABLE IF EXISTS setlist_pieces;");
            migrationBuilder.Sql("DROP TABLE IF EXISTS setlists;");
            migrationBuilder.Sql("DROP TABLE IF EXISTS music_piece_file_sections;");
            migrationBuilder.Sql("DROP TABLE IF EXISTS music_piece_file_roles;");
            migrationBuilder.Sql("DROP TABLE IF EXISTS music_piece_files;");
            migrationBuilder.Sql("DROP TABLE IF EXISTS music_piece_parts;");
            migrationBuilder.Sql("DROP TABLE IF EXISTS music_pieces;");
        }
    }
}
