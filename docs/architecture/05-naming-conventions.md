# Naming Conventions

## Purpose

Defines the naming conventions used across Operix to ensure consistency and maintainability.

## Database

| Item            | Convention            | Example                         |
| --------------- | --------------------- | ------------------------------- |
| Schemas         | lowercase             | `cmms`, `security`, `audit`     |
| Tables          | Singular, snake_case  | `organization`, `work_order`    |
| Columns         | snake_case            | `created_on`, `organization_id` |
| Primary Key     | `id`                  | `id`                            |
| Foreign Keys    | `<entity_name>_id`    | `organization_id`, `asset_id`   |
| Junction Tables | Combined entity names | `user_role`, `role_permission`  |

## C# Mapping

| C#               | Database          |
| ---------------- | ----------------- |
| `Organization`   | `organization`    |
| `WorkOrder`      | `work_order`      |
| `OrganizationId` | `organization_id` |
| `CreatedOn`      | `created_on`      |

## Notes

- Use **PascalCase** in C#.
- Use **snake_case** in PostgreSQL.
- Keep names descriptive and consistent.
