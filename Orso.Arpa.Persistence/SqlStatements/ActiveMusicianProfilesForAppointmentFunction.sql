create or replace function fn_active_mupro_for_appointments ( p_appointment_id uuid )
	returns table (
		id uuid
	) 
	language plpgsql
as $$
begin
	return query 

    select mp.id from musician_profiles mp 
	 where deleted=false and 
     	   (select fn_is_active_mupro_in_section(mp.id,p_appointment_id) ) 
      	   and
      	   (select fn_is_active_mupro_in_project(mp.id,p_appointment_id) )
    union 
	-- musician_profiles ohne Sections und ohne Projects
	select mp.id from musician_profiles mp 
     where deleted=false 
	   and mp.id NOT IN (select musician_profile_id from musician_profile_sections) 
	   and mp.id NOT IN (select musician_profile_id from project_participations)
    ;
end;$$
;
