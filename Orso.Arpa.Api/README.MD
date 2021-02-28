# Orso.Arpa.Api

## Abstract
The API Layer of the app.

## User Secrets

Use user secrets to locally override appsettings options, e. g. the smtp config.

Create user secret:
```
dotnet user-secrets set "EmailConfiguration:SmtpServer" "smtp.myemailprovider.com"
```

Display existing user secrets:
```
dotnet user-secrets list
```

For more information see [Microsoft Docs about app secrets](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-5.0&tabs=windows)
