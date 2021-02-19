using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Orso.Arpa.Persistence.Migrations
{
    public partial class AddedSectionsToSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("a19fa9af-dcba-48e3-bc21-be2130fa528c"));

            migrationBuilder.InsertData(
                table: "Sections",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "ModifiedAt", "ModifiedBy", "Name", "ParentId" },
                values: new object[,]
                {
                    { new Guid("8bba816f-2315-43c0-b18e-99a27b1c9668"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Performers", null },
                    { new Guid("067647c0-3f25-449e-9212-03f39fa88f0f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Volunteers", null },
                    { new Guid("f6af00f5-e81c-4d85-aadd-1e33748e9a64"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Visitors", null },
                    { new Guid("182019da-bde2-44d7-8c77-88cfb0ce428c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Crew", null }
                });

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("308659d6-6014-4d2c-a62a-be75bf202e62"),
                column: "ParentId",
                value: new Guid("8bba816f-2315-43c0-b18e-99a27b1c9668"));

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("c2cfb7a0-4981-4dda-b988-8ba74957f6a4"),
                column: "ParentId",
                value: new Guid("8bba816f-2315-43c0-b18e-99a27b1c9668"));

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("e0fdb057-c9b7-4477-be75-cbf920a26af6"),
                columns: new[] { "Name", "ParentId" },
                values: new object[] { "Soloists", new Guid("8bba816f-2315-43c0-b18e-99a27b1c9668") });

            migrationBuilder.InsertData(
                table: "Sections",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "ModifiedAt", "ModifiedBy", "Name", "ParentId" },
                values: new object[,]
                {
                    { new Guid("1994cb6c-877e-4d7c-aeca-26e68967c2ab"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Band", new Guid("8bba816f-2315-43c0-b18e-99a27b1c9668") },
                    { new Guid("8ed82e0e-0354-4192-8f26-5a2437e9208d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Stage", new Guid("182019da-bde2-44d7-8c77-88cfb0ce428c") },
                    { new Guid("0cf93477-f42f-46c3-8e3d-45ccdc54ad8c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Media", new Guid("182019da-bde2-44d7-8c77-88cfb0ce428c") },
                    { new Guid("bc6cfeb7-569d-4c22-8e80-647aed560bf0"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Sound", new Guid("182019da-bde2-44d7-8c77-88cfb0ce428c") },
                    { new Guid("614a8fd0-acfa-4268-b716-3b35a6a17b7a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Light", new Guid("182019da-bde2-44d7-8c77-88cfb0ce428c") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("067647c0-3f25-449e-9212-03f39fa88f0f"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("0cf93477-f42f-46c3-8e3d-45ccdc54ad8c"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("1994cb6c-877e-4d7c-aeca-26e68967c2ab"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("614a8fd0-acfa-4268-b716-3b35a6a17b7a"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("8ed82e0e-0354-4192-8f26-5a2437e9208d"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("bc6cfeb7-569d-4c22-8e80-647aed560bf0"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("f6af00f5-e81c-4d85-aadd-1e33748e9a64"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("182019da-bde2-44d7-8c77-88cfb0ce428c"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("8bba816f-2315-43c0-b18e-99a27b1c9668"));

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("308659d6-6014-4d2c-a62a-be75bf202e62"),
                column: "ParentId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("c2cfb7a0-4981-4dda-b988-8ba74957f6a4"),
                column: "ParentId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("e0fdb057-c9b7-4477-be75-cbf920a26af6"),
                columns: new[] { "Name", "ParentId" },
                values: new object[] { "Soloist", null });

            migrationBuilder.InsertData(
                table: "Sections",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "ModifiedAt", "ModifiedBy", "Name", "ParentId" },
                values: new object[] { new Guid("a19fa9af-dcba-48e3-bc21-be2130fa528c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Miscellaneous", null });
        }
    }
}
