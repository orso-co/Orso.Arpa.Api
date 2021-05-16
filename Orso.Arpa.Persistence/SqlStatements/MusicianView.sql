create or replace view musician as (
select p.given_name, p.surname, mp.person_id, mp.id as mp_id, -- mp.IsProfessional, 
	  s.name as section_name, mp.instrument_id --, s.Id  
  from persons p, musician_profiles mp, sections s where p.id = mp.person_id and mp.instrument_id = s.id
);
