create or replace view active_user_appointments_for_sections as
-- Termine, bei denen Sections angegeben sind, zu denen der User mindestens ein Musikerprofil hat,
select m.given_name, m.surname, m.person_id, m.mp_id, md.deactivation_start, a.id, a.name, a.public_details, a.internal_details, a.start_time, a.end_time
  from appointments a, section_appointments sa, sections s, musician m left outer join musician_profile_deactivations md on m.mp_id=md.musician_profile_id
 where a.id = sa.appointment_id and sa.section_id = s.id and s.id = m.instrument_id 
   and (a.start_time < md.deactivation_start or md.deactivation_start is null)
   and a.deleted = false and sa.deleted = false and s.deleted = false
union
-- Termine, bei denen Sections angegeben sind, zu denen der User mindestens ein Wechselinstrumente hat,
select m.given_name, m.surname, m.person_id, m.mp_id, md.deactivation_start, a.id, a.name, a.public_details, a.internal_details, a.start_time, a.end_time
  from appointments a,section_appointments sa, sections s, musician_profile_sections mps, musician m left outer join musician_profile_deactivations md on m.mp_id=md.musician_profile_id
 where a.id = sa.appointment_id and sa.section_id = s.id and s.id = mps.section_id and mps.musician_profile_id=m.mp_id
   and a.deleted = false and sa.deleted = false and s.deleted = false and mps.deleted=false
union
-- oder die Eltern-Knoten der Sections sind, zu denen der User Musikerprofile hat.
select m.given_name, m.surname, m.person_id, m.mp_id, md.deactivation_start, a.id, a.name, a.public_details, a.internal_details, a.start_time, a.end_time
  from appointments a, section_appointments sa, sections s, musician m left outer join musician_profile_deactivations md on m.mp_id=md.musician_profile_id
 where a.id = sa.appointment_id and sa.section_id = s.id 
   and s.id in ( select sec_id from fn_list_parent_sections(m.instrument_id) )  
   and (a.start_time < md.deactivation_start or md.deactivation_start is null)
   and a.deleted = false and sa.deleted = false and s.deleted = false
union 
-- oder die Eltern-Knoten der Sections sind, zu denen der User Wechselinstrumente hat.
select m.given_name, m.surname, m.person_id, m.mp_id, md.deactivation_start, a.id, a.name, a.public_details, a.internal_details, a.start_time, a.end_time
  from appointments a, section_appointments sa, sections s, musician_profile_sections mps, musician m left outer join musician_profile_deactivations md on m.mp_id=md.musician_profile_id
 where a.id = sa.appointment_id and sa.section_id = s.id 
   and s.id = mps.section_id and mps.musician_profile_id=m.mp_id
   and mps.section_id in ( select sec_id from fn_list_parent_sections(m.instrument_id) )
   and (a.start_time < md.deactivation_start or md.deactivation_start is null)
   and a.deleted = false and sa.deleted = false and s.deleted = false and mps.deleted=false
;
