# Orso Arpa 2.0 Backend

## Deployment
[![Build Status](https://dev.azure.com/OrsoDevOps/Orso.Arpa.Api/_apis/build/status/orso-arpa%20-%201%20-%20CI?branchName=master)](https://dev.azure.com/OrsoDevOps/Orso.Arpa.Api/_build/latest?definitionId=2&branchName=master)

[Deployed Test Api](https://orso-arpa.azurewebsites.net)

[Build and Release Pipelines](https://dev.azure.com/OrsoDevOps/Orso.Arpa.Api)

[Azure Resources](https://portal.azure.com/#@azureorso.onmicrosoft.com/resource/subscriptions/c0b08a7a-5482-41e5-a89a-8805790176c5/resourceGroups/Orso.Arpa/overview)

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


#### Using Docker
Use [docker-compose.yml](./docker-compose.yml) to spin up the api, mssql and mailhog (receives all emails).
1. API: http://localhost:5000
2. MSSQL: localhost:1433 (USerId: SA, Password: Local@Dev!)
3. MailHog:  localhost:8025

```shell
docker-compose up
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

### Test projects

* [Orso.Arpa.Api.Tests](Tests/Orso.Arpa.Api.Tests/README.MD)
* Orso.Arpa.Application.Tests
* Orso.Arpa.Domain.Tests
* Orso.Arpa.Infrastructure.Tests
* Orso.Arpa.Mail.Tests
* Orso.Arpa.Persistence.Tests
* Orso.Arpa.Tests.Shared
