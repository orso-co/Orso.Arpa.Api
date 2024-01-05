using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orso.Arpa.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RoomEquipmentOptimizations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "count",
                table: "room_section");

            migrationBuilder.DropColumn(
                name: "count",
                table: "room_equipment");

            migrationBuilder.AddColumn<int>(
                name: "quantity",
                table: "room_section",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "quantity",
                table: "room_equipment",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "quantity",
                table: "room_section");

            migrationBuilder.DropColumn(
                name: "quantity",
                table: "room_equipment");

            migrationBuilder.AddColumn<int>(
                name: "count",
                table: "room_section",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "count",
                table: "room_equipment",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
