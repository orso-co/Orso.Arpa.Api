create or replace function fn_mupro_for_appointments ( p_appointment_id uuid )
	returns table (
		id uuid
	) 
	language plpgsql
as $$
begin
	return query 

    select mp.id from musician_profiles mp 
	 where deleted=false and 
     	   (select fn_is_mupro_in_section(mp.id,p_appointment_id) ) 
      	   and
      	   (select fn_is_mupro_in_project(mp.id,p_appointment_id) )
    ;
end;$$
;
