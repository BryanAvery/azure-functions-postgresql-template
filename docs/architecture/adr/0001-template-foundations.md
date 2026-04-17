# ADR 0001: Template Foundations

## Status
Accepted

## Context
The template is intended for multi-team Azure Functions services with PostgreSQL persistence.

## Decisions
- Use Azure Functions isolated worker model for .NET workloads.
- Use Dapper for SQL-first, explicit data access.
- Use PostgreSQL as the default relational store.
- Use a layered structure (Function/Application/Domain/Infrastructure).
- Treat warnings as errors in CI where possible.

## Consequences
- Teams get a consistent baseline with explicit boundaries.
- SQL remains transparent and reviewable.
- More mapping/boilerplate is expected than ORM-heavy stacks.
