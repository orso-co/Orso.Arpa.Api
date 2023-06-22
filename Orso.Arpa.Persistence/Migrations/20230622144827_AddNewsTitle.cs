using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orso.Arpa.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddNewsTitle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "news_title",
                table: "news",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "news_title",
                table: "news");
        }
    }
}
