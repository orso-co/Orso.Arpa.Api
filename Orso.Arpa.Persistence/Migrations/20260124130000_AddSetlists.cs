using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orso.Arpa.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddSetlists : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Setlist table
            migrationBuilder.CreateTable(
                name: "setlists",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    is_template = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    created_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_setlists", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_setlists_name",
                table: "setlists",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "ix_setlists_is_template",
                table: "setlists",
                column: "is_template");

            // SetlistPiece table
            migrationBuilder.CreateTable(
                name: "setlist_pieces",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    setlist_id = table.Column<Guid>(type: "uuid", nullable: false),
                    music_piece_id = table.Column<Guid>(type: "uuid", nullable: false),
                    sort_order = table.Column<int>(type: "integer", nullable: false),
                    notes = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    created_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_setlist_pieces", x => x.id);
                    table.ForeignKey(
                        name: "fk_setlist_pieces_setlists_setlist_id",
                        column: x => x.setlist_id,
                        principalTable: "setlists",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_setlist_pieces_music_pieces_music_piece_id",
                        column: x => x.music_piece_id,
                        principalTable: "music_pieces",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_setlist_pieces_setlist_id_sort_order",
                table: "setlist_pieces",
                columns: new[] { "setlist_id", "sort_order" });

            migrationBuilder.CreateIndex(
                name: "ix_setlist_pieces_music_piece_id",
                table: "setlist_pieces",
                column: "music_piece_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "setlist_pieces");
            migrationBuilder.DropTable(name: "setlists");
        }
    }
}
