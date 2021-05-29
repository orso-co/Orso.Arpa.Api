using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Orso.Arpa.Persistence.Migrations
{
    public partial class ProjectParticipation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "comment_by_performer_inner",
                table: "project_participations",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "comment_by_staff_inner",
                table: "project_participations",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "comment_team",
                table: "project_participations",
                type: "text",
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

            migrationBuilder.InsertData(
                table: "select_value_categories",
                columns: new[] { "id", "created_at", "created_by", "deleted", "modified_at", "modified_by", "name", "property", "table" },
                values: new object[,]
                {
                    { new Guid("474775e9-f08a-4043-8474-e84f42bf3948"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Participation invitation status", "InvitationStatus", "ProjectParticipation" },
                    { new Guid("1bae5715-8363-4221-8735-8def3d2546e1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Participation status inner", "ParticipationStatusInner", "ProjectParticipation" },
                    { new Guid("13376e1d-2378-4e30-a6d2-808da4a4ba4d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Participation status internal", "ParticipationStatusInternal", "ProjectParticipation" }
                });

            migrationBuilder.InsertData(
                table: "select_values",
                columns: new[] { "id", "created_at", "created_by", "deleted", "description", "modified_at", "modified_by", "name" },
                values: new object[,]
                {
                    { new Guid("1c1bec30-91d2-4699-8753-67f4feb53df3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Interested" },
                    { new Guid("26686d6e-853e-4d57-b10d-35444ae824be"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Acceptance" },
                    { new Guid("78d6ce19-ac32-444f-94a6-aa4262340fa1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Refusal" },
                    { new Guid("a80c8892-7cba-4b19-b84d-937da70c8af3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Unclear" },
                    { new Guid("b3bd7011-2cda-49d9-8fea-46fa02db9c4b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Candidate" },
                    { new Guid("d2236889-d7d1-4896-b449-69f273c6b514"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Invited" },
                    { new Guid("77c68dbb-a627-4053-829e-86c555754f60"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Not invited" }
                });

            migrationBuilder.InsertData(
                table: "select_value_mappings",
                columns: new[] { "id", "created_at", "created_by", "deleted", "modified_at", "modified_by", "select_value_category_id", "select_value_id" },
                values: new object[,]
                {
                    { new Guid("625a9195-2380-4762-8dc6-13163e354ef6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("474775e9-f08a-4043-8474-e84f42bf3948"), new Guid("d2236889-d7d1-4896-b449-69f273c6b514") },
                    { new Guid("2ad77626-e0b3-45a6-9d24-e4677181ee7e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("474775e9-f08a-4043-8474-e84f42bf3948"), new Guid("77c68dbb-a627-4053-829e-86c555754f60") },
                    { new Guid("2a5f85e6-a7ed-48eb-852c-0b191d7ba949"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("474775e9-f08a-4043-8474-e84f42bf3948"), new Guid("b3bd7011-2cda-49d9-8fea-46fa02db9c4b") },
                    { new Guid("c6b0b06f-a915-4087-9827-34e76ab6895f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("474775e9-f08a-4043-8474-e84f42bf3948"), new Guid("a80c8892-7cba-4b19-b84d-937da70c8af3") },
                    { new Guid("e0abe26f-27da-4396-b80c-d1ceb836a8b2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("1bae5715-8363-4221-8735-8def3d2546e1"), new Guid("1c1bec30-91d2-4699-8753-67f4feb53df3") },
                    { new Guid("eef4a4d1-796b-4b37-96f6-f31dbccf0aeb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("1bae5715-8363-4221-8735-8def3d2546e1"), new Guid("26686d6e-853e-4d57-b10d-35444ae824be") },
                    { new Guid("1d402f12-816d-4994-a94d-28d52cb2d199"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("1bae5715-8363-4221-8735-8def3d2546e1"), new Guid("78d6ce19-ac32-444f-94a6-aa4262340fa1") },
                    { new Guid("8168cfbf-7e53-41c5-8bc4-f5392d9a3b57"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("1bae5715-8363-4221-8735-8def3d2546e1"), new Guid("362efd25-e1d2-496d-b7fa-884371a58682") },
                    { new Guid("b0dcb5e9-bbc6-4004-b9d7-0f6723416b9b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("13376e1d-2378-4e30-a6d2-808da4a4ba4d"), new Guid("b3bd7011-2cda-49d9-8fea-46fa02db9c4b") },
                    { new Guid("f1c2c792-f11f-43ab-8cf6-d6ff905894fc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("13376e1d-2378-4e30-a6d2-808da4a4ba4d"), new Guid("26686d6e-853e-4d57-b10d-35444ae824be") },
                    { new Guid("0096f414-50c9-4d45-9a85-4af30641b7fa"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("13376e1d-2378-4e30-a6d2-808da4a4ba4d"), new Guid("78d6ce19-ac32-444f-94a6-aa4262340fa1") },
                    { new Guid("03bdcf0a-2638-4b8f-a093-4084b9969162"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("13376e1d-2378-4e30-a6d2-808da4a4ba4d"), new Guid("362efd25-e1d2-496d-b7fa-884371a58682") }
                });

            migrationBuilder.CreateIndex(
                name: "ix_project_participations_invitation_status_id",
                table: "project_participations",
                column: "invitation_status_id");

            migrationBuilder.CreateIndex(
                name: "ix_project_participations_participation_status_inner_id",
                table: "project_participations",
                column: "participation_status_inner_id");

            migrationBuilder.CreateIndex(
                name: "ix_project_participations_participation_status_internal_id",
                table: "project_participations",
                column: "participation_status_internal_id");

            migrationBuilder.AddForeignKey(
                name: "fk_project_participations_select_value_mappings_invitation_sta",
                table: "project_participations",
                column: "invitation_status_id",
                principalTable: "select_value_mappings",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_project_participations_select_value_mappings_participation_",
                table: "project_participations",
                column: "participation_status_inner_id",
                principalTable: "select_value_mappings",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_project_participations_select_value_mappings_participation_1",
                table: "project_participations",
                column: "participation_status_internal_id",
                principalTable: "select_value_mappings",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_project_participations_select_value_mappings_invitation_sta",
                table: "project_participations");

            migrationBuilder.DropForeignKey(
                name: "fk_project_participations_select_value_mappings_participation_",
                table: "project_participations");

            migrationBuilder.DropForeignKey(
                name: "fk_project_participations_select_value_mappings_participation_1",
                table: "project_participations");

            migrationBuilder.DropIndex(
                name: "ix_project_participations_invitation_status_id",
                table: "project_participations");

            migrationBuilder.DropIndex(
                name: "ix_project_participations_participation_status_inner_id",
                table: "project_participations");

            migrationBuilder.DropIndex(
                name: "ix_project_participations_participation_status_internal_id",
                table: "project_participations");

            migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("0096f414-50c9-4d45-9a85-4af30641b7fa"));

            migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("03bdcf0a-2638-4b8f-a093-4084b9969162"));

            migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("1d402f12-816d-4994-a94d-28d52cb2d199"));

            migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("2a5f85e6-a7ed-48eb-852c-0b191d7ba949"));

            migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("2ad77626-e0b3-45a6-9d24-e4677181ee7e"));

            migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("625a9195-2380-4762-8dc6-13163e354ef6"));

            migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("8168cfbf-7e53-41c5-8bc4-f5392d9a3b57"));

            migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("b0dcb5e9-bbc6-4004-b9d7-0f6723416b9b"));

            migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("c6b0b06f-a915-4087-9827-34e76ab6895f"));

            migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("e0abe26f-27da-4396-b80c-d1ceb836a8b2"));

            migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("eef4a4d1-796b-4b37-96f6-f31dbccf0aeb"));

            migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("f1c2c792-f11f-43ab-8cf6-d6ff905894fc"));

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
                keyValue: new Guid("474775e9-f08a-4043-8474-e84f42bf3948"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("1c1bec30-91d2-4699-8753-67f4feb53df3"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("26686d6e-853e-4d57-b10d-35444ae824be"));

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
                keyValue: new Guid("a80c8892-7cba-4b19-b84d-937da70c8af3"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("b3bd7011-2cda-49d9-8fea-46fa02db9c4b"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("d2236889-d7d1-4896-b449-69f273c6b514"));

            migrationBuilder.DropColumn(
                name: "comment_by_performer_inner",
                table: "project_participations");

            migrationBuilder.DropColumn(
                name: "comment_by_staff_inner",
                table: "project_participations");

            migrationBuilder.DropColumn(
                name: "comment_team",
                table: "project_participations");

            migrationBuilder.DropColumn(
                name: "invitation_status_id",
                table: "project_participations");

            migrationBuilder.DropColumn(
                name: "participation_status_inner_id",
                table: "project_participations");

            migrationBuilder.DropColumn(
                name: "participation_status_internal_id",
                table: "project_participations");
        }
    }
}
