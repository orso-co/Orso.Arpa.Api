using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Orso.Arpa.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddClubEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "clubs",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    created_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_clubs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "club_membership_profiles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    person_id = table.Column<Guid>(type: "uuid", nullable: false),
                    club_id = table.Column<Guid>(type: "uuid", nullable: false),
                    deviating_membership_termination_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    reason_for_deviating_membership_termination_date = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    membership_termination_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    termination_reason = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    joining_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_club_membership_profiles", x => x.id);
                    table.ForeignKey(
                        name: "fk_club_membership_profiles_clubs_club_id",
                        column: x => x.club_id,
                        principalTable: "clubs",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_club_membership_profiles_persons_person_id",
                        column: x => x.person_id,
                        principalTable: "persons",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "club_membership_types",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    club_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    termination_period_in_months = table.Column<int>(type: "integer", nullable: false),
                    created_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_club_membership_types", x => x.id);
                    table.ForeignKey(
                        name: "fk_club_membership_types_clubs_club_id",
                        column: x => x.club_id,
                        principalTable: "clubs",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "club_membership_sub_types",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    memberhsip_type_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    advantages = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    prerequisites = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    created_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_club_membership_sub_types", x => x.id);
                    table.ForeignKey(
                        name: "fk_club_membership_sub_types_club_membership_types_memberhsip_",
                        column: x => x.memberhsip_type_id,
                        principalTable: "club_membership_types",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "club_membership_contribution",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    contribution_per_year_in_euro = table.Column<decimal>(type: "numeric", nullable: false),
                    valid_from = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    voucher_per_concert_in_percent = table.Column<int>(type: "integer", nullable: false),
                    membership_sub_type_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_club_membership_contribution", x => x.id);
                    table.ForeignKey(
                        name: "fk_club_membership_contribution_club_membership_sub_types_memb",
                        column: x => x.membership_sub_type_id,
                        principalTable: "club_membership_sub_types",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "club_membership_profile_data",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    club_membership_profile_id = table.Column<Guid>(type: "uuid", nullable: false),
                    membership_sub_type_id = table.Column<Guid>(type: "uuid", nullable: false),
                    deviating_annual_contribution_in_euro = table.Column<decimal>(type: "numeric", nullable: true),
                    reason_for_deviating_annual_contribution = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    valid_from = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    direct_debit_mandate_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_club_membership_profile_data", x => x.id);
                    table.ForeignKey(
                        name: "fk_club_membership_profile_data_club_membership_profiles_club_",
                        column: x => x.club_membership_profile_id,
                        principalTable: "club_membership_profiles",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_club_membership_profile_data_club_membership_sub_types_memb",
                        column: x => x.membership_sub_type_id,
                        principalTable: "club_membership_sub_types",
                        principalColumn: "id");
                });

            migrationBuilder.InsertData(
                table: "clubs",
                columns: new[] { "id", "created_at", "created_by", "deleted", "modified_at", "modified_by", "name" },
                values: new object[,]
                {
                    { new Guid("03aa55e4-d878-4dc3-a926-f5ebe12a7347"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Orso Stuttgart e. V." },
                    { new Guid("1fbfad36-080a-4e87-a022-9b3d441e81b9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Orso Berlin e. V." },
                    { new Guid("ef604736-1e4f-4ee1-80c2-41e39f344239"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Orso Freiburg e. V." }
                });

            migrationBuilder.InsertData(
                table: "club_membership_types",
                columns: new[] { "id", "club_id", "created_at", "created_by", "deleted", "description", "modified_at", "modified_by", "name", "termination_period_in_months" },
                values: new object[,]
                {
                    { new Guid("7e831185-d348-42ed-a0af-5f5f3c470391"), new Guid("ef604736-1e4f-4ee1-80c2-41e39f344239"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Fördermitgliedschaft Freiburg", null, null, "Fördermitgliedschaft", 3 },
                    { new Guid("a553eb8b-543c-4aa6-b50d-ec6e100f9532"), new Guid("ef604736-1e4f-4ee1-80c2-41e39f344239"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Orchester Freiburg", null, null, "Orchestermitgliedschaft", 3 },
                    { new Guid("d573fb50-03e5-4ca0-8cd7-babb88e9e23b"), new Guid("ef604736-1e4f-4ee1-80c2-41e39f344239"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Chor Freiburg", null, null, "Chormitgliedschaft", 3 }
                });

            migrationBuilder.InsertData(
                table: "club_membership_sub_types",
                columns: new[] { "id", "advantages", "created_at", "created_by", "deleted", "memberhsip_type_id", "modified_at", "modified_by", "name", "prerequisites" },
                values: new object[,]
                {
                    { new Guid("0733f733-aa41-4b44-a844-5fb5918f1e1e"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, new Guid("7e831185-d348-42ed-a0af-5f5f3c470391"), null, null, "Symphony", null },
                    { new Guid("157bb518-2a4f-4594-88c4-19c8a4564913"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, new Guid("7e831185-d348-42ed-a0af-5f5f3c470391"), null, null, "Sonata", null },
                    { new Guid("1fbbfbc8-b54f-4c3c-ade9-c22f646b1b7a"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, new Guid("7e831185-d348-42ed-a0af-5f5f3c470391"), null, null, "Opera", null },
                    { new Guid("45fd4efa-247a-4bcd-af3d-68d9e8b09f3b"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, new Guid("a553eb8b-543c-4aa6-b50d-ec6e100f9532"), null, null, "Vollmitgliedschaft", null },
                    { new Guid("4fc69809-3530-4fa3-b740-91ed627067ae"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, new Guid("a553eb8b-543c-4aa6-b50d-ec6e100f9532"), null, null, "Ermäßigte Mitgliedschaft", null },
                    { new Guid("58a5d420-316b-4999-9e35-d1df11e9b7d9"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, new Guid("d573fb50-03e5-4ca0-8cd7-babb88e9e23b"), null, null, "Passive Mitgliedschaft", null },
                    { new Guid("7eebc473-2555-434d-ae90-08f3e0e3d835"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, new Guid("d573fb50-03e5-4ca0-8cd7-babb88e9e23b"), null, null, "Vollmitgliedschaft", null },
                    { new Guid("b3aa7334-2198-4f74-b033-08aec1714762"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, new Guid("d573fb50-03e5-4ca0-8cd7-babb88e9e23b"), null, null, "Ermäßigte Mitgliedschaft", null },
                    { new Guid("d2a26311-37c9-4262-9c42-d5422791bf93"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, new Guid("7e831185-d348-42ed-a0af-5f5f3c470391"), null, null, "Concerto", null }
                });

            migrationBuilder.InsertData(
                table: "club_membership_contribution",
                columns: new[] { "id", "contribution_per_year_in_euro", "created_at", "created_by", "deleted", "membership_sub_type_id", "modified_at", "modified_by", "valid_from", "voucher_per_concert_in_percent" },
                values: new object[,]
                {
                    { new Guid("08db5cb9-29a3-4d1f-834c-6e7bd51c9bee"), 300m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, new Guid("1fbbfbc8-b54f-4c3c-ade9-c22f646b1b7a"), null, null, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 30 },
                    { new Guid("46895e9e-0a64-4ba5-99b9-58a48db7d061"), 30m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, new Guid("58a5d420-316b-4999-9e35-d1df11e9b7d9"), null, null, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 10 },
                    { new Guid("573d5fab-4d19-47c7-8442-bffb18bd5111"), 150m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, new Guid("0733f733-aa41-4b44-a844-5fb5918f1e1e"), null, null, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 15 },
                    { new Guid("6dd65f56-175f-42cf-b01a-87ed60699244"), 90m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, new Guid("b3aa7334-2198-4f74-b033-08aec1714762"), null, null, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 20 },
                    { new Guid("891839d0-7d63-4ead-97a9-14b59deb40a6"), 150m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, new Guid("7eebc473-2555-434d-ae90-08f3e0e3d835"), null, null, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 20 },
                    { new Guid("c3c9cac7-2d96-46bb-a9a0-90d331aa69a1"), 100m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, new Guid("d2a26311-37c9-4262-9c42-d5422791bf93"), null, null, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 10 },
                    { new Guid("c4edb43f-9d42-4791-9bb9-cf6f7c35cb9d"), 50m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, new Guid("157bb518-2a4f-4594-88c4-19c8a4564913"), null, null, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5 },
                    { new Guid("c770da21-7b1b-483c-9411-6f5a6f54b3e5"), 60m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, new Guid("45fd4efa-247a-4bcd-af3d-68d9e8b09f3b"), null, null, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 20 },
                    { new Guid("cceffa0d-07fd-4e37-aafd-dfb70e3f0de3"), 45m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, new Guid("4fc69809-3530-4fa3-b740-91ed627067ae"), null, null, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 20 }
                });

            migrationBuilder.CreateIndex(
                name: "ix_club_membership_contribution_membership_sub_type_id",
                table: "club_membership_contribution",
                column: "membership_sub_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_club_membership_profile_data_club_membership_profile_id",
                table: "club_membership_profile_data",
                column: "club_membership_profile_id");

            migrationBuilder.CreateIndex(
                name: "ix_club_membership_profile_data_membership_sub_type_id",
                table: "club_membership_profile_data",
                column: "membership_sub_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_club_membership_profiles_club_id",
                table: "club_membership_profiles",
                column: "club_id");

            migrationBuilder.CreateIndex(
                name: "ix_club_membership_profiles_person_id",
                table: "club_membership_profiles",
                column: "person_id");

            migrationBuilder.CreateIndex(
                name: "ix_club_membership_sub_types_memberhsip_type_id",
                table: "club_membership_sub_types",
                column: "memberhsip_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_club_membership_types_club_id",
                table: "club_membership_types",
                column: "club_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "club_membership_contribution");

            migrationBuilder.DropTable(
                name: "club_membership_profile_data");

            migrationBuilder.DropTable(
                name: "club_membership_profiles");

            migrationBuilder.DropTable(
                name: "club_membership_sub_types");

            migrationBuilder.DropTable(
                name: "club_membership_types");

            migrationBuilder.DropTable(
                name: "clubs");
        }
    }
}
