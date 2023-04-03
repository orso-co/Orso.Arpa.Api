using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orso.Arpa.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class IsInstrumentMusicalStaff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("18f1e750-f50d-4f06-8205-21203981bff6"),
                column: "is_instrument",
                value: true);

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("4e7a61c5-d2e4-4e3b-b21d-34a90cf958b2"),
                column: "is_instrument",
                value: true);

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("6fc908f0-da26-4237-80ca-dfe30453123c"),
                column: "is_instrument",
                value: true);

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("94c42496-fdb6-4341-b82f-735fd1706d39"),
                column: "is_instrument",
                value: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("18f1e750-f50d-4f06-8205-21203981bff6"),
                column: "is_instrument",
                value: false);

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("4e7a61c5-d2e4-4e3b-b21d-34a90cf958b2"),
                column: "is_instrument",
                value: false);

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("6fc908f0-da26-4237-80ca-dfe30453123c"),
                column: "is_instrument",
                value: false);

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: new Guid("94c42496-fdb6-4341-b82f-735fd1706d39"),
                column: "is_instrument",
                value: false);
        }
    }
}
