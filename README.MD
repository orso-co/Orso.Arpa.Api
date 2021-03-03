# Orso Arpa 2.0 Backend

## Deployment
[![Build Status](https://dev.azure.com/OrsoDevOps/Orso.Arpa.Api/_apis/build/status/orso-arpa%20-%201%20-%20CI?branchName=master)](https://dev.azure.com/OrsoDevOps/Orso.Arpa.Api/_build/latest?definitionId=2&branchName=master)

[Deployed Test Api](https://orso-arpa.azurewebsites.net)

[Build and Release Pipelines](https://dev.azure.com/OrsoDevOps/Orso.Arpa.Api)

[Azure Resources](https://portal.azure.com/#@azureorso.onmicrosoft.com/resource/subscriptions/c0b08a7a-5482-41e5-a89a-8805790176c5/resourceGroups/Orso.Arpa/overview)

## Running the app using Docker
>  Make sure to have the latest docker version installed for your OS: [Docker](https://www.docker.com/get-started)

## Development Database for MacOS
Localdb is used in the development environment as the local database. Localdb doesn't support MacOS, so to start the app a docker container running MSSQL is required and a few changes in the source code are necessary:
* Install Docker: https://docs.docker.com/docker-for-mac/install/
* Pull the latest MSSQL docker image: docker pull mcr.microsoft.com/mssql/server:2019-latest
* Start an MSSQL container: docker run -d --name sql_server -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=Pa$$w0rd' -p 1433:1433 mcr.microsoft.com/mssql/server:2019-latest
* Change the default connection string in appsettings.test.json and appsettings.development.json to "DefaultConnection": "Server=localhost,1433\\Catalog=orso-arpa;User=SA;Password=Pa$$w0rd;MultipleActiveResultSets=true"
* Replace \\\ in path names with /, for example in TemplateParser.cs:
var templatePath = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/Templates/Html/{templateData.Name}.html";
* There may be other places in the source code where path names need to be adjusted; please add them here if you find any

# Entity Framework Migrations

Use [docker-compose.yml](./docker-compose.yml) to spin up the api, mssql and mailhog.
- **API:** http://localhost:5000
- **MSSQL:** localhost:1433 (USerId: SA, Password: Local@Dev!)
- **MailHog:**  http://localhost:8025 (receives all emails)

#### Start
```shell
<project-root>$ docker-compose up -d
```
#### Stop
```shell
<project-root>$ docker-compose stop
```
#### Stop & Remove persistent volumes
```shell
<project-root>$ docker-compose down -v
```
#### Monitor Logs
```shell
<project-root>$ docker-compose logs -f api
```
#### Rebuild API
Rebuilding the API on source updates.
> Dotnet watch and volume src mounts are currently disabled due to performance impacts on the host system.
>
> **Development Notes:**
> Create an entry for *mssql* and *mail* in your local hosts file.
> c:\windows\system32\drivers\etc\hosts or /etc/hosts
> -> 127.0.0.1 mail mssql
>
> *It's also possible to use "localhost". Adjust the environment settings for ConnectionStrings and SmtpServer.*
>
> Stop the docker api service - *docker-compose stop api*
> - Use your IDE or the dotnet cli to run it on your local machine
> - You might need to set some environment variables or edit *appsettings.Development.json*:
>    - ASPNETCORE_ENVIRONMENT=Development
>    - ConnectionStrings__DefaultConnection=Server=mssql,1433;Database=orso-arpa;MultipleActiveResultSets=true;User Id=SA;Password=Local@Dev!
>    - EmailConfiguration__SmtpServer=mail
>    - EmailConfiguration__Port=1025
>    - EmailConfiguration__From=test@localhost
```shell
<project-root>$ docker-compose build --no-cache api
```

### Login
The initial database contains a single user with an admin role. You can login using the following credentials:
* UsernameOrEmail: admin
* Password: Pa$$w0rd

## Projects

### Application stack top to bottom:

* [Orso.Arpa.Api](Orso.Arpa.Api/README.MD)
* [Orso.Arpa.Application](Orso.Arpa.Application/README.MD)
* [Orso.Arpa.Domain](Orso.Arpa.Domain/README.MD)
* [Orso.Arpa.Persistence](Orso.Arpa.Persistence/README.MD)

### Helper projects

* [Orso.Arpa.Mail](Orso.Arpa.Mail/README.MD)
* [Orso.Arpa.Infrastructure](Orso.Arpa.Infrastructure/README.MD)
* [Orso.Arpa.Misc](Orso.Arpa.Misc/README.MD)
* [Orso.Arpa.FakeSmtp](Orso.Arpa.FakeSmtp/README.MD)

### Test projects

* Orso.Arpa.Api.Tests
* Orso.Arpa.Application.Tests
* Orso.Arpa.Domain.Tests
* Orso.Arpa.Infrastructure.Tests
* Orso.Arpa.Mail.Tests
* Orso.Arpa.Persistence.Tests
* Orso.Arpa.Tests.Shared
