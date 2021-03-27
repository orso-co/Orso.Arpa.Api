using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Orso.Arpa.Persistence.Migrations
{
    public partial class AddedUrlsToProjects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UrlId",
                table: "AspNetRoles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Url",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Href = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    AnchorText = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Url", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_UrlId",
                table: "AspNetRoles",
                column: "UrlId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_Url_UrlId",
                table: "AspNetRoles",
                column: "UrlId",
                principalTable: "Url",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_Url_UrlId",
                table: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Url");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoles_UrlId",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "UrlId",
                table: "AspNetRoles");
        }
    }
}
