using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Orso.Arpa.Persistence.Migrations
{
    public partial class SeedPersons : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "GivenName", "ModifiedAt", "ModifiedBy", "Surname" },
                values: new object[] { new Guid("56ed7c20-ba78-4a02-936e-5e840ef0748c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Initial", null, null, "Admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: new Guid("56ed7c20-ba78-4a02-936e-5e840ef0748c"));
        }
    }
}
