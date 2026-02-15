using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orso.Arpa.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddInstrumentationEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "instrumentations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    is_template = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    project_id = table.Column<Guid>(type: "uuid", nullable: true),
                    source_template_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_instrumentations", x => x.id);
                    table.ForeignKey(
                        name: "fk_instrumentations_projects_project_id",
                        column: x => x.project_id,
                        principalTable: "projects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "instrumentation_positions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    instrumentation_id = table.Column<Guid>(type: "uuid", nullable: false),
                    section_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    qualification_id = table.Column<Guid>(type: "uuid", nullable: true),
                    sort_order = table.Column<int>(type: "integer", nullable: false),
                    label = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    comment = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    created_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_instrumentation_positions", x => x.id);
                    table.ForeignKey(
                        name: "fk_instrumentation_positions_instrumentations_instrumentation_",
                        column: x => x.instrumentation_id,
                        principalTable: "instrumentations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_instrumentation_positions_sections_section_id",
                        column: x => x.section_id,
                        principalTable: "sections",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_instrumentation_positions_select_value_mappings_qualificati",
                        column: x => x.qualification_id,
                        principalTable: "select_value_mappings",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "instrumentation_position_doublings",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    instrumentation_position_id = table.Column<Guid>(type: "uuid", nullable: false),
                    section_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_instrumentation_position_doublings", x => x.id);
                    table.ForeignKey(
                        name: "fk_instrumentation_position_doublings_instrumentation_position",
                        column: x => x.instrumentation_position_id,
                        principalTable: "instrumentation_positions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_instrumentation_position_doublings_sections_section_id",
                        column: x => x.section_id,
                        principalTable: "sections",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_instrumentation_position_doublings_instrumentation_position",
                table: "instrumentation_position_doublings",
                column: "instrumentation_position_id");

            migrationBuilder.CreateIndex(
                name: "ix_instrumentation_position_doublings_section_id",
                table: "instrumentation_position_doublings",
                column: "section_id");

            migrationBuilder.CreateIndex(
                name: "ix_instrumentation_positions_instrumentation_id",
                table: "instrumentation_positions",
                column: "instrumentation_id");

            migrationBuilder.CreateIndex(
                name: "ix_instrumentation_positions_qualification_id",
                table: "instrumentation_positions",
                column: "qualification_id");

            migrationBuilder.CreateIndex(
                name: "ix_instrumentation_positions_section_id",
                table: "instrumentation_positions",
                column: "section_id");

            migrationBuilder.CreateIndex(
                name: "ix_instrumentations_is_template",
                table: "instrumentations",
                column: "is_template");

            migrationBuilder.CreateIndex(
                name: "ix_instrumentations_is_template_project_id",
                table: "instrumentations",
                columns: new[] { "is_template", "project_id" });

            migrationBuilder.CreateIndex(
                name: "ix_instrumentations_project_id",
                table: "instrumentations",
                column: "project_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "instrumentation_position_doublings");

            migrationBuilder.DropTable(
                name: "instrumentation_positions");

            migrationBuilder.DropTable(
                name: "instrumentations");
        }
    }
}
