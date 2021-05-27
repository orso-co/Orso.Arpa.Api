using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Orso.Arpa.Persistence.Migrations
{
    public partial class MusicianProfilePreferredParts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "instrument_part_count",
                table: "sections",
                type: "smallint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte[]>(
                name: "preferred_parts_performer",
                table: "musician_profiles",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "preferred_parts_staff",
                table: "musician_profiles",
                type: "bytea",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("18cbded8-0d64-4e0e-bc19-d6903e0fd5a9"),
                column: "instrument_part_count",
                value: (byte)2);

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("205b0a0e-1a36-48e9-8b45-df37dc5effa5"),
                column: "instrument_part_count",
                value: (byte)8);

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("2327a9c3-2c6f-41b7-9045-bb00af798b42"),
                column: "instrument_part_count",
                value: (byte)4);

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("2fabd3a1-d398-4108-a74f-2665710133d1"),
                column: "instrument_part_count",
                value: (byte)2);

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("305c06e0-b99f-4f91-ae83-869d8b25c63d"),
                column: "instrument_part_count",
                value: (byte)3);

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("31a2b9bf-0c2b-47ec-b8bc-34c9423b74d4"),
                column: "instrument_part_count",
                value: (byte)2);

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("4a31447d-63c2-4e28-ab39-255a956fbe18"),
                column: "instrument_part_count",
                value: (byte)2);

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("554fd3db-110b-4335-bc2a-1d5070f6621a"),
                column: "instrument_part_count",
                value: (byte)3);

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("5c14f673-13f2-488f-8c21-7286d3ee10c3"),
                column: "instrument_part_count",
                value: (byte)4);

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("69e64d64-419f-4f9c-9948-a117b02ff198"),
                column: "instrument_part_count",
                value: (byte)3);

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("803219aa-1a32-4a68-95ae-348bd487135a"),
                column: "instrument_part_count",
                value: (byte)3);

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("abe0d27b-2c99-4755-891c-fb0b91f19bb6"),
                column: "instrument_part_count",
                value: (byte)2);

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("b525e539-7fa4-49d7-ae93-ec0748022d4d"),
                column: "instrument_part_count",
                value: (byte)3);

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("b9532add-efec-4510-831c-902c32ef7dbb"),
                column: "instrument_part_count",
                value: (byte)8);

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("cdc390d5-0649-441d-a086-df2e3b9d3512"),
                column: "instrument_part_count",
                value: (byte)4);

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("d2551427-d727-42d9-be0e-dea2ae82f2d6"),
                column: "instrument_part_count",
                value: (byte)2);

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("d6961f83-e792-4ddf-b91a-ae0867caeb3b"),
                column: "instrument_part_count",
                value: (byte)4);

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("da660c21-0151-4255-a81b-4d25fede199b"),
                column: "instrument_part_count",
                value: (byte)2);

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("da998fcb-92b9-4828-976e-826e97e05cb3"),
                column: "instrument_part_count",
                value: (byte)2);

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("e20ce055-5715-42f4-97e6-4025559b15f7"),
                column: "instrument_part_count",
                value: (byte)4);

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("ea916a8d-1bce-4e87-b5b0-ff6304bb01a5"),
                column: "instrument_part_count",
                value: (byte)2);

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("ec8aeaf8-f370-4ac8-bd12-ccce0cbcfa0f"),
                column: "instrument_part_count",
                value: (byte)2);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "instrument_part_count",
                table: "sections");

            migrationBuilder.DropColumn(
                name: "preferred_parts_performer",
                table: "musician_profiles");

            migrationBuilder.DropColumn(
                name: "preferred_parts_staff",
                table: "musician_profiles");
        }
    }
}
