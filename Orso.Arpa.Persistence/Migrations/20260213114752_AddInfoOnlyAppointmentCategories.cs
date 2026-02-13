using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Orso.Arpa.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddInfoOnlyAppointmentCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "select_values",
                columns: new[] { "id", "created_at", "created_by", "deleted", "description", "modified_at", "modified_by", "name" },
                values: new object[,]
                {
                    { new Guid("e0000001-0001-4000-8000-000000000001"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Info" },
                    { new Guid("e0000001-0002-4000-8000-000000000002"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Deadline" },
                    { new Guid("e0000001-0003-4000-8000-000000000003"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Public Holiday" },
                    { new Guid("e0000001-0004-4000-8000-000000000004"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Vacation" }
                });

            migrationBuilder.InsertData(
                table: "select_value_mappings",
                columns: new[] { "id", "created_at", "created_by", "deleted", "modified_at", "modified_by", "select_value_category_id", "select_value_id", "sort_order" },
                values: new object[,]
                {
                    { new Guid("e0000001-0001-4000-8000-000000000011"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("791c7439-c72a-47ca-ad8d-193e2cad09e1"), new Guid("e0000001-0001-4000-8000-000000000001"), 100 },
                    { new Guid("e0000001-0002-4000-8000-000000000012"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("791c7439-c72a-47ca-ad8d-193e2cad09e1"), new Guid("e0000001-0002-4000-8000-000000000002"), 105 },
                    { new Guid("e0000001-0003-4000-8000-000000000013"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("791c7439-c72a-47ca-ad8d-193e2cad09e1"), new Guid("e0000001-0003-4000-8000-000000000003"), 110 },
                    { new Guid("e0000001-0004-4000-8000-000000000014"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("791c7439-c72a-47ca-ad8d-193e2cad09e1"), new Guid("e0000001-0004-4000-8000-000000000004"), 115 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("e0000001-0001-4000-8000-000000000011"));

            migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("e0000001-0002-4000-8000-000000000012"));

            migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("e0000001-0003-4000-8000-000000000013"));

            migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("e0000001-0004-4000-8000-000000000014"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("e0000001-0001-4000-8000-000000000001"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("e0000001-0002-4000-8000-000000000002"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("e0000001-0003-4000-8000-000000000003"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("e0000001-0004-4000-8000-000000000004"));
        }
    }
}
