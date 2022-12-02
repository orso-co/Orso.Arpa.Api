using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Orso.Arpa.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CleanUpObsoleteFieldsAndDataSets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_auditions_musician_profiles_musician_profile_id",
                table: "auditions");

            migrationBuilder.DropIndex(
                name: "ix_auditions_musician_profile_id",
                table: "auditions");

            migrationBuilder.DeleteData(
                table: "select_value_categories",
                keyColumn: "id",
                keyValue: new Guid("09be8eff-72e4-40a8-a1ed-717deb185a69"));

            migrationBuilder.DeleteData(
                table: "select_value_categories",
                keyColumn: "id",
                keyValue: new Guid("13376e1d-2378-4e30-a6d2-808da4a4ba4d"));

            migrationBuilder.DeleteData(
                table: "select_value_categories",
                keyColumn: "id",
                keyValue: new Guid("1bae5715-8363-4221-8735-8def3d2546e1"));

            migrationBuilder.DeleteData(
                table: "select_value_categories",
                keyColumn: "id",
                keyValue: new Guid("395ead29-7ecc-4999-b479-dffe97437e3a"));

            migrationBuilder.DeleteData(
                table: "select_value_categories",
                keyColumn: "id",
                keyValue: new Guid("474775e9-f08a-4043-8474-e84f42bf3948"));

            migrationBuilder.DeleteData(
                table: "select_value_categories",
                keyColumn: "id",
                keyValue: new Guid("5cf52155-927f-4d64-a482-348f952bab21"));

            migrationBuilder.DeleteData(
                table: "select_value_categories",
                keyColumn: "id",
                keyValue: new Guid("9804d695-d8c7-40bd-814f-8458b55fb583"));

            migrationBuilder.DeleteData(
                table: "select_value_categories",
                keyColumn: "id",
                keyValue: new Guid("d1ca913c-dee7-46d8-9fd4-ea564af8005f"));

            migrationBuilder.DeleteData(
                table: "select_value_categories",
                keyColumn: "id",
                keyValue: new Guid("f5d4763e-5862-4b62-ab92-2748ad213b10"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("0d1073cd-f6d5-4572-87ac-98ab6f15c05a"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("1c1bec30-91d2-4699-8753-67f4feb53df3"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("1e60dfdf-e7c9-4378-b1af-dcb53fe20022"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("1f0e9a86-4641-4d7e-8413-a1beba0e8afb"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("26686d6e-853e-4d57-b10d-35444ae824be"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("313445ca-57fa-45f0-8515-325949d60726"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("425f1526-0513-4535-bdd8-47632d82956f"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("4a9de438-ccce-4a95-873a-c8befb933067"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("4ee7d317-6d71-4d6e-b45a-954c8c7dcf03"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("5850e103-4ac9-472e-85f2-cddc08732ccc"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("5d31f1f7-73fd-42a4-a429-33fab925b15d"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("5db547d6-c115-4409-8db7-59374ca2af83"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("5e3edcf4-863b-433b-ae72-b6bb7e4dfc95"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("61dab188-a07d-4a58-8ec9-c54050e914ac"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("77c68dbb-a627-4053-829e-86c555754f60"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("78d6ce19-ac32-444f-94a6-aa4262340fa1"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("86bf6480-787a-4fe0-9d79-0f8d0d36acc4"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("99d192e1-332a-494e-b821-075be14211be"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("a80c8892-7cba-4b19-b84d-937da70c8af3"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("b3bd7011-2cda-49d9-8fea-46fa02db9c4b"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("b85984d6-4390-44f9-bd92-5d1000cb4d3f"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("bd0f37e1-ec14-4d87-8380-117b4480d7a4"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("c76de830-3746-449a-8f1f-bd5d9233655c"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("d2236889-d7d1-4896-b449-69f273c6b514"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("f0f26735-b796-4a70-a20c-92e0e2910bb4"));

            migrationBuilder.DropColumn(
                name: "state_id",
                table: "projects");

            migrationBuilder.DropColumn(
                name: "invitation_status_id",
                table: "project_participations");

            migrationBuilder.DropColumn(
                name: "participation_status_inner_id",
                table: "project_participations");

            migrationBuilder.DropColumn(
                name: "participation_status_internal_id",
                table: "project_participations");

            migrationBuilder.DropColumn(
                name: "inquiry_status_inner_id",
                table: "musician_profiles");

            migrationBuilder.DropColumn(
                name: "inquiry_status_team_id",
                table: "musician_profiles");

            migrationBuilder.DropColumn(
                name: "musician_profile_id",
                table: "auditions");

            migrationBuilder.DropColumn(
                name: "status_id",
                table: "appointments");

            migrationBuilder.DropColumn(
                name: "prediction_id",
                table: "appointment_participations");

            migrationBuilder.DropColumn(
                name: "result_id",
                table: "appointment_participations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "state_id",
                table: "projects",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "invitation_status_id",
                table: "project_participations",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "participation_status_inner_id",
                table: "project_participations",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "participation_status_internal_id",
                table: "project_participations",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "inquiry_status_inner_id",
                table: "musician_profiles",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "inquiry_status_team_id",
                table: "musician_profiles",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "musician_profile_id",
                table: "auditions",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "status_id",
                table: "appointments",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "prediction_id",
                table: "appointment_participations",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "result_id",
                table: "appointment_participations",
                type: "uuid",
                nullable: true);

            migrationBuilder.InsertData(
                table: "select_value_categories",
                columns: new[] { "id", "created_at", "created_by", "deleted", "modified_at", "modified_by", "name", "property", "table" },
                values: new object[,]
                {
                    { new Guid("09be8eff-72e4-40a8-a1ed-717deb185a69"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Status", "Status", "Appointment" },
                    { new Guid("13376e1d-2378-4e30-a6d2-808da4a4ba4d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Participation status internal", "ParticipationStatusInternal", "ProjectParticipation" },
                    { new Guid("1bae5715-8363-4221-8735-8def3d2546e1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Participation status inner", "ParticipationStatusInner", "ProjectParticipation" },
                    { new Guid("395ead29-7ecc-4999-b479-dffe97437e3a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Inquiry status staff", "InquiryStatusTeam", "MusicianProfile" },
                    { new Guid("474775e9-f08a-4043-8474-e84f42bf3948"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Participation invitation status", "InvitationStatus", "ProjectParticipation" },
                    { new Guid("5cf52155-927f-4d64-a482-348f952bab21"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Prediction Participant", "Prediction", "AppointmentParticipation" },
                    { new Guid("9804d695-d8c7-40bd-814f-8458b55fb583"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "State", "Status", "Project" },
                    { new Guid("d1ca913c-dee7-46d8-9fd4-ea564af8005f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Inquiry status performer", "InquiryStatusInner", "MusicianProfile" },
                    { new Guid("f5d4763e-5862-4b62-ab92-2748ad213b10"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Result", "Result", "AppointmentParticipation" }
                });

            migrationBuilder.InsertData(
                table: "select_values",
                columns: new[] { "id", "created_at", "created_by", "deleted", "description", "modified_at", "modified_by", "name" },
                values: new object[,]
                {
                    { new Guid("0d1073cd-f6d5-4572-87ac-98ab6f15c05a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "For contacts only" },
                    { new Guid("1c1bec30-91d2-4699-8753-67f4feb53df3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Interested" },
                    { new Guid("1e60dfdf-e7c9-4378-b1af-dcb53fe20022"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Partly" },
                    { new Guid("1f0e9a86-4641-4d7e-8413-a1beba0e8afb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Gladly" },
                    { new Guid("26686d6e-853e-4d57-b10d-35444ae824be"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Acceptance" },
                    { new Guid("313445ca-57fa-45f0-8515-325949d60726"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Present" },
                    { new Guid("425f1526-0513-4535-bdd8-47632d82956f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Archived" },
                    { new Guid("4a9de438-ccce-4a95-873a-c8befb933067"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "2nd section" },
                    { new Guid("4ee7d317-6d71-4d6e-b45a-954c8c7dcf03"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Don't know yet" },
                    { new Guid("5850e103-4ac9-472e-85f2-cddc08732ccc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Emergency only" },
                    { new Guid("5d31f1f7-73fd-42a4-a429-33fab925b15d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Awaiting Scan" },
                    { new Guid("5db547d6-c115-4409-8db7-59374ca2af83"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Never again" },
                    { new Guid("5e3edcf4-863b-433b-ae72-b6bb7e4dfc95"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Awaiting Poll" },
                    { new Guid("61dab188-a07d-4a58-8ec9-c54050e914ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Accompaniment" },
                    { new Guid("77c68dbb-a627-4053-829e-86c555754f60"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Not Invited" },
                    { new Guid("78d6ce19-ac32-444f-94a6-aa4262340fa1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Refusal" },
                    { new Guid("86bf6480-787a-4fe0-9d79-0f8d0d36acc4"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Inapplicable" },
                    { new Guid("99d192e1-332a-494e-b821-075be14211be"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Refused" },
                    { new Guid("a80c8892-7cba-4b19-b84d-937da70c8af3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Unclear" },
                    { new Guid("b3bd7011-2cda-49d9-8fea-46fa02db9c4b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Candidate" },
                    { new Guid("b85984d6-4390-44f9-bd92-5d1000cb4d3f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "1st section" },
                    { new Guid("bd0f37e1-ec14-4d87-8380-117b4480d7a4"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Postponed" },
                    { new Guid("c76de830-3746-449a-8f1f-bd5d9233655c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Scheduled" },
                    { new Guid("d2236889-d7d1-4896-b449-69f273c6b514"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Invited" },
                    { new Guid("f0f26735-b796-4a70-a20c-92e0e2910bb4"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Absent" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_auditions_musician_profile_id",
                table: "auditions",
                column: "musician_profile_id");

            migrationBuilder.AddForeignKey(
                name: "fk_auditions_musician_profiles_musician_profile_id",
                table: "auditions",
                column: "musician_profile_id",
                principalTable: "musician_profiles",
                principalColumn: "id");
        }
    }
}
