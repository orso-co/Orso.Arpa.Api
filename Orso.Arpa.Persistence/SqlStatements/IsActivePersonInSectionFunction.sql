-- IsActivePersonInSection
create or replace function fn_is_active_person_in_section ( p_appointment_id uuid, p_person_id uuid )
returns bool as 
$BODY$
BEGIN
  If not exists ( --appointment.SectionAppointments.Count == 0
     select 1 from section_appointments where appointment_id=p_appointment_id and deleted=false )
  then return true;
  end if; 
  perform id from active_user_appointments_for_sections where id=p_appointment_id and person_id=p_person_id;
  return found;
end;
$BODY$
  language plpgsql
;
​
-- For Performer
create or replace function fn_active_appointments_for_person ( p_person_id uuid )
	returns table (	id uuid	) 
	language plpgsql
as $$
begin
	return query 
​
    select a.id from appointments a 
	 where a.deleted=false and
      	   (select fn_is_active_person_in_section(a.id,p_person_id) ) 
      	   and
      	   (select fn_is_active_person_in_project(a.id,p_person_id) )
    union 
	-- Appointments ohne Sections und ohne Projects
	select a.id from appointments a 
	 where a.deleted=false 
	   and a.id NOT IN (select appointment_id from section_appointments)  
	   and a.id NOT IN (select appointment_id from Project_appointments)
    ;
end;$$
;
