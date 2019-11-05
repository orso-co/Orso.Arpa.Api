# Migrations

## Add New Migration
Alte Syntax:
```
dotnet ef migrations add "<NameOfMigration>" -s Orso.Arpa.Api/ -p Orso.Arpa.Persistence/
```

Neue Syntax:
```
Add-Migration -Name <NameOfMigration> -Project Orso.Arpa.Persistence -StartupProject Orso.Arpa.Api
```
