using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Orso.Arpa.Persistence.Migrations
{
    public partial class AddedVenueToAppointment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "VenueId",
                table: "Appointments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_VenueId",
                table: "Appointments",
                column: "VenueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Venues_VenueId",
                table: "Appointments",
                column: "VenueId",
                principalTable: "Venues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Venues_VenueId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_VenueId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "VenueId",
                table: "Appointments");
        }
    }
}
