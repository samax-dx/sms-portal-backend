# SMS-Portal Backend
*Details yet to be written*

<br>

## SDK/Toolchain/Tools Setup :

Download & install .Net 5 SDK - [https://dotnet.microsoft.com/download](https://dotnet.microsoft.com/download)

Install __Entity Framework__ cli tool - Run `dotnet tool install --global dotnet-ef`  
** *Look for any post install instruction(s) it may provide*

<br>

## Run project :
`cd <project_directory>`  
`dotnet run`  
http://localhost:5000/Category  
https://localhost:5001/Category

<br>

## Build/Run/Publish :
`dotnet build`  
** [https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-build](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-build)  

`dotnet run`  
** [https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-run](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-run)  

`dotnet publish`  
** [https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-publish](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-publish)

<br>

## Useful commands :

DB Context & Model scaffolding -  
`dotnet ef dbcontext scaffold <CONNECTION> <PROVIDER>`  

example:  
`dotnet ef dbcontext scaffold "DataSource=sms-portal.db" Microsoft.EntityFrameworkCore.Sqlite`  

details:  
[https://docs.microsoft.com/en-us/ef/core/cli/dotnet](https://docs.microsoft.com/en-us/ef/core/cli/dotnet)

<br>

Generate .Net 5 gitignore -  
`dotnet new gitignore`

<br>

## Setup - Visual Studio Code [optional] :

Install vscode C# Extension - [https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp)

Enable C# intellisense - Open Project -> Open Command-Palette (`[ctrl|cmd]+shift+p`) -> Search OmniSharp -> Select Project
