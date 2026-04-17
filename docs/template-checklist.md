# Post-Template Creation Checklist

Use this as a quick sign-off list for a new repository.

## Repository setup

- [ ] Repository name, description, and topics are updated
- [ ] `README.md` reflects service purpose and API endpoints
- [ ] All `REPLACE_ME` placeholders are removed
- [ ] `LICENSE` copyright owner updated
- [ ] `CODEOWNERS` updated with owning team

## Engineering controls

- [ ] Branch protection enabled on `main`
- [ ] Required checks configured (CI, CodeQL, dependency review)
- [ ] Dependabot enabled (version + security updates)
- [ ] Secret scanning and push protection enabled

## Runtime and deployment

- [ ] Environment variables configured in Azure
- [ ] Secrets moved to Key Vault / secure store
- [ ] Deployment identity has least required permissions
- [ ] Initial migration applied to non-prod database

## Validation

- [ ] `dotnet build --configuration Release` passes
- [ ] `dotnet test --configuration Release` passes
- [ ] Local function run verified (`func start ...`)
- [ ] Health endpoints return expected status
