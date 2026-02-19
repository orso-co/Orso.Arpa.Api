using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orso.Arpa.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddLastVisitedAtToDashboardLayout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "last_visited_at",
                table: "user_dashboard_layouts",
                type: "timestamp without time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "last_visited_at",
                table: "user_dashboard_layouts");
        }
    }
}
