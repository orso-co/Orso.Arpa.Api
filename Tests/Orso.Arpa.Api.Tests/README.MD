# Orso.Arpa.Api.Tests

## Abstract
Integration tests for Orso.Arpa.Api.

## Execution order of tests

The OrderAttribute may be placed on a test method or fixture to specify the order in
which tests are run within the fixture or other suite in which they are contained.
Ordering is given by the required order argument to the attribute, an int.

The OrderAttribute follow this numbering schema:

index       | type of tests
-----       | ------
1 - 99      | test that get values from the data base
100 - 999   | test that modify value in the data base
1000 - 9999 | test that create new entries in the data base
10000+      | tests that delete values from the data base

## PostgreSQL local server configuration

Today's databases are focussing on not loosing any data. Under no circumstances!
This is the absolute and best fit for *productive environment*s.

But when it comes to highly efficient *testing environments* where persistency is just a matter of a breath,
there is another focus needed: **performance**! Just imagine, if an *integration test* takes three time of a
performance it could have if it does not need to take care of not loosing any data. Well, *integration tests*
generate all data from scratch, tweak it during running the tests, and finally drops them without thinking
about it.

In order to gain speed for the *integration tests*, there are options to speed up the PostgreSQL database.
The settings are **local to a server instance**, i.e. **any** database on this server will have the identical settings.
So, please ensure to have your *productive environment* securely decoupled from your *testing environment*!

Adapt your configuration here: **````%ProgramFiles%\PostgreSQL\13\data\postgresql.conf````**
```batch
#------------------------------------------------------------------------------
# WRITE-AHEAD LOG
#------------------------------------------------------------------------------
# - Settings -
fsync = off               # do not flush data to disk for crash safety
                          # (turning this off can cause unrecoverable data corruption)
synchronous_commit = off  # synchronization level;
full_page_writes = off    # recover from partial page writes
```

After changing these settings in the **postgresql.conf** you need to restart your server for these changes to
take effect.

### Integration test user

To be able to run the integration tests (and do create and delete the databases and tables needed) it is required to
add a `integration_test_user` role to your postgres server used for the tests. The password in plain text is `integration`

```sql
-- Role: integration_test_user
-- DROP ROLE IF EXISTS integration_test_user;

CREATE ROLE integration_test_user WITH
  LOGIN
  SUPERUSER
  INHERIT
  CREATEDB
  CREATEROLE
  REPLICATION
  ENCRYPTED PASSWORD 'SCRAM-SHA-256$4096:JRWu4TWTeL/PSNyczHz0SA==$3gDkERY+8C58EyWaV6lld6Otp0ecMJo5PPdvmu+BH/g=:3xVVbsaPllZI2P1EJ+g7fe6GoiInMEInFNCilrihMBU=';
  ```


## How to get rid of orphaned *doomed_database_s*

*Integration tests* are creating temporary databases from scratch. These databases will be tweaked by the
*integration tests* and afterwards those database will be dropped (i.e. deleted). If a running test crashes
or if you are debugging a test and quitting the debugger, those databases cannot be dropped automatically,
they will remain as orphans on your PostgreSQL server.

All *integration test* databases follow the naming convention ```doomed_database_*```. After terminating the
*integration test*, these databases have no value at all.

From time to time you should check and delete those orphans. There is a little batch provided in
**Tests\IntegrationTests\PostgreSQL\dropDoomed.bat**. You can run this script directly from within the
*Developer-PowerShell* within VisualStudio.

Please note, the script contains the default password for accessing the PostgreSQL server. In case you've chosen a
different password on your test machine, please store a private copy of the batch file on your computer and adapt
the PGPASSWORD environment variable.
