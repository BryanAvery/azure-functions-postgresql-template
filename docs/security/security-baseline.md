# Security Baseline

## Secrets

- Do not commit secrets in source code or repo settings files.
- Use Key Vault or managed platform secret stores.
- Rotate credentials and keys on a defined schedule.

## Identity and access

- Prefer managed identity for Azure resource access.
- Grant least privilege for database users and service principals.
- Separate read/write identities when practical.

## Input and output

- Validate and sanitize all input.
- Use parameterized SQL only.
- Do not include sensitive data in logs or error responses.

## Configuration

- Keep production configuration externalized.
- Encrypt settings at rest and in transit.
- Document key rotation ownership and cadence.
