## BuberDinner
This repository follows course from YouTube on channel named `Amichai Mantinband` [(link)](https://www.youtube.com/channel/UClz49zOCnzsclUJY-t62lIw). The course's name is `REST API Following CLEAN ARCHITECTURE & DDD` [(link)](https://www.youtube.com/watch?v=fhM0V2N1GpY&list=PLzYkqgWkHPKBcDIP5gzLfASkQyTdy0t4k).

### About applications
Application is written in .NET 6.0 with C# using WebAPI technology. This solutions follows Clean Architecture principles and Domain-Driven Design principles. It contains 4 layers:
 - Domain
 - Application
 - Infrastructure
 - Api

#### Build and run
You can run the application in two ways:
1. Local application
> ⚠️ This way need to have separate database running on your local machine for some requests.

To run this project you need to have .NET 6.0 runtime installed. Then clone (or download) this repository.\
Then open CLI and open folder with application. Then run from CLI with command:
```
dotnet run --project .\BuberDinner.Api\
```

2. Docker

Application has `docker-compose` file for running the application with MSSQL Server database in container. \
To run the application using Docker you have to have it installed on your machine. Then close (or downlaod) this repository. \
In the main folder type the command:
```
docker-compose up -d --build
```

In both ways of running the application, you have to use migrations for creating tables to fully use the application.
