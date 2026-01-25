using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orso.Arpa.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddMusicLibrary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // MusicPiece table
            migrationBuilder.CreateTable(
                name: "music_pieces",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    composer = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    arranger = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    subtitle = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    duration = table.Column<int>(type: "integer", nullable: true),
                    year_composed = table.Column<int>(type: "integer", nullable: true),
                    opus = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    instrumentation = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    epoch_id = table.Column<Guid>(type: "uuid", nullable: true),
                    genre_id = table.Column<Guid>(type: "uuid", nullable: true),
                    difficulty_level_id = table.Column<Guid>(type: "uuid", nullable: true),
                    performance_notes = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: true),
                    internal_notes = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: true),
                    is_archived = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    created_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_music_pieces", x => x.id);
                    table.ForeignKey(
                        name: "fk_music_pieces_select_value_mappings_epoch_id",
                        column: x => x.epoch_id,
                        principalTable: "select_value_mappings",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_music_pieces_select_value_mappings_genre_id",
                        column: x => x.genre_id,
                        principalTable: "select_value_mappings",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_music_pieces_select_value_mappings_difficulty_level_id",
                        column: x => x.difficulty_level_id,
                        principalTable: "select_value_mappings",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_music_pieces_title",
                table: "music_pieces",
                column: "title");

            migrationBuilder.CreateIndex(
                name: "ix_music_pieces_composer",
                table: "music_pieces",
                column: "composer");

            migrationBuilder.CreateIndex(
                name: "ix_music_pieces_is_archived",
                table: "music_pieces",
                column: "is_archived");

            migrationBuilder.CreateIndex(
                name: "ix_music_pieces_epoch_id",
                table: "music_pieces",
                column: "epoch_id");

            migrationBuilder.CreateIndex(
                name: "ix_music_pieces_genre_id",
                table: "music_pieces",
                column: "genre_id");

            migrationBuilder.CreateIndex(
                name: "ix_music_pieces_difficulty_level_id",
                table: "music_pieces",
                column: "difficulty_level_id");

            // MusicPiecePart table
            migrationBuilder.CreateTable(
                name: "music_piece_parts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    music_piece_id = table.Column<Guid>(type: "uuid", nullable: false),
                    section_id = table.Column<Guid>(type: "uuid", nullable: true),
                    part_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    sort_order = table.Column<int>(type: "integer", nullable: false),
                    created_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_music_piece_parts", x => x.id);
                    table.ForeignKey(
                        name: "fk_music_piece_parts_music_pieces_music_piece_id",
                        column: x => x.music_piece_id,
                        principalTable: "music_pieces",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_music_piece_parts_sections_section_id",
                        column: x => x.section_id,
                        principalTable: "sections",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_music_piece_parts_music_piece_id_sort_order",
                table: "music_piece_parts",
                columns: new[] { "music_piece_id", "sort_order" });

            migrationBuilder.CreateIndex(
                name: "ix_music_piece_parts_section_id",
                table: "music_piece_parts",
                column: "section_id");

            // MusicPieceFile table
            migrationBuilder.CreateTable(
                name: "music_piece_files",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    music_piece_id = table.Column<Guid>(type: "uuid", nullable: false),
                    music_piece_part_id = table.Column<Guid>(type: "uuid", nullable: true),
                    file_name = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    storage_file_name = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    content_type = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    file_size = table.Column<long>(type: "bigint", nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    created_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_music_piece_files", x => x.id);
                    table.ForeignKey(
                        name: "fk_music_piece_files_music_pieces_music_piece_id",
                        column: x => x.music_piece_id,
                        principalTable: "music_pieces",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_music_piece_files_music_piece_parts_music_piece_part_id",
                        column: x => x.music_piece_part_id,
                        principalTable: "music_piece_parts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "ix_music_piece_files_music_piece_id",
                table: "music_piece_files",
                column: "music_piece_id");

            migrationBuilder.CreateIndex(
                name: "ix_music_piece_files_music_piece_part_id",
                table: "music_piece_files",
                column: "music_piece_part_id");

            // MusicPieceFileRole table (for role-based access control)
            migrationBuilder.CreateTable(
                name: "music_piece_file_roles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    music_piece_file_id = table.Column<Guid>(type: "uuid", nullable: false),
                    role_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_music_piece_file_roles", x => x.id);
                    table.ForeignKey(
                        name: "fk_music_piece_file_roles_music_piece_files_music_piece_file_id",
                        column: x => x.music_piece_file_id,
                        principalTable: "music_piece_files",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_music_piece_file_roles_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "asp_net_roles",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_music_piece_file_roles_music_piece_file_id_role_id",
                table: "music_piece_file_roles",
                columns: new[] { "music_piece_file_id", "role_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_music_piece_file_roles_role_id",
                table: "music_piece_file_roles",
                column: "role_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "music_piece_file_roles");
            migrationBuilder.DropTable(name: "music_piece_files");
            migrationBuilder.DropTable(name: "music_piece_parts");
            migrationBuilder.DropTable(name: "music_pieces");
        }
    }
}
