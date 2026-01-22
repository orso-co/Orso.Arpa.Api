-- Global search function using pg_trgm for fuzzy matching
-- Searches across: Persons (incl. email, instrument), Appointments, Projects, News

CREATE OR REPLACE FUNCTION fn_global_search(search_query TEXT, max_results INT DEFAULT 50)
RETURNS TABLE (
    entity_type TEXT,
    entity_id UUID,
    display_name TEXT,
    additional_info TEXT,
    relevance REAL
) AS $$
BEGIN
    RETURN QUERY

    -- Search Persons (Name, Email, Instrument)
    SELECT
        'Person'::TEXT as entity_type,
        p.id as entity_id,
        (p.given_name || ' ' || p.surname)::TEXT as display_name,
        COALESCE(
            (SELECT string_agg(DISTINCT s.name, ', ')
             FROM musician_profiles mp
             JOIN sections s ON s.id = mp.instrument_id
             WHERE mp.person_id = p.id AND mp.deleted = false AND s.deleted = false),
            ''
        ) || CASE
            WHEN cd.value IS NOT NULL THEN ' • ' || cd.value
            ELSE ''
        END as additional_info,
        GREATEST(
            similarity(p.given_name || ' ' || p.surname, search_query),
            similarity(COALESCE(cd.value, ''), search_query),
            CASE WHEN (p.given_name || ' ' || p.surname) ILIKE '%' || search_query || '%' THEN 0.8 ELSE 0 END,
            CASE WHEN COALESCE(cd.value, '') ILIKE '%' || search_query || '%' THEN 0.7 ELSE 0 END
        ) as relevance
    FROM persons p
    LEFT JOIN LATERAL (
        SELECT value FROM contact_detail
        WHERE person_id = p.id AND key = 0 AND deleted = false
        ORDER BY preference DESC
        LIMIT 1
    ) cd ON true
    WHERE p.deleted = false
      AND (
          (p.given_name || ' ' || p.surname) ILIKE '%' || search_query || '%'
          OR p.given_name ILIKE '%' || search_query || '%'
          OR p.surname ILIKE '%' || search_query || '%'
          OR COALESCE(cd.value, '') ILIKE '%' || search_query || '%'
          OR EXISTS (
              SELECT 1 FROM musician_profiles mp
              JOIN sections s ON s.id = mp.instrument_id
              WHERE mp.person_id = p.id
                AND mp.deleted = false
                AND s.deleted = false
                AND s.name ILIKE '%' || search_query || '%'
          )
          OR similarity(p.given_name || ' ' || p.surname, search_query) > 0.3
      )

    UNION ALL

    -- Search Appointments
    SELECT
        'Appointment'::TEXT as entity_type,
        a.id as entity_id,
        a.name::TEXT as display_name,
        to_char(a.start_time, 'DD.MM.YYYY HH24:MI') ||
            COALESCE(' • ' || v.name, '') as additional_info,
        GREATEST(
            similarity(a.name, search_query),
            similarity(COALESCE(a.public_details, ''), search_query),
            CASE WHEN a.name ILIKE '%' || search_query || '%' THEN 0.8 ELSE 0 END,
            CASE WHEN COALESCE(a.public_details, '') ILIKE '%' || search_query || '%' THEN 0.6 ELSE 0 END
        ) as relevance
    FROM appointments a
    LEFT JOIN venues v ON v.id = a.venue_id AND v.deleted = false
    WHERE a.deleted = false
      AND (
          a.name ILIKE '%' || search_query || '%'
          OR COALESCE(a.public_details, '') ILIKE '%' || search_query || '%'
          OR COALESCE(a.internal_details, '') ILIKE '%' || search_query || '%'
          OR COALESCE(v.name, '') ILIKE '%' || search_query || '%'
          OR similarity(a.name, search_query) > 0.3
      )

    UNION ALL

    -- Search Projects
    SELECT
        'Project'::TEXT as entity_type,
        pr.id as entity_id,
        pr.title::TEXT as display_name,
        COALESCE(pr.code, '') ||
            CASE WHEN pr.start_date IS NOT NULL
                THEN ' • ' || to_char(pr.start_date, 'DD.MM.YYYY')
                ELSE ''
            END as additional_info,
        GREATEST(
            similarity(pr.title, search_query),
            similarity(COALESCE(pr.short_title, ''), search_query),
            similarity(COALESCE(pr.code, ''), search_query),
            CASE WHEN pr.title ILIKE '%' || search_query || '%' THEN 0.8 ELSE 0 END,
            CASE WHEN COALESCE(pr.code, '') ILIKE '%' || search_query || '%' THEN 0.9 ELSE 0 END
        ) as relevance
    FROM projects pr
    WHERE pr.deleted = false
      AND (
          pr.title ILIKE '%' || search_query || '%'
          OR COALESCE(pr.short_title, '') ILIKE '%' || search_query || '%'
          OR COALESCE(pr.description, '') ILIKE '%' || search_query || '%'
          OR COALESCE(pr.code, '') ILIKE '%' || search_query || '%'
          OR similarity(pr.title, search_query) > 0.3
      )

    UNION ALL

    -- Search News
    SELECT
        'News'::TEXT as entity_type,
        n.id as entity_id,
        n.title::TEXT as display_name,
        to_char(n.created_at, 'DD.MM.YYYY') as additional_info,
        GREATEST(
            similarity(n.title, search_query),
            similarity(COALESCE(n.content, ''), search_query),
            CASE WHEN n.title ILIKE '%' || search_query || '%' THEN 0.8 ELSE 0 END,
            CASE WHEN COALESCE(n.content, '') ILIKE '%' || search_query || '%' THEN 0.6 ELSE 0 END
        ) as relevance
    FROM news n
    WHERE n.deleted = false
      AND n.show = true
      AND (
          n.title ILIKE '%' || search_query || '%'
          OR COALESCE(n.content, '') ILIKE '%' || search_query || '%'
          OR similarity(n.title, search_query) > 0.3
      )

    ORDER BY relevance DESC
    LIMIT max_results;

END;
$$ LANGUAGE plpgsql;
