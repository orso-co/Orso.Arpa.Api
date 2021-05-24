using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Orso.Arpa.Persistence.Migrations
{
    public partial class UpdatePropertiesOfMusicianProfile2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "experience_level",
                table: "musician_profiles");

            migrationBuilder.RenameColumn(
                name: "background",
                table: "musician_profiles",
                newName: "background_staff");

            migrationBuilder.AlterColumn<string>(
                name: "salary_comment",
                table: "musician_profiles",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "background_performer",
                table: "musician_profiles",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "instrument_part_id",
                table: "musician_profiles",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_deactivated",
                table: "musician_profiles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "comment",
                table: "musician_profile_sections",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "instrument_availability_id",
                table: "musician_profile_sections",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<byte>(
                name: "level_assessment_performer",
                table: "musician_profile_sections",
                type: "smallint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "level_assessment_staff",
                table: "musician_profile_sections",
                type: "smallint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.CreateTable(
                name: "instrument_part",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    section_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_instrument_part", x => x.id);
                    table.ForeignKey(
                        name: "fk_instrument_part_sections_section_id",
                        column: x => x.section_id,
                        principalTable: "sections",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "preferred_part",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    part_id = table.Column<Guid>(type: "uuid", nullable: true),
                    musician_profile_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_preferred_part", x => x.id);
                    table.ForeignKey(
                        name: "fk_preferred_part_instrument_part_part_id",
                        column: x => x.part_id,
                        principalTable: "instrument_part",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_preferred_part_musician_profiles_musician_profile_id",
                        column: x => x.musician_profile_id,
                        principalTable: "musician_profiles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_musician_profiles_instrument_part_id",
                table: "musician_profiles",
                column: "instrument_part_id");

            migrationBuilder.CreateIndex(
                name: "ix_instrument_part_section_id",
                table: "instrument_part",
                column: "section_id");

            migrationBuilder.CreateIndex(
                name: "ix_preferred_part_musician_profile_id",
                table: "preferred_part",
                column: "musician_profile_id");

            migrationBuilder.CreateIndex(
                name: "ix_preferred_part_part_id",
                table: "preferred_part",
                column: "part_id");

            migrationBuilder.AddForeignKey(
                name: "fk_musician_profiles_instrument_part_instrument_part_id",
                table: "musician_profiles",
                column: "instrument_part_id",
                principalTable: "instrument_part",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_musician_profiles_instrument_part_instrument_part_id",
                table: "musician_profiles");

            migrationBuilder.DropTable(
                name: "preferred_part");

            migrationBuilder.DropTable(
                name: "instrument_part");

            migrationBuilder.DropIndex(
                name: "ix_musician_profiles_instrument_part_id",
                table: "musician_profiles");

            migrationBuilder.DropColumn(
                name: "background_performer",
                table: "musician_profiles");

            migrationBuilder.DropColumn(
                name: "instrument_part_id",
                table: "musician_profiles");

            migrationBuilder.DropColumn(
                name: "is_deactivated",
                table: "musician_profiles");

            migrationBuilder.DropColumn(
                name: "comment",
                table: "musician_profile_sections");

            migrationBuilder.DropColumn(
                name: "instrument_availability_id",
                table: "musician_profile_sections");

            migrationBuilder.DropColumn(
                name: "level_assessment_performer",
                table: "musician_profile_sections");

            migrationBuilder.DropColumn(
                name: "level_assessment_staff",
                table: "musician_profile_sections");

            migrationBuilder.RenameColumn(
                name: "background_staff",
                table: "musician_profiles",
                newName: "background");

            migrationBuilder.AlterColumn<string>(
                name: "salary_comment",
                table: "musician_profiles",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "experience_level",
                table: "musician_profiles",
                type: "smallint",
                nullable: false,
                defaultValue: (byte)0);
        }
    }
}
