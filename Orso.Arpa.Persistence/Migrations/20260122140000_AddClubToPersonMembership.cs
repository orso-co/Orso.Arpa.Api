using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orso.Arpa.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddClubToPersonMembership : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "club_id",
                table: "person_memberships",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_person_memberships_club_id",
                table: "person_memberships",
                column: "club_id");

            migrationBuilder.AddForeignKey(
                name: "fk_person_memberships_select_value_mappings_club_id",
                table: "person_memberships",
                column: "club_id",
                principalTable: "select_value_mappings",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_person_memberships_select_value_mappings_club_id",
                table: "person_memberships");

            migrationBuilder.DropIndex(
                name: "ix_person_memberships_club_id",
                table: "person_memberships");

            migrationBuilder.DropColumn(
                name: "club_id",
                table: "person_memberships");
        }
    }
}
