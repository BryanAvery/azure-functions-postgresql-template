# Developer Onboarding

## Prerequisites

- .NET 8 SDK
- Azure Functions Core Tools v4
- Docker (optional, for integration dependencies)
- PostgreSQL access for local/integration testing

## First-run setup

1. Copy `src/FunctionApp/local.settings.sample.json` to `src/FunctionApp/local.settings.json`.
2. Update local values (especially `Postgres__ConnectionString`).
3. Restore dependencies: `dotnet restore`.
4. Build solution: `dotnet build --configuration Release`.
5. Run unit tests: `dotnet test tests/UnitTests/UnitTests.csproj --configuration Release`.
6. Run integration tests (optional):
   `INTEGRATION_TEST_POSTGRES_CONNECTION_STRING=... dotnet test tests/IntegrationTests/IntegrationTests.csproj --configuration Release`.

## Run function app locally

`func start --csharp --script-root src/FunctionApp/bin/Debug/net8.0`

## Troubleshooting

- If startup fails with connection errors, verify PostgreSQL host, SSL mode, and credentials.
- If queue triggers fail locally, validate `AzureWebJobsStorage` setting.
