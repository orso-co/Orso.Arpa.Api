using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Orso.Arpa.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddMusicPieceUrlAndSelectValues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Create MusicPieceUrl table
            migrationBuilder.CreateTable(
                name: "music_piece_urls",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    music_piece_id = table.Column<Guid>(type: "uuid", nullable: false),
                    href = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    anchor_text = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    url_type_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_music_piece_urls", x => x.id);
                    table.ForeignKey(
                        name: "fk_music_piece_urls_music_pieces_music_piece_id",
                        column: x => x.music_piece_id,
                        principalTable: "music_pieces",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_music_piece_urls_select_value_mappings_url_type_id",
                        column: x => x.url_type_id,
                        principalTable: "select_value_mappings",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_music_piece_urls_music_piece_id",
                table: "music_piece_urls",
                column: "music_piece_id");

            migrationBuilder.CreateIndex(
                name: "ix_music_piece_urls_url_type_id",
                table: "music_piece_urls",
                column: "url_type_id");

            // 2. Insert SelectValueCategory for MusicPieceUrlType
            migrationBuilder.InsertData(
                table: "select_value_categories",
                columns: new[] { "id", "created_at", "created_by", "deleted", "modified_at", "modified_by", "name", "property", "table" },
                values: new object[] { new Guid("a1b2c3d4-0004-4000-8000-000000000004"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "URL Type", "UrlType", "MusicPieceUrl" });

            // 3. Insert new SelectValues (Virtuoso difficulty + URL types)
            migrationBuilder.InsertData(
                table: "select_values",
                columns: new[] { "id", "created_at", "created_by", "deleted", "description", "modified_at", "modified_by", "name" },
                values: new object[,]
                {
                    // Virtuoso difficulty level
                    { new Guid("b0000003-0005-4000-8000-000000000005"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Highest difficulty level", null, null, "Virtuoso" },
                    // URL Types
                    { new Guid("b0000004-0001-4000-8000-000000000001"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "YouTube Video" },
                    { new Guid("b0000004-0002-4000-8000-000000000002"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Publisher" },
                    { new Guid("b0000004-0003-4000-8000-000000000003"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "IMDB" },
                    { new Guid("b0000004-0004-4000-8000-000000000004"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Wikipedia" },
                    { new Guid("b0000004-0005-4000-8000-000000000005"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Other" }
                });

            // 4. Insert SelectValueMappings
            migrationBuilder.InsertData(
                table: "select_value_mappings",
                columns: new[] { "id", "created_at", "created_by", "deleted", "modified_at", "modified_by", "select_value_category_id", "select_value_id", "sort_order" },
                values: new object[,]
                {
                    // Film Music genre mapping (FilmMusic SelectValue already exists in seed data as a3be7b91-7548-492e-99dc-2788497f2930)
                    { new Guid("c0000002-0007-4000-8000-000000000007"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a1b2c3d4-0002-4000-8000-000000000002"), new Guid("a3be7b91-7548-492e-99dc-2788497f2930"), 70 },
                    // Virtuoso difficulty level mapping
                    { new Guid("c0000003-0005-4000-8000-000000000005"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a1b2c3d4-0003-4000-8000-000000000003"), new Guid("b0000003-0005-4000-8000-000000000005"), 50 },
                    // URL Type mappings
                    { new Guid("c0000004-0001-4000-8000-000000000001"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a1b2c3d4-0004-4000-8000-000000000004"), new Guid("b0000004-0001-4000-8000-000000000001"), 10 },
                    { new Guid("c0000004-0002-4000-8000-000000000002"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a1b2c3d4-0004-4000-8000-000000000004"), new Guid("b0000004-0002-4000-8000-000000000002"), 20 },
                    { new Guid("c0000004-0003-4000-8000-000000000003"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a1b2c3d4-0004-4000-8000-000000000004"), new Guid("b0000004-0003-4000-8000-000000000003"), 30 },
                    { new Guid("c0000004-0004-4000-8000-000000000004"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a1b2c3d4-0004-4000-8000-000000000004"), new Guid("b0000004-0004-4000-8000-000000000004"), 40 },
                    { new Guid("c0000004-0005-4000-8000-000000000005"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a1b2c3d4-0004-4000-8000-000000000004"), new Guid("b0000004-0005-4000-8000-000000000005"), 50 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Delete SelectValueMappings
            migrationBuilder.DeleteData(table: "select_value_mappings", keyColumn: "id", keyValue: new Guid("c0000002-0007-4000-8000-000000000007"));
            migrationBuilder.DeleteData(table: "select_value_mappings", keyColumn: "id", keyValue: new Guid("c0000003-0005-4000-8000-000000000005"));
            migrationBuilder.DeleteData(table: "select_value_mappings", keyColumn: "id", keyValue: new Guid("c0000004-0001-4000-8000-000000000001"));
            migrationBuilder.DeleteData(table: "select_value_mappings", keyColumn: "id", keyValue: new Guid("c0000004-0002-4000-8000-000000000002"));
            migrationBuilder.DeleteData(table: "select_value_mappings", keyColumn: "id", keyValue: new Guid("c0000004-0003-4000-8000-000000000003"));
            migrationBuilder.DeleteData(table: "select_value_mappings", keyColumn: "id", keyValue: new Guid("c0000004-0004-4000-8000-000000000004"));
            migrationBuilder.DeleteData(table: "select_value_mappings", keyColumn: "id", keyValue: new Guid("c0000004-0005-4000-8000-000000000005"));

            // Delete SelectValues
            migrationBuilder.DeleteData(table: "select_values", keyColumn: "id", keyValue: new Guid("b0000003-0005-4000-8000-000000000005"));
            migrationBuilder.DeleteData(table: "select_values", keyColumn: "id", keyValue: new Guid("b0000004-0001-4000-8000-000000000001"));
            migrationBuilder.DeleteData(table: "select_values", keyColumn: "id", keyValue: new Guid("b0000004-0002-4000-8000-000000000002"));
            migrationBuilder.DeleteData(table: "select_values", keyColumn: "id", keyValue: new Guid("b0000004-0003-4000-8000-000000000003"));
            migrationBuilder.DeleteData(table: "select_values", keyColumn: "id", keyValue: new Guid("b0000004-0004-4000-8000-000000000004"));
            migrationBuilder.DeleteData(table: "select_values", keyColumn: "id", keyValue: new Guid("b0000004-0005-4000-8000-000000000005"));

            // Delete SelectValueCategory
            migrationBuilder.DeleteData(table: "select_value_categories", keyColumn: "id", keyValue: new Guid("a1b2c3d4-0004-4000-8000-000000000004"));

            // Drop MusicPieceUrl table
            migrationBuilder.DropTable(name: "music_piece_urls");
        }
    }
}
