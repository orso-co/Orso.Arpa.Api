using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Orso.Arpa.Persistence.Migrations
{
    public partial class SelectValueSectionConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_musician_profiles_instrument_part_instrument_part_id",
                table: "musician_profiles");

            migrationBuilder.DropForeignKey(
                name: "fk_musician_profiles_positions_position_id",
                table: "musician_profiles");

            migrationBuilder.DropForeignKey(
                name: "fk_preferred_genre_musician_profiles_musician_profile_id",
                table: "preferred_genre");

            migrationBuilder.DropForeignKey(
                name: "fk_preferred_genre_select_value_mappings_select_value_mapping_",
                table: "preferred_genre");

            migrationBuilder.DropForeignKey(
                name: "fk_preferred_part_instrument_part_part_id",
                table: "preferred_part");

            migrationBuilder.DropForeignKey(
                name: "fk_preferred_part_musician_profiles_musician_profile_id",
                table: "preferred_part");

            migrationBuilder.DropForeignKey(
                name: "fk_region_preference_performances_musician_profiles_musician_p",
                table: "region_preference_performances");

            migrationBuilder.DropForeignKey(
                name: "fk_region_preference_performances_venues_venue_id",
                table: "region_preference_performances");

            migrationBuilder.DropForeignKey(
                name: "fk_region_preference_rehearsals_musician_profiles_musician_pro",
                table: "region_preference_rehearsals");

            migrationBuilder.DropForeignKey(
                name: "fk_region_preference_rehearsals_venues_venue_id",
                table: "region_preference_rehearsals");

            migrationBuilder.DropTable(
                name: "instrument_part");

            migrationBuilder.DropTable(
                name: "preferred_position");

            migrationBuilder.DropTable(
                name: "positions");

            migrationBuilder.DropIndex(
                name: "ix_musician_profiles_instrument_part_id",
                table: "musician_profiles");

            migrationBuilder.DropIndex(
                name: "ix_musician_profiles_position_id",
                table: "musician_profiles");

            migrationBuilder.DropPrimaryKey(
                name: "pk_region_preference_rehearsals",
                table: "region_preference_rehearsals");

            migrationBuilder.DropPrimaryKey(
                name: "pk_region_preference_performances",
                table: "region_preference_performances");

            migrationBuilder.DropPrimaryKey(
                name: "pk_preferred_part",
                table: "preferred_part");

            migrationBuilder.DropPrimaryKey(
                name: "pk_preferred_genre",
                table: "preferred_genre");

            migrationBuilder.DropColumn(
                name: "instrument_part_id",
                table: "musician_profiles");

            migrationBuilder.DropColumn(
                name: "position_id",
                table: "musician_profiles");

            migrationBuilder.RenameTable(
                name: "region_preference_rehearsals",
                newName: "region_preferences_rehearsal");

            migrationBuilder.RenameTable(
                name: "region_preference_performances",
                newName: "region_preferences_performance");

            migrationBuilder.RenameTable(
                name: "preferred_part",
                newName: "preferred_parts");

            migrationBuilder.RenameTable(
                name: "preferred_genre",
                newName: "preferred_genres");

            migrationBuilder.RenameIndex(
                name: "ix_region_preference_rehearsals_venue_id",
                table: "region_preferences_rehearsal",
                newName: "ix_region_preferences_rehearsal_venue_id");

            migrationBuilder.RenameIndex(
                name: "ix_region_preference_performances_venue_id",
                table: "region_preferences_performance",
                newName: "ix_region_preferences_performance_venue_id");

            migrationBuilder.RenameIndex(
                name: "ix_preferred_part_part_id",
                table: "preferred_parts",
                newName: "ix_preferred_parts_part_id");

            migrationBuilder.RenameIndex(
                name: "ix_preferred_part_musician_profile_id",
                table: "preferred_parts",
                newName: "ix_preferred_parts_musician_profile_id");

            migrationBuilder.RenameIndex(
                name: "ix_preferred_genre_select_value_mapping_id",
                table: "preferred_genres",
                newName: "ix_preferred_genres_select_value_mapping_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_region_preferences_rehearsal",
                table: "region_preferences_rehearsal",
                columns: new[] { "musician_profile_id", "venue_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_region_preferences_performance",
                table: "region_preferences_performance",
                columns: new[] { "musician_profile_id", "venue_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_preferred_parts",
                table: "preferred_parts",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_preferred_genres",
                table: "preferred_genres",
                columns: new[] { "musician_profile_id", "select_value_mapping_id" });

            migrationBuilder.CreateTable(
                name: "select_value_sections",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    section_id = table.Column<Guid>(type: "uuid", nullable: false),
                    select_value_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_select_value_sections", x => x.id);
                    table.ForeignKey(
                        name: "fk_select_value_sections_sections_section_id",
                        column: x => x.section_id,
                        principalTable: "sections",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_select_value_sections_select_values_select_value_id",
                        column: x => x.select_value_id,
                        principalTable: "select_values",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "musician_profile_positions_performer",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    select_value_section_id = table.Column<Guid>(type: "uuid", nullable: false),
                    musician_profile_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_musician_profile_positions_performer", x => x.id);
                    table.ForeignKey(
                        name: "fk_musician_profile_positions_performer_musician_profiles_musi",
                        column: x => x.musician_profile_id,
                        principalTable: "musician_profiles",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_musician_profile_positions_performer_select_value_sections_",
                        column: x => x.select_value_section_id,
                        principalTable: "select_value_sections",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "musician_profile_positions_staff",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    select_value_section_id = table.Column<Guid>(type: "uuid", nullable: false),
                    musician_profile_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_musician_profile_positions_staff", x => x.id);
                    table.ForeignKey(
                        name: "fk_musician_profile_positions_staff_musician_profiles_musician",
                        column: x => x.musician_profile_id,
                        principalTable: "musician_profiles",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_musician_profile_positions_staff_select_value_sections_sele",
                        column: x => x.select_value_section_id,
                        principalTable: "select_value_sections",
                        principalColumn: "id");
                });

            migrationBuilder.InsertData(
                table: "select_values",
                columns: new[] { "id", "created_at", "created_by", "deleted", "description", "modified_at", "modified_by", "name" },
                values: new object[,]
                {
                    { new Guid("9353f2ee-f074-488b-a359-f2fc6f66da51"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Solo" },
                    { new Guid("a0e02d9f-03b5-49e0-9ae8-b34a419bc203"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "High" },
                    { new Guid("959e5b30-6ad1-4102-8dce-f1395b8ae73e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Low" },
                    { new Guid("a89a8323-4c82-4e55-8ef1-6d7150f564e9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Coach" },
                    { new Guid("5a4a1318-2f23-45b0-8329-3eec0f446389"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Tutti" },
                    { new Guid("b85984d6-4390-44f9-bd92-5d1000cb4d3f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "1st section" },
                    { new Guid("4a9de438-ccce-4a95-873a-c8befb933067"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "2nd section" },
                    { new Guid("36c6963d-a08c-4394-823a-1d24ba8330b4"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Section lead" },
                    { new Guid("fc2c8cf2-3189-44de-a124-2debe1d7b057"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Concert master" },
                    { new Guid("9ed94828-9deb-49a9-9a65-ecb83620c82e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "2nd concert master" },
                    { new Guid("ebae975b-d9a3-4d2f-b0a3-beff554e7041"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Orchestra piano" },
                    { new Guid("61dab188-a07d-4a58-8ec9-c54050e914ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Accompaniment" }
                });

            migrationBuilder.InsertData(
                table: "select_value_sections",
                columns: new[] { "id", "created_at", "created_by", "deleted", "modified_at", "modified_by", "section_id", "select_value_id" },
                values: new object[,]
                {
                    { new Guid("4abea964-f83c-4973-a376-6e7782da6e7e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("b9532add-efec-4510-831c-902c32ef7dbb"), new Guid("9353f2ee-f074-488b-a359-f2fc6f66da51") },
                    { new Guid("c7b2bf38-3fb0-46a1-93c1-a41f3d865d96"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("cdc390d5-0649-441d-a086-df2e3b9d3512"), new Guid("9353f2ee-f074-488b-a359-f2fc6f66da51") },
                    { new Guid("5748698c-fc7f-437e-867c-d3c3dc4dcf4e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("7daa1394-a70d-4a24-88a6-ccf511d75c4d"), new Guid("9353f2ee-f074-488b-a359-f2fc6f66da51") },
                    { new Guid("b43fc897-ebcf-4d2a-8682-33b6337b5ab2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("b9532add-efec-4510-831c-902c32ef7dbb"), new Guid("a0e02d9f-03b5-49e0-9ae8-b34a419bc203") },
                    { new Guid("3ecfed41-1b06-4dca-b3e1-ed84459e2493"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("7daa1394-a70d-4a24-88a6-ccf511d75c4d"), new Guid("a0e02d9f-03b5-49e0-9ae8-b34a419bc203") },
                    { new Guid("42525d3a-e158-44ee-88b5-1a4332a77862"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("b9532add-efec-4510-831c-902c32ef7dbb"), new Guid("959e5b30-6ad1-4102-8dce-f1395b8ae73e") },
                    { new Guid("a08ba21d-c850-4485-aabc-c42a1a016953"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("7daa1394-a70d-4a24-88a6-ccf511d75c4d"), new Guid("959e5b30-6ad1-4102-8dce-f1395b8ae73e") },
                    { new Guid("2e43c349-0a3b-4860-94fc-34e87a306845"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("b9532add-efec-4510-831c-902c32ef7dbb"), new Guid("a89a8323-4c82-4e55-8ef1-6d7150f564e9") },
                    { new Guid("d3b924d1-68ad-429f-a6e4-fab48b251470"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("cdc390d5-0649-441d-a086-df2e3b9d3512"), new Guid("a89a8323-4c82-4e55-8ef1-6d7150f564e9") },
                    { new Guid("497d2236-48a4-46a2-90c5-ef6f7d13f6a8"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("7daa1394-a70d-4a24-88a6-ccf511d75c4d"), new Guid("a89a8323-4c82-4e55-8ef1-6d7150f564e9") },
                    { new Guid("1524b2d5-609c-41b2-bbd3-bba7cfa521f9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("cdc390d5-0649-441d-a086-df2e3b9d3512"), new Guid("5a4a1318-2f23-45b0-8329-3eec0f446389") }
                });

            migrationBuilder.CreateIndex(
                name: "ix_musician_profile_positions_performer_musician_profile_id",
                table: "musician_profile_positions_performer",
                column: "musician_profile_id");

            migrationBuilder.CreateIndex(
                name: "ix_musician_profile_positions_performer_select_value_section_id",
                table: "musician_profile_positions_performer",
                column: "select_value_section_id");

            migrationBuilder.CreateIndex(
                name: "ix_musician_profile_positions_staff_musician_profile_id",
                table: "musician_profile_positions_staff",
                column: "musician_profile_id");

            migrationBuilder.CreateIndex(
                name: "ix_musician_profile_positions_staff_select_value_section_id",
                table: "musician_profile_positions_staff",
                column: "select_value_section_id");

            migrationBuilder.CreateIndex(
                name: "ix_select_value_sections_section_id",
                table: "select_value_sections",
                column: "section_id");

            migrationBuilder.CreateIndex(
                name: "ix_select_value_sections_select_value_id",
                table: "select_value_sections",
                column: "select_value_id");

            migrationBuilder.AddForeignKey(
                name: "fk_preferred_genres_musician_profiles_musician_profile_id",
                table: "preferred_genres",
                column: "musician_profile_id",
                principalTable: "musician_profiles",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_preferred_genres_select_value_mappings_select_value_mapping",
                table: "preferred_genres",
                column: "select_value_mapping_id",
                principalTable: "select_value_mappings",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_preferred_parts_musician_profiles_musician_profile_id",
                table: "preferred_parts",
                column: "musician_profile_id",
                principalTable: "musician_profiles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_preferred_parts_select_value_sections_part_id",
                table: "preferred_parts",
                column: "part_id",
                principalTable: "select_value_sections",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_region_preferences_performance_musician_profiles_musician_p",
                table: "region_preferences_performance",
                column: "musician_profile_id",
                principalTable: "musician_profiles",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_region_preferences_performance_venues_venue_id",
                table: "region_preferences_performance",
                column: "venue_id",
                principalTable: "venues",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_region_preferences_rehearsal_musician_profiles_musician_pro",
                table: "region_preferences_rehearsal",
                column: "musician_profile_id",
                principalTable: "musician_profiles",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_region_preferences_rehearsal_venues_venue_id",
                table: "region_preferences_rehearsal",
                column: "venue_id",
                principalTable: "venues",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_preferred_genres_musician_profiles_musician_profile_id",
                table: "preferred_genres");

            migrationBuilder.DropForeignKey(
                name: "fk_preferred_genres_select_value_mappings_select_value_mapping",
                table: "preferred_genres");

            migrationBuilder.DropForeignKey(
                name: "fk_preferred_parts_musician_profiles_musician_profile_id",
                table: "preferred_parts");

            migrationBuilder.DropForeignKey(
                name: "fk_preferred_parts_select_value_sections_part_id",
                table: "preferred_parts");

            migrationBuilder.DropForeignKey(
                name: "fk_region_preferences_performance_musician_profiles_musician_p",
                table: "region_preferences_performance");

            migrationBuilder.DropForeignKey(
                name: "fk_region_preferences_performance_venues_venue_id",
                table: "region_preferences_performance");

            migrationBuilder.DropForeignKey(
                name: "fk_region_preferences_rehearsal_musician_profiles_musician_pro",
                table: "region_preferences_rehearsal");

            migrationBuilder.DropForeignKey(
                name: "fk_region_preferences_rehearsal_venues_venue_id",
                table: "region_preferences_rehearsal");

            migrationBuilder.DropTable(
                name: "musician_profile_positions_performer");

            migrationBuilder.DropTable(
                name: "musician_profile_positions_staff");

            migrationBuilder.DropTable(
                name: "select_value_sections");

            migrationBuilder.DropPrimaryKey(
                name: "pk_region_preferences_rehearsal",
                table: "region_preferences_rehearsal");

            migrationBuilder.DropPrimaryKey(
                name: "pk_region_preferences_performance",
                table: "region_preferences_performance");

            migrationBuilder.DropPrimaryKey(
                name: "pk_preferred_parts",
                table: "preferred_parts");

            migrationBuilder.DropPrimaryKey(
                name: "pk_preferred_genres",
                table: "preferred_genres");

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("36c6963d-a08c-4394-823a-1d24ba8330b4"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("4a9de438-ccce-4a95-873a-c8befb933067"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("61dab188-a07d-4a58-8ec9-c54050e914ac"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("9ed94828-9deb-49a9-9a65-ecb83620c82e"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("b85984d6-4390-44f9-bd92-5d1000cb4d3f"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("ebae975b-d9a3-4d2f-b0a3-beff554e7041"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("fc2c8cf2-3189-44de-a124-2debe1d7b057"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("5a4a1318-2f23-45b0-8329-3eec0f446389"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("9353f2ee-f074-488b-a359-f2fc6f66da51"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("959e5b30-6ad1-4102-8dce-f1395b8ae73e"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("a0e02d9f-03b5-49e0-9ae8-b34a419bc203"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("a89a8323-4c82-4e55-8ef1-6d7150f564e9"));

            migrationBuilder.RenameTable(
                name: "region_preferences_rehearsal",
                newName: "region_preference_rehearsals");

            migrationBuilder.RenameTable(
                name: "region_preferences_performance",
                newName: "region_preference_performances");

            migrationBuilder.RenameTable(
                name: "preferred_parts",
                newName: "preferred_part");

            migrationBuilder.RenameTable(
                name: "preferred_genres",
                newName: "preferred_genre");

            migrationBuilder.RenameIndex(
                name: "ix_region_preferences_rehearsal_venue_id",
                table: "region_preference_rehearsals",
                newName: "ix_region_preference_rehearsals_venue_id");

            migrationBuilder.RenameIndex(
                name: "ix_region_preferences_performance_venue_id",
                table: "region_preference_performances",
                newName: "ix_region_preference_performances_venue_id");

            migrationBuilder.RenameIndex(
                name: "ix_preferred_parts_part_id",
                table: "preferred_part",
                newName: "ix_preferred_part_part_id");

            migrationBuilder.RenameIndex(
                name: "ix_preferred_parts_musician_profile_id",
                table: "preferred_part",
                newName: "ix_preferred_part_musician_profile_id");

            migrationBuilder.RenameIndex(
                name: "ix_preferred_genres_select_value_mapping_id",
                table: "preferred_genre",
                newName: "ix_preferred_genre_select_value_mapping_id");

            migrationBuilder.AddColumn<Guid>(
                name: "instrument_part_id",
                table: "musician_profiles",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "position_id",
                table: "musician_profiles",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "pk_region_preference_rehearsals",
                table: "region_preference_rehearsals",
                columns: new[] { "musician_profile_id", "venue_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_region_preference_performances",
                table: "region_preference_performances",
                columns: new[] { "musician_profile_id", "venue_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_preferred_part",
                table: "preferred_part",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_preferred_genre",
                table: "preferred_genre",
                columns: new[] { "musician_profile_id", "select_value_mapping_id" });

            migrationBuilder.CreateTable(
                name: "instrument_part",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    modified_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    section_id = table.Column<Guid>(type: "uuid", nullable: false),
                    select_value_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_instrument_part", x => x.id);
                    table.ForeignKey(
                        name: "fk_instrument_part_sections_section_id",
                        column: x => x.section_id,
                        principalTable: "sections",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_instrument_part_select_values_select_value_id",
                        column: x => x.select_value_id,
                        principalTable: "select_values",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "positions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    modified_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    section_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_positions", x => x.id);
                    table.ForeignKey(
                        name: "fk_positions_sections_section_id",
                        column: x => x.section_id,
                        principalTable: "sections",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "preferred_position",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    modified_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    musician_profile_id = table.Column<Guid>(type: "uuid", nullable: false),
                    position_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_preferred_position", x => x.id);
                    table.ForeignKey(
                        name: "fk_preferred_position_musician_profiles_musician_profile_id",
                        column: x => x.musician_profile_id,
                        principalTable: "musician_profiles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_preferred_position_positions_position_id",
                        column: x => x.position_id,
                        principalTable: "positions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_musician_profiles_instrument_part_id",
                table: "musician_profiles",
                column: "instrument_part_id");

            migrationBuilder.CreateIndex(
                name: "ix_musician_profiles_position_id",
                table: "musician_profiles",
                column: "position_id");

            migrationBuilder.CreateIndex(
                name: "ix_instrument_part_section_id",
                table: "instrument_part",
                column: "section_id");

            migrationBuilder.CreateIndex(
                name: "ix_instrument_part_select_value_id",
                table: "instrument_part",
                column: "select_value_id");

            migrationBuilder.CreateIndex(
                name: "ix_positions_section_id",
                table: "positions",
                column: "section_id");

            migrationBuilder.CreateIndex(
                name: "ix_preferred_position_musician_profile_id",
                table: "preferred_position",
                column: "musician_profile_id");

            migrationBuilder.CreateIndex(
                name: "ix_preferred_position_position_id",
                table: "preferred_position",
                column: "position_id");

            migrationBuilder.AddForeignKey(
                name: "fk_musician_profiles_instrument_part_instrument_part_id",
                table: "musician_profiles",
                column: "instrument_part_id",
                principalTable: "instrument_part",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_musician_profiles_positions_position_id",
                table: "musician_profiles",
                column: "position_id",
                principalTable: "positions",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_preferred_genre_musician_profiles_musician_profile_id",
                table: "preferred_genre",
                column: "musician_profile_id",
                principalTable: "musician_profiles",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_preferred_genre_select_value_mappings_select_value_mapping_",
                table: "preferred_genre",
                column: "select_value_mapping_id",
                principalTable: "select_value_mappings",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_preferred_part_instrument_part_part_id",
                table: "preferred_part",
                column: "part_id",
                principalTable: "instrument_part",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_preferred_part_musician_profiles_musician_profile_id",
                table: "preferred_part",
                column: "musician_profile_id",
                principalTable: "musician_profiles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_region_preference_performances_musician_profiles_musician_p",
                table: "region_preference_performances",
                column: "musician_profile_id",
                principalTable: "musician_profiles",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_region_preference_performances_venues_venue_id",
                table: "region_preference_performances",
                column: "venue_id",
                principalTable: "venues",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_region_preference_rehearsals_musician_profiles_musician_pro",
                table: "region_preference_rehearsals",
                column: "musician_profile_id",
                principalTable: "musician_profiles",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_region_preference_rehearsals_venues_venue_id",
                table: "region_preference_rehearsals",
                column: "venue_id",
                principalTable: "venues",
                principalColumn: "id");
        }
    }
}
