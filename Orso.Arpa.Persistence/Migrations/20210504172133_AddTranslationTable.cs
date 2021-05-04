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
                    { new Guid("93eaa20b-649a-4549-a673-bee9c09526e5"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Invalid Token supplied", "en,en-GB", null, null, "Validator", "Invalid Token supplied" },
                    { new Guid("ac309598-2b51-4a19-a553-b4931b5e7ee7"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Volunteers", "de,de-DE", null, null, "SectionDto", "Freiwillige" },
                    { new Guid("c8a8d3a0-feae-410e-a5ab-f5df89d85cb6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Visitors", "de,de-DE", null, null, "SectionDto", "Besucher" },
                    { new Guid("278b4094-ab9f-4cf7-b66e-f0fc07bc303b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Members", "de,de-DE", null, null, "SectionDto", "Mitglieder" },
                    { new Guid("e32a60b7-1522-4de3-88b7-92064d9b93af"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Orchestra", "de,de-DE", null, null, "SectionDto", "Orchester" },
                    { new Guid("44fc6e39-b2aa-47e9-88f9-d9467d3683b7"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Performers", "de,de-DE", null, null, "SectionDto", "Künstler" },
                    { new Guid("b6e10cbc-ce63-4f50-b367-d9dc072b4e7e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "A region with the requested name does already exist", "de,de-DE", null, null, "Validator", "Eine Region mit diesem Namen existiert bereits" },
                    { new Guid("db19aa8b-0e2e-4b50-af77-22b668740fa2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Email already exists", "de,de-DE", null, null, "Validator", "Die Email existiert bereits" },
                    { new Guid("a3176c10-b577-4059-b6cc-ea69b8d63ff7"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Username already exists", "de,de-DE", null, null, "Validator", "Der Benutzername existiert bereits" },
                    { new Guid("013c708f-a202-4d86-be45-91915f43e6d1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Your account is locked. Kindly wait for 10 minutes and try again", "de,de-DE", null, null, "Validator", "Dein Konto wurde gesperrt. Bitte warte 10 Minuten und versuche es anschließend erneut" },
                    { new Guid("dc843e1b-5d16-40c0-847a-c998937fbad8"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Your email address is not confirmed. Please confirm your email address first", "de,de-DE", null, null, "Validator", "Deine Email wurde noch nicht bestätigt. Bitte bestätige zuerst deine Email" },
                    { new Guid("c6b8fce9-6ff3-4287-97cd-fd424955bdd9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "The user could not be found", "de,de-DE", null, null, "Validator", "Der Benutzer konnte nicht gefunden werden" },
                    { new Guid("5f41473c-d8bf-49dd-9d37-96d98d92233b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Incorrect password supplied", "de,de-DE", null, null, "Validator", "Inkorrektes Passwort angegeben" },
                    { new Guid("bfaa4523-d490-400c-9ae5-d97954181ada"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Suppliers", "de,de-DE", null, null, "SectionDto", "Zulieferer" },
                    { new Guid("6c06eaf0-3b3a-4248-9614-f639ef3988fa"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "The section is not linked to the Appointment", "de,de-DE", null, null, "Validator", "Die Sektion ist dem Termin nicht zugeordnet" },
                    { new Guid("d74692c4-260e-4fc6-aa18-2044107a4d07"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "The project is not linked to the appointment", "de,de-DE", null, null, "Validator", "Das Projekt ist dem Termin nicht zugeordnet" },
                    { new Guid("969f1726-3b5c-4f0d-be74-d59071fc1433"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "The section is already linked to the Appointment", "de,de-DE", null, null, "Validator", "Die Sektion ist bereits dem Termin zugeordnet" },
                    { new Guid("7bd0accf-74fc-49db-902e-4070284ccbef"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "The room is already linked to the appointment", "de,de-DE", null, null, "Validator", "Der Raum ist bereits dem Termin zugeordnet" },
                    { new Guid("9bc920e6-dd3f-4d6d-806c-486e11539ebc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "The project is already linked to the appointment", "de,de-DE", null, null, "Validator", "Das Projekt ist bereits dem Termin zugeordnet" },
                    { new Guid("2fe30026-5ed6-45ba-b2e1-bada800d6ee9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Username may only contain alphanumeric characters", "de,de-DE", null, null, "Validator", "Der Benutzername darf nur alphanumerische Zeichen enthalten" },
                    { new Guid("32211140-a3c7-42d2-a42f-0691b05537a9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Password must contain at least one special character", "de,de-DE", null, null, "Validator", "Das Passwort muss mindestens ein Sonderzeichen enthalten" },
                    { new Guid("e1c07f9f-401d-47f4-93c7-fd1c4406e585"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Password must contain at least one digit", "de,de-DE", null, null, "Validator", "Das Passwort muss mindestens eine Zahl enthalten" },
                    { new Guid("74e7b16e-2852-4cd6-acd6-3ea2d59c9e44"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Password must contain at least one lowercase letter", "de,de-DE", null, null, "Validator", "Das Passwort muss mindestens einen Kleinbuchstaben enthalten" },
                    { new Guid("d3a32722-5986-42aa-a340-d4dc0cc612e2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Password must contain at least one uppercase letter", "de,de-DE", null, null, "Validator", "Das Passwort muss mindestens einen Großbuchstaben enthalten" },
                    { new Guid("c6a5d0df-432a-49ba-8bba-b325a213340c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Password must be at least 6 characters", "de,de-DE", null, null, "Validator", "Das Passwort muss mindestens 6 Zeichen enthalten" },
                    { new Guid("a7fa6ef8-14de-49e8-be75-e0951e50be65"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "EndTime must be later than StartTime", "de,de-DE", null, null, "Validator", "Endzeit muss später Startzeit sein" },
                    { new Guid("7431c854-2c92-4d58-8c7d-da5b54d0a034"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Invalid token supplied", "de,de-DE", null, null, "Validator", "Ungültiges Token angegeben" },
                    { new Guid("6fb9b085-ddc9-4546-a8a9-cf0e22c640cc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "The room is not linked to the appointment", "de,de-DE", null, null, "Validator", "Der Raum ist dem Termin nicht zugeordnet" },
                    { new Guid("23ec7f03-aac0-4c6c-818b-f77f234766cc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Contractors", "de,de-DE", null, null, "SectionDto", "Auftragnehmer" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "translations");
        }
    }
}
