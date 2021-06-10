using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Orso.Arpa.Persistence.Migrations
{
    public partial class NamingConventionAppliedToMusicianProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_musician_profiles_select_value_mappings_inquiry_status_perf",
                table: "musician_profiles");

            migrationBuilder.DropForeignKey(
                name: "fk_musician_profiles_select_value_mappings_inquiry_status_staf",
                table: "musician_profiles");

            migrationBuilder.DropTable(
                name: "musician_profile_positions_performer");

            migrationBuilder.DropTable(
                name: "musician_profile_positions_staff");

            migrationBuilder.RenameColumn(
                name: "profile_preference_staff",
                table: "musician_profiles",
                newName: "profile_preference_team");

            migrationBuilder.RenameColumn(
                name: "profile_preference_performer",
                table: "musician_profiles",
                newName: "profile_preference_inner");

            migrationBuilder.RenameColumn(
                name: "preferred_parts_staff",
                table: "musician_profiles",
                newName: "preferred_parts_team");

            migrationBuilder.RenameColumn(
                name: "preferred_parts_performer",
                table: "musician_profiles",
                newName: "preferred_parts_inner");

            migrationBuilder.RenameColumn(
                name: "level_assessment_staff",
                table: "musician_profiles",
                newName: "level_assessment_team");

            migrationBuilder.RenameColumn(
                name: "level_assessment_performer",
                table: "musician_profiles",
                newName: "level_assessment_inner");

            migrationBuilder.RenameColumn(
                name: "inquiry_status_staff_id",
                table: "musician_profiles",
                newName: "inquiry_status_team_id");

            migrationBuilder.RenameColumn(
                name: "inquiry_status_performer_id",
                table: "musician_profiles",
                newName: "inquiry_status_inner_id");

            migrationBuilder.RenameColumn(
                name: "background_staff",
                table: "musician_profiles",
                newName: "background_team");

            migrationBuilder.RenameColumn(
                name: "background_performer",
                table: "musician_profiles",
                newName: "background_inner");

            migrationBuilder.RenameIndex(
                name: "ix_musician_profiles_inquiry_status_staff_id",
                table: "musician_profiles",
                newName: "ix_musician_profiles_inquiry_status_team_id");

            migrationBuilder.RenameIndex(
                name: "ix_musician_profiles_inquiry_status_performer_id",
                table: "musician_profiles",
                newName: "ix_musician_profiles_inquiry_status_inner_id");

            migrationBuilder.RenameColumn(
                name: "level_assessment_staff",
                table: "musician_profile_sections",
                newName: "level_assessment_team");

            migrationBuilder.RenameColumn(
                name: "level_assessment_performer",
                table: "musician_profile_sections",
                newName: "level_assessment_inner");

            migrationBuilder.CreateTable(
                name: "musician_profile_positions_inner",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    select_value_section_id = table.Column<Guid>(type: "uuid", nullable: false),
                    musician_profile_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_musician_profile_positions_inner", x => x.id);
                    table.ForeignKey(
                        name: "fk_musician_profile_positions_inner_musician_profiles_musician",
                        column: x => x.musician_profile_id,
                        principalTable: "musician_profiles",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_musician_profile_positions_inner_select_value_sections_sele",
                        column: x => x.select_value_section_id,
                        principalTable: "select_value_sections",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "musician_profile_positions_team",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    select_value_section_id = table.Column<Guid>(type: "uuid", nullable: false),
                    musician_profile_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_musician_profile_positions_team", x => x.id);
                    table.ForeignKey(
                        name: "fk_musician_profile_positions_team_musician_profiles_musician_",
                        column: x => x.musician_profile_id,
                        principalTable: "musician_profiles",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_musician_profile_positions_team_select_value_sections_selec",
                        column: x => x.select_value_section_id,
                        principalTable: "select_value_sections",
                        principalColumn: "id");
                });

            migrationBuilder.UpdateData(
                table: "select_value_categories",
                keyColumn: "id",
                keyValue: new Guid("395ead29-7ecc-4999-b479-dffe97437e3a"),
                column: "property",
                value: "InquiryStatusTeam");

            migrationBuilder.UpdateData(
                table: "select_value_categories",
                keyColumn: "id",
                keyValue: new Guid("d1ca913c-dee7-46d8-9fd4-ea564af8005f"),
                column: "property",
                value: "InquiryStatusInner");

            migrationBuilder.CreateIndex(
                name: "ix_musician_profile_positions_inner_musician_profile_id",
                table: "musician_profile_positions_inner",
                column: "musician_profile_id");

            migrationBuilder.CreateIndex(
                name: "ix_musician_profile_positions_inner_select_value_section_id",
                table: "musician_profile_positions_inner",
                column: "select_value_section_id");

            migrationBuilder.CreateIndex(
                name: "ix_musician_profile_positions_team_musician_profile_id",
                table: "musician_profile_positions_team",
                column: "musician_profile_id");

            migrationBuilder.CreateIndex(
                name: "ix_musician_profile_positions_team_select_value_section_id",
                table: "musician_profile_positions_team",
                column: "select_value_section_id");

            migrationBuilder.AddForeignKey(
                name: "fk_musician_profiles_select_value_mappings_inquiry_status_inne",
                table: "musician_profiles",
                column: "inquiry_status_inner_id",
                principalTable: "select_value_mappings",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_musician_profiles_select_value_mappings_inquiry_status_team",
                table: "musician_profiles",
                column: "inquiry_status_team_id",
                principalTable: "select_value_mappings",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_musician_profiles_select_value_mappings_inquiry_status_inne",
                table: "musician_profiles");

            migrationBuilder.DropForeignKey(
                name: "fk_musician_profiles_select_value_mappings_inquiry_status_team",
                table: "musician_profiles");

            migrationBuilder.DropTable(
                name: "musician_profile_positions_inner");

            migrationBuilder.DropTable(
                name: "musician_profile_positions_team");

            migrationBuilder.RenameColumn(
                name: "profile_preference_team",
                table: "musician_profiles",
                newName: "profile_preference_staff");

            migrationBuilder.RenameColumn(
                name: "profile_preference_inner",
                table: "musician_profiles",
                newName: "profile_preference_performer");

            migrationBuilder.RenameColumn(
                name: "preferred_parts_team",
                table: "musician_profiles",
                newName: "preferred_parts_staff");

            migrationBuilder.RenameColumn(
                name: "preferred_parts_inner",
                table: "musician_profiles",
                newName: "preferred_parts_performer");

            migrationBuilder.RenameColumn(
                name: "level_assessment_team",
                table: "musician_profiles",
                newName: "level_assessment_staff");

            migrationBuilder.RenameColumn(
                name: "level_assessment_inner",
                table: "musician_profiles",
                newName: "level_assessment_performer");

            migrationBuilder.RenameColumn(
                name: "inquiry_status_team_id",
                table: "musician_profiles",
                newName: "inquiry_status_staff_id");

            migrationBuilder.RenameColumn(
                name: "inquiry_status_inner_id",
                table: "musician_profiles",
                newName: "inquiry_status_performer_id");

            migrationBuilder.RenameColumn(
                name: "background_team",
                table: "musician_profiles",
                newName: "background_staff");

            migrationBuilder.RenameColumn(
                name: "background_inner",
                table: "musician_profiles",
                newName: "background_performer");

            migrationBuilder.RenameIndex(
                name: "ix_musician_profiles_inquiry_status_team_id",
                table: "musician_profiles",
                newName: "ix_musician_profiles_inquiry_status_staff_id");

            migrationBuilder.RenameIndex(
                name: "ix_musician_profiles_inquiry_status_inner_id",
                table: "musician_profiles",
                newName: "ix_musician_profiles_inquiry_status_performer_id");

            migrationBuilder.RenameColumn(
                name: "level_assessment_team",
                table: "musician_profile_sections",
                newName: "level_assessment_staff");

            migrationBuilder.RenameColumn(
                name: "level_assessment_inner",
                table: "musician_profile_sections",
                newName: "level_assessment_performer");

            migrationBuilder.CreateTable(
                name: "musician_profile_positions_performer",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    modified_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    musician_profile_id = table.Column<Guid>(type: "uuid", nullable: false),
                    select_value_section_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_musician_profile_positions_performer", x => x.id);
                    table.ForeignKey(
                        name: "fk_musician_profile_positions_performer_musician_profiles_musi",
                        column: x => x.musician_profile_id,
                        principalTable: "musician_profiles",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_musician_profile_positions_performer_select_value_sections_",
                        column: x => x.select_value_section_id,
                        principalTable: "select_value_sections",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "musician_profile_positions_staff",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    modified_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    musician_profile_id = table.Column<Guid>(type: "uuid", nullable: false),
                    select_value_section_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_musician_profile_positions_staff", x => x.id);
                    table.ForeignKey(
                        name: "fk_musician_profile_positions_staff_musician_profiles_musician",
                        column: x => x.musician_profile_id,
                        principalTable: "musician_profiles",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_musician_profile_positions_staff_select_value_sections_sele",
                        column: x => x.select_value_section_id,
                        principalTable: "select_value_sections",
                        principalColumn: "id");
                });

            migrationBuilder.UpdateData(
                table: "select_value_categories",
                keyColumn: "id",
                keyValue: new Guid("395ead29-7ecc-4999-b479-dffe97437e3a"),
                column: "property",
                value: "InquiryStatusStaff");

            migrationBuilder.UpdateData(
                table: "select_value_categories",
                keyColumn: "id",
                keyValue: new Guid("d1ca913c-dee7-46d8-9fd4-ea564af8005f"),
                column: "property",
                value: "InquiryStatusPerformer");

            migrationBuilder.CreateIndex(
                name: "ix_musician_profile_positions_performer_musician_profile_id",
                table: "musician_profile_positions_performer",
                column: "musician_profile_id");

            migrationBuilder.CreateIndex(
                name: "ix_musician_profile_positions_performer_select_value_section_id",
                table: "musician_profile_positions_performer",
                column: "select_value_section_id");

            migrationBuilder.CreateIndex(
                name: "ix_musician_profile_positions_staff_musician_profile_id",
                table: "musician_profile_positions_staff",
                column: "musician_profile_id");

            migrationBuilder.CreateIndex(
                name: "ix_musician_profile_positions_staff_select_value_section_id",
                table: "musician_profile_positions_staff",
                column: "select_value_section_id");

            migrationBuilder.AddForeignKey(
                name: "fk_musician_profiles_select_value_mappings_inquiry_status_perf",
                table: "musician_profiles",
                column: "inquiry_status_performer_id",
                principalTable: "select_value_mappings",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_musician_profiles_select_value_mappings_inquiry_status_staf",
                table: "musician_profiles",
                column: "inquiry_status_staff_id",
                principalTable: "select_value_mappings",
                principalColumn: "id");
        }
    }
}
