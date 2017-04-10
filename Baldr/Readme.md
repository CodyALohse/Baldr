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

