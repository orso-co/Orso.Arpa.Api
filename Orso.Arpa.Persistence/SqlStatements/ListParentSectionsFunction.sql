create or replace function fn_list_parent_sections ( p_sec_id uuid
) 
	returns table (
		sec_name varchar(50),
		sec_id uuid,
		sec_parent_id uuid
	) 
	language plpgsql
as $$
begin
	return query 

	WITH RECURSIVE tbl_parent AS
	(   SELECT s.name, s.id, s.parent_id  
          FROM sections s WHERE Id = p_sec_id 
        UNION ALL
    	SELECT p.name, p.id, p.parent_id
          FROM sections p JOIN tbl_parent ON p.Id = tbl_parent.parent_id 
	)
	SELECT * FROM  tbl_parent where id <> p_sec_id ; 
end;$$

;
