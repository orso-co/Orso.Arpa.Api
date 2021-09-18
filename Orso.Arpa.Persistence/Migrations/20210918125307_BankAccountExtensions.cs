using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Orso.Arpa.Persistence.Migrations
{
    public partial class BankAccountExtensions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "account_owner",
                table: "bank_account",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.InsertData(
                table: "select_value_categories",
                columns: new[] { "id", "created_at", "created_by", "deleted", "modified_at", "modified_by", "name", "property", "table" },
                values: new object[] { new Guid("d75c2fe5-dba6-475e-a0f1-dd71285c0269"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Bank account status", "Status", "BankAccount" });

            migrationBuilder.InsertData(
                table: "select_values",
                columns: new[] { "id", "created_at", "created_by", "deleted", "description", "modified_at", "modified_by", "name" },
                values: new object[,]
                {
                    { new Guid("597bf9bc-4fad-433f-810d-ae4de4ac3bde"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Bank account expired" },
                    { new Guid("7efd1bdd-67b5-4706-a1f4-9d67eea05e5d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Incorrect bank details" },
                    { new Guid("c36e8662-2740-49c7-87dd-3c301ef86909"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Return debit received" },
                    { new Guid("b0f67138-7488-4c68-ad4c-63fce6f862cc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Return debit received" }
                });

            migrationBuilder.InsertData(
                table: "select_value_mappings",
                columns: new[] { "id", "created_at", "created_by", "deleted", "modified_at", "modified_by", "select_value_category_id", "select_value_id", "sort_order" },
                values: new object[,]
                {
                    { new Guid("a24f4ce6-b3c6-4d58-9e31-cb3a83ae2694"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("d75c2fe5-dba6-475e-a0f1-dd71285c0269"), new Guid("597bf9bc-4fad-433f-810d-ae4de4ac3bde"), 10 },
                    { new Guid("2f03daef-5795-45b6-9535-cf7748f84476"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("d75c2fe5-dba6-475e-a0f1-dd71285c0269"), new Guid("c36e8662-2740-49c7-87dd-3c301ef86909"), 20 },
                    { new Guid("77164303-d91d-4fa1-9c2c-ae9c05298e30"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("d75c2fe5-dba6-475e-a0f1-dd71285c0269"), new Guid("7efd1bdd-67b5-4706-a1f4-9d67eea05e5d"), 30 },
                    { new Guid("c59900fa-7dc6-4ca7-8a35-c73c7ea582b9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("d75c2fe5-dba6-475e-a0f1-dd71285c0269"), new Guid("b0f67138-7488-4c68-ad4c-63fce6f862cc"), 40 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("2f03daef-5795-45b6-9535-cf7748f84476"));

            migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("77164303-d91d-4fa1-9c2c-ae9c05298e30"));

            migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("a24f4ce6-b3c6-4d58-9e31-cb3a83ae2694"));

            migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValue: new Guid("c59900fa-7dc6-4ca7-8a35-c73c7ea582b9"));

            migrationBuilder.DeleteData(
                table: "select_value_categories",
                keyColumn: "id",
                keyValue: new Guid("d75c2fe5-dba6-475e-a0f1-dd71285c0269"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("597bf9bc-4fad-433f-810d-ae4de4ac3bde"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("7efd1bdd-67b5-4706-a1f4-9d67eea05e5d"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("b0f67138-7488-4c68-ad4c-63fce6f862cc"));

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValue: new Guid("c36e8662-2740-49c7-87dd-3c301ef86909"));

            migrationBuilder.DropColumn(
                name: "account_owner",
                table: "bank_account");
        }
    }
}
