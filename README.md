# SweatSpace
Api for finding and managing workouts

Refactored from N-tier to a pragmatic Clean Architecture version for loose coupling

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

# Some topics covered in this app
- http requests/reponses
- global exc handler
- ref by id
- db design/relationships with EF and fluent api
- generics 
- automapper
- N-tier 3 layered architecture
- coravel task scheduling+mailing 
- Authentication and Authorization with asp identity and Jwt token
- postman 
- swagger documentation
- SOLID, DRY, Repository pattern etc... 
- extension methods 
- logging
- CRUD operations
- paging and filtering
- Tests with xUnit, Moq and FluentAssertions





