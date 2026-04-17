# Contributing

## Branching

Create short-lived branches from `main`.

Use clear branch names such as:

- `feature/add-customer-search`
- `bugfix/fix-null-reference-on-order-save`
- `hotfix/handle-duplicate-callback`

## Pull requests

All changes must be submitted through a pull request.

Each pull request must include:

- a clear summary of the change
- why the change is needed
- test evidence
- notes on configuration changes
- notes on database changes if relevant
- any deployment or support considerations

## Coding expectations

Contributors must follow the standards in `docs/architecture/standards.md`.

This includes expectations for:

- Azure Function design
- C# coding practices
- PostgreSQL access
- security
- logging
- testing
- documentation

## Definition of done

A change is only complete when:

- code has been reviewed
- checks have passed
- tests have passed
- documentation is updated where needed
- database changes are included where needed
- configuration impacts are documented
- operational impacts are understood

## Database changes

All schema changes must be version controlled and reviewed through pull requests.

Manual database updates outside the deployment process are not allowed unless explicitly approved and documented.

## Secrets

Never commit:

- passwords
- API keys
- tokens
- certificates
- connection strings
- local settings containing secrets

## Quality expectations

Please keep changes focused and readable.

Avoid:

- large unrelated PRs
- mixed refactoring and feature changes without explanation
- hidden breaking changes
- untested business logic changes
