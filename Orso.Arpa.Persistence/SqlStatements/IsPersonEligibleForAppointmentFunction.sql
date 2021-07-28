-- Person ist für Termin teilnahmeberechtigt
create or replace function fn_is_person_eligible_for_appointment ( p_person_id uuid, p_appointment_id uuid )
returns bool as 
$BODY$
BEGIN
	-- OK, wenn Appointment ohne Sections und ohne Projects
	/* automatisch mit nächster Abfrage abgedeckt
	If exists 
	  (select a.id from appointments a 
	    where a.deleted=false -- and p_appointment_id=a.id
	      and p_appointment_id NOT IN (select appointment_id from section_appointments)  
	      and p_appointment_id NOT IN (select appointment_id from Project_appointments) )
    then return true;
    end if;     -- */
​
	-- OK, wenn ein MuPro der Person Section und Project zugeordnet ist und Terminstart > als das DeactivationStart-Datum.
    -- Diese Logik ist in den hier aufgerufenen Functions (und darin genutzten Views) bereits enthalten.
    return 
	  ( select 
          (select fn_is_active_person_in_section(p_appointment_id,p_person_id) ) 
          and
          (select fn_is_active_person_in_project(p_appointment_id,p_person_id) )
	  );
​
end;
$BODY$
  language plpgsql
;
