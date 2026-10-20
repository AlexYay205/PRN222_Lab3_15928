# Blazor CRUD scaffolding (.NET 8)

Run these commands from the solution directory.
The local tool manifest pins the generator to 8.0.23 without changing global tools.

```powershell
dotnet tool restore
dotnet build .\Vegan.BlazorWebApp.DuyLD\Vegan.BlazorWebApp.DuyLD.csproj /m:1
dotnet tool run dotnet-aspnet-codegenerator -- -p .\Vegan.BlazorWebApp.DuyLD\Vegan.BlazorWebApp.DuyLD.csproj --no-build blazor CRUD -m Vegan.Entities.DuyLD.Models.VeganPlaceDuyLd -dc Vegan.Entities.DuyLD.Models.VeganDBContext -dbProvider sqlserver
```

CRUD pages already exist in Components/Pages/VeganPlaceDuyLdPages.
Do not run scaffolding again over edited pages without backing them up.
The list route is /veganplaceduylds.

This project uses aspnet-codegenerator rather than dotnet scaffold.
The tested generator resolves the model and context from referenced projects.
No entity files need to be copied into the Blazor project.

The generated pages use IDbContextFactory, registered in Program.cs.
VeganDBContext.OnConfiguring provides the existing SQL Server configuration.
Verify your local database connection before running the pages.
