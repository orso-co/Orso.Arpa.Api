using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Orso.Arpa.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedSoloistSectionSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "localizations",
                columns: new[] { "id", "created_at", "created_by", "deleted", "key", "localization_culture", "modified_at", "modified_by", "resource_key", "text" },
                values: new object[,]
                {
                    { new Guid("134efd01-4c4e-42dc-8c9a-f5f355578094"), new DateTime(2023, 5, 9, 15, 59, 26, 475, DateTimeKind.Local).AddTicks(8601), "LocalizationSeedData", false, "Mezzo Soprano (Soloist)", "de", null, null, "SectionDto", "Mezzosopran (Solist)" },
                    { new Guid("191fed69-5173-44ae-bfda-0c973d036097"), new DateTime(2023, 5, 9, 15, 59, 26, 470, DateTimeKind.Local).AddTicks(1841), "LocalizationSeedData", false, "Narrator", "de", null, null, "SectionDto", "Sprecher/Erzähler" },
                    { new Guid("313b91ae-bd55-4c91-9238-3ce6a1eca0b6"), new DateTime(2023, 5, 9, 15, 59, 26, 469, DateTimeKind.Local).AddTicks(347), "LocalizationSeedData", false, "Moderator", "de", null, null, "SectionDto", "Moderator" },
                    { new Guid("34ed8754-87cf-42c8-abba-9c45ff00222e"), new DateTime(2023, 5, 9, 15, 59, 26, 471, DateTimeKind.Local).AddTicks(4046), "LocalizationSeedData", false, "Bass (Soloist)", "de", null, null, "SectionDto", "Bass (Solist)" },
                    { new Guid("6623a7ea-87b6-4a21-8317-3a672414f770"), new DateTime(2023, 5, 9, 15, 59, 26, 474, DateTimeKind.Local).AddTicks(7322), "LocalizationSeedData", false, "Alto (Soloist)", "de", null, null, "SectionDto", "Alt (Solist)" },
                    { new Guid("6bec3344-2767-47b3-8889-1b07fe8b9b6f"), new DateTime(2023, 5, 9, 15, 59, 26, 472, DateTimeKind.Local).AddTicks(5163), "LocalizationSeedData", false, "Baritone (Soloist)", "de", null, null, "SectionDto", "Bariton (Solist)" },
                    { new Guid("8e360068-a09e-401f-91dd-24762adead14"), new DateTime(2023, 5, 9, 15, 59, 26, 467, DateTimeKind.Local).AddTicks(8393), "LocalizationSeedData", false, "Vocal (Soloist)", "de", null, null, "SectionDto", "Gesang (Solist)" },
                    { new Guid("cd278f65-7762-4339-a1db-838d5c49760f"), new DateTime(2023, 5, 9, 15, 59, 26, 477, DateTimeKind.Local).AddTicks(709), "LocalizationSeedData", false, "Soprano (Soloist)", "de", null, null, "SectionDto", "Sopran (Solist)" },
                    { new Guid("f3f5c713-c37e-4059-a8f0-13fca7a8eaa7"), new DateTime(2023, 5, 9, 15, 59, 26, 473, DateTimeKind.Local).AddTicks(6048), "LocalizationSeedData", false, "Tenor (Soloist)", "de", null, null, "SectionDto", "Tenor (Solist)" }
                });

            migrationBuilder.InsertData(
                table: "sections",
                columns: new[] { "id", "created_at", "created_by", "deleted", "instrument_part_count", "is_instrument", "modified_at", "modified_by", "name", "parent_id" },
                values: new object[,]
                {
                    { new Guid("08afd287-82b6-4259-b4f4-c40b78d3b69d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, (byte)0, true, null, null, "Baritone (Soloist)", new Guid("e0fdb057-c9b7-4477-be75-cbf920a26af6") },
                    { new Guid("54cef8d8-e891-4d27-be25-94e44d3c365a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, (byte)0, true, null, null, "Soprano (Soloist)", new Guid("e0fdb057-c9b7-4477-be75-cbf920a26af6") },
                    { new Guid("5c9d7048-1c80-4e16-b783-e39cd99dfc89"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, (byte)0, true, null, null, "Narrator", new Guid("e0fdb057-c9b7-4477-be75-cbf920a26af6") },
                    { new Guid("71738278-1583-4875-9830-b182043e4ac3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, (byte)0, true, null, null, "Tenor (Soloist)", new Guid("e0fdb057-c9b7-4477-be75-cbf920a26af6") },
                    { new Guid("ac157b00-106e-4277-99f1-9404f0df96b8"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, (byte)0, true, null, null, "Vocal (Soloist)", new Guid("e0fdb057-c9b7-4477-be75-cbf920a26af6") },
                    { new Guid("d0762cb0-4a6b-4935-b560-af4f148c949a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, (byte)0, true, null, null, "Alto (Soloist)", new Guid("e0fdb057-c9b7-4477-be75-cbf920a26af6") },
                    { new Guid("d1f8bd21-efa8-41d8-96ac-fe87e2b0092f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, (byte)0, true, null, null, "Bass (Soloist)", new Guid("e0fdb057-c9b7-4477-be75-cbf920a26af6") },
                    { new Guid("e84ffc93-fc24-481c-916f-b5aef4ba2d1f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, (byte)0, true, null, null, "Mezzo Soprano (Soloist)", new Guid("e0fdb057-c9b7-4477-be75-cbf920a26af6") },
                    { new Guid("f33d5126-4cd8-41d7-8d35-4c188591ec3b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, (byte)0, true, null, null, "Moderator", new Guid("e0fdb057-c9b7-4477-be75-cbf920a26af6") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("134efd01-4c4e-42dc-8c9a-f5f355578094"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("191fed69-5173-44ae-bfda-0c973d036097"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("313b91ae-bd55-4c91-9238-3ce6a1eca0b6"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("34ed8754-87cf-42c8-abba-9c45ff00222e"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("6623a7ea-87b6-4a21-8317-3a672414f770"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("6bec3344-2767-47b3-8889-1b07fe8b9b6f"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("8e360068-a09e-401f-91dd-24762adead14"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("cd278f65-7762-4339-a1db-838d5c49760f"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("f3f5c713-c37e-4059-a8f0-13fca7a8eaa7"));

            migrationBuilder.DeleteData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("08afd287-82b6-4259-b4f4-c40b78d3b69d"));

            migrationBuilder.DeleteData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("54cef8d8-e891-4d27-be25-94e44d3c365a"));

            migrationBuilder.DeleteData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("5c9d7048-1c80-4e16-b783-e39cd99dfc89"));

            migrationBuilder.DeleteData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("71738278-1583-4875-9830-b182043e4ac3"));

            migrationBuilder.DeleteData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("ac157b00-106e-4277-99f1-9404f0df96b8"));

            migrationBuilder.DeleteData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("d0762cb0-4a6b-4935-b560-af4f148c949a"));

            migrationBuilder.DeleteData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("d1f8bd21-efa8-41d8-96ac-fe87e2b0092f"));

            migrationBuilder.DeleteData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("e84ffc93-fc24-481c-916f-b5aef4ba2d1f"));

            migrationBuilder.DeleteData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("f33d5126-4cd8-41d7-8d35-4c188591ec3b"));
        }
    }
}
