create or replace function fn_is_active_mupro_in_project (p_mupro_id uuid, p_appointment_id uuid)
returns bool as 
$BODY$
begin
  If not exists ( --appointment.ProjectAppointments.Count == 0
     select 1 from project_appointments where appointment_id=p_appointment_id and deleted=false )
  then return true;
  end if;
  perform mp_id from active_user_appointments_for_projects where id=p_appointment_id and mp_id=p_mupro_id; 
  return found;
end;
$BODY$
  language plpgsql
;
