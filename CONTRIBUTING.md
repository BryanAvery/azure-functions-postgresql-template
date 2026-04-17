# Contributing

Thanks for contributing.

## Development flow

1. Create a short-lived branch from `main`.
2. Make focused changes (one concern per pull request).
3. Run local checks before pushing.
4. Open a pull request using the template.

## Branch naming

Use one of the following patterns:

- `feature/<short-description>`
- `fix/<short-description>`
- `chore/<short-description>`
- `docs/<short-description>`

## Local quality checks

Run these commands from repo root:

```bash
dotnet restore
dotnet build --configuration Release
dotnet test --configuration Release
dotnet format --verify-no-changes
```

If integration tests are required:

```bash
INTEGRATION_TEST_POSTGRES_CONNECTION_STRING="Host=localhost;Port=5432;Database=appdb;Username=app;Password=app" \
  dotnet test tests/IntegrationTests/IntegrationTests.csproj --configuration Release
```

## Pull request expectations

Every PR should include:

- clear summary and motivation
- test evidence (commands + outcomes)
- documentation updates where needed
- notes on migration/configuration/operational impact

## Standards

Contributors must follow:

- `docs/architecture/standards.md`
- `docs/architecture/dependency-rules.md`
- `docs/security/security-baseline.md`

## Definition of done

A change is done when:

- code review is complete
- CI passes
- tests are updated and passing
- docs are updated
- migrations/config changes are included and explained
- no secrets are committed
