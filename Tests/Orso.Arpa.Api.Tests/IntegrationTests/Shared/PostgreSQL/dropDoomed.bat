@echo off

rem ------------------------------------------------------------------
rem created by: Erich Hermann, 2021-04-11
rem 
rem drops all *doomed_database* databases from the PostgreSQL server
rem ------------------------------------------------------------------

setlocal
pushd %ProgramFiles%\PostgreSQL\13\bin

rem TODO: set your secret PostgreSQL password for the user 'postgres' here
set PGPASSWORD=postgres

rem lets get rid of all doomed databases
for /f "tokens=1*" %%a in ('psql -c "\l" -U postgres ^| grep doomed_database*') do dropdb -f -U postgres %%a & echo Dropped: %%a
