using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orso.Arpa.Persistence.Migrations
{
    public partial class CleanupCompletionTranslations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("38e33e88-0bd2-4c63-b127-e584b4d7eeaf"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("5100a0ad-25ac-4e7d-a7f9-c9816b02be2e"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("6defbec6-09e1-4714-97c7-688a08ab71b9"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("b131383c-d3e5-4222-8556-61aa6f59aa0e"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("bc51b97c-95e2-4ee0-be43-05538dd253f8"));

            migrationBuilder.UpdateData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("0b418529-b6f6-4808-b570-fc5fcf2d7486"),
                column: "text",
                value: "E-Bass");

            migrationBuilder.UpdateData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("1575ef92-8714-4bdb-a3ee-14c95f544e14"),
                column: "text",
                value: "Marimbaphon");

            migrationBuilder.UpdateData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("4fc4c991-a38d-4bfd-8b6a-43f38f5cf871"),
                column: "text",
                value: "Blech");

            migrationBuilder.UpdateData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("9b245b7d-864e-4f33-b06e-fe5e4d449339"),
                column: "text",
                value: "E-Gitarre");

            migrationBuilder.UpdateData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("a8dc77f8-4674-4c88-b6ad-d8fd72eea57e"),
                column: "text",
                value: "Viola");

            migrationBuilder.UpdateData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("ba0385b4-251c-4fbf-b3f2-379d02b44132"),
                column: "text",
                value: "Violine");

            migrationBuilder.InsertData(
                table: "localizations",
                columns: new[] { "id", "created_at", "created_by", "deleted", "key", "localization_culture", "modified_at", "modified_by", "resource_key", "text" },
                values: new object[,]
                {
                    { new Guid("0108dcdb-caa7-4783-b69e-5c282a1e1770"), new DateTime(2022, 2, 19, 21, 8, 46, 632, DateTimeKind.Local).AddTicks(2300), "LocalizationSeedData", false, "Bagpipes", "de", null, null, "SectionDto", "Dudelsack" },
                    { new Guid("0ce3a0d7-919f-43cd-90f1-79a3e06a2659"), new DateTime(2022, 2, 19, 21, 8, 46, 737, DateTimeKind.Local).AddTicks(1180), "LocalizationSeedData", false, "Gladly", "de", null, null, "SelectValueDto", "Gerne anfragen" },
                    { new Guid("115814e0-fe21-433d-a65f-3e725c2e7ab0"), new DateTime(2022, 2, 19, 21, 8, 46, 737, DateTimeKind.Local).AddTicks(7620), "LocalizationSeedData", false, "Emergency only", "de", null, null, "SelectValueDto", "Nur im Notfall" },
                    { new Guid("169900b0-43a9-4182-a402-c1bd49bf540b"), new DateTime(2022, 2, 19, 21, 8, 46, 646, DateTimeKind.Local).AddTicks(8600), "LocalizationSeedData", false, "Timpani", "de", null, null, "SectionDto", "Pauken" },
                    { new Guid("1adf93d9-ba07-4e15-99ac-8ec37bfd2c47"), new DateTime(2022, 2, 19, 21, 8, 46, 641, DateTimeKind.Local).AddTicks(5530), "LocalizationSeedData", false, "Bass Trombone", "de", null, null, "SectionDto", "Bass-Posaune" },
                    { new Guid("22f4fe1b-eae7-46cd-9363-4103b48f7611"), new DateTime(2022, 2, 19, 21, 8, 46, 739, DateTimeKind.Local).AddTicks(590), "LocalizationSeedData", false, "Never again", "de", null, null, "SelectValueDto", "Nie wieder" },
                    { new Guid("2f01dec8-155e-4da8-a321-62a5c53f65f4"), new DateTime(2022, 2, 19, 21, 8, 46, 627, DateTimeKind.Local).AddTicks(9480), "LocalizationSeedData", false, "Tenor Saxophone", "de", null, null, "SectionDto", "Tenor-Saxophon" },
                    { new Guid("30000f37-7a8f-47a0-98bb-f560777c4737"), new DateTime(2022, 2, 19, 21, 8, 46, 638, DateTimeKind.Local).AddTicks(9860), "LocalizationSeedData", false, "Cornet", "de", null, null, "SectionDto", "Kornett" },
                    { new Guid("36b0fb71-afcd-4473-a47b-a860f66409b3"), new DateTime(2022, 2, 19, 21, 8, 46, 738, DateTimeKind.Local).AddTicks(4180), "LocalizationSeedData", false, "For contacts only", "de", null, null, "SelectValueDto", "Aquisekontakt" },
                    { new Guid("37db8c9b-d375-4531-bbec-df3ea5055e24"), new DateTime(2022, 2, 19, 21, 8, 46, 713, DateTimeKind.Local).AddTicks(6040), "LocalizationSeedData", false, "Orchestra Concert Rate: 9€/11€ at 12h", "de", null, null, "SelectValueDto", "Orchester Konzertpauschale 9€/11€ bei 12h" },
                    { new Guid("3b5f7b27-ceb0-4446-9713-e5cca94691d1"), new DateTime(2022, 2, 19, 21, 8, 46, 599, DateTimeKind.Local).AddTicks(830), "LocalizationSeedData", false, "Contractors", "de", null, null, "SectionDto", "Vertragspartner" },
                    { new Guid("3e227927-ba6b-4505-a7b3-22a2fe1d0b5e"), new DateTime(2022, 2, 19, 21, 8, 46, 601, DateTimeKind.Local).AddTicks(7510), "LocalizationSeedData", false, "Vocal Coach", "de", null, null, "SectionDto", "Stimmbildner:in" },
                    { new Guid("46fa11bd-4c1b-4296-a178-dac320247b40"), new DateTime(2022, 2, 19, 21, 8, 46, 621, DateTimeKind.Local).AddTicks(6490), "LocalizationSeedData", false, "Basset Horn", "de", null, null, "SectionDto", "Bassett-Horn" },
                    { new Guid("487f31e7-b6c8-4051-a36d-cdb94e106a8a"), new DateTime(2022, 2, 19, 21, 8, 46, 630, DateTimeKind.Local).AddTicks(8730), "LocalizationSeedData", false, "Contra Bassoon", "de", null, null, "SectionDto", "Kontrafagott" },
                    { new Guid("500c6c2d-722c-4801-a26d-bc1f0ef11d0b"), new DateTime(2022, 2, 19, 21, 8, 46, 662, DateTimeKind.Local).AddTicks(5930), "LocalizationSeedData", false, "Electric Guitar (Orchestra)", "de", null, null, "SectionDto", "E-Gitarre (Orchester)" },
                    { new Guid("52a0c124-95d8-4794-be63-5db904dda2fc"), new DateTime(2022, 2, 19, 21, 8, 46, 712, DateTimeKind.Local).AddTicks(9160), "LocalizationSeedData", false, "Orchestra Concert Rate: 9€/11€ at 10h", "de", null, null, "SelectValueDto", "Orchester Konzertpauschale 9€/11€ bei 10h" },
                    { new Guid("5897d4dd-f6a6-4c11-a36d-d697abe04f1d"), new DateTime(2022, 2, 19, 21, 8, 46, 610, DateTimeKind.Local).AddTicks(4980), "LocalizationSeedData", false, "Bass", "de", null, null, "SectionDto", "Bass (Chor)" },
                    { new Guid("5e754fb1-46af-4607-9579-d4430ab054ed"), new DateTime(2022, 2, 19, 21, 8, 46, 631, DateTimeKind.Local).AddTicks(5260), "LocalizationSeedData", false, "Contraforte", "de", null, null, "SectionDto", "Kontraforte" },
                    { new Guid("629e32a1-d497-48ca-9c97-d35fd8111488"), new DateTime(2022, 2, 19, 21, 8, 46, 601, DateTimeKind.Local).AddTicks(460), "LocalizationSeedData", false, "Répétiteur", "de", null, null, "SectionDto", "Korrepetitor" },
                    { new Guid("641c41dd-1d18-4695-94cb-315e36b52b1c"), new DateTime(2022, 2, 19, 21, 8, 46, 712, DateTimeKind.Local).AddTicks(2130), "LocalizationSeedData", false, "Orchestra Concert Rate: 1808", "de", null, null, "SelectValueDto", "Orchester Konzertpauschale 1808" },
                    { new Guid("6935d438-4967-411d-a0ca-f3b4e5790a7d"), new DateTime(2022, 2, 19, 21, 8, 46, 648, DateTimeKind.Local).AddTicks(1730), "LocalizationSeedData", false, "Drum Set (Band)", "de", null, null, "SectionDto", "Schlagzeug (Band)" },
                    { new Guid("6f54ce52-0db0-4a26-8faa-42e2cce46167"), new DateTime(2022, 2, 19, 21, 8, 46, 599, DateTimeKind.Local).AddTicks(7360), "LocalizationSeedData", false, "Conductor", "de", null, null, "SectionDto", "Dirigent" },
                    { new Guid("91a04cfb-c85c-4113-b36d-eaf1b24e9db7"), new DateTime(2022, 2, 19, 21, 8, 46, 623, DateTimeKind.Local).AddTicks(2090), "LocalizationSeedData", false, "Double Bass Clarinet", "de", null, null, "SectionDto", "Kontrabass-Klarinette" },
                    { new Guid("93194d04-45f7-4889-9c9b-680d5a1305b8"), new DateTime(2022, 2, 19, 21, 8, 46, 652, DateTimeKind.Local).AddTicks(2670), "LocalizationSeedData", false, "Mallets", "de", null, null, "SectionDto", "Stabspiel" },
                    { new Guid("a57cde5e-b9e3-4786-9bbf-85f1f2cb1151"), new DateTime(2022, 2, 19, 21, 8, 46, 654, DateTimeKind.Local).AddTicks(6570), "LocalizationSeedData", false, "GlassHarp", "de", null, null, "SectionDto", "Glasharfe" },
                    { new Guid("a96d385e-18a8-45d0-b165-b513f20ed2a9"), new DateTime(2022, 2, 19, 21, 8, 46, 642, DateTimeKind.Local).AddTicks(2200), "LocalizationSeedData", false, "Double Bass Trombone", "de", null, null, "SectionDto", "Kontrabass-Posaune" },
                    { new Guid("bdffc24c-35f8-4275-86e3-bdebc402e336"), new DateTime(2022, 2, 19, 21, 8, 46, 661, DateTimeKind.Local).AddTicks(3030), "LocalizationSeedData", false, "Acoustic Guitar (Orchestra)", "de", null, null, "SectionDto", "Akustik-Gitarre (Orchester)" },
                    { new Guid("c32c7ff2-9908-41ae-ab4e-d84441354fd3"), new DateTime(2022, 2, 19, 21, 8, 46, 635, DateTimeKind.Local).AddTicks(350), "LocalizationSeedData", false, "Low Brass", "de", null, null, "SectionDto", "Tiefes Blech" },
                    { new Guid("c5bb7cf5-d79b-4040-8e74-54baf19139d1"), new DateTime(2022, 2, 19, 21, 8, 46, 634, DateTimeKind.Local).AddTicks(350), "LocalizationSeedData", false, "High Brass", "de", null, null, "SectionDto", "Hohes Blech" },
                    { new Guid("cde22ff7-841f-4075-ba19-25bd0a085d76"), new DateTime(2022, 2, 19, 21, 8, 46, 671, DateTimeKind.Local).AddTicks(910), "LocalizationSeedData", false, "Double Bass", "de", null, null, "SectionDto", "Kontrabass" },
                    { new Guid("d1360e28-f9b4-47ed-9378-0d2bf48bba22"), new DateTime(2022, 2, 19, 21, 8, 46, 640, DateTimeKind.Local).AddTicks(9130), "LocalizationSeedData", false, "Alto Trombone", "de", null, null, "SectionDto", "Alt-Posaune" },
                    { new Guid("d861b543-b1a3-448a-b1a2-df0b8bbeaebe"), new DateTime(2022, 2, 19, 21, 8, 46, 648, DateTimeKind.Local).AddTicks(8190), "LocalizationSeedData", false, "Drum Set (Orchestra)", "de", null, null, "SectionDto", "Schlagzeug (Orchester)" },
                    { new Guid("e402dfd1-ebb6-48c4-b3c8-049362682130"), new DateTime(2022, 2, 19, 21, 8, 46, 630, DateTimeKind.Local).AddTicks(1820), "LocalizationSeedData", false, "Bassoon", "de", null, null, "SectionDto", "Fagott" },
                    { new Guid("e56b2e27-7da2-42b2-83a4-c6c455c1199e"), new DateTime(2022, 2, 19, 21, 8, 46, 600, DateTimeKind.Local).AddTicks(3900), "LocalizationSeedData", false, "Assistant Conductor", "de", null, null, "SectionDto", "Dirigierassistent" },
                    { new Guid("e9f6caf6-773c-4982-ab65-9c96f6ac7cc9"), new DateTime(2022, 2, 19, 21, 8, 46, 639, DateTimeKind.Local).AddTicks(6280), "LocalizationSeedData", false, "Soprano Cornet", "de", null, null, "SectionDto", "Sopran-Kornett" },
                    { new Guid("edf764af-c96b-472a-b8c6-4342a9da71fb"), new DateTime(2022, 2, 19, 21, 8, 46, 640, DateTimeKind.Local).AddTicks(2740), "LocalizationSeedData", false, "Trombone", "de", null, null, "SectionDto", "Posaune" },
                    { new Guid("f700e62b-9ce7-4eed-94fd-e3ae18cd57c0"), new DateTime(2022, 2, 19, 21, 8, 46, 663, DateTimeKind.Local).AddTicks(8810), "LocalizationSeedData", false, "Electric Bass (Orchestra)", "de", null, null, "SectionDto", "E-Bass (Orchester)" }
                });

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("e7dd10ef-1c39-4440-9a6c-65d397f010ca"),
                column: "name",
                value: "Bass");

            migrationBuilder.UpdateData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("a10ce98a-b903-4dca-801d-3afb07711877"),
                column: "name",
                value: "Orchestra Concert Rate: 9€/11€ at 12h");

            migrationBuilder.UpdateData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("d91def3e-4c55-42c7-ac56-147846be6bfa"),
                column: "name",
                value: "Orchestra Concert Rate: 9€/11€ at 10h");

            migrationBuilder.UpdateData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("ddb23793-af96-4ea6-9b27-5e2dcfc90b65"),
                column: "name",
                value: "Orchestra Concert Rate: 1808");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("0108dcdb-caa7-4783-b69e-5c282a1e1770"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("0ce3a0d7-919f-43cd-90f1-79a3e06a2659"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("115814e0-fe21-433d-a65f-3e725c2e7ab0"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("169900b0-43a9-4182-a402-c1bd49bf540b"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("1adf93d9-ba07-4e15-99ac-8ec37bfd2c47"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("22f4fe1b-eae7-46cd-9363-4103b48f7611"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("2f01dec8-155e-4da8-a321-62a5c53f65f4"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("30000f37-7a8f-47a0-98bb-f560777c4737"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("36b0fb71-afcd-4473-a47b-a860f66409b3"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("37db8c9b-d375-4531-bbec-df3ea5055e24"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("3b5f7b27-ceb0-4446-9713-e5cca94691d1"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("3e227927-ba6b-4505-a7b3-22a2fe1d0b5e"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("46fa11bd-4c1b-4296-a178-dac320247b40"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("487f31e7-b6c8-4051-a36d-cdb94e106a8a"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("500c6c2d-722c-4801-a26d-bc1f0ef11d0b"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("52a0c124-95d8-4794-be63-5db904dda2fc"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("5897d4dd-f6a6-4c11-a36d-d697abe04f1d"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("5e754fb1-46af-4607-9579-d4430ab054ed"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("629e32a1-d497-48ca-9c97-d35fd8111488"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("641c41dd-1d18-4695-94cb-315e36b52b1c"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("6935d438-4967-411d-a0ca-f3b4e5790a7d"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("6f54ce52-0db0-4a26-8faa-42e2cce46167"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("91a04cfb-c85c-4113-b36d-eaf1b24e9db7"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("93194d04-45f7-4889-9c9b-680d5a1305b8"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("a57cde5e-b9e3-4786-9bbf-85f1f2cb1151"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("a96d385e-18a8-45d0-b165-b513f20ed2a9"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("bdffc24c-35f8-4275-86e3-bdebc402e336"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("c32c7ff2-9908-41ae-ab4e-d84441354fd3"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("c5bb7cf5-d79b-4040-8e74-54baf19139d1"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("cde22ff7-841f-4075-ba19-25bd0a085d76"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("d1360e28-f9b4-47ed-9378-0d2bf48bba22"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("d861b543-b1a3-448a-b1a2-df0b8bbeaebe"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("e402dfd1-ebb6-48c4-b3c8-049362682130"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("e56b2e27-7da2-42b2-83a4-c6c455c1199e"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("e9f6caf6-773c-4982-ab65-9c96f6ac7cc9"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("edf764af-c96b-472a-b8c6-4342a9da71fb"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("f700e62b-9ce7-4eed-94fd-e3ae18cd57c0"));

            migrationBuilder.UpdateData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("0b418529-b6f6-4808-b570-fc5fcf2d7486"),
                column: "text",
                value: "Elektrischer Bass");

            migrationBuilder.UpdateData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("1575ef92-8714-4bdb-a3ee-14c95f544e14"),
                column: "text",
                value: "Marimbaphone");

            migrationBuilder.UpdateData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("4fc4c991-a38d-4bfd-8b6a-43f38f5cf871"),
                column: "text",
                value: "Bass");

            migrationBuilder.UpdateData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("9b245b7d-864e-4f33-b06e-fe5e4d449339"),
                column: "text",
                value: "Elektrische Gitarre");

            migrationBuilder.UpdateData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("a8dc77f8-4674-4c88-b6ad-d8fd72eea57e"),
                column: "text",
                value: "Bratsche");

            migrationBuilder.UpdateData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("ba0385b4-251c-4fbf-b3f2-379d02b44132"),
                column: "text",
                value: "Geige");

            migrationBuilder.InsertData(
                table: "localizations",
                columns: new[] { "id", "created_at", "created_by", "deleted", "key", "localization_culture", "modified_at", "modified_by", "resource_key", "text" },
                values: new object[,]
                {
                    { new Guid("38e33e88-0bd2-4c63-b127-e584b4d7eeaf"), new DateTime(2021, 11, 6, 14, 56, 26, 648, DateTimeKind.Local).AddTicks(6980), "LocalizationSeedData", false, "Orchestra Concert Lump Sum 9 €/11€ at 12h", "de", null, null, "SelectValueDto", "Orchester Konzertpauschale 9€/11€ bei 12h" },
                    { new Guid("5100a0ad-25ac-4e7d-a7f9-c9816b02be2e"), new DateTime(2021, 11, 6, 14, 56, 26, 555, DateTimeKind.Local).AddTicks(3686), "LocalizationSeedData", false, "Basso", "de", null, null, "SectionDto", "Bass" },
                    { new Guid("6defbec6-09e1-4714-97c7-688a08ab71b9"), new DateTime(2021, 11, 6, 14, 56, 26, 647, DateTimeKind.Local).AddTicks(9502), "LocalizationSeedData", false, "Orchestra Concert Lump Sum 9€/11€ at 10h", "de", null, null, "SelectValueDto", "Orchester Konzertpauschale 9€/11€ bei 10h" },
                    { new Guid("b131383c-d3e5-4222-8556-61aa6f59aa0e"), new DateTime(2021, 11, 6, 14, 56, 26, 647, DateTimeKind.Local).AddTicks(1795), "LocalizationSeedData", false, "Orchesetra Concert Lump Sump 1808", "de", null, null, "SelectValueDto", "Orchester Konzertpauschale 1808" },
                    { new Guid("bc51b97c-95e2-4ee0-be43-05538dd253f8"), new DateTime(2021, 11, 6, 14, 56, 26, 567, DateTimeKind.Local).AddTicks(5400), "LocalizationSeedData", false, "Bassett Horn", "de", null, null, "SectionDto", "Bassett-Horn" }
                });

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("e7dd10ef-1c39-4440-9a6c-65d397f010ca"),
                column: "name",
                value: "Basso");

            migrationBuilder.UpdateData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("a10ce98a-b903-4dca-801d-3afb07711877"),
                column: "name",
                value: "Orchestra Concert Lump Sum 9 €/11€ at 12h");

            migrationBuilder.UpdateData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("d91def3e-4c55-42c7-ac56-147846be6bfa"),
                column: "name",
                value: "Orchestra Concert Lump Sum 9€/11€ at 10h");

            migrationBuilder.UpdateData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("ddb23793-af96-4ea6-9b27-5e2dcfc90b65"),
                column: "name",
                value: "Orchestra Concert Lump Sum 1808");
        }
    }
}
