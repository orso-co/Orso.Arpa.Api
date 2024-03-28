using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Orso.Arpa.Persistence.Migrations
{
    public partial class AddLocalizationTableAndSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "localizations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    key = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    text = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    localization_culture = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    resource_key = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_localizations", x => x.id);
                    table.UniqueConstraint("ak_localizations_resource_key_localization_culture_key", x => new { x.resource_key, x.localization_culture, x.key });
                });

            migrationBuilder.InsertData(
                table: "localizations",
                columns: new[] { "id", "created_at", "created_by", "deleted", "key", "localization_culture", "modified_at", "modified_by", "resource_key", "text" },
                values: new object[,]
                {
                    { new Guid("97fde212-3305-4fb9-b0a8-a12a8efb2872"), new DateTime(2021, 6, 16, 15, 30, 19, 324, DateTimeKind.Local).AddTicks(7866), "LocalizationSeedData", false, "Invalid cookie supplied", "en", null, null, "Validator", "Please try to login again" },
                    { new Guid("676b04c8-4256-4008-9fc4-be62ea8f5dd0"), new DateTime(2021, 6, 16, 15, 30, 19, 400, DateTimeKind.Local).AddTicks(7391), "LocalizationSeedData", false, "Performer", "de-DE", null, null, "RoleDto", "Künstler" },
                    { new Guid("563c7c31-3976-43e0-ac08-e8251004d647"), new DateTime(2021, 6, 16, 15, 30, 19, 400, DateTimeKind.Local).AddTicks(3657), "LocalizationSeedData", false, "Suppliers", "de-DE", null, null, "SectionDto", "Zulieferer" },
                    { new Guid("fd5dba23-9685-4821-9237-e182dafbcb52"), new DateTime(2021, 6, 16, 15, 30, 19, 399, DateTimeKind.Local).AddTicks(9816), "LocalizationSeedData", false, "Volunteers", "de-DE", null, null, "SectionDto", "Freiwillige" },
                    { new Guid("6aed06d4-2c86-414d-bf15-bce230d4d0e3"), new DateTime(2021, 6, 16, 15, 30, 19, 399, DateTimeKind.Local).AddTicks(5924), "LocalizationSeedData", false, "Visitors", "de-DE", null, null, "SectionDto", "Besucher" },
                    { new Guid("b2d699b8-e3ab-4cb9-8a1e-cd671e2fac02"), new DateTime(2021, 6, 16, 15, 30, 19, 399, DateTimeKind.Local).AddTicks(2187), "LocalizationSeedData", false, "Members", "de-DE", null, null, "SectionDto", "Mitglieder" },
                    { new Guid("8c6c4e59-8396-4975-b311-8c865cc48bb3"), new DateTime(2021, 6, 16, 15, 30, 19, 398, DateTimeKind.Local).AddTicks(7809), "LocalizationSeedData", false, "Orchestra", "de-DE", null, null, "SectionDto", "Orchester" },
                    { new Guid("631a8511-ae67-43c2-acfe-c8938e81e105"), new DateTime(2021, 6, 16, 15, 30, 19, 398, DateTimeKind.Local).AddTicks(3894), "LocalizationSeedData", false, "Performers", "de-DE", null, null, "SectionDto", "Künstler" },
                    { new Guid("5b0f922b-28be-4e7a-8954-24a022cc32cd"), new DateTime(2021, 6, 16, 15, 30, 19, 398, DateTimeKind.Local).AddTicks(74), "LocalizationSeedData", false, "A region with the requested name does already exist", "de-DE", null, null, "Validator", "Eine Region mit diesem Namen existiert bereits" },
                    { new Guid("b70c6131-2413-4da3-97c1-1a319b725db1"), new DateTime(2021, 6, 16, 15, 30, 19, 397, DateTimeKind.Local).AddTicks(6316), "LocalizationSeedData", false, "Email already exists", "de-DE", null, null, "Validator", "Die Email existiert bereits" },
                    { new Guid("906c9e51-590b-4142-a527-c868fb21d861"), new DateTime(2021, 6, 16, 15, 30, 19, 397, DateTimeKind.Local).AddTicks(2433), "LocalizationSeedData", false, "Username already exists", "de-DE", null, null, "Validator", "Der Benutzername existiert bereits" },
                    { new Guid("20712e04-5fcc-478f-9f3f-f32a91595344"), new DateTime(2021, 6, 16, 15, 30, 19, 396, DateTimeKind.Local).AddTicks(8890), "LocalizationSeedData", false, "Your account is locked. Kindly wait for 10 minutes and try again", "de-DE", null, null, "Validator", "Dein Konto wurde gesperrt. Bitte warte 10 Minuten und versuche es anschließend erneut" },
                    { new Guid("c5a807e4-9698-4d35-83d8-e23898d2557e"), new DateTime(2021, 6, 16, 15, 30, 19, 396, DateTimeKind.Local).AddTicks(4978), "LocalizationSeedData", false, "Your email address is not confirmed. Please confirm your email address first", "de-DE", null, null, "Validator", "Deine Email wurde noch nicht bestätigt. Bitte bestätige zuerst deine Email" },
                    { new Guid("f39d1e82-ed27-4afe-8f43-19ed4eee917a"), new DateTime(2021, 6, 16, 15, 30, 19, 396, DateTimeKind.Local).AddTicks(1240), "LocalizationSeedData", false, "The user could not be found", "de-DE", null, null, "Validator", "Der Benutzer konnte nicht gefunden werden" },
                    { new Guid("1365f7af-a435-484e-8f4c-a1f3e00d8a8d"), new DateTime(2021, 6, 16, 15, 30, 19, 395, DateTimeKind.Local).AddTicks(7653), "LocalizationSeedData", false, "Incorrect password supplied", "de-DE", null, null, "Validator", "Inkorrektes Passwort angegeben" },
                    { new Guid("9860c80a-fa54-49e6-b314-ba895bd31348"), new DateTime(2021, 6, 16, 15, 30, 19, 401, DateTimeKind.Local).AddTicks(1189), "LocalizationSeedData", false, "Staff", "de-DE", null, null, "RoleDto", "Mitarbeiter" },
                    { new Guid("6918a90d-0582-4991-937a-60a6c006e538"), new DateTime(2021, 6, 16, 15, 30, 19, 395, DateTimeKind.Local).AddTicks(3734), "LocalizationSeedData", false, "The section is not linked to the Appointment", "de-DE", null, null, "Validator", "Die Sektion ist dem Termin nicht zugeordnet" },
                    { new Guid("0cf37641-d3c0-4120-ad1b-d76e14507a57"), new DateTime(2021, 6, 16, 15, 30, 19, 394, DateTimeKind.Local).AddTicks(5913), "LocalizationSeedData", false, "The project is not linked to the appointment", "de-DE", null, null, "Validator", "Das Projekt ist dem Termin nicht zugeordnet" },
                    { new Guid("af9fe89a-d491-409f-af60-4be1421b0569"), new DateTime(2021, 6, 16, 15, 30, 19, 394, DateTimeKind.Local).AddTicks(1976), "LocalizationSeedData", false, "The section is already linked to the Appointment", "de-DE", null, null, "Validator", "Die Sektion ist bereits dem Termin zugeordnet" },
                    { new Guid("afeaddef-bc32-4fd5-8292-bdeafd0a8c2a"), new DateTime(2021, 6, 16, 15, 30, 19, 393, DateTimeKind.Local).AddTicks(8104), "LocalizationSeedData", false, "The room is already linked to the appointment", "de-DE", null, null, "Validator", "Der Raum ist bereits dem Termin zugeordnet" },
                    { new Guid("2c875289-6ce9-44a5-aa51-06a3e60aa32c"), new DateTime(2021, 6, 16, 15, 30, 19, 393, DateTimeKind.Local).AddTicks(4642), "LocalizationSeedData", false, "The project is already linked to the appointment", "de-DE", null, null, "Validator", "Das Projekt ist bereits dem Termin zugeordnet" },
                    { new Guid("89cf4e82-589a-4349-a61e-5c0958de1712"), new DateTime(2021, 6, 16, 15, 30, 19, 393, DateTimeKind.Local).AddTicks(1208), "LocalizationSeedData", false, "Username may only contain alphanumeric characters", "de-DE", null, null, "Validator", "Der Benutzername darf nur alphanumerische Zeichen enthalten" },
                    { new Guid("68df3bbc-f974-4a90-866c-d324fa513a5e"), new DateTime(2021, 6, 16, 15, 30, 19, 392, DateTimeKind.Local).AddTicks(6917), "LocalizationSeedData", false, "Password must contain at least one special character", "de-DE", null, null, "Validator", "Das Passwort muss mindestens ein Sonderzeichen enthalten" },
                    { new Guid("fbe724f3-3ddb-4010-a15e-e9dfafa99c2d"), new DateTime(2021, 6, 16, 15, 30, 19, 392, DateTimeKind.Local).AddTicks(748), "LocalizationSeedData", false, "Password must contain at least one digit", "de-DE", null, null, "Validator", "Das Passwort muss mindestens eine Zahl enthalten" },
                    { new Guid("a40cae79-a99d-41db-a198-2bb396ca2c0f"), new DateTime(2021, 6, 16, 15, 30, 19, 391, DateTimeKind.Local).AddTicks(7030), "LocalizationSeedData", false, "Password must contain at least one lowercase letter", "de-DE", null, null, "Validator", "Das Passwort muss mindestens einen Kleinbuchstaben enthalten" },
                    { new Guid("f067cffe-e377-4fec-b96a-da9ff784e761"), new DateTime(2021, 6, 16, 15, 30, 19, 391, DateTimeKind.Local).AddTicks(2502), "LocalizationSeedData", false, "Password must contain at least one uppercase letter", "de-DE", null, null, "Validator", "Das Passwort muss mindestens einen Großbuchstaben enthalten" },
                    { new Guid("da208786-f55a-4b32-92c2-365184ffe604"), new DateTime(2021, 6, 16, 15, 30, 19, 390, DateTimeKind.Local).AddTicks(9011), "LocalizationSeedData", false, "Password must be at least 6 characters", "de-DE", null, null, "Validator", "Das Passwort muss mindestens 6 Zeichen enthalten" },
                    { new Guid("c9f96e9b-5829-48d9-a58c-c3fc3cb284a1"), new DateTime(2021, 6, 16, 15, 30, 19, 390, DateTimeKind.Local).AddTicks(5458), "LocalizationSeedData", false, "EndTime must be later than StartTime", "de-DE", null, null, "Validator", "Endzeit muss später Startzeit sein" },
                    { new Guid("39e5d409-aa17-4bcc-94dc-dc5b576fd910"), new DateTime(2021, 6, 16, 15, 30, 19, 390, DateTimeKind.Local).AddTicks(1201), "LocalizationSeedData", false, "Invalid cookie supplied", "de-DE", null, null, "Validator", "Ungültiges Cookie angegeben" },
                    { new Guid("4899a356-74c0-4f48-9ca5-2a716f7718ab"), new DateTime(2021, 6, 16, 15, 30, 19, 389, DateTimeKind.Local).AddTicks(7493), "LocalizationSeedData", false, "This request requires a valid cookie to be provided", "de-DE", null, null, "Validator", "Diese Anfrage erfordert einen gültigen Cookie" },
                    { new Guid("1cc1999c-eec3-4891-a833-798a8ec6baae"), new DateTime(2021, 6, 16, 15, 30, 19, 386, DateTimeKind.Local).AddTicks(6960), "LocalizationSeedData", false, "Invalid cookie supplied", "en-GB", null, null, "Validator", "Please try to login again" },
                    { new Guid("cd15676c-3cfd-4578-a0a9-65e0844e8e21"), new DateTime(2021, 6, 16, 15, 30, 19, 394, DateTimeKind.Local).AddTicks(9976), "LocalizationSeedData", false, "The room is not linked to the appointment", "de-DE", null, null, "Validator", "Der Raum ist dem Termin nicht zugeordnet" },
                    { new Guid("cc54cb2a-30b5-473b-8d31-7788410bbc58"), new DateTime(2021, 6, 16, 15, 30, 19, 401, DateTimeKind.Local).AddTicks(4545), "LocalizationSeedData", false, "Admin", "de-DE", null, null, "RoleDto", "Administrator" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
               name: "localizations");
        }
    }
}
