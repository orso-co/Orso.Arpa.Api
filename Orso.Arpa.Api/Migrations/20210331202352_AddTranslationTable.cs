using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Orso.Arpa.Persistence.Migrations
{
    public partial class AddTranslationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Translations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Text = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    LocalizationCulture = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    ResourceKey = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translations", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Translations",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "Key", "LocalizationCulture", "ModifiedAt", "ModifiedBy", "ResourceKey", "Text" },
                values: new object[,]
                {
                    { new Guid("f55993f0-2629-4a6e-95b8-758f6e68f774"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "EndTime must be later than StartTime", "de-DE", null, null, "ApplicationResource", "Endzeit muss später Startzeit sein" },
                    { new Guid("982692bf-f056-46f6-8c0f-47d14da90e0d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Username already exists", "de-DE", null, null, "DomainResource", "Der Benutzername existiert bereits" },
                    { new Guid("ebc99220-9b0b-4e8f-b6b8-ea6dde34f3f4"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Your account is locked. Kindly wait for 10 minutes and try again", "de-DE", null, null, "DomainResource", "Dein Konto wurde gesperrt. Bitte warte 10 Minuten und versuche es anschließend erneut" },
                    { new Guid("635f453f-7caf-4f65-99dd-787882ad02ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Your email address is not confirmed. Please confirm your email address first", "de-DE", null, null, "DomainResource", "Deine Email wurde noch nicht bestätigt. Bitte bestätige zuerst deine Email" },
                    { new Guid("573c4551-d766-48bd-b6ee-16dc5cfdb913"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "The user could not be found", "de-DE", null, null, "DomainResource", "Der Benutzer konnte nicht gefunden werden" },
                    { new Guid("77ed3f2e-fecf-477c-ab03-de14019d2147"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Incorrect password supplied", "de-DE", null, null, "DomainResource", "Inkorrektes Passwort angegeben" },
                    { new Guid("71f27314-0515-402e-9a9c-f32dc2335e2c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "The section is not linked to the Appointment", "de-DE", null, null, "DomainResource", "Die Sektion ist dem Termin nicht zugeordnet" },
                    { new Guid("54580bf2-7a47-4211-9842-dcb1ea083933"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "The room is not linked to the appointment", "de-DE", null, null, "DomainResource", "Der Raum ist dem Termin nicht zugeordnet" },
                    { new Guid("9dab3175-3bab-4faa-a92c-efdd54bbfc3d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "The project is not linked to the appointment", "de-DE", null, null, "DomainResource", "Das Projekt ist dem Termin nicht zugeordnet" },
                    { new Guid("a08b8308-f219-41ee-8e2f-f5b1c7c1421a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "The section is already linked to the Appointment", "de-DE", null, null, "DomainResource", "Die Sektion ist bereits dem Termin zugeordnet" },
                    { new Guid("27d1b47c-1f49-482d-af2a-ba655a925f7e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "The room is already linked to the appointment", "de-DE", null, null, "DomainResource", "Der Raum ist bereits dem Termin zugeordnet" },
                    { new Guid("355ba10a-3611-4609-8d35-1f47d58d8379"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "The project is already linked to the appointment", "de-DE", null, null, "DomainResource", "Das Projekt ist bereits dem Termin zugeordnet" },
                    { new Guid("ae32fa39-9e66-49e3-aefb-b22583201845"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Username may only contain alphanumeric characters", "de-DE", null, null, "ApplicationResource", "Der Benutzername darf nur alphanumerische Zeichen enthalten" },
                    { new Guid("3848468e-cd87-4cac-9e38-274b5ae481e4"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Password must contain at least one special character", "de-DE", null, null, "ApplicationResource", "Das Passwort muss mindestens ein Sonderzeichen enthalten" },
                    { new Guid("acf85e3f-d013-49cd-9943-e00c97ea458d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Password must contain at least one digit", "de-DE", null, null, "ApplicationResource", "Das Passwort muss mindestens eine Zahl enthalten" },
                    { new Guid("4fcedf65-e57d-429c-8fa3-266383205b45"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Password must contain at least one lowercase letter", "de-DE", null, null, "ApplicationResource", "Das Passwort muss mindestens einen Kleinbuchstaben enthalten" },
                    { new Guid("122894ba-c993-47c0-af17-87e57e8daf35"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Password must contain at least one uppercase letter", "de-DE", null, null, "ApplicationResource", "Das Passwort muss mindestens einen Großbuchstaben enthalten" },
                    { new Guid("62b75f0f-ca82-48bf-acd0-853c642b4a52"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Password must be at least 6 characters", "de-DE", null, null, "ApplicationResource", "Das Passwort muss mindestens 6 Zeichen enthalten" },
                    { new Guid("77ca5561-d75c-4e2c-91a5-34f896fc3152"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Email already exists", "de-DE", null, null, "DomainResource", "Die Email existiert bereits" },
                    { new Guid("ddf7ce73-8f66-4547-9c58-7cc051b6e887"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "A region with the requested name does already exist", "de-DE", null, null, "DomainResource", "Eine Region mit diesem Namen existiert bereits" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Translations");
        }
    }
}
