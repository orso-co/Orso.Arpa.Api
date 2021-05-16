CREATE OR REPLACE VIEW appointments_for_user AS
-- Appointments ohne Sections und ohne Projects
SELECT null AS given_name, null AS surname, null AS person_id, a.id, a.name, a.public_details, a.internal_details, a.start_time, a.end_time FROM appointments a
    WHERE id NOT IN (SELECT appointment_id FROM section_appointments) AND id NOT IN (SELECT appointment_id FROM project_appointments)
UNION ALL
-- Außerdem Termine, bei denen Projekte angegeben sind, für die der User mit mindestens einem Musikerprofil angemeldet ist
SELECT m.given_name, m.surname, m.person_id, a.id, a.name, a.public_details, a.internal_details, a.start_time, a.end_time
    FROM appointments a, project_appointments pa, projects p, project_participations pp, musician m
    WHERE a.id = pa.appointment_id AND pa.project_id = p.id AND p.id = pp.project_id AND pp.musician_profile_id = m.musician_profile_id
UNION ALL
-- und Termine, bei denen Sections angegeben sind, zu denen der User mindestens ein Musikerprofil hat,
SELECT m.given_name, m.surname, m.person_id, a.id, a.name, a.public_details, a.internal_details, a.start_time, a.end_time
    FROM appointments a, section_appointments sa, sections s, musician m
    WHERE a.id = sa.appointment_id AND sa.section_id = s.id AND s.id = m.instrument_id
UNION ALL
-- oder die Eltern-Knoten der Sections sind, zu denen der User Musikerprofile hat.
SELECT m.given_name, m.surname, m.person_id, a.id, a.name, a.public_details, a.internal_details, a.start_time, a.end_time
    FROM appointments a, section_appointments sa, sections s, musician m
    WHERE a.id = sa.appointment_id AND sa.section_id = s.id
    AND s.id IN ( SELECT section_id FROM fn_list_parent_sections(m.instrument_id) );
 ;
