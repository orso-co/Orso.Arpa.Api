using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Orso.Arpa.Persistence.Migrations
{
    public partial class AddedInqueryToMusicianProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Background",
                table: "MusicianProfiles",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "ExperienceLevel",
                table: "MusicianProfiles",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<Guid>(
                name: "InqueryId",
                table: "MusicianProfiles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.InsertData(
                table: "SelectValueCategories",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "ModifiedAt", "ModifiedBy", "Name", "Property", "Table" },
                values: new object[] { new Guid("d1ca913c-dee7-46d8-9fd4-ea564af8005f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Inquery", "Inquery", "MusicianProfile" });

            migrationBuilder.InsertData(
                table: "SelectValues",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "Description", "ModifiedAt", "ModifiedBy", "Name" },
                values: new object[,]
                {
                    { new Guid("1f0e9a86-4641-4d7e-8413-a1beba0e8afb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Gladly" },
                    { new Guid("5850e103-4ac9-472e-85f2-cddc08732ccc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Emergency only" },
                    { new Guid("5db547d6-c115-4409-8db7-59374ca2af83"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Never again" },
                    { new Guid("0d1073cd-f6d5-4572-87ac-98ab6f15c05a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "For contacts only" }
                });

            migrationBuilder.InsertData(
                table: "SelectValueMappings",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "ModifiedAt", "ModifiedBy", "SelectValueCategoryId", "SelectValueId" },
                values: new object[,]
                {
                    { new Guid("68e947c0-9450-4b64-90d7-553850396a3f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("d1ca913c-dee7-46d8-9fd4-ea564af8005f"), new Guid("1f0e9a86-4641-4d7e-8413-a1beba0e8afb") },
                    { new Guid("60c1a391-59b4-4cea-ba83-59e09f7512b6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("d1ca913c-dee7-46d8-9fd4-ea564af8005f"), new Guid("5850e103-4ac9-472e-85f2-cddc08732ccc") },
                    { new Guid("ab5c5904-2683-47c4-a436-319303b8e62f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("d1ca913c-dee7-46d8-9fd4-ea564af8005f"), new Guid("5db547d6-c115-4409-8db7-59374ca2af83") },
                    { new Guid("a15014ad-582e-4388-9b58-aceb52cf41bf"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("d1ca913c-dee7-46d8-9fd4-ea564af8005f"), new Guid("b67d1ac5-80ec-4b7d-bcb8-72e3da55f201") },
                    { new Guid("90b5cfa9-890b-4b89-a750-646f3a26db23"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("d1ca913c-dee7-46d8-9fd4-ea564af8005f"), new Guid("0d1073cd-f6d5-4572-87ac-98ab6f15c05a") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MusicianProfiles_InqueryId",
                table: "MusicianProfiles",
                column: "InqueryId");

            migrationBuilder.AddForeignKey(
                name: "FK_MusicianProfiles_SelectValueMappings_InqueryId",
                table: "MusicianProfiles",
                column: "InqueryId",
                principalTable: "SelectValueMappings",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MusicianProfiles_SelectValueMappings_InqueryId",
                table: "MusicianProfiles");

            migrationBuilder.DropIndex(
                name: "IX_MusicianProfiles_InqueryId",
                table: "MusicianProfiles");

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("60c1a391-59b4-4cea-ba83-59e09f7512b6"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("68e947c0-9450-4b64-90d7-553850396a3f"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("90b5cfa9-890b-4b89-a750-646f3a26db23"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("a15014ad-582e-4388-9b58-aceb52cf41bf"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("ab5c5904-2683-47c4-a436-319303b8e62f"));

            migrationBuilder.DeleteData(
                table: "SelectValueCategories",
                keyColumn: "Id",
                keyValue: new Guid("d1ca913c-dee7-46d8-9fd4-ea564af8005f"));

            migrationBuilder.DeleteData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("0d1073cd-f6d5-4572-87ac-98ab6f15c05a"));

            migrationBuilder.DeleteData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("1f0e9a86-4641-4d7e-8413-a1beba0e8afb"));

            migrationBuilder.DeleteData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("5850e103-4ac9-472e-85f2-cddc08732ccc"));

            migrationBuilder.DeleteData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("5db547d6-c115-4409-8db7-59374ca2af83"));

            migrationBuilder.DropColumn(
                name: "Background",
                table: "MusicianProfiles");

            migrationBuilder.DropColumn(
                name: "ExperienceLevel",
                table: "MusicianProfiles");

            migrationBuilder.DropColumn(
                name: "InqueryId",
                table: "MusicianProfiles");
        }
    }
}
