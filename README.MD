# Orso Arpa 2.0 Backend

Digital development in the cultural sector

ORSO Berlin develops choir and orchestra management software

[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=orso-co_Orso.Arpa.Api&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=orso-co_Orso.Arpa.Api)
[![Lines of Code](https://sonarcloud.io/api/project_badges/measure?project=orso-co_Orso.Arpa.Api&metric=ncloc)](https://sonarcloud.io/summary/new_code?id=orso-co_Orso.Arpa.Api)
[![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=orso-co_Orso.Arpa.Api&metric=sqale_rating)](https://sonarcloud.io/summary/new_code?id=orso-co_Orso.Arpa.Api)
[![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=orso-co_Orso.Arpa.Api&metric=security_rating)](https://sonarcloud.io/summary/new_code?id=orso-co_Orso.Arpa.Api)
[![Vulnerabilities](https://sonarcloud.io/api/project_badges/measure?project=orso-co_Orso.Arpa.Api&metric=vulnerabilities)](https://sonarcloud.io/summary/new_code?id=orso-co_Orso.Arpa.Api)
[![Bugs](https://sonarcloud.io/api/project_badges/measure?project=orso-co_Orso.Arpa.Api&metric=bugs)](https://sonarcloud.io/summary/new_code?id=orso-co_Orso.Arpa.Api)
[![Code Smells](https://sonarcloud.io/api/project_badges/measure?project=orso-co_Orso.Arpa.Api&metric=code_smells)](https://sonarcloud.io/summary/new_code?id=orso-co_Orso.Arpa.Api)
[![Technical Debt](https://sonarcloud.io/api/project_badges/measure?project=orso-co_Orso.Arpa.Api&metric=sqale_index)](https://sonarcloud.io/summary/new_code?id=orso-co_Orso.Arpa.Api)
[![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=orso-co_Orso.Arpa.Api&metric=reliability_rating)](https://sonarcloud.io/summary/new_code?id=orso-co_Orso.Arpa.Api)

## Deployment
[![Build Status](https://dev.azure.com/OrsoDevOps/Orso.Arpa.Api/_apis/build/status/orso-arpa%20-%201%20-%20CI?branchName=master)](https://dev.azure.com/OrsoDevOps/Orso.Arpa.Api/_build/latest?definitionId=2&branchName=master)

[Build and Release Pipelines](https://dev.azure.com/OrsoDevOps/Orso.Arpa.Api)

## Running the app using Docker
>  Make sure to have the latest docker version installed for your OS: [Docker](https://www.docker.com/get-started)

Use [docker-compose.yml](./docker-compose.yml) to spin up the api, postgres and mailhog.
- **API:** http://localhost:5000
- **Postgres:** localhost:5432
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
```shell
<project-root>$ docker-compose stop api
<project-root>$ docker-compose build --no-cache api
```
> Dotnet watch and volume src mounts are currently disabled due to performance impacts on the host system.
>
> **Development Notes:**
> Create an entry for *mssql* and *mail* in your local hosts file.
> c:\windows\system32\drivers\etc\hosts or /etc/hosts
> -> 127.0.0.1 mail postgres
>
> *It's also possible to use "localhost". Adjust the environment settings for ConnectionStrings and SmtpServer.*
>
> Stop the docker api service - *docker-compose stop api*
> - Use your IDE or the dotnet cli to run it on your local machine
> - You might need to set some environment variables or edit *appsettings.Development.json*:
>    - ASPNETCORE_ENVIRONMENT=Development
>    - ConnectionStrings__PostgreSQLConnection=host=postgres;database=orso-arpa;User Id=postgres;Password=postgres;
>    - EmailConfiguration__SmtpServer=mail
>    - EmailConfiguration__Port=1025
>    - EmailConfiguration__From=test@localhost

## Starting the app locally without using docker

- Install the [.NET 8.x SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- Install [PostgreSQL 16.x](https://www.postgresql.org/download/), start a server on `localhost:5432`. 
- Set the PostgreSQL connection string via [dotnet user-secrets](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets) (see appsettings.Development.json ConnectionStrings:PostgreSQLConnection)
- Install and start [azurite](https://learn.microsoft.com/en-us/azure/storage/common/storage-use-azurite?tabs=npm%2Cblob-storage), e. g. via npm
- Install oder configure a fake smtp server, e.g. [Mailtrap](https://mailtrap.io)
- Set the smtp configuration via [dotnet user-secrets](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets) (see appsettings.Development.json EmailConfiguration)
- Run the project with the command `dotnet run --project Orso.Arpa.Api`
- Open the Swagger UI on `https://localhost:5001`
- Calling the local backend via the frontend may lead to certificate errors. In this case, run `dotnet dev-certs https`  and `dotnet dev-certs https --trust`

## Login
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

### Test projects

* [Orso.Arpa.Api.Tests](Tests/Orso.Arpa.Api.Tests/README.MD)
* Orso.Arpa.Application.Tests
* Orso.Arpa.Domain.Tests
* Orso.Arpa.Infrastructure.Tests
* Orso.Arpa.Mail.Tests
* Orso.Arpa.Persistence.Tests
* [Orso.Arpa.Tests.Shared](Tests/Orso.Arpa.Tests.Shared/README.MD)
