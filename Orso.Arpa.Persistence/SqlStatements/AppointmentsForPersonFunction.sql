create or replace function fn_appointments_for_person ( p_person_id uuid )
	returns table (
		id uuid
	) 
	language plpgsql
as $$
begin
	return query 

    select a.id from appointments a 
	 where a.deleted=false and
      	   (select fn_is_person_in_section(a.id,p_person_id) ) 
      	   and
      	   (select fn_is_person_in_project(a.id,p_person_id) )
    union 
	-- Appointments ohne Sections und ohne Projects
	select a.id from appointments a 
	 where a.deleted=false 
	   and a.id NOT IN (select appointment_id from section_appointments)  
	   and a.id NOT IN (select appointment_id from Project_appointments)
    ;
end;$$
;
