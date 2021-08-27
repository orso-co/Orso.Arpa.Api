using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Orso.Arpa.Persistence.Migrations
{
    public partial class AddBankAccountTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "bank_account",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    iban = table.Column<string>(type: "character varying(34)", maxLength: 34, nullable: true),
                    bic = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: true),
                    status_id = table.Column<Guid>(type: "uuid", nullable: true),
                    comment_inner = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    person_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_bank_account", x => x.id);
                    table.ForeignKey(
                        name: "fk_bank_account_persons_person_id",
                        column: x => x.person_id,
                        principalTable: "persons",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_bank_account_select_value_mappings_status_id",
                        column: x => x.status_id,
                        principalTable: "select_value_mappings",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_bank_account_person_id",
                table: "bank_account",
                column: "person_id");

            migrationBuilder.CreateIndex(
                name: "ix_bank_account_status_id",
                table: "bank_account",
                column: "status_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bank_account");
        }
    }
}
