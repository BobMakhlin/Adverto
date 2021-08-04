# Adverto
[![Codacy Badge](https://app.codacy.com/project/badge/Grade/ba354f4a93d1430ca19cf0235e19ac65)](https://www.codacy.com/gh/BobMakhlin/Adverto/dashboard?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=BobMakhlin/Adverto&amp;utm_campaign=Badge_Grade)

The advertisement API built on .NET Core 3.1 following the Clean Architecture and CQRS
with [Azure Cognitive Services](https://azure.microsoft.com/en-us/services/cognitive-services/) interaction.

## How to setup

1. Set database connection strings inside of `appsettings.Persistence.json`.
2. Configure connection to the Azure inside of `appsettings.Azure.json`.
3. Configure CORS inside of `appsettings.CORS.json`.
4. Run migrations:

*   `dotnet ef database update --project="src/Persistence.Primary" --startup-project="src/Presentation.API" --context=AdvertoDbContext`
*   `dotnet ef database update --project="src/Persistence.Logging" --startup-project="src/Presentation.API" --context=AdvertoLoggingDbContext`
