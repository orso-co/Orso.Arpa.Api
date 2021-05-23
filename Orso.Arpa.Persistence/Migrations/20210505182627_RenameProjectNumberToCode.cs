using Microsoft.EntityFrameworkCore.Migrations;

namespace Orso.Arpa.Persistence.Migrations
{
    public partial class RenameProjectNumberToCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "number",
                table: "projects",
                newName: "code");

            migrationBuilder.RenameIndex(
                name: "ix_projects_number",
                table: "projects",
                newName: "ix_projects_code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "code",
                table: "projects",
                newName: "number");

            migrationBuilder.RenameIndex(
                name: "ix_projects_code",
                table: "projects",
                newName: "ix_projects_number");
        }
    }
}
