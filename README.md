# C# Azure Functions + PostgreSQL Template

Production-ready starter template for building **.NET 8 Azure Functions (isolated worker)** services backed by **PostgreSQL**.

This repository is intentionally structured to be reusable as a GitHub template for internal services.

## What this template gives you

- Azure Functions isolated worker app with DI-first composition
- PostgreSQL data access with Dapper and parameterized SQL
- Unit and integration test projects
- Baseline architecture, naming, and response conventions docs
- GitHub Actions CI, CodeQL, dependency review, and Dependabot config
- Governance files for open-source/compliance readiness
- Template customization guide and post-template checklist

## Repository structure

```text
src/FunctionApp/                  # Function app source
tests/UnitTests/                  # Fast, isolated tests
tests/IntegrationTests/           # DB connectivity/integration tests
database/migrations/              # SQL migrations (version controlled)
database/scripts/                 # Operational SQL scripts
docs/                             # Engineering, security, and onboarding docs
.github/                          # CI workflows, templates, automations
.devcontainer/                    # Optional reproducible dev environment
```

## Getting started (new developer)

### 1) Prerequisites

- .NET SDK 8.x
- Azure Functions Core Tools v4
- Docker Desktop (recommended for local PostgreSQL + Azurite)

### 2) Bootstrap local settings

```bash
cp src/FunctionApp/local.settings.sample.json src/FunctionApp/local.settings.json
```

Update placeholder values in `src/FunctionApp/local.settings.json`.

### 3) Start local dependencies (optional but recommended)

```bash
docker compose up -d
```

This starts:

- PostgreSQL on `localhost:5432`
- Azurite storage emulator on `localhost:10000`

### 4) Build, test, and run

```bash
dotnet restore
dotnet build --configuration Release
dotnet test --configuration Release
func start --csharp --script-root src/FunctionApp/bin/Debug/net8.0
```

## Required template customization

Before using this as a real service, complete:

1. `docs/template-customization.md`
2. `docs/template-checklist.md`

Replace all `[REPLACE_ME_*]` placeholders.

## Configuration

Use environment variables / Function values only. Never commit secrets.

Key settings:

- `Postgres__ConnectionString`
- `AzureWebJobsStorage`
- `FUNCTIONS_WORKER_RUNTIME` (`dotnet-isolated`)

See safe examples in:

- `src/FunctionApp/local.settings.sample.json`
- `.env.sample`

## Quality gates

Pull requests run:

- restore/build/test
- formatting verification (`dotnet format --verify-no-changes`)
- dependency vulnerability checks (Dependabot + dependency review)
- CodeQL static analysis

## Security expectations

- No secrets in git history
- Parameterized SQL only
- Least privilege DB and cloud identities
- Structured logging without sensitive payloads

See `SECURITY.md` and `docs/security/security-baseline.md`.

## License

This template is licensed under the MIT License. Update the copyright holder in `LICENSE`.
