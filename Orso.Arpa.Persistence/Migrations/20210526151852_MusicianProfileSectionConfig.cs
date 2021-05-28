using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Orso.Arpa.Persistence.Migrations
{
    public partial class MusicianProfileSectionConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "name",
                table: "instrument_part");

            migrationBuilder.AlterColumn<Guid>(
                name: "instrument_availability_id",
                table: "musician_profile_sections",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "select_value_id",
                table: "instrument_part",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "select_value_categories",
                columns: new[] { "id", "created_at", "created_by", "deleted", "modified_at", "modified_by", "name", "property", "table" },
                values: new object[] { new Guid("e3756ad6-de58-4c22-9a7c-363bc33c613c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Instrument Availability", "InstrumentAvailability", "MusicianProfileSection" });

            migrationBuilder.InsertData(
                table: "select_values",
                columns: new[] { "id", "created_at", "created_by", "deleted", "description", "modified_at", "modified_by", "name" },
                values: new object[,]
                {
                    { new Guid("6fbab698-993f-4268-a28e-b1f1599771c5"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Private ownership" },
                    { new Guid("e7442e9b-8c54-41ed-8607-accba2d04f61"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Need to borrow" },
                    { new Guid("28927b59-a999-4f84-abca-4f146888457f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Provision by staff" }
                });

            migrationBuilder.InsertData(
                table: "select_value_mappings",
                columns: new[] { "id", "created_at", "created_by", "deleted", "modified_at", "modified_by", "select_value_category_id", "select_value_id" },
                values: new object[,]
                {
                    { new Guid("d33ea034-0c5f-458d-bef5-26d2c12b6b03"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("e3756ad6-de58-4c22-9a7c-363bc33c613c"), new Guid("6fbab698-993f-4268-a28e-b1f1599771c5") },
                    { new Guid("c6b28eb5-e9d6-4250-bc79-6fa9bfbdbc5a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("e3756ad6-de58-4c22-9a7c-363bc33c613c"), new Guid("e7442e9b-8c54-41ed-8607-accba2d04f61") },
                    { new Guid("7869a9b0-fb13-4c00-ac7c-2fa1b27a00af"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("e3756ad6-de58-4c22-9a7c-363bc33c613c"), new Guid("28927b59-a999-4f84-abca-4f146888457f") },
                    { new Guid("0298c0d1-57e2-415a-9d6c-3f47e9ab6f22"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("e3756ad6-de58-4c22-9a7c-363bc33c613c"), new Guid("b67d1ac5-80ec-4b7d-bcb8-72e3da55f201") }
                });

            migrationBuilder.CreateIndex(
                name: "ix_musician_profile_sections_instrument_availability_id",
                table: "musician_profile_sections",
                column: "instrument_availability_id");

            migrationBuilder.CreateIndex(
                name: "ix_instrument_part_select_value_id",
                table: "instrument_part",
                column: "select_value_id");

            migrationBuilder.AddForeignKey(
                name: "fk_instrument_part_select_values_select_value_id",
                table: "instrument_part",
                column: "select_value_id",
                principalTable: "select_values",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_musician_profile_sections_select_value_mappings_instrument_",
                table: "musician_profile_sections",
                column: "instrument_availability_id",
                principalTable: "select_value_mappings",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_instrument_part_select_values_select_value_id",
                table: "instrument_part");

            migrationBuilder.DropForeignKey(
                name: "fk_musician_profile_sections_select_value_mappings_instrument_",
                table: "musician_profile_sections");

            migrationBuilder.DropIndex(
                name: "ix_musician_profile_sections_instrument_availability_id",
                table: "musician_profile_sections");

            migrationBuilder.DropIndex(
                name: "ix_instrument_part_select_value_id",
                table: "instrument_part");

            migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("0298c0d1-57e2-415a-9d6c-3f47e9ab6f22"));

            migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("7869a9b0-fb13-4c00-ac7c-2fa1b27a00af"));

            migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("c6b28eb5-e9d6-4250-bc79-6fa9bfbdbc5a"));

            migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("d33ea034-0c5f-458d-bef5-26d2c12b6b03"));

            migrationBuilder.DeleteData(
                table: "select_value_categories",
                keyColumn: "id",
                keyValue: new Guid("e3756ad6-de58-4c22-9a7c-363bc33c613c"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("28927b59-a999-4f84-abca-4f146888457f"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("6fbab698-993f-4268-a28e-b1f1599771c5"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("e7442e9b-8c54-41ed-8607-accba2d04f61"));

            migrationBuilder.DropColumn(
                name: "select_value_id",
                table: "instrument_part");

            migrationBuilder.AlterColumn<Guid>(
                name: "instrument_availability_id",
                table: "musician_profile_sections",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "instrument_part",
                type: "text",
                nullable: true);
        }
    }
}
