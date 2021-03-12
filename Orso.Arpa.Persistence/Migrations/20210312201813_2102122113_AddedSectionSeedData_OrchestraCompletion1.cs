using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Orso.Arpa.Persistence.Migrations
{
    public partial class _2102122113_AddedSectionSeedData_OrchestraCompletion1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("0cf93477-f42f-46c3-8e3d-45ccdc54ad8c"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("1994cb6c-877e-4d7c-aeca-26e68967c2ab"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("614a8fd0-acfa-4268-b716-3b35a6a17b7a"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("8ed82e0e-0354-4192-8f26-5a2437e9208d"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("bc6cfeb7-569d-4c22-8e80-647aed560bf0"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("c9403ca4-6b75-44c3-b567-e53bbd78fb75"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("e0fdb057-c9b7-4477-be75-cbf920a26af6"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("f6af00f5-e81c-4d85-aadd-1e33748e9a64"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("182019da-bde2-44d7-8c77-88cfb0ce428c"));

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("067647c0-3f25-449e-9212-03f39fa88f0f"),
                column: "Name",
                value: "Members");

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("b289cfe7-d66e-48d8-83a9-f4b1f7710863"),
                column: "Name",
                value: "Winds");

            migrationBuilder.InsertData(
                table: "Sections",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "ModifiedAt", "ModifiedBy", "Name", "ParentId" },
                values: new object[,]
                {
                    { new Guid("6a107070-daae-41fc-b27d-416d44d36374"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Contractors", null },
                    { new Guid("13802d8b-4c73-4a52-8748-20bf3ba0c2b1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Suppliers", null },
                    { new Guid("b58d047f-ec04-41e9-a728-06a8a160f55b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Visitors", null },
                    { new Guid("e45ec6fa-7595-4084-9e01-991746b7f5e9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Double Bass", new Guid("fdd5d68c-2620-47a3-80e4-64fda6dc7e3f") },
                    { new Guid("d8686f68-78da-4022-b0b8-97e0c263d694"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Violoncello", new Guid("fdd5d68c-2620-47a3-80e4-64fda6dc7e3f") },
                    { new Guid("df541ea1-a5fd-4975-b6fd-7cd652a79073"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Strings", new Guid("7cef5e36-fe7f-4acb-b17a-24feeac8d5f8") },
                    { new Guid("f3ee3c42-4e4e-411d-a839-6e0420bc35a3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Strings", new Guid("fab9a49a-9fa4-4af3-9e40-e13bdc930513") },
                    { new Guid("eb5728b5-b1fd-4a70-8894-7bb152087837"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Violin I", new Guid("fab9a49a-9fa4-4af3-9e40-e13bdc930513") },
                    { new Guid("d787fe9a-2283-43f6-bbc8-8a098e1f1c81"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Drum Set (Orchestra)", new Guid("0558a5ff-ee27-44a1-82ab-d0c0cc018c3c") },
                    { new Guid("49966aee-18d0-4884-ad34-038ca5390b83"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Others", new Guid("308659d6-6014-4d2c-a62a-be75bf202e62") },
                    { new Guid("d12ebc93-4b55-455c-a9db-a826fca9a1f2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Mallets", new Guid("0558a5ff-ee27-44a1-82ab-d0c0cc018c3c") },
                    { new Guid("18cbded8-0d64-4e0e-bc19-d6903e0fd5a9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Tuba", new Guid("e4e7239e-0d0d-4a30-93b6-8a61e3ab8041") },
                    { new Guid("554fd3db-110b-4335-bc2a-1d5070f6621a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Euphonium", new Guid("e4e7239e-0d0d-4a30-93b6-8a61e3ab8041") },
                    { new Guid("e20ce055-5715-42f4-97e6-4025559b15f7"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Trombone", new Guid("e4e7239e-0d0d-4a30-93b6-8a61e3ab8041") },
                    { new Guid("205b0a0e-1a36-48e9-8b45-df37dc5effa5"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Trumpet", new Guid("7d0d2295-df8a-4cfa-9f43-87dbf9fc133f") },
                    { new Guid("b9532add-efec-4510-831c-902c32ef7dbb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Horn", new Guid("7d0d2295-df8a-4cfa-9f43-87dbf9fc133f") },
                    { new Guid("5c14f673-13f2-488f-8c21-7286d3ee10c3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Bassoon", new Guid("a6abdeec-8185-40ac-a418-2e422bb9adbd") },
                    { new Guid("cdc390d5-0649-441d-a086-df2e3b9d3512"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Clarinet", new Guid("a6abdeec-8185-40ac-a418-2e422bb9adbd") },
                    { new Guid("2327a9c3-2c6f-41b7-9045-bb00af798b42"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Oboe", new Guid("a6abdeec-8185-40ac-a418-2e422bb9adbd") },
                    { new Guid("d6961f83-e792-4ddf-b91a-ae0867caeb3b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Flute", new Guid("a6abdeec-8185-40ac-a418-2e422bb9adbd") },
                    { new Guid("bb647161-8394-47d3-9f43-825762a70fc2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Baritone", new Guid("b9673cfd-7cdb-472c-86e0-1304cbb3840a") },
                    { new Guid("ea916a8d-1bce-4e87-b5b0-ff6304bb01a5"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Timpani", new Guid("0558a5ff-ee27-44a1-82ab-d0c0cc018c3c") },
                    { new Guid("eb42b2f7-413e-4c1a-ab79-23c74b02d054"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Mezzo Soprano", new Guid("5d469fc5-b3e6-40b8-9fa9-542981083ce3") }
                });

            migrationBuilder.InsertData(
                table: "Sections",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "ModifiedAt", "ModifiedBy", "Name", "ParentId" },
                values: new object[,]
                {
                    { new Guid("ec8aeaf8-f370-4ac8-bd12-ccce0cbcfa0f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Piccolo Flute", new Guid("d6961f83-e792-4ddf-b91a-ae0867caeb3b") },
                    { new Guid("76891771-b5f2-4666-8972-ba7f494fc9de"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Accordion", new Guid("49966aee-18d0-4884-ad34-038ca5390b83") },
                    { new Guid("08bc313b-d0dd-4b78-bdbf-d976682d965e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "GlassHarp", new Guid("49966aee-18d0-4884-ad34-038ca5390b83") },
                    { new Guid("a22b6f19-3e9c-4389-824b-22db7b8cf8fd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Guitars", new Guid("49966aee-18d0-4884-ad34-038ca5390b83") },
                    { new Guid("2a777891-847a-4014-b801-639c0cacf18d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Keyboards", new Guid("49966aee-18d0-4884-ad34-038ca5390b83") },
                    { new Guid("852d8129-a5b7-4378-ad9c-df89dc878b4f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Vibraphone", new Guid("d12ebc93-4b55-455c-a9db-a826fca9a1f2") },
                    { new Guid("2804ed14-7b73-4e17-bd21-edd048a60cb4"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Xylophone", new Guid("d12ebc93-4b55-455c-a9db-a826fca9a1f2") },
                    { new Guid("bb0715dc-7f9d-4ddb-b5f5-9e7806e1069f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Marimbaphone", new Guid("d12ebc93-4b55-455c-a9db-a826fca9a1f2") },
                    { new Guid("dcf267e6-5b58-4534-8e4b-a8c5747b1816"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Glockenspiel", new Guid("d12ebc93-4b55-455c-a9db-a826fca9a1f2") },
                    { new Guid("da660c21-0151-4255-a81b-4d25fede199b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Bass Trombone", new Guid("e20ce055-5715-42f4-97e6-4025559b15f7") },
                    { new Guid("80f15184-6417-476a-87ac-0f752d011391"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Alto Trombone", new Guid("e20ce055-5715-42f4-97e6-4025559b15f7") },
                    { new Guid("69e64d64-419f-4f9c-9948-a117b02ff198"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Flugelhorn", new Guid("205b0a0e-1a36-48e9-8b45-df37dc5effa5") },
                    { new Guid("c42591db-4e41-413f-8b98-6607e2f12e39"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Wagner Tuba", new Guid("b9532add-efec-4510-831c-902c32ef7dbb") },
                    { new Guid("8d01524c-7c22-4a20-8f26-711d11addbfd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Contra Bassoon", new Guid("5c14f673-13f2-488f-8c21-7286d3ee10c3") },
                    { new Guid("5109e464-7b01-40bd-a5e0-398ac3d1bb83"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Bass Clarinet", new Guid("cdc390d5-0649-441d-a086-df2e3b9d3512") },
                    { new Guid("d2551427-d727-42d9-be0e-dea2ae82f2d6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Eb Clarinet", new Guid("cdc390d5-0649-441d-a086-df2e3b9d3512") },
                    { new Guid("abe0d27b-2c99-4755-891c-fb0b91f19bb6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "English Horn", new Guid("2327a9c3-2c6f-41b7-9045-bb00af798b42") },
                    { new Guid("f9c1924b-2b45-459c-b919-99059cb41e73"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Alto Flute", new Guid("d6961f83-e792-4ddf-b91a-ae0867caeb3b") },
                    { new Guid("d7ff1f62-e5c5-4662-823b-f77ff7706b4e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Bandoneon", new Guid("49966aee-18d0-4884-ad34-038ca5390b83") },
                    { new Guid("0031e6f5-2d51-4e88-9e82-7bd2c8340cac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Bagpipes", new Guid("49966aee-18d0-4884-ad34-038ca5390b83") }
                });

            migrationBuilder.InsertData(
                table: "Sections",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "ModifiedAt", "ModifiedBy", "Name", "ParentId" },
                values: new object[,]
                {
                    { new Guid("76422714-9571-45d7-bb3a-567a95fd893d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Piano", new Guid("2a777891-847a-4014-b801-639c0cacf18d") },
                    { new Guid("78831a47-8469-4a0c-aa39-343f44a0bb09"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Cembalo", new Guid("2a777891-847a-4014-b801-639c0cacf18d") },
                    { new Guid("b1639d96-347b-4ee2-bef0-ab73a0194d8e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Celesta", new Guid("2a777891-847a-4014-b801-639c0cacf18d") },
                    { new Guid("370d72de-4185-4a91-a3e6-ea83c15ec51c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Organ", new Guid("2a777891-847a-4014-b801-639c0cacf18d") },
                    { new Guid("d22fb8aa-7d38-42c4-9586-30e559f63799"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Synthesizer", new Guid("2a777891-847a-4014-b801-639c0cacf18d") },
                    { new Guid("1d0ed0b3-b87b-439f-932e-616d7e03a0d6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Electric Guitar (Orchestra)", new Guid("a22b6f19-3e9c-4389-824b-22db7b8cf8fd") },
                    { new Guid("ed0829d0-d978-430e-96ec-b93cf75f3fd6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Electric Guitar (Orchestra)", new Guid("a22b6f19-3e9c-4389-824b-22db7b8cf8fd") },
                    { new Guid("9cd74865-f82a-4be9-afc1-384fb25b7fe4"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Electric Bass (Orchestra)", new Guid("a22b6f19-3e9c-4389-824b-22db7b8cf8fd") }
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
                keyValue: new Guid("18cbded8-0d64-4e0e-bc19-d6903e0fd5a9"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("1d0ed0b3-b87b-439f-932e-616d7e03a0d6"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("2804ed14-7b73-4e17-bd21-edd048a60cb4"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("370d72de-4185-4a91-a3e6-ea83c15ec51c"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("5109e464-7b01-40bd-a5e0-398ac3d1bb83"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("554fd3db-110b-4335-bc2a-1d5070f6621a"));

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
                keyValue: new Guid("76422714-9571-45d7-bb3a-567a95fd893d"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("76891771-b5f2-4666-8972-ba7f494fc9de"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("78831a47-8469-4a0c-aa39-343f44a0bb09"));

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
                keyValue: new Guid("8d01524c-7c22-4a20-8f26-711d11addbfd"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("9cd74865-f82a-4be9-afc1-384fb25b7fe4"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("abe0d27b-2c99-4755-891c-fb0b91f19bb6"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("b1639d96-347b-4ee2-bef0-ab73a0194d8e"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("b58d047f-ec04-41e9-a728-06a8a160f55b"));

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
                keyValue: new Guid("c42591db-4e41-413f-8b98-6607e2f12e39"));

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
                keyValue: new Guid("205b0a0e-1a36-48e9-8b45-df37dc5effa5"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("2327a9c3-2c6f-41b7-9045-bb00af798b42"));

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("2a777891-847a-4014-b801-639c0cacf18d"));

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
                keyValue: new Guid("49966aee-18d0-4884-ad34-038ca5390b83"));

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("067647c0-3f25-449e-9212-03f39fa88f0f"),
                column: "Name",
                value: "Volunteers");

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: new Guid("b289cfe7-d66e-48d8-83a9-f4b1f7710863"),
                column: "Name",
                value: "Wind Section");

            migrationBuilder.InsertData(
                table: "Sections",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "ModifiedAt", "ModifiedBy", "Name", "ParentId" },
                values: new object[,]
                {
                    { new Guid("e0fdb057-c9b7-4477-be75-cbf920a26af6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Soloists", new Guid("8bba816f-2315-43c0-b18e-99a27b1c9668") },
                    { new Guid("c9403ca4-6b75-44c3-b567-e53bbd78fb75"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Other", null },
                    { new Guid("f6af00f5-e81c-4d85-aadd-1e33748e9a64"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Visitors", null },
                    { new Guid("1994cb6c-877e-4d7c-aeca-26e68967c2ab"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Band", new Guid("8bba816f-2315-43c0-b18e-99a27b1c9668") },
                    { new Guid("182019da-bde2-44d7-8c77-88cfb0ce428c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Crew", null }
                });

            migrationBuilder.InsertData(
                table: "Sections",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "ModifiedAt", "ModifiedBy", "Name", "ParentId" },
                values: new object[,]
                {
                    { new Guid("8ed82e0e-0354-4192-8f26-5a2437e9208d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Stage", new Guid("182019da-bde2-44d7-8c77-88cfb0ce428c") },
                    { new Guid("0cf93477-f42f-46c3-8e3d-45ccdc54ad8c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Media", new Guid("182019da-bde2-44d7-8c77-88cfb0ce428c") },
                    { new Guid("bc6cfeb7-569d-4c22-8e80-647aed560bf0"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Sound", new Guid("182019da-bde2-44d7-8c77-88cfb0ce428c") },
                    { new Guid("614a8fd0-acfa-4268-b716-3b35a6a17b7a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Light", new Guid("182019da-bde2-44d7-8c77-88cfb0ce428c") }
                });
        }
    }
}
