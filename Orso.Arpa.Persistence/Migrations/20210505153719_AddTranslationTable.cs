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
                    { new Guid("a3cdee11-efb1-49c1-bba4-20e6119eb47f"), new DateTime(2021, 5, 5, 17, 36, 0, 389, DateTimeKind.Local).AddTicks(7031), "TranslationSeedData", false, "Invalid Token supplied", "en,en-GB", null, null, "Validator", "Invalid Token supplied" },
                    { new Guid("9044f388-ea9c-4922-9f83-ad70fe82194f"), new DateTime(2021, 5, 5, 17, 36, 0, 448, DateTimeKind.Local).AddTicks(8645), "TranslationSeedData", false, "Visitors", "de,de-DE", null, null, "SectionDto", "Besucher" },
                    { new Guid("4199d3b9-4551-4b01-9dbd-830da8c991e1"), new DateTime(2021, 5, 5, 17, 36, 0, 448, DateTimeKind.Local).AddTicks(5395), "TranslationSeedData", false, "Members", "de,de-DE", null, null, "SectionDto", "Mitglieder" },
                    { new Guid("8ed22d22-6fe2-4b5b-9ebf-23a230b10723"), new DateTime(2021, 5, 5, 17, 36, 0, 448, DateTimeKind.Local).AddTicks(2046), "TranslationSeedData", false, "Orchestra", "de,de-DE", null, null, "SectionDto", "Orchester" },
                    { new Guid("8ef2ae60-54e0-4d7f-a835-956fbf3ed85e"), new DateTime(2021, 5, 5, 17, 36, 0, 447, DateTimeKind.Local).AddTicks(8737), "TranslationSeedData", false, "Performers", "de,de-DE", null, null, "SectionDto", "Künstler" },
                    { new Guid("225710c5-bca2-4454-b6d0-bc00709ac6f2"), new DateTime(2021, 5, 5, 17, 36, 0, 447, DateTimeKind.Local).AddTicks(5626), "TranslationSeedData", false, "A region with the requested name does already exist", "de,de-DE", null, null, "Validator", "Eine Region mit diesem Namen existiert bereits" },
                    { new Guid("7ff6230d-1f53-48e2-a682-501e246591bf"), new DateTime(2021, 5, 5, 17, 36, 0, 447, DateTimeKind.Local).AddTicks(2496), "TranslationSeedData", false, "Email already exists", "de,de-DE", null, null, "Validator", "Die Email existiert bereits" },
                    { new Guid("915f3d94-c98e-47fc-a60c-9f41031a30d0"), new DateTime(2021, 5, 5, 17, 36, 0, 446, DateTimeKind.Local).AddTicks(8963), "TranslationSeedData", false, "Username already exists", "de,de-DE", null, null, "Validator", "Der Benutzername existiert bereits" },
                    { new Guid("62e4fe21-6d12-4a02-9846-f79505928cd4"), new DateTime(2021, 5, 5, 17, 36, 0, 446, DateTimeKind.Local).AddTicks(5919), "TranslationSeedData", false, "Your account is locked. Kindly wait for 10 minutes and try again", "de,de-DE", null, null, "Validator", "Dein Konto wurde gesperrt. Bitte warte 10 Minuten und versuche es anschließend erneut" },
                    { new Guid("86353d87-ca62-48c5-9500-3069b7fc6395"), new DateTime(2021, 5, 5, 17, 36, 0, 446, DateTimeKind.Local).AddTicks(2996), "TranslationSeedData", false, "Your email address is not confirmed. Please confirm your email address first", "de,de-DE", null, null, "Validator", "Deine Email wurde noch nicht bestätigt. Bitte bestätige zuerst deine Email" },
                    { new Guid("59f303b2-5046-45ae-bf8a-41d18528be5a"), new DateTime(2021, 5, 5, 17, 36, 0, 445, DateTimeKind.Local).AddTicks(9354), "TranslationSeedData", false, "The user could not be found", "de,de-DE", null, null, "Validator", "Der Benutzer konnte nicht gefunden werden" },
                    { new Guid("b4bc79da-0ced-4cc9-9c4f-25a715d45e5c"), new DateTime(2021, 5, 5, 17, 36, 0, 445, DateTimeKind.Local).AddTicks(6065), "TranslationSeedData", false, "Incorrect password supplied", "de,de-DE", null, null, "Validator", "Inkorrektes Passwort angegeben" },
                    { new Guid("397ed7cd-c9dc-40a4-8a6e-525af4ab7f8c"), new DateTime(2021, 5, 5, 17, 36, 0, 445, DateTimeKind.Local).AddTicks(2661), "TranslationSeedData", false, "The section is not linked to the Appointment", "de,de-DE", null, null, "Validator", "Die Sektion ist dem Termin nicht zugeordnet" },
                    { new Guid("84d57eff-c66a-4de0-bbf6-b5e3cc58cee4"), new DateTime(2021, 5, 5, 17, 36, 0, 449, DateTimeKind.Local).AddTicks(1912), "TranslationSeedData", false, "Volunteers", "de,de-DE", null, null, "SectionDto", "Freiwillige" },
                    { new Guid("ba1cbe3a-328a-40c1-936d-7458debe56f4"), new DateTime(2021, 5, 5, 17, 36, 0, 444, DateTimeKind.Local).AddTicks(9260), "TranslationSeedData", false, "The room is not linked to the appointment", "de,de-DE", null, null, "Validator", "Der Raum ist dem Termin nicht zugeordnet" },
                    { new Guid("8020fde1-b9c5-4bef-9002-8ac47cc1829c"), new DateTime(2021, 5, 5, 17, 36, 0, 444, DateTimeKind.Local).AddTicks(2167), "TranslationSeedData", false, "The section is already linked to the Appointment", "de,de-DE", null, null, "Validator", "Die Sektion ist bereits dem Termin zugeordnet" },
                    { new Guid("8070c0d9-64c4-443d-81e0-edc1a1b3d438"), new DateTime(2021, 5, 5, 17, 36, 0, 443, DateTimeKind.Local).AddTicks(9010), "TranslationSeedData", false, "The room is already linked to the appointment", "de,de-DE", null, null, "Validator", "Der Raum ist bereits dem Termin zugeordnet" },
                    { new Guid("762f8376-c10d-403c-913d-58b240739247"), new DateTime(2021, 5, 5, 17, 36, 0, 443, DateTimeKind.Local).AddTicks(5183), "TranslationSeedData", false, "The project is already linked to the appointment", "de,de-DE", null, null, "Validator", "Das Projekt ist bereits dem Termin zugeordnet" },
                    { new Guid("aaf187e0-6469-4dfd-bdd1-add3498efe08"), new DateTime(2021, 5, 5, 17, 36, 0, 443, DateTimeKind.Local).AddTicks(1599), "TranslationSeedData", false, "Username may only contain alphanumeric characters", "de,de-DE", null, null, "Validator", "Der Benutzername darf nur alphanumerische Zeichen enthalten" },
                    { new Guid("f2d78706-7324-48e3-b225-b0d905747b10"), new DateTime(2021, 5, 5, 17, 36, 0, 442, DateTimeKind.Local).AddTicks(7536), "TranslationSeedData", false, "Password must contain at least one special character", "de,de-DE", null, null, "Validator", "Das Passwort muss mindestens ein Sonderzeichen enthalten" },
                    { new Guid("dcadedaf-5e0c-4592-8b2f-5142375b46ba"), new DateTime(2021, 5, 5, 17, 36, 0, 442, DateTimeKind.Local).AddTicks(4428), "TranslationSeedData", false, "Password must contain at least one digit", "de,de-DE", null, null, "Validator", "Das Passwort muss mindestens eine Zahl enthalten" },
                    { new Guid("d0fcfee7-6968-4ed6-a99f-ae159639c1d2"), new DateTime(2021, 5, 5, 17, 36, 0, 442, DateTimeKind.Local).AddTicks(1152), "TranslationSeedData", false, "Password must contain at least one lowercase letter", "de,de-DE", null, null, "Validator", "Das Passwort muss mindestens einen Kleinbuchstaben enthalten" },
                    { new Guid("3dd56653-ea29-4bb3-8c4b-191464a33ca1"), new DateTime(2021, 5, 5, 17, 36, 0, 441, DateTimeKind.Local).AddTicks(7779), "TranslationSeedData", false, "Password must contain at least one uppercase letter", "de,de-DE", null, null, "Validator", "Das Passwort muss mindestens einen Großbuchstaben enthalten" },
                    { new Guid("87c25e29-e0bb-4f58-a0cb-db0a33df62ae"), new DateTime(2021, 5, 5, 17, 36, 0, 441, DateTimeKind.Local).AddTicks(4669), "TranslationSeedData", false, "Password must be at least 6 characters", "de,de-DE", null, null, "Validator", "Das Passwort muss mindestens 6 Zeichen enthalten" },
                    { new Guid("b6ffb704-b60b-4b64-a586-d78a8a8d2f23"), new DateTime(2021, 5, 5, 17, 36, 0, 441, DateTimeKind.Local).AddTicks(1526), "TranslationSeedData", false, "EndTime must be later than StartTime", "de,de-DE", null, null, "Validator", "Endzeit muss später Startzeit sein" },
                    { new Guid("7e7d0d27-affb-4772-b506-596a0cfc6be6"), new DateTime(2021, 5, 5, 17, 36, 0, 440, DateTimeKind.Local).AddTicks(7957), "TranslationSeedData", false, "Invalid token supplied", "de,de-DE", null, null, "Validator", "Ungültiges Token agegeben" },
                    { new Guid("b9e1803f-b76d-4e8f-a437-c5753f2085e9"), new DateTime(2021, 5, 5, 17, 36, 0, 440, DateTimeKind.Local).AddTicks(4496), "TranslationSeedData", false, "This request requires a valid JWT access token to be provided", "de,de-DE", null, null, "Validator", "Diese Anfrage erfordert einen gültigen JWT Token" },
                    { new Guid("d188ff99-d683-4b2a-9557-a78c5967474c"), new DateTime(2021, 5, 5, 17, 36, 0, 444, DateTimeKind.Local).AddTicks(6101), "TranslationSeedData", false, "The project is not linked to the appointment", "de,de-DE", null, null, "Validator", "Das Projekt ist dem Termin nicht zugeordnet" },
                    { new Guid("579803cf-6d21-43a6-b8ce-c7a3a07607cc"), new DateTime(2021, 5, 5, 17, 36, 0, 449, DateTimeKind.Local).AddTicks(5111), "TranslationSeedData", false, "Suppliers", "de,de-DE", null, null, "SectionDto", "Zulieferer" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "translations");
        }
    }
}
