using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Orso.Arpa.Persistence.Migrations
{
    public partial class ChangedDateTimeOffsetToDateTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Venues",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Venues",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "SelectValues",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "SelectValues",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "SelectValueMappings",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "SelectValueMappings",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "SelectValueCategories",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "SelectValueCategories",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Rooms",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Rooms",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "RehearsalRooms",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "RehearsalRooms",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Registers",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Registers",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Regions",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Regions",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Projects",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Projects",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "ProjectParticipations",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ProjectParticipations",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Persons",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Persons",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "MusicianProfiles",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "MusicianProfiles",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "ConcertRooms",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ConcertRooms",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "Appointments",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Appointments",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "Appointments",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Appointments",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "AppointmentParticipations",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "AppointmentParticipations",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Addresses",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Addresses",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("3e6c559e-8d50-488d-a1ea-5dbc0f44ba9b"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("ac9544e3-e756-486c-a1dc-62988a882ac2"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("ca3c9cce-1aee-4c50-93e1-be963542741a"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Registers",
                keyColumn: "Id",
                keyValue: new Guid("0558a5ff-ee27-44a1-82ab-d0c0cc018c3c"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Registers",
                keyColumn: "Id",
                keyValue: new Guid("1579d7e7-4f55-4532-a078-69fd1ec939da"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Registers",
                keyColumn: "Id",
                keyValue: new Guid("1bde9862-3ed5-45cd-8d80-0a52c6b4c0fb"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Registers",
                keyColumn: "Id",
                keyValue: new Guid("22d7cf92-7b29-4cf1-a6fa-2954377589b4"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Registers",
                keyColumn: "Id",
                keyValue: new Guid("308659d6-6014-4d2c-a62a-be75bf202e62"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Registers",
                keyColumn: "Id",
                keyValue: new Guid("3db46ff0-9165-46cc-8f28-6a1d52dee518"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Registers",
                keyColumn: "Id",
                keyValue: new Guid("3ed0960c-1eed-4a45-a1ef-343aa8e7b2d6"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Registers",
                keyColumn: "Id",
                keyValue: new Guid("4599103d-f220-4744-92d1-7c6993e9bda4"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Registers",
                keyColumn: "Id",
                keyValue: new Guid("48337b78-70f0-493e-911b-296632b06ef8"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Registers",
                keyColumn: "Id",
                keyValue: new Guid("50dfa2be-85e2-4638-aa53-22dadc97a844"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Registers",
                keyColumn: "Id",
                keyValue: new Guid("5d469fc5-b3e6-40b8-9fa9-542981083ce3"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Registers",
                keyColumn: "Id",
                keyValue: new Guid("61fa66ec-3103-43fe-800c-930547dff82c"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Registers",
                keyColumn: "Id",
                keyValue: new Guid("7924daef-2542-4648-a42f-4c4374ee09db"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Registers",
                keyColumn: "Id",
                keyValue: new Guid("7daa1394-a70d-4a24-88a6-ccf511d75c4d"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Registers",
                keyColumn: "Id",
                keyValue: new Guid("8470ddf0-43ab-477e-b3bc-47ede014b359"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Registers",
                keyColumn: "Id",
                keyValue: new Guid("a06431be-f9d6-44dc-8fdb-fbf8aa2bb940"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Registers",
                keyColumn: "Id",
                keyValue: new Guid("a19fa9af-dcba-48e3-bc21-be2130fa528c"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Registers",
                keyColumn: "Id",
                keyValue: new Guid("a6abdeec-8185-40ac-a418-2e422bb9adbd"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Registers",
                keyColumn: "Id",
                keyValue: new Guid("afef89cf-90e1-4d4f-83ab-d2b47e97af0f"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Registers",
                keyColumn: "Id",
                keyValue: new Guid("b289cfe7-d66e-48d8-83a9-f4b1f7710863"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Registers",
                keyColumn: "Id",
                keyValue: new Guid("b9673cfd-7cdb-472c-86e0-1304cbb3840a"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Registers",
                keyColumn: "Id",
                keyValue: new Guid("bfe0e1ca-95ce-4cb6-a9c9-3c23c70bab21"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Registers",
                keyColumn: "Id",
                keyValue: new Guid("c2cfb7a0-4981-4dda-b988-8ba74957f6a4"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Registers",
                keyColumn: "Id",
                keyValue: new Guid("c9403ca4-6b75-44c3-b567-e53bbd78fb75"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Registers",
                keyColumn: "Id",
                keyValue: new Guid("e0fdb057-c9b7-4477-be75-cbf920a26af6"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Registers",
                keyColumn: "Id",
                keyValue: new Guid("e7dd10ef-1c39-4440-9a6c-65d397f010ca"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Registers",
                keyColumn: "Id",
                keyValue: new Guid("e809ee90-23f9-44de-b80e-2fddd5ee3683"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Registers",
                keyColumn: "Id",
                keyValue: new Guid("f4c70178-d069-44dc-8956-7160c5fef52e"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValueCategories",
                keyColumn: "Id",
                keyValue: new Guid("09be8eff-72e4-40a8-a1ed-717deb185a69"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValueCategories",
                keyColumn: "Id",
                keyValue: new Guid("0fdb6218-54fa-4e94-a880-2a65fc1cf9d7"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValueCategories",
                keyColumn: "Id",
                keyValue: new Guid("1d62ed51-c99e-4b55-83d7-f9f9a5b8234e"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValueCategories",
                keyColumn: "Id",
                keyValue: new Guid("4649b6b9-1362-41c2-ac5c-884f216dff30"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValueCategories",
                keyColumn: "Id",
                keyValue: new Guid("5cf52155-927f-4d64-a482-348f952bab21"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValueCategories",
                keyColumn: "Id",
                keyValue: new Guid("791c7439-c72a-47ca-ad8d-193e2cad09e1"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValueCategories",
                keyColumn: "Id",
                keyValue: new Guid("d438c160-0588-41fa-93c3-cd33c0f97063"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValueCategories",
                keyColumn: "Id",
                keyValue: new Guid("e4ff93b9-318e-41ed-b067-51ee94f41adf"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValueCategories",
                keyColumn: "Id",
                keyValue: new Guid("f5d4763e-5862-4b62-ab92-2748ad213b10"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("0126fded-0a82-4b53-85e4-1c04a4f79296"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("0c8af1d2-ae39-464d-9e03-a1487cfd7321"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("104fc525-bb0b-49dc-b2b2-9a8f63e45c92"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("17d201fc-777b-43bb-9c46-0d07737a8ab7"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("2634c0cc-31d2-4f61-813d-7ec60fc8ab34"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("29e1142f-aa9e-4b94-ae21-9a63f7b65c15"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("319d508e-a6e2-437e-b48b-6be51e3459bd"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("36176b7e-0926-43d6-b19a-72838ccd2acd"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("3801aa69-cc4e-4fd5-947c-bfdd4e95d48e"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("466aa422-0ef2-4e7f-a6a8-d188d80491f6"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("4dc9db05-357a-43a6-ba20-f2a9e5033802"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("4e9d4a1b-cae7-4002-93a1-cef3f209146b"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("50e6049b-a9cd-400b-a475-e2563147aebc"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("547b561e-cea7-4296-9b1d-4dd41e0d5179"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("5b89cf6e-0194-4e01-bb32-8b1813a51e16"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("5b936e5f-3743-4cc3-91af-0cc8742c846e"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("609f9ece-42ce-4cc9-a89b-1fec1ddbc5fe"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("63437ce4-b63b-4558-9f91-1474b896bf1c"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("642cc60f-e582-47ed-a40f-ea490dd3d950"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("647f674a-bc2f-4d3a-9ce4-f0aefa98cd9d"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("74278b65-fd54-49d2-9cd2-061dd6318c7d"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("7fb30d45-1faf-4f6a-ac5d-436204ad8e69"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("836c69d6-46f1-40d4-b75d-6aa86f9ec066"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("86672779-5e70-4965-b59c-032086d00914"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("867622fa-7aa5-43f3-b3ef-5290d1f61734"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("88da1c17-9efc-4f69-ba0f-39c76592845b"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("8b51c75f-d597-48ef-8451-5f5fc32d57d1"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("8b7d7f26-b7e5-42e2-afc1-cedddbae841a"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("93033f7e-a3c1-45e3-8a17-021d0a4abe5a"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("9cf090a3-680d-4770-b929-0a0d080576a0"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("a39a92fe-bea2-40fa-817b-e7272bfc9d4b"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("ac1ccdd4-39aa-4767-95ea-099a829f275b"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("ade78d45-b010-4ed7-87f0-e30e0558f151"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("aedc27f3-e2e8-4368-ad69-1ab1c3dd7974"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("b09bc4a6-06ab-4d45-8b82-7971e662ccb5"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("b62cc155-f1a9-4904-8e6a-95e85339da83"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("b6cf76a5-ec3f-4e81-9499-174d33bb7249"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("bbe90120-55f3-4474-a059-1334d0adaa57"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("c1b6d08b-f31e-4f38-a8c0-761e42fbd2b7"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("c9225a82-0348-41bb-a591-7726f8079cde"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("cfc62012-4d74-4cf6-a06e-7fc3dbacbff8"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("d64abb04-dc1c-4e17-bed5-a62196a59c49"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("d733e38d-1d80-4054-b654-4ea4a128b0a8"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("d8c99a34-025d-455b-b317-92453da36631"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("dd4556b3-d8b3-4002-8bde-68e327945916"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("e7e78e76-3850-4eb5-892f-d90be6c256a4"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("e9c79ae9-5498-459d-8852-9f135da7afae"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("f15b88b2-395d-4195-af25-8b8879640baf"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("f81c698e-0017-41c0-8ff9-78dbaa3d2676"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("fb44b625-7086-48e6-bcc6-a004dd472012"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("ff994b2c-a3bd-4676-a974-f53d4fa562ba"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("130f63c3-9d2f-4301-b062-236c78663e3b"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("1e60dfdf-e7c9-4378-b1af-dcb53fe20022"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("2567e7be-5a5a-4671-b5ad-765c1e80fd41"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("313445ca-57fa-45f0-8515-325949d60726"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("34a52363-4a57-4019-abcf-0c9880246891"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("362efd25-e1d2-496d-b7fa-884371a58682"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("3a6218de-6dfc-4aa9-a2a7-f1da20fd61cb"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("404f1bfd-2819-47c2-a78b-f3dbd4bc8953"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("43d8eafa-ef3f-4034-8c88-9a0b68c33ab1"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("4418bfea-0e79-4f76-9e20-527644f654e0"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("4ee7d317-6d71-4d6e-b45a-954c8c7dcf03"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("52d67a48-e99f-4c2f-ac9b-0302d5d3e518"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("5b57a267-f331-41df-995a-93b60fc206ff"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("5d31f1f7-73fd-42a4-a429-33fab925b15d"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("5d50c5c3-e85a-4810-ac46-49572e1ca2f5"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("5e3edcf4-863b-433b-ae72-b6bb7e4dfc95"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("608b5583-a8dc-48d7-8afa-ef87ca0327f0"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("61dd102e-d449-40e1-8c6b-4ead99403ac1"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("66a6446a-7191-4f14-9c5d-052891b9c5a2"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("71779748-6d3c-496a-9842-8dc508de6676"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("717a27d5-2ef3-4266-92a8-84b3600115eb"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("75a017d3-dca5-49ec-9bbd-3b01b159ba5b"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("79de43be-57cc-484f-bff2-57f3ba78dbe9"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("7c894293-82c2-4320-82f5-f77955feae5a"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("86bf6480-787a-4fe0-9d79-0f8d0d36acc4"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("87a541e7-706a-47f3-99b3-8b2d6de7a134"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("88b763ac-8093-4c5d-a881-85be1fb8a24d"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("8f64e072-6523-4158-b92e-5c38c8ebca59"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("99d192e1-332a-494e-b821-075be14211be"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("9c0295b7-1b16-4fd6-a7de-ecd724c823b3"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("a0b98a79-7c74-4093-8f5f-79003cad219a"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("a10ce98a-b903-4dca-801d-3afb07711877"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("a4734d39-cbb9-4635-b3ae-f4e1192a6bd1"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("a85738d9-e68e-4584-bac8-ccca8d539636"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("ae6dc389-93eb-4d96-bd66-c61dd81155ea"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("b60d04e0-9841-41c9-9d24-976c8363a33d"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("c76de830-3746-449a-8f1f-bd5d9233655c"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("d6848ef8-51c6-44e3-bc29-af1df87afcc1"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("d91def3e-4c55-42c7-ac56-147846be6bfa"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("db1d2c88-a7b3-41c3-a17f-4fd7fe9faca5"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("ddb23793-af96-4ea6-9b27-5e2dcfc90b65"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("dfe6e73e-9a15-4094-80a5-151a64f3b4db"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("e030b53e-3615-4cd6-9fe6-0d818632a4b0"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("efb2b680-c904-481a-ba7c-9e6a64a998c3"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("f0f26735-b796-4a70-a20c-92e0e2910bb4"),
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "ModifiedAt",
                table: "Venues",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Venues",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "ModifiedAt",
                table: "SelectValues",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "SelectValues",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "ModifiedAt",
                table: "SelectValueMappings",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "SelectValueMappings",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "ModifiedAt",
                table: "SelectValueCategories",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "SelectValueCategories",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "ModifiedAt",
                table: "Rooms",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Rooms",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "ModifiedAt",
                table: "RehearsalRooms",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "RehearsalRooms",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "ModifiedAt",
                table: "Registers",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Registers",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "ModifiedAt",
                table: "Regions",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Regions",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "ModifiedAt",
                table: "Projects",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Projects",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "ModifiedAt",
                table: "ProjectParticipations",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "ProjectParticipations",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "ModifiedAt",
                table: "Persons",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Persons",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "ModifiedAt",
                table: "MusicianProfiles",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "MusicianProfiles",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "ModifiedAt",
                table: "ConcertRooms",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "ConcertRooms",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "StartTime",
                table: "Appointments",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "ModifiedAt",
                table: "Appointments",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "EndTime",
                table: "Appointments",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Appointments",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "ModifiedAt",
                table: "AppointmentParticipations",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "AppointmentParticipations",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "ModifiedAt",
                table: "Addresses",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Addresses",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("3e6c559e-8d50-488d-a1ea-5dbc0f44ba9b"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("ac9544e3-e756-486c-a1dc-62988a882ac2"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("ca3c9cce-1aee-4c50-93e1-be963542741a"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Registers",
                keyColumn: "Id",
                keyValue: new Guid("0558a5ff-ee27-44a1-82ab-d0c0cc018c3c"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Registers",
                keyColumn: "Id",
                keyValue: new Guid("1579d7e7-4f55-4532-a078-69fd1ec939da"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Registers",
                keyColumn: "Id",
                keyValue: new Guid("1bde9862-3ed5-45cd-8d80-0a52c6b4c0fb"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Registers",
                keyColumn: "Id",
                keyValue: new Guid("22d7cf92-7b29-4cf1-a6fa-2954377589b4"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Registers",
                keyColumn: "Id",
                keyValue: new Guid("308659d6-6014-4d2c-a62a-be75bf202e62"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Registers",
                keyColumn: "Id",
                keyValue: new Guid("3db46ff0-9165-46cc-8f28-6a1d52dee518"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Registers",
                keyColumn: "Id",
                keyValue: new Guid("3ed0960c-1eed-4a45-a1ef-343aa8e7b2d6"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Registers",
                keyColumn: "Id",
                keyValue: new Guid("4599103d-f220-4744-92d1-7c6993e9bda4"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Registers",
                keyColumn: "Id",
                keyValue: new Guid("48337b78-70f0-493e-911b-296632b06ef8"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Registers",
                keyColumn: "Id",
                keyValue: new Guid("50dfa2be-85e2-4638-aa53-22dadc97a844"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Registers",
                keyColumn: "Id",
                keyValue: new Guid("5d469fc5-b3e6-40b8-9fa9-542981083ce3"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Registers",
                keyColumn: "Id",
                keyValue: new Guid("61fa66ec-3103-43fe-800c-930547dff82c"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Registers",
                keyColumn: "Id",
                keyValue: new Guid("7924daef-2542-4648-a42f-4c4374ee09db"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Registers",
                keyColumn: "Id",
                keyValue: new Guid("7daa1394-a70d-4a24-88a6-ccf511d75c4d"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Registers",
                keyColumn: "Id",
                keyValue: new Guid("8470ddf0-43ab-477e-b3bc-47ede014b359"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Registers",
                keyColumn: "Id",
                keyValue: new Guid("a06431be-f9d6-44dc-8fdb-fbf8aa2bb940"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Registers",
                keyColumn: "Id",
                keyValue: new Guid("a19fa9af-dcba-48e3-bc21-be2130fa528c"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Registers",
                keyColumn: "Id",
                keyValue: new Guid("a6abdeec-8185-40ac-a418-2e422bb9adbd"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Registers",
                keyColumn: "Id",
                keyValue: new Guid("afef89cf-90e1-4d4f-83ab-d2b47e97af0f"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Registers",
                keyColumn: "Id",
                keyValue: new Guid("b289cfe7-d66e-48d8-83a9-f4b1f7710863"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Registers",
                keyColumn: "Id",
                keyValue: new Guid("b9673cfd-7cdb-472c-86e0-1304cbb3840a"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Registers",
                keyColumn: "Id",
                keyValue: new Guid("bfe0e1ca-95ce-4cb6-a9c9-3c23c70bab21"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Registers",
                keyColumn: "Id",
                keyValue: new Guid("c2cfb7a0-4981-4dda-b988-8ba74957f6a4"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Registers",
                keyColumn: "Id",
                keyValue: new Guid("c9403ca4-6b75-44c3-b567-e53bbd78fb75"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Registers",
                keyColumn: "Id",
                keyValue: new Guid("e0fdb057-c9b7-4477-be75-cbf920a26af6"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Registers",
                keyColumn: "Id",
                keyValue: new Guid("e7dd10ef-1c39-4440-9a6c-65d397f010ca"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Registers",
                keyColumn: "Id",
                keyValue: new Guid("e809ee90-23f9-44de-b80e-2fddd5ee3683"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Registers",
                keyColumn: "Id",
                keyValue: new Guid("f4c70178-d069-44dc-8956-7160c5fef52e"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValueCategories",
                keyColumn: "Id",
                keyValue: new Guid("09be8eff-72e4-40a8-a1ed-717deb185a69"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValueCategories",
                keyColumn: "Id",
                keyValue: new Guid("0fdb6218-54fa-4e94-a880-2a65fc1cf9d7"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValueCategories",
                keyColumn: "Id",
                keyValue: new Guid("1d62ed51-c99e-4b55-83d7-f9f9a5b8234e"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValueCategories",
                keyColumn: "Id",
                keyValue: new Guid("4649b6b9-1362-41c2-ac5c-884f216dff30"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValueCategories",
                keyColumn: "Id",
                keyValue: new Guid("5cf52155-927f-4d64-a482-348f952bab21"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValueCategories",
                keyColumn: "Id",
                keyValue: new Guid("791c7439-c72a-47ca-ad8d-193e2cad09e1"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValueCategories",
                keyColumn: "Id",
                keyValue: new Guid("d438c160-0588-41fa-93c3-cd33c0f97063"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValueCategories",
                keyColumn: "Id",
                keyValue: new Guid("e4ff93b9-318e-41ed-b067-51ee94f41adf"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValueCategories",
                keyColumn: "Id",
                keyValue: new Guid("f5d4763e-5862-4b62-ab92-2748ad213b10"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("0126fded-0a82-4b53-85e4-1c04a4f79296"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("0c8af1d2-ae39-464d-9e03-a1487cfd7321"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("104fc525-bb0b-49dc-b2b2-9a8f63e45c92"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("17d201fc-777b-43bb-9c46-0d07737a8ab7"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("2634c0cc-31d2-4f61-813d-7ec60fc8ab34"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("29e1142f-aa9e-4b94-ae21-9a63f7b65c15"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("319d508e-a6e2-437e-b48b-6be51e3459bd"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("36176b7e-0926-43d6-b19a-72838ccd2acd"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("3801aa69-cc4e-4fd5-947c-bfdd4e95d48e"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("466aa422-0ef2-4e7f-a6a8-d188d80491f6"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("4dc9db05-357a-43a6-ba20-f2a9e5033802"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("4e9d4a1b-cae7-4002-93a1-cef3f209146b"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("50e6049b-a9cd-400b-a475-e2563147aebc"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("547b561e-cea7-4296-9b1d-4dd41e0d5179"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("5b89cf6e-0194-4e01-bb32-8b1813a51e16"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("5b936e5f-3743-4cc3-91af-0cc8742c846e"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("609f9ece-42ce-4cc9-a89b-1fec1ddbc5fe"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("63437ce4-b63b-4558-9f91-1474b896bf1c"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("642cc60f-e582-47ed-a40f-ea490dd3d950"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("647f674a-bc2f-4d3a-9ce4-f0aefa98cd9d"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("74278b65-fd54-49d2-9cd2-061dd6318c7d"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("7fb30d45-1faf-4f6a-ac5d-436204ad8e69"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("836c69d6-46f1-40d4-b75d-6aa86f9ec066"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("86672779-5e70-4965-b59c-032086d00914"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("867622fa-7aa5-43f3-b3ef-5290d1f61734"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("88da1c17-9efc-4f69-ba0f-39c76592845b"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("8b51c75f-d597-48ef-8451-5f5fc32d57d1"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("8b7d7f26-b7e5-42e2-afc1-cedddbae841a"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("93033f7e-a3c1-45e3-8a17-021d0a4abe5a"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("9cf090a3-680d-4770-b929-0a0d080576a0"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("a39a92fe-bea2-40fa-817b-e7272bfc9d4b"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("ac1ccdd4-39aa-4767-95ea-099a829f275b"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("ade78d45-b010-4ed7-87f0-e30e0558f151"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("aedc27f3-e2e8-4368-ad69-1ab1c3dd7974"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("b09bc4a6-06ab-4d45-8b82-7971e662ccb5"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("b62cc155-f1a9-4904-8e6a-95e85339da83"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("b6cf76a5-ec3f-4e81-9499-174d33bb7249"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("bbe90120-55f3-4474-a059-1334d0adaa57"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("c1b6d08b-f31e-4f38-a8c0-761e42fbd2b7"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("c9225a82-0348-41bb-a591-7726f8079cde"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("cfc62012-4d74-4cf6-a06e-7fc3dbacbff8"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("d64abb04-dc1c-4e17-bed5-a62196a59c49"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("d733e38d-1d80-4054-b654-4ea4a128b0a8"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("d8c99a34-025d-455b-b317-92453da36631"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("dd4556b3-d8b3-4002-8bde-68e327945916"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("e7e78e76-3850-4eb5-892f-d90be6c256a4"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("e9c79ae9-5498-459d-8852-9f135da7afae"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("f15b88b2-395d-4195-af25-8b8879640baf"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("f81c698e-0017-41c0-8ff9-78dbaa3d2676"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("fb44b625-7086-48e6-bcc6-a004dd472012"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("ff994b2c-a3bd-4676-a974-f53d4fa562ba"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("130f63c3-9d2f-4301-b062-236c78663e3b"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("1e60dfdf-e7c9-4378-b1af-dcb53fe20022"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("2567e7be-5a5a-4671-b5ad-765c1e80fd41"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("313445ca-57fa-45f0-8515-325949d60726"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("34a52363-4a57-4019-abcf-0c9880246891"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("362efd25-e1d2-496d-b7fa-884371a58682"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("3a6218de-6dfc-4aa9-a2a7-f1da20fd61cb"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("404f1bfd-2819-47c2-a78b-f3dbd4bc8953"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("43d8eafa-ef3f-4034-8c88-9a0b68c33ab1"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("4418bfea-0e79-4f76-9e20-527644f654e0"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("4ee7d317-6d71-4d6e-b45a-954c8c7dcf03"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("52d67a48-e99f-4c2f-ac9b-0302d5d3e518"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("5b57a267-f331-41df-995a-93b60fc206ff"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("5d31f1f7-73fd-42a4-a429-33fab925b15d"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("5d50c5c3-e85a-4810-ac46-49572e1ca2f5"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("5e3edcf4-863b-433b-ae72-b6bb7e4dfc95"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("608b5583-a8dc-48d7-8afa-ef87ca0327f0"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("61dd102e-d449-40e1-8c6b-4ead99403ac1"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("66a6446a-7191-4f14-9c5d-052891b9c5a2"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("71779748-6d3c-496a-9842-8dc508de6676"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("717a27d5-2ef3-4266-92a8-84b3600115eb"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("75a017d3-dca5-49ec-9bbd-3b01b159ba5b"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("79de43be-57cc-484f-bff2-57f3ba78dbe9"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("7c894293-82c2-4320-82f5-f77955feae5a"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("86bf6480-787a-4fe0-9d79-0f8d0d36acc4"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("87a541e7-706a-47f3-99b3-8b2d6de7a134"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("88b763ac-8093-4c5d-a881-85be1fb8a24d"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("8f64e072-6523-4158-b92e-5c38c8ebca59"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("99d192e1-332a-494e-b821-075be14211be"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("9c0295b7-1b16-4fd6-a7de-ecd724c823b3"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("a0b98a79-7c74-4093-8f5f-79003cad219a"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("a10ce98a-b903-4dca-801d-3afb07711877"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("a4734d39-cbb9-4635-b3ae-f4e1192a6bd1"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("a85738d9-e68e-4584-bac8-ccca8d539636"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("ae6dc389-93eb-4d96-bd66-c61dd81155ea"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("b60d04e0-9841-41c9-9d24-976c8363a33d"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("c76de830-3746-449a-8f1f-bd5d9233655c"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("d6848ef8-51c6-44e3-bc29-af1df87afcc1"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("d91def3e-4c55-42c7-ac56-147846be6bfa"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("db1d2c88-a7b3-41c3-a17f-4fd7fe9faca5"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("ddb23793-af96-4ea6-9b27-5e2dcfc90b65"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("dfe6e73e-9a15-4094-80a5-151a64f3b4db"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("e030b53e-3615-4cd6-9fe6-0d818632a4b0"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("efb2b680-c904-481a-ba7c-9e6a64a998c3"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("f0f26735-b796-4a70-a20c-92e0e2910bb4"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }
    }
}
