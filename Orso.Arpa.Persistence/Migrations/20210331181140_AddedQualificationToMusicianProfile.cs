using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Orso.Arpa.Persistence.Migrations
{
    public partial class AddedQualificationToMusicianProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MusicianProfiles_Sections_SectionId",
                table: "MusicianProfiles");

            migrationBuilder.RenameColumn(
                name: "SectionId",
                table: "MusicianProfiles",
                newName: "InstrumentId");

            migrationBuilder.RenameIndex(
                name: "IX_MusicianProfiles_SectionId",
                table: "MusicianProfiles",
                newName: "IX_MusicianProfiles_InstrumentId");

            migrationBuilder.AddColumn<bool>(
                name: "IsInstrument",
                table: "Sections",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "QualificationId",
                table: "MusicianProfiles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("0031e6f5-2d51-4e88-9e82-7bd2c8340cac"),
                column: "IsInstrument",
                value: true);

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("08bc313b-d0dd-4b78-bdbf-d976682d965e"),
                column: "IsInstrument",
                value: true);

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("0cf93477-f42f-46c3-8e3d-45ccdc54ad8c"),
                column: "IsInstrument",
                value: true);

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("1579d7e7-4f55-4532-a078-69fd1ec939da"),
                column: "IsInstrument",
                value: true);

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("18cbded8-0d64-4e0e-bc19-d6903e0fd5a9"),
                column: "IsInstrument",
                value: true);

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("205b0a0e-1a36-48e9-8b45-df37dc5effa5"),
                column: "IsInstrument",
                value: true);

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("2327a9c3-2c6f-41b7-9045-bb00af798b42"),
                column: "IsInstrument",
                value: true);

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("454c2ad6-e3c8-428a-b74e-c73873159c0e"),
                column: "IsInstrument",
                value: true);

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("48833c1b-cbc1-43b2-a4c5-f1fa4289f5ab"),
                column: "IsInstrument",
                value: true);

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("554fd3db-110b-4335-bc2a-1d5070f6621a"),
                column: "IsInstrument",
                value: true);

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("566260fb-b6be-41dc-956d-4070d30fa88d"),
                column: "IsInstrument",
                value: true);

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("5c14f673-13f2-488f-8c21-7286d3ee10c3"),
                column: "IsInstrument",
                value: true);

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("614a8fd0-acfa-4268-b716-3b35a6a17b7a"),
                column: "IsInstrument",
                value: true);

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("76891771-b5f2-4666-8972-ba7f494fc9de"),
                column: "IsInstrument",
                value: true);

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("7daa1394-a70d-4a24-88a6-ccf511d75c4d"),
                column: "IsInstrument",
                value: true);

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("7f811b88-e7db-461a-af5d-e249b1ce9e7d"),
                column: "IsInstrument",
                value: true);

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("8903b8c5-0ef8-48fd-9c2b-71fbae827965"),
                column: "IsInstrument",
                value: true);

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("a06431be-f9d6-44dc-8fdb-fbf8aa2bb940"),
                column: "IsInstrument",
                value: true);

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("a22b6f19-3e9c-4389-824b-22db7b8cf8fd"),
                column: "IsInstrument",
                value: true);

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("b9532add-efec-4510-831c-902c32ef7dbb"),
                column: "IsInstrument",
                value: true);

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("bb647161-8394-47d3-9f43-825762a70fc2"),
                column: "IsInstrument",
                value: true);

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("c15c3649-d7bb-4bbf-bdd3-f6146ebc825c"),
                column: "IsInstrument",
                value: true);

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("cdc390d5-0649-441d-a086-df2e3b9d3512"),
                column: "IsInstrument",
                value: true);

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("d12ebc93-4b55-455c-a9db-a826fca9a1f2"),
                column: "IsInstrument",
                value: true);

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("d6961f83-e792-4ddf-b91a-ae0867caeb3b"),
                column: "IsInstrument",
                value: true);

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("d787fe9a-2283-43f6-bbc8-8a098e1f1c81"),
                column: "IsInstrument",
                value: true);

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("d7ff1f62-e5c5-4662-823b-f77ff7706b4e"),
                column: "IsInstrument",
                value: true);

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("d8686f68-78da-4022-b0b8-97e0c263d694"),
                column: "IsInstrument",
                value: true);

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("df541ea1-a5fd-4975-b6fd-7cd652a79073"),
                column: "IsInstrument",
                value: true);

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("e20ce055-5715-42f4-97e6-4025559b15f7"),
                column: "IsInstrument",
                value: true);

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("e45ec6fa-7595-4084-9e01-991746b7f5e9"),
                column: "IsInstrument",
                value: true);

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("e7dd10ef-1c39-4440-9a6c-65d397f010ca"),
                column: "IsInstrument",
                value: true);

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("ea916a8d-1bce-4e87-b5b0-ff6304bb01a5"),
                column: "IsInstrument",
                value: true);

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("eb42b2f7-413e-4c1a-ab79-23c74b02d054"),
                column: "IsInstrument",
                value: true);

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("fab9a49a-9fa4-4af3-9e40-e13bdc930513"),
                column: "IsInstrument",
                value: true);

            migrationBuilder.InsertData(
                table: "SelectValueCategories",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "ModifiedAt", "ModifiedBy", "Name", "Property", "Table" },
                values: new object[] { new Guid("9648daa0-c2b2-4b97-912b-7ce30b9534a8"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Qualification", "Qualification", "MusicianProfile" });

            migrationBuilder.InsertData(
                table: "SelectValues",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "Description", "ModifiedAt", "ModifiedBy", "Name" },
                values: new object[,]
                {
                    { new Guid("b67d1ac5-80ec-4b7d-bcb8-72e3da55f201"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Unknown" },
                    { new Guid("f52b9170-c6f6-4828-b96c-df5dfbe9bd73"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Professional" },
                    { new Guid("35d63f30-8704-47d5-865a-ee713a082433"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Semi-Professional" },
                    { new Guid("e20ff004-aafc-4e28-87f9-0d9c6372951c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Student" },
                    { new Guid("3f93768e-ac24-4741-9eb8-49d1e8e4a6e1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Amateur" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MusicianProfiles_QualificationId",
                table: "MusicianProfiles",
                column: "QualificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_MusicianProfiles_Sections_InstrumentId",
                table: "MusicianProfiles",
                column: "InstrumentId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MusicianProfiles_SelectValueMappings_QualificationId",
                table: "MusicianProfiles",
                column: "QualificationId",
                principalTable: "SelectValueMappings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MusicianProfiles_Sections_InstrumentId",
                table: "MusicianProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_MusicianProfiles_SelectValueMappings_QualificationId",
                table: "MusicianProfiles");

            migrationBuilder.DropIndex(
                name: "IX_MusicianProfiles_QualificationId",
                table: "MusicianProfiles");

            migrationBuilder.DeleteData(
                table: "SelectValueCategories",
                keyColumn: "Id",
                keyValue: new Guid("9648daa0-c2b2-4b97-912b-7ce30b9534a8"));

            migrationBuilder.DeleteData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("35d63f30-8704-47d5-865a-ee713a082433"));

            migrationBuilder.DeleteData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("3f93768e-ac24-4741-9eb8-49d1e8e4a6e1"));

            migrationBuilder.DeleteData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("b67d1ac5-80ec-4b7d-bcb8-72e3da55f201"));

            migrationBuilder.DeleteData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("e20ff004-aafc-4e28-87f9-0d9c6372951c"));

            migrationBuilder.DeleteData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("f52b9170-c6f6-4828-b96c-df5dfbe9bd73"));

            migrationBuilder.DropColumn(
                name: "IsInstrument",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "QualificationId",
                table: "MusicianProfiles");

            migrationBuilder.RenameColumn(
                name: "InstrumentId",
                table: "MusicianProfiles",
                newName: "SectionId");

            migrationBuilder.RenameIndex(
                name: "IX_MusicianProfiles_InstrumentId",
                table: "MusicianProfiles",
                newName: "IX_MusicianProfiles_SectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_MusicianProfiles_Sections_SectionId",
                table: "MusicianProfiles",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
