using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Orso.Arpa.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class StatusAsEnums : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder.DropForeignKey(
                name: "fk_appointment_participations_select_value_mappings_prediction",
                table: "appointment_participations");

            _ = migrationBuilder.DropForeignKey(
                name: "fk_appointment_participations_select_value_mappings_result_id",
                table: "appointment_participations");

            _ = migrationBuilder.DropForeignKey(
                name: "fk_appointments_select_value_mappings_status_id",
                table: "appointments");

            _ = migrationBuilder.DropForeignKey(
                name: "fk_musician_profiles_select_value_mappings_inquiry_status_inne",
                table: "musician_profiles");

            _ = migrationBuilder.DropForeignKey(
                name: "fk_musician_profiles_select_value_mappings_inquiry_status_team",
                table: "musician_profiles");

            _ = migrationBuilder.DropForeignKey(
                name: "fk_project_participations_select_value_mappings_invitation_sta",
                table: "project_participations");

            _ = migrationBuilder.DropForeignKey(
                name: "fk_project_participations_select_value_mappings_participation_",
                table: "project_participations");

            _ = migrationBuilder.DropForeignKey(
                name: "fk_project_participations_select_value_mappings_participation_1",
                table: "project_participations");

            _ = migrationBuilder.DropForeignKey(
                name: "fk_projects_select_value_mappings_state_id",
                table: "projects");

            _ = migrationBuilder.DropIndex(
                name: "ix_projects_state_id",
                table: "projects");

            _ = migrationBuilder.DropIndex(
                name: "ix_project_participations_invitation_status_id",
                table: "project_participations");

            _ = migrationBuilder.DropIndex(
                name: "ix_project_participations_participation_status_inner_id",
                table: "project_participations");

            _ = migrationBuilder.DropIndex(
                name: "ix_project_participations_participation_status_internal_id",
                table: "project_participations");

            _ = migrationBuilder.DropIndex(
                name: "ix_musician_profiles_inquiry_status_inner_id",
                table: "musician_profiles");

            _ = migrationBuilder.DropIndex(
                name: "ix_musician_profiles_inquiry_status_team_id",
                table: "musician_profiles");

            _ = migrationBuilder.DropIndex(
                name: "ix_appointments_status_id",
                table: "appointments");

            _ = migrationBuilder.DropIndex(
                name: "ix_appointment_participations_prediction_id",
                table: "appointment_participations");

            _ = migrationBuilder.DropIndex(
                name: "ix_appointment_participations_result_id",
                table: "appointment_participations");

            _ = migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("0096f414-50c9-4d45-9a85-4af30641b7fa"));

            _ = migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("0126fded-0a82-4b53-85e4-1c04a4f79296"));

            _ = migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("03a0cbc1-4546-4b54-b05d-ec37dafeec25"));

            _ = migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("03bdcf0a-2638-4b8f-a093-4084b9969162"));

            _ = migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("0fdbc388-feba-4607-9771-7751009f1fc8"));

            _ = migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("17d201fc-777b-43bb-9c46-0d07737a8ab7"));

            _ = migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("1d402f12-816d-4994-a94d-28d52cb2d199"));

            _ = migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("2a5f85e6-a7ed-48eb-852c-0b191d7ba949"));

            _ = migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("2ad77626-e0b3-45a6-9d24-e4677181ee7e"));

            _ = migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("319d508e-a6e2-437e-b48b-6be51e3459bd"));

            _ = migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("354ef017-70ca-4c2b-914c-71be7289a0e5"));

            _ = migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("36176b7e-0926-43d6-b19a-72838ccd2acd"));

            _ = migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("3801aa69-cc4e-4fd5-947c-bfdd4e95d48e"));

            _ = migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("4dc9db05-357a-43a6-ba20-f2a9e5033802"));

            _ = migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("50e6049b-a9cd-400b-a475-e2563147aebc"));

            _ = migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("60c1a391-59b4-4cea-ba83-59e09f7512b6"));

            _ = migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("625a9195-2380-4762-8dc6-13163e354ef6"));

            _ = migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("65975857-ab27-480d-87c3-dba74d45cb63"));

            _ = migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("68e947c0-9450-4b64-90d7-553850396a3f"));

            _ = migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("725a4f4a-37cb-46ba-93a3-7b9cc2b015cb"));

            _ = migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("75f2d1c3-4ba2-4acc-8fd3-6b01ca66d1c9"));

            _ = migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("7fb30d45-1faf-4f6a-ac5d-436204ad8e69"));

            _ = migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("8168cfbf-7e53-41c5-8bc4-f5392d9a3b57"));

            _ = migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("8b7d7f26-b7e5-42e2-afc1-cedddbae841a"));

            _ = migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("90b5cfa9-890b-4b89-a750-646f3a26db23"));

            _ = migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("93033f7e-a3c1-45e3-8a17-021d0a4abe5a"));

            _ = migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("9363bb46-937e-42bf-bb71-5fb16126b501"));

            _ = migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("a15014ad-582e-4388-9b58-aceb52cf41bf"));

            _ = migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("ab5c5904-2683-47c4-a436-319303b8e62f"));

            _ = migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("ade78d45-b010-4ed7-87f0-e30e0558f151"));

            _ = migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("b0dcb5e9-bbc6-4004-b9d7-0f6723416b9b"));

            _ = migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("b6cf76a5-ec3f-4e81-9499-174d33bb7249"));

            _ = migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("b793fa86-2025-4258-8993-8045f4c312d7"));

            _ = migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("bc29bf0a-2ebb-4db8-8765-a5f835492552"));

            _ = migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("c6b0b06f-a915-4087-9827-34e76ab6895f"));

            _ = migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("c9225a82-0348-41bb-a591-7726f8079cde"));

            _ = migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("cdfb1c47-22dc-4657-aab8-1dbfaf21e862"));

            _ = migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("e0abe26f-27da-4396-b80c-d1ceb836a8b2"));

            _ = migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("eef4a4d1-796b-4b37-96f6-f31dbccf0aeb"));

            _ = migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("f1c2c792-f11f-43ab-8cf6-d6ff905894fc"));

            _ = migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("ff994b2c-a3bd-4676-a974-f53d4fa562ba"));

            _ = migrationBuilder.AddColumn<string>(
                name: "status",
                table: "projects",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            _ = migrationBuilder.Sql(
                    "UPDATE projects SET status = " +
                    "(CASE state_id " +
                        "WHEN '725a4f4a-37cb-46ba-93a3-7b9cc2b015cb' then 'Pending' " +
                        "WHEN 'b793fa86-2025-4258-8993-8045f4c312d7' then 'Confirmed' " +
                        "WHEN '65975857-ab27-480d-87c3-dba74d45cb63' then 'Cancelled' " +
                        "WHEN 'bc29bf0a-2ebb-4db8-8765-a5f835492552' then 'Postponed' " +
                        "WHEN '75f2d1c3-4ba2-4acc-8fd3-6b01ca66d1c9' then 'Archived' " +
                    "ELSE null END);");

            _ = migrationBuilder.AddColumn<string>(
                name: "invitation_status",
                table: "project_participations",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            _ = migrationBuilder.AddColumn<string>(
                name: "participation_status_inner",
                table: "project_participations",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            _ = migrationBuilder.AddColumn<string>(
                name: "participation_status_internal",
                table: "project_participations",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            _ = migrationBuilder.Sql(
                    "UPDATE project_participations SET invitation_status = " +
                    "(CASE invitation_status_id " +
                    "WHEN '625a9195-2380-4762-8dc6-13163e354ef6' then 'Invited' " +
                    "WHEN '2ad77626-e0b3-45a6-9d24-e4677181ee7e' then 'NotInvited' " +
                    "WHEN '2a5f85e6-a7ed-48eb-852c-0b191d7ba949' then 'Candidate' " +
                    "WHEN 'c6b0b06f-a915-4087-9827-34e76ab6895f' then 'Unclear' " +
                    "ELSE null END), " +
                    "participation_status_inner = " +
                    "(CASE participation_status_inner_id " +
                    "WHEN 'e0abe26f-27da-4396-b80c-d1ceb836a8b2' then 'Interested' " +
                    "WHEN 'eef4a4d1-796b-4b37-96f6-f31dbccf0aeb' then 'Acceptance' " +
                    "WHEN '1d402f12-816d-4994-a94d-28d52cb2d199' then 'Refusal' " +
                    "WHEN '8168cfbf-7e53-41c5-8bc4-f5392d9a3b57' then 'Pending' " +
                    "ELSE null END) ," +
                    "participation_status_internal = " +
                    "(CASE participation_status_internal_id " +
                    "WHEN 'b0dcb5e9-bbc6-4004-b9d7-0f6723416b9b' then 'Candidate' " +
                    "WHEN 'f1c2c792-f11f-43ab-8cf6-d6ff905894fc' then 'Acceptance' " +
                    "WHEN '0096f414-50c9-4d45-9a85-4af30641b7fa' then 'Refusal' " +
                    "WHEN '03bdcf0a-2638-4b8f-a093-4084b9969162' then 'Pending' " +
                    "ELSE null END)" +
                    ";");

            _ = migrationBuilder.AddColumn<string>(
                name: "inquiry_status_inner",
                table: "musician_profiles",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            _ = migrationBuilder.AddColumn<string>(
                name: "inquiry_status_team",
                table: "musician_profiles",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            _ = migrationBuilder.Sql(
                    "UPDATE musician_profiles SET inquiry_status_inner = " +
                    "(CASE inquiry_status_inner_id " +
                    "WHEN '68e947c0-9450-4b64-90d7-553850396a3f' then 'Gladly' " +
                    "WHEN '60c1a391-59b4-4cea-ba83-59e09f7512b6' then 'EmergencyOnly' " +
                    "WHEN 'ab5c5904-2683-47c4-a436-319303b8e62f' then 'NeverAgain' " +
                    "WHEN 'a15014ad-582e-4388-9b58-aceb52cf41bf' then 'Unknown' " +
                    "WHEN '90b5cfa9-890b-4b89-a750-646f3a26db23' then 'ForContactsOnly' " +
                    "ELSE null END), " +
                    "inquiry_status_team = " +
                    "(CASE inquiry_status_team_id " +
                    "WHEN 'cdfb1c47-22dc-4657-aab8-1dbfaf21e862' then 'Gladly' " +
                    "WHEN '9363bb46-937e-42bf-bb71-5fb16126b501' then 'EmergencyOnly' " +
                    "WHEN '03a0cbc1-4546-4b54-b05d-ec37dafeec25' then 'NeverAgain' " +
                    "WHEN '0fdbc388-feba-4607-9771-7751009f1fc8' then 'Unknown' " +
                    "WHEN '354ef017-70ca-4c2b-914c-71be7289a0e5' then 'ForContactsOnly' " +
                    "ELSE null END)" +
                    ";");

            _ = migrationBuilder.AddColumn<string>(
                name: "status",
                table: "appointments",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            _ = migrationBuilder.Sql(
                    "UPDATE appointments SET status = " +
                    "(CASE status_id " +
                    "WHEN '36176b7e-0926-43d6-b19a-72838ccd2acd' then 'Confirmed' " +
                    "WHEN '93033f7e-a3c1-45e3-8a17-021d0a4abe5a' then 'Scheduled' " +
                    "WHEN '0126fded-0a82-4b53-85e4-1c04a4f79296' then 'Refused' " +
                    "WHEN 'b6cf76a5-ec3f-4e81-9499-174d33bb7249' then 'Ambiguous' " +
                    "WHEN '4dc9db05-357a-43a6-ba20-f2a9e5033802' then 'AwaitingPoll' " +
                    "ELSE null END);");

            _ = migrationBuilder.AddColumn<string>(
                name: "prediction",
                table: "appointment_participations",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            _ = migrationBuilder.AddColumn<string>(
                name: "result",
                table: "appointment_participations",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            _ = migrationBuilder.Sql(
                    "UPDATE appointment_participations SET prediction = " +
                    "(CASE prediction_id " +
                    "WHEN '319d508e-a6e2-437e-b48b-6be51e3459bd' then 'Yes' " +
                    "WHEN 'c9225a82-0348-41bb-a591-7726f8079cde' then 'Partly' " +
                    "WHEN '17d201fc-777b-43bb-9c46-0d07737a8ab7' then 'No' " +
                    "WHEN '50e6049b-a9cd-400b-a475-e2563147aebc' then 'DontKnowYet' " +
                    "ELSE null END), " +
                    "result = " +
                    "(CASE result_id " +
                    "WHEN '3801aa69-cc4e-4fd5-947c-bfdd4e95d48e' then 'Present' " +
                    "WHEN 'ade78d45-b010-4ed7-87f0-e30e0558f151' then 'Absent' " +
                    "WHEN 'ff994b2c-a3bd-4676-a974-f53d4fa562ba' then 'Inapplicable' " +
                    "WHEN '8b7d7f26-b7e5-42e2-afc1-cedddbae841a' then 'Ambiguous' " +
                    "WHEN '7fb30d45-1faf-4f6a-ac5d-436204ad8e69' then 'AwaitingScan' " +
                    "ELSE null END)" +
                    ";");

            _ = migrationBuilder.UpdateData(
                table: "select_value_categories",
                keyColumn: "id",
                keyValue: new Guid("9804d695-d8c7-40bd-814f-8458b55fb583"),
                column: "property",
                value: "Status");

            _ = migrationBuilder.CreateIndex(
                name: "ix_projects_status",
                table: "projects",
                column: "status");

            _ = migrationBuilder.CreateIndex(
                name: "ix_project_participations_invitation_status",
                table: "project_participations",
                column: "invitation_status");

            _ = migrationBuilder.CreateIndex(
                name: "ix_project_participations_participation_status_inner",
                table: "project_participations",
                column: "participation_status_inner");

            _ = migrationBuilder.CreateIndex(
                name: "ix_project_participations_participation_status_internal",
                table: "project_participations",
                column: "participation_status_internal");

            _ = migrationBuilder.CreateIndex(
                name: "ix_musician_profiles_inquiry_status_inner",
                table: "musician_profiles",
                column: "inquiry_status_inner");

            _ = migrationBuilder.CreateIndex(
                name: "ix_musician_profiles_inquiry_status_team",
                table: "musician_profiles",
                column: "inquiry_status_team");

            _ = migrationBuilder.CreateIndex(
                name: "ix_appointments_status",
                table: "appointments",
                column: "status");

            _ = migrationBuilder.CreateIndex(
                name: "ix_appointment_participations_prediction",
                table: "appointment_participations",
                column: "prediction");

            _ = migrationBuilder.CreateIndex(
                name: "ix_appointment_participations_result",
                table: "appointment_participations",
                column: "result");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder.DropIndex(
                name: "ix_projects_status",
                table: "projects");

            _ = migrationBuilder.DropIndex(
                name: "ix_project_participations_invitation_status",
                table: "project_participations");

            _ = migrationBuilder.DropIndex(
                name: "ix_project_participations_participation_status_inner",
                table: "project_participations");

            _ = migrationBuilder.DropIndex(
                name: "ix_project_participations_participation_status_internal",
                table: "project_participations");

            _ = migrationBuilder.DropIndex(
                name: "ix_musician_profiles_inquiry_status_inner",
                table: "musician_profiles");

            _ = migrationBuilder.DropIndex(
                name: "ix_musician_profiles_inquiry_status_team",
                table: "musician_profiles");

            _ = migrationBuilder.DropIndex(
                name: "ix_appointments_status",
                table: "appointments");

            _ = migrationBuilder.DropIndex(
                name: "ix_appointment_participations_prediction",
                table: "appointment_participations");

            _ = migrationBuilder.DropIndex(
                name: "ix_appointment_participations_result",
                table: "appointment_participations");

            _ = migrationBuilder.DropColumn(
                name: "status",
                table: "projects");

            _ = migrationBuilder.DropColumn(
                name: "invitation_status",
                table: "project_participations");

            _ = migrationBuilder.DropColumn(
                name: "participation_status_inner",
                table: "project_participations");

            _ = migrationBuilder.DropColumn(
                name: "participation_status_internal",
                table: "project_participations");

            _ = migrationBuilder.DropColumn(
                name: "inquiry_status_inner",
                table: "musician_profiles");

            _ = migrationBuilder.DropColumn(
                name: "inquiry_status_team",
                table: "musician_profiles");

            _ = migrationBuilder.DropColumn(
                name: "status",
                table: "appointments");

            _ = migrationBuilder.DropColumn(
                name: "prediction",
                table: "appointment_participations");

            _ = migrationBuilder.DropColumn(
                name: "result",
                table: "appointment_participations");

            _ = migrationBuilder.UpdateData(
                table: "select_value_categories",
                keyColumn: "id",
                keyValue: new Guid("9804d695-d8c7-40bd-814f-8458b55fb583"),
                column: "property",
                value: "State");

            _ = migrationBuilder.InsertData(
                table: "select_value_mappings",
                columns: new[] { "id", "created_at", "created_by", "deleted", "modified_at", "modified_by", "select_value_category_id", "select_value_id", "sort_order" },
                values: new object[,]
                {
                    { new Guid("0096f414-50c9-4d45-9a85-4af30641b7fa"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("13376e1d-2378-4e30-a6d2-808da4a4ba4d"), new Guid("78d6ce19-ac32-444f-94a6-aa4262340fa1"), 40 },
                    { new Guid("0126fded-0a82-4b53-85e4-1c04a4f79296"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("09be8eff-72e4-40a8-a1ed-717deb185a69"), new Guid("99d192e1-332a-494e-b821-075be14211be"), 50 },
                    { new Guid("03a0cbc1-4546-4b54-b05d-ec37dafeec25"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("395ead29-7ecc-4999-b479-dffe97437e3a"), new Guid("5db547d6-c115-4409-8db7-59374ca2af83"), 40 },
                    { new Guid("03bdcf0a-2638-4b8f-a093-4084b9969162"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("13376e1d-2378-4e30-a6d2-808da4a4ba4d"), new Guid("362efd25-e1d2-496d-b7fa-884371a58682"), 20 },
                    { new Guid("0fdbc388-feba-4607-9771-7751009f1fc8"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("395ead29-7ecc-4999-b479-dffe97437e3a"), new Guid("b67d1ac5-80ec-4b7d-bcb8-72e3da55f201"), 90 },
                    { new Guid("17d201fc-777b-43bb-9c46-0d07737a8ab7"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("5cf52155-927f-4d64-a482-348f952bab21"), new Guid("88b763ac-8093-4c5d-a881-85be1fb8a24d"), 20 },
                    { new Guid("1d402f12-816d-4994-a94d-28d52cb2d199"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("1bae5715-8363-4221-8735-8def3d2546e1"), new Guid("78d6ce19-ac32-444f-94a6-aa4262340fa1"), 40 },
                    { new Guid("2a5f85e6-a7ed-48eb-852c-0b191d7ba949"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("474775e9-f08a-4043-8474-e84f42bf3948"), new Guid("b3bd7011-2cda-49d9-8fea-46fa02db9c4b"), 10 },
                    { new Guid("2ad77626-e0b3-45a6-9d24-e4677181ee7e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("474775e9-f08a-4043-8474-e84f42bf3948"), new Guid("77c68dbb-a627-4053-829e-86c555754f60"), 30 },
                    { new Guid("319d508e-a6e2-437e-b48b-6be51e3459bd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("5cf52155-927f-4d64-a482-348f952bab21"), new Guid("75a017d3-dca5-49ec-9bbd-3b01b159ba5b"), 10 },
                    { new Guid("354ef017-70ca-4c2b-914c-71be7289a0e5"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("395ead29-7ecc-4999-b479-dffe97437e3a"), new Guid("0d1073cd-f6d5-4572-87ac-98ab6f15c05a"), 30 },
                    { new Guid("36176b7e-0926-43d6-b19a-72838ccd2acd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("09be8eff-72e4-40a8-a1ed-717deb185a69"), new Guid("34a52363-4a57-4019-abcf-0c9880246891"), 10 },
                    { new Guid("3801aa69-cc4e-4fd5-947c-bfdd4e95d48e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("f5d4763e-5862-4b62-ab92-2748ad213b10"), new Guid("313445ca-57fa-45f0-8515-325949d60726"), 10 },
                    { new Guid("4dc9db05-357a-43a6-ba20-f2a9e5033802"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("09be8eff-72e4-40a8-a1ed-717deb185a69"), new Guid("5e3edcf4-863b-433b-ae72-b6bb7e4dfc95"), 40 },
                    { new Guid("50e6049b-a9cd-400b-a475-e2563147aebc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("5cf52155-927f-4d64-a482-348f952bab21"), new Guid("4ee7d317-6d71-4d6e-b45a-954c8c7dcf03"), 40 },
                    { new Guid("60c1a391-59b4-4cea-ba83-59e09f7512b6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("d1ca913c-dee7-46d8-9fd4-ea564af8005f"), new Guid("5850e103-4ac9-472e-85f2-cddc08732ccc"), 20 },
                    { new Guid("625a9195-2380-4762-8dc6-13163e354ef6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("474775e9-f08a-4043-8474-e84f42bf3948"), new Guid("d2236889-d7d1-4896-b449-69f273c6b514"), 20 },
                    { new Guid("65975857-ab27-480d-87c3-dba74d45cb63"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("9804d695-d8c7-40bd-814f-8458b55fb583"), new Guid("33bbdccf-59a9-4b05-bdac-af50137cecb3"), 30 },
                    { new Guid("68e947c0-9450-4b64-90d7-553850396a3f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("d1ca913c-dee7-46d8-9fd4-ea564af8005f"), new Guid("1f0e9a86-4641-4d7e-8413-a1beba0e8afb"), 10 },
                    { new Guid("725a4f4a-37cb-46ba-93a3-7b9cc2b015cb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("9804d695-d8c7-40bd-814f-8458b55fb583"), new Guid("362efd25-e1d2-496d-b7fa-884371a58682"), 10 },
                    { new Guid("75f2d1c3-4ba2-4acc-8fd3-6b01ca66d1c9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("9804d695-d8c7-40bd-814f-8458b55fb583"), new Guid("425f1526-0513-4535-bdd8-47632d82956f"), 50 },
                    { new Guid("7fb30d45-1faf-4f6a-ac5d-436204ad8e69"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("f5d4763e-5862-4b62-ab92-2748ad213b10"), new Guid("5d31f1f7-73fd-42a4-a429-33fab925b15d"), 50 },
                    { new Guid("8168cfbf-7e53-41c5-8bc4-f5392d9a3b57"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("1bae5715-8363-4221-8735-8def3d2546e1"), new Guid("362efd25-e1d2-496d-b7fa-884371a58682"), 20 },
                    { new Guid("8b7d7f26-b7e5-42e2-afc1-cedddbae841a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("f5d4763e-5862-4b62-ab92-2748ad213b10"), new Guid("66a6446a-7191-4f14-9c5d-052891b9c5a2"), 40 },
                    { new Guid("90b5cfa9-890b-4b89-a750-646f3a26db23"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("d1ca913c-dee7-46d8-9fd4-ea564af8005f"), new Guid("0d1073cd-f6d5-4572-87ac-98ab6f15c05a"), 30 },
                    { new Guid("93033f7e-a3c1-45e3-8a17-021d0a4abe5a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("09be8eff-72e4-40a8-a1ed-717deb185a69"), new Guid("c76de830-3746-449a-8f1f-bd5d9233655c"), 20 },
                    { new Guid("9363bb46-937e-42bf-bb71-5fb16126b501"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("395ead29-7ecc-4999-b479-dffe97437e3a"), new Guid("5850e103-4ac9-472e-85f2-cddc08732ccc"), 20 },
                    { new Guid("a15014ad-582e-4388-9b58-aceb52cf41bf"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("d1ca913c-dee7-46d8-9fd4-ea564af8005f"), new Guid("b67d1ac5-80ec-4b7d-bcb8-72e3da55f201"), 40 },
                    { new Guid("ab5c5904-2683-47c4-a436-319303b8e62f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("d1ca913c-dee7-46d8-9fd4-ea564af8005f"), new Guid("5db547d6-c115-4409-8db7-59374ca2af83"), 90 },
                    { new Guid("ade78d45-b010-4ed7-87f0-e30e0558f151"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("f5d4763e-5862-4b62-ab92-2748ad213b10"), new Guid("f0f26735-b796-4a70-a20c-92e0e2910bb4"), 20 },
                    { new Guid("b0dcb5e9-bbc6-4004-b9d7-0f6723416b9b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("13376e1d-2378-4e30-a6d2-808da4a4ba4d"), new Guid("b3bd7011-2cda-49d9-8fea-46fa02db9c4b"), 10 },
                    { new Guid("b6cf76a5-ec3f-4e81-9499-174d33bb7249"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("09be8eff-72e4-40a8-a1ed-717deb185a69"), new Guid("66a6446a-7191-4f14-9c5d-052891b9c5a2"), 30 },
                    { new Guid("b793fa86-2025-4258-8993-8045f4c312d7"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("9804d695-d8c7-40bd-814f-8458b55fb583"), new Guid("34a52363-4a57-4019-abcf-0c9880246891"), 20 },
                    { new Guid("bc29bf0a-2ebb-4db8-8765-a5f835492552"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("9804d695-d8c7-40bd-814f-8458b55fb583"), new Guid("bd0f37e1-ec14-4d87-8380-117b4480d7a4"), 40 },
                    { new Guid("c6b0b06f-a915-4087-9827-34e76ab6895f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("474775e9-f08a-4043-8474-e84f42bf3948"), new Guid("a80c8892-7cba-4b19-b84d-937da70c8af3"), 40 },
                    { new Guid("c9225a82-0348-41bb-a591-7726f8079cde"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("5cf52155-927f-4d64-a482-348f952bab21"), new Guid("1e60dfdf-e7c9-4378-b1af-dcb53fe20022"), 30 },
                    { new Guid("cdfb1c47-22dc-4657-aab8-1dbfaf21e862"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("395ead29-7ecc-4999-b479-dffe97437e3a"), new Guid("1f0e9a86-4641-4d7e-8413-a1beba0e8afb"), 10 },
                    { new Guid("e0abe26f-27da-4396-b80c-d1ceb836a8b2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("1bae5715-8363-4221-8735-8def3d2546e1"), new Guid("1c1bec30-91d2-4699-8753-67f4feb53df3"), 10 },
                    { new Guid("eef4a4d1-796b-4b37-96f6-f31dbccf0aeb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("1bae5715-8363-4221-8735-8def3d2546e1"), new Guid("26686d6e-853e-4d57-b10d-35444ae824be"), 30 },
                    { new Guid("f1c2c792-f11f-43ab-8cf6-d6ff905894fc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("13376e1d-2378-4e30-a6d2-808da4a4ba4d"), new Guid("26686d6e-853e-4d57-b10d-35444ae824be"), 30 },
                    { new Guid("ff994b2c-a3bd-4676-a974-f53d4fa562ba"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("f5d4763e-5862-4b62-ab92-2748ad213b10"), new Guid("86bf6480-787a-4fe0-9d79-0f8d0d36acc4"), 30 }
                });

            _ = migrationBuilder.CreateIndex(
                name: "ix_projects_state_id",
                table: "projects",
                column: "state_id");

            _ = migrationBuilder.CreateIndex(
                name: "ix_project_participations_invitation_status_id",
                table: "project_participations",
                column: "invitation_status_id");

            _ = migrationBuilder.CreateIndex(
                name: "ix_project_participations_participation_status_inner_id",
                table: "project_participations",
                column: "participation_status_inner_id");

            _ = migrationBuilder.CreateIndex(
                name: "ix_project_participations_participation_status_internal_id",
                table: "project_participations",
                column: "participation_status_internal_id");

            _ = migrationBuilder.CreateIndex(
                name: "ix_musician_profiles_inquiry_status_inner_id",
                table: "musician_profiles",
                column: "inquiry_status_inner_id");

            _ = migrationBuilder.CreateIndex(
                name: "ix_musician_profiles_inquiry_status_team_id",
                table: "musician_profiles",
                column: "inquiry_status_team_id");

            _ = migrationBuilder.CreateIndex(
                name: "ix_appointments_status_id",
                table: "appointments",
                column: "status_id");

            _ = migrationBuilder.CreateIndex(
                name: "ix_appointment_participations_prediction_id",
                table: "appointment_participations",
                column: "prediction_id");

            _ = migrationBuilder.CreateIndex(
                name: "ix_appointment_participations_result_id",
                table: "appointment_participations",
                column: "result_id");

            _ = migrationBuilder.AddForeignKey(
                name: "fk_appointment_participations_select_value_mappings_prediction",
                table: "appointment_participations",
                column: "prediction_id",
                principalTable: "select_value_mappings",
                principalColumn: "id");

            _ = migrationBuilder.AddForeignKey(
                name: "fk_appointment_participations_select_value_mappings_result_id",
                table: "appointment_participations",
                column: "result_id",
                principalTable: "select_value_mappings",
                principalColumn: "id");

            _ = migrationBuilder.AddForeignKey(
                name: "fk_appointments_select_value_mappings_status_id",
                table: "appointments",
                column: "status_id",
                principalTable: "select_value_mappings",
                principalColumn: "id");

            _ = migrationBuilder.AddForeignKey(
                name: "fk_musician_profiles_select_value_mappings_inquiry_status_inne",
                table: "musician_profiles",
                column: "inquiry_status_inner_id",
                principalTable: "select_value_mappings",
                principalColumn: "id");

            _ = migrationBuilder.AddForeignKey(
                name: "fk_musician_profiles_select_value_mappings_inquiry_status_team",
                table: "musician_profiles",
                column: "inquiry_status_team_id",
                principalTable: "select_value_mappings",
                principalColumn: "id");

            _ = migrationBuilder.AddForeignKey(
                name: "fk_project_participations_select_value_mappings_invitation_sta",
                table: "project_participations",
                column: "invitation_status_id",
                principalTable: "select_value_mappings",
                principalColumn: "id");

            _ = migrationBuilder.AddForeignKey(
                name: "fk_project_participations_select_value_mappings_participation_",
                table: "project_participations",
                column: "participation_status_inner_id",
                principalTable: "select_value_mappings",
                principalColumn: "id");

            _ = migrationBuilder.AddForeignKey(
                name: "fk_project_participations_select_value_mappings_participation_1",
                table: "project_participations",
                column: "participation_status_internal_id",
                principalTable: "select_value_mappings",
                principalColumn: "id");

            _ = migrationBuilder.AddForeignKey(
                name: "fk_projects_select_value_mappings_state_id",
                table: "projects",
                column: "state_id",
                principalTable: "select_value_mappings",
                principalColumn: "id");
        }
    }
}
