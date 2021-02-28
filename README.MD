# Orso Arpa 2.0 Backend

## Deployment
[![Build Status](https://dev.azure.com/OrsoDevOps/Orso.Arpa.Api/_apis/build/status/orso-arpa%20-%201%20-%20CI?branchName=master)](https://dev.azure.com/OrsoDevOps/Orso.Arpa.Api/_build/latest?definitionId=2&branchName=master)

[Deployed Test Api](https://orso-arpa.azurewebsites.net)

[Build and Release Pipelines](https://dev.azure.com/OrsoDevOps/Orso.Arpa.Api)

[Azure Resources](https://portal.azure.com/#@azureorso.onmicrosoft.com/resource/subscriptions/c0b08a7a-5482-41e5-a89a-8805790176c5/resourceGroups/Orso.Arpa/overview)

# Running the app

## Start projects
For a developer setup, use Orso.Arpa.Api and Orso.Arpa.FakeSmtp as start projects.

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
* [Orso.Arpa.FakeSmtp](Orso.Arpa.FakeSmtp/README.MD)

### Test projects

* Orso.Arpa.Api.Tests
* Orso.Arpa.Application.Tests
* Orso.Arpa.Domain.Tests
* Orso.Arpa.Infrastructure.Tests
* Orso.Arpa.Mail.Tests
* Orso.Arpa.Persistence.Tests
* Orso.Arpa.Tests.Shared
