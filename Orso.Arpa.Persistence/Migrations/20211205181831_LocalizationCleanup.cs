using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orso.Arpa.Persistence.Migrations
{
    public partial class LocalizationCleanup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("563c7c31-3976-43e0-ac08-e8251004d647"),
                columns: new[] { "modified_at", "modified_by" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("631a8511-ae67-43c2-acfe-c8938e81e105"),
                columns: new[] { "modified_at", "modified_by" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("676b04c8-4256-4008-9fc4-be62ea8f5dd0"),
                columns: new[] { "modified_at", "modified_by" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("6aed06d4-2c86-414d-bf15-bce230d4d0e3"),
                columns: new[] { "modified_at", "modified_by" },
                values: new object[] { null, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("563c7c31-3976-43e0-ac08-e8251004d647"),
                columns: new[] { "modified_at", "modified_by" },
                values: new object[] { new DateTime(2021, 11, 6, 14, 56, 26, 513, DateTimeKind.Local).AddTicks(4645), "LocalizationSeedData" });

            migrationBuilder.UpdateData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("631a8511-ae67-43c2-acfe-c8938e81e105"),
                columns: new[] { "modified_at", "modified_by" },
                values: new object[] { new DateTime(2021, 11, 6, 14, 56, 26, 505, DateTimeKind.Local).AddTicks(5667), "LocalizationSeedData" });

            migrationBuilder.UpdateData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("676b04c8-4256-4008-9fc4-be62ea8f5dd0"),
                columns: new[] { "modified_at", "modified_by" },
                values: new object[] { new DateTime(2021, 11, 6, 14, 56, 26, 514, DateTimeKind.Local).AddTicks(9100), "LocalizationSeedData" });

            migrationBuilder.UpdateData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("6aed06d4-2c86-414d-bf15-bce230d4d0e3"),
                columns: new[] { "modified_at", "modified_by" },
                values: new object[] { new DateTime(2021, 11, 6, 14, 56, 26, 510, DateTimeKind.Local).AddTicks(6036), "LocalizationSeedData" });
        }
    }
}
