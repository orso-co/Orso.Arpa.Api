CREATE OR REPLACE VIEW public.active_user_appointments_for_projects
 AS
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
  WHERE a.id = pa.appointment_id AND pa.project_id = p.id AND p.id = pp.project_id AND pp.musician_profile_id = m.mp_id AND (a.start_time < md.deactivation_start OR md.deleted = true OR md.deactivation_start IS NULL) AND a.deleted = false AND pa.deleted = false AND p.deleted = false AND pp.deleted = false;

