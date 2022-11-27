CREATE OR REPLACE VIEW public.active_user_appointments_for_sections
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
    section_appointments sa,
    sections s,
    musician m
     LEFT JOIN musician_profile_deactivations md ON m.mp_id = md.musician_profile_id
  WHERE a.id = sa.appointment_id
  AND sa.section_id = s.id
  AND s.id = m.instrument_id
  AND (a.start_time < md.deactivation_start OR md.deleted = true OR md.deactivation_start IS NULL)
  AND a.deleted = false
  AND sa.deleted = false
  AND s.deleted = false
  AND (a.status <> 'Refused' OR a.status is null) 
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
  WHERE a.id = sa.appointment_id
  AND sa.section_id = s.id
  AND s.id = mps.section_id
  AND mps.musician_profile_id = m.mp_id
  AND a.deleted = false
  AND sa.deleted = false
  AND s.deleted = false
  AND mps.deleted = false
  AND (a.status <> 'Refused' OR a.status is null) 
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
  WHERE a.id = sa.appointment_id
  AND sa.section_id = s.id
  AND (s.id IN ( SELECT fn_list_parent_sections.sec_id
           FROM fn_list_parent_sections(m.instrument_id) fn_list_parent_sections(sec_name, sec_id, sec_parent_id)))
  AND (a.start_time < md.deactivation_start OR md.deleted OR md.deactivation_start IS NULL)
  AND a.deleted = false
  AND sa.deleted = false
  AND s.deleted = false
  AND (a.status <> 'Refused' OR a.status is null)
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
  WHERE a.id = sa.appointment_id
  AND sa.section_id = s.id
  AND s.id = mps.section_id
  AND mps.musician_profile_id = m.mp_id
  AND (mps.section_id IN ( SELECT fn_list_parent_sections.sec_id
           FROM fn_list_parent_sections(m.instrument_id) fn_list_parent_sections(sec_name, sec_id, sec_parent_id)))
  AND (a.start_time < md.deactivation_start OR md.deleted OR md.deactivation_start IS NULL)
  AND a.deleted = false
  AND sa.deleted = false
  AND s.deleted = false
  AND mps.deleted = false
  AND (a.status <> 'Refused' OR a.status is null);


