using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Orso.Arpa.Persistence.Migrations
{
    public partial class EntitiesEducationAndCurriculumVitaeReferences : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "details",
                table: "curriculum_vitae_reference");

            migrationBuilder.DropColumn(
                name: "keyword",
                table: "curriculum_vitae_reference");

            migrationBuilder.RenameColumn(
                name: "comment",
                table: "educations",
                newName: "description");

            migrationBuilder.AlterColumn<string>(
                name: "institution",
                table: "educations",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "type_id",
                table: "educations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "curriculum_vitae_reference",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "institution",
                table: "curriculum_vitae_reference",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "type_id",
                table: "curriculum_vitae_reference",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "select_value_categories",
                columns: new[] { "id", "created_at", "created_by", "deleted", "modified_at", "modified_by", "name", "property", "table" },
                values: new object[,]
                {
                    { new Guid("502a47d4-6c2f-4729-99db-470f4e0e1a3b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Education type", "Type", "Education" },
                    { new Guid("3addf4f6-1904-4944-86f6-434d2660594f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Curriculum vitae reference type", "Type", "CurriculumVitaeReference" }
                });

            migrationBuilder.InsertData(
                table: "select_values",
                columns: new[] { "id", "created_at", "created_by", "deleted", "description", "modified_at", "modified_by", "name" },
                values: new object[,]
                {
                    { new Guid("d73cba63-f92e-4c17-b416-59f8e021fbf2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Private lesson" },
                    { new Guid("d45ac8a2-f17c-49ca-9525-99473771a340"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Music school" },
                    { new Guid("371ee51d-3612-4eb4-b169-25eae26c382f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "University" },
                    { new Guid("fcad4595-cea8-4339-bc48-312d43d7d4a0"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Conservatory" },
                    { new Guid("bfdf244d-6d85-41e8-a10f-6f309abe9ffe"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Master class" },
                    { new Guid("57bf8f44-d6f5-4551-a571-a42565e5861a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Ensemble position" },
                    { new Guid("8cf0c997-33bd-431b-a28c-7d22c00d8d87"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Solo performance" },
                    { new Guid("674abb4f-89d1-4802-bfee-8eb0d61bed80"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Competition / Prize" },
                    { new Guid("64db8d53-128b-4b3d-85ac-23292fad29e9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Recommendation" }
                });

            migrationBuilder.InsertData(
                table: "select_value_mappings",
                columns: new[] { "id", "created_at", "created_by", "deleted", "modified_at", "modified_by", "select_value_category_id", "select_value_id" },
                values: new object[,]
                {
                    { new Guid("99251f16-deca-437e-84e2-a747e1a8ad7f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("502a47d4-6c2f-4729-99db-470f4e0e1a3b"), new Guid("d73cba63-f92e-4c17-b416-59f8e021fbf2") },
                    { new Guid("d259e4bc-9302-4b42-9b0c-2087fc1680e7"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("502a47d4-6c2f-4729-99db-470f4e0e1a3b"), new Guid("d45ac8a2-f17c-49ca-9525-99473771a340") },
                    { new Guid("5f071c88-813b-47c2-85a3-1d89321b7302"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("502a47d4-6c2f-4729-99db-470f4e0e1a3b"), new Guid("371ee51d-3612-4eb4-b169-25eae26c382f") },
                    { new Guid("574e8627-14fa-4a76-b05a-b80305994f98"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("502a47d4-6c2f-4729-99db-470f4e0e1a3b"), new Guid("fcad4595-cea8-4339-bc48-312d43d7d4a0") },
                    { new Guid("025a7a5c-3c61-4527-8ae0-769ad546bf1a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("502a47d4-6c2f-4729-99db-470f4e0e1a3b"), new Guid("bfdf244d-6d85-41e8-a10f-6f309abe9ffe") },
                    { new Guid("149d5e63-a800-423a-b893-f1b763989d04"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("502a47d4-6c2f-4729-99db-470f4e0e1a3b"), new Guid("e030b53e-3615-4cd6-9fe6-0d818632a4b0") },
                    { new Guid("dead0ae2-bb2b-4584-992e-dddeb7f23d53"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("3addf4f6-1904-4944-86f6-434d2660594f"), new Guid("57bf8f44-d6f5-4551-a571-a42565e5861a") },
                    { new Guid("3245182e-7985-4c07-828e-d69287ff2a2d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("3addf4f6-1904-4944-86f6-434d2660594f"), new Guid("8cf0c997-33bd-431b-a28c-7d22c00d8d87") },
                    { new Guid("d30083ca-235f-43fa-9cba-3acdacf52b93"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("3addf4f6-1904-4944-86f6-434d2660594f"), new Guid("674abb4f-89d1-4802-bfee-8eb0d61bed80") },
                    { new Guid("28d79b43-18be-48b2-a6c9-776ddea0bdb2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("3addf4f6-1904-4944-86f6-434d2660594f"), new Guid("64db8d53-128b-4b3d-85ac-23292fad29e9") },
                    { new Guid("8822614e-3e7c-4224-bb9c-468cec939bbc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("3addf4f6-1904-4944-86f6-434d2660594f"), new Guid("e030b53e-3615-4cd6-9fe6-0d818632a4b0") }
                });

            migrationBuilder.CreateIndex(
                name: "ix_educations_type_id",
                table: "educations",
                column: "type_id");

            migrationBuilder.CreateIndex(
                name: "ix_curriculum_vitae_reference_type_id",
                table: "curriculum_vitae_reference",
                column: "type_id");

            migrationBuilder.AddForeignKey(
                name: "fk_curriculum_vitae_reference_select_value_mappings_type_id",
                table: "curriculum_vitae_reference",
                column: "type_id",
                principalTable: "select_value_mappings",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_educations_select_value_mappings_type_id",
                table: "educations",
                column: "type_id",
                principalTable: "select_value_mappings",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_curriculum_vitae_reference_select_value_mappings_type_id",
                table: "curriculum_vitae_reference");

            migrationBuilder.DropForeignKey(
                name: "fk_educations_select_value_mappings_type_id",
                table: "educations");

            migrationBuilder.DropIndex(
                name: "ix_educations_type_id",
                table: "educations");

            migrationBuilder.DropIndex(
                name: "ix_curriculum_vitae_reference_type_id",
                table: "curriculum_vitae_reference");

            migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("025a7a5c-3c61-4527-8ae0-769ad546bf1a"));

            migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("149d5e63-a800-423a-b893-f1b763989d04"));

            migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("28d79b43-18be-48b2-a6c9-776ddea0bdb2"));

            migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("3245182e-7985-4c07-828e-d69287ff2a2d"));

            migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("574e8627-14fa-4a76-b05a-b80305994f98"));

            migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("5f071c88-813b-47c2-85a3-1d89321b7302"));

            migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("8822614e-3e7c-4224-bb9c-468cec939bbc"));

            migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("99251f16-deca-437e-84e2-a747e1a8ad7f"));

            migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("d259e4bc-9302-4b42-9b0c-2087fc1680e7"));

            migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("d30083ca-235f-43fa-9cba-3acdacf52b93"));

            migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("dead0ae2-bb2b-4584-992e-dddeb7f23d53"));

            migrationBuilder.DeleteData(
                table: "select_value_categories",
                keyColumn: "id",
                keyValue: new Guid("3addf4f6-1904-4944-86f6-434d2660594f"));

            migrationBuilder.DeleteData(
                table: "select_value_categories",
                keyColumn: "id",
                keyValue: new Guid("502a47d4-6c2f-4729-99db-470f4e0e1a3b"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("371ee51d-3612-4eb4-b169-25eae26c382f"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("57bf8f44-d6f5-4551-a571-a42565e5861a"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("64db8d53-128b-4b3d-85ac-23292fad29e9"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("674abb4f-89d1-4802-bfee-8eb0d61bed80"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("8cf0c997-33bd-431b-a28c-7d22c00d8d87"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("bfdf244d-6d85-41e8-a10f-6f309abe9ffe"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("d45ac8a2-f17c-49ca-9525-99473771a340"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("d73cba63-f92e-4c17-b416-59f8e021fbf2"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("fcad4595-cea8-4339-bc48-312d43d7d4a0"));

            migrationBuilder.DropColumn(
                name: "type_id",
                table: "educations");

            migrationBuilder.DropColumn(
                name: "description",
                table: "curriculum_vitae_reference");

            migrationBuilder.DropColumn(
                name: "institution",
                table: "curriculum_vitae_reference");

            migrationBuilder.DropColumn(
                name: "type_id",
                table: "curriculum_vitae_reference");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "educations",
                newName: "comment");

            migrationBuilder.AlterColumn<string>(
                name: "institution",
                table: "educations",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "details",
                table: "curriculum_vitae_reference",
                type: "character varying(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "keyword",
                table: "curriculum_vitae_reference",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);
        }
    }
}
