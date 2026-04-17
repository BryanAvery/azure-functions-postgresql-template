# Template Customization Guide

Use this guide immediately after creating a new repository from this template.

## 1) Rename identifiers

Update these values first:

- solution filename (`azure-function-postgres-starter.sln`)
- project name (`src/FunctionApp/FunctionApp.csproj`)
- root namespace (`FunctionApp`)
- function names/routes to fit your service domain

## 2) Replace placeholders

Search for `REPLACE_ME` and update all values, especially:

- `LICENSE` copyright owner
- contacts in `SECURITY.md`, `SUPPORT.md`, `CODE_OF_CONDUCT.md`
- `.env.sample` metadata values

Recommended command:

```bash
rg "REPLACE_ME"
```

## 3) Configure GitHub

- Enable branch protection on `main`
- Require pull request review(s)
- Require CI, CodeQL, and dependency review checks
- Enable Dependabot alerts + security updates
- Enable secret scanning and push protection (if available)

## 4) Configure Azure + runtime settings

- provision Function App and PostgreSQL resources
- move secrets to Key Vault and/or GitHub environment secrets
- set production `Postgres__ConnectionString`
- set production `AzureWebJobsStorage`

## 5) Establish team ownership

- update `CODEOWNERS`
- update support/security escalation aliases
- define on-call runbook links in docs

## 6) Database baseline

- add initial migration scripts under `database/migrations`
- define migration execution in your deployment pipeline
- verify DB role permissions follow least privilege
