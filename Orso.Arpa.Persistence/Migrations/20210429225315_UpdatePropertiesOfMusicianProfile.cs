using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Orso.Arpa.Persistence.Migrations
{
    public partial class UpdatePropertiesOfMusicianProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_musician_profiles_positions_preferred_position_id",
                table: "musician_profiles");

            migrationBuilder.DropForeignKey(
                name: "fk_musician_profiles_select_value_mappings_inquery_id",
                table: "musician_profiles");

            migrationBuilder.DropTable(
                name: "musician_profile_credentials");

            migrationBuilder.DropTable(
                name: "sphere_of_activity_concerts");

            migrationBuilder.DropTable(
                name: "sphere_of_activity_rehearsals");

            migrationBuilder.DropTable(
                name: "credentials");

            migrationBuilder.RenameColumn(
                name: "profile_favorizitation",
                table: "musician_profiles",
                newName: "profile_preference_staff");

            migrationBuilder.RenameColumn(
                name: "preferred_position_id",
                table: "musician_profiles",
                newName: "position_id");

            migrationBuilder.RenameColumn(
                name: "level_self_assessment",
                table: "musician_profiles",
                newName: "profile_preference_performer");

            migrationBuilder.RenameColumn(
                name: "level_inner_assessment",
                table: "musician_profiles",
                newName: "level_assessment_staff");

            migrationBuilder.RenameColumn(
                name: "inquery_id",
                table: "musician_profiles",
                newName: "inquiry_status_staff_id");

            migrationBuilder.RenameIndex(
                name: "ix_musician_profiles_preferred_position_id",
                table: "musician_profiles",
                newName: "ix_musician_profiles_position_id");

            migrationBuilder.RenameIndex(
                name: "ix_musician_profiles_inquery_id",
                table: "musician_profiles",
                newName: "ix_musician_profiles_inquiry_status_staff_id");

            migrationBuilder.AddColumn<Guid>(
                name: "inquiry_status_performer_id",
                table: "musician_profiles",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "level_assessment_performer",
                table: "musician_profiles",
                type: "smallint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<string>(
                name: "salary_comment",
                table: "musician_profiles",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "musician_profile_id",
                table: "auditions",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "curriculum_vitae_reference",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    timespan = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    keyword = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    details = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    sort_order = table.Column<byte>(type: "smallint", nullable: false),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_curriculum_vitae_reference", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "preferred_position",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    position_id = table.Column<Guid>(type: "uuid", nullable: true),
                    musician_profile_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_preferred_position", x => x.id);
                    table.ForeignKey(
                        name: "fk_preferred_position_musician_profiles_musician_profile_id",
                        column: x => x.musician_profile_id,
                        principalTable: "musician_profiles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_preferred_position_positions_position_id",
                        column: x => x.position_id,
                        principalTable: "positions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "region_preference_performances",
                columns: table => new
                {
                    venue_id = table.Column<Guid>(type: "uuid", nullable: false),
                    musician_profile_id = table.Column<Guid>(type: "uuid", nullable: false),
                    rating = table.Column<byte>(type: "smallint", nullable: false),
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_region_preference_performances", x => new { x.musician_profile_id, x.venue_id });
                    table.ForeignKey(
                        name: "fk_region_preference_performances_musician_profiles_musician_p",
                        column: x => x.musician_profile_id,
                        principalTable: "musician_profiles",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_region_preference_performances_venues_venue_id",
                        column: x => x.venue_id,
                        principalTable: "venues",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "region_preference_rehearsals",
                columns: table => new
                {
                    venue_id = table.Column<Guid>(type: "uuid", nullable: false),
                    musician_profile_id = table.Column<Guid>(type: "uuid", nullable: false),
                    rating = table.Column<byte>(type: "smallint", nullable: false),
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_region_preference_rehearsals", x => new { x.musician_profile_id, x.venue_id });
                    table.ForeignKey(
                        name: "fk_region_preference_rehearsals_musician_profiles_musician_pro",
                        column: x => x.musician_profile_id,
                        principalTable: "musician_profiles",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_region_preference_rehearsals_venues_venue_id",
                        column: x => x.venue_id,
                        principalTable: "venues",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "musician_profile_curriculum_vitae_references",
                columns: table => new
                {
                    curriculum_vitae_reference_id = table.Column<Guid>(type: "uuid", nullable: false),
                    musician_profile_id = table.Column<Guid>(type: "uuid", nullable: false),
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_musician_profile_curriculum_vitae_references", x => new { x.musician_profile_id, x.curriculum_vitae_reference_id });
                    table.ForeignKey(
                        name: "fk_musician_profile_curriculum_vitae_references_curriculum_vit",
                        column: x => x.curriculum_vitae_reference_id,
                        principalTable: "curriculum_vitae_reference",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_musician_profile_curriculum_vitae_references_musician_profi",
                        column: x => x.musician_profile_id,
                        principalTable: "musician_profiles",
                        principalColumn: "id");
                });

            migrationBuilder.UpdateData(
                table: "select_value_categories",
                keyColumn: "id",
                keyValue: new Guid("c4ff62bb-9f40-4499-b237-d7b87b2b36f7"),
                column: "property",
                value: "AvailableDocuments");

            migrationBuilder.UpdateData(
                table: "select_value_categories",
                keyColumn: "id",
                keyValue: new Guid("d1ca913c-dee7-46d8-9fd4-ea564af8005f"),
                columns: new[] { "name", "property" },
                values: new object[] { "Inquiry status performer", "InquiryStatusPerformer" });

            migrationBuilder.InsertData(
                table: "select_value_categories",
                columns: new[] { "id", "created_at", "created_by", "deleted", "modified_at", "modified_by", "name", "property", "table" },
                values: new object[] { new Guid("395ead29-7ecc-4999-b479-dffe97437e3a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Inquiry status staff", "InquiryStatusStaff", "MusicianProfile" });

            migrationBuilder.InsertData(
                table: "select_value_mappings",
                columns: new[] { "id", "created_at", "created_by", "deleted", "modified_at", "modified_by", "select_value_category_id", "select_value_id" },
                values: new object[,]
                {
                    { new Guid("cdfb1c47-22dc-4657-aab8-1dbfaf21e862"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("395ead29-7ecc-4999-b479-dffe97437e3a"), new Guid("1f0e9a86-4641-4d7e-8413-a1beba0e8afb") },
                    { new Guid("9363bb46-937e-42bf-bb71-5fb16126b501"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("395ead29-7ecc-4999-b479-dffe97437e3a"), new Guid("5850e103-4ac9-472e-85f2-cddc08732ccc") },
                    { new Guid("03a0cbc1-4546-4b54-b05d-ec37dafeec25"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("395ead29-7ecc-4999-b479-dffe97437e3a"), new Guid("5db547d6-c115-4409-8db7-59374ca2af83") },
                    { new Guid("0fdbc388-feba-4607-9771-7751009f1fc8"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("395ead29-7ecc-4999-b479-dffe97437e3a"), new Guid("b67d1ac5-80ec-4b7d-bcb8-72e3da55f201") },
                    { new Guid("354ef017-70ca-4c2b-914c-71be7289a0e5"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("395ead29-7ecc-4999-b479-dffe97437e3a"), new Guid("0d1073cd-f6d5-4572-87ac-98ab6f15c05a") }
                });

            migrationBuilder.CreateIndex(
                name: "ix_musician_profiles_inquiry_status_performer_id",
                table: "musician_profiles",
                column: "inquiry_status_performer_id");

            migrationBuilder.CreateIndex(
                name: "ix_auditions_musician_profile_id",
                table: "auditions",
                column: "musician_profile_id");

            migrationBuilder.CreateIndex(
                name: "ix_musician_profile_curriculum_vitae_references_curriculum_vit",
                table: "musician_profile_curriculum_vitae_references",
                column: "curriculum_vitae_reference_id");

            migrationBuilder.CreateIndex(
                name: "ix_preferred_position_musician_profile_id",
                table: "preferred_position",
                column: "musician_profile_id");

            migrationBuilder.CreateIndex(
                name: "ix_preferred_position_position_id",
                table: "preferred_position",
                column: "position_id");

            migrationBuilder.CreateIndex(
                name: "ix_region_preference_performances_venue_id",
                table: "region_preference_performances",
                column: "venue_id");

            migrationBuilder.CreateIndex(
                name: "ix_region_preference_rehearsals_venue_id",
                table: "region_preference_rehearsals",
                column: "venue_id");

            migrationBuilder.AddForeignKey(
                name: "fk_auditions_musician_profiles_musician_profile_id",
                table: "auditions",
                column: "musician_profile_id",
                principalTable: "musician_profiles",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_musician_profiles_positions_position_id",
                table: "musician_profiles",
                column: "position_id",
                principalTable: "positions",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_auditions_musician_profiles_musician_profile_id",
                table: "auditions");

            migrationBuilder.DropForeignKey(
                name: "fk_musician_profiles_positions_position_id",
                table: "musician_profiles");

            migrationBuilder.DropForeignKey(
                name: "fk_musician_profiles_select_value_mappings_inquiry_status_perf",
                table: "musician_profiles");

            migrationBuilder.DropForeignKey(
                name: "fk_musician_profiles_select_value_mappings_inquiry_status_staf",
                table: "musician_profiles");

            migrationBuilder.DropTable(
                name: "musician_profile_curriculum_vitae_references");

            migrationBuilder.DropTable(
                name: "preferred_position");

            migrationBuilder.DropTable(
                name: "region_preference_performances");

            migrationBuilder.DropTable(
                name: "region_preference_rehearsals");

            migrationBuilder.DropTable(
                name: "curriculum_vitae_reference");

            migrationBuilder.DropIndex(
                name: "ix_musician_profiles_inquiry_status_performer_id",
                table: "musician_profiles");

            migrationBuilder.DropIndex(
                name: "ix_auditions_musician_profile_id",
                table: "auditions");

            migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("03a0cbc1-4546-4b54-b05d-ec37dafeec25"));

            migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("0fdbc388-feba-4607-9771-7751009f1fc8"));

            migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("354ef017-70ca-4c2b-914c-71be7289a0e5"));

            migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("9363bb46-937e-42bf-bb71-5fb16126b501"));

            migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("cdfb1c47-22dc-4657-aab8-1dbfaf21e862"));

            migrationBuilder.DeleteData(
                table: "select_value_categories",
                keyColumn: "id",
                keyValue: new Guid("395ead29-7ecc-4999-b479-dffe97437e3a"));

            migrationBuilder.DropColumn(
                name: "inquiry_status_performer_id",
                table: "musician_profiles");

            migrationBuilder.DropColumn(
                name: "level_assessment_performer",
                table: "musician_profiles");

            migrationBuilder.DropColumn(
                name: "salary_comment",
                table: "musician_profiles");

            migrationBuilder.DropColumn(
                name: "musician_profile_id",
                table: "auditions");

            migrationBuilder.RenameColumn(
                name: "profile_preference_staff",
                table: "musician_profiles",
                newName: "profile_favorizitation");

            migrationBuilder.RenameColumn(
                name: "profile_preference_performer",
                table: "musician_profiles",
                newName: "level_self_assessment");

            migrationBuilder.RenameColumn(
                name: "position_id",
                table: "musician_profiles",
                newName: "preferred_position_id");

            migrationBuilder.RenameColumn(
                name: "level_assessment_staff",
                table: "musician_profiles",
                newName: "level_inner_assessment");

            migrationBuilder.RenameColumn(
                name: "inquiry_status_staff_id",
                table: "musician_profiles",
                newName: "inquery_id");

            migrationBuilder.RenameIndex(
                name: "ix_musician_profiles_position_id",
                table: "musician_profiles",
                newName: "ix_musician_profiles_preferred_position_id");

            migrationBuilder.RenameIndex(
                name: "ix_musician_profiles_inquiry_status_staff_id",
                table: "musician_profiles",
                newName: "ix_musician_profiles_inquery_id");

            migrationBuilder.CreateTable(
                name: "credentials",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false),
                    details = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    keyword = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    modified_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    sort_order = table.Column<byte>(type: "smallint", nullable: false),
                    timespan = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_credentials", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "sphere_of_activity_concerts",
                columns: table => new
                {
                    musician_profile_id = table.Column<Guid>(type: "uuid", nullable: false),
                    venue_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false),
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    modified_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    rating = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sphere_of_activity_concerts", x => new { x.musician_profile_id, x.venue_id });
                    table.ForeignKey(
                        name: "fk_sphere_of_activity_concerts_musician_profiles_musician_prof",
                        column: x => x.musician_profile_id,
                        principalTable: "musician_profiles",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_sphere_of_activity_concerts_venues_venue_id",
                        column: x => x.venue_id,
                        principalTable: "venues",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "sphere_of_activity_rehearsals",
                columns: table => new
                {
                    musician_profile_id = table.Column<Guid>(type: "uuid", nullable: false),
                    venue_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false),
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    modified_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    rating = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sphere_of_activity_rehearsals", x => new { x.musician_profile_id, x.venue_id });
                    table.ForeignKey(
                        name: "fk_sphere_of_activity_rehearsals_musician_profiles_musician_pr",
                        column: x => x.musician_profile_id,
                        principalTable: "musician_profiles",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_sphere_of_activity_rehearsals_venues_venue_id",
                        column: x => x.venue_id,
                        principalTable: "venues",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "musician_profile_credentials",
                columns: table => new
                {
                    musician_profile_id = table.Column<Guid>(type: "uuid", nullable: false),
                    credential_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false),
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    modified_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_musician_profile_credentials", x => new { x.musician_profile_id, x.credential_id });
                    table.ForeignKey(
                        name: "fk_musician_profile_credentials_credentials_credential_id",
                        column: x => x.credential_id,
                        principalTable: "credentials",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_musician_profile_credentials_musician_profiles_musician_pro",
                        column: x => x.musician_profile_id,
                        principalTable: "musician_profiles",
                        principalColumn: "id");
                });

            migrationBuilder.UpdateData(
                table: "select_value_categories",
                keyColumn: "id",
                keyValue: new Guid("c4ff62bb-9f40-4499-b237-d7b87b2b36f7"),
                column: "property",
                value: "AvailableDocumentStatus");

            migrationBuilder.UpdateData(
                table: "select_value_categories",
                keyColumn: "id",
                keyValue: new Guid("d1ca913c-dee7-46d8-9fd4-ea564af8005f"),
                columns: new[] { "name", "property" },
                values: new object[] { "Inquery", "Inquery" });

            migrationBuilder.CreateIndex(
                name: "ix_musician_profile_credentials_credential_id",
                table: "musician_profile_credentials",
                column: "credential_id");

            migrationBuilder.CreateIndex(
                name: "ix_sphere_of_activity_concerts_venue_id",
                table: "sphere_of_activity_concerts",
                column: "venue_id");

            migrationBuilder.CreateIndex(
                name: "ix_sphere_of_activity_rehearsals_venue_id",
                table: "sphere_of_activity_rehearsals",
                column: "venue_id");

            migrationBuilder.AddForeignKey(
                name: "fk_musician_profiles_positions_preferred_position_id",
                table: "musician_profiles",
                column: "preferred_position_id",
                principalTable: "positions",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_musician_profiles_select_value_mappings_inquery_id",
                table: "musician_profiles",
                column: "inquery_id",
                principalTable: "select_value_mappings",
                principalColumn: "id");
        }
    }
}
