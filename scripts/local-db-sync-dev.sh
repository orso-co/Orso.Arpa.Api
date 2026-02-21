#!/bin/bash
# Synct lokale DB mit DEV-Stand (arpa.loopus.it)
# Voraussetzung: SSH-Zugang zu raspi3, lokale DB läuft

set -e

SSH_TARGET="wolf@r3.loopus.it -p 2222"
DEV_CONTAINER="arpa-postgres-1"
LOCAL_CONTAINER="orsoarpaapi-postgres-1"
DB_NAME="orso-arpa"
DB_USER="postgres"
DUMP_FILE="/tmp/arpa-dev-dump.sql"

echo "1/3 Dumping DEV database..."
ssh $SSH_TARGET "docker exec $DEV_CONTAINER pg_dump -U $DB_USER -d $DB_NAME --clean --if-exists" > "$DUMP_FILE"
echo "    Dump: $(du -h "$DUMP_FILE" | cut -f1)"

echo "2/3 Importing into local database..."
docker exec -i "$LOCAL_CONTAINER" psql -U "$DB_USER" -d "$DB_NAME" < "$DUMP_FILE"

echo "3/3 Done!"
echo ""
echo "WICHTIG: Seed-User (admin/performer/staff/testwolf) haben jetzt DEV-Passwörter."
echo "Zum Zurücksetzen: ./scripts/local-db-reset.sh (DB komplett neu + seeden)"
echo "Oder: Backend starten, Migrationen laufen automatisch."
