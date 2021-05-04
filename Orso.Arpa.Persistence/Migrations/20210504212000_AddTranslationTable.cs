using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Orso.Arpa.Persistence.Migrations
{
    public partial class AddTranslationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "translations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    key = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    text = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    localization_culture = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    resource_key = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_translations", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "translations",
                columns: new[] { "id", "created_at", "created_by", "deleted", "key", "localization_culture", "modified_at", "modified_by", "resource_key", "text" },
                values: new object[,]
                {
                    { new Guid("118f341d-daed-4469-ba5b-d200c6c06f4a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Invalid Token supplied", "en,en-GB", null, null, "Validator", "Invalid Token supplied" },
                    { new Guid("f0ba8d0b-0385-441b-a49e-2b61d5e3aeae"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Suppliers", "de,de-DE", null, null, "SectionDto", "Zulieferer" },
                    { new Guid("53c17927-088f-4a82-b3a7-38620be329b4"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Volunteers", "de,de-DE", null, null, "SectionDto", "Freiwillige" },
                    { new Guid("99d2dad6-7d49-4554-badb-629275f9628b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Visitors", "de,de-DE", null, null, "SectionDto", "Besucher" },
                    { new Guid("a5afa0e6-5f75-4447-83bc-081389af5ba4"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Members", "de,de-DE", null, null, "SectionDto", "Mitglieder" },
                    { new Guid("a9e22452-7bf0-4e44-a461-633b6e0ca9ca"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Orchestra", "de,de-DE", null, null, "SectionDto", "Orchester" },
                    { new Guid("9c53fe16-740e-4adb-bb1b-680bfb13e953"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Performers", "de,de-DE", null, null, "SectionDto", "Künstler" },
                    { new Guid("e257e659-3636-4050-936c-64882ca98c74"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "A region with the requested name does already exist", "de,de-DE", null, null, "Validator", "Eine Region mit diesem Namen existiert bereits" },
                    { new Guid("08f8d37c-33a3-48f7-9ecd-946b6db7d113"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Email already exists", "de,de-DE", null, null, "Validator", "Die Email existiert bereits" },
                    { new Guid("0b0f9815-9a65-4209-839f-0cffa0d5a1ad"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Username already exists", "de,de-DE", null, null, "Validator", "Der Benutzername existiert bereits" },
                    { new Guid("fe8f9371-2143-4e07-832c-dbac93102901"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Your account is locked. Kindly wait for 10 minutes and try again", "de,de-DE", null, null, "Validator", "Dein Konto wurde gesperrt. Bitte warte 10 Minuten und versuche es anschließend erneut" },
                    { new Guid("1f782b84-bb22-40a4-8d05-6945997711b7"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Your email address is not confirmed. Please confirm your email address first", "de,de-DE", null, null, "Validator", "Deine Email wurde noch nicht bestätigt. Bitte bestätige zuerst deine Email" },
                    { new Guid("184aced7-dabf-4a33-be75-ab2712090e5a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "The user could not be found", "de,de-DE", null, null, "Validator", "Der Benutzer konnte nicht gefunden werden" },
                    { new Guid("190e83f8-55ef-46af-a5d0-60b751a3801f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Incorrect password supplied", "de,de-DE", null, null, "Validator", "Inkorrektes Passwort angegeben" },
                    { new Guid("295b2816-d2fa-4752-8ca2-23fb3d73a404"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "The section is not linked to the Appointment", "de,de-DE", null, null, "Validator", "Die Sektion ist dem Termin nicht zugeordnet" },
                    { new Guid("468fb69a-42df-4bd5-913b-536cd9e5d48a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "The room is not linked to the appointment", "de,de-DE", null, null, "Validator", "Der Raum ist dem Termin nicht zugeordnet" },
                    { new Guid("b94bddd7-996b-431c-bc0e-d4e2e7950c0d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "The project is not linked to the appointment", "de,de-DE", null, null, "Validator", "Das Projekt ist dem Termin nicht zugeordnet" },
                    { new Guid("284767aa-78f7-4062-abc3-4f79eeaa4be0"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "The section is already linked to the Appointment", "de,de-DE", null, null, "Validator", "Die Sektion ist bereits dem Termin zugeordnet" },
                    { new Guid("0cc67d40-58b7-48ba-b23c-3773f865c5bb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "The room is already linked to the appointment", "de,de-DE", null, null, "Validator", "Der Raum ist bereits dem Termin zugeordnet" },
                    { new Guid("e6a38f1d-0974-4351-ac1a-595f115a0a57"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "The project is already linked to the appointment", "de,de-DE", null, null, "Validator", "Das Projekt ist bereits dem Termin zugeordnet" },
                    { new Guid("e2d4f1b8-2c31-4e77-8b4f-475b660212aa"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Username may only contain alphanumeric characters", "de,de-DE", null, null, "Validator", "Der Benutzername darf nur alphanumerische Zeichen enthalten" },
                    { new Guid("c6350566-90f0-4734-aabd-b8f8d779c98f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Password must contain at least one special character", "de,de-DE", null, null, "Validator", "Das Passwort muss mindestens ein Sonderzeichen enthalten" },
                    { new Guid("f6e3cf69-aec4-4125-ba20-fad1aff89824"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Password must contain at least one digit", "de,de-DE", null, null, "Validator", "Das Passwort muss mindestens eine Zahl enthalten" },
                    { new Guid("f8c177a4-91f3-4651-92cb-ceefe69f1bd6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Password must contain at least one lowercase letter", "de,de-DE", null, null, "Validator", "Das Passwort muss mindestens einen Kleinbuchstaben enthalten" },
                    { new Guid("15244dbb-4c0b-4285-8893-3d1361674e70"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Password must contain at least one uppercase letter", "de,de-DE", null, null, "Validator", "Das Passwort muss mindestens einen Großbuchstaben enthalten" },
                    { new Guid("75199bd2-a2c7-4777-9052-aef07ce2eed3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Password must be at least 6 characters", "de,de-DE", null, null, "Validator", "Das Passwort muss mindestens 6 Zeichen enthalten" },
                    { new Guid("843e9f09-cd17-4f7c-a022-686b779fa416"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "EndTime must be later than StartTime", "de,de-DE", null, null, "Validator", "Endzeit muss später Startzeit sein" },
                    { new Guid("882df0ec-bc0b-4b16-92cd-1db9a6a6361c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Invalid token supplied", "de,de-DE", null, null, "Validator", "Ungültiges Token agegeben" },
                    { new Guid("12addddc-cc71-43aa-b740-c05d23a85831"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Contractors", "de,de-DE", null, null, "SectionDto", "Auftragnehmer" },
                    { new Guid("0be0c194-82f9-4015-904f-062e9762af13"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "This request requires a valid JWT access token to be provided", "de,de-DE", null, null, "Validator", "Diese Anfrage erfordert einen gültigen JWT Token" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "translations");
        }
    }
}
