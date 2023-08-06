@echo off

rem ------------------------------------------------------------------
rem created by: Erich Hermann, 2021-04-11
rem 
rem drops all *doomed_database* databases from the PostgreSQL server
rem
rem precondition: please install grep untility from
rem    http://gnuwin32.sourceforge.net/packages/grep.htm
rem into the same folder as this batch, or somewhere in your %PATH%,
rem or use the fully qualified path to the grep utiliy in the
rem for statement below
rem ------------------------------------------------------------------

setlocal
pushd %ProgramFiles%\PostgreSQL\13\bin

rem set your secret PostgreSQL password for the user 'postgres' here
set PGPASSWORD=postgres

rem lets get rid of all doomed databases
for /f "tokens=1*" %%a in ('psql -c "\l" -U postgres ^| grep doomed_database*') do dropdb -f -U postgres %%a & echo Dropped: %%a
