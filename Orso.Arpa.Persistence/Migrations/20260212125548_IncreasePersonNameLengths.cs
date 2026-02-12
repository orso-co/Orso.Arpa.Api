using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orso.Arpa.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class IncreasePersonNameLengths : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop the musician view that references persons.given_name and persons.surname
            // PostgreSQL does not allow altering column types when a view depends on them
            migrationBuilder.Sql("DROP VIEW IF EXISTS musician;");

            migrationBuilder.AlterColumn<string>(
                name: "surname",
                table: "persons",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "given_name",
                table: "persons",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "birthplace",
                table: "persons",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "birth_name",
                table: "persons",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            // Recreate the musician view
            migrationBuilder.Sql(@"
                CREATE VIEW musician AS
                SELECT p.given_name,
                       p.surname,
                       mp.person_id,
                       mp.id AS mp_id,
                       s.name AS section_name,
                       mp.instrument_id
                FROM persons p,
                     musician_profiles mp,
                     sections s
                WHERE p.id = mp.person_id
                  AND mp.instrument_id = s.id
                  AND p.deleted = false
                  AND mp.deleted = false
                  AND s.deleted = false;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "surname",
                table: "persons",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "given_name",
                table: "persons",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "birthplace",
                table: "persons",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "birth_name",
                table: "persons",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200,
                oldNullable: true);
        }
    }
}
