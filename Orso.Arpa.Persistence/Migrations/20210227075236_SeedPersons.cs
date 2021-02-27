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
                values: new object[] { new Guid("8d960214-8f1b-4b69-8734-543aad67581c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Ad", null, null, "Min" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: new Guid("8d960214-8f1b-4b69-8734-543aad67581c"));
        }
    }
}
