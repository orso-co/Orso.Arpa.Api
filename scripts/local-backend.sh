#!/bin/bash
# Startet das Backend lokal mit allen nötigen Env-Vars
# Voraussetzung: docker compose up -d postgres mail

set -e

SCRIPT_DIR="$(cd "$(dirname "$0")" && pwd)"
ENV_FILE="$SCRIPT_DIR/.env.local"

if [ ! -f "$ENV_FILE" ]; then
  echo "FEHLER: $ENV_FILE nicht gefunden!"
  echo "Erstelle die Datei mit den lokalen Dev-Secrets (siehe .env.local.example)"
  exit 1
fi

# Secrets aus .env.local laden
set -a
source "$ENV_FILE"
set +a

cd "$SCRIPT_DIR/../Orso.Arpa.Api"
dotnet run
