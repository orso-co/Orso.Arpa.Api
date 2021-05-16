CREATE OR REPLACE FUNCTION fn_list_parent_sections( p_sec_id uuid )
	returns table (
		section_name varchar(50),
		section_id uuid,
		section_parent_id uuid
	)
	language plpgsql
as $$
begin
	return query
	WITH RECURSIVE tbl_parent AS
	(   SELECT s.name, s.id, s.parent_id
          FROM sections s WHERE id = p_sec_id
        UNION ALL
    	SELECT p.name, p.id, p.parent_id
          FROM sections p JOIN tbl_parent ON p.id = tbl_parent.parent_id
	)
	SELECT * FROM tbl_parent WHERE id <> p_sec_id ;
end;$$
