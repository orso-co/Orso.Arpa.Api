using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Orso.Arpa.Persistence.Migrations
{
    public partial class MusicianProfileDocumentAdaptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "available_documents");

            migrationBuilder.CreateTable(
                name: "musician_profile_documents",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    select_value_mapping_id = table.Column<Guid>(type: "uuid", nullable: false),
                    musician_profile_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_musician_profile_documents", x => x.id);
                    table.ForeignKey(
                        name: "fk_musician_profile_documents_musician_profiles_musician_profi",
                        column: x => x.musician_profile_id,
                        principalTable: "musician_profiles",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_musician_profile_documents_select_value_mappings_select_val",
                        column: x => x.select_value_mapping_id,
                        principalTable: "select_value_mappings",
                        principalColumn: "id");
                });

            migrationBuilder.UpdateData(
                table: "select_value_categories",
                keyColumn: "id",
                keyValue: new Guid("c4ff62bb-9f40-4499-b237-d7b87b2b36f7"),
                column: "property",
                value: "Documents");

            migrationBuilder.CreateIndex(
                name: "ix_musician_profile_documents_musician_profile_id",
                table: "musician_profile_documents",
                column: "musician_profile_id");

            migrationBuilder.CreateIndex(
                name: "ix_musician_profile_documents_select_value_mapping_id",
                table: "musician_profile_documents",
                column: "select_value_mapping_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "musician_profile_documents");

            migrationBuilder.CreateTable(
                name: "available_documents",
                columns: table => new
                {
                    musician_profile_id = table.Column<Guid>(type: "uuid", nullable: false),
                    select_value_mapping_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false),
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    modified_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_available_documents", x => new { x.musician_profile_id, x.select_value_mapping_id });
                    table.ForeignKey(
                        name: "fk_available_documents_musician_profiles_musician_profile_id",
                        column: x => x.musician_profile_id,
                        principalTable: "musician_profiles",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_available_documents_select_value_mappings_select_value_mapp",
                        column: x => x.select_value_mapping_id,
                        principalTable: "select_value_mappings",
                        principalColumn: "id");
                });

            migrationBuilder.UpdateData(
                table: "select_value_categories",
                keyColumn: "id",
                keyValue: new Guid("c4ff62bb-9f40-4499-b237-d7b87b2b36f7"),
                column: "property",
                value: "AvailableDocuments");

            migrationBuilder.CreateIndex(
                name: "ix_available_documents_select_value_mapping_id",
                table: "available_documents",
                column: "select_value_mapping_id");
        }
    }
}
