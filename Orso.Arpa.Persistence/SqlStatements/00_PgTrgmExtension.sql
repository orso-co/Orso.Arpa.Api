-- Enable pg_trgm extension for fuzzy text search (trigram matching)
-- This enables ILIKE with better performance and similarity() function
CREATE EXTENSION IF NOT EXISTS pg_trgm;
