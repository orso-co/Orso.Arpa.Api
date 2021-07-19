create or replace view user_appointments_for_sections as
-- Termine, bei denen Sections angegeben sind, zu denen der User mindestens ein Musikerprofil hat,
select m.given_name, m.surname, m.person_id, m.mp_id, a.id, a.name, a.public_details, a.internal_details, a.start_time, a.end_time
  from appointments a,section_appointments sa, sections s, musician m
 where a.id = sa.appointment_id and sa.section_id = s.id and s.id = m.instrument_id 
   and a.deleted = false and sa.deleted = false and s.deleted = false
union
-- Termine, bei denen Sections angegeben sind, zu denen der User mindestens ein Wechselinstrumente hat,
select m.given_name, m.surname, m.person_id, m.mp_id, a.id, a.name, a.public_details, a.internal_details, a.start_time, a.end_time
  from appointments a,section_appointments sa, sections s, musician_profile_sections mps, musician m
 where a.id = sa.appointment_id and sa.section_id = s.id and s.id = mps.section_id and mps.musician_profile_id=m.mp_id
   and a.deleted = false and sa.deleted = false and s.deleted = false and mps.deleted=false
union
-- oder die Eltern-Knoten der Sections sind, zu denen der User Musikerprofile hat.
select m.given_name, m.surname, m.person_id, m.mp_id, a.id, a.name, a.public_details, a.internal_details, a.start_time, a.end_time
  from appointments a, section_appointments sa, sections s, musician m
 where a.id = sa.appointment_id and sa.section_id = s.id 
   and a.deleted = false and sa.deleted = false and s.deleted = false
   and s.id in ( select sec_id from fn_list_parent_sections(m.instrument_id) )
union 
-- oder die Eltern-Knoten der Sections sind, zu denen der User Wechselinstrumente hat.
select m.given_name, m.surname, m.person_id, m.mp_id, a.id, a.name, a.public_details, a.internal_details, a.start_time, a.end_time
  from appointments a, section_appointments sa, sections s, musician_profile_sections mps, musician m
 where a.id = sa.appointment_id and sa.section_id = s.id and s.id = mps.section_id and mps.musician_profile_id=m.mp_id
   and a.deleted = false and sa.deleted = false and s.deleted = false and mps.deleted=false
   and mps.section_id in ( select sec_id from fn_list_parent_sections(m.instrument_id) )
 ;
