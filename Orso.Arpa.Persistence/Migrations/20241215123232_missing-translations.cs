using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Orso.Arpa.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class missingtranslations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "localizations",
                columns: new[] { "id", "created_at", "created_by", "deleted", "key", "localization_culture", "modified_at", "modified_by", "resource_key", "text" },
                values: new object[,]
                {
                    { new Guid("1cc0536c-d7a6-4a2c-aa82-3eafd001b822"), new DateTime(2024, 12, 15, 13, 32, 31, 349, DateTimeKind.Local).AddTicks(6950), "LocalizationSeedData", false, "Provision by staff", "de", null, null, "SelectValueDto", "Wird vom Verein bereitgestellt" },
                    { new Guid("2ef1df57-24be-4cdc-896c-33f5c865c606"), new DateTime(2024, 12, 15, 13, 32, 31, 350, DateTimeKind.Local).AddTicks(330), "LocalizationSeedData", false, "Private ownership", "de", null, null, "SelectValueDto", "Ich besitze eins" },
                    { new Guid("374801b3-7e93-4f6f-bf2d-5ae1a46ae99f"), new DateTime(2024, 12, 15, 13, 32, 31, 349, DateTimeKind.Local).AddTicks(3620), "LocalizationSeedData", false, "Need to borrow", "de", null, null, "SelectValueDto", "Muss extern geliehen werden" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("1cc0536c-d7a6-4a2c-aa82-3eafd001b822"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("2ef1df57-24be-4cdc-896c-33f5c865c606"));

            migrationBuilder.DeleteData(
                table: "localizations",
                keyColumn: "id",
                keyValue: new Guid("374801b3-7e93-4f6f-bf2d-5ae1a46ae99f"));
        }
    }
}
