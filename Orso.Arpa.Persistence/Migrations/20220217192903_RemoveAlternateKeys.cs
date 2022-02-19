using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orso.Arpa.Persistence.Migrations
{
    public partial class RemoveAlternateKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_url_roles",
                table: "url_roles");

            migrationBuilder.DropPrimaryKey(
                name: "pk_section_appointments",
                table: "section_appointments");

            migrationBuilder.DropPrimaryKey(
                name: "pk_project_appointments",
                table: "project_appointments");

            migrationBuilder.DropPrimaryKey(
                name: "pk_preferred_genres",
                table: "preferred_genres");

            migrationBuilder.DropPrimaryKey(
                name: "pk_person_sections",
                table: "person_sections");

            migrationBuilder.DropPrimaryKey(
                name: "pk_appointment_rooms",
                table: "appointment_rooms");

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("676b04c8-4256-4008-9fc4-be62ea8f5dd0"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("9860c80a-fa54-49e6-b314-ba895bd31348"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("cc54cb2a-30b5-473b-8d31-7788410bbc58"));

            migrationBuilder.AddPrimaryKey(
                name: "pk_url_roles",
                table: "url_roles",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_section_appointments",
                table: "section_appointments",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_project_appointments",
                table: "project_appointments",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_preferred_genres",
                table: "preferred_genres",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_person_sections",
                table: "person_sections",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_appointment_rooms",
                table: "appointment_rooms",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "ix_url_roles_url_id",
                table: "url_roles",
                column: "url_id");

            migrationBuilder.CreateIndex(
                name: "ix_section_appointments_section_id",
                table: "section_appointments",
                column: "section_id");

            migrationBuilder.CreateIndex(
                name: "ix_project_appointments_project_id",
                table: "project_appointments",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "ix_preferred_genres_musician_profile_id",
                table: "preferred_genres",
                column: "musician_profile_id");

            migrationBuilder.CreateIndex(
                name: "ix_person_sections_person_id",
                table: "person_sections",
                column: "person_id");

            migrationBuilder.CreateIndex(
                name: "ix_appointment_rooms_appointment_id",
                table: "appointment_rooms",
                column: "appointment_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_url_roles",
                table: "url_roles");

            migrationBuilder.DropIndex(
                name: "ix_url_roles_url_id",
                table: "url_roles");

            migrationBuilder.DropPrimaryKey(
                name: "pk_section_appointments",
                table: "section_appointments");

            migrationBuilder.DropIndex(
                name: "ix_section_appointments_section_id",
                table: "section_appointments");

            migrationBuilder.DropPrimaryKey(
                name: "pk_project_appointments",
                table: "project_appointments");

            migrationBuilder.DropIndex(
                name: "ix_project_appointments_project_id",
                table: "project_appointments");

            migrationBuilder.DropPrimaryKey(
                name: "pk_preferred_genres",
                table: "preferred_genres");

            migrationBuilder.DropIndex(
                name: "ix_preferred_genres_musician_profile_id",
                table: "preferred_genres");

            migrationBuilder.DropPrimaryKey(
                name: "pk_person_sections",
                table: "person_sections");

            migrationBuilder.DropIndex(
                name: "ix_person_sections_person_id",
                table: "person_sections");

            migrationBuilder.DropPrimaryKey(
                name: "pk_appointment_rooms",
                table: "appointment_rooms");

            migrationBuilder.DropIndex(
                name: "ix_appointment_rooms_appointment_id",
                table: "appointment_rooms");

            migrationBuilder.AddPrimaryKey(
                name: "pk_url_roles",
                table: "url_roles",
                columns: new[] { "url_id", "role_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_section_appointments",
                table: "section_appointments",
                columns: new[] { "section_id", "appointment_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_project_appointments",
                table: "project_appointments",
                columns: new[] { "project_id", "appointment_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_preferred_genres",
                table: "preferred_genres",
                columns: new[] { "musician_profile_id", "select_value_mapping_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_person_sections",
                table: "person_sections",
                columns: new[] { "person_id", "section_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_appointment_rooms",
                table: "appointment_rooms",
                columns: new[] { "appointment_id", "room_id" });

            migrationBuilder.InsertData(
                table: "localizations",
                columns: new[] { "id", "created_at", "created_by", "deleted", "key", "localization_culture", "modified_at", "modified_by", "resource_key", "text" },
                values: new object[,]
                {
                    { new Guid("676b04c8-4256-4008-9fc4-be62ea8f5dd0"), new DateTime(2021, 6, 16, 15, 30, 19, 400, DateTimeKind.Local).AddTicks(7391), "LocalizationSeedData", true, "Performer", "de", new DateTime(2021, 12, 17, 16, 14, 23, 215, DateTimeKind.Local).AddTicks(9665), "LocalizationSeedData", "RoleDto", "Mitwirkender" },
                    { new Guid("9860c80a-fa54-49e6-b314-ba895bd31348"), new DateTime(2021, 6, 16, 15, 30, 19, 401, DateTimeKind.Local).AddTicks(1189), "LocalizationSeedData", true, "Staff", "de", new DateTime(2021, 12, 17, 16, 14, 23, 217, DateTimeKind.Local).AddTicks(17), "LocalizationSeedData", "RoleDto", "Mitarbeiter" },
                    { new Guid("cc54cb2a-30b5-473b-8d31-7788410bbc58"), new DateTime(2021, 6, 16, 15, 30, 19, 401, DateTimeKind.Local).AddTicks(4545), "LocalizationSeedData", true, "Admin", "de", new DateTime(2021, 12, 17, 16, 14, 23, 217, DateTimeKind.Local).AddTicks(7085), "LocalizationSeedData", "RoleDto", "Administrator" }
                });
        }
    }
}
