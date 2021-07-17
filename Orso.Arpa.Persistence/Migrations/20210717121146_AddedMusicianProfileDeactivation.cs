using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Orso.Arpa.Persistence.Migrations
{
    public partial class AddedMusicianProfileDeactivation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_deactivated",
                table: "musician_profiles");

            migrationBuilder.CreateTable(
                name: "musician_profile_deactivations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    deactivation_start = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    purpose = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    musician_profile_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_musician_profile_deactivations", x => x.id);
                    table.ForeignKey(
                        name: "fk_musician_profile_deactivations_musician_profiles_musician_p",
                        column: x => x.musician_profile_id,
                        principalTable: "musician_profiles",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_musician_profile_deactivations_musician_profile_id",
                table: "musician_profile_deactivations",
                column: "musician_profile_id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "musician_profile_deactivations");

            migrationBuilder.AddColumn<bool>(
                name: "is_deactivated",
                table: "musician_profiles",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
