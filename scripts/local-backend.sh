#!/bin/bash
# Startet das Backend lokal mit allen nötigen Env-Vars
# Voraussetzung: docker compose up -d postgres mail

set -e

export ConnectionStrings__PostgreSQLConnection="host=localhost;database=orso-arpa;User Id=postgres;Password=postgres;"
export JwtConfiguration__TokenKey="ThisIsAVeryLongSecretKeyForJWTTokensThatMustBeAtLeast64CharactersLongForHS512Algorithm"
export SeedConfiguration__InitialAdmin__Password='Pa$$w0rd'

cd "$(dirname "$0")/../Orso.Arpa.Api"
dotnet run
