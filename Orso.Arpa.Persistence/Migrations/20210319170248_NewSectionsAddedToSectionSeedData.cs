using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Orso.Arpa.Persistence.Migrations
{
    public partial class NewSectionsAddedToSectionSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("067647c0-3f25-449e-9212-03f39fa88f0f"),
                column: "Name",
                value: "Members");

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("0cf93477-f42f-46c3-8e3d-45ccdc54ad8c"),
                columns: new[] { "Name", "ParentId" },
                values: new object[] { "Harp", new Guid("c9403ca4-6b75-44c3-b567-e53bbd78fb75") });

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("182019da-bde2-44d7-8c77-88cfb0ce428c"),
                columns: new[] { "Name", "ParentId" },
                values: new object[] { "Organ", new Guid("614a8fd0-acfa-4268-b716-3b35a6a17b7a") });

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("48337b78-70f0-493e-911b-296632b06ef8"),
                column: "Name",
                value: "Low Female Voices");

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("614a8fd0-acfa-4268-b716-3b35a6a17b7a"),
                columns: new[] { "Name", "ParentId" },
                values: new object[] { "Keyboards", new Guid("c9403ca4-6b75-44c3-b567-e53bbd78fb75") });

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("8ed82e0e-0354-4192-8f26-5a2437e9208d"),
                columns: new[] { "Name", "ParentId" },
                values: new object[] { "Piano", new Guid("614a8fd0-acfa-4268-b716-3b35a6a17b7a") });

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("a6abdeec-8185-40ac-a418-2e422bb9adbd"),
                column: "Name",
                value: "Woodwinds");

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("b289cfe7-d66e-48d8-83a9-f4b1f7710863"),
                column: "Name",
                value: "Winds");

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("b9673cfd-7cdb-472c-86e0-1304cbb3840a"),
                column: "Name",
                value: "Low Male Voices");

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("bc6cfeb7-569d-4c22-8e80-647aed560bf0"),
                columns: new[] { "Name", "ParentId" },
                values: new object[] { "Celesta", new Guid("614a8fd0-acfa-4268-b716-3b35a6a17b7a") });

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("c9403ca4-6b75-44c3-b567-e53bbd78fb75"),
                columns: new[] { "Name", "ParentId" },
                values: new object[] { "Others", new Guid("308659d6-6014-4d2c-a62a-be75bf202e62") });

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("f6af00f5-e81c-4d85-aadd-1e33748e9a64"),
                columns: new[] { "Name", "ParentId" },
                values: new object[] { "Cembalo", new Guid("614a8fd0-acfa-4268-b716-3b35a6a17b7a") });

            migrationBuilder.InsertData(
                table: "Sections",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "ModifiedAt", "ModifiedBy", "Name", "ParentId" },
                values: new object[,]
                {
                    { new Guid("fdd5d68c-2620-47a3-80e4-64fda6dc7e3f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Low Strings", new Guid("1bde9862-3ed5-45cd-8d80-0a52c6b4c0fb") },
                    { new Guid("6a107070-daae-41fc-b27d-416d44d36374"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Contractors", null },
                    { new Guid("13802d8b-4c73-4a52-8748-20bf3ba0c2b1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Suppliers", null },
                    { new Guid("75f593aa-fd20-4c05-9300-b31dbb90712e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Volunteers", null },
                    { new Guid("b58d047f-ec04-41e9-a728-06a8a160f55b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Visitors", null },
                    { new Guid("7f811b88-e7db-461a-af5d-e249b1ce9e7d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Keyboards (Band)", new Guid("1994cb6c-877e-4d7c-aeca-26e68967c2ab") },
                    { new Guid("d787fe9a-2283-43f6-bbc8-8a098e1f1c81"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Drum Set (Band)", new Guid("1994cb6c-877e-4d7c-aeca-26e68967c2ab") },
                    { new Guid("454c2ad6-e3c8-428a-b74e-c73873159c0e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Electric Bass (Band)", new Guid("1994cb6c-877e-4d7c-aeca-26e68967c2ab") },
                    { new Guid("48833c1b-cbc1-43b2-a4c5-f1fa4289f5ab"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Electric Guitar (Band)", new Guid("1994cb6c-877e-4d7c-aeca-26e68967c2ab") },
                    { new Guid("7cef5e36-fe7f-4acb-b17a-24feeac8d5f8"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "High Strings", new Guid("1bde9862-3ed5-45cd-8d80-0a52c6b4c0fb") },
                    { new Guid("0031e6f5-2d51-4e88-9e82-7bd2c8340cac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Bagpipes", new Guid("c9403ca4-6b75-44c3-b567-e53bbd78fb75") },
                    { new Guid("eb42b2f7-413e-4c1a-ab79-23c74b02d054"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Mezzo Soprano", new Guid("5d469fc5-b3e6-40b8-9fa9-542981083ce3") },
                    { new Guid("bb647161-8394-47d3-9f43-825762a70fc2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Baritone", new Guid("b9673cfd-7cdb-472c-86e0-1304cbb3840a") },
                    { new Guid("d6961f83-e792-4ddf-b91a-ae0867caeb3b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Flute", new Guid("a6abdeec-8185-40ac-a418-2e422bb9adbd") },
                    { new Guid("2327a9c3-2c6f-41b7-9045-bb00af798b42"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Oboe", new Guid("a6abdeec-8185-40ac-a418-2e422bb9adbd") },
                    { new Guid("cdc390d5-0649-441d-a086-df2e3b9d3512"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Clarinet", new Guid("a6abdeec-8185-40ac-a418-2e422bb9adbd") },
                    { new Guid("5c14f673-13f2-488f-8c21-7286d3ee10c3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Bassoon", new Guid("a6abdeec-8185-40ac-a418-2e422bb9adbd") },
                    { new Guid("566260fb-b6be-41dc-956d-4070d30fa88d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Saxophone", new Guid("a6abdeec-8185-40ac-a418-2e422bb9adbd") },
                    { new Guid("7d0d2295-df8a-4cfa-9f43-87dbf9fc133f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "High Brass", new Guid("f4c70178-d069-44dc-8956-7160c5fef52e") },
                    { new Guid("e4e7239e-0d0d-4a30-93b6-8a61e3ab8041"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Low Brass", new Guid("f4c70178-d069-44dc-8956-7160c5fef52e") },
                    { new Guid("ea916a8d-1bce-4e87-b5b0-ff6304bb01a5"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Timpani", new Guid("0558a5ff-ee27-44a1-82ab-d0c0cc018c3c") },
                    { new Guid("d12ebc93-4b55-455c-a9db-a826fca9a1f2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Mallets", new Guid("0558a5ff-ee27-44a1-82ab-d0c0cc018c3c") },
                    { new Guid("c15c3649-d7bb-4bbf-bdd3-f6146ebc825c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Drum Set (Orchestra)", new Guid("0558a5ff-ee27-44a1-82ab-d0c0cc018c3c") },
                    { new Guid("d22fb8aa-7d38-42c4-9586-30e559f63799"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Synthesizer", new Guid("614a8fd0-acfa-4268-b716-3b35a6a17b7a") },
                    { new Guid("76891771-b5f2-4666-8972-ba7f494fc9de"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Accordion", new Guid("c9403ca4-6b75-44c3-b567-e53bbd78fb75") },
                    { new Guid("d7ff1f62-e5c5-4662-823b-f77ff7706b4e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Bandoneon", new Guid("c9403ca4-6b75-44c3-b567-e53bbd78fb75") },
                    { new Guid("a22b6f19-3e9c-4389-824b-22db7b8cf8fd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Guitars", new Guid("c9403ca4-6b75-44c3-b567-e53bbd78fb75") },
                    { new Guid("08bc313b-d0dd-4b78-bdbf-d976682d965e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "GlassHarp", new Guid("c9403ca4-6b75-44c3-b567-e53bbd78fb75") },
                    { new Guid("8903b8c5-0ef8-48fd-9c2b-71fbae827965"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Didgeridoo", new Guid("c9403ca4-6b75-44c3-b567-e53bbd78fb75") },
                    { new Guid("4e7a61c5-d2e4-4e3b-b21d-34a90cf958b2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Conductor", new Guid("8bba816f-2315-43c0-b18e-99a27b1c9668") }
                });

            migrationBuilder.InsertData(
                table: "Sections",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "ModifiedAt", "ModifiedBy", "Name", "ParentId" },
                values: new object[,]
                {
                    { new Guid("18f1e750-f50d-4f06-8205-21203981bff6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Assistant Conductor", new Guid("4e7a61c5-d2e4-4e3b-b21d-34a90cf958b2") },
                    { new Guid("fb4f9841-294a-4b6c-bfec-02d3735b1ea0"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Bass Saxophone", new Guid("566260fb-b6be-41dc-956d-4070d30fa88d") },
                    { new Guid("b9532add-efec-4510-831c-902c32ef7dbb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Horn", new Guid("7d0d2295-df8a-4cfa-9f43-87dbf9fc133f") },
                    { new Guid("205b0a0e-1a36-48e9-8b45-df37dc5effa5"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Trumpet", new Guid("7d0d2295-df8a-4cfa-9f43-87dbf9fc133f") },
                    { new Guid("e20ce055-5715-42f4-97e6-4025559b15f7"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Trombone", new Guid("e4e7239e-0d0d-4a30-93b6-8a61e3ab8041") },
                    { new Guid("554fd3db-110b-4335-bc2a-1d5070f6621a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Euphonium", new Guid("e4e7239e-0d0d-4a30-93b6-8a61e3ab8041") },
                    { new Guid("18cbded8-0d64-4e0e-bc19-d6903e0fd5a9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Tuba", new Guid("e4e7239e-0d0d-4a30-93b6-8a61e3ab8041") },
                    { new Guid("dcf267e6-5b58-4534-8e4b-a8c5747b1816"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Glockenspiel", new Guid("d12ebc93-4b55-455c-a9db-a826fca9a1f2") },
                    { new Guid("852d8129-a5b7-4378-ad9c-df89dc878b4f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Vibraphone", new Guid("d12ebc93-4b55-455c-a9db-a826fca9a1f2") },
                    { new Guid("2804ed14-7b73-4e17-bd21-edd048a60cb4"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Xylophone", new Guid("d12ebc93-4b55-455c-a9db-a826fca9a1f2") },
                    { new Guid("bb0715dc-7f9d-4ddb-b5f5-9e7806e1069f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Marimbaphone", new Guid("d12ebc93-4b55-455c-a9db-a826fca9a1f2") },
                    { new Guid("1d0ed0b3-b87b-439f-932e-616d7e03a0d6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Acoustic Guitar (Orchestra)", new Guid("a22b6f19-3e9c-4389-824b-22db7b8cf8fd") },
                    { new Guid("ed0829d0-d978-430e-96ec-b93cf75f3fd6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Electric Guitar (Orchestra)", new Guid("a22b6f19-3e9c-4389-824b-22db7b8cf8fd") },
                    { new Guid("9cd74865-f82a-4be9-afc1-384fb25b7fe4"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Electric Bass (Orchestra)", new Guid("a22b6f19-3e9c-4389-824b-22db7b8cf8fd") },
                    { new Guid("fab9a49a-9fa4-4af3-9e40-e13bdc930513"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Violins", new Guid("7cef5e36-fe7f-4acb-b17a-24feeac8d5f8") },
                    { new Guid("df541ea1-a5fd-4975-b6fd-7cd652a79073"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Viola", new Guid("7cef5e36-fe7f-4acb-b17a-24feeac8d5f8") },
                    { new Guid("e4622ea3-f6a0-40b2-ac80-a2c9df099aeb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Baritone Saxophone", new Guid("566260fb-b6be-41dc-956d-4070d30fa88d") },
                    { new Guid("da998fcb-92b9-4828-976e-826e97e05cb3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Tenor Saxophone", new Guid("566260fb-b6be-41dc-956d-4070d30fa88d") },
                    { new Guid("4a31447d-63c2-4e28-ab39-255a956fbe18"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Alto Saxophone", new Guid("566260fb-b6be-41dc-956d-4070d30fa88d") },
                    { new Guid("b5d01e60-af61-4d29-bfb3-2f0dbac1e2fb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Soprano Saxophone", new Guid("566260fb-b6be-41dc-956d-4070d30fa88d") },
                    { new Guid("6fc908f0-da26-4237-80ca-dfe30453123c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Répétiteur", new Guid("4e7a61c5-d2e4-4e3b-b21d-34a90cf958b2") },
                    { new Guid("94c42496-fdb6-4341-b82f-735fd1706d39"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Vocal Coach", new Guid("4e7a61c5-d2e4-4e3b-b21d-34a90cf958b2") },
                    { new Guid("ec8aeaf8-f370-4ac8-bd12-ccce0cbcfa0f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Piccolo Flute", new Guid("d6961f83-e792-4ddf-b91a-ae0867caeb3b") },
                    { new Guid("f9c1924b-2b45-459c-b919-99059cb41e73"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Alto Flute", new Guid("d6961f83-e792-4ddf-b91a-ae0867caeb3b") },
                    { new Guid("d0a18a79-ad5a-450d-92cc-20a58496aaf0"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Tenor Flute", new Guid("d6961f83-e792-4ddf-b91a-ae0867caeb3b") },
                    { new Guid("fc66c8b8-d9de-4ff0-a695-37e717103686"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Bass Flute", new Guid("d6961f83-e792-4ddf-b91a-ae0867caeb3b") },
                    { new Guid("4e71ffc3-e086-4c16-a932-3d80fd302971"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Oboe d'Amore", new Guid("2327a9c3-2c6f-41b7-9045-bb00af798b42") },
                    { new Guid("d8686f68-78da-4022-b0b8-97e0c263d694"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Violoncello", new Guid("fdd5d68c-2620-47a3-80e4-64fda6dc7e3f") },
                    { new Guid("abe0d27b-2c99-4755-891c-fb0b91f19bb6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "English Horn", new Guid("2327a9c3-2c6f-41b7-9045-bb00af798b42") },
                    { new Guid("d2551427-d727-42d9-be0e-dea2ae82f2d6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Eb Clarinet", new Guid("cdc390d5-0649-441d-a086-df2e3b9d3512") },
                    { new Guid("be75913a-9703-4a8d-9e07-7a8d32c459f8"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Alto Clarinet", new Guid("cdc390d5-0649-441d-a086-df2e3b9d3512") },
                    { new Guid("8c0a80d1-5889-4794-89b6-b80a3828aa5b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Basset Horn", new Guid("cdc390d5-0649-441d-a086-df2e3b9d3512") },
                    { new Guid("5109e464-7b01-40bd-a5e0-398ac3d1bb83"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Bass Clarinet", new Guid("cdc390d5-0649-441d-a086-df2e3b9d3512") },
                    { new Guid("a5cc5e9d-b318-4edc-af84-ff3d701d0bcb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Double Bass Clarinet", new Guid("cdc390d5-0649-441d-a086-df2e3b9d3512") },
                    { new Guid("8d01524c-7c22-4a20-8f26-711d11addbfd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Contra Bassoon", new Guid("5c14f673-13f2-488f-8c21-7286d3ee10c3") },
                    { new Guid("7cb00d2e-5a98-4b68-b775-3b5d1f267d96"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Contraforte", new Guid("5c14f673-13f2-488f-8c21-7286d3ee10c3") },
                    { new Guid("2f8d732f-bf82-4a62-86a1-62bffd708189"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Bariton Oboe", new Guid("2327a9c3-2c6f-41b7-9045-bb00af798b42") },
                    { new Guid("e45ec6fa-7595-4084-9e01-991746b7f5e9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Double Bass", new Guid("fdd5d68c-2620-47a3-80e4-64fda6dc7e3f") }
                });

            migrationBuilder.InsertData(
                table: "Sections",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "ModifiedAt", "ModifiedBy", "Name", "ParentId" },
                values: new object[,]
                {
                    { new Guid("c42591db-4e41-413f-8b98-6607e2f12e39"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Wagner Tuba", new Guid("b9532add-efec-4510-831c-902c32ef7dbb") },
                    { new Guid("69e64d64-419f-4f9c-9948-a117b02ff198"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Flugelhorn", new Guid("205b0a0e-1a36-48e9-8b45-df37dc5effa5") },
                    { new Guid("2393549e-5b16-4414-a896-3cebb7bcc9df"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Piccolo Trumpet", new Guid("205b0a0e-1a36-48e9-8b45-df37dc5effa5") },
                    { new Guid("290f84d4-bb3f-41c3-9f42-c649c8eeea26"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Soprano Cornet", new Guid("205b0a0e-1a36-48e9-8b45-df37dc5effa5") },
                    { new Guid("305c06e0-b99f-4f91-ae83-869d8b25c63d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Cornet", new Guid("205b0a0e-1a36-48e9-8b45-df37dc5effa5") },
                    { new Guid("80f15184-6417-476a-87ac-0f752d011391"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Alto Trombone", new Guid("e20ce055-5715-42f4-97e6-4025559b15f7") },
                    { new Guid("da660c21-0151-4255-a81b-4d25fede199b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Bass Trombone", new Guid("e20ce055-5715-42f4-97e6-4025559b15f7") },
                    { new Guid("32f3fdd9-9517-4db5-856e-376e9fa52b84"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Double Bass Trombone", new Guid("e20ce055-5715-42f4-97e6-4025559b15f7") },
                    { new Guid("803219aa-1a32-4a68-95ae-348bd487135a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Tenor Horn", new Guid("554fd3db-110b-4335-bc2a-1d5070f6621a") },
                    { new Guid("b525e539-7fa4-49d7-ae93-ec0748022d4d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Baritone Horn", new Guid("554fd3db-110b-4335-bc2a-1d5070f6621a") },
                    { new Guid("2fabd3a1-d398-4108-a74f-2665710133d1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Eb Tuba", new Guid("18cbded8-0d64-4e0e-bc19-d6903e0fd5a9") },
                    { new Guid("31a2b9bf-0c2b-47ec-b8bc-34c9423b74d4"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "F Tuba", new Guid("18cbded8-0d64-4e0e-bc19-d6903e0fd5a9") },
                    { new Guid("eb5728b5-b1fd-4a70-8894-7bb152087837"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Violin I", new Guid("fab9a49a-9fa4-4af3-9e40-e13bdc930513") },
                    { new Guid("f3ee3c42-4e4e-411d-a839-6e0420bc35a3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Violin II", new Guid("fab9a49a-9fa4-4af3-9e40-e13bdc930513") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("0031e6f5-2d51-4e88-9e82-7bd2c8340cac"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("08bc313b-d0dd-4b78-bdbf-d976682d965e"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("13802d8b-4c73-4a52-8748-20bf3ba0c2b1"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("18f1e750-f50d-4f06-8205-21203981bff6"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("1d0ed0b3-b87b-439f-932e-616d7e03a0d6"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("2393549e-5b16-4414-a896-3cebb7bcc9df"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("2804ed14-7b73-4e17-bd21-edd048a60cb4"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("290f84d4-bb3f-41c3-9f42-c649c8eeea26"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("2f8d732f-bf82-4a62-86a1-62bffd708189"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("2fabd3a1-d398-4108-a74f-2665710133d1"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("305c06e0-b99f-4f91-ae83-869d8b25c63d"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("31a2b9bf-0c2b-47ec-b8bc-34c9423b74d4"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("32f3fdd9-9517-4db5-856e-376e9fa52b84"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("454c2ad6-e3c8-428a-b74e-c73873159c0e"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("48833c1b-cbc1-43b2-a4c5-f1fa4289f5ab"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("4a31447d-63c2-4e28-ab39-255a956fbe18"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("4e71ffc3-e086-4c16-a932-3d80fd302971"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("5109e464-7b01-40bd-a5e0-398ac3d1bb83"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("69e64d64-419f-4f9c-9948-a117b02ff198"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("6a107070-daae-41fc-b27d-416d44d36374"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("6fc908f0-da26-4237-80ca-dfe30453123c"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("75f593aa-fd20-4c05-9300-b31dbb90712e"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("76891771-b5f2-4666-8972-ba7f494fc9de"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("7cb00d2e-5a98-4b68-b775-3b5d1f267d96"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("7f811b88-e7db-461a-af5d-e249b1ce9e7d"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("803219aa-1a32-4a68-95ae-348bd487135a"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("80f15184-6417-476a-87ac-0f752d011391"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("852d8129-a5b7-4378-ad9c-df89dc878b4f"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("8903b8c5-0ef8-48fd-9c2b-71fbae827965"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("8c0a80d1-5889-4794-89b6-b80a3828aa5b"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("8d01524c-7c22-4a20-8f26-711d11addbfd"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("94c42496-fdb6-4341-b82f-735fd1706d39"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("9cd74865-f82a-4be9-afc1-384fb25b7fe4"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("a5cc5e9d-b318-4edc-af84-ff3d701d0bcb"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("abe0d27b-2c99-4755-891c-fb0b91f19bb6"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("b525e539-7fa4-49d7-ae93-ec0748022d4d"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("b58d047f-ec04-41e9-a728-06a8a160f55b"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("b5d01e60-af61-4d29-bfb3-2f0dbac1e2fb"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("bb0715dc-7f9d-4ddb-b5f5-9e7806e1069f"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("bb647161-8394-47d3-9f43-825762a70fc2"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("be75913a-9703-4a8d-9e07-7a8d32c459f8"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("c15c3649-d7bb-4bbf-bdd3-f6146ebc825c"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("c42591db-4e41-413f-8b98-6607e2f12e39"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("d0a18a79-ad5a-450d-92cc-20a58496aaf0"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("d22fb8aa-7d38-42c4-9586-30e559f63799"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("d2551427-d727-42d9-be0e-dea2ae82f2d6"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("d787fe9a-2283-43f6-bbc8-8a098e1f1c81"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("d7ff1f62-e5c5-4662-823b-f77ff7706b4e"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("d8686f68-78da-4022-b0b8-97e0c263d694"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("da660c21-0151-4255-a81b-4d25fede199b"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("da998fcb-92b9-4828-976e-826e97e05cb3"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("dcf267e6-5b58-4534-8e4b-a8c5747b1816"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("df541ea1-a5fd-4975-b6fd-7cd652a79073"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("e45ec6fa-7595-4084-9e01-991746b7f5e9"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("e4622ea3-f6a0-40b2-ac80-a2c9df099aeb"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("ea916a8d-1bce-4e87-b5b0-ff6304bb01a5"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("eb42b2f7-413e-4c1a-ab79-23c74b02d054"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("eb5728b5-b1fd-4a70-8894-7bb152087837"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("ec8aeaf8-f370-4ac8-bd12-ccce0cbcfa0f"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("ed0829d0-d978-430e-96ec-b93cf75f3fd6"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("f3ee3c42-4e4e-411d-a839-6e0420bc35a3"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("f9c1924b-2b45-459c-b919-99059cb41e73"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("fb4f9841-294a-4b6c-bfec-02d3735b1ea0"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("fc66c8b8-d9de-4ff0-a695-37e717103686"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("18cbded8-0d64-4e0e-bc19-d6903e0fd5a9"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("205b0a0e-1a36-48e9-8b45-df37dc5effa5"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("2327a9c3-2c6f-41b7-9045-bb00af798b42"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("4e7a61c5-d2e4-4e3b-b21d-34a90cf958b2"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("554fd3db-110b-4335-bc2a-1d5070f6621a"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("566260fb-b6be-41dc-956d-4070d30fa88d"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("5c14f673-13f2-488f-8c21-7286d3ee10c3"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("a22b6f19-3e9c-4389-824b-22db7b8cf8fd"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("b9532add-efec-4510-831c-902c32ef7dbb"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("cdc390d5-0649-441d-a086-df2e3b9d3512"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("d12ebc93-4b55-455c-a9db-a826fca9a1f2"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("d6961f83-e792-4ddf-b91a-ae0867caeb3b"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("e20ce055-5715-42f4-97e6-4025559b15f7"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("fab9a49a-9fa4-4af3-9e40-e13bdc930513"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("fdd5d68c-2620-47a3-80e4-64fda6dc7e3f"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("7cef5e36-fe7f-4acb-b17a-24feeac8d5f8"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("7d0d2295-df8a-4cfa-9f43-87dbf9fc133f"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("e4e7239e-0d0d-4a30-93b6-8a61e3ab8041"));

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("067647c0-3f25-449e-9212-03f39fa88f0f"),
                column: "Name",
                value: "Volunteers");

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("0cf93477-f42f-46c3-8e3d-45ccdc54ad8c"),
                columns: new[] { "Name", "ParentId" },
                values: new object[] { "Media", new Guid("182019da-bde2-44d7-8c77-88cfb0ce428c") });

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("182019da-bde2-44d7-8c77-88cfb0ce428c"),
                columns: new[] { "Name", "ParentId" },
                values: new object[] { "Crew", null });

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("48337b78-70f0-493e-911b-296632b06ef8"),
                column: "Name",
                value: "Deep Female Voices");

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("614a8fd0-acfa-4268-b716-3b35a6a17b7a"),
                columns: new[] { "Name", "ParentId" },
                values: new object[] { "Light", new Guid("182019da-bde2-44d7-8c77-88cfb0ce428c") });

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("8ed82e0e-0354-4192-8f26-5a2437e9208d"),
                columns: new[] { "Name", "ParentId" },
                values: new object[] { "Stage", new Guid("182019da-bde2-44d7-8c77-88cfb0ce428c") });

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("a6abdeec-8185-40ac-a418-2e422bb9adbd"),
                column: "Name",
                value: "Woodwind");

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("b289cfe7-d66e-48d8-83a9-f4b1f7710863"),
                column: "Name",
                value: "Wind Section");

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("b9673cfd-7cdb-472c-86e0-1304cbb3840a"),
                column: "Name",
                value: "Deep Male Voices");

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("bc6cfeb7-569d-4c22-8e80-647aed560bf0"),
                columns: new[] { "Name", "ParentId" },
                values: new object[] { "Sound", new Guid("182019da-bde2-44d7-8c77-88cfb0ce428c") });

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("c9403ca4-6b75-44c3-b567-e53bbd78fb75"),
                columns: new[] { "Name", "ParentId" },
                values: new object[] { "Other", null });

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("f6af00f5-e81c-4d85-aadd-1e33748e9a64"),
                columns: new[] { "Name", "ParentId" },
                values: new object[] { "Visitors", null });
        }
    }
}
