# Deployment
[![Build Status](https://dev.azure.com/OrsoDevOps/Orso.Arpa.Api/_apis/build/status/orso-arpa%20-%201%20-%20CI?branchName=master)](https://dev.azure.com/OrsoDevOps/Orso.Arpa.Api/_build/latest?definitionId=2&branchName=master)

[Deployed Api](https://orso-arpa.azurewebsites.net)

[Build and Release Pipelines](https://dev.azure.com/OrsoDevOps/Orso.Arpa.Api)

[Azure Resources](https://portal.azure.com/#@azureorso.onmicrosoft.com/resource/subscriptions/c0b08a7a-5482-41e5-a89a-8805790176c5/resourceGroups/Orso.Arpa/overview)

# Running the app

## Login
The initial database contains a single user with an admin role. You can login using the following credentials:
* UsernameOrEmail: admin
* Password: Pa$$w0rd

## Development Database for MacOS
Localdb is used in the development environment as the local database. Localdb doesn't support MacOS, so to start the app a docker container running MSSQL is required and a few changes in the source code are necessary:
* Install Docker: https://docs.docker.com/docker-for-mac/install/
* Pull the latest MSSQL docker image: docker pull mcr.microsoft.com/mssql/server:2019-latest
* Start an MSSQL container: docker run -d --name sql_server -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=Pa$$w0rd' -p 1433:1433 mcr.microsoft.com/mssql/server:2019-latest
* Change the default connection string in appsettings.test.json and appsettings.development.json to "DefaultConnection": "Server=localhost,1433\\Catalog=orso-arpa;User=SA;Password=Pa$$w0rd;MultipleActiveResultSets=true"
* Replace \\ in path names with /, for example in TemplateParser.cs:
var templatePath = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/Templates/Html/{templateData.Name}.html";
* There may be other places in the source code where path names need to be adjusted; please add them here if you find any

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
