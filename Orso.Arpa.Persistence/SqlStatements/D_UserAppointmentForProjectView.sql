create or replace View user_appointments_for_projects as
-- Termine, bei denen Projekte angegeben sind, für die der User mit mindestens einem Musikerprofil angemeldet ist
select m.given_name, m.surname, m.person_id, m.mp_id, a.id, a.name, a.public_details, a.internal_details, a.start_time, a.end_time
  from appointments a, project_appointments pa, projects p, project_participations pp, musician m
 where a.id=pa.appointment_id and pa.project_id=p.id and p.id=pp.project_id and pp.musician_profile_id=m.mp_id 
   and a.deleted=false and pa.deleted=false and p.deleted=false and pp.deleted=false and p.is_hidden_for_performers = false
   and (a.status <> 'Refused' OR a.status is null)
   AND (pp.participation_status_inner <> 'Refusal' OR pp.participation_status_inner is null)
   AND (pp.participation_status_internal <> 'Refusal' OR pp.participation_status_internal is null)
   AND (p.status <> 'Cancelled' OR p.status is null)
; 
