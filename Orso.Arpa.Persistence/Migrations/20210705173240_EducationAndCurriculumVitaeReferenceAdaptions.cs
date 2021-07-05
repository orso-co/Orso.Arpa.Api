using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Orso.Arpa.Persistence.Migrations
{
    public partial class EducationAndCurriculumVitaeReferenceAdaptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_curriculum_vitae_reference_select_value_mappings_type_id",
                table: "curriculum_vitae_reference");

            migrationBuilder.DropTable(
                name: "musician_profile_curriculum_vitae_references");

            migrationBuilder.DropTable(
                name: "musician_profile_educations");

            migrationBuilder.DropPrimaryKey(
                name: "pk_curriculum_vitae_reference",
                table: "curriculum_vitae_reference");

            migrationBuilder.RenameTable(
                name: "curriculum_vitae_reference",
                newName: "curriculum_vitae_references");

            migrationBuilder.RenameColumn(
                name: "timespan",
                table: "educations",
                newName: "time_span");

            migrationBuilder.RenameColumn(
                name: "timespan",
                table: "curriculum_vitae_references",
                newName: "time_span");

            migrationBuilder.RenameIndex(
                name: "ix_curriculum_vitae_reference_type_id",
                table: "curriculum_vitae_references",
                newName: "ix_curriculum_vitae_references_type_id");

            migrationBuilder.AlterColumn<Guid>(
                name: "type_id",
                table: "educations",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<byte>(
                name: "sort_order",
                table: "educations",
                type: "smallint",
                nullable: true,
                oldClrType: typeof(byte),
                oldType: "smallint");

            migrationBuilder.AddColumn<Guid>(
                name: "musician_profile_id",
                table: "educations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "type_id",
                table: "curriculum_vitae_references",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<byte>(
                name: "sort_order",
                table: "curriculum_vitae_references",
                type: "smallint",
                nullable: true,
                oldClrType: typeof(byte),
                oldType: "smallint");

            migrationBuilder.AddColumn<Guid>(
                name: "musician_profile_id",
                table: "curriculum_vitae_references",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "pk_curriculum_vitae_references",
                table: "curriculum_vitae_references",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "ix_educations_musician_profile_id",
                table: "educations",
                column: "musician_profile_id");

            migrationBuilder.CreateIndex(
                name: "ix_curriculum_vitae_references_musician_profile_id",
                table: "curriculum_vitae_references",
                column: "musician_profile_id");

            migrationBuilder.AddForeignKey(
                name: "fk_curriculum_vitae_references_musician_profiles_musician_prof",
                table: "curriculum_vitae_references",
                column: "musician_profile_id",
                principalTable: "musician_profiles",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_curriculum_vitae_references_select_value_mappings_type_id",
                table: "curriculum_vitae_references",
                column: "type_id",
                principalTable: "select_value_mappings",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_educations_musician_profiles_musician_profile_id",
                table: "educations",
                column: "musician_profile_id",
                principalTable: "musician_profiles",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_curriculum_vitae_references_musician_profiles_musician_prof",
                table: "curriculum_vitae_references");

            migrationBuilder.DropForeignKey(
                name: "fk_curriculum_vitae_references_select_value_mappings_type_id",
                table: "curriculum_vitae_references");

            migrationBuilder.DropForeignKey(
                name: "fk_educations_musician_profiles_musician_profile_id",
                table: "educations");

            migrationBuilder.DropIndex(
                name: "ix_educations_musician_profile_id",
                table: "educations");

            migrationBuilder.DropPrimaryKey(
                name: "pk_curriculum_vitae_references",
                table: "curriculum_vitae_references");

            migrationBuilder.DropIndex(
                name: "ix_curriculum_vitae_references_musician_profile_id",
                table: "curriculum_vitae_references");

            migrationBuilder.DropColumn(
                name: "musician_profile_id",
                table: "educations");

            migrationBuilder.DropColumn(
                name: "musician_profile_id",
                table: "curriculum_vitae_references");

            migrationBuilder.RenameTable(
                name: "curriculum_vitae_references",
                newName: "curriculum_vitae_reference");

            migrationBuilder.RenameColumn(
                name: "time_span",
                table: "educations",
                newName: "timespan");

            migrationBuilder.RenameColumn(
                name: "time_span",
                table: "curriculum_vitae_reference",
                newName: "timespan");

            migrationBuilder.RenameIndex(
                name: "ix_curriculum_vitae_references_type_id",
                table: "curriculum_vitae_reference",
                newName: "ix_curriculum_vitae_reference_type_id");

            migrationBuilder.AlterColumn<Guid>(
                name: "type_id",
                table: "educations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte>(
                name: "sort_order",
                table: "educations",
                type: "smallint",
                nullable: false,
                defaultValue: (byte)0,
                oldClrType: typeof(byte),
                oldType: "smallint",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "type_id",
                table: "curriculum_vitae_reference",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte>(
                name: "sort_order",
                table: "curriculum_vitae_reference",
                type: "smallint",
                nullable: false,
                defaultValue: (byte)0,
                oldClrType: typeof(byte),
                oldType: "smallint",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "pk_curriculum_vitae_reference",
                table: "curriculum_vitae_reference",
                column: "id");

            migrationBuilder.CreateTable(
                name: "musician_profile_curriculum_vitae_references",
                columns: table => new
                {
                    musician_profile_id = table.Column<Guid>(type: "uuid", nullable: false),
                    curriculum_vitae_reference_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false),
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    modified_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_musician_profile_curriculum_vitae_references", x => new { x.musician_profile_id, x.curriculum_vitae_reference_id });
                    table.ForeignKey(
                        name: "fk_musician_profile_curriculum_vitae_references_curriculum_vit",
                        column: x => x.curriculum_vitae_reference_id,
                        principalTable: "curriculum_vitae_reference",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_musician_profile_curriculum_vitae_references_musician_profi",
                        column: x => x.musician_profile_id,
                        principalTable: "musician_profiles",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "musician_profile_educations",
                columns: table => new
                {
                    musician_profile_id = table.Column<Guid>(type: "uuid", nullable: false),
                    education_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false),
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    modified_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_musician_profile_educations", x => new { x.musician_profile_id, x.education_id });
                    table.ForeignKey(
                        name: "fk_musician_profile_educations_educations_education_id",
                        column: x => x.education_id,
                        principalTable: "educations",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_musician_profile_educations_musician_profiles_musician_prof",
                        column: x => x.musician_profile_id,
                        principalTable: "musician_profiles",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_musician_profile_curriculum_vitae_references_curriculum_vit",
                table: "musician_profile_curriculum_vitae_references",
                column: "curriculum_vitae_reference_id");

            migrationBuilder.CreateIndex(
                name: "ix_musician_profile_educations_education_id",
                table: "musician_profile_educations",
                column: "education_id");

            migrationBuilder.AddForeignKey(
                name: "fk_curriculum_vitae_reference_select_value_mappings_type_id",
                table: "curriculum_vitae_reference",
                column: "type_id",
                principalTable: "select_value_mappings",
                principalColumn: "id");
        }
    }
}
