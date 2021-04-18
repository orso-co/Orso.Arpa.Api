CREATE OR REPLACE VIEW musician AS (
SELECT p.given_name, p.surname, mp.person_id, mp.id AS musician_profile_id, s.name AS instrument_name, mp.instrument_id
  FROM persons p, musician_profiles mp, sections s WHERE p.id = mp.person_id AND mp.instrument_id = s.id
);
