create or replace function fn_is_active_mupro_in_section (p_mupro_id uuid, p_appointment_id uuid)
returns bool as 
$BODY$
BEGIN
  If not exists (
     select 1 from section_appointments where appointment_id=p_appointment_id and deleted=false )
  then return true;
  end if; 
  perform mp_id from active_user_appointments_for_sections where id=p_appointment_id and mp_id=p_mupro_id; 
  return found;
end;
$BODY$
  language plpgsql
;
