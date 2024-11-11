using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Orso.Arpa.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddClubSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "deviating_voucher_per_concert_for_participants_in_percent",
                table: "club_membership_contribution",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "club_membership_contribution",
                keyColumn: "id",
                keyValue: new Guid("08db5cb9-29a3-4d1f-834c-6e7bd51c9bee"),
                column: "deviating_voucher_per_concert_for_participants_in_percent",
                value: null);

            migrationBuilder.UpdateData(
                table: "club_membership_contribution",
                keyColumn: "id",
                keyValue: new Guid("46895e9e-0a64-4ba5-99b9-58a48db7d061"),
                column: "deviating_voucher_per_concert_for_participants_in_percent",
                value: null);

            migrationBuilder.UpdateData(
                table: "club_membership_contribution",
                keyColumn: "id",
                keyValue: new Guid("573d5fab-4d19-47c7-8442-bffb18bd5111"),
                column: "deviating_voucher_per_concert_for_participants_in_percent",
                value: null);

            migrationBuilder.UpdateData(
                table: "club_membership_contribution",
                keyColumn: "id",
                keyValue: new Guid("6dd65f56-175f-42cf-b01a-87ed60699244"),
                column: "deviating_voucher_per_concert_for_participants_in_percent",
                value: null);

            migrationBuilder.UpdateData(
                table: "club_membership_contribution",
                keyColumn: "id",
                keyValue: new Guid("891839d0-7d63-4ead-97a9-14b59deb40a6"),
                column: "deviating_voucher_per_concert_for_participants_in_percent",
                value: null);

            migrationBuilder.UpdateData(
                table: "club_membership_contribution",
                keyColumn: "id",
                keyValue: new Guid("c3c9cac7-2d96-46bb-a9a0-90d331aa69a1"),
                column: "deviating_voucher_per_concert_for_participants_in_percent",
                value: null);

            migrationBuilder.UpdateData(
                table: "club_membership_contribution",
                keyColumn: "id",
                keyValue: new Guid("c4edb43f-9d42-4791-9bb9-cf6f7c35cb9d"),
                column: "deviating_voucher_per_concert_for_participants_in_percent",
                value: null);

            migrationBuilder.UpdateData(
                table: "club_membership_contribution",
                keyColumn: "id",
                keyValue: new Guid("c770da21-7b1b-483c-9411-6f5a6f54b3e5"),
                column: "deviating_voucher_per_concert_for_participants_in_percent",
                value: null);

            migrationBuilder.UpdateData(
                table: "club_membership_contribution",
                keyColumn: "id",
                keyValue: new Guid("cceffa0d-07fd-4e37-aafd-dfb70e3f0de3"),
                column: "deviating_voucher_per_concert_for_participants_in_percent",
                value: null);

            migrationBuilder.InsertData(
                table: "club_membership_types",
                columns: new[] { "id", "club_id", "created_at", "created_by", "deleted", "description", "modified_at", "modified_by", "name", "termination_period_in_months" },
                values: new object[,]
                {
                    { new Guid("1bcb11b9-2c7b-4d79-a762-dc43e4718446"), new Guid("1fbfad36-080a-4e87-a022-9b3d441e81b9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Fördermitgliedschaft Berlin", null, null, "Fördermitgliedschaft", 3 },
                    { new Guid("7465dfc2-d55d-4faa-be76-1fea524f9135"), new Guid("03aa55e4-d878-4dc3-a926-f5ebe12a7347"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Fördermitgliedschaft Stuttgart", null, null, "Fördermitgliedschaft", 3 },
                    { new Guid("931de5ba-bcab-422f-b3d3-721f787bface"), new Guid("1fbfad36-080a-4e87-a022-9b3d441e81b9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Orchester Berlin", null, null, "Orchestermitgliedschaft", 3 },
                    { new Guid("b1d82548-d19d-4d96-b684-e6d3b9748d90"), new Guid("03aa55e4-d878-4dc3-a926-f5ebe12a7347"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Ordentliche Mitgliedschaft Stuttgart", null, null, "Ordentliche Mitgliedschaft", 3 },
                    { new Guid("ba084757-3f98-4b87-8e4a-9f2e497b95c8"), new Guid("1fbfad36-080a-4e87-a022-9b3d441e81b9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Chor Berlin", null, null, "Chormitgliedschaft", 3 }
                });

            migrationBuilder.InsertData(
                table: "club_membership_sub_types",
                columns: new[] { "id", "advantages", "created_at", "created_by", "deleted", "memberhsip_type_id", "modified_at", "modified_by", "name", "prerequisites" },
                values: new object[,]
                {
                    { new Guid("1435c3c5-6aaa-4b81-a269-f2b6f0e831b2"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, new Guid("b1d82548-d19d-4d96-b684-e6d3b9748d90"), null, null, "Ermäßigte Mitgliedschaft", null },
                    { new Guid("2b3c4a88-39d7-49a2-8dfb-f4be156c06ed"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, new Guid("1bcb11b9-2c7b-4d79-a762-dc43e4718446"), null, null, "Sonata", null },
                    { new Guid("378b5352-8d96-412c-af28-e48b296cb360"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, new Guid("7465dfc2-d55d-4faa-be76-1fea524f9135"), null, null, "Concerto", null },
                    { new Guid("38e971b6-6592-4efe-8919-8b19a2c365d7"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, new Guid("7465dfc2-d55d-4faa-be76-1fea524f9135"), null, null, "Symphony", null },
                    { new Guid("55f4067d-f88e-4f51-a33e-d6e53597d275"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, new Guid("1bcb11b9-2c7b-4d79-a762-dc43e4718446"), null, null, "Opera", null },
                    { new Guid("644547ec-330b-4b70-8987-94622074b514"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, new Guid("931de5ba-bcab-422f-b3d3-721f787bface"), null, null, "Ermäßigte Mitgliedschaft", null },
                    { new Guid("6df7f8fd-08cc-4188-8b65-a5ecbfadac49"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, new Guid("931de5ba-bcab-422f-b3d3-721f787bface"), null, null, "Vollmitgliedschaft", null },
                    { new Guid("7cf3b7b1-c9df-49ea-b7d4-f1063900fb48"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, new Guid("ba084757-3f98-4b87-8e4a-9f2e497b95c8"), null, null, "Ermäßigte Mitgliedschaft", null },
                    { new Guid("80f328c9-48c9-4d72-9811-e3b93afae405"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, new Guid("b1d82548-d19d-4d96-b684-e6d3b9748d90"), null, null, "Vollmitgliedschaft", null },
                    { new Guid("9ac25c3c-af53-4aa7-a1fb-13bb899f5dba"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, new Guid("1bcb11b9-2c7b-4d79-a762-dc43e4718446"), null, null, "Concerto", null },
                    { new Guid("9ceedd14-edaa-4c59-b25d-6187a07fac7d"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, new Guid("7465dfc2-d55d-4faa-be76-1fea524f9135"), null, null, "Opera", null },
                    { new Guid("f472ae22-e9ba-4091-9635-e63b4eebcec1"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, new Guid("1bcb11b9-2c7b-4d79-a762-dc43e4718446"), null, null, "Symphony", null },
                    { new Guid("fd21006f-ec6b-41fb-b383-0a11f6623707"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, new Guid("ba084757-3f98-4b87-8e4a-9f2e497b95c8"), null, null, "Vollmitgliedschaft", null }
                });

            migrationBuilder.InsertData(
                table: "club_membership_contribution",
                columns: new[] { "id", "contribution_per_year_in_euro", "created_at", "created_by", "deleted", "deviating_voucher_per_concert_for_participants_in_percent", "membership_sub_type_id", "modified_at", "modified_by", "valid_from", "voucher_per_concert_in_percent" },
                values: new object[,]
                {
                    { new Guid("00b78bd4-196e-433a-ae10-e99003350e83"), 150m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, 20, new Guid("80f328c9-48c9-4d72-9811-e3b93afae405"), null, null, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 10 },
                    { new Guid("0381a852-c83b-4b7e-a804-4be46851f696"), 100m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, 10, new Guid("378b5352-8d96-412c-af28-e48b296cb360"), null, null, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 10 },
                    { new Guid("0a8c5f09-419d-44c6-a717-92a7534eb192"), 150m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, new Guid("9ac25c3c-af53-4aa7-a1fb-13bb899f5dba"), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 10 },
                    { new Guid("13e48948-cd21-4d50-93e9-34411713f572"), 60m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, 20, new Guid("644547ec-330b-4b70-8987-94622074b514"), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 10 },
                    { new Guid("1edfa391-8fbd-41ab-b88b-c3867547cd87"), 300m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, new Guid("9ceedd14-edaa-4c59-b25d-6187a07fac7d"), null, null, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 30 },
                    { new Guid("6e26fdb2-2ed8-438f-94c1-4e08876898f6"), 100m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, new Guid("2b3c4a88-39d7-49a2-8dfb-f4be156c06ed"), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5 },
                    { new Guid("725207a8-8087-4aff-8057-550cf88158cc"), 120m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, 20, new Guid("7cf3b7b1-c9df-49ea-b7d4-f1063900fb48"), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 10 },
                    { new Guid("a3cc890f-ca0f-4c9b-bea3-b95180f40338"), 240m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, 20, new Guid("fd21006f-ec6b-41fb-b383-0a11f6623707"), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 10 },
                    { new Guid("a7c27fe2-c830-4974-b4ac-60726b9d0eba"), 90m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, 20, new Guid("1435c3c5-6aaa-4b81-a269-f2b6f0e831b2"), null, null, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 10 },
                    { new Guid("a7cff67e-89dd-4300-8b31-a475c12f6d43"), 150m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, new Guid("38e971b6-6592-4efe-8919-8b19a2c365d7"), null, null, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 15 },
                    { new Guid("aa0bc024-4a19-4b39-9140-b2a2446d59f3"), 200m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, new Guid("f472ae22-e9ba-4091-9635-e63b4eebcec1"), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 20 },
                    { new Guid("d8bdbb3a-5123-4385-a81e-789d3a0e3488"), 120m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, 20, new Guid("6df7f8fd-08cc-4188-8b65-a5ecbfadac49"), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 10 },
                    { new Guid("fca5b166-de29-4c55-bbea-56143df3119f"), 300m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, new Guid("55f4067d-f88e-4f51-a33e-d6e53597d275"), null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 30 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "club_membership_contribution",
                keyColumn: "id",
                keyValue: new Guid("00b78bd4-196e-433a-ae10-e99003350e83"));

            migrationBuilder.DeleteData(
                table: "club_membership_contribution",
                keyColumn: "id",
                keyValue: new Guid("0381a852-c83b-4b7e-a804-4be46851f696"));

            migrationBuilder.DeleteData(
                table: "club_membership_contribution",
                keyColumn: "id",
                keyValue: new Guid("0a8c5f09-419d-44c6-a717-92a7534eb192"));

            migrationBuilder.DeleteData(
                table: "club_membership_contribution",
                keyColumn: "id",
                keyValue: new Guid("13e48948-cd21-4d50-93e9-34411713f572"));

            migrationBuilder.DeleteData(
                table: "club_membership_contribution",
                keyColumn: "id",
                keyValue: new Guid("1edfa391-8fbd-41ab-b88b-c3867547cd87"));

            migrationBuilder.DeleteData(
                table: "club_membership_contribution",
                keyColumn: "id",
                keyValue: new Guid("6e26fdb2-2ed8-438f-94c1-4e08876898f6"));

            migrationBuilder.DeleteData(
                table: "club_membership_contribution",
                keyColumn: "id",
                keyValue: new Guid("725207a8-8087-4aff-8057-550cf88158cc"));

            migrationBuilder.DeleteData(
                table: "club_membership_contribution",
                keyColumn: "id",
                keyValue: new Guid("a3cc890f-ca0f-4c9b-bea3-b95180f40338"));

            migrationBuilder.DeleteData(
                table: "club_membership_contribution",
                keyColumn: "id",
                keyValue: new Guid("a7c27fe2-c830-4974-b4ac-60726b9d0eba"));

            migrationBuilder.DeleteData(
                table: "club_membership_contribution",
                keyColumn: "id",
                keyValue: new Guid("a7cff67e-89dd-4300-8b31-a475c12f6d43"));

            migrationBuilder.DeleteData(
                table: "club_membership_contribution",
                keyColumn: "id",
                keyValue: new Guid("aa0bc024-4a19-4b39-9140-b2a2446d59f3"));

            migrationBuilder.DeleteData(
                table: "club_membership_contribution",
                keyColumn: "id",
                keyValue: new Guid("d8bdbb3a-5123-4385-a81e-789d3a0e3488"));

            migrationBuilder.DeleteData(
                table: "club_membership_contribution",
                keyColumn: "id",
                keyValue: new Guid("fca5b166-de29-4c55-bbea-56143df3119f"));

            migrationBuilder.DeleteData(
                table: "club_membership_sub_types",
                keyColumn: "id",
                keyValue: new Guid("1435c3c5-6aaa-4b81-a269-f2b6f0e831b2"));

            migrationBuilder.DeleteData(
                table: "club_membership_sub_types",
                keyColumn: "id",
                keyValue: new Guid("2b3c4a88-39d7-49a2-8dfb-f4be156c06ed"));

            migrationBuilder.DeleteData(
                table: "club_membership_sub_types",
                keyColumn: "id",
                keyValue: new Guid("378b5352-8d96-412c-af28-e48b296cb360"));

            migrationBuilder.DeleteData(
                table: "club_membership_sub_types",
                keyColumn: "id",
                keyValue: new Guid("38e971b6-6592-4efe-8919-8b19a2c365d7"));

            migrationBuilder.DeleteData(
                table: "club_membership_sub_types",
                keyColumn: "id",
                keyValue: new Guid("55f4067d-f88e-4f51-a33e-d6e53597d275"));

            migrationBuilder.DeleteData(
                table: "club_membership_sub_types",
                keyColumn: "id",
                keyValue: new Guid("644547ec-330b-4b70-8987-94622074b514"));

            migrationBuilder.DeleteData(
                table: "club_membership_sub_types",
                keyColumn: "id",
                keyValue: new Guid("6df7f8fd-08cc-4188-8b65-a5ecbfadac49"));

            migrationBuilder.DeleteData(
                table: "club_membership_sub_types",
                keyColumn: "id",
                keyValue: new Guid("7cf3b7b1-c9df-49ea-b7d4-f1063900fb48"));

            migrationBuilder.DeleteData(
                table: "club_membership_sub_types",
                keyColumn: "id",
                keyValue: new Guid("80f328c9-48c9-4d72-9811-e3b93afae405"));

            migrationBuilder.DeleteData(
                table: "club_membership_sub_types",
                keyColumn: "id",
                keyValue: new Guid("9ac25c3c-af53-4aa7-a1fb-13bb899f5dba"));

            migrationBuilder.DeleteData(
                table: "club_membership_sub_types",
                keyColumn: "id",
                keyValue: new Guid("9ceedd14-edaa-4c59-b25d-6187a07fac7d"));

            migrationBuilder.DeleteData(
                table: "club_membership_sub_types",
                keyColumn: "id",
                keyValue: new Guid("f472ae22-e9ba-4091-9635-e63b4eebcec1"));

            migrationBuilder.DeleteData(
                table: "club_membership_sub_types",
                keyColumn: "id",
                keyValue: new Guid("fd21006f-ec6b-41fb-b383-0a11f6623707"));

            migrationBuilder.DeleteData(
                table: "club_membership_types",
                keyColumn: "id",
                keyValue: new Guid("1bcb11b9-2c7b-4d79-a762-dc43e4718446"));

            migrationBuilder.DeleteData(
                table: "club_membership_types",
                keyColumn: "id",
                keyValue: new Guid("7465dfc2-d55d-4faa-be76-1fea524f9135"));

            migrationBuilder.DeleteData(
                table: "club_membership_types",
                keyColumn: "id",
                keyValue: new Guid("931de5ba-bcab-422f-b3d3-721f787bface"));

            migrationBuilder.DeleteData(
                table: "club_membership_types",
                keyColumn: "id",
                keyValue: new Guid("b1d82548-d19d-4d96-b684-e6d3b9748d90"));

            migrationBuilder.DeleteData(
                table: "club_membership_types",
                keyColumn: "id",
                keyValue: new Guid("ba084757-3f98-4b87-8e4a-9f2e497b95c8"));

            migrationBuilder.DropColumn(
                name: "deviating_voucher_per_concert_for_participants_in_percent",
                table: "club_membership_contribution");
        }
    }
}
