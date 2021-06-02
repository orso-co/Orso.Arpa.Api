using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Orso.Arpa.Persistence.Migrations
{
    public partial class SectionValueMappingSortOrderAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "sort_order",
                table: "select_value_mappings",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("29e1142f-aa9e-4b94-ae21-9a63f7b65c15"),
                column: "sort_order",
                value: 30);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("5578f637-14b7-4c11-85a8-0b94d83da678"),
                column: "sort_order",
                value: 30);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("8daa5ae4-3885-4739-803a-693c7cfdf314"),
                column: "sort_order",
                value: 30);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("d733e38d-1d80-4054-b654-4ea4a128b0a8"),
                column: "sort_order",
                value: 10);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "sort_order",
                table: "select_value_mappings");
        }
    }
}
