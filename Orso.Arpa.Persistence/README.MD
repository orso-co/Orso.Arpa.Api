# Orso.Arpa.Persistence

## Abstract
This project contains the database layer. Entity Framework Core (EF Core) Core First approach is used.

## Subfolders

### Configurations
Contains the EntityTypeConfigurations for the entities.

### DataAccess
Contains the DbContext `ArpaContext`.

### Migrations
Contains the EF Core Migrations.

### Seed
Contains Data that will be seeded on app start. Don't forget to add a new migrations after adding or updating seed data.

## Database

PostgreSQL Server is used. If you run the app locally, the LocalDB is used by default. Consider changing
the connection string in the `appsettings.Development.json` file if no LocalDB is available. The appsettings
file can be found in the Orso.Arpa.Api project.

## Entity Framework Migrations
Use a command line or terminal window to run these commands. If you are using Visual Studio consider using the
built-in Package Manager Console.

### Tool Installation

Install globally the dotnet ef tools: `dotnet tool install --global dotnet-ef`

Update tools to the most recent version: `dotnet tool update --global dotnet-ef`

### Add New Migration

You may be forced to remove `${aspnet-request-posted-body-shadowed}` from appsettings(.Development).json to make this work.
```
dotnet ef migrations add "<NameOfMigration>" -s Orso.Arpa.Api/ -p Orso.Arpa.Persistence/ -c ArpaContext
```

### Remove Last Migration
In case a formerly added migration needs to be revoked, ensure to have revoked *EntityTypeConfigurations* to the point of
time, where the errorneous add was issued. Then run the *remove* command.
```
dotnet ef migrations remove -s Orso.Arpa.Api/ -p Orso.Arpa.Persistence/
```

### Update Database
```
dotnet ef database update -s Orso.Arpa.Api/ -p Orso.Arpa.Persistence/
```
In case you need to downgrade your database to a former migration,

1. obtain then list of migration names
    ```
    dotnet ef migrations list -s Orso.Arpa.Api/ -p Orso.Arpa.Persistence/
    ```
1. downgrade to a selected migration
    ```
    dotnet ef database update "<NameOfMigration>" -s Orso.Arpa.Api/ -p Orso.Arpa.Persistence/
    ```


Hint: The database is updated automatically to the most recent migration at app start. Only use this
command if you want to update the database without starting the app.
