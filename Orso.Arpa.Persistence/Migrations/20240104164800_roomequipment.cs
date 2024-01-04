using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Orso.Arpa.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RoomEquipment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "capacity_id",
                table: "rooms",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ceiling_height",
                table: "rooms",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "room_equipment",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    equipment_id = table.Column<Guid>(type: "uuid", nullable: true),
                    room_id = table.Column<Guid>(type: "uuid", nullable: true),
                    count = table.Column<int>(type: "integer", nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    created_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_room_equipment", x => x.id);
                    table.ForeignKey(
                        name: "fk_room_equipment_rooms_room_id",
                        column: x => x.room_id,
                        principalTable: "rooms",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_room_equipment_select_value_mappings_equipment_id",
                        column: x => x.equipment_id,
                        principalTable: "select_value_mappings",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "room_section",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    room_id = table.Column<Guid>(type: "uuid", nullable: false),
                    section_id = table.Column<Guid>(type: "uuid", nullable: false),
                    count = table.Column<int>(type: "integer", nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    created_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_room_section", x => x.id);
                    table.ForeignKey(
                        name: "fk_room_section_rooms_room_id",
                        column: x => x.room_id,
                        principalTable: "rooms",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_room_section_sections_section_id",
                        column: x => x.section_id,
                        principalTable: "sections",
                        principalColumn: "id");
                });

            migrationBuilder.InsertData(
                table: "localizations",
                columns: new[] { "id", "created_at", "created_by", "deleted", "key", "localization_culture", "modified_at", "modified_by", "resource_key", "text" },
                values: new object[,]
                {
                    { new Guid("19ad1ca5-f6d2-41ba-b914-5ecb618e5a47"), new DateTime(2024, 1, 4, 17, 42, 24, 461, DateTimeKind.Local).AddTicks(1600), "LocalizationSeedData", false, "Chairs", "de", null, null, "SelectValueDto", "Stühle" },
                    { new Guid("203b17e0-a680-463c-8f58-bf83568303d2"), new DateTime(2024, 1, 4, 17, 42, 24, 464, DateTimeKind.Local).AddTicks(180), "LocalizationSeedData", false, "Voice Rehearsal", "de", null, null, "SelectValueDto", "Stimmprobe" },
                    { new Guid("2b56278e-4c70-4c39-9873-d2988ce2f2e8"), new DateTime(2024, 1, 4, 17, 42, 24, 461, DateTimeKind.Local).AddTicks(5060), "LocalizationSeedData", false, "Beamer", "de", null, null, "SelectValueDto", "Beamer" },
                    { new Guid("3a03d196-411f-4029-b185-890ecb17e86a"), new DateTime(2024, 1, 4, 17, 42, 24, 461, DateTimeKind.Local).AddTicks(8520), "LocalizationSeedData", false, "Flipchart", "de", null, null, "SelectValueDto", "Flipchart" },
                    { new Guid("66e772c7-cef9-4feb-8bcd-1c3faf3635bb"), new DateTime(2024, 1, 4, 17, 42, 24, 460, DateTimeKind.Local).AddTicks(4670), "LocalizationSeedData", false, "Tableware", "de", null, null, "SelectValueDto", "Geschirr" },
                    { new Guid("6f3499ca-2600-4f52-910e-9e8efcc7f2a8"), new DateTime(2024, 1, 4, 17, 42, 24, 462, DateTimeKind.Local).AddTicks(5530), "LocalizationSeedData", false, "Stage", "de", null, null, "SelectValueDto", "Bühne" },
                    { new Guid("707c9863-5631-4da2-a594-337e850d6a29"), new DateTime(2024, 1, 4, 17, 42, 24, 462, DateTimeKind.Local).AddTicks(9210), "LocalizationSeedData", false, "Drinking Fointain", "de", null, null, "SelectValueDto", "Trinkbrunnen" },
                    { new Guid("713593d0-7a35-46ae-9de6-0e29e9036577"), new DateTime(2024, 1, 4, 17, 42, 24, 463, DateTimeKind.Local).AddTicks(3070), "LocalizationSeedData", false, "Music Stands", "de", null, null, "SelectValueDto", "Notenpulte" },
                    { new Guid("735e6d6c-f28b-4889-af35-9c24ecad70ae"), new DateTime(2024, 1, 4, 17, 42, 24, 464, DateTimeKind.Local).AddTicks(3690), "LocalizationSeedData", false, "Orchestra", "de", null, null, "SelectValueDto", "Orchester" },
                    { new Guid("8de7e728-100f-4a2b-9687-85decd703d2a"), new DateTime(2024, 1, 4, 17, 42, 24, 460, DateTimeKind.Local).AddTicks(1240), "LocalizationSeedData", false, "Kitchen", "de", null, null, "SelectValueDto", "Küche" },
                    { new Guid("93117938-9c44-42ce-b8dd-8886dd59de7f"), new DateTime(2024, 1, 4, 17, 42, 24, 463, DateTimeKind.Local).AddTicks(6750), "LocalizationSeedData", false, "Choir", "de", null, null, "SelectValueDto", "Chor" },
                    { new Guid("9b09794f-822b-4a8e-865b-350b2eb653f7"), new DateTime(2024, 1, 4, 17, 42, 24, 462, DateTimeKind.Local).AddTicks(2000), "LocalizationSeedData", false, "Platforms", "de", null, null, "SelectValueDto", "Podeste" },
                    { new Guid("9bd3569b-8d70-4dc3-a95a-60f1515f237f"), new DateTime(2024, 1, 4, 17, 42, 24, 459, DateTimeKind.Local).AddTicks(7710), "LocalizationSeedData", false, "WLAN", "de", null, null, "SelectValueDto", "WLAN" },
                    { new Guid("bbfae343-d69a-4538-8495-dae831ad0698"), new DateTime(2024, 1, 4, 17, 42, 24, 460, DateTimeKind.Local).AddTicks(8160), "LocalizationSeedData", false, "Tables", "de", null, null, "SelectValueDto", "Tische" }
                });

            migrationBuilder.InsertData(
                table: "select_value_categories",
                columns: new[] { "id", "created_at", "created_by", "deleted", "modified_at", "modified_by", "name", "property", "table" },
                values: new object[,]
                {
                    { new Guid("29a3e970-6650-4050-8cc8-2f5120b7fec9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Room equipment type", "Equipment", "RoomEquipment" },
                    { new Guid("a0f655d9-2044-4a79-b717-118e7397e697"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Room capacity", "Capacity", "Room" }
                });

            migrationBuilder.InsertData(
                table: "select_values",
                columns: new[] { "id", "created_at", "created_by", "deleted", "description", "modified_at", "modified_by", "name" },
                values: new object[,]
                {
                    { new Guid("115b0f67-f259-4b78-86d6-fd837a5b3986"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Beamer" },
                    { new Guid("172e5980-4381-4e7d-99e2-2dc7a9082579"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Tableware" },
                    { new Guid("1fc8766b-1693-4b6a-b1c3-e2ba25293175"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Kitchen" },
                    { new Guid("2c506352-db12-4e03-9b3d-5ac1e527e268"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Choir" },
                    { new Guid("3f2adf28-6ce1-4e13-a2ca-6a15c158bc3a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Stage" },
                    { new Guid("4d9bc543-ff88-496c-89ac-6e7ea38cc6de"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Music Stands" },
                    { new Guid("58e055b5-9f05-4989-8afa-244e9c759418"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Platforms" },
                    { new Guid("7c5479fb-dac2-444d-bce7-5652261148ae"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Flipchart" },
                    { new Guid("8fc47f52-825c-4084-beb2-6b135347a4ce"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Tables" },
                    { new Guid("b1d0272b-25c0-4f16-b81e-7c33ba8c7d9b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "WLAN" },
                    { new Guid("b26488c2-b4e8-485f-b3bd-cbded38fb1f4"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Chairs" },
                    { new Guid("b35fa703-962a-4dee-a593-742f2904d6b0"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Voice Rehearsal" },
                    { new Guid("c2e266e6-2245-4c87-9dbe-4ac478179b96"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Drinking Fountain" },
                    { new Guid("e0317d1f-3d4d-4e23-9afb-c800f307a930"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Orchestra" }
                });

            migrationBuilder.InsertData(
                table: "select_value_mappings",
                columns: new[] { "id", "created_at", "created_by", "deleted", "modified_at", "modified_by", "select_value_category_id", "select_value_id", "sort_order" },
                values: new object[,]
                {
                    { new Guid("006691f9-2948-4aa4-8d92-0f05643220e0"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("29a3e970-6650-4050-8cc8-2f5120b7fec9"), new Guid("8fc47f52-825c-4084-beb2-6b135347a4ce"), 60 },
                    { new Guid("2bf96d06-b639-45af-82f2-409d020e7fbb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("29a3e970-6650-4050-8cc8-2f5120b7fec9"), new Guid("1fc8766b-1693-4b6a-b1c3-e2ba25293175"), 20 },
                    { new Guid("47faa760-b0a2-4900-9505-1b61d4ef99f2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a0f655d9-2044-4a79-b717-118e7397e697"), new Guid("e0317d1f-3d4d-4e23-9afb-c800f307a930"), 30 },
                    { new Guid("4ed969a3-ba48-4116-b934-6ff1bb6719ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a0f655d9-2044-4a79-b717-118e7397e697"), new Guid("5a4a1318-2f23-45b0-8329-3eec0f446389"), 40 },
                    { new Guid("5d13ce58-e896-4443-9a63-5068ef6289b2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("29a3e970-6650-4050-8cc8-2f5120b7fec9"), new Guid("7c5479fb-dac2-444d-bce7-5652261148ae"), 40 },
                    { new Guid("5e49bb48-fadd-48d9-bac5-dd567002b978"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("29a3e970-6650-4050-8cc8-2f5120b7fec9"), new Guid("3f2adf28-6ce1-4e13-a2ca-6a15c158bc3a"), 90 },
                    { new Guid("5f1ce14f-7d85-465d-8cfb-dd551d09d4a5"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("29a3e970-6650-4050-8cc8-2f5120b7fec9"), new Guid("115b0f67-f259-4b78-86d6-fd837a5b3986"), 50 },
                    { new Guid("6b82e9fa-cb38-410d-a35d-1ed84cae902f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("29a3e970-6650-4050-8cc8-2f5120b7fec9"), new Guid("c2e266e6-2245-4c87-9dbe-4ac478179b96"), 110 },
                    { new Guid("9814654f-b7af-42f9-a77c-434899714652"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a0f655d9-2044-4a79-b717-118e7397e697"), new Guid("b35fa703-962a-4dee-a593-742f2904d6b0"), 10 },
                    { new Guid("a348e3e6-d110-42b4-9865-1d8e96a746bc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("29a3e970-6650-4050-8cc8-2f5120b7fec9"), new Guid("4d9bc543-ff88-496c-89ac-6e7ea38cc6de"), 100 },
                    { new Guid("cb08b618-a2f0-4c5b-872c-6b6821453429"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a0f655d9-2044-4a79-b717-118e7397e697"), new Guid("2c506352-db12-4e03-9b3d-5ac1e527e268"), 20 },
                    { new Guid("d658dd90-c17d-4005-b6bd-6ede3e9166d6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("29a3e970-6650-4050-8cc8-2f5120b7fec9"), new Guid("58e055b5-9f05-4989-8afa-244e9c759418"), 80 },
                    { new Guid("e9a135f7-9894-42d0-9175-df1f0b53172b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("29a3e970-6650-4050-8cc8-2f5120b7fec9"), new Guid("172e5980-4381-4e7d-99e2-2dc7a9082579"), 30 },
                    { new Guid("eec36685-52a5-42a4-969d-aa7946f3b14b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("29a3e970-6650-4050-8cc8-2f5120b7fec9"), new Guid("b1d0272b-25c0-4f16-b81e-7c33ba8c7d9b"), 10 },
                    { new Guid("efe41455-e9d5-4bcc-94b8-086a5926df96"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("29a3e970-6650-4050-8cc8-2f5120b7fec9"), new Guid("b26488c2-b4e8-485f-b3bd-cbded38fb1f4"), 70 }
                });

            migrationBuilder.CreateIndex(
                name: "ix_rooms_capacity_id",
                table: "rooms",
                column: "capacity_id");

            migrationBuilder.CreateIndex(
                name: "ix_room_equipment_equipment_id",
                table: "room_equipment",
                column: "equipment_id");

            migrationBuilder.CreateIndex(
                name: "ix_room_equipment_room_id",
                table: "room_equipment",
                column: "room_id");

            migrationBuilder.CreateIndex(
                name: "ix_room_section_room_id",
                table: "room_section",
                column: "room_id");

            migrationBuilder.CreateIndex(
                name: "ix_room_section_section_id",
                table: "room_section",
                column: "section_id");

            migrationBuilder.AddForeignKey(
                name: "fk_rooms_select_value_mappings_capacity_id",
                table: "rooms",
                column: "capacity_id",
                principalTable: "select_value_mappings",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_rooms_select_value_mappings_capacity_id",
                table: "rooms");

            migrationBuilder.DropTable(
                name: "room_equipment");

            migrationBuilder.DropTable(
                name: "room_section");

            migrationBuilder.DropIndex(
                name: "ix_rooms_capacity_id",
                table: "rooms");

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("19ad1ca5-f6d2-41ba-b914-5ecb618e5a47"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("203b17e0-a680-463c-8f58-bf83568303d2"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("2b56278e-4c70-4c39-9873-d2988ce2f2e8"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("3a03d196-411f-4029-b185-890ecb17e86a"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("66e772c7-cef9-4feb-8bcd-1c3faf3635bb"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("6f3499ca-2600-4f52-910e-9e8efcc7f2a8"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("707c9863-5631-4da2-a594-337e850d6a29"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("713593d0-7a35-46ae-9de6-0e29e9036577"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("735e6d6c-f28b-4889-af35-9c24ecad70ae"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("8de7e728-100f-4a2b-9687-85decd703d2a"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("93117938-9c44-42ce-b8dd-8886dd59de7f"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("9b09794f-822b-4a8e-865b-350b2eb653f7"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("9bd3569b-8d70-4dc3-a95a-60f1515f237f"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("bbfae343-d69a-4538-8495-dae831ad0698"));

            migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("006691f9-2948-4aa4-8d92-0f05643220e0"));

            migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("2bf96d06-b639-45af-82f2-409d020e7fbb"));

            migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("47faa760-b0a2-4900-9505-1b61d4ef99f2"));

            migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("4ed969a3-ba48-4116-b934-6ff1bb6719ac"));

            migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("5d13ce58-e896-4443-9a63-5068ef6289b2"));

            migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("5e49bb48-fadd-48d9-bac5-dd567002b978"));

            migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("5f1ce14f-7d85-465d-8cfb-dd551d09d4a5"));

            migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("6b82e9fa-cb38-410d-a35d-1ed84cae902f"));

            migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("9814654f-b7af-42f9-a77c-434899714652"));

            migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("a348e3e6-d110-42b4-9865-1d8e96a746bc"));

            migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("cb08b618-a2f0-4c5b-872c-6b6821453429"));

            migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("d658dd90-c17d-4005-b6bd-6ede3e9166d6"));

            migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("e9a135f7-9894-42d0-9175-df1f0b53172b"));

            migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("eec36685-52a5-42a4-969d-aa7946f3b14b"));

            migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("efe41455-e9d5-4bcc-94b8-086a5926df96"));

            migrationBuilder.DeleteData(
                table: "select_value_categories",
                keyColumn: "id",
                keyValue: new Guid("29a3e970-6650-4050-8cc8-2f5120b7fec9"));

            migrationBuilder.DeleteData(
                table: "select_value_categories",
                keyColumn: "id",
                keyValue: new Guid("a0f655d9-2044-4a79-b717-118e7397e697"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("115b0f67-f259-4b78-86d6-fd837a5b3986"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("172e5980-4381-4e7d-99e2-2dc7a9082579"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("1fc8766b-1693-4b6a-b1c3-e2ba25293175"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("2c506352-db12-4e03-9b3d-5ac1e527e268"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("3f2adf28-6ce1-4e13-a2ca-6a15c158bc3a"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("4d9bc543-ff88-496c-89ac-6e7ea38cc6de"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("58e055b5-9f05-4989-8afa-244e9c759418"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("7c5479fb-dac2-444d-bce7-5652261148ae"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("8fc47f52-825c-4084-beb2-6b135347a4ce"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("b1d0272b-25c0-4f16-b81e-7c33ba8c7d9b"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("b26488c2-b4e8-485f-b3bd-cbded38fb1f4"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("b35fa703-962a-4dee-a593-742f2904d6b0"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("c2e266e6-2245-4c87-9dbe-4ac478179b96"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("e0317d1f-3d4d-4e23-9afb-c800f307a930"));

            migrationBuilder.DropColumn(
                name: "capacity_id",
                table: "rooms");

            migrationBuilder.DropColumn(
                name: "ceiling_height",
                table: "rooms");
        }
    }
}
