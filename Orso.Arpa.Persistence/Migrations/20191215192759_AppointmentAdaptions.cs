using Microsoft.EntityFrameworkCore.Migrations;

namespace Orso.Arpa.Persistence.Migrations
{
    public partial class AppointmentAdaptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Table",
                table: "SelectValueCategories",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Property",
                table: "SelectValueCategories",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "Projects",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_SelectValueCategories_Property",
                table: "SelectValueCategories",
                column: "Property");

            migrationBuilder.CreateIndex(
                name: "IX_SelectValueCategories_Table",
                table: "SelectValueCategories",
                column: "Table");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SelectValueCategories_Property",
                table: "SelectValueCategories");

            migrationBuilder.DropIndex(
                name: "IX_SelectValueCategories_Table",
                table: "SelectValueCategories");

            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "Projects");

            migrationBuilder.AlterColumn<string>(
                name: "Table",
                table: "SelectValueCategories",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Property",
                table: "SelectValueCategories",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
