using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orso.Arpa.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddImportBatchIdToBankAccountAndContactDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                ALTER TABLE contact_detail ADD COLUMN IF NOT EXISTS import_batch_id uuid;
                ALTER TABLE bank_account ADD COLUMN IF NOT EXISTS import_batch_id uuid;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "import_batch_id",
                table: "contact_detail");

            migrationBuilder.DropColumn(
                name: "import_batch_id",
                table: "bank_account");
        }
    }
}
