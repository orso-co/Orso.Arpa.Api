using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orso.Arpa.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddAppointmentType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "type",
                table: "appointments",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "Default");

            migrationBuilder.CreateIndex(
                name: "ix_appointments_type",
                table: "appointments",
                column: "type");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_appointments_type",
                table: "appointments");

            migrationBuilder.DropColumn(
                name: "type",
                table: "appointments");
        }
    }
}
