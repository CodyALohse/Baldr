# Baldr
Nordic god of love, peace, forgiveness, justice, and light. 

## Tech Stack
dotnet core V1.1

## ORM
Entity Framwork Core

## Database
Postgre 9.2
Create new Login Role with password
Set DB owner as new login role
Add new login role to database under Privileges with ALL Privileges
In order to fix: 42501: permission denied for relation __EFMigrationsHistory try :
``GRANT ALL PRIVILEGES ON ALL TABLES IN SCHEMA public TO 'new login role';``

## Baldr.IntegrationTests
Setting Environment - Set Environment Variables under Project properties -> Debug
Set ASPNETCORE_ENVIRONMENT
https://docs.microsoft.com/en-us/aspnet/core/fundamentals/environments

## Running as a Windows Service
Since this project right now is being run on an internal network only the application does not need
to be run behind a reverse proxy such as IIS. In order to run the application in the background 
the application will be created as a windows service. At the moment dotnet core base libraries do not 
have support for creating windows services without creating a dependency on the entire .Net framework. 
There maybe future support from the dotnet core libraries but at this time it doesn't appear to be
actively persued.
Instead the project uses a 3rd party lib to provide the windows service functionality. 

https://github.com/dasMulli/dotnet-win32-service

## Logging
### Bootstrap Logger
Used to log basic messages to a text file prior to the main logging framework takes over. Initially created to debug
the windows service startup. The logger will not create the file automatically, in order to get the logger output
a file with the name ``bootstraplog.log`` needs to be created next to the core.dll.

### Serilog & Elasticsearch
Serilog is used and configured to write output to Elasticsearch. Kibana can be used to visualize the output.
Serilog can be configured via the appsettings.json file.

https://github.com/serilog/serilog-sinks-elasticsearch

