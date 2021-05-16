create or replace View user_appointments_for_sections as
-- Termine, bei denen Sections angegeben sind, zu denen der User mindestens ein Musikerprofil hat,
select m.given_name, m.surname, m.person_id, a.id, a.name, a.public_details, a.internal_details, a.start_time, a.end_time
  from appointments a,section_appointments sa, sections s, musician m
 where a.id = sa.appointment_id and sa.section_id = s.id and s.id = m.instrument_id and a.deleted = false
union all
-- oder die Eltern-Knoten der Sections sind, zu denen der User Musikerprofile hat.
select m.given_name, m.surname, m.person_id, a.id, a.name, a.public_details, a.internal_details, a.start_time, a.end_time
  from appointments a, section_appointments sa, sections s, musician m
 where a.id = sa.appointment_id and sa.section_id = s.id and a.deleted = false
   and s.id in ( SELECT sec_id FROM fn_list_parent_sections(m.instrument_id) )
 ;
