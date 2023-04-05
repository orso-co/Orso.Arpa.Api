using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orso.Arpa.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class IsInstrumentConductors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("4e7a61c5-d2e4-4e3b-b21d-34a90cf958b2"),
                column: "is_instrument",
                value: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("4e7a61c5-d2e4-4e3b-b21d-34a90cf958b2"),
                column: "is_instrument",
                value: false);
        }
    }
}
