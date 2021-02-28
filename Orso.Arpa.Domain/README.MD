# Orso.Arpa.Domain

## Abstract
This project contains the domain logic of the app.

## Subfolders

### Configuration
Contains classes that represent some sections of the `appsettings*.json` file which can be found in the Orso.Arpa.Api project.

### Entities
Contains the entities which are used by `Entity Framework Core` as representations of the database tables.

### Enums
Contains some Enum definitions

### Errors
Contains classes that define app specific exceptions

### Extensions
Contains...well...extensions ;-)

### Generic Handlers
Contains generic handlers to handle standard CRUD operations (Yes, developers are sometimes lazy to write)

### Identity
Contains overwrite implementations of ASP.NET Core Identity manager classes (e. g. User Manager)

### Interfaces
Contains interfaces which grant access to implementations located in other projects such as Orso.Arpa.Infrastructure or Orso.Arpa.Persistence.

### Logic
Contains the domain logic which is encapsuled in MediatR queries, commands and handlers.
