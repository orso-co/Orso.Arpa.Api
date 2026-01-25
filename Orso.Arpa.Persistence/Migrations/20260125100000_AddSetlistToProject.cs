using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orso.Arpa.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddSetlistToProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Add SetlistId to Project
            migrationBuilder.AddColumn<Guid>(
                name: "setlist_id",
                table: "projects",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_projects_setlist_id",
                table: "projects",
                column: "setlist_id");

            migrationBuilder.AddForeignKey(
                name: "fk_projects_setlists_setlist_id",
                table: "projects",
                column: "setlist_id",
                principalTable: "setlists",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            // AppointmentSetlistPiece table (prioritized pieces per appointment)
            migrationBuilder.CreateTable(
                name: "appointment_setlist_pieces",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    appointment_id = table.Column<Guid>(type: "uuid", nullable: false),
                    setlist_piece_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_appointment_setlist_pieces", x => x.id);
                    table.ForeignKey(
                        name: "fk_appointment_setlist_pieces_appointments_appointment_id",
                        column: x => x.appointment_id,
                        principalTable: "appointments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_appointment_setlist_pieces_setlist_pieces_setlist_piece_id",
                        column: x => x.setlist_piece_id,
                        principalTable: "setlist_pieces",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_appointment_setlist_pieces_appointment_id_setlist_piece_id",
                table: "appointment_setlist_pieces",
                columns: new[] { "appointment_id", "setlist_piece_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_appointment_setlist_pieces_setlist_piece_id",
                table: "appointment_setlist_pieces",
                column: "setlist_piece_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "appointment_setlist_pieces");

            migrationBuilder.DropForeignKey(
                name: "fk_projects_setlists_setlist_id",
                table: "projects");

            migrationBuilder.DropIndex(
                name: "ix_projects_setlist_id",
                table: "projects");

            migrationBuilder.DropColumn(
                name: "setlist_id",
                table: "projects");
        }
    }
}
