create or replace View user_appointments_for_projects as
-- Termine, bei denen Projekte angegeben sind, für die der User mit mindestens einem Musikerprofil angemeldet ist
select m.given_name, m.surname, m.person_id, a.Id, a.name, a.public_details, a.internal_details, a.start_time, a.end_time
  from appointments a, project_appointments pa, projects p, project_participations pp, musician m
 where a.id = pa.appointment_id and pa.project_id = p.id and p.id = pp.project_id and pp.musician_profile_id =m.mp_id and a.deleted = false
;
