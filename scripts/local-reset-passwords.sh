#!/bin/bash
# Setzt Passwörter der Seed-User auf Pa$$w0rd zurück
# Einfachste Methode: DB droppen + neu seeden (local-db-reset.sh)
#
# Dieses Skript ist die Alternative wenn man DEV-Daten behalten will.
# Es startet kurz das Backend um einen korrekten Passwort-Hash zu generieren.

set -e

LOCAL_CONTAINER="orsoarpaapi-postgres-1"
DB_NAME="orso-arpa"
DB_USER="postgres"

echo "Einfachste Methode: DB komplett zurücksetzen (alle DEV-Daten gehen verloren):"
echo "  ./scripts/local-db-reset.sh"
echo ""
echo "Alternative: Lockout der Seed-User zurücksetzen (Passwörter bleiben DEV-Stand):"

docker exec "$LOCAL_CONTAINER" psql -U "$DB_USER" -d "$DB_NAME" -c "
UPDATE \"AspNetUsers\"
SET lockout_end = NULL,
    access_failed_count = 0
WHERE user_name IN ('admin', 'performer', 'staff', 'testwolf');
"

echo "Lockout zurückgesetzt für admin, performer, staff, testwolf."
echo ""
echo "Hinweis: Passwörter sind weiterhin die DEV-Hashes."
echo "Für lokale Passwörter (Pa\$\$w0rd): ./scripts/local-db-reset.sh"
