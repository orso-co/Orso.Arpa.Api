using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Orso.Arpa.Persistence.Migrations
{
    public partial class ChangeNavigationTablesToBaseEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "SectionAppointments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "SectionAppointments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "SectionAppointments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "SectionAppointments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "SectionAppointments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "SectionAppointments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ProjectAppointments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ProjectAppointments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "ProjectAppointments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "ProjectAppointments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "ProjectAppointments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "ProjectAppointments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "PersonSection",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "PersonSection",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "PersonSection",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "PersonSection",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "PersonSection",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "PersonSection",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "AppointmentRooms",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "AppointmentRooms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "AppointmentRooms",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "AppointmentRooms",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "AppointmentRooms",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "AppointmentRooms",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "SectionAppointments");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "SectionAppointments");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "SectionAppointments");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "SectionAppointments");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "SectionAppointments");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "SectionAppointments");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ProjectAppointments");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ProjectAppointments");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "ProjectAppointments");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProjectAppointments");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "ProjectAppointments");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "ProjectAppointments");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "PersonSection");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "PersonSection");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "PersonSection");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PersonSection");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "PersonSection");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "PersonSection");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "AppointmentRooms");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "AppointmentRooms");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "AppointmentRooms");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AppointmentRooms");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "AppointmentRooms");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "AppointmentRooms");
        }
    }
}
