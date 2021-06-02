using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Orso.Arpa.Persistence.Migrations
{
    public partial class SectionValueMappingSortOrderAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "sort_order",
                table: "select_value_mappings",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("0096f414-50c9-4d45-9a85-4af30641b7fa"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("0126fded-0a82-4b53-85e4-1c04a4f79296"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("0298c0d1-57e2-415a-9d6c-3f47e9ab6f22"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("03a0cbc1-4546-4b54-b05d-ec37dafeec25"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("03bdcf0a-2638-4b8f-a093-4084b9969162"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("0c8af1d2-ae39-464d-9e03-a1487cfd7321"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("0d1b888f-0f45-4f02-806b-480d5594bd27"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("0e997440-53f2-4823-8581-4d4716525885"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("0fdbc388-feba-4607-9771-7751009f1fc8"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("104fc525-bb0b-49dc-b2b2-9a8f63e45c92"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("17d201fc-777b-43bb-9c46-0d07737a8ab7"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("1b53d96a-f9a1-4037-b103-f7aae9b278d7"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("1d402f12-816d-4994-a94d-28d52cb2d199"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("20f9698c-2f3d-41fd-9f33-1feeababfb1c"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("24c5bbe1-37eb-4368-ac7c-a6061058bbef"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("2634c0cc-31d2-4f61-813d-7ec60fc8ab34"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("29e1142f-aa9e-4b94-ae21-9a63f7b65c15"),
                column: "sort_order",
                value: 30);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("2a5f85e6-a7ed-48eb-852c-0b191d7ba949"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("2ad77626-e0b3-45a6-9d24-e4677181ee7e"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("2fbb99cd-d14a-4b7c-b7fb-9b676f961e2c"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("30f592f6-485a-468a-bfb2-4854be733e74"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("319d508e-a6e2-437e-b48b-6be51e3459bd"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("34f05f05-ef23-4f36-94e7-73b917530c51"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("354ef017-70ca-4c2b-914c-71be7289a0e5"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("36176b7e-0926-43d6-b19a-72838ccd2acd"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("3801aa69-cc4e-4fd5-947c-bfdd4e95d48e"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("3acd5be1-5fbc-4de4-a45c-2e230c413c85"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("3f166c3c-c85e-404b-aad3-c8996f4fb75f"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("4298e1f5-ea1d-4a83-9b32-e5dc3a7cbca9"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("42d76464-4b4b-4884-b8e3-1f69baaced4c"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("44710a6b-93c0-4aac-b552-e0423f1b106a"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("459dc1ea-de92-4a26-9b7b-848d67154cae"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("466aa422-0ef2-4e7f-a6a8-d188d80491f6"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("4dc9db05-357a-43a6-ba20-f2a9e5033802"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("4e9d4a1b-cae7-4002-93a1-cef3f209146b"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("4ef47024-d8a5-4b2d-8584-aeb29263dddb"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("50e6049b-a9cd-400b-a475-e2563147aebc"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("547b561e-cea7-4296-9b1d-4dd41e0d5179"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("5578f637-14b7-4c11-85a8-0b94d83da678"),
                column: "sort_order",
                value: 30);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("574e0c4b-cbb3-4750-926b-3df4c377fc5e"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("58a0d16f-2dac-4836-930e-1dd320430ef4"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("5b89cf6e-0194-4e01-bb32-8b1813a51e16"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("5b936e5f-3743-4cc3-91af-0cc8742c846e"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("5c3f5e18-7afd-4404-98db-658e852901dc"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("609f9ece-42ce-4cc9-a89b-1fec1ddbc5fe"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("60c1a391-59b4-4cea-ba83-59e09f7512b6"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("625a9195-2380-4762-8dc6-13163e354ef6"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("6304b935-633d-4bba-a90f-9bd864c867c6"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("63437ce4-b63b-4558-9f91-1474b896bf1c"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("642cc60f-e582-47ed-a40f-ea490dd3d950"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("647f674a-bc2f-4d3a-9ce4-f0aefa98cd9d"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("65975857-ab27-480d-87c3-dba74d45cb63"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("679116ec-7840-4c6d-bb45-fa2d89d6e779"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("68e947c0-9450-4b64-90d7-553850396a3f"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("725a4f4a-37cb-46ba-93a3-7b9cc2b015cb"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("74278b65-fd54-49d2-9cd2-061dd6318c7d"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("75f2d1c3-4ba2-4acc-8fd3-6b01ca66d1c9"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("7869a9b0-fb13-4c00-ac7c-2fa1b27a00af"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("7b8defe6-9922-43d6-8df0-3a73f47d6980"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("7f76d426-cab7-4f4f-aba3-bd430bcec003"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("7fb30d45-1faf-4f6a-ac5d-436204ad8e69"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("8168cfbf-7e53-41c5-8bc4-f5392d9a3b57"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("836c69d6-46f1-40d4-b75d-6aa86f9ec066"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("86672779-5e70-4965-b59c-032086d00914"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("867622fa-7aa5-43f3-b3ef-5290d1f61734"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("887e7e2e-0c90-4c4c-9504-3f2a5af7fbcb"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("88da1c17-9efc-4f69-ba0f-39c76592845b"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("8b51c75f-d597-48ef-8451-5f5fc32d57d1"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("8b7d7f26-b7e5-42e2-afc1-cedddbae841a"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("8daa5ae4-3885-4739-803a-693c7cfdf314"),
                column: "sort_order",
                value: 30);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("90b5cfa9-890b-4b89-a750-646f3a26db23"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("93033f7e-a3c1-45e3-8a17-021d0a4abe5a"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("9363bb46-937e-42bf-bb71-5fb16126b501"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("9808c1f6-0bbd-4054-acca-779d56a8a934"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("98addc5f-f2fa-4640-8441-d4220b7daea2"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("9cf090a3-680d-4770-b929-0a0d080576a0"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("a15014ad-582e-4388-9b58-aceb52cf41bf"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("a39a92fe-bea2-40fa-817b-e7272bfc9d4b"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("a3e5843b-05c3-452c-a29d-da8de738181a"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("a88b874f-9879-482f-85ec-1ddda9bb545c"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("ab5c5904-2683-47c4-a436-319303b8e62f"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("ac1ccdd4-39aa-4767-95ea-099a829f275b"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("ade78d45-b010-4ed7-87f0-e30e0558f151"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("ae2f10ff-39ae-427e-a5e8-ddcd89422d44"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("aedc27f3-e2e8-4368-ad69-1ab1c3dd7974"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("b09bc4a6-06ab-4d45-8b82-7971e662ccb5"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("b0dcb5e9-bbc6-4004-b9d7-0f6723416b9b"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("b62cc155-f1a9-4904-8e6a-95e85339da83"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("b6cf76a5-ec3f-4e81-9499-174d33bb7249"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("b793fa86-2025-4258-8993-8045f4c312d7"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("bbe90120-55f3-4474-a059-1334d0adaa57"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("bc29bf0a-2ebb-4db8-8765-a5f835492552"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("be152c92-b807-4850-8327-9d1916dabead"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("c1b6d08b-f31e-4f38-a8c0-761e42fbd2b7"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("c6b0b06f-a915-4087-9827-34e76ab6895f"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("c6b28eb5-e9d6-4250-bc79-6fa9bfbdbc5a"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("c9225a82-0348-41bb-a591-7726f8079cde"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("cdfb1c47-22dc-4657-aab8-1dbfaf21e862"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("cfc62012-4d74-4cf6-a06e-7fc3dbacbff8"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("d33ea034-0c5f-458d-bef5-26d2c12b6b03"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("d64abb04-dc1c-4e17-bed5-a62196a59c49"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("d733e38d-1d80-4054-b654-4ea4a128b0a8"),
                column: "sort_order",
                value: 10);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("d80bf2be-de2f-4d72-ba02-6081b5ba77d2"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("d8c99a34-025d-455b-b317-92453da36631"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("d8f337d0-84fc-4a4d-b75c-fbe2208808ea"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("dd4556b3-d8b3-4002-8bde-68e327945916"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("e0abe26f-27da-4396-b80c-d1ceb836a8b2"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("e7e78e76-3850-4eb5-892f-d90be6c256a4"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("e9c79ae9-5498-459d-8852-9f135da7afae"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("edfad6f1-6584-4798-a09a-9f6146127d82"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("eef4a4d1-796b-4b37-96f6-f31dbccf0aeb"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("f036bca9-95d4-4526-b845-fff9208ab103"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("f15b88b2-395d-4195-af25-8b8879640baf"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("f1626a63-6bf1-442a-86ad-8a86242bde94"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("f1c2c792-f11f-43ab-8cf6-d6ff905894fc"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("f81c698e-0017-41c0-8ff9-78dbaa3d2676"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("f9cc5445-8a6e-480b-bffb-410089f55896"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("fab42540-8c9d-4b18-9341-660f60dd7644"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("fb44b625-7086-48e6-bcc6-a004dd472012"),
                column: "sort_order",
                value: 2147483647);

            migrationBuilder.UpdateData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("ff994b2c-a3bd-4676-a974-f53d4fa562ba"),
                column: "sort_order",
                value: 2147483647);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "sort_order",
                table: "select_value_mappings");
        }
    }
}
