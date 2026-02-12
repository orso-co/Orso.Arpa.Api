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
            // Drop all views that depend on musician view, then musician itself
            // PostgreSQL does not allow altering column types when a view depends on them
            migrationBuilder.Sql("DROP VIEW IF EXISTS active_user_appointments_for_projects CASCADE;");
            migrationBuilder.Sql("DROP VIEW IF EXISTS active_user_appointments_for_sections CASCADE;");
            migrationBuilder.Sql("DROP VIEW IF EXISTS user_appointments_for_projects CASCADE;");
            migrationBuilder.Sql("DROP VIEW IF EXISTS user_appointments_for_sections CASCADE;");
            migrationBuilder.Sql("DROP VIEW IF EXISTS musician CASCADE;");

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

            // Recreate all views in dependency order: musician first, then dependent views

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

            migrationBuilder.Sql(@"
CREATE VIEW user_appointments_for_sections AS
SELECT m.given_name,
    m.surname,
    m.person_id,
    m.mp_id,
    a.id,
    a.name,
    a.public_details,
    a.internal_details,
    a.start_time,
    a.end_time
   FROM appointments a,
    section_appointments sa,
    sections s,
    musician m
  WHERE a.id = sa.appointment_id AND sa.section_id = s.id AND s.id = m.instrument_id AND a.deleted = false AND sa.deleted = false AND s.deleted = false AND (a.status::text <> 'Refused'::text OR a.status IS NULL)
UNION
 SELECT m.given_name,
    m.surname,
    m.person_id,
    m.mp_id,
    a.id,
    a.name,
    a.public_details,
    a.internal_details,
    a.start_time,
    a.end_time
   FROM appointments a,
    section_appointments sa,
    sections s,
    musician_profile_sections mps,
    musician m
  WHERE a.id = sa.appointment_id AND sa.section_id = s.id AND s.id = mps.section_id AND mps.musician_profile_id = m.mp_id AND a.deleted = false AND sa.deleted = false AND s.deleted = false AND mps.deleted = false AND (a.status::text <> 'Refused'::text OR a.status IS NULL)
UNION
 SELECT m.given_name,
    m.surname,
    m.person_id,
    m.mp_id,
    a.id,
    a.name,
    a.public_details,
    a.internal_details,
    a.start_time,
    a.end_time
   FROM appointments a,
    section_appointments sa,
    sections s,
    musician m
  WHERE a.id = sa.appointment_id AND sa.section_id = s.id AND a.deleted = false AND sa.deleted = false AND s.deleted = false AND (s.id IN ( SELECT fn_list_parent_sections.sec_id
           FROM fn_list_parent_sections(m.instrument_id) fn_list_parent_sections(sec_name, sec_id, sec_parent_id))) AND (a.status::text <> 'Refused'::text OR a.status IS NULL)
UNION
 SELECT m.given_name,
    m.surname,
    m.person_id,
    m.mp_id,
    a.id,
    a.name,
    a.public_details,
    a.internal_details,
    a.start_time,
    a.end_time
   FROM appointments a,
    section_appointments sa,
    sections s,
    musician_profile_sections mps,
    musician m
  WHERE a.id = sa.appointment_id AND sa.section_id = s.id AND s.id = mps.section_id AND mps.musician_profile_id = m.mp_id AND a.deleted = false AND sa.deleted = false AND s.deleted = false AND mps.deleted = false AND (mps.section_id IN ( SELECT fn_list_parent_sections.sec_id
           FROM fn_list_parent_sections(m.instrument_id) fn_list_parent_sections(sec_name, sec_id, sec_parent_id))) AND (a.status::text <> 'Refused'::text OR a.status IS NULL);
");

            migrationBuilder.Sql(@"
CREATE VIEW user_appointments_for_projects AS
SELECT m.given_name,
    m.surname,
    m.person_id,
    m.mp_id,
    a.id,
    a.name,
    a.public_details,
    a.internal_details,
    a.start_time,
    a.end_time
   FROM appointments a,
    project_appointments pa,
    projects p,
    project_participations pp,
    musician m
  WHERE a.id = pa.appointment_id AND pa.project_id = p.id AND p.id = pp.project_id AND pp.musician_profile_id = m.mp_id AND a.deleted = false AND pa.deleted = false AND p.deleted = false AND pp.deleted = false AND p.is_hidden_for_performers = false AND (a.status::text <> 'Refused'::text OR a.status IS NULL) AND (pp.participation_status_inner::text <> 'Refusal'::text OR pp.participation_status_inner IS NULL) AND (pp.participation_status_internal::text <> 'Refusal'::text OR pp.participation_status_internal IS NULL) AND (p.status::text <> 'Cancelled'::text OR p.status IS NULL);
");

            migrationBuilder.Sql(@"
CREATE VIEW active_user_appointments_for_sections AS
SELECT m.given_name,
    m.surname,
    m.person_id,
    m.mp_id,
    md.deactivation_start,
    a.id,
    a.name,
    a.public_details,
    a.internal_details,
    a.start_time,
    a.end_time
   FROM appointments a,
    section_appointments sa,
    sections s,
    musician m
     LEFT JOIN musician_profile_deactivations md ON m.mp_id = md.musician_profile_id
  WHERE a.id = sa.appointment_id AND sa.section_id = s.id AND s.id = m.instrument_id AND (a.start_time < md.deactivation_start OR md.deleted = true OR md.deactivation_start IS NULL) AND a.deleted = false AND sa.deleted = false AND s.deleted = false AND (a.status::text <> 'Refused'::text OR a.status IS NULL)
UNION
 SELECT m.given_name,
    m.surname,
    m.person_id,
    m.mp_id,
    md.deactivation_start,
    a.id,
    a.name,
    a.public_details,
    a.internal_details,
    a.start_time,
    a.end_time
   FROM appointments a,
    section_appointments sa,
    sections s,
    musician_profile_sections mps,
    musician m
     LEFT JOIN musician_profile_deactivations md ON m.mp_id = md.musician_profile_id
  WHERE a.id = sa.appointment_id AND sa.section_id = s.id AND s.id = mps.section_id AND mps.musician_profile_id = m.mp_id AND a.deleted = false AND sa.deleted = false AND s.deleted = false AND mps.deleted = false AND (a.status::text <> 'Refused'::text OR a.status IS NULL)
UNION
 SELECT m.given_name,
    m.surname,
    m.person_id,
    m.mp_id,
    md.deactivation_start,
    a.id,
    a.name,
    a.public_details,
    a.internal_details,
    a.start_time,
    a.end_time
   FROM appointments a,
    section_appointments sa,
    sections s,
    musician m
     LEFT JOIN musician_profile_deactivations md ON m.mp_id = md.musician_profile_id
  WHERE a.id = sa.appointment_id AND sa.section_id = s.id AND (s.id IN ( SELECT fn_list_parent_sections.sec_id
           FROM fn_list_parent_sections(m.instrument_id) fn_list_parent_sections(sec_name, sec_id, sec_parent_id))) AND (a.start_time < md.deactivation_start OR md.deleted OR md.deactivation_start IS NULL) AND a.deleted = false AND sa.deleted = false AND s.deleted = false AND (a.status::text <> 'Refused'::text OR a.status IS NULL)
UNION
 SELECT m.given_name,
    m.surname,
    m.person_id,
    m.mp_id,
    md.deactivation_start,
    a.id,
    a.name,
    a.public_details,
    a.internal_details,
    a.start_time,
    a.end_time
   FROM appointments a,
    section_appointments sa,
    sections s,
    musician_profile_sections mps,
    musician m
     LEFT JOIN musician_profile_deactivations md ON m.mp_id = md.musician_profile_id
  WHERE a.id = sa.appointment_id AND sa.section_id = s.id AND s.id = mps.section_id AND mps.musician_profile_id = m.mp_id AND (mps.section_id IN ( SELECT fn_list_parent_sections.sec_id
           FROM fn_list_parent_sections(m.instrument_id) fn_list_parent_sections(sec_name, sec_id, sec_parent_id))) AND (a.start_time < md.deactivation_start OR md.deleted OR md.deactivation_start IS NULL) AND a.deleted = false AND sa.deleted = false AND s.deleted = false AND mps.deleted = false AND (a.status::text <> 'Refused'::text OR a.status IS NULL);
");

            migrationBuilder.Sql(@"
CREATE VIEW active_user_appointments_for_projects AS
SELECT m.given_name,
    m.surname,
    m.person_id,
    m.mp_id,
    md.deactivation_start,
    a.id,
    a.name,
    a.public_details,
    a.internal_details,
    a.start_time,
    a.end_time
   FROM appointments a,
    project_appointments pa,
    projects p,
    project_participations pp,
    musician m
     LEFT JOIN musician_profile_deactivations md ON m.mp_id = md.musician_profile_id
  WHERE a.id = pa.appointment_id AND pa.project_id = p.id AND p.id = pp.project_id AND pp.musician_profile_id = m.mp_id AND (a.start_time < md.deactivation_start OR md.deleted = true OR md.deactivation_start IS NULL) AND a.deleted = false AND pa.deleted = false AND p.deleted = false AND pp.deleted = false AND p.is_hidden_for_performers = false AND (pp.participation_status_inner::text <> 'Refusal'::text OR pp.participation_status_inner IS NULL) AND (pp.participation_status_internal::text <> 'Refusal'::text OR pp.participation_status_internal IS NULL) AND (p.status::text <> 'Cancelled'::text OR p.status IS NULL) AND (a.status::text <> 'Refused'::text OR a.status IS NULL);
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
