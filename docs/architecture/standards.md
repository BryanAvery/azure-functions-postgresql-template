# Engineering Standards

## Purpose

This repository contains an Azure Function App built in C# with PostgreSQL as the persistence layer.

These standards exist to keep the codebase readable, secure, testable, maintainable, and operationally supportable.

## 1. Engineering principles

### Keep functions thin

Functions should only handle:

- trigger input
- request validation
- orchestration to services
- response mapping

Business logic should not live inside function trigger classes.

### Separate responsibilities

Use a clear separation of concerns:

- Function layer - trigger bindings and request handling
- Application layer - orchestration and business logic
- Domain layer - business concepts and rules
- Infrastructure layer - data access and external integration

### Prefer clarity over cleverness

Code should be obvious to the next developer reading it.

Avoid:

- hidden behaviour
- excessive abstraction
- overuse of static helpers
- magic strings and silent assumptions

### Build for support

Logging, traceability, and consistent error handling are part of the design, not an afterthought.

## 2. C# coding standards

### Language baseline

- Use the current approved .NET LTS version
- Use Azure Functions isolated worker model for new development
- Enable nullable reference types
- Keep warnings clean
- Avoid obsolete or legacy patterns unless there is a documented reason

### Naming conventions

- PascalCase for classes, methods, properties, enums, and records
- camelCase for parameters and local variables
- _camelCase for private readonly fields

Use descriptive names.

Good examples:

- `CustomerRepository`
- `CreateInvoiceAsync`
- `customerId`
- `_logger`

Avoid vague names such as:

- `DataHelper`
- `DoWork`
- `obj`
- `temp`

### Methods and classes

- A class should have one clear responsibility
- Methods should be small and focused
- Prefer constructor injection
- Async methods must end with `Async`
- Use guard clauses early
- Keep public APIs explicit and easy to understand

### Models

- Use DTOs for request and response contracts
- Keep transport models separate from persistence models where possible
- Use records where immutability makes sense

## 3. Azure Function standards

### Function design

Each function should:

- serve one trigger purpose
- validate incoming input
- call an application service
- return a consistent result
- log meaningful start, success, and failure information

### Dependency injection

Use dependency injection for:

- services
- repositories
- configuration
- database clients
- external clients
- logging and helper components

Do not new up dependencies inside function methods unless there is a very strong reason.

### Configuration

- Store environment-specific values outside code
- Use strongly typed options classes where practical
- Keep secrets in Key Vault or secured pipeline configuration
- Never hardcode secrets, URLs, tenant IDs, or connection strings

### Logging

Use structured logging.

Log:

- correlation identifiers where available
- function name
- business action
- important technical identifiers where appropriate
- duration or outcome where helpful

Do not log:

- passwords
- tokens
- secrets
- connection strings
- sensitive personal data without explicit approval

### Idempotency

Functions that may be retried or triggered more than once should be safe to run again or should detect and reject duplicates.

This matters especially for:

- queue-triggered work
- event processing
- callback handling
- message-based workflows

## 4. PostgreSQL standards

### Data access

Use one consistent access pattern across the repo.

Typical options:

- Dapper for lightweight, explicit SQL
- EF Core where richer object mapping is genuinely useful

Pick one approach intentionally and avoid mixing styles without good reason.

### SQL safety

- Parameterise every query
- Never concatenate user input into SQL
- Keep SQL readable and maintainable
- Avoid `SELECT *`
- Return only the fields you need

### Schema changes

- All schema changes must be version controlled
- Migrations must be reviewed
- Production changes must go through a controlled deployment path

### Performance and indexing

- Add indexes based on real query usage
- Keep transactions short
- Paginate large result sets
- Review slow-running queries
- Avoid accidental N+1 access patterns

### Time handling

Use UTC consistently for persisted timestamps unless there is a very specific documented reason not to.

## 5. API and HTTP contract standards

If the Function App exposes HTTP APIs:

### Contracts

- Use explicit request and response models
- Validate inbound models
- Return consistent error formats
- Version contracts deliberately where needed

### Status codes

- `200` for successful reads and updates
- `201` for successful creation where relevant
- `400` for invalid input
- `401` or `403` for access issues
- `404` for missing resources
- `409` for conflicts
- `500` only for unexpected server errors

## 6. Testing standards

### Unit tests

Use unit tests for:

- business rules
- validators
- mapping logic
- application services

### Integration tests

Use integration tests for:

- repository behaviour
- PostgreSQL access
- wiring and configuration where useful
- function execution paths where practical

### Test principles

- tests must be deterministic
- tests must be readable
- each test should verify one behaviour
- test names should describe the expected behaviour clearly

Example:

- `CreateOrderAsync_ReturnsValidationError_WhenCustomerIdIsMissing`
- `GetInvoiceAsync_ReturnsInvoice_WhenRecordExists`

## 7. Security standards

### Secrets

- Secrets must not be stored in source control
- Use Azure Key Vault or secure pipeline secret storage
- Rotate secrets according to platform policy

### Access control

- Apply least privilege to Azure resources
- Apply least privilege to PostgreSQL roles
- Prefer managed identity where supported

### Data handling

- minimise sensitive data retention
- encrypt data in transit
- avoid logging sensitive fields
- mask values where logging is necessary

### Dependencies

- keep dependencies current
- review security alerts promptly
- fix critical vulnerabilities quickly

## 8. GitHub workflow standards

### Pull requests

Require for merge:

- successful build
- successful tests
- review approval
- no secret leaks
- no critical code scanning failures

### Commits

Commit messages should be clear and meaningful.

Good examples:

- `Add customer repository for PostgreSQL lookup`
- `Introduce validation for invoice creation`
- `Add migration for payment reference index`

Avoid vague messages such as:

- `update`
- `changes`
- `fix stuff`

## 9. Operational readiness

Before production release, ensure:

- logging is in place
- alerts exist
- dashboards exist where needed
- failure paths are understood
- rollback or mitigation steps are known
- connection pooling and scaling have been considered

## 10. Documentation expectations

The repository should explain:

- what the service does
- how to run it locally
- how to configure it
- how to test it
- how database changes are managed
- how deployment works
- what the support considerations are
