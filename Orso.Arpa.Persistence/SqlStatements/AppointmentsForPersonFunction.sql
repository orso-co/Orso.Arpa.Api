CREATE OR REPLACE FUNCTION fn_appointments_for_person ( pPersonid uuid )
	returns table (
		given_name varchar(50),
		surname varchar(50),
		person_id uuid,
		id uuid,
		name varchar(50),
		public_details varchar(50),
		internal_details varchar(50),
		start_time timestamp,
		end_time timestamp
	)
	language plpgsql
as $$
BEGIN
	return query
	SELECT DISTINCT * FROM appointments_for_user a WHERE a.person_id = pPersonid
    UNION ALL
	SELECT null as given_name, null as surname, null as person_id, a.id, a.name, a.public_details, a.internal_details, a.start_time, a.end_time
      FROM appointments a
	 WHERE a.id NOT IN (SELECT appointment_id FROM section_appointments)
	   AND a.id NOT IN (SELECT appointment_id FROM project_appointments)
    ;
end;$$

-- https://docs.microsoft.com/en-us/ef/core/querying/user-defined-function-mapping
