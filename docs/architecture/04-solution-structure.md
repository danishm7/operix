# Solution Structure

## Overview

Operix is organized into separate frontend, backend, database, and documentation directories. The backend follows the Clean Architecture pattern to keep business logic independent from infrastructure.

---

## Repository Structure

```text
operix/
│
├── backend/
│   ├── Operix.sln
│   ├── src/
│   │   ├── Operix.Api
│   │   │   ├── Controllers
│   │   │   ├── Middlewares
│   │   │   ├── Authorization
│   │   │   ├── Filters
│   │   │
│   │   ├── Operix.Application
│   │   │   ├── Features
│   │   │   │     ├── Organizations
│   │   │   │     │     ├── OrganizationDtos
│   │   │   │     │     ├── OrganizationValidators
│   │   │   │     │     ├── OrganizationService
│   │   │   │     │
│   │   │   │     ├── Departments
│   │   │   │     ├── Users
│   │   │   │     ├── Roles
│   │   │   │
│   │   │   ├── Interfaces
│   │   │   │   ├── Persistence
│   │   │   │
│   │   │   ├── Authentication
│   │   │
│   │   ├── Operix.Domain
│   │   │   ├── Common
│   │   │   ├── Entities
│   │   │   ├── Enums
│   │   │
│   │   └── Operix.Infrastructure
│   │       ├── Data
│   │       │    ├── OperixDbContext.cs
│   │       │    ├── Configurations
│   │       │    ├── Interceptors
│   │       │    ├── Seed
│   │       │
│   │       ├── Extensions
│   │       ├── Migrations
│   │       ├── Repositories
│   │       ├── Services
│   │
│   └── tests/
│
├── frontend/
│   └── operix-web/
│
├── docs/
│   ├── architecture
│   ├── database
│   ├── roadmap
│   ├── ui-ux
│
├── docker/
├── scripts/
└── .github/
```

---

## Backend Projects

| Project                   | Responsibility                            |
| ------------------------- | ----------------------------------------- |
| **Operix.Api**            | API endpoints, middleware, authentication |
| **Operix.Application**    | Use cases, services, DTOs, validation     |
| **Operix.Domain**         | Entities, business rules, interfaces      |
| **Operix.Infrastructure** | Database, repositories, external services |

---

## Project Dependencies

```text
Operix.Api
      │
      ▼
Operix.Application
      │
      ▼
Operix.Domain
      ▲
      │
Operix.Infrastructure
```

---

## Naming Conventions

- Projects: `Operix.<ProjectName>`
- Namespaces: `Operix.<ProjectName>`
- One responsibility per project
- Keep dependencies pointing toward the Domain
