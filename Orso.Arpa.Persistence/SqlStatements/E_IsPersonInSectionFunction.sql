create or replace function fn_is_person_in_section ( p_appointment_id uuid, p_person_id uuid )
returns bool as 
$BODY$
BEGIN
  If not exists ( --appointment.SectionAppointments.Count == 0
     select 1 from section_appointments where appointment_id=p_appointment_id and deleted=false )
  then return true;
  end if; 
  perform id from user_appointments_for_sections where id=p_appointment_id and person_id=p_person_id;
  return found;
end;
$BODY$
  language plpgsql
;
