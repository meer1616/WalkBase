# WalkBase API

A secure, scalable .NET Web API for managing walking trails, built with JWT-based authentication, authorization, and clean architecture principles.

---

## 🚀 Features

- ✅ JWT Authentication & Authorization
- ✅ Entity Framework Core + Code First Migrations
- ✅ Global Exception Handling via Middleware
- ✅ Input Validation with Custom Filters
- ✅ Modular Architecture with Repositories & Mappings
- ✅ Dockerized for container-based deployment
- ✅ Swagger API Documentation

---

## 🏗️ Project Structure

| Folder / File                 | Purpose                                              |
| ----------------------------- | ---------------------------------------------------- |
| `Controllers/`                | API endpoints for authentication and walk management |
| `Contracts/`                  | DTOs / error response models                         |
| `CustomActionFilters/`        | Custom model validation filters                      |
| `Data/`                       | EF Core DB context files                             |
| `Middleware/`                 | Global error handling middleware                     |
| `Mappings/`                   | AutoMapper profile mappings                          |
| `Models/`                     | Domain models: `Walk`, `Region`, `Difficulty`        |
| `Repositories/`               | Data access logic using Repository Pattern           |
| `Migrations/`                 | EF Core migrations                                   |
| `Program.cs`                  | Main configuration (DI, authentication, middleware)  |
| `.dockerignore`, `Dockerfile` | Docker support                                       |
| `appsettings*.json`           | Configuration files for development & production     |

---

## 🧪 Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server)
- (Optional) Docker

### Setup

```bash
# Clone the repo
git clone https://github.com/meer1616/WalkBase.git
cd WalkBase

# Restore dependencies
dotnet restore

# Apply migrations
dotnet ef database update --project Authentication.csproj

# Run the API
dotnet run
```
