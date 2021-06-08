using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Orso.Arpa.Persistence.Migrations
{
    public partial class AddedSelectValueSectionSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "select_value_sections",
                columns: new[] { "id", "created_at", "created_by", "deleted", "modified_at", "modified_by", "section_id", "select_value_id" },
                values: new object[,]
                {
                    { new Guid("81e75718-d8dc-4316-bc7d-bac9da549245"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("7daa1394-a70d-4a24-88a6-ccf511d75c4d"), new Guid("36c6963d-a08c-4394-823a-1d24ba8330b4") },
                    { new Guid("00241b8c-7b88-4e32-b391-69b6e3b6acf2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("554fd3db-110b-4335-bc2a-1d5070f6621a"), new Guid("a89a8323-4c82-4e55-8ef1-6d7150f564e9") },
                    { new Guid("3fdaad51-200d-4578-b9bb-bc3a00480fef"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("18cbded8-0d64-4e0e-bc19-d6903e0fd5a9"), new Guid("9353f2ee-f074-488b-a359-f2fc6f66da51") },
                    { new Guid("4027a00d-4370-46f9-82b3-8618d572a117"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("18cbded8-0d64-4e0e-bc19-d6903e0fd5a9"), new Guid("a89a8323-4c82-4e55-8ef1-6d7150f564e9") },
                    { new Guid("069141f0-9ba3-4e10-822e-8f83d5282bda"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("0558a5ff-ee27-44a1-82ab-d0c0cc018c3c"), new Guid("9353f2ee-f074-488b-a359-f2fc6f66da51") },
                    { new Guid("3a9c04d8-ec63-4b46-b01e-fc1729ed529c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("0558a5ff-ee27-44a1-82ab-d0c0cc018c3c"), new Guid("36c6963d-a08c-4394-823a-1d24ba8330b4") },
                    { new Guid("040cbf2a-e70b-4dcf-98d0-45a7a4592093"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("0558a5ff-ee27-44a1-82ab-d0c0cc018c3c"), new Guid("a89a8323-4c82-4e55-8ef1-6d7150f564e9") },
                    { new Guid("f3e64014-b6c6-46ca-8334-c744fb2b07cc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("ea916a8d-1bce-4e87-b5b0-ff6304bb01a5"), new Guid("9353f2ee-f074-488b-a359-f2fc6f66da51") },
                    { new Guid("706fc83a-fbe8-4446-bc89-d42c6d4b5c76"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("ea916a8d-1bce-4e87-b5b0-ff6304bb01a5"), new Guid("a89a8323-4c82-4e55-8ef1-6d7150f564e9") },
                    { new Guid("281aa638-cc0c-45a1-a3d7-ae5a07644933"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("d12ebc93-4b55-455c-a9db-a826fca9a1f2"), new Guid("9353f2ee-f074-488b-a359-f2fc6f66da51") },
                    { new Guid("1ba1e082-fcf9-4b41-a996-2204038b5026"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("d12ebc93-4b55-455c-a9db-a826fca9a1f2"), new Guid("a89a8323-4c82-4e55-8ef1-6d7150f564e9") },
                    { new Guid("7ead42e9-7ea6-4bea-9ebd-9e232bd71a93"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("0cf93477-f42f-46c3-8e3d-45ccdc54ad8c"), new Guid("a89a8323-4c82-4e55-8ef1-6d7150f564e9") },
                    { new Guid("4199dbe4-9544-46c9-96af-3f1bb8488230"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("0cf93477-f42f-46c3-8e3d-45ccdc54ad8c"), new Guid("a89a8323-4c82-4e55-8ef1-6d7150f564e9") },
                    { new Guid("b27010dd-82dd-4f2a-af3e-d18c73fc4a31"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("8ed82e0e-0354-4192-8f26-5a2437e9208d"), new Guid("9353f2ee-f074-488b-a359-f2fc6f66da51") },
                    { new Guid("63e9c074-df8c-4d68-9c69-3e61bb5518ad"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("8ed82e0e-0354-4192-8f26-5a2437e9208d"), new Guid("ebae975b-d9a3-4d2f-b0a3-beff554e7041") },
                    { new Guid("774dc855-901a-41df-8b99-9cba9e973b7f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("554fd3db-110b-4335-bc2a-1d5070f6621a"), new Guid("9353f2ee-f074-488b-a359-f2fc6f66da51") },
                    { new Guid("602609d7-2f1a-4a3b-90f1-390515c531f9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("fab9a49a-9fa4-4af3-9e40-e13bdc930513"), new Guid("9353f2ee-f074-488b-a359-f2fc6f66da51") },
                    { new Guid("be79168e-620e-46c2-862c-efaffbeb82ee"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("fab9a49a-9fa4-4af3-9e40-e13bdc930513"), new Guid("9ed94828-9deb-49a9-9a65-ecb83620c82e") },
                    { new Guid("a4365301-bea0-40c9-b6a6-626c4cf00f74"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("fab9a49a-9fa4-4af3-9e40-e13bdc930513"), new Guid("36c6963d-a08c-4394-823a-1d24ba8330b4") },
                    { new Guid("776c3d50-0958-490e-98b0-6043cf580c3f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("fab9a49a-9fa4-4af3-9e40-e13bdc930513"), new Guid("5a4a1318-2f23-45b0-8329-3eec0f446389") },
                    { new Guid("12f2912a-139d-42af-99b4-61eb02a73701"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("fab9a49a-9fa4-4af3-9e40-e13bdc930513"), new Guid("a89a8323-4c82-4e55-8ef1-6d7150f564e9") },
                    { new Guid("c883b3ea-6522-499d-aebc-0e2937d7a09e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("df541ea1-a5fd-4975-b6fd-7cd652a79073"), new Guid("9353f2ee-f074-488b-a359-f2fc6f66da51") },
                    { new Guid("ed6d7457-869d-433f-9a14-e7327120bad2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("df541ea1-a5fd-4975-b6fd-7cd652a79073"), new Guid("36c6963d-a08c-4394-823a-1d24ba8330b4") },
                    { new Guid("7361f67c-4fe1-49c5-9d47-fb7225296ad7"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("df541ea1-a5fd-4975-b6fd-7cd652a79073"), new Guid("5a4a1318-2f23-45b0-8329-3eec0f446389") },
                    { new Guid("127bcbee-e946-44db-99ee-7e5645902689"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("df541ea1-a5fd-4975-b6fd-7cd652a79073"), new Guid("a89a8323-4c82-4e55-8ef1-6d7150f564e9") },
                    { new Guid("e155d063-df88-4629-ba50-8213b59501fd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("d8686f68-78da-4022-b0b8-97e0c263d694"), new Guid("9353f2ee-f074-488b-a359-f2fc6f66da51") },
                    { new Guid("141d8189-4731-4a24-9e20-0cdef1d8d150"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("d8686f68-78da-4022-b0b8-97e0c263d694"), new Guid("36c6963d-a08c-4394-823a-1d24ba8330b4") },
                    { new Guid("081e7457-5d72-4de2-adfe-beb427425738"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("d8686f68-78da-4022-b0b8-97e0c263d694"), new Guid("5a4a1318-2f23-45b0-8329-3eec0f446389") },
                    { new Guid("6c14c8e3-64aa-42a4-b6c2-366dc1fe89b5"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("d8686f68-78da-4022-b0b8-97e0c263d694"), new Guid("a89a8323-4c82-4e55-8ef1-6d7150f564e9") },
                    { new Guid("8e849991-2d30-41b4-85f2-4258d458def2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("e45ec6fa-7595-4084-9e01-991746b7f5e9"), new Guid("9353f2ee-f074-488b-a359-f2fc6f66da51") },
                    { new Guid("e75597e0-6173-4171-b5a7-ace60484967f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("e45ec6fa-7595-4084-9e01-991746b7f5e9"), new Guid("36c6963d-a08c-4394-823a-1d24ba8330b4") },
                    { new Guid("c810b38a-d80a-4f16-9c01-3f9183507804"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("fab9a49a-9fa4-4af3-9e40-e13bdc930513"), new Guid("fc2c8cf2-3189-44de-a124-2debe1d7b057") },
                    { new Guid("b85474fd-327d-4a52-8404-9ca9dc699daa"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("205b0a0e-1a36-48e9-8b45-df37dc5effa5"), new Guid("a89a8323-4c82-4e55-8ef1-6d7150f564e9") },
                    { new Guid("e971bdf0-d36f-4ce8-9bdd-ae027edd0bb0"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("e20ce055-5715-42f4-97e6-4025559b15f7"), new Guid("9353f2ee-f074-488b-a359-f2fc6f66da51") },
                    { new Guid("d606db59-9900-4f0d-9aaa-677d76329fc9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("205b0a0e-1a36-48e9-8b45-df37dc5effa5"), new Guid("a89a8323-4c82-4e55-8ef1-6d7150f564e9") },
                    { new Guid("640eaff9-0234-46cb-8dfe-2ba97399e6d3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a06431be-f9d6-44dc-8fdb-fbf8aa2bb940"), new Guid("9353f2ee-f074-488b-a359-f2fc6f66da51") },
                    { new Guid("7b01cc1c-15c7-4d66-8971-d2bf5507a676"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a06431be-f9d6-44dc-8fdb-fbf8aa2bb940"), new Guid("36c6963d-a08c-4394-823a-1d24ba8330b4") },
                    { new Guid("de6a82d3-4374-491d-8125-dca3d55dcdf1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a06431be-f9d6-44dc-8fdb-fbf8aa2bb940"), new Guid("a0e02d9f-03b5-49e0-9ae8-b34a419bc203") },
                    { new Guid("f85ecc0c-f793-49ee-a7e1-780edde12ec5"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a06431be-f9d6-44dc-8fdb-fbf8aa2bb940"), new Guid("959e5b30-6ad1-4102-8dce-f1395b8ae73e") },
                    { new Guid("6993ab28-3a79-4941-8a14-f07bdae5a3ba"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a06431be-f9d6-44dc-8fdb-fbf8aa2bb940"), new Guid("a89a8323-4c82-4e55-8ef1-6d7150f564e9") },
                    { new Guid("9e5d6525-4916-4294-8ace-a2b698925c7f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("1579d7e7-4f55-4532-a078-69fd1ec939da"), new Guid("9353f2ee-f074-488b-a359-f2fc6f66da51") },
                    { new Guid("e0eadd53-5de4-4d3a-82ad-3551b9a22766"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("1579d7e7-4f55-4532-a078-69fd1ec939da"), new Guid("36c6963d-a08c-4394-823a-1d24ba8330b4") },
                    { new Guid("c1e830ce-77c9-4253-af52-e6f350bfe479"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("1579d7e7-4f55-4532-a078-69fd1ec939da"), new Guid("a0e02d9f-03b5-49e0-9ae8-b34a419bc203") },
                    { new Guid("abc02ea8-8785-49b4-b519-07cb02a10e06"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("1579d7e7-4f55-4532-a078-69fd1ec939da"), new Guid("959e5b30-6ad1-4102-8dce-f1395b8ae73e") },
                    { new Guid("d5aa0e4e-ae90-4924-96be-05fb5459abe4"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("1579d7e7-4f55-4532-a078-69fd1ec939da"), new Guid("a89a8323-4c82-4e55-8ef1-6d7150f564e9") },
                    { new Guid("d0987cc0-f924-4d76-985f-b1e85be9e7b5"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("e7dd10ef-1c39-4440-9a6c-65d397f010ca"), new Guid("9353f2ee-f074-488b-a359-f2fc6f66da51") },
                    { new Guid("4cb43aeb-68ac-4d2d-b66b-a3b252178c11"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("e7dd10ef-1c39-4440-9a6c-65d397f010ca"), new Guid("36c6963d-a08c-4394-823a-1d24ba8330b4") },
                    { new Guid("2da6c9c0-3d83-4ee0-9c56-c9b3a8356081"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("e7dd10ef-1c39-4440-9a6c-65d397f010ca"), new Guid("a0e02d9f-03b5-49e0-9ae8-b34a419bc203") },
                    { new Guid("f8aef705-7e10-4db9-9d2b-06b90194b7d2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("e7dd10ef-1c39-4440-9a6c-65d397f010ca"), new Guid("959e5b30-6ad1-4102-8dce-f1395b8ae73e") },
                    { new Guid("1dee84b7-5cd3-4a6d-9819-2d507606398b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("d6961f83-e792-4ddf-b91a-ae0867caeb3b"), new Guid("9353f2ee-f074-488b-a359-f2fc6f66da51") },
                    { new Guid("8da412fa-830e-4f16-a387-8e0e5a8bc5a9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("d6961f83-e792-4ddf-b91a-ae0867caeb3b"), new Guid("5a4a1318-2f23-45b0-8329-3eec0f446389") },
                    { new Guid("1279d4e8-c50b-4835-93f0-5f31d7345770"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("d6961f83-e792-4ddf-b91a-ae0867caeb3b"), new Guid("a89a8323-4c82-4e55-8ef1-6d7150f564e9") },
                    { new Guid("7466eccf-3450-4fc1-948e-1de04d17e5b3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("205b0a0e-1a36-48e9-8b45-df37dc5effa5"), new Guid("959e5b30-6ad1-4102-8dce-f1395b8ae73e") },
                    { new Guid("ebb29506-4552-413a-a43b-0f8dba5fba8d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("205b0a0e-1a36-48e9-8b45-df37dc5effa5"), new Guid("a0e02d9f-03b5-49e0-9ae8-b34a419bc203") },
                    { new Guid("99def608-eea1-4738-8cd4-aeb786b38c91"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("205b0a0e-1a36-48e9-8b45-df37dc5effa5"), new Guid("9353f2ee-f074-488b-a359-f2fc6f66da51") },
                    { new Guid("bc61e9e1-c344-4269-a851-84af7b43db54"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("e4622ea3-f6a0-40b2-ac80-a2c9df099aeb"), new Guid("9353f2ee-f074-488b-a359-f2fc6f66da51") },
                    { new Guid("d2297caf-03e0-44d9-979a-f4fbd53812fb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("da998fcb-92b9-4828-976e-826e97e05cb3"), new Guid("a89a8323-4c82-4e55-8ef1-6d7150f564e9") },
                    { new Guid("e383f6ee-eac0-4bca-85a6-e4f024c0db81"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("da998fcb-92b9-4828-976e-826e97e05cb3"), new Guid("9353f2ee-f074-488b-a359-f2fc6f66da51") },
                    { new Guid("097923c1-e85e-4afc-af85-8af01ae27655"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("e45ec6fa-7595-4084-9e01-991746b7f5e9"), new Guid("5a4a1318-2f23-45b0-8329-3eec0f446389") },
                    { new Guid("b1f7b38f-2624-4526-99a5-46c3eef1152b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("4a31447d-63c2-4e28-ab39-255a956fbe18"), new Guid("a89a8323-4c82-4e55-8ef1-6d7150f564e9") },
                    { new Guid("eec68681-b8d1-4142-9a82-c38cf342101d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("b5d01e60-af61-4d29-bfb3-2f0dbac1e2fb"), new Guid("9353f2ee-f074-488b-a359-f2fc6f66da51") },
                    { new Guid("50da7fa5-8d15-475c-8ebf-154aeac181d5"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("8d01524c-7c22-4a20-8f26-711d11addbfd"), new Guid("9353f2ee-f074-488b-a359-f2fc6f66da51") },
                    { new Guid("7676806b-2f80-47f1-991f-b731b89182f0"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("5c14f673-13f2-488f-8c21-7286d3ee10c3"), new Guid("a89a8323-4c82-4e55-8ef1-6d7150f564e9") },
                    { new Guid("1886d75e-26cd-49f1-8ad9-a35d6c1786fd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("5c14f673-13f2-488f-8c21-7286d3ee10c3"), new Guid("9353f2ee-f074-488b-a359-f2fc6f66da51") },
                    { new Guid("6f78a38f-2366-4ee2-bd5e-7b67f388b993"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("2327a9c3-2c6f-41b7-9045-bb00af798b42"), new Guid("a89a8323-4c82-4e55-8ef1-6d7150f564e9") },
                    { new Guid("5d335fff-919a-4deb-b313-9d0b7cfc7bde"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("2327a9c3-2c6f-41b7-9045-bb00af798b42"), new Guid("9353f2ee-f074-488b-a359-f2fc6f66da51") },
                    { new Guid("024c5961-9f0d-4b1e-a695-39b3222635f9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("4a31447d-63c2-4e28-ab39-255a956fbe18"), new Guid("9353f2ee-f074-488b-a359-f2fc6f66da51") },
                    { new Guid("ad3014d9-336f-4ca0-9f37-1937b8da8bff"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("e45ec6fa-7595-4084-9e01-991746b7f5e9"), new Guid("a89a8323-4c82-4e55-8ef1-6d7150f564e9") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("00241b8c-7b88-4e32-b391-69b6e3b6acf2"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("024c5961-9f0d-4b1e-a695-39b3222635f9"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("040cbf2a-e70b-4dcf-98d0-45a7a4592093"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("069141f0-9ba3-4e10-822e-8f83d5282bda"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("081e7457-5d72-4de2-adfe-beb427425738"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("097923c1-e85e-4afc-af85-8af01ae27655"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("1279d4e8-c50b-4835-93f0-5f31d7345770"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("127bcbee-e946-44db-99ee-7e5645902689"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("12f2912a-139d-42af-99b4-61eb02a73701"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("141d8189-4731-4a24-9e20-0cdef1d8d150"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("1886d75e-26cd-49f1-8ad9-a35d6c1786fd"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("1ba1e082-fcf9-4b41-a996-2204038b5026"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("1dee84b7-5cd3-4a6d-9819-2d507606398b"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("281aa638-cc0c-45a1-a3d7-ae5a07644933"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("2da6c9c0-3d83-4ee0-9c56-c9b3a8356081"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("3a9c04d8-ec63-4b46-b01e-fc1729ed529c"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("3fdaad51-200d-4578-b9bb-bc3a00480fef"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("4027a00d-4370-46f9-82b3-8618d572a117"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("4199dbe4-9544-46c9-96af-3f1bb8488230"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("4cb43aeb-68ac-4d2d-b66b-a3b252178c11"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("50da7fa5-8d15-475c-8ebf-154aeac181d5"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("5d335fff-919a-4deb-b313-9d0b7cfc7bde"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("602609d7-2f1a-4a3b-90f1-390515c531f9"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("63e9c074-df8c-4d68-9c69-3e61bb5518ad"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("640eaff9-0234-46cb-8dfe-2ba97399e6d3"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("6993ab28-3a79-4941-8a14-f07bdae5a3ba"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("6c14c8e3-64aa-42a4-b6c2-366dc1fe89b5"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("6f78a38f-2366-4ee2-bd5e-7b67f388b993"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("706fc83a-fbe8-4446-bc89-d42c6d4b5c76"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("7361f67c-4fe1-49c5-9d47-fb7225296ad7"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("7466eccf-3450-4fc1-948e-1de04d17e5b3"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("7676806b-2f80-47f1-991f-b731b89182f0"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("774dc855-901a-41df-8b99-9cba9e973b7f"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("776c3d50-0958-490e-98b0-6043cf580c3f"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("7b01cc1c-15c7-4d66-8971-d2bf5507a676"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("7ead42e9-7ea6-4bea-9ebd-9e232bd71a93"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("81e75718-d8dc-4316-bc7d-bac9da549245"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("8da412fa-830e-4f16-a387-8e0e5a8bc5a9"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("8e849991-2d30-41b4-85f2-4258d458def2"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("99def608-eea1-4738-8cd4-aeb786b38c91"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("9e5d6525-4916-4294-8ace-a2b698925c7f"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("a4365301-bea0-40c9-b6a6-626c4cf00f74"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("abc02ea8-8785-49b4-b519-07cb02a10e06"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("ad3014d9-336f-4ca0-9f37-1937b8da8bff"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("b1f7b38f-2624-4526-99a5-46c3eef1152b"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("b27010dd-82dd-4f2a-af3e-d18c73fc4a31"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("b85474fd-327d-4a52-8404-9ca9dc699daa"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("bc61e9e1-c344-4269-a851-84af7b43db54"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("be79168e-620e-46c2-862c-efaffbeb82ee"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("c1e830ce-77c9-4253-af52-e6f350bfe479"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("c810b38a-d80a-4f16-9c01-3f9183507804"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("c883b3ea-6522-499d-aebc-0e2937d7a09e"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("d0987cc0-f924-4d76-985f-b1e85be9e7b5"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("d2297caf-03e0-44d9-979a-f4fbd53812fb"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("d5aa0e4e-ae90-4924-96be-05fb5459abe4"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("d606db59-9900-4f0d-9aaa-677d76329fc9"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("de6a82d3-4374-491d-8125-dca3d55dcdf1"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("e0eadd53-5de4-4d3a-82ad-3551b9a22766"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("e155d063-df88-4629-ba50-8213b59501fd"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("e383f6ee-eac0-4bca-85a6-e4f024c0db81"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("e75597e0-6173-4171-b5a7-ace60484967f"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("e971bdf0-d36f-4ce8-9bdd-ae027edd0bb0"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("ebb29506-4552-413a-a43b-0f8dba5fba8d"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("ed6d7457-869d-433f-9a14-e7327120bad2"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("eec68681-b8d1-4142-9a82-c38cf342101d"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("f3e64014-b6c6-46ca-8334-c744fb2b07cc"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("f85ecc0c-f793-49ee-a7e1-780edde12ec5"));

            migrationBuilder.DeleteData(
                table: "select_value_sections",
                keyColumn: "id",
                keyValue: new Guid("f8aef705-7e10-4db9-9d2b-06b90194b7d2"));
        }
    }
}
