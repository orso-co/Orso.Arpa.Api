using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Orso.Arpa.Persistence.Migrations
{
    public partial class AddedSalaryToMusicianProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_SelectValueMappings_EmolumentId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_SelectValueMappings_EmolumentPatternId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_SelectValueMappings_ExpectationId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_SelectValueMappings_StatusId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_MusicianProfiles_SelectValueMappings_QualificationId",
                table: "MusicianProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_Sections_ParentId",
                table: "Sections");

            migrationBuilder.AddColumn<bool>(
                name: "IsMainProfile",
                table: "MusicianProfiles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<byte>(
                name: "LevelInnerASsessment",
                table: "MusicianProfiles",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "LevelSelfAssessment",
                table: "MusicianProfiles",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "ProfileFavorizitation",
                table: "MusicianProfiles",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<Guid>(
                name: "SalaryId",
                table: "MusicianProfiles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.InsertData(
                table: "SelectValueCategories",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "ModifiedAt", "ModifiedBy", "Name", "Property", "Table" },
                values: new object[] { new Guid("9c6b7ba0-f672-433f-b1e3-a80a2eb0a3ea"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Salary", "Salary", "MusicianProfile" });

            migrationBuilder.InsertData(
                table: "SelectValueMappings",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "ModifiedAt", "ModifiedBy", "SelectValueCategoryId", "SelectValueId" },
                values: new object[,]
                {
                    { new Guid("f036bca9-95d4-4526-b845-fff9208ab103"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("9648daa0-c2b2-4b97-912b-7ce30b9534a8"), new Guid("3f93768e-ac24-4741-9eb8-49d1e8e4a6e1") },
                    { new Guid("6304b935-633d-4bba-a90f-9bd864c867c6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("9648daa0-c2b2-4b97-912b-7ce30b9534a8"), new Guid("e20ff004-aafc-4e28-87f9-0d9c6372951c") },
                    { new Guid("20f9698c-2f3d-41fd-9f33-1feeababfb1c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("9648daa0-c2b2-4b97-912b-7ce30b9534a8"), new Guid("35d63f30-8704-47d5-865a-ee713a082433") },
                    { new Guid("30f592f6-485a-468a-bfb2-4854be733e74"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("9648daa0-c2b2-4b97-912b-7ce30b9534a8"), new Guid("f52b9170-c6f6-4828-b96c-df5dfbe9bd73") },
                    { new Guid("42d76464-4b4b-4884-b8e3-1f69baaced4c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("9648daa0-c2b2-4b97-912b-7ce30b9534a8"), new Guid("b67d1ac5-80ec-4b7d-bcb8-72e3da55f201") }
                });

            migrationBuilder.InsertData(
                table: "SelectValues",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "Description", "ModifiedAt", "ModifiedBy", "Name" },
                values: new object[,]
                {
                    { new Guid("3c014654-b4c9-4c7a-a251-ae88ad504c8a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Without" },
                    { new Guid("dec26aef-f0de-4c9f-a164-e23e2543c987"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "With - strict" },
                    { new Guid("d53b4a35-f472-42a1-ab22-c7afb1e7d77e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "With - negotiable" }
                });

            migrationBuilder.InsertData(
                table: "SelectValueMappings",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "ModifiedAt", "ModifiedBy", "SelectValueCategoryId", "SelectValueId" },
                values: new object[,]
                {
                    { new Guid("58a0d16f-2dac-4836-930e-1dd320430ef4"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("9c6b7ba0-f672-433f-b1e3-a80a2eb0a3ea"), new Guid("3c014654-b4c9-4c7a-a251-ae88ad504c8a") },
                    { new Guid("459dc1ea-de92-4a26-9b7b-848d67154cae"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("9c6b7ba0-f672-433f-b1e3-a80a2eb0a3ea"), new Guid("dec26aef-f0de-4c9f-a164-e23e2543c987") },
                    { new Guid("2fbb99cd-d14a-4b7c-b7fb-9b676f961e2c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("9c6b7ba0-f672-433f-b1e3-a80a2eb0a3ea"), new Guid("d53b4a35-f472-42a1-ab22-c7afb1e7d77e") },
                    { new Guid("d80bf2be-de2f-4d72-ba02-6081b5ba77d2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("9c6b7ba0-f672-433f-b1e3-a80a2eb0a3ea"), new Guid("b67d1ac5-80ec-4b7d-bcb8-72e3da55f201") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MusicianProfiles_SalaryId",
                table: "MusicianProfiles",
                column: "SalaryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_SelectValueMappings_EmolumentId",
                table: "Appointments",
                column: "EmolumentId",
                principalTable: "SelectValueMappings",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_SelectValueMappings_EmolumentPatternId",
                table: "Appointments",
                column: "EmolumentPatternId",
                principalTable: "SelectValueMappings",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_SelectValueMappings_ExpectationId",
                table: "Appointments",
                column: "ExpectationId",
                principalTable: "SelectValueMappings",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_SelectValueMappings_StatusId",
                table: "Appointments",
                column: "StatusId",
                principalTable: "SelectValueMappings",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_MusicianProfiles_SelectValueMappings_QualificationId",
                table: "MusicianProfiles",
                column: "QualificationId",
                principalTable: "SelectValueMappings",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_MusicianProfiles_SelectValueMappings_SalaryId",
                table: "MusicianProfiles",
                column: "SalaryId",
                principalTable: "SelectValueMappings",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_Sections_ParentId",
                table: "Sections",
                column: "ParentId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_SelectValueMappings_EmolumentId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_SelectValueMappings_EmolumentPatternId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_SelectValueMappings_ExpectationId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_SelectValueMappings_StatusId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_MusicianProfiles_SelectValueMappings_QualificationId",
                table: "MusicianProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_MusicianProfiles_SelectValueMappings_SalaryId",
                table: "MusicianProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_Sections_ParentId",
                table: "Sections");

            migrationBuilder.DropIndex(
                name: "IX_MusicianProfiles_SalaryId",
                table: "MusicianProfiles");

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("20f9698c-2f3d-41fd-9f33-1feeababfb1c"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("2fbb99cd-d14a-4b7c-b7fb-9b676f961e2c"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("30f592f6-485a-468a-bfb2-4854be733e74"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("42d76464-4b4b-4884-b8e3-1f69baaced4c"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("459dc1ea-de92-4a26-9b7b-848d67154cae"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("58a0d16f-2dac-4836-930e-1dd320430ef4"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("6304b935-633d-4bba-a90f-9bd864c867c6"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("d80bf2be-de2f-4d72-ba02-6081b5ba77d2"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("f036bca9-95d4-4526-b845-fff9208ab103"));

            migrationBuilder.DeleteData(
                table: "SelectValueCategories",
                keyColumn: "Id",
                keyValue: new Guid("9c6b7ba0-f672-433f-b1e3-a80a2eb0a3ea"));

            migrationBuilder.DeleteData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("3c014654-b4c9-4c7a-a251-ae88ad504c8a"));

            migrationBuilder.DeleteData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("d53b4a35-f472-42a1-ab22-c7afb1e7d77e"));

            migrationBuilder.DeleteData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("dec26aef-f0de-4c9f-a164-e23e2543c987"));

            migrationBuilder.DropColumn(
                name: "IsMainProfile",
                table: "MusicianProfiles");

            migrationBuilder.DropColumn(
                name: "LevelInnerASsessment",
                table: "MusicianProfiles");

            migrationBuilder.DropColumn(
                name: "LevelSelfAssessment",
                table: "MusicianProfiles");

            migrationBuilder.DropColumn(
                name: "ProfileFavorizitation",
                table: "MusicianProfiles");

            migrationBuilder.DropColumn(
                name: "SalaryId",
                table: "MusicianProfiles");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_SelectValueMappings_EmolumentId",
                table: "Appointments",
                column: "EmolumentId",
                principalTable: "SelectValueMappings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_SelectValueMappings_EmolumentPatternId",
                table: "Appointments",
                column: "EmolumentPatternId",
                principalTable: "SelectValueMappings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_SelectValueMappings_ExpectationId",
                table: "Appointments",
                column: "ExpectationId",
                principalTable: "SelectValueMappings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_SelectValueMappings_StatusId",
                table: "Appointments",
                column: "StatusId",
                principalTable: "SelectValueMappings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MusicianProfiles_SelectValueMappings_QualificationId",
                table: "MusicianProfiles",
                column: "QualificationId",
                principalTable: "SelectValueMappings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_Sections_ParentId",
                table: "Sections",
                column: "ParentId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
