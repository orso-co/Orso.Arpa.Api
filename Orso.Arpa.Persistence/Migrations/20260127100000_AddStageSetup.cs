using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orso.Arpa.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddStageSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Create stage_setups table
            migrationBuilder.CreateTable(
                name: "stage_setups",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    file_name = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    storage_path = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    content_type = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    file_size = table.Column<long>(type: "bigint", nullable: false),
                    canvas_width = table.Column<int>(type: "integer", nullable: false),
                    canvas_height = table.Column<int>(type: "integer", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    is_visible_to_performers = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    project_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_stage_setups", x => x.id);
                    table.ForeignKey(
                        name: "fk_stage_setups_projects_project_id",
                        column: x => x.project_id,
                        principalTable: "projects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            // Create stage_setup_positions table
            migrationBuilder.CreateTable(
                name: "stage_setup_positions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    position_x = table.Column<double>(type: "double precision", nullable: false),
                    position_y = table.Column<double>(type: "double precision", nullable: false),
                    row = table.Column<int>(type: "integer", nullable: true),
                    stand = table.Column<int>(type: "integer", nullable: true),
                    stage_setup_id = table.Column<Guid>(type: "uuid", nullable: false),
                    musician_profile_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_stage_setup_positions", x => x.id);
                    table.ForeignKey(
                        name: "fk_stage_setup_positions_stage_setups_stage_setup_id",
                        column: x => x.stage_setup_id,
                        principalTable: "stage_setups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_stage_setup_positions_musician_profiles_musician_profile_id",
                        column: x => x.musician_profile_id,
                        principalTable: "musician_profiles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            // Indexes for stage_setups
            migrationBuilder.CreateIndex(
                name: "ix_stage_setups_project_id",
                table: "stage_setups",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "ix_stage_setups_project_id_is_active",
                table: "stage_setups",
                columns: new[] { "project_id", "is_active" });

            // Indexes for stage_setup_positions
            migrationBuilder.CreateIndex(
                name: "ix_stage_setup_positions_stage_setup_id",
                table: "stage_setup_positions",
                column: "stage_setup_id");

            migrationBuilder.CreateIndex(
                name: "ix_stage_setup_positions_musician_profile_id",
                table: "stage_setup_positions",
                column: "musician_profile_id");

            // Unique constraint: each musician can only be positioned once per setup
            migrationBuilder.CreateIndex(
                name: "ix_stage_setup_positions_stage_setup_id_musician_profile_id",
                table: "stage_setup_positions",
                columns: new[] { "stage_setup_id", "musician_profile_id" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "stage_setup_positions");

            migrationBuilder.DropTable(
                name: "stage_setups");
        }
    }
}
