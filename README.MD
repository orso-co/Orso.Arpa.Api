# Deployment
[![Build Status](https://dev.azure.com/OrsoDevOps/Orso.Arpa.Api/_apis/build/status/orso-arpa%20-%201%20-%20CI?branchName=master)](https://dev.azure.com/OrsoDevOps/Orso.Arpa.Api/_build/latest?definitionId=2&branchName=master)

[Deployed Api](https://orso-arpa.azurewebsites.net)

[Build and Release Pipelines](https://dev.azure.com/OrsoDevOps/Orso.Arpa.Api)

[Azure Resources](https://portal.azure.com/#@azureorso.onmicrosoft.com/resource/subscriptions/c0b08a7a-5482-41e5-a89a-8805790176c5/resourceGroups/Orso.Arpa/overview)

# Entity Framework Migrations

## Add New Migration
Old Syntax:
```
dotnet ef migrations add "<NameOfMigration>" -s Orso.Arpa.Api/ -p Orso.Arpa.Persistence/

dotnet ef database update -s Orso.Arpa.Api/ -p Orso.Arpa.Persistence/
```

New Syntax:
```
Add-Migration -Name <NameOfMigration> -Project Orso.Arpa.Persistence -StartupProject Orso.Arpa.Api
```

# User Secrets

Use user secrets to locally override appsettings options, e. g. the smtp config.

Create user secret:
```
dotnet user-secrets set "EmailConfiguration:SmtpServer" "smtp.myemailprovider.com"
```

Display existing user secrets:
```
dotnet user-secrets list
```

[Microsoft Docs](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-5.0&tabs=windows)
