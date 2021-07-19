create or replace view active_user_appointments_for_projects as
select m.given_name, m.surname, m.person_id, m.mp_id, md.deactivation_start, a.id, a.name, a.public_details, a.internal_details, a.start_time, a.end_time
  from appointments a, project_appointments pa, projects p, project_participations pp, 
       musician m left outer join musician_profile_deactivations md on m.mp_id=md.musician_profile_id
 where a.id=pa.appointment_id and pa.project_id=p.id and p.id=pp.project_id and pp.musician_profile_id=m.mp_id 
   and (a.start_time < md.deactivation_start or md.deactivation_start is null)
   and a.deleted=false and pa.deleted=false and p.deleted=false and pp.deleted=false 
;
