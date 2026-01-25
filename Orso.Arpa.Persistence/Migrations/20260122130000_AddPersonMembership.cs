using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orso.Arpa.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddPersonMembership : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "person_memberships",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    entry_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    exit_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    annual_fee = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    support_level_id = table.Column<Guid>(type: "uuid", nullable: true),
                    membership_status_id = table.Column<Guid>(type: "uuid", nullable: true),
                    payment_method_id = table.Column<Guid>(type: "uuid", nullable: true),
                    payment_frequency_id = table.Column<Guid>(type: "uuid", nullable: true),
                    staff_comment = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    performer_comment = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    person_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_person_memberships", x => x.id);
                    table.ForeignKey(
                        name: "fk_person_memberships_persons_person_id",
                        column: x => x.person_id,
                        principalTable: "persons",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_person_memberships_select_value_mappings_support_level_id",
                        column: x => x.support_level_id,
                        principalTable: "select_value_mappings",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_person_memberships_select_value_mappings_membership_status_id",
                        column: x => x.membership_status_id,
                        principalTable: "select_value_mappings",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_person_memberships_select_value_mappings_payment_method_id",
                        column: x => x.payment_method_id,
                        principalTable: "select_value_mappings",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_person_memberships_select_value_mappings_payment_frequency_id",
                        column: x => x.payment_frequency_id,
                        principalTable: "select_value_mappings",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_person_memberships_person_id",
                table: "person_memberships",
                column: "person_id");

            migrationBuilder.CreateIndex(
                name: "ix_person_memberships_support_level_id",
                table: "person_memberships",
                column: "support_level_id");

            migrationBuilder.CreateIndex(
                name: "ix_person_memberships_membership_status_id",
                table: "person_memberships",
                column: "membership_status_id");

            migrationBuilder.CreateIndex(
                name: "ix_person_memberships_payment_method_id",
                table: "person_memberships",
                column: "payment_method_id");

            migrationBuilder.CreateIndex(
                name: "ix_person_memberships_payment_frequency_id",
                table: "person_memberships",
                column: "payment_frequency_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "person_memberships");
        }
    }
}
