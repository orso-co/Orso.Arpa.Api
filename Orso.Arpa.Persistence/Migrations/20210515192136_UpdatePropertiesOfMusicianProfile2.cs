using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Orso.Arpa.Persistence.Migrations
{
    public partial class UpdatePropertiesOfMusicianProfile2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("0d1073cd-f6d5-4572-87ac-98ab6f15c05a"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("1f0e9a86-4641-4d7e-8413-a1beba0e8afb"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("35d63f30-8704-47d5-865a-ee713a082433"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("3c014654-b4c9-4c7a-a251-ae88ad504c8a"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("3f93768e-ac24-4741-9eb8-49d1e8e4a6e1"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("5850e103-4ac9-472e-85f2-cddc08732ccc"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("5db547d6-c115-4409-8db7-59374ca2af83"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("b67d1ac5-80ec-4b7d-bcb8-72e3da55f201"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("d53b4a35-f472-42a1-ab22-c7afb1e7d77e"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("dec26aef-f0de-4c9f-a164-e23e2543c987"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("e20ff004-aafc-4e28-87f9-0d9c6372951c"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("f52b9170-c6f6-4828-b96c-df5dfbe9bd73"));

            migrationBuilder.DropColumn(
                name: "experience_level",
                table: "musician_profiles");

            migrationBuilder.RenameColumn(
                name: "background",
                table: "musician_profiles",
                newName: "background_staff");

            migrationBuilder.AlterColumn<string>(
                name: "salary_comment",
                table: "musician_profiles",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "background_performer",
                table: "musician_profiles",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "instrument_part_id",
                table: "musician_profiles",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_deactivated",
                table: "musician_profiles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "comment",
                table: "musician_profile_sections",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "instrument_availability_id",
                table: "musician_profile_sections",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<byte>(
                name: "level_assessment_performer",
                table: "musician_profile_sections",
                type: "smallint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "level_assessment_staff",
                table: "musician_profile_sections",
                type: "smallint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.CreateTable(
                name: "instrument_part",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    section_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_instrument_part", x => x.id);
                    table.ForeignKey(
                        name: "fk_instrument_part_sections_section_id",
                        column: x => x.section_id,
                        principalTable: "sections",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "preferred_part",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    part_id = table.Column<Guid>(type: "uuid", nullable: true),
                    musician_profile_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_preferred_part", x => x.id);
                    table.ForeignKey(
                        name: "fk_preferred_part_instrument_part_part_id",
                        column: x => x.part_id,
                        principalTable: "instrument_part",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_preferred_part_musician_profiles_musician_profile_id",
                        column: x => x.musician_profile_id,
                        principalTable: "musician_profiles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "select_values",
                columns: new[] { "id", "created_at", "created_by", "deleted", "description", "modified_at", "modified_by", "name" },
                values: new object[,]
                {
                    { new Guid("f036bca9-95d4-4526-b845-fff9208ab103"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Amateur" },
                    { new Guid("6304b935-633d-4bba-a90f-9bd864c867c6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Student" },
                    { new Guid("20f9698c-2f3d-41fd-9f33-1feeababfb1c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Semi-Professional" },
                    { new Guid("30f592f6-485a-468a-bfb2-4854be733e74"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Professional" },
                    { new Guid("42d76464-4b4b-4884-b8e3-1f69baaced4c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Unknown" },
                    { new Guid("58a0d16f-2dac-4836-930e-1dd320430ef4"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Without" },
                    { new Guid("459dc1ea-de92-4a26-9b7b-848d67154cae"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "With - strict" },
                    { new Guid("2fbb99cd-d14a-4b7c-b7fb-9b676f961e2c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "With - negotiable" },
                    { new Guid("68e947c0-9450-4b64-90d7-553850396a3f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Gladly" },
                    { new Guid("60c1a391-59b4-4cea-ba83-59e09f7512b6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Emergency only" },
                    { new Guid("ab5c5904-2683-47c4-a436-319303b8e62f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Never again" },
                    { new Guid("90b5cfa9-890b-4b89-a750-646f3a26db23"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "For contacts only" }
                });

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("03a0cbc1-4546-4b54-b05d-ec37dafeec25"),
                column: "select_value_id",
                value: new Guid("ab5c5904-2683-47c4-a436-319303b8e62f"));

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("0fdbc388-feba-4607-9771-7751009f1fc8"),
                column: "select_value_id",
                value: new Guid("42d76464-4b4b-4884-b8e3-1f69baaced4c"));

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("20f9698c-2f3d-41fd-9f33-1feeababfb1c"),
                column: "select_value_id",
                value: new Guid("20f9698c-2f3d-41fd-9f33-1feeababfb1c"));

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("24c5bbe1-37eb-4368-ac7c-a6061058bbef"),
                column: "select_value_id",
                value: new Guid("42d76464-4b4b-4884-b8e3-1f69baaced4c"));

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("2fbb99cd-d14a-4b7c-b7fb-9b676f961e2c"),
                column: "select_value_id",
                value: new Guid("2fbb99cd-d14a-4b7c-b7fb-9b676f961e2c"));

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("30f592f6-485a-468a-bfb2-4854be733e74"),
                column: "select_value_id",
                value: new Guid("30f592f6-485a-468a-bfb2-4854be733e74"));

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("354ef017-70ca-4c2b-914c-71be7289a0e5"),
                column: "select_value_id",
                value: new Guid("90b5cfa9-890b-4b89-a750-646f3a26db23"));

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("42d76464-4b4b-4884-b8e3-1f69baaced4c"),
                column: "select_value_id",
                value: new Guid("42d76464-4b4b-4884-b8e3-1f69baaced4c"));

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("459dc1ea-de92-4a26-9b7b-848d67154cae"),
                column: "select_value_id",
                value: new Guid("459dc1ea-de92-4a26-9b7b-848d67154cae"));

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("58a0d16f-2dac-4836-930e-1dd320430ef4"),
                column: "select_value_id",
                value: new Guid("58a0d16f-2dac-4836-930e-1dd320430ef4"));

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("60c1a391-59b4-4cea-ba83-59e09f7512b6"),
                column: "select_value_id",
                value: new Guid("60c1a391-59b4-4cea-ba83-59e09f7512b6"));

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("6304b935-633d-4bba-a90f-9bd864c867c6"),
                column: "select_value_id",
                value: new Guid("6304b935-633d-4bba-a90f-9bd864c867c6"));

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("68e947c0-9450-4b64-90d7-553850396a3f"),
                column: "select_value_id",
                value: new Guid("68e947c0-9450-4b64-90d7-553850396a3f"));

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("90b5cfa9-890b-4b89-a750-646f3a26db23"),
                column: "select_value_id",
                value: new Guid("90b5cfa9-890b-4b89-a750-646f3a26db23"));

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("9363bb46-937e-42bf-bb71-5fb16126b501"),
                column: "select_value_id",
                value: new Guid("60c1a391-59b4-4cea-ba83-59e09f7512b6"));

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("98addc5f-f2fa-4640-8441-d4220b7daea2"),
                column: "select_value_id",
                value: new Guid("42d76464-4b4b-4884-b8e3-1f69baaced4c"));

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("a15014ad-582e-4388-9b58-aceb52cf41bf"),
                column: "select_value_id",
                value: new Guid("42d76464-4b4b-4884-b8e3-1f69baaced4c"));

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("ab5c5904-2683-47c4-a436-319303b8e62f"),
                column: "select_value_id",
                value: new Guid("ab5c5904-2683-47c4-a436-319303b8e62f"));

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("cdfb1c47-22dc-4657-aab8-1dbfaf21e862"),
                column: "select_value_id",
                value: new Guid("68e947c0-9450-4b64-90d7-553850396a3f"));

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("d80bf2be-de2f-4d72-ba02-6081b5ba77d2"),
                column: "select_value_id",
                value: new Guid("42d76464-4b4b-4884-b8e3-1f69baaced4c"));

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("f036bca9-95d4-4526-b845-fff9208ab103"),
                column: "select_value_id",
                value: new Guid("f036bca9-95d4-4526-b845-fff9208ab103"));

            migrationBuilder.CreateIndex(
                name: "ix_musician_profiles_instrument_part_id",
                table: "musician_profiles",
                column: "instrument_part_id");

            migrationBuilder.CreateIndex(
                name: "ix_instrument_part_section_id",
                table: "instrument_part",
                column: "section_id");

            migrationBuilder.CreateIndex(
                name: "ix_preferred_part_musician_profile_id",
                table: "preferred_part",
                column: "musician_profile_id");

            migrationBuilder.CreateIndex(
                name: "ix_preferred_part_part_id",
                table: "preferred_part",
                column: "part_id");

            migrationBuilder.AddForeignKey(
                name: "fk_musician_profiles_instrument_part_instrument_part_id",
                table: "musician_profiles",
                column: "instrument_part_id",
                principalTable: "instrument_part",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_musician_profiles_instrument_part_instrument_part_id",
                table: "musician_profiles");

            migrationBuilder.DropTable(
                name: "preferred_part");

            migrationBuilder.DropTable(
                name: "instrument_part");

            migrationBuilder.DropIndex(
                name: "ix_musician_profiles_instrument_part_id",
                table: "musician_profiles");

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("20f9698c-2f3d-41fd-9f33-1feeababfb1c"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("2fbb99cd-d14a-4b7c-b7fb-9b676f961e2c"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("30f592f6-485a-468a-bfb2-4854be733e74"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("42d76464-4b4b-4884-b8e3-1f69baaced4c"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("459dc1ea-de92-4a26-9b7b-848d67154cae"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("58a0d16f-2dac-4836-930e-1dd320430ef4"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("60c1a391-59b4-4cea-ba83-59e09f7512b6"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("6304b935-633d-4bba-a90f-9bd864c867c6"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("68e947c0-9450-4b64-90d7-553850396a3f"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("90b5cfa9-890b-4b89-a750-646f3a26db23"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("ab5c5904-2683-47c4-a436-319303b8e62f"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("f036bca9-95d4-4526-b845-fff9208ab103"));

            migrationBuilder.DropColumn(
                name: "background_performer",
                table: "musician_profiles");

            migrationBuilder.DropColumn(
                name: "instrument_part_id",
                table: "musician_profiles");

            migrationBuilder.DropColumn(
                name: "is_deactivated",
                table: "musician_profiles");

            migrationBuilder.DropColumn(
                name: "comment",
                table: "musician_profile_sections");

            migrationBuilder.DropColumn(
                name: "instrument_availability_id",
                table: "musician_profile_sections");

            migrationBuilder.DropColumn(
                name: "level_assessment_performer",
                table: "musician_profile_sections");

            migrationBuilder.DropColumn(
                name: "level_assessment_staff",
                table: "musician_profile_sections");

            migrationBuilder.RenameColumn(
                name: "background_staff",
                table: "musician_profiles",
                newName: "background");

            migrationBuilder.AlterColumn<string>(
                name: "salary_comment",
                table: "musician_profiles",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "experience_level",
                table: "musician_profiles",
                type: "smallint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.InsertData(
                table: "select_values",
                columns: new[] { "id", "created_at", "created_by", "deleted", "description", "modified_at", "modified_by", "name" },
                values: new object[,]
                {
                    { new Guid("3f93768e-ac24-4741-9eb8-49d1e8e4a6e1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Amateur" },
                    { new Guid("e20ff004-aafc-4e28-87f9-0d9c6372951c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Student" },
                    { new Guid("35d63f30-8704-47d5-865a-ee713a082433"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Semi-Professional" },
                    { new Guid("f52b9170-c6f6-4828-b96c-df5dfbe9bd73"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Professional" },
                    { new Guid("b67d1ac5-80ec-4b7d-bcb8-72e3da55f201"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Unknown" },
                    { new Guid("3c014654-b4c9-4c7a-a251-ae88ad504c8a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Without" },
                    { new Guid("dec26aef-f0de-4c9f-a164-e23e2543c987"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "With - strict" },
                    { new Guid("d53b4a35-f472-42a1-ab22-c7afb1e7d77e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "With - negotiable" },
                    { new Guid("1f0e9a86-4641-4d7e-8413-a1beba0e8afb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Gladly" },
                    { new Guid("5850e103-4ac9-472e-85f2-cddc08732ccc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Emergency only" },
                    { new Guid("5db547d6-c115-4409-8db7-59374ca2af83"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Never again" },
                    { new Guid("0d1073cd-f6d5-4572-87ac-98ab6f15c05a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "For contacts only" }
                });

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("03a0cbc1-4546-4b54-b05d-ec37dafeec25"),
                column: "select_value_id",
                value: new Guid("5db547d6-c115-4409-8db7-59374ca2af83"));

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("0fdbc388-feba-4607-9771-7751009f1fc8"),
                column: "select_value_id",
                value: new Guid("b67d1ac5-80ec-4b7d-bcb8-72e3da55f201"));

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("20f9698c-2f3d-41fd-9f33-1feeababfb1c"),
                column: "select_value_id",
                value: new Guid("35d63f30-8704-47d5-865a-ee713a082433"));

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("24c5bbe1-37eb-4368-ac7c-a6061058bbef"),
                column: "select_value_id",
                value: new Guid("b67d1ac5-80ec-4b7d-bcb8-72e3da55f201"));

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("2fbb99cd-d14a-4b7c-b7fb-9b676f961e2c"),
                column: "select_value_id",
                value: new Guid("d53b4a35-f472-42a1-ab22-c7afb1e7d77e"));

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("30f592f6-485a-468a-bfb2-4854be733e74"),
                column: "select_value_id",
                value: new Guid("f52b9170-c6f6-4828-b96c-df5dfbe9bd73"));

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("354ef017-70ca-4c2b-914c-71be7289a0e5"),
                column: "select_value_id",
                value: new Guid("0d1073cd-f6d5-4572-87ac-98ab6f15c05a"));

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("42d76464-4b4b-4884-b8e3-1f69baaced4c"),
                column: "select_value_id",
                value: new Guid("b67d1ac5-80ec-4b7d-bcb8-72e3da55f201"));

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("459dc1ea-de92-4a26-9b7b-848d67154cae"),
                column: "select_value_id",
                value: new Guid("dec26aef-f0de-4c9f-a164-e23e2543c987"));

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("58a0d16f-2dac-4836-930e-1dd320430ef4"),
                column: "select_value_id",
                value: new Guid("3c014654-b4c9-4c7a-a251-ae88ad504c8a"));

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("60c1a391-59b4-4cea-ba83-59e09f7512b6"),
                column: "select_value_id",
                value: new Guid("5850e103-4ac9-472e-85f2-cddc08732ccc"));

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("6304b935-633d-4bba-a90f-9bd864c867c6"),
                column: "select_value_id",
                value: new Guid("e20ff004-aafc-4e28-87f9-0d9c6372951c"));

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("68e947c0-9450-4b64-90d7-553850396a3f"),
                column: "select_value_id",
                value: new Guid("1f0e9a86-4641-4d7e-8413-a1beba0e8afb"));

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("90b5cfa9-890b-4b89-a750-646f3a26db23"),
                column: "select_value_id",
                value: new Guid("0d1073cd-f6d5-4572-87ac-98ab6f15c05a"));

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("9363bb46-937e-42bf-bb71-5fb16126b501"),
                column: "select_value_id",
                value: new Guid("5850e103-4ac9-472e-85f2-cddc08732ccc"));

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("98addc5f-f2fa-4640-8441-d4220b7daea2"),
                column: "select_value_id",
                value: new Guid("b67d1ac5-80ec-4b7d-bcb8-72e3da55f201"));

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("a15014ad-582e-4388-9b58-aceb52cf41bf"),
                column: "select_value_id",
                value: new Guid("b67d1ac5-80ec-4b7d-bcb8-72e3da55f201"));

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("ab5c5904-2683-47c4-a436-319303b8e62f"),
                column: "select_value_id",
                value: new Guid("5db547d6-c115-4409-8db7-59374ca2af83"));

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("cdfb1c47-22dc-4657-aab8-1dbfaf21e862"),
                column: "select_value_id",
                value: new Guid("1f0e9a86-4641-4d7e-8413-a1beba0e8afb"));

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("d80bf2be-de2f-4d72-ba02-6081b5ba77d2"),
                column: "select_value_id",
                value: new Guid("b67d1ac5-80ec-4b7d-bcb8-72e3da55f201"));

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("f036bca9-95d4-4526-b845-fff9208ab103"),
                column: "select_value_id",
                value: new Guid("3f93768e-ac24-4741-9eb8-49d1e8e4a6e1"));
        }
    }
}
