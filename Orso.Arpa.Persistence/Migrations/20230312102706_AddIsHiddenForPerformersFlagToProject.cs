using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orso.Arpa.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddIsHiddenForPerformersFlagToProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_hidden_for_performers",
                table: "projects",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_hidden_for_performers",
                table: "projects");
        }
    }
}
