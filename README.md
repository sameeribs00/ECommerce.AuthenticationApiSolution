# ECommerce Authentication API

A robust, scalable authentication microservice built with .NET 8, designed to handle user registration, login, and JWT-based authentication for e-commerce applications.

## 🚀 Overview

The ECommerce Authentication API is a dedicated microservice that provides secure user authentication and authorization capabilities. It implements industry-standard security practices including password hashing with BCrypt, JWT token generation, and comprehensive user management functionality.

### Key Features

- **User Registration & Login**: Secure user account creation and authentication
- **JWT Token Authentication**: Stateless authentication using JSON Web Tokens
- **Password Security**: BCrypt hashing for secure password storage
- **User Management**: Retrieve users by ID or email address
- **Role-Based Access**: Support for user roles and permissions
- **Clean Architecture**: Separation of concerns with layered architecture
- **Docker Support**: Containerized deployment ready
- **Comprehensive Logging**: Structured logging with Serilog
- **API Documentation**: Swagger/OpenAPI integration

## 🏗️ Architecture

This project follows **Clean Architecture** principles with clear separation of concerns across multiple layers:

### Project Structure

```
ECommerce.AuthenticationApiSolution/
├── AuthenticationApi.Api/              # Presentation Layer (Web API)
│   ├── Controllers/                    # API Controllers
│   ├── Program.cs                      # Application entry point
│   └── appsettings.json               # Configuration
├── AuthenticationApi.Application/      # Application Layer
│   ├── DTOs/                          # Data Transfer Objects
│   ├── Interfaces/                    # Application contracts
│   └── DTOsConversion/                # Entity-DTO mapping
├── AuthenticationApi.Domain/           # Domain Layer
│   └── Entities/                      # Domain entities
├── AuthenticationApi.Infrastructure/   # Infrastructure Layer
│   ├── Data/                          # Database context & migrations
│   ├── Repositories/                  # Data access implementations
│   └── DependencyInjection/           # Service registration
└── ECommerce.AuthenticationApiSolution.sln
```

### Design Patterns & Practices

- **Repository Pattern**: Abstracted data access through `IUser` interface
- **Dependency Injection**: IoC container for loose coupling
- **DTO Pattern**: Clean data transfer between layers
- **Clean Architecture**: Clear separation of business logic from infrastructure
- **Configuration Pattern**: Centralized configuration management
- **Exception Handling**: Comprehensive error handling with logging

## 🛠️ Technology Stack

### Core Technologies
- **.NET 8.0**: Latest LTS version of .NET framework
- **ASP.NET Core Web API**: RESTful API framework
- **Entity Framework Core 8.0**: ORM for database operations
- **SQL Server**: Primary database (configurable)

### Security & Authentication
- **JWT (JSON Web Tokens)**: Stateless authentication
- **BCrypt.Net**: Secure password hashing
- **System.IdentityModel.Tokens.Jwt**: JWT token handling

### Development & Deployment
- **Swagger/OpenAPI**: API documentation and testing
- **Docker**: Containerization support
- **Serilog**: Structured logging
- **Entity Framework Migrations**: Database versioning

### External Dependencies
- **ECommerce.CommonLibrary**: Shared utilities and common functionality

## 📋 Prerequisites

Before running this project, ensure you have the following installed:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (LocalDB or full instance)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)
- [Docker Desktop](https://www.docker.com/products/docker-desktop) (optional, for containerized deployment)

## 🚀 Getting Started

### 1. Clone the Repository

```bash
git clone <repository-url>
cd ECommerce.AuthenticationApiSolution
```

### 2. Database Setup

#### Option A: Using LocalDB (Recommended for Development)
The project is configured to use LocalDB by default. No additional setup required.

#### Option B: Using SQL Server Instance
Update the connection string in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "eCommerceConnection": "Server=YOUR_SERVER; Database=ECommerce.Authentication; Trusted_Connection=true; TrustServerCertificate=true;"
  }
}
```

### 3. Run Database Migrations

```bash
cd AuthenticationApi.Infrastructure
dotnet ef database update --startup-project ../AuthenticationApi.Api
```

### 4. Build and Run

```bash
# Restore dependencies
dotnet restore

# Build the solution
dotnet build

# Run the API
cd AuthenticationApi.Api
dotnet run
```


## 📁 Project Dependencies

### NuGet Packages

- `Microsoft.EntityFrameworkCore.Tools` (8.0.7)
- `Swashbuckle.AspNetCore` (6.6.2)
- `BCrypt.Net-Next` (4.0.3)
- `Microsoft.VisualStudio.Azure.Containers.Tools.Targets` (1.21.0)

### Project References

- `ECommerce.CommonLibrary`: Shared utilities and common functionality

