# SweatSpace
Api for finding and managing workouts

Sort of folder structured N-tier for loose coupling.

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
Moq tests and documentation will be made
Available endpoints right now (april) are available inside the Docs folder
The app also has task scheduling (coravel) which is currently sending a mail to a fail every week.

