# Security Policy

## Supported versions

This repository is a template. Teams who create services from this template own patching and security updates in their service repositories.

For this template repository itself, only the `main` branch is supported.

## Reporting a vulnerability

Please do **not** open public GitHub issues for suspected vulnerabilities.

Report privately to: **[REPLACE_ME_SECURITY_CONTACT]**.

Include:

- affected repository and commit/branch
- impact summary
- reproduction steps or proof of concept
- suggested remediation (if known)

We will acknowledge receipt within **2 business days** and provide a status update as triage progresses.

## Security baseline for generated services

Repositories created from this template should:

- keep dependencies updated (Dependabot enabled)
- enable GitHub Advanced Security features when available
- enforce pull request reviews and required status checks
- store secrets in Azure Key Vault or GitHub Actions secrets (never in source)
- run tests and static analysis on every pull request

See `docs/security/security-baseline.md` for implementation guidance.
