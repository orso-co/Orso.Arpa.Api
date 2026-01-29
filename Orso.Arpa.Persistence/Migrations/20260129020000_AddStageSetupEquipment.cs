using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orso.Arpa.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddStageSetupEquipment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Create stage_setup_equipment table
            migrationBuilder.CreateTable(
                name: "stage_setup_equipment",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    equipment_id = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    position_x = table.Column<double>(type: "double precision", nullable: false),
                    position_y = table.Column<double>(type: "double precision", nullable: false),
                    rotation = table.Column<double>(type: "double precision", nullable: false),
                    stage_setup_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_stage_setup_equipment", x => x.id);
                    table.ForeignKey(
                        name: "fk_stage_setup_equipment_stage_setups_stage_setup_id",
                        column: x => x.stage_setup_id,
                        principalTable: "stage_setups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            // Index for faster queries by setup
            migrationBuilder.CreateIndex(
                name: "ix_stage_setup_equipment_stage_setup_id",
                table: "stage_setup_equipment",
                column: "stage_setup_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "stage_setup_equipment");
        }
    }
}
