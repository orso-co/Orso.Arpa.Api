using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Orso.Arpa.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddMusicPieceSelectValues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Insert SelectValueCategories for MusicPiece
            migrationBuilder.InsertData(
                table: "select_value_categories",
                columns: new[] { "id", "created_at", "created_by", "deleted", "modified_at", "modified_by", "name", "property", "table" },
                values: new object[,]
                {
                    { new Guid("a1b2c3d4-0001-4000-8000-000000000001"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Music Epoch", "Epoch", "MusicPiece" },
                    { new Guid("a1b2c3d4-0002-4000-8000-000000000002"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Music Genre", "Genre", "MusicPiece" },
                    { new Guid("a1b2c3d4-0003-4000-8000-000000000003"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Difficulty Level", "DifficultyLevel", "MusicPiece" }
                });

            // Insert SelectValues for MusicPiece
            migrationBuilder.InsertData(
                table: "select_values",
                columns: new[] { "id", "created_at", "created_by", "deleted", "description", "modified_at", "modified_by", "name" },
                values: new object[,]
                {
                    // Epochs
                    { new Guid("b0000001-0001-4000-8000-000000000001"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "ca. 1600-1750", null, null, "Baroque" },
                    { new Guid("b0000001-0002-4000-8000-000000000002"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "ca. 1750-1820", null, null, "Classical" },
                    { new Guid("b0000001-0003-4000-8000-000000000003"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "ca. 1820-1910", null, null, "Romantic" },
                    { new Guid("b0000001-0004-4000-8000-000000000004"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "ca. 1910-1975", null, null, "Modern" },
                    { new Guid("b0000001-0005-4000-8000-000000000005"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "from 1975", null, null, "Contemporary" },
                    // Genres
                    { new Guid("b0000002-0001-4000-8000-000000000001"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Symphonic Music" },
                    { new Guid("b0000002-0002-4000-8000-000000000002"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Concerto" },
                    { new Guid("b0000002-0003-4000-8000-000000000003"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Chamber Music" },
                    { new Guid("b0000002-0004-4000-8000-000000000004"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Opera / Musical Theatre" },
                    { new Guid("b0000002-0005-4000-8000-000000000005"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Sacred Music" },
                    { new Guid("b0000002-0006-4000-8000-000000000006"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Choral Music" },
                    // Difficulty Levels
                    { new Guid("b0000003-0001-4000-8000-000000000001"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Easy" },
                    { new Guid("b0000003-0002-4000-8000-000000000002"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Medium" },
                    { new Guid("b0000003-0003-4000-8000-000000000003"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Advanced" },
                    { new Guid("b0000003-0004-4000-8000-000000000004"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Expert" }
                });

            // Insert SelectValueMappings for MusicPiece
            migrationBuilder.InsertData(
                table: "select_value_mappings",
                columns: new[] { "id", "created_at", "created_by", "deleted", "modified_at", "modified_by", "select_value_category_id", "select_value_id", "sort_order" },
                values: new object[,]
                {
                    // Epoch mappings
                    { new Guid("c0000001-0001-4000-8000-000000000001"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a1b2c3d4-0001-4000-8000-000000000001"), new Guid("b0000001-0001-4000-8000-000000000001"), 10 },
                    { new Guid("c0000001-0002-4000-8000-000000000002"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a1b2c3d4-0001-4000-8000-000000000001"), new Guid("b0000001-0002-4000-8000-000000000002"), 20 },
                    { new Guid("c0000001-0003-4000-8000-000000000003"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a1b2c3d4-0001-4000-8000-000000000001"), new Guid("b0000001-0003-4000-8000-000000000003"), 30 },
                    { new Guid("c0000001-0004-4000-8000-000000000004"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a1b2c3d4-0001-4000-8000-000000000001"), new Guid("b0000001-0004-4000-8000-000000000004"), 40 },
                    { new Guid("c0000001-0005-4000-8000-000000000005"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a1b2c3d4-0001-4000-8000-000000000001"), new Guid("b0000001-0005-4000-8000-000000000005"), 50 },
                    // Genre mappings
                    { new Guid("c0000002-0001-4000-8000-000000000001"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a1b2c3d4-0002-4000-8000-000000000002"), new Guid("b0000002-0001-4000-8000-000000000001"), 10 },
                    { new Guid("c0000002-0002-4000-8000-000000000002"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a1b2c3d4-0002-4000-8000-000000000002"), new Guid("b0000002-0002-4000-8000-000000000002"), 20 },
                    { new Guid("c0000002-0003-4000-8000-000000000003"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a1b2c3d4-0002-4000-8000-000000000002"), new Guid("b0000002-0003-4000-8000-000000000003"), 30 },
                    { new Guid("c0000002-0004-4000-8000-000000000004"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a1b2c3d4-0002-4000-8000-000000000002"), new Guid("b0000002-0004-4000-8000-000000000004"), 40 },
                    { new Guid("c0000002-0005-4000-8000-000000000005"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a1b2c3d4-0002-4000-8000-000000000002"), new Guid("b0000002-0005-4000-8000-000000000005"), 50 },
                    { new Guid("c0000002-0006-4000-8000-000000000006"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a1b2c3d4-0002-4000-8000-000000000002"), new Guid("b0000002-0006-4000-8000-000000000006"), 60 },
                    // Difficulty Level mappings
                    { new Guid("c0000003-0001-4000-8000-000000000001"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a1b2c3d4-0003-4000-8000-000000000003"), new Guid("b0000003-0001-4000-8000-000000000001"), 10 },
                    { new Guid("c0000003-0002-4000-8000-000000000002"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a1b2c3d4-0003-4000-8000-000000000003"), new Guid("b0000003-0002-4000-8000-000000000002"), 20 },
                    { new Guid("c0000003-0003-4000-8000-000000000003"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a1b2c3d4-0003-4000-8000-000000000003"), new Guid("b0000003-0003-4000-8000-000000000003"), 30 },
                    { new Guid("c0000003-0004-4000-8000-000000000004"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a1b2c3d4-0003-4000-8000-000000000003"), new Guid("b0000003-0004-4000-8000-000000000004"), 40 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Delete SelectValueMappings
            migrationBuilder.DeleteData(table: "select_value_mappings", keyColumn: "id", keyValue: new Guid("c0000001-0001-4000-8000-000000000001"));
            migrationBuilder.DeleteData(table: "select_value_mappings", keyColumn: "id", keyValue: new Guid("c0000001-0002-4000-8000-000000000002"));
            migrationBuilder.DeleteData(table: "select_value_mappings", keyColumn: "id", keyValue: new Guid("c0000001-0003-4000-8000-000000000003"));
            migrationBuilder.DeleteData(table: "select_value_mappings", keyColumn: "id", keyValue: new Guid("c0000001-0004-4000-8000-000000000004"));
            migrationBuilder.DeleteData(table: "select_value_mappings", keyColumn: "id", keyValue: new Guid("c0000001-0005-4000-8000-000000000005"));
            migrationBuilder.DeleteData(table: "select_value_mappings", keyColumn: "id", keyValue: new Guid("c0000002-0001-4000-8000-000000000001"));
            migrationBuilder.DeleteData(table: "select_value_mappings", keyColumn: "id", keyValue: new Guid("c0000002-0002-4000-8000-000000000002"));
            migrationBuilder.DeleteData(table: "select_value_mappings", keyColumn: "id", keyValue: new Guid("c0000002-0003-4000-8000-000000000003"));
            migrationBuilder.DeleteData(table: "select_value_mappings", keyColumn: "id", keyValue: new Guid("c0000002-0004-4000-8000-000000000004"));
            migrationBuilder.DeleteData(table: "select_value_mappings", keyColumn: "id", keyValue: new Guid("c0000002-0005-4000-8000-000000000005"));
            migrationBuilder.DeleteData(table: "select_value_mappings", keyColumn: "id", keyValue: new Guid("c0000002-0006-4000-8000-000000000006"));
            migrationBuilder.DeleteData(table: "select_value_mappings", keyColumn: "id", keyValue: new Guid("c0000003-0001-4000-8000-000000000001"));
            migrationBuilder.DeleteData(table: "select_value_mappings", keyColumn: "id", keyValue: new Guid("c0000003-0002-4000-8000-000000000002"));
            migrationBuilder.DeleteData(table: "select_value_mappings", keyColumn: "id", keyValue: new Guid("c0000003-0003-4000-8000-000000000003"));
            migrationBuilder.DeleteData(table: "select_value_mappings", keyColumn: "id", keyValue: new Guid("c0000003-0004-4000-8000-000000000004"));

            // Delete SelectValues
            migrationBuilder.DeleteData(table: "select_values", keyColumn: "id", keyValue: new Guid("b0000001-0001-4000-8000-000000000001"));
            migrationBuilder.DeleteData(table: "select_values", keyColumn: "id", keyValue: new Guid("b0000001-0002-4000-8000-000000000002"));
            migrationBuilder.DeleteData(table: "select_values", keyColumn: "id", keyValue: new Guid("b0000001-0003-4000-8000-000000000003"));
            migrationBuilder.DeleteData(table: "select_values", keyColumn: "id", keyValue: new Guid("b0000001-0004-4000-8000-000000000004"));
            migrationBuilder.DeleteData(table: "select_values", keyColumn: "id", keyValue: new Guid("b0000001-0005-4000-8000-000000000005"));
            migrationBuilder.DeleteData(table: "select_values", keyColumn: "id", keyValue: new Guid("b0000002-0001-4000-8000-000000000001"));
            migrationBuilder.DeleteData(table: "select_values", keyColumn: "id", keyValue: new Guid("b0000002-0002-4000-8000-000000000002"));
            migrationBuilder.DeleteData(table: "select_values", keyColumn: "id", keyValue: new Guid("b0000002-0003-4000-8000-000000000003"));
            migrationBuilder.DeleteData(table: "select_values", keyColumn: "id", keyValue: new Guid("b0000002-0004-4000-8000-000000000004"));
            migrationBuilder.DeleteData(table: "select_values", keyColumn: "id", keyValue: new Guid("b0000002-0005-4000-8000-000000000005"));
            migrationBuilder.DeleteData(table: "select_values", keyColumn: "id", keyValue: new Guid("b0000002-0006-4000-8000-000000000006"));
            migrationBuilder.DeleteData(table: "select_values", keyColumn: "id", keyValue: new Guid("b0000003-0001-4000-8000-000000000001"));
            migrationBuilder.DeleteData(table: "select_values", keyColumn: "id", keyValue: new Guid("b0000003-0002-4000-8000-000000000002"));
            migrationBuilder.DeleteData(table: "select_values", keyColumn: "id", keyValue: new Guid("b0000003-0003-4000-8000-000000000003"));
            migrationBuilder.DeleteData(table: "select_values", keyColumn: "id", keyValue: new Guid("b0000003-0004-4000-8000-000000000004"));

            // Delete SelectValueCategories
            migrationBuilder.DeleteData(table: "select_value_categories", keyColumn: "id", keyValue: new Guid("a1b2c3d4-0001-4000-8000-000000000001"));
            migrationBuilder.DeleteData(table: "select_value_categories", keyColumn: "id", keyValue: new Guid("a1b2c3d4-0002-4000-8000-000000000002"));
            migrationBuilder.DeleteData(table: "select_value_categories", keyColumn: "id", keyValue: new Guid("a1b2c3d4-0003-4000-8000-000000000003"));
        }
    }
}
