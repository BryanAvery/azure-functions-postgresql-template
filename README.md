# Azure Function App - Engineering Standards Starter

This repository contains an Azure Function App built in C# with PostgreSQL.

The goal of this repo is to keep the solution simple, secure, maintainable, and easy to support in production.

## Core working rules

- Keep Azure Functions thin
- Put business logic in services, not trigger handlers
- Use dependency injection throughout
- Use structured logging
- Parameterise all SQL
- Version control all database changes
- Never commit secrets
- Require pull request review
- Keep tests aligned with code changes
- Document config, database, and operational impacts

## Architecture and standards docs

- Engineering standards: `docs/architecture/standards.md`
- Dependency rules: `docs/architecture/dependency-rules.md`
- Response conventions: `docs/architecture/response-conventions.md`
- Naming conventions: `docs/architecture/naming-conventions.md`
- ADRs: `docs/architecture/adr/`
- Observability baseline: `docs/operations/observability.md`
- Security baseline: `docs/security/security-baseline.md`
- Developer onboarding: `docs/development/onboarding.md`

## Technology expectations

- .NET LTS
- Azure Functions isolated worker model
- PostgreSQL with Dapper
- Nullable reference types enabled
- Central NuGet package management
- Build and test validation in GitHub Actions
- Security scanning via CodeQL

## Sample capabilities in this starter

- HTTP GET endpoint: `GET /customers/{customerId}`
- HTTP POST endpoint: `POST /customers`
- Request validation in the service layer
- Standard success and problem response contracts
- Database connection abstraction via `IDbConnectionFactory`
- Queue trigger example: `CustomerSyncQueue`

## Local development quick start

1. Copy `src/FunctionApp/local.settings.sample.json` to `src/FunctionApp/local.settings.json`.
2. Set connection strings and local values.
3. Run:

```bash
dotnet restore
dotnet build --configuration Release
dotnet test tests/UnitTests/UnitTests.csproj --configuration Release
```

For full setup details, see `docs/development/onboarding.md`.

## CI quality gates

The `build.yml` workflow separates:

- build
- unit tests + coverage artifact
- integration tests + coverage artifact

This keeps feedback focused and allows teams to tune branch protection per job.

## Security

- Never commit secrets, passwords, tokens, or connection strings
- Use Azure Key Vault or pipeline-managed secrets
- Use least privilege access to Azure and PostgreSQL
- Do not log secrets or sensitive personal data
