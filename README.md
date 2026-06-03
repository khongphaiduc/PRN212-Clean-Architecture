# Sample Clean Architecture

WPF desktop solution for PRN212 coursework, built with .NET 8, Entity Framework Core, SQL Server, and a layered Retail module following Clean Architecture principles.

## Overview

This repository contains two WPF applications and one unit test project:

- `Retail.Presentation`: Retail product and stock management UI.
- `Homework1`: Project and employee assignment management UI.
- `Retail.Tests`: Unit tests for Retail application services.

The Retail module is organized into Domain, Application, Infrastructure, Presentation, and Test layers. It demonstrates common enterprise patterns such as DTOs, repositories, Unit of Work, AutoMapper, dependency injection, and service-based business logic.

## Features

### Retail Management

- View product list.
- Filter products by category.
- Add new products.
- Import stock for an existing product.
- Track stock transaction data through service and repository layers.
- Modern WPF interface with shared styling for buttons, inputs, and data grids.

### Project and Employee Management

- View project list.
- View employees assigned to a selected project.
- Add an employee to a project with a role.
- Prevent duplicate employee assignments.
- Modernized WPF dashboard layout.

### Unit Tests

- Tests for `ProductService`.
- Tests for `StockService`.
- Fake repositories and fake Unit of Work for fast service-level testing.
- Tests run without requiring a live SQL Server database.

## Technology Stack

- .NET 8
- WPF
- Entity Framework Core 8
- SQL Server
- AutoMapper
- Microsoft.Extensions.Hosting
- Microsoft Dependency Injection
- DotNetEnv
- xUnit

## Solution Structure

```text
Homework1.sln
+-- Homework1/                 # WPF app for project and employee management
+-- RetailPresentation/         # WPF app for retail management
+-- RetailApplication/          # DTOs, service contracts, repository contracts
+-- Retail_Domain/              # Domain entities and value objects
+-- Retail_Infastructure/       # EF Core context, repositories, services, mappings
+-- Retail.Tests/               # xUnit test project
```

## Architecture

The Retail module follows a layered structure:

```text
Presentation
    |
    v
Application
    |
    v
Domain
    ^
    |
Infrastructure
```

- `RetailPresentation` owns the WPF screens and user interactions.
- `RetailApplication` defines DTOs, interfaces, factories, and service contracts.
- `Retail_Domain` contains business entities and value objects.
- `Retail_Infastructure` implements persistence, repositories, Unit of Work, AutoMapper profiles, and application services.
- `Retail.Tests` validates service behavior without connecting to the database.

## Prerequisites

Install the following before running the project:

- Visual Studio 2022 or later
- .NET 8 SDK
- SQL Server or SQL Server Express
- SQL Server Management Studio, optional but recommended

## Database Configuration

The Retail application reads its SQL Server connection string from:

```text
Retail_Infastructure/config/.env
```

Expected format:

```env
SQLConnectString=Data Source=YOUR_SERVER;Initial Catalog=ManagementRetail;User ID=YOUR_USER;Password=YOUR_PASSWORD;TrustServerCertificate=True;
```

For the `Homework1` project, the current scaffolded `CompanyDbContext` contains a direct SQL Server connection string. For a production-ready setup, move that connection string to configuration or user secrets.

## Build

Restore packages and build the full solution:

```powershell
dotnet restore Homework1.sln
dotnet build Homework1.sln
```

## Run

Open `Homework1.sln` in Visual Studio.

To run the Retail app:

1. Set `Retail.Presentation` as the startup project.
2. Make sure `Retail_Infastructure/config/.env` contains a valid SQL Server connection string.
3. Run the project.

To run the Project and Employee app:

1. Set `Homework1` as the startup project.
2. Make sure the `CompanyDB` database is available.
3. Run the project.

## Test

Run all unit tests:

```powershell
dotnet test Homework1.sln
```

Expected result:

```text
Passed: 5
Failed: 0
```

## Important Notes

- This repository is intended for coursework and learning purposes.
- Do not commit real production credentials.
- The Retail module has a cleaner layered structure than the legacy `Homework1` module.
- Existing build warnings are mostly related to nullable reference types and scaffolded connection string warnings.

## Suggested Improvements

- Move all connection strings to `appsettings.json`, user secrets, or environment variables.
- Add EF Core migrations or SQL scripts for database setup.
- Add integration tests for repositories using an isolated test database.
- Expand validation rules in domain entities and services.
- Refactor the `Homework1` project to use dependency injection consistently.

## Author

PHAM TRUNG DUC
