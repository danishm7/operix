# Core Entities

## Purpose

Defines the business entities, their attributes, and relationships for Operix.

---

## Organization

| Column    | Type        | Required | Description                                  |
| --------- | ----------- | -------- | -------------------------------------------- |
| id        | int         | Yes      | Primary key                                  |
| name      | string(200) | Yes      | Organization name                            |
| code      | string(50)  | Yes      | Unique organization code                     |
| is_active | bool        | Yes      | Indicates whether the organization is active |

### Relationships

| Entity     | Type        |
| ---------- | ----------- |
| Department | One-to-Many |
| Location   | One-to-Many |
| User       | One-to-Many |
| Asset      | One-to-Many |

---

## Location

| Column             | Type        | Required | Description                                  |
| ------------------ | ----------- | :------: | -------------------------------------------- |
| id                 | int         |   Yes    | Primary key                                  |
| organization_id    | int         |   Yes    | Reference to Organization                    |
| parent_location_id | int         |    No    | Parent location                              |
| name               | string(200) |   Yes    | Location name                                |
| code               | string(50)  |   Yes    | Unique location code within the organization |
| is_active          | bool        |   Yes    | Indicates whether the location is active     |

### Relationships

| Entity       | Type                    |
| ------------ | ----------------------- |
| Organization | Many-to-One             |
| Location     | One-to-Many (Hierarchy) |
| Asset        | One-to-Many             |

---

## Department

| Column               | Type        | Required | Description                                    |
| -------------------- | ----------- | :------: | ---------------------------------------------- |
| id                   | int         |   Yes    | Primary key                                    |
| organization_id      | int         |   Yes    | Reference to Organization                      |
| parent_department_id | int         |    No    | Parent department                              |
| name                 | string(200) |   Yes    | Department name                                |
| code                 | string(50)  |   Yes    | Unique department code within the organization |
| is_active            | bool        |   Yes    | Indicates whether the department is active     |

### Relationships

| Entity       | Type                    |
| ------------ | ----------------------- |
| Organization | Many-to-One             |
| Department   | One-to-Many (Hierarchy) |
| User         | One-to-Many             |
| Asset        | One-to-Many             |

---

## User

| Column          | Type        | Required | Description                           |
| --------------- | ----------- | :------: | ------------------------------------- |
| id              | int         |   Yes    | Primary key                           |
| organization_id | int         |   Yes    | Reference to Organization             |
| department_id   | int         |   Yes    | Reference to Department               |
| first_name      | string(100) |   Yes    | First name                            |
| last_name       | string(100) |    No    | Last name                             |
| email           | string(255) |   Yes    | Login email(unique within the system) |
| password_hash   | string      |   Yes    | Password hash                         |
| is_active       | bool        |   Yes    | Indicates whether the user is active  |

### Relationships

| Entity       | Type                      |
| ------------ | ------------------------- |
| Organization | Many-to-One               |
| Department   | Many-to-One               |
| Role         | Many-to-Many              |
| Work Order   | One-to-Many (Assigned To) |
| Audit Log    | One-to-Many               |

---

## Role

| Column          | Type        | Required | Description                          |
| --------------- | ----------- | :------: | ------------------------------------ |
| id              | int         |   Yes    | Primary key                          |
| organization_id | int         |   Yes    | Reference to Organization            |
| name            | string(100) |   Yes    | Role name                            |
| description     | string(500) |    No    | Role description                     |
| is_active       | bool        |   Yes    | Indicates whether the role is active |

### Relationships

| Entity       | Type         |
| ------------ | ------------ |
| Organization | Many-to-One  |
| User         | Many-to-Many |
| Permission   | Many-to-Many |

---

## Permission

| Column      | Type        | Required | Description                                |
| ----------- | ----------- | :------: | ------------------------------------------ |
| id          | int         |   Yes    | Primary key                                |
| name        | string(100) |   Yes    | Permission name                            |
| code        | string(100) |   Yes    | Unique permission code                     |
| description | string(500) |    No    | Permission description                     |
| is_active   | bool        |   Yes    | Indicates whether the permission is active |

### Relationships

| Entity | Type         |
| ------ | ------------ |
| Role   | Many-to-Many |

---

## User Role

| Column  | Type | Required | Description       |
| ------- | ---- | :------: | ----------------- |
| user_id | int  |   Yes    | Reference to User |
| role_id | int  |   Yes    | Reference to Role |

### Relationships

| Entity | Type        |
| ------ | ----------- |
| User   | Many-to-One |
| Role   | Many-to-One |

---

### Role Permission

| Column        | Type | Required | Description             |
| ------------- | ---- | :------: | ----------------------- |
| role_id       | int  |   Yes    | Reference to Role       |
| permission_id | int  |   Yes    | Reference to Permission |

### Relationships

| Entity     | Type        |
| ---------- | ----------- |
| Role       | Many-to-One |
| Permission | Many-to-One |

