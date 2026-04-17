# Developer Onboarding

## Prerequisites

- .NET SDK 8.x
- Azure Functions Core Tools v4
- Docker Desktop (recommended)

## Quick start

1. Create local settings:

   ```bash
   ./scripts/bootstrap-local.sh
   ```

2. Start local dependencies:

   ```bash
   docker compose up -d
   ```

3. Build and test:

   ```bash
   dotnet restore
   dotnet build --configuration Release
   dotnet test --configuration Release
   dotnet format --verify-no-changes
   ```

4. Run the function app:

   ```bash
   func start --csharp --script-root src/FunctionApp/bin/Debug/net8.0
   ```

## Local configuration guidance

Update `src/FunctionApp/local.settings.json` values:

- `AzureWebJobsStorage` should point to Azurite locally (`UseDevelopmentStorage=true`)
- `Postgres__ConnectionString` should use your local PostgreSQL credentials

## Integration tests

Integration tests run only when `INTEGRATION_TEST_POSTGRES_CONNECTION_STRING` is set.

```bash
INTEGRATION_TEST_POSTGRES_CONNECTION_STRING="Host=localhost;Port=5432;Database=appdb;Username=app;Password=app" \
  dotnet test tests/IntegrationTests/IntegrationTests.csproj --configuration Release
```

## Troubleshooting

- Connection failure: verify local containers are healthy (`docker compose ps`).
- Functions startup failure: verify `Postgres__ConnectionString` is configured.
- Queue trigger issues: verify `AzureWebJobsStorage` and Azurite availability.
