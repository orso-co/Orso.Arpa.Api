using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orso.Arpa.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCheckinCheckoutTimestamps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                ALTER TABLE appointment_participations ADD COLUMN IF NOT EXISTS checked_in_at TIMESTAMP;
                ALTER TABLE appointment_participations ADD COLUMN IF NOT EXISTS checked_out_at TIMESTAMP;
            """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "checked_in_at",
                table: "appointment_participations");

            migrationBuilder.DropColumn(
                name: "checked_out_at",
                table: "appointment_participations");
        }
    }
}
