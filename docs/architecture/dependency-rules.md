# Dependency Rules

These rules keep the layered architecture maintainable and prevent circular references.

## Allowed references

- `FunctionApp` (trigger layer) -> `Application`, `Contracts`, `Infrastructure`, `Domain`
- `Application` -> `Domain`, `Contracts`
- `Infrastructure` -> `Application`, `Domain`
- `Domain` -> no project references
- `Contracts` -> no project references

## Review rule

A pull request that introduces a forbidden dependency should be rejected unless an ADR explicitly approves it.
