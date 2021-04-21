using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Orso.Arpa.Persistence.Migrations
{
    public partial class DisplayNameAsReadonlyProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<short>(
                name: "Level",
                table: "AspNetRoles",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "ModifiedAt", "ModifiedBy", "Name" },
                values: new object[] { new Guid("3e6c559e-8d50-488d-a1ea-5dbc0f44ba9b"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, "Freiburg" });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "ModifiedAt", "ModifiedBy", "Name" },
                values: new object[] { new Guid("ac9544e3-e756-486c-a1dc-62988a882ac2"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, "Stuttgart" });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "ModifiedAt", "ModifiedBy", "Name" },
                values: new object[] { new Guid("ca3c9cce-1aee-4c50-93e1-be963542741a"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, "Berlin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("3e6c559e-8d50-488d-a1ea-5dbc0f44ba9b"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("ac9544e3-e756-486c-a1dc-62988a882ac2"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("ca3c9cce-1aee-4c50-93e1-be963542741a"));

            migrationBuilder.DropColumn(
                name: "Level",
                table: "AspNetRoles");

            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "AspNetUsers",
                nullable: true);
        }
    }
}
