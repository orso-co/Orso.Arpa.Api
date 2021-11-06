using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Orso.Arpa.Persistence.Migrations
{
    public partial class AddedGermanTranslations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("0cf37641-d3c0-4120-ad1b-d76e14507a57"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("1365f7af-a435-484e-8f4c-a1f3e00d8a8d"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("1cc1999c-eec3-4891-a833-798a8ec6baae"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("20712e04-5fcc-478f-9f3f-f32a91595344"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("2c875289-6ce9-44a5-aa51-06a3e60aa32c"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("39e5d409-aa17-4bcc-94dc-dc5b576fd910"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("4899a356-74c0-4f48-9ca5-2a716f7718ab"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("563c7c31-3976-43e0-ac08-e8251004d647"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("5b0f922b-28be-4e7a-8954-24a022cc32cd"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("631a8511-ae67-43c2-acfe-c8938e81e105"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("676b04c8-4256-4008-9fc4-be62ea8f5dd0"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("68df3bbc-f974-4a90-866c-d324fa513a5e"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("6918a90d-0582-4991-937a-60a6c006e538"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("6aed06d4-2c86-414d-bf15-bce230d4d0e3"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("89cf4e82-589a-4349-a61e-5c0958de1712"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("8c6c4e59-8396-4975-b311-8c865cc48bb3"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("906c9e51-590b-4142-a527-c868fb21d861"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("9860c80a-fa54-49e6-b314-ba895bd31348"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("a40cae79-a99d-41db-a198-2bb396ca2c0f"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("af9fe89a-d491-409f-af60-4be1421b0569"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("afeaddef-bc32-4fd5-8292-bdeafd0a8c2a"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("b2d699b8-e3ab-4cb9-8a1e-cd671e2fac02"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("b70c6131-2413-4da3-97c1-1a319b725db1"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("c5a807e4-9698-4d35-83d8-e23898d2557e"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("c9f96e9b-5829-48d9-a58c-c3fc3cb284a1"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("cc54cb2a-30b5-473b-8d31-7788410bbc58"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("cd15676c-3cfd-4578-a0a9-65e0844e8e21"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("da208786-f55a-4b32-92c2-365184ffe604"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("f067cffe-e377-4fec-b96a-da9ff784e761"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("f39d1e82-ed27-4afe-8f43-19ed4eee917a"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("fbe724f3-3ddb-4010-a15e-e9dfafa99c2d"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("fd5dba23-9685-4821-9237-e182dafbcb52"));

            migrationBuilder.InsertData(
                table: "localizations",
                columns: new[] { "id", "created_at", "created_by", "deleted", "key", "localization_culture", "modified_at", "modified_by", "resource_key", "text" },
                values: new object[,]
                {
                    { new Guid("ab328d92-3e3f-4815-b8d1-172b64cee552"), new DateTime(2021, 11, 6, 14, 56, 26, 545, DateTimeKind.Local).AddTicks(7328), "LocalizationSeedData", false, "Choir", "de", null, null, "SectionDto", "Chor" },
                    { new Guid("d1462152-541a-44db-8954-dfb802b9e0ff"), new DateTime(2021, 11, 6, 14, 56, 26, 644, DateTimeKind.Local).AddTicks(8557), "LocalizationSeedData", false, "Other", "de", null, null, "SelectValueDto", "Sonstiges" },
                    { new Guid("31f5f9b8-c3aa-4533-b10b-7172c75d27d8"), new DateTime(2021, 11, 6, 14, 56, 26, 645, DateTimeKind.Local).AddTicks(6367), "LocalizationSeedData", false, "Special Case", "de", null, null, "SelectValueDto", "Sonderfall" },
                    { new Guid("bc23ca38-31c9-43f7-81ef-1c330ba48385"), new DateTime(2021, 11, 6, 14, 56, 26, 646, DateTimeKind.Local).AddTicks(4074), "LocalizationSeedData", false, "Glöckner 2018", "de", null, null, "SelectValueDto", "Glöckner 2018" },
                    { new Guid("b131383c-d3e5-4222-8556-61aa6f59aa0e"), new DateTime(2021, 11, 6, 14, 56, 26, 647, DateTimeKind.Local).AddTicks(1795), "LocalizationSeedData", false, "Orchesetra Concert Lump Sump 1808", "de", null, null, "SelectValueDto", "Orchester Konzertpauschale 1808" },
                    { new Guid("6defbec6-09e1-4714-97c7-688a08ab71b9"), new DateTime(2021, 11, 6, 14, 56, 26, 647, DateTimeKind.Local).AddTicks(9502), "LocalizationSeedData", false, "Orchestra Concert Lump Sum 9€/11€ at 10h", "de", null, null, "SelectValueDto", "Orchester Konzertpauschale 9€/11€ bei 10h" },
                    { new Guid("38e33e88-0bd2-4c63-b127-e584b4d7eeaf"), new DateTime(2021, 11, 6, 14, 56, 26, 648, DateTimeKind.Local).AddTicks(6980), "LocalizationSeedData", false, "Orchestra Concert Lump Sum 9 €/11€ at 12h", "de", null, null, "SelectValueDto", "Orchester Konzertpauschale 9€/11€ bei 12h" },
                    { new Guid("7a7fbead-646e-44d8-b1ec-3db050c95850"), new DateTime(2021, 11, 6, 14, 56, 26, 649, DateTimeKind.Local).AddTicks(5017), "LocalizationSeedData", false, "Orchestra Rehearsal Hourly Rate 9/11", "de", null, null, "SelectValueDto", "Orchester Probe Stundensatz 9/11" },
                    { new Guid("70741068-6853-49d3-911a-69ad0776a633"), new DateTime(2021, 11, 6, 14, 56, 26, 650, DateTimeKind.Local).AddTicks(2907), "LocalizationSeedData", false, "Private", "de", null, null, "SelectValueDto", "Privat" },
                    { new Guid("1c74f84c-ac85-4576-ab8d-ec8ad6a1ac16"), new DateTime(2021, 11, 6, 14, 56, 26, 651, DateTimeKind.Local).AddTicks(315), "LocalizationSeedData", false, "Business", "de", null, null, "SelectValueDto", "Arbeit" },
                    { new Guid("81195307-6595-44c1-b5fa-f3307ab005ea"), new DateTime(2021, 11, 6, 14, 56, 26, 651, DateTimeKind.Local).AddTicks(7317), "LocalizationSeedData", false, "Amateur", "de", null, null, "SelectValueDto", "Amateur" },
                    { new Guid("48ff5e22-631b-4ad6-8d56-7a93ec74b3f0"), new DateTime(2021, 11, 6, 14, 56, 26, 652, DateTimeKind.Local).AddTicks(4672), "LocalizationSeedData", false, "Student", "de", null, null, "SelectValueDto", "Student" },
                    { new Guid("6fc929f4-d573-459b-9821-b3f275abc18b"), new DateTime(2021, 11, 6, 14, 56, 26, 653, DateTimeKind.Local).AddTicks(2059), "LocalizationSeedData", false, "Semi-Professional", "de", null, null, "SelectValueDto", "Semi-Professionell" },
                    { new Guid("e4b360c5-d035-47c1-910d-c6a78869bd12"), new DateTime(2021, 11, 6, 14, 56, 26, 653, DateTimeKind.Local).AddTicks(9720), "LocalizationSeedData", false, "Professional", "de", null, null, "SelectValueDto", "Professionell" },
                    { new Guid("2ce5ebda-c43e-4e73-902b-0444dd3ae434"), new DateTime(2021, 11, 6, 14, 56, 26, 655, DateTimeKind.Local).AddTicks(3385), "LocalizationSeedData", false, "Unknown", "de", null, null, "SelectValueDto", "Unbekannt" },
                    { new Guid("c20eec3a-ed3d-45fc-bc32-beef053f543f"), new DateTime(2021, 11, 6, 14, 56, 26, 656, DateTimeKind.Local).AddTicks(651), "LocalizationSeedData", false, "Without", "de", null, null, "SelectValueDto", "Ohne" },
                    { new Guid("dd82b8cb-525c-4b7c-b80f-7df6865b7add"), new DateTime(2021, 11, 6, 14, 56, 26, 656, DateTimeKind.Local).AddTicks(7776), "LocalizationSeedData", false, "Classical Music", "de", null, null, "SelectValueDto", "Klassische Musik" },
                    { new Guid("33be87b2-d85a-4060-a902-9ef2bafe55c8"), new DateTime(2021, 11, 6, 14, 56, 26, 658, DateTimeKind.Local).AddTicks(122), "LocalizationSeedData", false, "Crossover", "de", null, null, "SelectValueDto", "Crossover" },
                    { new Guid("26f71071-b529-4a7d-8d8a-dd6fdb3fdae0"), new DateTime(2021, 11, 6, 14, 56, 26, 658, DateTimeKind.Local).AddTicks(7328), "LocalizationSeedData", false, "Chamber Music", "de", null, null, "SelectValueDto", "Kammermusik" },
                    { new Guid("4472ed16-05dd-44a0-9364-43c17d5775ca"), new DateTime(2021, 11, 6, 14, 56, 26, 659, DateTimeKind.Local).AddTicks(5012), "LocalizationSeedData", false, "Film Music", "de", null, null, "SelectValueDto", "Filmmusik" },
                    { new Guid("a0233148-7245-4f67-a5ad-bd83703013eb"), new DateTime(2021, 11, 6, 14, 56, 26, 644, DateTimeKind.Local).AddTicks(740), "LocalizationSeedData", false, "Audition", "de", null, null, "SelectValueDto", "Vorsingen" },
                    { new Guid("b2c4833e-6ac6-4f3e-bf19-5c01ecf7b4f7"), new DateTime(2021, 11, 6, 14, 56, 26, 660, DateTimeKind.Local).AddTicks(2305), "LocalizationSeedData", false, "Interested", "de", null, null, "SelectValueDto", "Interessiert" },
                    { new Guid("6cb115c3-f287-4190-b302-0004945eb33b"), new DateTime(2021, 11, 6, 14, 56, 26, 643, DateTimeKind.Local).AddTicks(2974), "LocalizationSeedData", false, "Assembly", "de", null, null, "SelectValueDto", "Versammlung" },
                    { new Guid("6f85232d-d967-4318-b065-f715fa4af490"), new DateTime(2021, 11, 6, 14, 56, 26, 641, DateTimeKind.Local).AddTicks(4842), "LocalizationSeedData", false, "See Comment", "de", null, null, "SelectValueDto", "Siehe Kommentar" },
                    { new Guid("833bae79-aa89-4736-8bd0-4158b080509e"), new DateTime(2021, 11, 6, 14, 56, 26, 626, DateTimeKind.Local).AddTicks(9260), "LocalizationSeedData", false, "Rehearsal", "de", null, null, "SelectValueDto", "Probe" },
                    { new Guid("1dc469c0-2dbf-4dd1-be48-31fb4aef2a14"), new DateTime(2021, 11, 6, 14, 56, 26, 627, DateTimeKind.Local).AddTicks(6822), "LocalizationSeedData", false, "Sectional Rehearsal", "de", null, null, "SelectValueDto", "Stimmprobe" },
                    { new Guid("42e362f5-1919-4907-8d6e-c15efa09a081"), new DateTime(2021, 11, 6, 14, 56, 26, 629, DateTimeKind.Local).AddTicks(1284), "LocalizationSeedData", false, "Rehearsal Weekend Choir", "de", null, null, "SelectValueDto", "Probewochenende Chor" },
                    { new Guid("4cbc42c1-809d-4330-bd87-bf8b0a32f720"), new DateTime(2021, 11, 6, 14, 56, 26, 629, DateTimeKind.Local).AddTicks(8398), "LocalizationSeedData", false, "Vocal Coaching", "de", null, null, "SelectValueDto", "Stimmbildung" },
                    { new Guid("132942e0-9b2e-44aa-918f-a94a95728059"), new DateTime(2021, 11, 6, 14, 56, 26, 630, DateTimeKind.Local).AddTicks(5472), "LocalizationSeedData", false, "Choreography Rehearsal", "de", null, null, "SelectValueDto", "Choreoprobe" },
                    { new Guid("690cee26-4cac-4bab-a26a-4328247207f3"), new DateTime(2021, 11, 6, 14, 56, 26, 631, DateTimeKind.Local).AddTicks(2573), "LocalizationSeedData", false, "Soundcheck", "de", null, null, "SelectValueDto", "Soundcheck" },
                    { new Guid("37108879-01e4-4e58-a0ea-69ff235cbf44"), new DateTime(2021, 11, 6, 14, 56, 26, 632, DateTimeKind.Local).AddTicks(74), "LocalizationSeedData", false, "Warm-Up Rehearsal", "de", null, null, "SelectValueDto", "Anspielprobe" },
                    { new Guid("1d281676-51d0-426d-b307-c1db99260485"), new DateTime(2021, 11, 6, 14, 56, 26, 632, DateTimeKind.Local).AddTicks(8227), "LocalizationSeedData", false, "Concert", "de", null, null, "SelectValueDto", "Konzert" },
                    { new Guid("fe0db7ed-9642-4c53-b748-bfa89dc56835"), new DateTime(2021, 11, 6, 14, 56, 26, 633, DateTimeKind.Local).AddTicks(5315), "LocalizationSeedData", false, "Concert Tour", "de", null, null, "SelectValueDto", "Konzertreise (Tour)" },
                    { new Guid("6d9e6fc8-472b-4637-963b-f3ca5e9e92d3"), new DateTime(2021, 11, 6, 14, 56, 26, 634, DateTimeKind.Local).AddTicks(2370), "LocalizationSeedData", false, "Special Project", "de", null, null, "SelectValueDto", "Sonderprojekt" },
                    { new Guid("5fc07780-7ed3-4c41-bc3f-a56f607d74d7"), new DateTime(2021, 11, 6, 14, 56, 26, 634, DateTimeKind.Local).AddTicks(9447), "LocalizationSeedData", false, "CD Recording", "de", null, null, "SelectValueDto", "CD-Aufnahme" },
                    { new Guid("68a3db4a-a62b-4f25-8a65-55db89849ac7"), new DateTime(2021, 11, 6, 14, 56, 26, 635, DateTimeKind.Local).AddTicks(6463), "LocalizationSeedData", false, "Contest", "de", null, null, "SelectValueDto", "Wettbewerb" },
                    { new Guid("0e4ce182-f536-40bc-8f3e-77e6c8948e99"), new DateTime(2021, 11, 6, 14, 56, 26, 636, DateTimeKind.Local).AddTicks(3818), "LocalizationSeedData", false, "Meeting", "de", null, null, "SelectValueDto", "Besprechung" },
                    { new Guid("9e4fb163-4a73-4cdd-8f12-bca0423b7852"), new DateTime(2021, 11, 6, 14, 56, 26, 637, DateTimeKind.Local).AddTicks(877), "LocalizationSeedData", false, "Stage Briefing", "de", null, null, "SelectValueDto", "Bühneneinweisung" },
                    { new Guid("c1c9874d-ace4-40d8-994d-ee920575a892"), new DateTime(2021, 11, 6, 14, 56, 26, 637, DateTimeKind.Local).AddTicks(7928), "LocalizationSeedData", false, "Photo Session", "de", null, null, "SelectValueDto", "Fototermin" },
                    { new Guid("038b4791-b99b-4dad-ac23-33a0d1ca53c0"), new DateTime(2021, 11, 6, 14, 56, 26, 638, DateTimeKind.Local).AddTicks(4892), "LocalizationSeedData", false, "Workshop", "de", null, null, "SelectValueDto", "Kurs" },
                    { new Guid("c7f9e550-ca1b-4b36-a932-46a115d0f229"), new DateTime(2021, 11, 6, 14, 56, 26, 639, DateTimeKind.Local).AddTicks(1974), "LocalizationSeedData", false, "Party", "de", null, null, "SelectValueDto", "Party" },
                    { new Guid("db180a62-be30-4e6c-94ce-efeec5cac085"), new DateTime(2021, 11, 6, 14, 56, 26, 639, DateTimeKind.Local).AddTicks(9031), "LocalizationSeedData", false, "Show", "de", null, null, "SelectValueDto", "Show" },
                    { new Guid("a7851a46-abbc-4960-a26c-ce23f0e71bbc"), new DateTime(2021, 11, 6, 14, 56, 26, 640, DateTimeKind.Local).AddTicks(6690), "LocalizationSeedData", false, "Watch Show", "de", null, null, "SelectValueDto", "Show schauen" },
                    { new Guid("f3d8fc29-afc7-4112-9fd6-16f0598475e7"), new DateTime(2021, 11, 6, 14, 56, 26, 642, DateTimeKind.Local).AddTicks(4976), "LocalizationSeedData", false, "Transfer", "de", null, null, "SelectValueDto", "Transfer" },
                    { new Guid("85aa2777-9555-4751-8c68-fab87499f24a"), new DateTime(2021, 11, 6, 14, 56, 26, 626, DateTimeKind.Local).AddTicks(1937), "LocalizationSeedData", false, "Awaiting Poll", "de", null, null, "SelectValueDto", "Umfrage abwarten" },
                    { new Guid("fca0817b-a1f3-4caf-a106-309f52e2e40f"), new DateTime(2021, 11, 6, 14, 56, 26, 660, DateTimeKind.Local).AddTicks(9433), "LocalizationSeedData", false, "Acceptance", "de", null, null, "SelectValueDto", "Zusage" },
                    { new Guid("599e4267-2735-440e-a380-1ffed11eadd0"), new DateTime(2021, 11, 6, 14, 56, 26, 662, DateTimeKind.Local).AddTicks(3910), "LocalizationSeedData", false, "Candidate", "de", null, null, "SelectValueDto", "Kandidat" },
                    { new Guid("20712e04-5fcc-478f-9f3f-f32a91595344"), new DateTime(2021, 6, 16, 15, 30, 19, 396, DateTimeKind.Local).AddTicks(8890), "LocalizationSeedData", false, "Your account is locked. Kindly wait for 10 minutes and try again", "de", null, null, "Validator", "Dein Konto wurde gesperrt. Bitte warte 10 Minuten und versuche es anschließend erneut" },
                    { new Guid("c5a807e4-9698-4d35-83d8-e23898d2557e"), new DateTime(2021, 6, 16, 15, 30, 19, 396, DateTimeKind.Local).AddTicks(4978), "LocalizationSeedData", false, "Your email address is not confirmed. Please confirm your email address first", "de", null, null, "Validator", "Deine Email wurde noch nicht bestätigt. Bitte bestätige zuerst deine Email" },
                    { new Guid("f39d1e82-ed27-4afe-8f43-19ed4eee917a"), new DateTime(2021, 6, 16, 15, 30, 19, 396, DateTimeKind.Local).AddTicks(1240), "LocalizationSeedData", false, "The user could not be found", "de", null, null, "Validator", "Der Benutzer konnte nicht gefunden werden" },
                    { new Guid("1365f7af-a435-484e-8f4c-a1f3e00d8a8d"), new DateTime(2021, 6, 16, 15, 30, 19, 395, DateTimeKind.Local).AddTicks(7653), "LocalizationSeedData", false, "Incorrect password supplied", "de", null, null, "Validator", "Inkorrektes Passwort angegeben" },
                    { new Guid("6918a90d-0582-4991-937a-60a6c006e538"), new DateTime(2021, 6, 16, 15, 30, 19, 395, DateTimeKind.Local).AddTicks(3734), "LocalizationSeedData", false, "The section is not linked to the Appointment", "de", null, null, "Validator", "Die Sektion ist dem Termin nicht zugeordnet" },
                    { new Guid("cd15676c-3cfd-4578-a0a9-65e0844e8e21"), new DateTime(2021, 6, 16, 15, 30, 19, 394, DateTimeKind.Local).AddTicks(9976), "LocalizationSeedData", false, "The room is not linked to the appointment", "de", null, null, "Validator", "Der Raum ist dem Termin nicht zugeordnet" },
                    { new Guid("0cf37641-d3c0-4120-ad1b-d76e14507a57"), new DateTime(2021, 6, 16, 15, 30, 19, 394, DateTimeKind.Local).AddTicks(5913), "LocalizationSeedData", false, "The project is not linked to the appointment", "de", null, null, "Validator", "Das Projekt ist dem Termin nicht zugeordnet" },
                    { new Guid("af9fe89a-d491-409f-af60-4be1421b0569"), new DateTime(2021, 6, 16, 15, 30, 19, 394, DateTimeKind.Local).AddTicks(1976), "LocalizationSeedData", false, "The section is already linked to the Appointment", "de", null, null, "Validator", "Die Sektion ist bereits dem Termin zugeordnet" },
                    { new Guid("afeaddef-bc32-4fd5-8292-bdeafd0a8c2a"), new DateTime(2021, 6, 16, 15, 30, 19, 393, DateTimeKind.Local).AddTicks(8104), "LocalizationSeedData", false, "The room is already linked to the appointment", "de", null, null, "Validator", "Der Raum ist bereits dem Termin zugeordnet" },
                    { new Guid("2c875289-6ce9-44a5-aa51-06a3e60aa32c"), new DateTime(2021, 6, 16, 15, 30, 19, 393, DateTimeKind.Local).AddTicks(4642), "LocalizationSeedData", false, "The project is already linked to the appointment", "de", null, null, "Validator", "Das Projekt ist bereits dem Termin zugeordnet" },
                    { new Guid("89cf4e82-589a-4349-a61e-5c0958de1712"), new DateTime(2021, 6, 16, 15, 30, 19, 393, DateTimeKind.Local).AddTicks(1208), "LocalizationSeedData", false, "Username may only contain alphanumeric characters", "de", null, null, "Validator", "Der Benutzername darf nur alphanumerische Zeichen enthalten" },
                    { new Guid("68df3bbc-f974-4a90-866c-d324fa513a5e"), new DateTime(2021, 6, 16, 15, 30, 19, 392, DateTimeKind.Local).AddTicks(6917), "LocalizationSeedData", false, "Password must contain at least one special character", "de", null, null, "Validator", "Das Passwort muss mindestens ein Sonderzeichen enthalten" },
                    { new Guid("fbe724f3-3ddb-4010-a15e-e9dfafa99c2d"), new DateTime(2021, 6, 16, 15, 30, 19, 392, DateTimeKind.Local).AddTicks(748), "LocalizationSeedData", false, "Password must contain at least one digit", "de", null, null, "Validator", "Das Passwort muss mindestens eine Zahl enthalten" },
                    { new Guid("a40cae79-a99d-41db-a198-2bb396ca2c0f"), new DateTime(2021, 6, 16, 15, 30, 19, 391, DateTimeKind.Local).AddTicks(7030), "LocalizationSeedData", false, "Password must contain at least one lowercase letter", "de", null, null, "Validator", "Das Passwort muss mindestens einen Kleinbuchstaben enthalten" },
                    { new Guid("f067cffe-e377-4fec-b96a-da9ff784e761"), new DateTime(2021, 6, 16, 15, 30, 19, 391, DateTimeKind.Local).AddTicks(2502), "LocalizationSeedData", false, "Password must contain at least one uppercase letter", "de", null, null, "Validator", "Das Passwort muss mindestens einen Großbuchstaben enthalten" },
                    { new Guid("da208786-f55a-4b32-92c2-365184ffe604"), new DateTime(2021, 6, 16, 15, 30, 19, 390, DateTimeKind.Local).AddTicks(9011), "LocalizationSeedData", false, "Password must be at least 6 characters", "de", null, null, "Validator", "Das Passwort muss mindestens 6 Zeichen enthalten" },
                    { new Guid("c9f96e9b-5829-48d9-a58c-c3fc3cb284a1"), new DateTime(2021, 6, 16, 15, 30, 19, 390, DateTimeKind.Local).AddTicks(5458), "LocalizationSeedData", false, "EndTime must be later than StartTime", "de", null, null, "Validator", "Endzeit muss später Startzeit sein" },
                    { new Guid("39e5d409-aa17-4bcc-94dc-dc5b576fd910"), new DateTime(2021, 6, 16, 15, 30, 19, 390, DateTimeKind.Local).AddTicks(1201), "LocalizationSeedData", false, "Invalid token supplied", "de", null, null, "Validator", "Ungültiges Token angegeben" },
                    { new Guid("4899a356-74c0-4f48-9ca5-2a716f7718ab"), new DateTime(2021, 6, 16, 15, 30, 19, 389, DateTimeKind.Local).AddTicks(7493), "LocalizationSeedData", false, "This request requires a valid JWT access token to be provided", "de", null, null, "Validator", "Diese Anfrage erfordert einen gültigen JWT Token" },
                    { new Guid("906c9e51-590b-4142-a527-c868fb21d861"), new DateTime(2021, 6, 16, 15, 30, 19, 397, DateTimeKind.Local).AddTicks(2433), "LocalizationSeedData", false, "Username already exists", "de", null, null, "Validator", "Der Benutzername existiert bereits" },
                    { new Guid("997c054a-f8c3-44b8-a5f4-f0369ae7fc7c"), new DateTime(2021, 11, 6, 14, 56, 26, 661, DateTimeKind.Local).AddTicks(6605), "LocalizationSeedData", false, "Refusal", "de", null, null, "SelectValueDto", "Absage" },
                    { new Guid("b70c6131-2413-4da3-97c1-1a319b725db1"), new DateTime(2021, 6, 16, 15, 30, 19, 397, DateTimeKind.Local).AddTicks(6316), "LocalizationSeedData", false, "Email already exists", "de", null, null, "Validator", "Die Email existiert bereits" },
                    { new Guid("631a8511-ae67-43c2-acfe-c8938e81e105"), new DateTime(2021, 6, 16, 15, 30, 19, 398, DateTimeKind.Local).AddTicks(3894), "LocalizationSeedData", false, "Performers", "de", new DateTime(2021, 11, 6, 14, 56, 26, 505, DateTimeKind.Local).AddTicks(5667), "LocalizationSeedData", "SectionDto", "Mitwirkende" },
                    { new Guid("d090e733-e5c9-4975-afa9-306af576d6d6"), new DateTime(2021, 11, 6, 14, 56, 26, 663, DateTimeKind.Local).AddTicks(1072), "LocalizationSeedData", false, "Invited", "de", null, null, "SelectValueDto", "Eingeladen" },
                    { new Guid("b2d434da-a3cf-46ff-8a47-7fb2c8e1f87c"), new DateTime(2021, 11, 6, 14, 56, 26, 663, DateTimeKind.Local).AddTicks(8400), "LocalizationSeedData", false, "Not Invited", "de", null, null, "SelectValueDto", "Nicht eingeladen" },
                    { new Guid("d7d6eb42-b9c7-4ffd-9187-aafaa4c7748d"), new DateTime(2021, 11, 6, 14, 56, 26, 664, DateTimeKind.Local).AddTicks(5387), "LocalizationSeedData", false, "Unclear", "de", null, null, "SelectValueDto", "Unklar" },
                    { new Guid("e9320576-debc-49f5-b3da-4c12f0691c06"), new DateTime(2021, 11, 6, 14, 56, 26, 665, DateTimeKind.Local).AddTicks(2139), "LocalizationSeedData", false, "Male", "de", null, null, "SelectValueDto", "Männlich" },
                    { new Guid("9656d314-289d-4dc5-ad00-ea6579b56363"), new DateTime(2021, 11, 6, 14, 56, 26, 665, DateTimeKind.Local).AddTicks(9144), "LocalizationSeedData", false, "Female", "de", null, null, "SelectValueDto", "Weiblich" },
                    { new Guid("8340227d-9f21-4dcc-82dc-7f9836f91223"), new DateTime(2021, 11, 6, 14, 56, 26, 666, DateTimeKind.Local).AddTicks(5990), "LocalizationSeedData", false, "Diverse", "de", null, null, "SelectValueDto", "Divers" },
                    { new Guid("93eb89ec-47bc-4392-b78d-e434a566f217"), new DateTime(2021, 11, 6, 14, 56, 26, 667, DateTimeKind.Local).AddTicks(2918), "LocalizationSeedData", false, "Bank Account Expired", "de", null, null, "SelectValueDto", "Bankkonto erloschen" },
                    { new Guid("13e0657b-062b-427a-a61e-3cb74ef6fb3f"), new DateTime(2021, 11, 6, 14, 56, 26, 668, DateTimeKind.Local).AddTicks(2553), "LocalizationSeedData", false, "Return Debit Received", "de", null, null, "SelectValueDto", "Rücklastschrift erhalten" },
                    { new Guid("312d4540-4141-48c3-80e8-95ff26fca1d9"), new DateTime(2021, 11, 6, 14, 56, 26, 669, DateTimeKind.Local).AddTicks(2188), "LocalizationSeedData", false, "Incorrect Bank Details", "de", null, null, "SelectValueDto", "Fehlerhafte Bankverbindung" },
                    { new Guid("bb2202cb-c94b-4818-8e00-d8d561f54b2c"), new DateTime(2021, 11, 6, 14, 56, 26, 669, DateTimeKind.Local).AddTicks(8959), "LocalizationSeedData", false, "Other (see comment field)", "de", null, null, "SelectValueDto", "Sonstiges (siehe Kommentarfeld)" },
                    { new Guid("280e101d-0cbf-4473-9a4f-df5956b54516"), new DateTime(2021, 11, 6, 14, 56, 26, 670, DateTimeKind.Local).AddTicks(5684), "LocalizationSeedData", false, "University", "de", null, null, "SelectValueDto", "Universität" },
                    { new Guid("cc54cb2a-30b5-473b-8d31-7788410bbc58"), new DateTime(2021, 6, 16, 15, 30, 19, 401, DateTimeKind.Local).AddTicks(4545), "LocalizationSeedData", false, "Admin", "de", null, null, "RoleDto", "Administrator" },
                    { new Guid("9860c80a-fa54-49e6-b314-ba895bd31348"), new DateTime(2021, 6, 16, 15, 30, 19, 401, DateTimeKind.Local).AddTicks(1189), "LocalizationSeedData", false, "Staff", "de", null, null, "RoleDto", "Mitarbeiter" },
                    { new Guid("676b04c8-4256-4008-9fc4-be62ea8f5dd0"), new DateTime(2021, 6, 16, 15, 30, 19, 400, DateTimeKind.Local).AddTicks(7391), "LocalizationSeedData", false, "Performer", "de", new DateTime(2021, 11, 6, 14, 56, 26, 514, DateTimeKind.Local).AddTicks(9100), "LocalizationSeedData", "RoleDto", "Mitwirkender" },
                    { new Guid("563c7c31-3976-43e0-ac08-e8251004d647"), new DateTime(2021, 6, 16, 15, 30, 19, 400, DateTimeKind.Local).AddTicks(3657), "LocalizationSeedData", false, "Suppliers", "de", new DateTime(2021, 11, 6, 14, 56, 26, 513, DateTimeKind.Local).AddTicks(4645), "LocalizationSeedData", "SectionDto", "Lieferanten" },
                    { new Guid("fd5dba23-9685-4821-9237-e182dafbcb52"), new DateTime(2021, 6, 16, 15, 30, 19, 399, DateTimeKind.Local).AddTicks(9816), "LocalizationSeedData", false, "Volunteers", "de", null, null, "SectionDto", "Freiwillige" },
                    { new Guid("6aed06d4-2c86-414d-bf15-bce230d4d0e3"), new DateTime(2021, 6, 16, 15, 30, 19, 399, DateTimeKind.Local).AddTicks(5924), "LocalizationSeedData", false, "Visitors", "de", new DateTime(2021, 11, 6, 14, 56, 26, 510, DateTimeKind.Local).AddTicks(6036), "LocalizationSeedData", "SectionDto", "Zuschauer" },
                    { new Guid("b2d699b8-e3ab-4cb9-8a1e-cd671e2fac02"), new DateTime(2021, 6, 16, 15, 30, 19, 399, DateTimeKind.Local).AddTicks(2187), "LocalizationSeedData", false, "Members", "de", null, null, "SectionDto", "Mitglieder" },
                    { new Guid("8c6c4e59-8396-4975-b311-8c865cc48bb3"), new DateTime(2021, 6, 16, 15, 30, 19, 398, DateTimeKind.Local).AddTicks(7809), "LocalizationSeedData", false, "Orchestra", "de", null, null, "SectionDto", "Orchester" },
                    { new Guid("5b0f922b-28be-4e7a-8954-24a022cc32cd"), new DateTime(2021, 6, 16, 15, 30, 19, 398, DateTimeKind.Local).AddTicks(74), "LocalizationSeedData", false, "A region with the requested name does already exist", "de", null, null, "Validator", "Eine Region mit diesem Namen existiert bereits" },
                    { new Guid("124d2b21-06bc-4b4b-b981-a077d10159df"), new DateTime(2021, 11, 6, 14, 56, 26, 625, DateTimeKind.Local).AddTicks(3161), "LocalizationSeedData", false, "Refused", "de", null, null, "SelectValueDto", "Verworfen" },
                    { new Guid("2d9e912f-c393-4237-ad33-a7f4b0410412"), new DateTime(2021, 11, 6, 14, 56, 26, 628, DateTimeKind.Local).AddTicks(3858), "LocalizationSeedData", false, "Rehearsal Weekend", "de", null, null, "SelectValueDto", "Probewochenende allgemein" },
                    { new Guid("4646cfa2-b845-4f10-86c5-0d98dab9a97f"), new DateTime(2021, 11, 6, 14, 56, 26, 623, DateTimeKind.Local).AddTicks(8708), "LocalizationSeedData", false, "Optional", "de", null, null, "SelectValueDto", "Teilnahme möglich" },
                    { new Guid("cda58b4a-7dc4-48ea-9649-40e4901245e5"), new DateTime(2021, 11, 6, 14, 56, 26, 565, DateTimeKind.Local).AddTicks(2859), "LocalizationSeedData", false, "Clarinet", "de", null, null, "SectionDto", "Klarinette" },
                    { new Guid("6799f970-c22a-41ab-a817-b493ee68b8f3"), new DateTime(2021, 11, 6, 14, 56, 26, 566, DateTimeKind.Local).AddTicks(615), "LocalizationSeedData", false, "Eb Clarinet", "de", null, null, "SectionDto", "Eb-Klarinette" },
                    { new Guid("06be4626-f021-41b9-89a0-89ee3d4a40d6"), new DateTime(2021, 11, 6, 14, 56, 26, 566, DateTimeKind.Local).AddTicks(8049), "LocalizationSeedData", false, "Alto Clarinet", "de", null, null, "SectionDto", "Alt-Klarinette" },
                    { new Guid("bc51b97c-95e2-4ee0-be43-05538dd253f8"), new DateTime(2021, 11, 6, 14, 56, 26, 567, DateTimeKind.Local).AddTicks(5400), "LocalizationSeedData", false, "Bassett Horn", "de", null, null, "SectionDto", "Bassett-Horn" },
                    { new Guid("b01a1500-cf94-4b49-9256-1aec9ab7d5f7"), new DateTime(2021, 11, 6, 14, 56, 26, 568, DateTimeKind.Local).AddTicks(3235), "LocalizationSeedData", false, "Bass Clarinet", "de", null, null, "SectionDto", "Bass-Klarinette" },
                    { new Guid("0e8f1957-9ecf-4301-812b-3407427cf5ac"), new DateTime(2021, 11, 6, 14, 56, 26, 569, DateTimeKind.Local).AddTicks(1101), "LocalizationSeedData", false, "Saxophone", "de", null, null, "SectionDto", "Saxophon" },
                    { new Guid("38b60d3a-250c-4d88-9605-6e69103b7814"), new DateTime(2021, 11, 6, 14, 56, 26, 569, DateTimeKind.Local).AddTicks(8094), "LocalizationSeedData", false, "Sopran Saxophone", "de", null, null, "SectionDto", "Sopran-Saxophon" },
                    { new Guid("6dc99d30-b059-47ea-89f4-c83b2b7936d0"), new DateTime(2021, 11, 6, 14, 56, 26, 570, DateTimeKind.Local).AddTicks(5395), "LocalizationSeedData", false, "Alto Saxophone", "de", null, null, "SectionDto", "Alt-Saxophon" },
                    { new Guid("054aa861-a6b6-473e-999d-1a4245bba25b"), new DateTime(2021, 11, 6, 14, 56, 26, 571, DateTimeKind.Local).AddTicks(2688), "LocalizationSeedData", false, "Baritone Saxophone", "de", null, null, "SectionDto", "Bariton-Saxophon" },
                    { new Guid("1bb2ad85-c8cf-4ec2-b685-ed28f1f40f5f"), new DateTime(2021, 11, 6, 14, 56, 26, 571, DateTimeKind.Local).AddTicks(9829), "LocalizationSeedData", false, "Bass Saxophone", "de", null, null, "SectionDto", "Bass-Saxophon" },
                    { new Guid("4fc4c991-a38d-4bfd-8b6a-43f38f5cf871"), new DateTime(2021, 11, 6, 14, 56, 26, 572, DateTimeKind.Local).AddTicks(7288), "LocalizationSeedData", false, "Brass", "de", null, null, "SectionDto", "Bass" },
                    { new Guid("60a45401-d501-4f63-b730-0acf151a548e"), new DateTime(2021, 11, 6, 14, 56, 26, 573, DateTimeKind.Local).AddTicks(4627), "LocalizationSeedData", false, "Horn", "de", null, null, "SectionDto", "Horn" },
                    { new Guid("647d6a09-d4b7-4ec2-b7e4-0133949c1de7"), new DateTime(2021, 11, 6, 14, 56, 26, 574, DateTimeKind.Local).AddTicks(2259), "LocalizationSeedData", false, "Wagner Tuba", "de", null, null, "SectionDto", "Wagner Tuba" },
                    { new Guid("950312be-202e-4d3e-b5d5-0b59d1d6b861"), new DateTime(2021, 11, 6, 14, 56, 26, 574, DateTimeKind.Local).AddTicks(9829), "LocalizationSeedData", false, "Trumpet", "de", null, null, "SectionDto", "Trompete" },
                    { new Guid("0d564a24-c517-4c5c-8240-b21a240613df"), new DateTime(2021, 11, 6, 14, 56, 26, 575, DateTimeKind.Local).AddTicks(7400), "LocalizationSeedData", false, "Flugelhorn", "de", null, null, "SectionDto", "Flügelhorn" },
                    { new Guid("91cf97e6-b330-4945-bf35-350f05c11770"), new DateTime(2021, 11, 6, 14, 56, 26, 576, DateTimeKind.Local).AddTicks(4700), "LocalizationSeedData", false, "Piccolo Trumpet", "de", null, null, "SectionDto", "Piccolo-Trompete" },
                    { new Guid("c658f430-aa46-4d25-ae11-da8fadc1655c"), new DateTime(2021, 11, 6, 14, 56, 26, 577, DateTimeKind.Local).AddTicks(2487), "LocalizationSeedData", false, "Euphonium", "de", null, null, "SectionDto", "Euphonium" },
                    { new Guid("03b8dead-18d1-46fc-998d-0fb3916dd526"), new DateTime(2021, 11, 6, 14, 56, 26, 624, DateTimeKind.Local).AddTicks(5974), "LocalizationSeedData", false, "Scheduled", "de", null, null, "SelectValueDto", "Geplant" },
                    { new Guid("0c242960-8ba4-48be-be67-f0e83881c519"), new DateTime(2021, 11, 6, 14, 56, 26, 579, DateTimeKind.Local).AddTicks(6073), "LocalizationSeedData", false, "Baritone Horn", "de", null, null, "SectionDto", "Bariton-Horn" },
                    { new Guid("e77f675d-2c2e-4e09-aa49-14f662044d9e"), new DateTime(2021, 11, 6, 14, 56, 26, 564, DateTimeKind.Local).AddTicks(5593), "LocalizationSeedData", false, "Bariton Oboe", "de", null, null, "SectionDto", "Bariton-Oboe" },
                    { new Guid("6769eaa6-81d1-4d4b-bc6d-5bde93c62c26"), new DateTime(2021, 11, 6, 14, 56, 26, 581, DateTimeKind.Local).AddTicks(1488), "LocalizationSeedData", false, "Tuba", "de", null, null, "SectionDto", "Tuba" },
                    { new Guid("d29cb388-3f9f-49e2-8835-098c7e1b7e67"), new DateTime(2021, 11, 6, 14, 56, 26, 563, DateTimeKind.Local).AddTicks(8253), "LocalizationSeedData", false, "English Horn", "de", null, null, "SectionDto", "Englisch Horn" },
                    { new Guid("fd91326e-ea12-4f46-874c-5d52b646db71"), new DateTime(2021, 11, 6, 14, 56, 26, 561, DateTimeKind.Local).AddTicks(9129), "LocalizationSeedData", false, "Oboe", "de", null, null, "SectionDto", "Oboe" },
                    { new Guid("87e41c30-e5bc-4060-9549-ef7820385c8a"), new DateTime(2021, 11, 6, 14, 56, 26, 546, DateTimeKind.Local).AddTicks(7938), "LocalizationSeedData", false, "Female Voices", "de", null, null, "SectionDto", "Frauenstimmen" },
                    { new Guid("ff484291-0f10-4866-948c-722ab37f3501"), new DateTime(2021, 11, 6, 14, 56, 26, 547, DateTimeKind.Local).AddTicks(6331), "LocalizationSeedData", false, "High Female Voices", "de", null, null, "SectionDto", "Hohe Frauenstimmen" },
                    { new Guid("8c0cb6e9-5b08-4a45-9c4a-bdf32634f25c"), new DateTime(2021, 11, 6, 14, 56, 26, 548, DateTimeKind.Local).AddTicks(3912), "LocalizationSeedData", false, "Sporano", "de", null, null, "SectionDto", "Sopran" },
                    { new Guid("dd2f52db-71ce-4022-bda4-6118b9249188"), new DateTime(2021, 11, 6, 14, 56, 26, 549, DateTimeKind.Local).AddTicks(1420), "LocalizationSeedData", false, "Mezzo Soprano", "de", null, null, "SectionDto", "Mezzo-Sopran" },
                    { new Guid("2307503a-3355-45c7-807b-c467fdc4a6bd"), new DateTime(2021, 11, 6, 14, 56, 26, 549, DateTimeKind.Local).AddTicks(9381), "LocalizationSeedData", false, "Low Female Voices", "de", null, null, "SectionDto", "Tiefe Frauenstimmen" },
                    { new Guid("4e82d8e0-35af-462d-a7cf-209d947b2b18"), new DateTime(2021, 11, 6, 14, 56, 26, 550, DateTimeKind.Local).AddTicks(6904), "LocalizationSeedData", false, "Alto", "de", null, null, "SectionDto", "Alt" },
                    { new Guid("c465625f-4800-4dc5-a8b2-b0b62f438fb6"), new DateTime(2021, 11, 6, 14, 56, 26, 551, DateTimeKind.Local).AddTicks(4721), "LocalizationSeedData", false, "Male Voices", "de", null, null, "SectionDto", "Männerstimmen" },
                    { new Guid("d148dfb5-a9eb-41e2-9d88-acbc109d3bd8"), new DateTime(2021, 11, 6, 14, 56, 26, 552, DateTimeKind.Local).AddTicks(1939), "LocalizationSeedData", false, "High Male Voices", "de", null, null, "SectionDto", "Hohe Männerstimmen" },
                    { new Guid("8345e5e3-03ca-4d55-a893-9b9a78ac1115"), new DateTime(2021, 11, 6, 14, 56, 26, 552, DateTimeKind.Local).AddTicks(9693), "LocalizationSeedData", false, "Tenor", "de", null, null, "SectionDto", "Tenor" },
                    { new Guid("af7b823e-868e-47e1-862f-be5916ed40fa"), new DateTime(2021, 11, 6, 14, 56, 26, 553, DateTimeKind.Local).AddTicks(7637), "LocalizationSeedData", false, "Low Male Voices", "de", null, null, "SectionDto", "Tiefe Männerstimmen" },
                    { new Guid("0c182262-a9e4-4c89-bb93-5550de698271"), new DateTime(2021, 11, 6, 14, 56, 26, 554, DateTimeKind.Local).AddTicks(6110), "LocalizationSeedData", false, "Baritone", "de", null, null, "SectionDto", "Bariton" },
                    { new Guid("5100a0ad-25ac-4e7d-a7f9-c9816b02be2e"), new DateTime(2021, 11, 6, 14, 56, 26, 555, DateTimeKind.Local).AddTicks(3686), "LocalizationSeedData", false, "Basso", "de", null, null, "SectionDto", "Bass" },
                    { new Guid("f678e4b3-7c10-4a9e-8483-05d43361137a"), new DateTime(2021, 11, 6, 14, 56, 26, 556, DateTimeKind.Local).AddTicks(1053), "LocalizationSeedData", false, "Winds", "de", null, null, "SectionDto", "Bläser" },
                    { new Guid("1ce6e399-db60-4b4c-89eb-d8e547e900ce"), new DateTime(2021, 11, 6, 14, 56, 26, 557, DateTimeKind.Local).AddTicks(348), "LocalizationSeedData", false, "Woodwinds", "de", null, null, "SectionDto", "Holzbläser" },
                    { new Guid("91733935-2eae-4710-8c83-e522aaa45ce5"), new DateTime(2021, 11, 6, 14, 56, 26, 557, DateTimeKind.Local).AddTicks(9343), "LocalizationSeedData", false, "Flute", "de", null, null, "SectionDto", "Flöte" },
                    { new Guid("f304ad53-6bb7-45fe-a11b-7f1eaf79d6df"), new DateTime(2021, 11, 6, 14, 56, 26, 558, DateTimeKind.Local).AddTicks(9504), "LocalizationSeedData", false, "Piccolo Flute", "de", null, null, "SectionDto", "Piccolo-Flöte" },
                    { new Guid("874e2e22-0ba1-4578-89d7-1a7b74741283"), new DateTime(2021, 11, 6, 14, 56, 26, 559, DateTimeKind.Local).AddTicks(7443), "LocalizationSeedData", false, "Alto Flute", "de", null, null, "SectionDto", "Alt-Flöte" },
                    { new Guid("0574f19c-2061-4b88-bf0e-ab64fbbe2dbc"), new DateTime(2021, 11, 6, 14, 56, 26, 560, DateTimeKind.Local).AddTicks(4909), "LocalizationSeedData", false, "Tenor Flute", "de", null, null, "SectionDto", "Tenor-Flöte" },
                    { new Guid("9c318bca-fa1e-49fa-920b-dae2b0a23b5c"), new DateTime(2021, 11, 6, 14, 56, 26, 561, DateTimeKind.Local).AddTicks(2240), "LocalizationSeedData", false, "Bass Flute", "de", null, null, "SectionDto", "Bass-Flöte" },
                    { new Guid("d2ac9f50-2d73-47d9-a446-3eb3ded478e8"), new DateTime(2021, 11, 6, 14, 56, 26, 563, DateTimeKind.Local).AddTicks(503), "LocalizationSeedData", false, "Oboe d'Amore", "de", null, null, "SectionDto", "Oboe d'Amore" },
                    { new Guid("ad5ce3ec-e819-4a59-86c7-ef277e8bf9c0"), new DateTime(2021, 11, 6, 14, 56, 26, 582, DateTimeKind.Local).AddTicks(4099), "LocalizationSeedData", false, "Eb Tuba", "de", null, null, "SectionDto", "Eb-Tuba" },
                    { new Guid("2d705575-c129-4914-9211-4c6b8c865687"), new DateTime(2021, 11, 6, 14, 56, 26, 577, DateTimeKind.Local).AddTicks(9656), "LocalizationSeedData", false, "Tenor Horn", "de", null, null, "SectionDto", "Tenor-Horn" },
                    { new Guid("6a8737b8-8aec-4dfd-873e-593487985bee"), new DateTime(2021, 11, 6, 14, 56, 26, 585, DateTimeKind.Local).AddTicks(4527), "LocalizationSeedData", false, "Percussion", "de", null, null, "SectionDto", "Percussion" },
                    { new Guid("a8dc77f8-4674-4c88-b6ad-d8fd72eea57e"), new DateTime(2021, 11, 6, 14, 56, 26, 607, DateTimeKind.Local).AddTicks(4188), "LocalizationSeedData", false, "Viola", "de", null, null, "SectionDto", "Bratsche" },
                    { new Guid("a43c0bfe-debc-4ab9-9dd7-b6728b5ddda4"), new DateTime(2021, 11, 6, 14, 56, 26, 608, DateTimeKind.Local).AddTicks(2149), "LocalizationSeedData", false, "Low Strings", "de", null, null, "SectionDto", "Tiefe Streicher" },
                    { new Guid("da7eef28-30e2-4040-96bc-90a7b8d7a60d"), new DateTime(2021, 11, 6, 14, 56, 26, 584, DateTimeKind.Local).AddTicks(1746), "LocalizationSeedData", false, "F Tuba", "de", null, null, "SectionDto", "F-Tuba" },
                    { new Guid("68a707dc-1aa5-405c-b8cb-62d85e196c77"), new DateTime(2021, 11, 6, 14, 56, 26, 609, DateTimeKind.Local).AddTicks(6405), "LocalizationSeedData", false, "Band", "de", null, null, "SectionDto", "Band" },
                    { new Guid("e7446f94-b4e0-455a-a42b-f7a827b8fa11"), new DateTime(2021, 11, 6, 14, 56, 26, 610, DateTimeKind.Local).AddTicks(5760), "LocalizationSeedData", false, "Soloists", "de", null, null, "SectionDto", "Solisten" },
                    { new Guid("8a1f834e-d050-43db-9a5f-cf96996c6572"), new DateTime(2021, 11, 6, 14, 56, 26, 613, DateTimeKind.Local).AddTicks(5383), "LocalizationSeedData", false, "Present", "de", null, null, "SelectValueDto", "Anwesend" },
                    { new Guid("207688d3-bde2-4642-84bf-ce6de8c170de"), new DateTime(2021, 11, 6, 14, 56, 26, 614, DateTimeKind.Local).AddTicks(2839), "LocalizationSeedData", false, "Absent", "de", null, null, "SelectValueDto", "Nicht anwesend" },
                    { new Guid("80eedc07-10c2-41cc-b3e2-c55ca9e09d69"), new DateTime(2021, 11, 6, 14, 56, 26, 615, DateTimeKind.Local).AddTicks(449), "LocalizationSeedData", false, "Inapplicable", "de", null, null, "SelectValueDto", "Unzutreffend" },
                    { new Guid("7f1b7318-02f5-49c9-9cd1-ace4776d187f"), new DateTime(2021, 11, 6, 14, 56, 26, 615, DateTimeKind.Local).AddTicks(7728), "LocalizationSeedData", false, "Ambiguous", "de", null, null, "SelectValueDto", "Unklar" },
                    { new Guid("40774af5-2c23-47a4-b016-6a2178d4932b"), new DateTime(2021, 11, 6, 14, 56, 26, 616, DateTimeKind.Local).AddTicks(6148), "LocalizationSeedData", false, "Awaiting Scan", "de", null, null, "SelectValueDto", "Eintrag nach Scan" },
                    { new Guid("98622c74-c0b3-44f3-aed7-c184523837f4"), new DateTime(2021, 11, 6, 14, 56, 26, 617, DateTimeKind.Local).AddTicks(3435), "LocalizationSeedData", false, "Yes", "de", null, null, "SelectValueDto", "Ja" },
                    { new Guid("a60f5161-3a7c-4872-afa0-af9ce3c65a9d"), new DateTime(2021, 11, 6, 14, 56, 26, 618, DateTimeKind.Local).AddTicks(784), "LocalizationSeedData", false, "No", "de", null, null, "SelectValueDto", "Nein" },
                    { new Guid("c075211f-30c6-4099-83ec-731c18b3e41c"), new DateTime(2021, 11, 6, 14, 56, 26, 618, DateTimeKind.Local).AddTicks(7669), "LocalizationSeedData", false, "Partly", "de", null, null, "SelectValueDto", "Teilweise" },
                    { new Guid("afa84594-0d55-49a8-b8b7-7138be7daca4"), new DateTime(2021, 11, 6, 14, 56, 26, 619, DateTimeKind.Local).AddTicks(5111), "LocalizationSeedData", false, "Don't know yet", "de", null, null, "SelectValueDto", "Ich weiß es noch nicht" },
                    { new Guid("855e5d1b-b4e8-4329-a6c2-4e190c57f42e"), new DateTime(2021, 11, 6, 14, 56, 26, 620, DateTimeKind.Local).AddTicks(2267), "LocalizationSeedData", false, "Confirmed", "de", null, null, "SelectValueDto", "Bestätigt" },
                    { new Guid("c81554b8-c4f1-46ad-a9d2-80353b6c3795"), new DateTime(2021, 11, 6, 14, 56, 26, 620, DateTimeKind.Local).AddTicks(9495), "LocalizationSeedData", false, "Cancelled", "de", null, null, "SelectValueDto", "Abgesagt" },
                    { new Guid("4a2e2fd3-beab-4770-b000-e98ad3200363"), new DateTime(2021, 11, 6, 14, 56, 26, 621, DateTimeKind.Local).AddTicks(6743), "LocalizationSeedData", false, "Postponed", "de", null, null, "SelectValueDto", "Verschoben" },
                    { new Guid("65b7ed5d-5444-4ce1-a3e6-af93aaabffd6"), new DateTime(2021, 11, 6, 14, 56, 26, 622, DateTimeKind.Local).AddTicks(4002), "LocalizationSeedData", false, "Archived", "de", null, null, "SelectValueDto", "Archiviert" },
                    { new Guid("35a3df40-7439-4e1d-9872-c937be1d4ace"), new DateTime(2021, 11, 6, 14, 56, 26, 623, DateTimeKind.Local).AddTicks(1285), "LocalizationSeedData", false, "Mandatory", "de", null, null, "SelectValueDto", "Teilnahme erwünscht" },
                    { new Guid("ba0385b4-251c-4fbf-b3f2-379d02b44132"), new DateTime(2021, 11, 6, 14, 56, 26, 606, DateTimeKind.Local).AddTicks(6717), "LocalizationSeedData", false, "Violin", "de", null, null, "SectionDto", "Geige" },
                    { new Guid("cd122c30-1cac-414d-8413-d54d801be26e"), new DateTime(2021, 11, 6, 14, 56, 26, 605, DateTimeKind.Local).AddTicks(8226), "LocalizationSeedData", false, "High Strings", "de", null, null, "SectionDto", "Hohe Streicher" },
                    { new Guid("d861a2fc-af0b-412f-ac38-3086bdf8b6d2"), new DateTime(2021, 11, 6, 14, 56, 26, 608, DateTimeKind.Local).AddTicks(9410), "LocalizationSeedData", false, "Violoncello", "de", null, null, "SectionDto", "Cello" },
                    { new Guid("aba3dddb-f2d3-4067-ac51-da48d5bfe359"), new DateTime(2021, 11, 6, 14, 56, 26, 604, DateTimeKind.Local).AddTicks(4132), "LocalizationSeedData", false, "Didgeridoo", "de", null, null, "SectionDto", "Didgeridoo" },
                    { new Guid("b578ba36-5297-416f-9946-e4444c5f4048"), new DateTime(2021, 11, 6, 14, 56, 26, 587, DateTimeKind.Local).AddTicks(2352), "LocalizationSeedData", false, "Glockenspiel", "de", null, null, "SectionDto", "Glockenspiel" },
                    { new Guid("34ceac7a-24ee-43d5-ae17-764ee266b409"), new DateTime(2021, 11, 6, 14, 56, 26, 589, DateTimeKind.Local).AddTicks(623), "LocalizationSeedData", false, "Vibraphone", "de", null, null, "SectionDto", "Vibraphon" },
                    { new Guid("82706e73-83f8-445a-8b50-ed6bc495c116"), new DateTime(2021, 11, 6, 14, 56, 26, 590, DateTimeKind.Local).AddTicks(1021), "LocalizationSeedData", false, "Xylophone", "de", null, null, "SectionDto", "Xylophon" },
                    { new Guid("1575ef92-8714-4bdb-a3ee-14c95f544e14"), new DateTime(2021, 11, 6, 14, 56, 26, 591, DateTimeKind.Local).AddTicks(5862), "LocalizationSeedData", false, "Marimbaphone", "de", null, null, "SectionDto", "Marimbaphone" },
                    { new Guid("91a88417-2fe8-4b67-9e94-0c9ae7ed0878"), new DateTime(2021, 11, 6, 14, 56, 26, 592, DateTimeKind.Local).AddTicks(9702), "LocalizationSeedData", false, "Others", "de", null, null, "SectionDto", "Andere" },
                    { new Guid("c7cb8a58-833c-4759-8919-404e6fcd68fd"), new DateTime(2021, 11, 6, 14, 56, 26, 605, DateTimeKind.Local).AddTicks(1370), "LocalizationSeedData", false, "Strings", "de", null, null, "SectionDto", "Streicher" },
                    { new Guid("65a54836-e73e-4a2a-83c9-fada544d6ecd"), new DateTime(2021, 11, 6, 14, 56, 26, 594, DateTimeKind.Local).AddTicks(8731), "LocalizationSeedData", false, "Keyboards", "de", null, null, "SectionDto", "Keyboard" },
                    { new Guid("982add29-f7ae-483a-b6db-19c83ce4580f"), new DateTime(2021, 11, 6, 14, 56, 26, 595, DateTimeKind.Local).AddTicks(6118), "LocalizationSeedData", false, "Piano", "de", null, null, "SectionDto", "Klavier" },
                    { new Guid("dae23eca-d2a0-482d-95fd-c1d1ae7cb623"), new DateTime(2021, 11, 6, 14, 56, 26, 596, DateTimeKind.Local).AddTicks(3367), "LocalizationSeedData", false, "Celesta", "de", null, null, "SectionDto", "Celesta" },
                    { new Guid("fd88fd6d-c5db-47f8-a844-b4c76403e6c5"), new DateTime(2021, 11, 6, 14, 56, 26, 593, DateTimeKind.Local).AddTicks(8501), "LocalizationSeedData", false, "Harp", "de", null, null, "SectionDto", "Harfe" },
                    { new Guid("6f17f440-94ea-4780-87e1-1c097e7936e3"), new DateTime(2021, 11, 6, 14, 56, 26, 597, DateTimeKind.Local).AddTicks(8058), "LocalizationSeedData", false, "Organ", "de", null, null, "SectionDto", "Orgel" },
                    { new Guid("76625cc9-a4f5-4d50-aee8-2ea349c5f822"), new DateTime(2021, 11, 6, 14, 56, 26, 597, DateTimeKind.Local).AddTicks(522), "LocalizationSeedData", false, "Cembalo", "de", null, null, "SectionDto", "Cembalo" },
                    { new Guid("681c0175-abb7-46bd-bdd7-98e01aa8952d"), new DateTime(2021, 11, 6, 14, 56, 26, 602, DateTimeKind.Local).AddTicks(9681), "LocalizationSeedData", false, "Glass Harp", "de", null, null, "SectionDto", "Glasharfe" },
                    { new Guid("0b418529-b6f6-4808-b570-fc5fcf2d7486"), new DateTime(2021, 11, 6, 14, 56, 26, 602, DateTimeKind.Local).AddTicks(2500), "LocalizationSeedData", false, "Electric Bass", "de", null, null, "SectionDto", "Elektrischer Bass" },
                    { new Guid("9b245b7d-864e-4f33-b06e-fe5e4d449339"), new DateTime(2021, 11, 6, 14, 56, 26, 601, DateTimeKind.Local).AddTicks(4920), "LocalizationSeedData", false, "Electric Guitar", "de", null, null, "SectionDto", "Elektrische Gitarre" },
                    { new Guid("193255c8-eb77-4726-9e1a-feda91ce21d3"), new DateTime(2021, 11, 6, 14, 56, 26, 603, DateTimeKind.Local).AddTicks(6808), "LocalizationSeedData", false, "Bag Pipes", "de", null, null, "SectionDto", "Dudelsack" },
                    { new Guid("39c16c14-ef4d-4ed0-81e8-b3e36e72f8bc"), new DateTime(2021, 11, 6, 14, 56, 26, 600, DateTimeKind.Local).AddTicks(297), "LocalizationSeedData", false, "Guitars", "de", null, null, "SectionDto", "Gitarre" },
                    { new Guid("8675ee6c-c0d4-480d-8bcc-0f7ad2eedee0"), new DateTime(2021, 11, 6, 14, 56, 26, 599, DateTimeKind.Local).AddTicks(2439), "LocalizationSeedData", false, "Accordion", "de", null, null, "SectionDto", "Akkordeon" },
                    { new Guid("72c2aa5f-2fe0-4b3c-9051-796fb2f87b9d"), new DateTime(2021, 11, 6, 14, 56, 26, 598, DateTimeKind.Local).AddTicks(5252), "LocalizationSeedData", false, "Synthesizer", "de", null, null, "SectionDto", "Synthesizer" },
                    { new Guid("fe4083c6-27c5-4590-b8ef-cc36ebafc191"), new DateTime(2021, 11, 6, 14, 56, 26, 600, DateTimeKind.Local).AddTicks(7748), "LocalizationSeedData", false, "Acoustic Guitar", "de", null, null, "SectionDto", "Akustik-Gitarre" }
                });

            migrationBuilder.UpdateData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("52fad37d-23a7-4515-9b77-3ee3bda03b9a"),
                column: "name",
                value: "CD Recording");

            migrationBuilder.UpdateData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("597bf9bc-4fad-433f-810d-ae4de4ac3bde"),
                column: "name",
                value: "Bank Account Expired");

            migrationBuilder.UpdateData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("63a6b9a9-30a8-4cdb-983b-336b587069cb"),
                column: "name",
                value: "Rehearsal Weekend");

            migrationBuilder.UpdateData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("77c68dbb-a627-4053-829e-86c555754f60"),
                column: "name",
                value: "Not Invited");

            migrationBuilder.UpdateData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("7efd1bdd-67b5-4706-a1f4-9d67eea05e5d"),
                column: "name",
                value: "Incorrect Bank Details");

            migrationBuilder.UpdateData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("7f6b69f3-4fe8-4b0c-a586-38a661c60af5"),
                column: "name",
                value: "Concert Tour");

            migrationBuilder.UpdateData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("b0f67138-7488-4c68-ad4c-63fce6f862cc"),
                column: "name",
                value: "Other (see comment field)");

            migrationBuilder.UpdateData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("c36e8662-2740-49c7-87dd-3c301ef86909"),
                column: "name",
                value: "Return Debit Received");

            migrationBuilder.UpdateData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("f2a6ef3d-bb32-4505-83a5-2cb9f611ce0f"),
                column: "name",
                value: "Special Project");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("038b4791-b99b-4dad-ac23-33a0d1ca53c0"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("03b8dead-18d1-46fc-998d-0fb3916dd526"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("054aa861-a6b6-473e-999d-1a4245bba25b"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("0574f19c-2061-4b88-bf0e-ab64fbbe2dbc"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("06be4626-f021-41b9-89a0-89ee3d4a40d6"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("0b418529-b6f6-4808-b570-fc5fcf2d7486"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("0c182262-a9e4-4c89-bb93-5550de698271"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("0c242960-8ba4-48be-be67-f0e83881c519"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("0cf37641-d3c0-4120-ad1b-d76e14507a57"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("0d564a24-c517-4c5c-8240-b21a240613df"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("0e4ce182-f536-40bc-8f3e-77e6c8948e99"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("0e8f1957-9ecf-4301-812b-3407427cf5ac"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("124d2b21-06bc-4b4b-b981-a077d10159df"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("132942e0-9b2e-44aa-918f-a94a95728059"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("1365f7af-a435-484e-8f4c-a1f3e00d8a8d"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("13e0657b-062b-427a-a61e-3cb74ef6fb3f"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("1575ef92-8714-4bdb-a3ee-14c95f544e14"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("193255c8-eb77-4726-9e1a-feda91ce21d3"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("1bb2ad85-c8cf-4ec2-b685-ed28f1f40f5f"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("1c74f84c-ac85-4576-ab8d-ec8ad6a1ac16"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("1ce6e399-db60-4b4c-89eb-d8e547e900ce"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("1d281676-51d0-426d-b307-c1db99260485"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("1dc469c0-2dbf-4dd1-be48-31fb4aef2a14"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("20712e04-5fcc-478f-9f3f-f32a91595344"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("207688d3-bde2-4642-84bf-ce6de8c170de"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("2307503a-3355-45c7-807b-c467fdc4a6bd"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("26f71071-b529-4a7d-8d8a-dd6fdb3fdae0"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("280e101d-0cbf-4473-9a4f-df5956b54516"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("2c875289-6ce9-44a5-aa51-06a3e60aa32c"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("2ce5ebda-c43e-4e73-902b-0444dd3ae434"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("2d705575-c129-4914-9211-4c6b8c865687"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("2d9e912f-c393-4237-ad33-a7f4b0410412"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("312d4540-4141-48c3-80e8-95ff26fca1d9"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("31f5f9b8-c3aa-4533-b10b-7172c75d27d8"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("33be87b2-d85a-4060-a902-9ef2bafe55c8"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("34ceac7a-24ee-43d5-ae17-764ee266b409"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("35a3df40-7439-4e1d-9872-c937be1d4ace"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("37108879-01e4-4e58-a0ea-69ff235cbf44"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("38b60d3a-250c-4d88-9605-6e69103b7814"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("38e33e88-0bd2-4c63-b127-e584b4d7eeaf"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("39c16c14-ef4d-4ed0-81e8-b3e36e72f8bc"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("39e5d409-aa17-4bcc-94dc-dc5b576fd910"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("40774af5-2c23-47a4-b016-6a2178d4932b"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("42e362f5-1919-4907-8d6e-c15efa09a081"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("4472ed16-05dd-44a0-9364-43c17d5775ca"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("4646cfa2-b845-4f10-86c5-0d98dab9a97f"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("4899a356-74c0-4f48-9ca5-2a716f7718ab"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("48ff5e22-631b-4ad6-8d56-7a93ec74b3f0"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("4a2e2fd3-beab-4770-b000-e98ad3200363"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("4cbc42c1-809d-4330-bd87-bf8b0a32f720"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("4e82d8e0-35af-462d-a7cf-209d947b2b18"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("4fc4c991-a38d-4bfd-8b6a-43f38f5cf871"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("5100a0ad-25ac-4e7d-a7f9-c9816b02be2e"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("563c7c31-3976-43e0-ac08-e8251004d647"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("599e4267-2735-440e-a380-1ffed11eadd0"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("5b0f922b-28be-4e7a-8954-24a022cc32cd"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("5fc07780-7ed3-4c41-bc3f-a56f607d74d7"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("60a45401-d501-4f63-b730-0acf151a548e"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("631a8511-ae67-43c2-acfe-c8938e81e105"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("647d6a09-d4b7-4ec2-b7e4-0133949c1de7"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("65a54836-e73e-4a2a-83c9-fada544d6ecd"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("65b7ed5d-5444-4ce1-a3e6-af93aaabffd6"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("6769eaa6-81d1-4d4b-bc6d-5bde93c62c26"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("676b04c8-4256-4008-9fc4-be62ea8f5dd0"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("6799f970-c22a-41ab-a817-b493ee68b8f3"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("681c0175-abb7-46bd-bdd7-98e01aa8952d"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("68a3db4a-a62b-4f25-8a65-55db89849ac7"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("68a707dc-1aa5-405c-b8cb-62d85e196c77"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("68df3bbc-f974-4a90-866c-d324fa513a5e"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("690cee26-4cac-4bab-a26a-4328247207f3"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("6918a90d-0582-4991-937a-60a6c006e538"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("6a8737b8-8aec-4dfd-873e-593487985bee"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("6aed06d4-2c86-414d-bf15-bce230d4d0e3"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("6cb115c3-f287-4190-b302-0004945eb33b"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("6d9e6fc8-472b-4637-963b-f3ca5e9e92d3"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("6dc99d30-b059-47ea-89f4-c83b2b7936d0"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("6defbec6-09e1-4714-97c7-688a08ab71b9"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("6f17f440-94ea-4780-87e1-1c097e7936e3"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("6f85232d-d967-4318-b065-f715fa4af490"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("6fc929f4-d573-459b-9821-b3f275abc18b"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("70741068-6853-49d3-911a-69ad0776a633"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("72c2aa5f-2fe0-4b3c-9051-796fb2f87b9d"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("76625cc9-a4f5-4d50-aee8-2ea349c5f822"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("7a7fbead-646e-44d8-b1ec-3db050c95850"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("7f1b7318-02f5-49c9-9cd1-ace4776d187f"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("80eedc07-10c2-41cc-b3e2-c55ca9e09d69"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("81195307-6595-44c1-b5fa-f3307ab005ea"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("82706e73-83f8-445a-8b50-ed6bc495c116"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("833bae79-aa89-4736-8bd0-4158b080509e"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("8340227d-9f21-4dcc-82dc-7f9836f91223"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("8345e5e3-03ca-4d55-a893-9b9a78ac1115"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("855e5d1b-b4e8-4329-a6c2-4e190c57f42e"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("85aa2777-9555-4751-8c68-fab87499f24a"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("8675ee6c-c0d4-480d-8bcc-0f7ad2eedee0"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("874e2e22-0ba1-4578-89d7-1a7b74741283"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("87e41c30-e5bc-4060-9549-ef7820385c8a"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("89cf4e82-589a-4349-a61e-5c0958de1712"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("8a1f834e-d050-43db-9a5f-cf96996c6572"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("8c0cb6e9-5b08-4a45-9c4a-bdf32634f25c"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("8c6c4e59-8396-4975-b311-8c865cc48bb3"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("906c9e51-590b-4142-a527-c868fb21d861"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("91733935-2eae-4710-8c83-e522aaa45ce5"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("91a88417-2fe8-4b67-9e94-0c9ae7ed0878"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("91cf97e6-b330-4945-bf35-350f05c11770"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("93eb89ec-47bc-4392-b78d-e434a566f217"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("950312be-202e-4d3e-b5d5-0b59d1d6b861"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("9656d314-289d-4dc5-ad00-ea6579b56363"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("982add29-f7ae-483a-b6db-19c83ce4580f"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("9860c80a-fa54-49e6-b314-ba895bd31348"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("98622c74-c0b3-44f3-aed7-c184523837f4"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("997c054a-f8c3-44b8-a5f4-f0369ae7fc7c"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("9b245b7d-864e-4f33-b06e-fe5e4d449339"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("9c318bca-fa1e-49fa-920b-dae2b0a23b5c"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("9e4fb163-4a73-4cdd-8f12-bca0423b7852"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("a0233148-7245-4f67-a5ad-bd83703013eb"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("a40cae79-a99d-41db-a198-2bb396ca2c0f"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("a43c0bfe-debc-4ab9-9dd7-b6728b5ddda4"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("a60f5161-3a7c-4872-afa0-af9ce3c65a9d"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("a7851a46-abbc-4960-a26c-ce23f0e71bbc"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("a8dc77f8-4674-4c88-b6ad-d8fd72eea57e"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("ab328d92-3e3f-4815-b8d1-172b64cee552"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("aba3dddb-f2d3-4067-ac51-da48d5bfe359"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("ad5ce3ec-e819-4a59-86c7-ef277e8bf9c0"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("af7b823e-868e-47e1-862f-be5916ed40fa"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("af9fe89a-d491-409f-af60-4be1421b0569"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("afa84594-0d55-49a8-b8b7-7138be7daca4"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("afeaddef-bc32-4fd5-8292-bdeafd0a8c2a"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("b01a1500-cf94-4b49-9256-1aec9ab7d5f7"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("b131383c-d3e5-4222-8556-61aa6f59aa0e"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("b2c4833e-6ac6-4f3e-bf19-5c01ecf7b4f7"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("b2d434da-a3cf-46ff-8a47-7fb2c8e1f87c"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("b2d699b8-e3ab-4cb9-8a1e-cd671e2fac02"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("b578ba36-5297-416f-9946-e4444c5f4048"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("b70c6131-2413-4da3-97c1-1a319b725db1"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("ba0385b4-251c-4fbf-b3f2-379d02b44132"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("bb2202cb-c94b-4818-8e00-d8d561f54b2c"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("bc23ca38-31c9-43f7-81ef-1c330ba48385"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("bc51b97c-95e2-4ee0-be43-05538dd253f8"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("c075211f-30c6-4099-83ec-731c18b3e41c"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("c1c9874d-ace4-40d8-994d-ee920575a892"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("c20eec3a-ed3d-45fc-bc32-beef053f543f"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("c465625f-4800-4dc5-a8b2-b0b62f438fb6"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("c5a807e4-9698-4d35-83d8-e23898d2557e"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("c658f430-aa46-4d25-ae11-da8fadc1655c"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("c7cb8a58-833c-4759-8919-404e6fcd68fd"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("c7f9e550-ca1b-4b36-a932-46a115d0f229"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("c81554b8-c4f1-46ad-a9d2-80353b6c3795"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("c9f96e9b-5829-48d9-a58c-c3fc3cb284a1"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("cc54cb2a-30b5-473b-8d31-7788410bbc58"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("cd122c30-1cac-414d-8413-d54d801be26e"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("cd15676c-3cfd-4578-a0a9-65e0844e8e21"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("cda58b4a-7dc4-48ea-9649-40e4901245e5"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("d090e733-e5c9-4975-afa9-306af576d6d6"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("d1462152-541a-44db-8954-dfb802b9e0ff"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("d148dfb5-a9eb-41e2-9d88-acbc109d3bd8"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("d29cb388-3f9f-49e2-8835-098c7e1b7e67"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("d2ac9f50-2d73-47d9-a446-3eb3ded478e8"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("d7d6eb42-b9c7-4ffd-9187-aafaa4c7748d"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("d861a2fc-af0b-412f-ac38-3086bdf8b6d2"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("da208786-f55a-4b32-92c2-365184ffe604"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("da7eef28-30e2-4040-96bc-90a7b8d7a60d"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("dae23eca-d2a0-482d-95fd-c1d1ae7cb623"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("db180a62-be30-4e6c-94ce-efeec5cac085"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("dd2f52db-71ce-4022-bda4-6118b9249188"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("dd82b8cb-525c-4b7c-b80f-7df6865b7add"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("e4b360c5-d035-47c1-910d-c6a78869bd12"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("e7446f94-b4e0-455a-a42b-f7a827b8fa11"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("e77f675d-2c2e-4e09-aa49-14f662044d9e"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("e9320576-debc-49f5-b3da-4c12f0691c06"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("f067cffe-e377-4fec-b96a-da9ff784e761"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("f304ad53-6bb7-45fe-a11b-7f1eaf79d6df"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("f39d1e82-ed27-4afe-8f43-19ed4eee917a"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("f3d8fc29-afc7-4112-9fd6-16f0598475e7"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("f678e4b3-7c10-4a9e-8483-05d43361137a"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("fbe724f3-3ddb-4010-a15e-e9dfafa99c2d"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("fca0817b-a1f3-4caf-a106-309f52e2e40f"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("fd5dba23-9685-4821-9237-e182dafbcb52"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("fd88fd6d-c5db-47f8-a844-b4c76403e6c5"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("fd91326e-ea12-4f46-874c-5d52b646db71"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("fe0db7ed-9642-4c53-b748-bfa89dc56835"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("fe4083c6-27c5-4590-b8ef-cc36ebafc191"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("ff484291-0f10-4866-948c-722ab37f3501"));

            migrationBuilder.InsertData(
                table: "localizations",
                columns: new[] { "id", "created_at", "created_by", "deleted", "key", "localization_culture", "modified_at", "modified_by", "resource_key", "text" },
                values: new object[,]
                {
                    { new Guid("1cc1999c-eec3-4891-a833-798a8ec6baae"), new DateTime(2021, 6, 16, 15, 30, 19, 386, DateTimeKind.Local).AddTicks(6960), "LocalizationSeedData", false, "Invalid token supplied", "en", null, null, "Validator", "Please try to login again" },
                    { new Guid("4899a356-74c0-4f48-9ca5-2a716f7718ab"), new DateTime(2021, 6, 16, 15, 30, 19, 389, DateTimeKind.Local).AddTicks(7493), "LocalizationSeedData", false, "This request requires a valid JWT access token to be provided", "de", null, null, "Validator", "Diese Anfrage erfordert einen gültigen JWT Token" },
                    { new Guid("39e5d409-aa17-4bcc-94dc-dc5b576fd910"), new DateTime(2021, 6, 16, 15, 30, 19, 390, DateTimeKind.Local).AddTicks(1201), "LocalizationSeedData", false, "Invalid token supplied", "de", null, null, "Validator", "Ungültiges Token angegeben" },
                    { new Guid("c9f96e9b-5829-48d9-a58c-c3fc3cb284a1"), new DateTime(2021, 6, 16, 15, 30, 19, 390, DateTimeKind.Local).AddTicks(5458), "LocalizationSeedData", false, "EndTime must be later than StartTime", "de", null, null, "Validator", "Endzeit muss später Startzeit sein" },
                    { new Guid("da208786-f55a-4b32-92c2-365184ffe604"), new DateTime(2021, 6, 16, 15, 30, 19, 390, DateTimeKind.Local).AddTicks(9011), "LocalizationSeedData", false, "Password must be at least 6 characters", "de", null, null, "Validator", "Das Passwort muss mindestens 6 Zeichen enthalten" },
                    { new Guid("f067cffe-e377-4fec-b96a-da9ff784e761"), new DateTime(2021, 6, 16, 15, 30, 19, 391, DateTimeKind.Local).AddTicks(2502), "LocalizationSeedData", false, "Password must contain at least one uppercase letter", "de", null, null, "Validator", "Das Passwort muss mindestens einen Großbuchstaben enthalten" },
                    { new Guid("a40cae79-a99d-41db-a198-2bb396ca2c0f"), new DateTime(2021, 6, 16, 15, 30, 19, 391, DateTimeKind.Local).AddTicks(7030), "LocalizationSeedData", false, "Password must contain at least one lowercase letter", "de", null, null, "Validator", "Das Passwort muss mindestens einen Kleinbuchstaben enthalten" },
                    { new Guid("fbe724f3-3ddb-4010-a15e-e9dfafa99c2d"), new DateTime(2021, 6, 16, 15, 30, 19, 392, DateTimeKind.Local).AddTicks(748), "LocalizationSeedData", false, "Password must contain at least one digit", "de", null, null, "Validator", "Das Passwort muss mindestens eine Zahl enthalten" },
                    { new Guid("68df3bbc-f974-4a90-866c-d324fa513a5e"), new DateTime(2021, 6, 16, 15, 30, 19, 392, DateTimeKind.Local).AddTicks(6917), "LocalizationSeedData", false, "Password must contain at least one special character", "de", null, null, "Validator", "Das Passwort muss mindestens ein Sonderzeichen enthalten" },
                    { new Guid("89cf4e82-589a-4349-a61e-5c0958de1712"), new DateTime(2021, 6, 16, 15, 30, 19, 393, DateTimeKind.Local).AddTicks(1208), "LocalizationSeedData", false, "Username may only contain alphanumeric characters", "de", null, null, "Validator", "Der Benutzername darf nur alphanumerische Zeichen enthalten" },
                    { new Guid("2c875289-6ce9-44a5-aa51-06a3e60aa32c"), new DateTime(2021, 6, 16, 15, 30, 19, 393, DateTimeKind.Local).AddTicks(4642), "LocalizationSeedData", false, "The project is already linked to the appointment", "de", null, null, "Validator", "Das Projekt ist bereits dem Termin zugeordnet" },
                    { new Guid("afeaddef-bc32-4fd5-8292-bdeafd0a8c2a"), new DateTime(2021, 6, 16, 15, 30, 19, 393, DateTimeKind.Local).AddTicks(8104), "LocalizationSeedData", false, "The room is already linked to the appointment", "de", null, null, "Validator", "Der Raum ist bereits dem Termin zugeordnet" },
                    { new Guid("0cf37641-d3c0-4120-ad1b-d76e14507a57"), new DateTime(2021, 6, 16, 15, 30, 19, 394, DateTimeKind.Local).AddTicks(5913), "LocalizationSeedData", false, "The project is not linked to the appointment", "de", null, null, "Validator", "Das Projekt ist dem Termin nicht zugeordnet" },
                    { new Guid("cd15676c-3cfd-4578-a0a9-65e0844e8e21"), new DateTime(2021, 6, 16, 15, 30, 19, 394, DateTimeKind.Local).AddTicks(9976), "LocalizationSeedData", false, "The room is not linked to the appointment", "de", null, null, "Validator", "Der Raum ist dem Termin nicht zugeordnet" },
                    { new Guid("6918a90d-0582-4991-937a-60a6c006e538"), new DateTime(2021, 6, 16, 15, 30, 19, 395, DateTimeKind.Local).AddTicks(3734), "LocalizationSeedData", false, "The section is not linked to the Appointment", "de", null, null, "Validator", "Die Sektion ist dem Termin nicht zugeordnet" },
                    { new Guid("1365f7af-a435-484e-8f4c-a1f3e00d8a8d"), new DateTime(2021, 6, 16, 15, 30, 19, 395, DateTimeKind.Local).AddTicks(7653), "LocalizationSeedData", false, "Incorrect password supplied", "de", null, null, "Validator", "Inkorrektes Passwort angegeben" },
                    { new Guid("af9fe89a-d491-409f-af60-4be1421b0569"), new DateTime(2021, 6, 16, 15, 30, 19, 394, DateTimeKind.Local).AddTicks(1976), "LocalizationSeedData", false, "The section is already linked to the Appointment", "de", null, null, "Validator", "Die Sektion ist bereits dem Termin zugeordnet" },
                    { new Guid("c5a807e4-9698-4d35-83d8-e23898d2557e"), new DateTime(2021, 6, 16, 15, 30, 19, 396, DateTimeKind.Local).AddTicks(4978), "LocalizationSeedData", false, "Your email address is not confirmed. Please confirm your email address first", "de", null, null, "Validator", "Deine Email wurde noch nicht bestätigt. Bitte bestätige zuerst deine Email" },
                    { new Guid("cc54cb2a-30b5-473b-8d31-7788410bbc58"), new DateTime(2021, 6, 16, 15, 30, 19, 401, DateTimeKind.Local).AddTicks(4545), "LocalizationSeedData", false, "Admin", "de", null, null, "RoleDto", "Administrator" },
                    { new Guid("9860c80a-fa54-49e6-b314-ba895bd31348"), new DateTime(2021, 6, 16, 15, 30, 19, 401, DateTimeKind.Local).AddTicks(1189), "LocalizationSeedData", false, "Staff", "de", null, null, "RoleDto", "Mitarbeiter" },
                    { new Guid("676b04c8-4256-4008-9fc4-be62ea8f5dd0"), new DateTime(2021, 6, 16, 15, 30, 19, 400, DateTimeKind.Local).AddTicks(7391), "LocalizationSeedData", false, "Performer", "de", null, null, "RoleDto", "Künstler" },
                    { new Guid("f39d1e82-ed27-4afe-8f43-19ed4eee917a"), new DateTime(2021, 6, 16, 15, 30, 19, 396, DateTimeKind.Local).AddTicks(1240), "LocalizationSeedData", false, "The user could not be found", "de", null, null, "Validator", "Der Benutzer konnte nicht gefunden werden" },
                    { new Guid("fd5dba23-9685-4821-9237-e182dafbcb52"), new DateTime(2021, 6, 16, 15, 30, 19, 399, DateTimeKind.Local).AddTicks(9816), "LocalizationSeedData", false, "Volunteers", "de", null, null, "SectionDto", "Freiwillige" },
                    { new Guid("6aed06d4-2c86-414d-bf15-bce230d4d0e3"), new DateTime(2021, 6, 16, 15, 30, 19, 399, DateTimeKind.Local).AddTicks(5924), "LocalizationSeedData", false, "Visitors", "de", null, null, "SectionDto", "Besucher" },
                    { new Guid("b2d699b8-e3ab-4cb9-8a1e-cd671e2fac02"), new DateTime(2021, 6, 16, 15, 30, 19, 399, DateTimeKind.Local).AddTicks(2187), "LocalizationSeedData", false, "Members", "de", null, null, "SectionDto", "Mitglieder" },
                    { new Guid("563c7c31-3976-43e0-ac08-e8251004d647"), new DateTime(2021, 6, 16, 15, 30, 19, 400, DateTimeKind.Local).AddTicks(3657), "LocalizationSeedData", false, "Suppliers", "de", null, null, "SectionDto", "Zulieferer" },
                    { new Guid("631a8511-ae67-43c2-acfe-c8938e81e105"), new DateTime(2021, 6, 16, 15, 30, 19, 398, DateTimeKind.Local).AddTicks(3894), "LocalizationSeedData", false, "Performers", "de", null, null, "SectionDto", "Künstler" },
                    { new Guid("5b0f922b-28be-4e7a-8954-24a022cc32cd"), new DateTime(2021, 6, 16, 15, 30, 19, 398, DateTimeKind.Local).AddTicks(74), "LocalizationSeedData", false, "A region with the requested name does already exist", "de", null, null, "Validator", "Eine Region mit diesem Namen existiert bereits" },
                    { new Guid("b70c6131-2413-4da3-97c1-1a319b725db1"), new DateTime(2021, 6, 16, 15, 30, 19, 397, DateTimeKind.Local).AddTicks(6316), "LocalizationSeedData", false, "Email already exists", "de", null, null, "Validator", "Die Email existiert bereits" },
                    { new Guid("906c9e51-590b-4142-a527-c868fb21d861"), new DateTime(2021, 6, 16, 15, 30, 19, 397, DateTimeKind.Local).AddTicks(2433), "LocalizationSeedData", false, "Username already exists", "de", null, null, "Validator", "Der Benutzername existiert bereits" },
                    { new Guid("20712e04-5fcc-478f-9f3f-f32a91595344"), new DateTime(2021, 6, 16, 15, 30, 19, 396, DateTimeKind.Local).AddTicks(8890), "LocalizationSeedData", false, "Your account is locked. Kindly wait for 10 minutes and try again", "de", null, null, "Validator", "Dein Konto wurde gesperrt. Bitte warte 10 Minuten und versuche es anschließend erneut" },
                    { new Guid("8c6c4e59-8396-4975-b311-8c865cc48bb3"), new DateTime(2021, 6, 16, 15, 30, 19, 398, DateTimeKind.Local).AddTicks(7809), "LocalizationSeedData", false, "Orchestra", "de", null, null, "SectionDto", "Orchester" }
                });

            migrationBuilder.UpdateData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("52fad37d-23a7-4515-9b77-3ee3bda03b9a"),
                column: "name",
                value: "CD recording");

            migrationBuilder.UpdateData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("597bf9bc-4fad-433f-810d-ae4de4ac3bde"),
                column: "name",
                value: "Bank account expired");

            migrationBuilder.UpdateData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("63a6b9a9-30a8-4cdb-983b-336b587069cb"),
                column: "name",
                value: "Rehearsal weekend");

            migrationBuilder.UpdateData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("77c68dbb-a627-4053-829e-86c555754f60"),
                column: "name",
                value: "Not invited");

            migrationBuilder.UpdateData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("7efd1bdd-67b5-4706-a1f4-9d67eea05e5d"),
                column: "name",
                value: "Incorrect bank details");

            migrationBuilder.UpdateData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("7f6b69f3-4fe8-4b0c-a586-38a661c60af5"),
                column: "name",
                value: "Concert tour");

            migrationBuilder.UpdateData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("b0f67138-7488-4c68-ad4c-63fce6f862cc"),
                column: "name",
                value: "Return debit received");

            migrationBuilder.UpdateData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("c36e8662-2740-49c7-87dd-3c301ef86909"),
                column: "name",
                value: "Return debit received");

            migrationBuilder.UpdateData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("f2a6ef3d-bb32-4505-83a5-2cb9f611ce0f"),
                column: "name",
                value: "Special project");
        }
    }
}
