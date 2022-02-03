# SweatSpace
Api for finding and managing workouts

Refactored from N-tier to a pragmatic Clean Architecture version for loose coupling. Right now the repositories are "stupid repositories" but if one did instead add some business rules and exception handling there alternatively place a service in between the Core Services and the Repository you could even remove the dependency on Entity framework core and Identity completely. However the Paginated list would also have to be abstracted away behind an interface which can be done by placing the interface inside Core and then having the implementation inside the Infrastructure layer.  

# How to use
1. Add a appsettings.secret.json file
- Add a connection string for PostgreSQL using this template: `
"Default": "Server=localhost;Port=5432;Database=SweatSpace;User Id=yourUsername;Password=yourPassword;Integrated Security=true;"`
- Add a random TokenKey
- Configure Coravel driver for mailing. Currently just logging to a file: `"Coravel": {
    "Mail": {
      "Driver": "FileLog"
    }`

# Project info
Available endpoints right now (april) are available inside the Docs folder.
The app also has task scheduling (coravel) which is currently sending a mail to a file every week.

## Some topics covered in this app
- http requests/reponses
- global exc handler
- ref by id
- db design/relationships with EF and fluent api
- generics 
- automapper
- Multi layered architecture with focus on core
- coravel task scheduling+mailing 
- Authentication and Authorization with asp identity and Jwt token
- postman 
- swagger documentation
- SOLID, DRY, Repository pattern etc... 
- extension methods 
- logging
- CRUD operations
- paging and filtering
- Unit and Integration tests using xUnit, Moq, FluentAssertions, AutoFixture and a Sqlite in memory db

### Generate test reports using Coverlet and ReportGenerator
1. Run `dotnet tool install -g dotnet-reportgenerator-globaltool`
2. Run `dotnet test --collect:"XPlat Code Coverage"`
3. Copy the generated path
4. Run with the generated path `reportgenerator -reports:pastePathHere -targetdir:.coverage-report -reporttypes:HTML`







