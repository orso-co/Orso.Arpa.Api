create or replace function fn_is_person_in_project ( p_appointment_id uuid, p_person_id uuid )
returns bool as 
$BODY$
begin
  If not exists
     (select 1 from project_appointments where appointment_id = p_appointment_id)
  then return true;
  end if; 
  perform id from user_appointments_for_projects where id=p_appointment_id and person_id = p_person_id;
  return found;
end;
$BODY$
  language plpgsql
;
