# Portal2

Portal2 is a raw Modular Application Framework similar to IBySpy Portal based on asp.net core mvc. 


# Getting Started

- set connection string in appsetting.json
- run Instal.sql in ~/Portal.Core/Install/
- run portal.core

# Running Module
For module developent you have to publish Portal2 and follow the steps below:

- Create Empty project in ~/Portal.Core/Module/
- Add Portal.Infrustructure.dll to dependencies
- Set Assembly name and Default namespace to Portal.Module.[ModuleName]
- In Startup.cs Inherit from IStartUp
- Add module name with Area attribute in controller
- Define New Page in Page table in sql
- Define Module and Permissions in it's table
- run Portal.Core.exe and navigate to /page?pageid=[PageId]

For more information you can download [Authentication](https://github.com/sh71ali40/Authentication) module
