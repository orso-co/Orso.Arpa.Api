using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Orso.Arpa.Persistence.Migrations
{
    public partial class AddContactViaAndContactRecommended : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "contact_via_id",
                table: "persons",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_persons_contact_via_id",
                table: "persons",
                column: "contact_via_id");

            migrationBuilder.AddForeignKey(
                name: "fk_persons_persons_contact_via_id",
                table: "persons",
                column: "contact_via_id",
                principalTable: "persons",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_persons_persons_contact_via_id",
                table: "persons");

            migrationBuilder.DropIndex(
                name: "ix_persons_contact_via_id",
                table: "persons");

            migrationBuilder.DropColumn(
                name: "contact_via_id",
                table: "persons");
        }
    }
}
