# Naming Conventions

- Projects: `Company.Product.FunctionApp.<Layer>` for expanded multi-project layouts.
- Namespaces mirror folder structure.
- Functions: `<Action><Entity>Function` (e.g., `GetCustomerFunction`).
- Routes: plural resource names (e.g., `/customers/{id}`).
- DTOs: `Request` / `Response` suffix.
- Options classes: `<Resource>Options`.
- Repositories: `<Entity>Repository` with `I<Entity>Repository`.
- Services: `<Entity>Service` with `I<Entity>Service`.
