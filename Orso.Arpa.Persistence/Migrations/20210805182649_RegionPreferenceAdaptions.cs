using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Orso.Arpa.Persistence.Migrations
{
    public partial class RegionPreferenceAdaptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_addresses_regions_region_id",
                table: "addresses");

            migrationBuilder.DropTable(
                name: "region_preferences_performance");

            migrationBuilder.DropTable(
                name: "region_preferences_rehearsal");

            migrationBuilder.DropIndex(
                name: "ix_addresses_region_id",
                table: "addresses");

            migrationBuilder.DropColumn(
                name: "region_id",
                table: "addresses");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "regions",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_for_performance",
                table: "regions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_for_rehearsal",
                table: "regions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "region_preferences",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    region_id = table.Column<Guid>(type: "uuid", nullable: false),
                    musician_profile_id = table.Column<Guid>(type: "uuid", nullable: false),
                    rating = table.Column<byte>(type: "smallint", nullable: false),
                    comment = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    type = table.Column<int>(type: "integer", nullable: false),
                    created_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_region_preferences", x => x.id);
                    table.ForeignKey(
                        name: "fk_region_preferences_musician_profiles_musician_profile_id",
                        column: x => x.musician_profile_id,
                        principalTable: "musician_profiles",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_region_preferences_regions_region_id",
                        column: x => x.region_id,
                        principalTable: "regions",
                        principalColumn: "id");
                });

            migrationBuilder.UpdateData(
                table: "regions",
                keyColumn: "id",
                keyValue: new Guid("3e6c559e-8d50-488d-a1ea-5dbc0f44ba9b"),
                column: "is_for_rehearsal",
                value: true);

            migrationBuilder.UpdateData(
                table: "regions",
                keyColumn: "id",
                keyValue: new Guid("ac9544e3-e756-486c-a1dc-62988a882ac2"),
                columns: new[] { "is_for_rehearsal", "name" },
                values: new object[] { true, "Stuttgart City" });

            migrationBuilder.UpdateData(
                table: "regions",
                keyColumn: "id",
                keyValue: new Guid("ca3c9cce-1aee-4c50-93e1-be963542741a"),
                column: "is_for_performance",
                value: true);

            migrationBuilder.InsertData(
                table: "regions",
                columns: new[] { "id", "created_at", "created_by", "deleted", "is_for_performance", "is_for_rehearsal", "modified_at", "modified_by", "name" },
                values: new object[,]
                {
                    { new Guid("a3ed2672-19bc-4561-9147-490bc0778148"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, true, false, null, null, "Baden-Württemberg" },
                    { new Guid("1cb82c0c-304c-42bd-bfc1-a3f3e8a50cba"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, true, false, null, null, "North Germany" },
                    { new Guid("f1208633-c4bb-4c07-adb3-39e3ac1e8703"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, true, false, null, null, "Germany" },
                    { new Guid("b82dd9aa-4f80-45ca-82cb-db9d0d6ea47d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, true, false, null, null, "Europe" },
                    { new Guid("3ad098d3-7367-44f3-a1c3-685d2f8c7e81"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, true, false, null, null, "Tour" },
                    { new Guid("1e0b63cb-b25c-43cc-bdbf-d0b7f00d90da"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, true, false, null, null, "Up to a 100km radius from where I live" },
                    { new Guid("92f9c1a1-0482-481b-8e34-307b4af017f0"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, true, null, null, "Berlin - Mitte" },
                    { new Guid("37d379f9-567f-4522-9301-2cf7308c669a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, true, null, null, "Berlin - Schöneberg" },
                    { new Guid("8abcbb9c-3940-4903-9ef0-ba2cffaac2bc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, true, null, null, "Up to a 20km radius from where I live" },
                    { new Guid("47fbae86-05d6-4a7c-9225-a875ea29de4b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, true, null, null, "Jamulus" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_region_preferences_musician_profile_id",
                table: "region_preferences",
                column: "musician_profile_id");

            migrationBuilder.CreateIndex(
                name: "ix_region_preferences_region_id",
                table: "region_preferences",
                column: "region_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "region_preferences");

            migrationBuilder.DeleteData(
                table: "regions",
                keyColumn: "id",
                keyValue: new Guid("1cb82c0c-304c-42bd-bfc1-a3f3e8a50cba"));

            migrationBuilder.DeleteData(
                table: "regions",
                keyColumn: "id",
                keyValue: new Guid("1e0b63cb-b25c-43cc-bdbf-d0b7f00d90da"));

            migrationBuilder.DeleteData(
                table: "regions",
                keyColumn: "id",
                keyValue: new Guid("37d379f9-567f-4522-9301-2cf7308c669a"));

            migrationBuilder.DeleteData(
                table: "regions",
                keyColumn: "id",
                keyValue: new Guid("3ad098d3-7367-44f3-a1c3-685d2f8c7e81"));

            migrationBuilder.DeleteData(
                table: "regions",
                keyColumn: "id",
                keyValue: new Guid("47fbae86-05d6-4a7c-9225-a875ea29de4b"));

            migrationBuilder.DeleteData(
                table: "regions",
                keyColumn: "id",
                keyValue: new Guid("8abcbb9c-3940-4903-9ef0-ba2cffaac2bc"));

            migrationBuilder.DeleteData(
                table: "regions",
                keyColumn: "id",
                keyValue: new Guid("92f9c1a1-0482-481b-8e34-307b4af017f0"));

            migrationBuilder.DeleteData(
                table: "regions",
                keyColumn: "id",
                keyValue: new Guid("a3ed2672-19bc-4561-9147-490bc0778148"));

            migrationBuilder.DeleteData(
                table: "regions",
                keyColumn: "id",
                keyValue: new Guid("b82dd9aa-4f80-45ca-82cb-db9d0d6ea47d"));

            migrationBuilder.DeleteData(
                table: "regions",
                keyColumn: "id",
                keyValue: new Guid("f1208633-c4bb-4c07-adb3-39e3ac1e8703"));

            migrationBuilder.DropColumn(
                name: "is_for_performance",
                table: "regions");

            migrationBuilder.DropColumn(
                name: "is_for_rehearsal",
                table: "regions");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "regions",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "region_id",
                table: "addresses",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "region_preferences_performance",
                columns: table => new
                {
                    musician_profile_id = table.Column<Guid>(type: "uuid", nullable: false),
                    venue_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false),
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    modified_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    rating = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_region_preferences_performance", x => new { x.musician_profile_id, x.venue_id });
                    table.ForeignKey(
                        name: "fk_region_preferences_performance_musician_profiles_musician_p",
                        column: x => x.musician_profile_id,
                        principalTable: "musician_profiles",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_region_preferences_performance_venues_venue_id",
                        column: x => x.venue_id,
                        principalTable: "venues",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "region_preferences_rehearsal",
                columns: table => new
                {
                    musician_profile_id = table.Column<Guid>(type: "uuid", nullable: false),
                    venue_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false),
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    modified_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    rating = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_region_preferences_rehearsal", x => new { x.musician_profile_id, x.venue_id });
                    table.ForeignKey(
                        name: "fk_region_preferences_rehearsal_musician_profiles_musician_pro",
                        column: x => x.musician_profile_id,
                        principalTable: "musician_profiles",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_region_preferences_rehearsal_venues_venue_id",
                        column: x => x.venue_id,
                        principalTable: "venues",
                        principalColumn: "id");
                });

            migrationBuilder.UpdateData(
                table: "regions",
                keyColumn: "id",
                keyValue: new Guid("ac9544e3-e756-486c-a1dc-62988a882ac2"),
                column: "name",
                value: "Stuttgart");

            migrationBuilder.CreateIndex(
                name: "ix_addresses_region_id",
                table: "addresses",
                column: "region_id");

            migrationBuilder.CreateIndex(
                name: "ix_region_preferences_performance_venue_id",
                table: "region_preferences_performance",
                column: "venue_id");

            migrationBuilder.CreateIndex(
                name: "ix_region_preferences_rehearsal_venue_id",
                table: "region_preferences_rehearsal",
                column: "venue_id");

            migrationBuilder.AddForeignKey(
                name: "fk_addresses_regions_region_id",
                table: "addresses",
                column: "region_id",
                principalTable: "regions",
                principalColumn: "id");
        }
    }
}
