using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Orso.Arpa.Persistence.Migrations
{
    public partial class AddedPropertiesToProjects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Projects",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Number",
                table: "Projects",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Projects",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShortTitle",
                table: "Projects",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Projects",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StateId",
                table: "Projects",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TypeId",
                table: "Projects",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.InsertData(
                table: "SelectValueCategories",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "ModifiedAt", "ModifiedBy", "Name", "Property", "Table" },
                values: new object[,]
                {
                    { new Guid("53ed1791-36d7-4534-867c-15175e6f4584"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Type", "Type", "Project" },
                    { new Guid("9804d695-d8c7-40bd-814f-8458b55fb583"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "State", "State", "Project" }
                });

            migrationBuilder.InsertData(
                table: "SelectValueMappings",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "ModifiedAt", "ModifiedBy", "SelectValueCategoryId", "SelectValueId" },
                values: new object[,]
                {
                    { new Guid("34f05f05-ef23-4f36-94e7-73b917530c51"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("4649b6b9-1362-41c2-ac5c-884f216dff30"), new Guid("71779748-6d3c-496a-9842-8dc508de6676") },
                    { new Guid("ae2f10ff-39ae-427e-a5e8-ddcd89422d44"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("4649b6b9-1362-41c2-ac5c-884f216dff30"), new Guid("5d50c5c3-e85a-4810-ac46-49572e1ca2f5") },
                    { new Guid("44710a6b-93c0-4aac-b552-e0423f1b106a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("4649b6b9-1362-41c2-ac5c-884f216dff30"), new Guid("79de43be-57cc-484f-bff2-57f3ba78dbe9") },
                    { new Guid("3f166c3c-c85e-404b-aad3-c8996f4fb75f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("4649b6b9-1362-41c2-ac5c-884f216dff30"), new Guid("130f63c3-9d2f-4301-b062-236c78663e3b") }
                });

            migrationBuilder.InsertData(
                table: "SelectValues",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "Description", "ModifiedAt", "ModifiedBy", "Name" },
                values: new object[,]
                {
                    { new Guid("33bbdccf-59a9-4b05-bdac-af50137cecb3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Cancelled" },
                    { new Guid("bd0f37e1-ec14-4d87-8380-117b4480d7a4"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Postponed" },
                    { new Guid("425f1526-0513-4535-bdd8-47632d82956f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Archived" },
                    { new Guid("7f6b69f3-4fe8-4b0c-a586-38a661c60af5"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Concert tour" },
                    { new Guid("63a6b9a9-30a8-4cdb-983b-336b587069cb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Rehearsal weekend" },
                    { new Guid("f2a6ef3d-bb32-4505-83a5-2cb9f611ce0f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Special project" },
                    { new Guid("52fad37d-23a7-4515-9b77-3ee3bda03b9a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "CD recording" },
                    { new Guid("95de5380-4027-4b73-b4db-3697aba5ba38"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Contest" }
                });

            migrationBuilder.InsertData(
                table: "SelectValueMappings",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "ModifiedAt", "ModifiedBy", "SelectValueCategoryId", "SelectValueId" },
                values: new object[,]
                {
                    { new Guid("7f76d426-cab7-4f4f-aba3-bd430bcec003"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("4649b6b9-1362-41c2-ac5c-884f216dff30"), new Guid("7f6b69f3-4fe8-4b0c-a586-38a661c60af5") },
                    { new Guid("d8f337d0-84fc-4a4d-b75c-fbe2208808ea"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("4649b6b9-1362-41c2-ac5c-884f216dff30"), new Guid("63a6b9a9-30a8-4cdb-983b-336b587069cb") },
                    { new Guid("574e0c4b-cbb3-4750-926b-3df4c377fc5e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("4649b6b9-1362-41c2-ac5c-884f216dff30"), new Guid("f2a6ef3d-bb32-4505-83a5-2cb9f611ce0f") },
                    { new Guid("679116ec-7840-4c6d-bb45-fa2d89d6e779"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("4649b6b9-1362-41c2-ac5c-884f216dff30"), new Guid("52fad37d-23a7-4515-9b77-3ee3bda03b9a") },
                    { new Guid("5c3f5e18-7afd-4404-98db-658e852901dc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("4649b6b9-1362-41c2-ac5c-884f216dff30"), new Guid("95de5380-4027-4b73-b4db-3697aba5ba38") },
                    { new Guid("725a4f4a-37cb-46ba-93a3-7b9cc2b015cb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("9804d695-d8c7-40bd-814f-8458b55fb583"), new Guid("362efd25-e1d2-496d-b7fa-884371a58682") },
                    { new Guid("b793fa86-2025-4258-8993-8045f4c312d7"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("9804d695-d8c7-40bd-814f-8458b55fb583"), new Guid("34a52363-4a57-4019-abcf-0c9880246891") },
                    { new Guid("65975857-ab27-480d-87c3-dba74d45cb63"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("9804d695-d8c7-40bd-814f-8458b55fb583"), new Guid("33bbdccf-59a9-4b05-bdac-af50137cecb3") },
                    { new Guid("bc29bf0a-2ebb-4db8-8765-a5f835492552"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("9804d695-d8c7-40bd-814f-8458b55fb583"), new Guid("bd0f37e1-ec14-4d87-8380-117b4480d7a4") },
                    { new Guid("75f2d1c3-4ba2-4acc-8fd3-6b01ca66d1c9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("9804d695-d8c7-40bd-814f-8458b55fb583"), new Guid("425f1526-0513-4535-bdd8-47632d82956f") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_Number",
                table: "Projects",
                column: "Number");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_StateId",
                table: "Projects",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_TypeId",
                table: "Projects",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_SelectValueMappings_StateId",
                table: "Projects",
                column: "StateId",
                principalTable: "SelectValueMappings",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_SelectValueMappings_TypeId",
                table: "Projects",
                column: "TypeId",
                principalTable: "SelectValueMappings",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_SelectValueMappings_StateId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_SelectValueMappings_TypeId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_Number",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_StateId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_TypeId",
                table: "Projects");

            migrationBuilder.DeleteData(
                table: "SelectValueCategories",
                keyColumn: "Id",
                keyValue: new Guid("53ed1791-36d7-4534-867c-15175e6f4584"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("34f05f05-ef23-4f36-94e7-73b917530c51"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("3f166c3c-c85e-404b-aad3-c8996f4fb75f"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("44710a6b-93c0-4aac-b552-e0423f1b106a"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("574e0c4b-cbb3-4750-926b-3df4c377fc5e"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("5c3f5e18-7afd-4404-98db-658e852901dc"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("65975857-ab27-480d-87c3-dba74d45cb63"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("679116ec-7840-4c6d-bb45-fa2d89d6e779"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("725a4f4a-37cb-46ba-93a3-7b9cc2b015cb"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("75f2d1c3-4ba2-4acc-8fd3-6b01ca66d1c9"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("7f76d426-cab7-4f4f-aba3-bd430bcec003"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("ae2f10ff-39ae-427e-a5e8-ddcd89422d44"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("b793fa86-2025-4258-8993-8045f4c312d7"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("bc29bf0a-2ebb-4db8-8765-a5f835492552"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("d8f337d0-84fc-4a4d-b75c-fbe2208808ea"));

            migrationBuilder.DeleteData(
                table: "SelectValueCategories",
                keyColumn: "Id",
                keyValue: new Guid("9804d695-d8c7-40bd-814f-8458b55fb583"));

            migrationBuilder.DeleteData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("33bbdccf-59a9-4b05-bdac-af50137cecb3"));

            migrationBuilder.DeleteData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("425f1526-0513-4535-bdd8-47632d82956f"));

            migrationBuilder.DeleteData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("52fad37d-23a7-4515-9b77-3ee3bda03b9a"));

            migrationBuilder.DeleteData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("63a6b9a9-30a8-4cdb-983b-336b587069cb"));

            migrationBuilder.DeleteData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("7f6b69f3-4fe8-4b0c-a586-38a661c60af5"));

            migrationBuilder.DeleteData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("95de5380-4027-4b73-b4db-3697aba5ba38"));

            migrationBuilder.DeleteData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("bd0f37e1-ec14-4d87-8380-117b4480d7a4"));

            migrationBuilder.DeleteData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("f2a6ef3d-bb32-4505-83a5-2cb9f611ce0f"));

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ShortTitle",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Projects");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Projects",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Number",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15,
                oldNullable: true);
        }
    }
}
