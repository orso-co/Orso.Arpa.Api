using Microsoft.EntityFrameworkCore.Migrations;

namespace Orso.Arpa.Persistence.Migrations
{
    public partial class RenamedRegisterToSectionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MusicianProfiles_Registers_SectionId",
                table: "MusicianProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_RegisterAppointments_Appointments_AppointmentId",
                table: "RegisterAppointments");

            migrationBuilder.DropForeignKey(
                name: "FK_RegisterAppointments_Registers_SectionId",
                table: "RegisterAppointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Registers_Registers_ParentId",
                table: "Registers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Registers",
                table: "Registers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RegisterAppointments",
                table: "RegisterAppointments");

            migrationBuilder.RenameTable(
                name: "Registers",
                newName: "Sections");

            migrationBuilder.RenameTable(
                name: "RegisterAppointments",
                newName: "SectionAppointments");

            migrationBuilder.RenameIndex(
                name: "IX_Registers_ParentId",
                table: "Sections",
                newName: "IX_Sections_ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_RegisterAppointments_AppointmentId",
                table: "SectionAppointments",
                newName: "IX_SectionAppointments_AppointmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sections",
                table: "Sections",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SectionAppointments",
                table: "SectionAppointments",
                columns: new[] { "SectionId", "AppointmentId" });

            migrationBuilder.AddForeignKey(
                name: "FK_MusicianProfiles_Sections_SectionId",
                table: "MusicianProfiles",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SectionAppointments_Appointments_AppointmentId",
                table: "SectionAppointments",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SectionAppointments_Sections_SectionId",
                table: "SectionAppointments",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_Sections_ParentId",
                table: "Sections",
                column: "ParentId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MusicianProfiles_Sections_SectionId",
                table: "MusicianProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_SectionAppointments_Appointments_AppointmentId",
                table: "SectionAppointments");

            migrationBuilder.DropForeignKey(
                name: "FK_SectionAppointments_Sections_SectionId",
                table: "SectionAppointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_Sections_ParentId",
                table: "Sections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sections",
                table: "Sections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SectionAppointments",
                table: "SectionAppointments");

            migrationBuilder.RenameTable(
                name: "Sections",
                newName: "Registers");

            migrationBuilder.RenameTable(
                name: "SectionAppointments",
                newName: "RegisterAppointments");

            migrationBuilder.RenameIndex(
                name: "IX_Sections_ParentId",
                table: "Registers",
                newName: "IX_Registers_ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_SectionAppointments_AppointmentId",
                table: "RegisterAppointments",
                newName: "IX_RegisterAppointments_AppointmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Registers",
                table: "Registers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RegisterAppointments",
                table: "RegisterAppointments",
                columns: new[] { "SectionId", "AppointmentId" });

            migrationBuilder.AddForeignKey(
                name: "FK_MusicianProfiles_Registers_SectionId",
                table: "MusicianProfiles",
                column: "SectionId",
                principalTable: "Registers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RegisterAppointments_Appointments_AppointmentId",
                table: "RegisterAppointments",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RegisterAppointments_Registers_SectionId",
                table: "RegisterAppointments",
                column: "SectionId",
                principalTable: "Registers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Registers_Registers_ParentId",
                table: "Registers",
                column: "ParentId",
                principalTable: "Registers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
