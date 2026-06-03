# Retail Management - Clean Architecture WPF

Retail Management is a .NET 8 WPF desktop application for managing products, categories, and stock imports. The solution is organized with a Clean Architecture-inspired structure and includes unit tests for core application services.

## Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Technology Stack](#technology-stack)
- [Solution Structure](#solution-structure)
- [Architecture](#architecture)
- [Prerequisites](#prerequisites)
- [Database Configuration](#database-configuration)
- [Build and Run](#build-and-run)
- [Testing](#testing)
- [Current Notes](#current-notes)
- [Future Improvements](#future-improvements)

## Overview

This project demonstrates a layered WPF application using:

- Domain entities for business data.
- Application interfaces and DTOs for service contracts.
- Infrastructure implementations for Entity Framework Core, repositories, services, and mappings.
- WPF presentation screens for end-user workflows.
- xUnit tests for service-level business logic.

The main user-facing application is `Retail.Presentation`.

## Features

- Display all products in a modern WPF data grid.
- Filter products by category.
- Add new products with initial quantity.
- Import stock for an existing product.
- Record stock import transactions.
- Use dependency injection to wire services, repositories, and database context.
- Validate stock import business rules through unit tests.
- Modernized WPF styling for windows, buttons, inputs, and tables.

## Technology Stack

- .NET 8
- WPF
- Entity Framework Core 8
- SQL Server
- AutoMapper
- Microsoft.Extensions.Hosting
- Microsoft.Extensions.DependencyInjection
- DotNetEnv
- xUnit

## Solution Structure

```text
Homework1.sln
+-- RetailPresentation/
|   +-- WPF application entry point and UI screens
|
+-- RetailApplication/
|   +-- DTOs, service contracts, repository contracts, and factory contracts
|
+-- Retail_Domain/
|   +-- Domain entities and value objects
|
+-- Retail_Infastructure/
|   +-- EF Core DbContext, models, repositories, service implementations, mappings, and DI setup
|
+-- Retail.Tests/
    +-- xUnit tests and test doubles for application services
```

## Architecture

The Retail module follows a layered design:

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

### Layer Responsibilities

- `RetailPresentation`: WPF windows, UI events, and user interaction flow.
- `RetailApplication`: DTOs, interfaces, service contracts, repository contracts, and factory contracts.
- `Retail_Domain`: Domain entities and value objects.
- `Retail_Infastructure`: SQL Server access, EF Core context, repositories, Unit of Work, AutoMapper profiles, service implementations, and dependency injection registration.
- `Retail.Tests`: Unit tests for application service behavior without requiring a live database.

## Prerequisites

Install the following tools before running the project:

- Visual Studio 2022 or later
- .NET 8 SDK
- SQL Server or SQL Server Express
- SQL Server Management Studio, optional but recommended

## Database Configuration

The Retail application loads the SQL Server connection string from:

```text
Retail_Infastructure/config/.env
```

Expected format:

```env
SQLConnectString=Data Source=YOUR_SERVER;Initial Catalog=ManagementRetail;User ID=YOUR_USER;Password=YOUR_PASSWORD;TrustServerCertificate=True;
```

Do not commit real production credentials. For a production-ready application, store secrets in user secrets, environment variables, or a secure secret manager.

## Build and Run

Restore packages:

```powershell
dotnet restore Homework1.sln
```

Build the solution:

```powershell
dotnet build Homework1.sln
```

Run the WPF application from Visual Studio:

1. Open `Homework1.sln`.
2. Set `Retail.Presentation` as the startup project.
3. Ensure the `.env` file contains a valid SQL Server connection string.
4. Start the application.

## Testing

Run all unit tests:

```powershell
dotnet test Homework1.sln
```

The test project currently validates:

- Product creation flow in `ProductService`.
- Product retrieval mapping in `ProductService`.
- Stock import validation in `StockService`.
- Stock import behavior when the product does not exist.
- Successful stock import quantity update and transaction creation.

Expected result:

```text
Passed: 5
Failed: 0
```

## Current Notes

- The solution name is `Homework1.sln`, but the current solution content is focused on the Retail Management application.
- The infrastructure folder is named `Retail_Infastructure`; this appears to be a spelling typo kept to avoid breaking project references.
- Some nullable-reference warnings may still appear during build.
- Database migrations or SQL setup scripts are not currently included.

## Future Improvements

- Add EF Core migrations or SQL scripts for repeatable database setup.
- Move all connection strings and secrets out of committed files.
- Add integration tests for repositories with an isolated test database.
- Expand domain validation and error handling.
- Rename `Retail_Infastructure` to `Retail_Infrastructure` and update references.
- Add screenshots or a short demo guide for the WPF interface.

## Author

PHAM TRUNG DUC
