using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Orso.Arpa.Persistence.Migrations
{
    public partial class RenameEmolumentToSalary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_appointments_select_value_mappings_emolument_id",
                table: "appointments");

            migrationBuilder.DropForeignKey(
                name: "fk_appointments_select_value_mappings_emolument_pattern_id",
                table: "appointments");

            migrationBuilder.RenameColumn(
                name: "emolument_pattern_id",
                table: "appointments",
                newName: "salary_pattern_id");

            migrationBuilder.RenameColumn(
                name: "emolument_id",
                table: "appointments",
                newName: "salary_id");

            migrationBuilder.RenameIndex(
                name: "ix_appointments_emolument_pattern_id",
                table: "appointments",
                newName: "ix_appointments_salary_pattern_id");

            migrationBuilder.RenameIndex(
                name: "ix_appointments_emolument_id",
                table: "appointments",
                newName: "ix_appointments_salary_id");

            migrationBuilder.UpdateData(
                table: "select_value_categories",
                keyColumn: "id",
                keyValue: new Guid("1d62ed51-c99e-4b55-83d7-f9f9a5b8234e"),
                columns: new[] { "name", "property" },
                values: new object[] { "Salary", "Salary" });

            migrationBuilder.UpdateData(
                table: "select_value_categories",
                keyColumn: "id",
                keyValue: new Guid("e4ff93b9-318e-41ed-b067-51ee94f41adf"),
                columns: new[] { "name", "property" },
                values: new object[] { "Salary Pattern", "SalaryPattern" });

            migrationBuilder.AddForeignKey(
                name: "fk_appointments_select_value_mappings_salary_id",
                table: "appointments",
                column: "salary_id",
                principalTable: "select_value_mappings",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_appointments_select_value_mappings_salary_pattern_id",
                table: "appointments",
                column: "salary_pattern_id",
                principalTable: "select_value_mappings",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_appointments_select_value_mappings_salary_id",
                table: "appointments");

            migrationBuilder.DropForeignKey(
                name: "fk_appointments_select_value_mappings_salary_pattern_id",
                table: "appointments");

            migrationBuilder.RenameColumn(
                name: "salary_pattern_id",
                table: "appointments",
                newName: "emolument_pattern_id");

            migrationBuilder.RenameColumn(
                name: "salary_id",
                table: "appointments",
                newName: "emolument_id");

            migrationBuilder.RenameIndex(
                name: "ix_appointments_salary_pattern_id",
                table: "appointments",
                newName: "ix_appointments_emolument_pattern_id");

            migrationBuilder.RenameIndex(
                name: "ix_appointments_salary_id",
                table: "appointments",
                newName: "ix_appointments_emolument_id");

            migrationBuilder.UpdateData(
                table: "select_value_categories",
                keyColumn: "id",
                keyValue: new Guid("1d62ed51-c99e-4b55-83d7-f9f9a5b8234e"),
                columns: new[] { "name", "property" },
                values: new object[] { "Emolument", "Emolument" });

            migrationBuilder.UpdateData(
                table: "select_value_categories",
                keyColumn: "id",
                keyValue: new Guid("e4ff93b9-318e-41ed-b067-51ee94f41adf"),
                columns: new[] { "name", "property" },
                values: new object[] { "Emolument Pattern", "EmolumentPattern" });

            migrationBuilder.AddForeignKey(
                name: "fk_appointments_select_value_mappings_emolument_id",
                table: "appointments",
                column: "emolument_id",
                principalTable: "select_value_mappings",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_appointments_select_value_mappings_emolument_pattern_id",
                table: "appointments",
                column: "emolument_pattern_id",
                principalTable: "select_value_mappings",
                principalColumn: "id");
        }
    }
}
