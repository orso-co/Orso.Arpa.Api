using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Orso.Arpa.Persistence.Seed;

namespace Orso.Arpa.Persistence.Migrations
{
    public partial class RemoveDedicatedPartsInSectionSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                string.Format("UPDATE musician_profiles SET instrument_id = '{0}' WHERE instrument_id = '{1}'", SectionSeedData.Alto.Id.ToString(), SectionSeedData.Soprano.Id.ToString())
            );
            migrationBuilder.Sql("UPDATE musician_profiles SET profile_preference_performer = 1");

            migrationBuilder.DeleteData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("22d7cf92-7b29-4cf1-a6fa-2954377589b4"));

            migrationBuilder.DeleteData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("3db46ff0-9165-46cc-8f28-6a1d52dee518"));

            migrationBuilder.DeleteData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("50dfa2be-85e2-4638-aa53-22dadc97a844"));

            migrationBuilder.DeleteData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("61fa66ec-3103-43fe-800c-930547dff82c"));

            migrationBuilder.DeleteData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("8470ddf0-43ab-477e-b3bc-47ede014b359"));

            migrationBuilder.DeleteData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("afef89cf-90e1-4d4f-83ab-d2b47e97af0f"));

            migrationBuilder.DeleteData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("bfe0e1ca-95ce-4cb6-a9c9-3c23c70bab21"));

            migrationBuilder.DeleteData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("e809ee90-23f9-44de-b80e-2fddd5ee3683"));

            migrationBuilder.DeleteData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("eb5728b5-b1fd-4a70-8894-7bb152087837"));

            migrationBuilder.DeleteData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("f3ee3c42-4e4e-411d-a839-6e0420bc35a3"));

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("08bc313b-d0dd-4b78-bdbf-d976682d965e"),
                column: "instrument_part_count",
                value: (byte)2);

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("0cf93477-f42f-46c3-8e3d-45ccdc54ad8c"),
                column: "instrument_part_count",
                value: (byte)2);

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("1579d7e7-4f55-4532-a078-69fd1ec939da"),
                column: "instrument_part_count",
                value: (byte)2);

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("1d0ed0b3-b87b-439f-932e-616d7e03a0d6"),
                column: "instrument_part_count",
                value: (byte)2);

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("48833c1b-cbc1-43b2-a4c5-f1fa4289f5ab"),
                column: "instrument_part_count",
                value: (byte)2);

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("4a31447d-63c2-4e28-ab39-255a956fbe18"),
                column: "instrument_part_count",
                value: (byte)3);

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("5109e464-7b01-40bd-a5e0-398ac3d1bb83"),
                column: "instrument_part_count",
                value: (byte)2);

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("7daa1394-a70d-4a24-88a6-ccf511d75c4d"),
                column: "instrument_part_count",
                value: (byte)2);

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("8c0a80d1-5889-4794-89b6-b80a3828aa5b"),
                column: "instrument_part_count",
                value: (byte)2);

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("8d01524c-7c22-4a20-8f26-711d11addbfd"),
                column: "instrument_part_count",
                value: (byte)2);

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("8ed82e0e-0354-4192-8f26-5a2437e9208d"),
                column: "instrument_part_count",
                value: (byte)2);

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("a06431be-f9d6-44dc-8fdb-fbf8aa2bb940"),
                column: "instrument_part_count",
                value: (byte)2);

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("c42591db-4e41-413f-8b98-6607e2f12e39"),
                column: "instrument_part_count",
                value: (byte)2);

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("da998fcb-92b9-4828-976e-826e97e05cb3"),
                column: "instrument_part_count",
                value: (byte)3);

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("e7dd10ef-1c39-4440-9a6c-65d397f010ca"),
                column: "instrument_part_count",
                value: (byte)2);

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("ed0829d0-d978-430e-96ec-b93cf75f3fd6"),
                column: "instrument_part_count",
                value: (byte)2);

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("fab9a49a-9fa4-4af3-9e40-e13bdc930513"),
                columns: new[] { "instrument_part_count", "name" },
                values: new object[] { (byte)2, "Violin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("08bc313b-d0dd-4b78-bdbf-d976682d965e"),
                column: "instrument_part_count",
                value: (byte)0);

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("0cf93477-f42f-46c3-8e3d-45ccdc54ad8c"),
                column: "instrument_part_count",
                value: (byte)0);

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("1579d7e7-4f55-4532-a078-69fd1ec939da"),
                column: "instrument_part_count",
                value: (byte)0);

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("1d0ed0b3-b87b-439f-932e-616d7e03a0d6"),
                column: "instrument_part_count",
                value: (byte)0);

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("48833c1b-cbc1-43b2-a4c5-f1fa4289f5ab"),
                column: "instrument_part_count",
                value: (byte)0);

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("4a31447d-63c2-4e28-ab39-255a956fbe18"),
                column: "instrument_part_count",
                value: (byte)2);

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("5109e464-7b01-40bd-a5e0-398ac3d1bb83"),
                column: "instrument_part_count",
                value: (byte)0);

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("7daa1394-a70d-4a24-88a6-ccf511d75c4d"),
                column: "instrument_part_count",
                value: (byte)0);

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("8c0a80d1-5889-4794-89b6-b80a3828aa5b"),
                column: "instrument_part_count",
                value: (byte)0);

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("8d01524c-7c22-4a20-8f26-711d11addbfd"),
                column: "instrument_part_count",
                value: (byte)0);

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("8ed82e0e-0354-4192-8f26-5a2437e9208d"),
                column: "instrument_part_count",
                value: (byte)0);

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("a06431be-f9d6-44dc-8fdb-fbf8aa2bb940"),
                column: "instrument_part_count",
                value: (byte)0);

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("c42591db-4e41-413f-8b98-6607e2f12e39"),
                column: "instrument_part_count",
                value: (byte)0);

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("da998fcb-92b9-4828-976e-826e97e05cb3"),
                column: "instrument_part_count",
                value: (byte)2);

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("e7dd10ef-1c39-4440-9a6c-65d397f010ca"),
                column: "instrument_part_count",
                value: (byte)0);

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("ed0829d0-d978-430e-96ec-b93cf75f3fd6"),
                column: "instrument_part_count",
                value: (byte)0);

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("fab9a49a-9fa4-4af3-9e40-e13bdc930513"),
                columns: new[] { "instrument_part_count", "name" },
                values: new object[] { (byte)0, "Violins" });

            migrationBuilder.InsertData(
                table: "sections",
                columns: new[] { "id", "created_at", "created_by", "deleted", "instrument_part_count", "is_instrument", "modified_at", "modified_by", "name", "parent_id" },
                values: new object[,]
                {
                    { new Guid("f3ee3c42-4e4e-411d-a839-6e0420bc35a3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, (byte)0, false, null, null, "Violin II", new Guid("fab9a49a-9fa4-4af3-9e40-e13bdc930513") },
                    { new Guid("eb5728b5-b1fd-4a70-8894-7bb152087837"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, (byte)0, false, null, null, "Violin I", new Guid("fab9a49a-9fa4-4af3-9e40-e13bdc930513") },
                    { new Guid("61fa66ec-3103-43fe-800c-930547dff82c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, (byte)0, false, null, null, "Basso 2", new Guid("e7dd10ef-1c39-4440-9a6c-65d397f010ca") },
                    { new Guid("bfe0e1ca-95ce-4cb6-a9c9-3c23c70bab21"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, (byte)0, false, null, null, "Basso 1", new Guid("e7dd10ef-1c39-4440-9a6c-65d397f010ca") },
                    { new Guid("afef89cf-90e1-4d4f-83ab-d2b47e97af0f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, (byte)0, false, null, null, "Tenor 2", new Guid("1579d7e7-4f55-4532-a078-69fd1ec939da") },
                    { new Guid("3db46ff0-9165-46cc-8f28-6a1d52dee518"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, (byte)0, false, null, null, "Tenor 1", new Guid("1579d7e7-4f55-4532-a078-69fd1ec939da") },
                    { new Guid("50dfa2be-85e2-4638-aa53-22dadc97a844"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, (byte)0, false, null, null, "Alto 2", new Guid("a06431be-f9d6-44dc-8fdb-fbf8aa2bb940") },
                    { new Guid("e809ee90-23f9-44de-b80e-2fddd5ee3683"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, (byte)0, false, null, null, "Alto 1", new Guid("a06431be-f9d6-44dc-8fdb-fbf8aa2bb940") },
                    { new Guid("22d7cf92-7b29-4cf1-a6fa-2954377589b4"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, (byte)0, false, null, null, "Soprano 2", new Guid("7daa1394-a70d-4a24-88a6-ccf511d75c4d") },
                    { new Guid("8470ddf0-43ab-477e-b3bc-47ede014b359"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, (byte)0, false, null, null, "Soprano 1", new Guid("7daa1394-a70d-4a24-88a6-ccf511d75c4d") }
                });
        }
    }
}
