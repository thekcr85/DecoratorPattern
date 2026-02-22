# DecoratorPattern 🎨

> Clean Architecture demonstration using Decorator Pattern with Minimal API in .NET 10

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-14.0-239120)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![Minimal API](https://img.shields.io/badge/Minimal_API-Enabled-512BD4)](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis)
[![Scrutor](https://img.shields.io/badge/Scrutor-7.0.0-blue)](https://github.com/khellang/Scrutor)

## What This Does

A .NET 10 Minimal API demonstrating the Decorator design pattern with clean architecture:

- 🎨 **Decorator Pattern** for cross-cutting concerns (logging)
- 🚀 **Dynamic Endpoint Registration** using reflection
- 📦 **Scrutor** for service decoration in DI container
- 🏗️ **Clean Architecture** with proper layer separation
- ⚡ **Minimal API** for lightweight HTTP endpoints

## Tech Stack

```
.NET 10 + C# 14
Minimal API
Scrutor (Service Decoration)
Clean Architecture
Dynamic Endpoint Mapping
```

## Quick Start

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

### Run Locally

```bash
# 1. Clone
git clone https://github.com/thekcr85/DecoratorPattern.git
cd DecoratorPattern

# 2. Run
cd src/DecoratorPattern.API
dotnet run

# 3. Test endpoint
curl http://localhost:5110/users/11111111-1111-1111-1111-111111111111
```

### Test with .http File

Open `src/DecoratorPattern.API/DecoratorPattern.API.http` in Visual Studio or VS Code with REST Client extension:

```http
GET http://localhost:5110/users/11111111-1111-1111-1111-111111111111
Accept: application/json
```

## Project Structure

```
src/
├── DecoratorPattern.Domain/              # 🎯 Core (Entities)
│   └── Entities/
│       └── User.cs                       # User record
│
├── DecoratorPattern.Application/         # 💼 Business Logic
│   ├── Interfaces/
│   │   ├── IUserRepository.cs            # Repository contract
│   │   └── IUserService.cs               # Service contract
│   ├── Services/
│   │   └── UserService.cs                # Core user service
│   ├── Decorators/
│   │   └── LoggingUserServiceDecorator.cs # Logging decorator 📝
│   └── Extensions/
│       └── ApplicationServiceExtensions.cs # DI registration
│
├── DecoratorPattern.Infrastructure/      # 🔧 Data Access
│   ├── Repositories/
│   │   └── UserRepository.cs             # In-memory user data
│   └── Extensions/
│       └── InfrastructureServiceExtensions.cs # DI registration
│
└── DecoratorPattern.API/                 # 🎨 Web API
    ├── Endpoints/
    │   ├── IEndpoint.cs                  # Endpoint abstraction
    │   └── UserEndpoints.cs              # User API endpoints
    ├── Extensions/
    │   └── EndpointExtensions.cs         # Dynamic endpoint registration
    └── Program.cs                        # Application entry point
```

**Clean Architecture** - dependencies flow inward (API → Infrastructure → Application → Domain)

## How Decorator Pattern Works

```
HTTP Request: GET /users/{id}
         ↓
┌─────────────────────────────────────┐
│ LoggingUserServiceDecorator         │
├─────────────────────────────────────┤
│ 1. Log: "Fetching user with ID..."  │
│ 2. Call wrapped UserService         │
│ 3. Log: "Successfully fetched..."   │
│    or "User not found..."           │
└─────────────────────────────────────┘
         ↓
┌─────────────────────────────────────┐
│ UserService (Core)                  │
├─────────────────────────────────────┤
│ Call UserRepository.GetByIdAsync    │
└─────────────────────────────────────┘
         ↓
┌─────────────────────────────────────┐
│ UserRepository                      │
├─────────────────────────────────────┤
│ Return user from in-memory list     │
└─────────────────────────────────────┘
```

### Service Registration with Scrutor

```csharp
// In ApplicationServiceExtensions.cs
services.AddScoped<IUserService, UserService>();
services.Decorate<IUserService, LoggingUserServiceDecorator>(); // 🎨 Magic happens here!
```

Scrutor wraps `UserService` with `LoggingUserServiceDecorator` transparently!

## Endpoints

| Method | Route | Description |
|--------|-------|-------------|
| `GET` | `/users/{id:guid}` | Get user by ID |

### Sample Requests

```bash
# Get Alice Smith
curl http://localhost:5110/users/11111111-1111-1111-1111-111111111111

# Get Bob Jones
curl http://localhost:5110/users/22222222-2222-2222-2222-222222222222

# Non-existent user (returns 404)
curl http://localhost:5110/users/00000000-0000-0000-0000-000000000000
```

### Sample Responses

**Success (200 OK):**
```json
{
  "id": "11111111-1111-1111-1111-111111111111",
  "name": "Alice Smith",
  "email": "alice@example.com"
}
```

**Not Found (404):**
```json
```

## Dynamic Endpoint Registration

Endpoints are automatically discovered and registered using reflection:

```csharp
// In EndpointExtensions.cs
public static IServiceCollection AddEndpoints(this IServiceCollection services)
{
    var endpointTypes = Assembly.GetExecutingAssembly()
        .GetTypes()
        .Where(t => typeof(IEndpoint).IsAssignableFrom(t) && 
                    t is { IsInterface: false, IsAbstract: false });

    foreach (var type in endpointTypes)
        services.AddScoped(typeof(IEndpoint), type);

    return services;
}
```

Just implement `IEndpoint` interface - no manual registration needed! ✨

## NuGet Packages

### DecoratorPattern.API
```xml
<PackageReference Include="Microsoft.AspNetCore.OpenApi" Version="10.0.3" />
<PackageReference Include="Scrutor" Version="7.0.0" />
```

### DecoratorPattern.Application
```xml
<PackageReference Include="Microsoft.Extensions.Logging.Abstractions" Version="10.0.3" />
<PackageReference Include="Scrutor" Version="7.0.0" />
```

**All packages are compatible with .NET 10!**

## Development Commands

```bash
# Build solution
dotnet build

# Restore packages
dotnet restore

# Run API
cd src/DecoratorPattern.API
dotnet run

# Clean build artifacts
dotnet clean

# Run with watch mode (hot reload)
cd src/DecoratorPattern.API
dotnet watch run
```

## Architecture Highlights

✅ **Clean Architecture** - testable, maintainable, layer separation  
✅ **Decorator Pattern** - cross-cutting concerns without tight coupling  
✅ **Scrutor Integration** - seamless service decoration in DI  
✅ **Dynamic Endpoint Registration** - reflection-based auto-discovery  
✅ **Minimal API** - lightweight, performance-focused HTTP APIs  
✅ **C# 14 Features** - primary constructors, records, pattern matching  

## Design Patterns Used

### 1. Decorator Pattern 🎨
**Problem**: Need to add logging to `UserService` without modifying its code  
**Solution**: `LoggingUserServiceDecorator` wraps the original service

```csharp
public class LoggingUserServiceDecorator(
    IUserService userService,  // Wrapped service
    ILogger<LoggingUserServiceDecorator> logger) : IUserService
{
    public async Task<User?> GetUserAsync(Guid id, CancellationToken cancellationToken)
    {
        logger.LogInformation("Fetching user with ID: {UserId}", id);
        var user = await userService.GetUserAsync(id, cancellationToken); // Delegate to wrapped service
        // ... log result
        return user;
    }
}
```

### 2. Repository Pattern 📦
**Problem**: Decouple data access from business logic  
**Solution**: `IUserRepository` interface with `UserRepository` implementation

### 3. Extension Method Pattern 🔧
**Problem**: Organize DI registration cleanly  
**Solution**: `AddApplicationServices()`, `AddInfrastructureServices()`, `AddEndpoints()`

## Logging Output

When you make a request, you'll see logs like:

```
info: DecoratorPattern.Application.Decorators.LoggingUserServiceDecorator[0]
      Fetching user with ID: 11111111-1111-1111-1111-111111111111
info: DecoratorPattern.Application.Decorators.LoggingUserServiceDecorator[0]
      Successfully fetched user: Alice Smith (alice@example.com)
```

Or for non-existent user:

```
info: DecoratorPattern.Application.Decorators.LoggingUserServiceDecorator[0]
      Fetching user with ID: 00000000-0000-0000-0000-000000000000
warn: DecoratorPattern.Application.Decorators.LoggingUserServiceDecorator[0]
      User with ID: 00000000-0000-0000-0000-000000000000 was not found
```

## Why Decorator Pattern?

### ❌ Without Decorator (Tightly Coupled)
```csharp
public class UserService(IUserRepository repo, ILogger logger)
{
    public async Task<User?> GetUserAsync(Guid id)
    {
        logger.LogInformation("Fetching..."); // Logging mixed with business logic ❌
        var user = await repo.GetByIdAsync(id);
        logger.LogInformation("Fetched...");
        return user;
    }
}
```

### ✅ With Decorator (Separation of Concerns)
```csharp
// UserService - pure business logic
public class UserService(IUserRepository repo)
{
    public Task<User?> GetUserAsync(Guid id) 
        => repo.GetByIdAsync(id); // ✅ Clean!
}

// LoggingUserServiceDecorator - cross-cutting concern
public class LoggingUserServiceDecorator(IUserService service, ILogger logger)
{
    public async Task<User?> GetUserAsync(Guid id)
    {
        logger.LogInformation("Fetching...");
        var user = await service.GetUserAsync(id); // ✅ Delegating
        logger.LogInformation("Fetched...");
        return user;
    }
}
```

**Benefits:**
- ✅ Single Responsibility Principle
- ✅ Open/Closed Principle (extend without modifying)
- ✅ Easy to add more decorators (caching, validation, etc.)
- ✅ Easy to test in isolation

## Extending with More Decorators

You can easily add more decorators:

```csharp
// Register multiple decorators
services.AddScoped<IUserService, UserService>();
services.Decorate<IUserService, LoggingUserServiceDecorator>();
services.Decorate<IUserService, CachingUserServiceDecorator>();    // Caching layer
services.Decorate<IUserService, ValidationUserServiceDecorator>(); // Validation layer

// Execution order: Validation → Caching → Logging → UserService
```

Each decorator wraps the previous one - like Russian nesting dolls! 🪆

## Author

**Michał Bąkiewicz** • [GitHub](https://github.com/thekcr85)

Demonstration project showcasing:
- **Decorator Pattern** for cross-cutting concerns
- **Clean Architecture** with proper layer separation
- **Scrutor** for declarative service decoration
- **Dynamic Endpoint Registration** using reflection
- **Minimal API** in .NET 10 with C# 14

**Project Repository**: [github.com/thekcr85/DecoratorPattern](https://github.com/thekcr85/DecoratorPattern)

---

## Key Takeaways

1. 🎨 **Decorator Pattern** separates cross-cutting concerns from business logic
2. 📦 **Scrutor** makes service decoration trivial in ASP.NET Core
3. 🏗️ **Clean Architecture** keeps your code testable and maintainable
4. ⚡ **Minimal API** reduces boilerplate in .NET 10
5. 🔍 **Reflection-based registration** eliminates manual endpoint mapping

---

## License

MIT License - Educational demonstration project

---

**Get Started:** `dotnet run` 🚀
