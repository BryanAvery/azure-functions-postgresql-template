# Observability Baseline

## Correlation

- Use `FunctionContext.InvocationId` as the default correlation identifier.
- Include correlation ID in response envelopes and structured log scope.

## Required structured fields

Every request-level log scope should include:

- `FunctionName`
- `InvocationId`
- key business identifier (e.g., `CustomerId`)

## Always log

- request started
- success outcome
- error outcome with exception metadata

## Never log

- secrets
- connection strings
- tokens
- personal/sensitive payload content unless approved

## Telemetry

Use Application Insights or OpenTelemetry exporters in deployed environments.
Start traces at the function boundary and propagate correlation through downstream calls.
