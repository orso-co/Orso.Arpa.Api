#!/bin/bash
# Setzt die lokale DB zurück auf Seeder-Stand
# Docker PostgreSQL muss laufen (docker compose up -d postgres)

set -e

DB_CONTAINER="orsoarpaapi-postgres-1"
DB_NAME="orso-arpa"
DB_USER="postgres"

echo "Dropping and recreating database..."
docker exec "$DB_CONTAINER" psql -U "$DB_USER" -c "DROP DATABASE IF EXISTS \"$DB_NAME\" WITH (FORCE);"
docker exec "$DB_CONTAINER" psql -U "$DB_USER" -c "CREATE DATABASE \"$DB_NAME\";"

echo "Database reset."
echo "Start backend to apply migrations + seed data:"
echo "  ./scripts/local-backend.sh"
echo ""
echo "Seed-User: admin/performer/staff/testwolf — Passwort: Pa\$\$w0rd"
