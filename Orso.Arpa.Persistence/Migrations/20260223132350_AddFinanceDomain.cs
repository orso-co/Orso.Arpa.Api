using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orso.Arpa.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddFinanceDomain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "organization_bank_accounts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    iban = table.Column<string>(type: "character varying(34)", maxLength: 34, nullable: true),
                    bic = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: true),
                    bank_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    account_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    encrypted_fin_ts_credentials = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    encrypted_pay_pal_credentials = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    created_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_organization_bank_accounts", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "bank_account_balance_snapshots",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    organization_bank_account_id = table.Column<Guid>(type: "uuid", nullable: false),
                    balance = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    available_balance = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    currency = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: true, defaultValue: "EUR"),
                    balance_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    synced_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    sync_status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    error_message = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    created_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_bank_account_balance_snapshots", x => x.id);
                    table.ForeignKey(
                        name: "fk_bank_account_balance_snapshots_organization_bank_accounts_o",
                        column: x => x.organization_bank_account_id,
                        principalTable: "organization_bank_accounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pending_tan_requests",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    organization_bank_account_id = table.Column<Guid>(type: "uuid", nullable: false),
                    tan_challenge = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    tan_medium_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    expires_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    submitted_tan = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    created_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pending_tan_requests", x => x.id);
                    table.ForeignKey(
                        name: "fk_pending_tan_requests_organization_bank_accounts_organizatio",
                        column: x => x.organization_bank_account_id,
                        principalTable: "organization_bank_accounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_bank_account_balance_snapshots_balance_date",
                table: "bank_account_balance_snapshots",
                column: "balance_date");

            migrationBuilder.CreateIndex(
                name: "ix_bank_account_balance_snapshots_organization_bank_account_id",
                table: "bank_account_balance_snapshots",
                column: "organization_bank_account_id");

            migrationBuilder.CreateIndex(
                name: "ix_bank_account_balance_snapshots_synced_at",
                table: "bank_account_balance_snapshots",
                column: "synced_at");

            migrationBuilder.CreateIndex(
                name: "ix_organization_bank_accounts_account_type",
                table: "organization_bank_accounts",
                column: "account_type");

            migrationBuilder.CreateIndex(
                name: "ix_organization_bank_accounts_is_active",
                table: "organization_bank_accounts",
                column: "is_active");

            migrationBuilder.CreateIndex(
                name: "ix_pending_tan_requests_organization_bank_account_id",
                table: "pending_tan_requests",
                column: "organization_bank_account_id");

            migrationBuilder.CreateIndex(
                name: "ix_pending_tan_requests_status",
                table: "pending_tan_requests",
                column: "status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bank_account_balance_snapshots");

            migrationBuilder.DropTable(
                name: "pending_tan_requests");

            migrationBuilder.DropTable(
                name: "organization_bank_accounts");
        }
    }
}
