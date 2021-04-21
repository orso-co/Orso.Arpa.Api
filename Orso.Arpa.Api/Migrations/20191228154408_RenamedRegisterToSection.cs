using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Orso.Arpa.Persistence.Migrations
{
    public partial class RenamedRegisterToSection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MusicianProfiles_Registers_RegisterId",
                table: "MusicianProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_RegisterAppointments_Registers_RegisterId",
                table: "RegisterAppointments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RegisterAppointments",
                table: "RegisterAppointments");

            migrationBuilder.DropIndex(
                name: "IX_MusicianProfiles_RegisterId",
                table: "MusicianProfiles");

            migrationBuilder.DropColumn(
                name: "RegisterId",
                table: "RegisterAppointments");

            migrationBuilder.DropColumn(
                name: "RegisterId",
                table: "MusicianProfiles");

            migrationBuilder.AddColumn<Guid>(
                name: "SectionId",
                table: "RegisterAppointments",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SectionId",
                table: "MusicianProfiles",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_RegisterAppointments",
                table: "RegisterAppointments",
                columns: new[] { "SectionId", "AppointmentId" });

            migrationBuilder.CreateIndex(
                name: "IX_MusicianProfiles_SectionId",
                table: "MusicianProfiles",
                column: "SectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_MusicianProfiles_Registers_SectionId",
                table: "MusicianProfiles",
                column: "SectionId",
                principalTable: "Registers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RegisterAppointments_Registers_SectionId",
                table: "RegisterAppointments",
                column: "SectionId",
                principalTable: "Registers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MusicianProfiles_Registers_SectionId",
                table: "MusicianProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_RegisterAppointments_Registers_SectionId",
                table: "RegisterAppointments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RegisterAppointments",
                table: "RegisterAppointments");

            migrationBuilder.DropIndex(
                name: "IX_MusicianProfiles_SectionId",
                table: "MusicianProfiles");

            migrationBuilder.DropColumn(
                name: "SectionId",
                table: "RegisterAppointments");

            migrationBuilder.DropColumn(
                name: "SectionId",
                table: "MusicianProfiles");

            migrationBuilder.AddColumn<Guid>(
                name: "RegisterId",
                table: "RegisterAppointments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "RegisterId",
                table: "MusicianProfiles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_RegisterAppointments",
                table: "RegisterAppointments",
                columns: new[] { "RegisterId", "AppointmentId" });

            migrationBuilder.CreateIndex(
                name: "IX_MusicianProfiles_RegisterId",
                table: "MusicianProfiles",
                column: "RegisterId");

            migrationBuilder.AddForeignKey(
                name: "FK_MusicianProfiles_Registers_RegisterId",
                table: "MusicianProfiles",
                column: "RegisterId",
                principalTable: "Registers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RegisterAppointments_Registers_RegisterId",
                table: "RegisterAppointments",
                column: "RegisterId",
                principalTable: "Registers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
