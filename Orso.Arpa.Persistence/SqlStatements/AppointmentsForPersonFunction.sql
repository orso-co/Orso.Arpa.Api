CREATE OR REPLACE FUNCTION fn_appointments_for_person ( pPersonid uuid )
	returns table (
		id uuid
	)
	language plpgsql
as $$
BEGIN
	return query
	SELECT DISTINCT a.id FROM appointments_for_user a WHERE a.person_id = pPersonid
    UNION ALL
	SELECT a.id
      FROM appointments a
	 WHERE a.id NOT IN (SELECT appointment_id FROM section_appointments)
	   AND a.id NOT IN (SELECT appointment_id FROM project_appointments)
    ;
end;$$

-- https://docs.microsoft.com/en-us/ef/core/querying/user-defined-function-mapping
