using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Orso.Arpa.Persistence.Migrations
{
    public partial class MovedExpectationToAppointment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentParticipations_SelectValueMappings_ExpectationId",
                table: "AppointmentParticipations");

            migrationBuilder.DropIndex(
                name: "IX_AppointmentParticipations_ExpectationId",
                table: "AppointmentParticipations");

            migrationBuilder.DropColumn(
                name: "ExpectationId",
                table: "AppointmentParticipations");

            migrationBuilder.AddColumn<Guid>(
                name: "ExpectationId",
                table: "Appointments",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "SelectValueCategories",
                keyColumn: "Id",
                keyValue: new Guid("0fdb6218-54fa-4e94-a880-2a65fc1cf9d7"),
                column: "Table",
                value: "Appointment");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ExpectationId",
                table: "Appointments",
                column: "ExpectationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_SelectValueMappings_ExpectationId",
                table: "Appointments",
                column: "ExpectationId",
                principalTable: "SelectValueMappings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_SelectValueMappings_ExpectationId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_ExpectationId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "ExpectationId",
                table: "Appointments");

            migrationBuilder.AddColumn<Guid>(
                name: "ExpectationId",
                table: "AppointmentParticipations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "SelectValueCategories",
                keyColumn: "Id",
                keyValue: new Guid("0fdb6218-54fa-4e94-a880-2a65fc1cf9d7"),
                column: "Table",
                value: "AppointmentParticipation");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentParticipations_ExpectationId",
                table: "AppointmentParticipations",
                column: "ExpectationId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentParticipations_SelectValueMappings_ExpectationId",
                table: "AppointmentParticipations",
                column: "ExpectationId",
                principalTable: "SelectValueMappings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
