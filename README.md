# NzProducts.Management

A **.NET product management system** built to demonstrate clean architecture, dependency injection, and the repository pattern — with multi-currency display support.

---

## What It Does

- Manages a catalogue of NZ products across multiple types
- Displays products in the user's selected currency (multi-currency support)
- Built with production-grade architecture patterns from day one

---

## Architecture

The solution is structured as a layered, clean architecture — each project has a single responsibility:

```
NzProducts.Management/
├── NzProducts.Common.Contracts        # Interfaces & shared contracts (abstractions only)
├── NzProducts.Business                # Business logic layer — rules, transformations
├── NzProducts.Configuration.Management # App configuration, settings management
├── NzProducts.Ioc                     # Autofac DI container — all bindings in one place
├── NzProducts.Start                   # Entry point — bootstraps the application
├── NzProducts.Tests.Common            # Shared test utilities and base classes
├── RefactorMe                         # Legacy code (before refactor)
├── RefactorMe.DontRefactor            # Intentionally untouched legacy reference
└── RefactorMe.Tests                   # Tests covering the refactored implementation
```

**Key design decisions:**
- All dependencies flow inward — business logic has zero knowledge of infrastructure
- Contracts layer ensures every dependency is against an abstraction, never a concrete
- IoC wiring is isolated to `NzProducts.Ioc` — swap containers without touching business code

---

## Skills Demonstrated

| Skill | How |
|---|---|
| **Clean Architecture** | Strict layer separation — Contracts → Business → IoC → Start |
| **Repository Pattern** | Products accessed through abstracted repository interfaces |
| **Dependency Injection** | Autofac container wired in dedicated IoC project |
| **C# / .NET** | 100% C#, idiomatic .NET patterns throughout |
| **Unit Testing** | Shared test base, dedicated test projects per layer |
| **Refactoring** | RefactorMe projects show before/after of a real refactor exercise |
| **Configuration Management** | Dedicated project for settings, environment-aware config |
| **Multi-currency Support** | Runtime currency selection driving product display logic |

---

## Tech Stack

- **Language:** C# / .NET
- **DI Container:** Autofac
- **Pattern:** Repository Pattern, Clean Architecture
- **Testing:** xUnit (common test utilities in `NzProducts.Tests.Common`)

---

## Getting Started

```bash
# Clone
git clone https://github.com/sri-dosapati/NzProducts.Management.git
cd NzProducts.Management

# Open in Visual Studio or Rider
start RefactorMe.sln

# Build
dotnet build

# Run tests
dotnet test
```

---

## The RefactorMe Layer

The `RefactorMe` / `RefactorMe.DontRefactor` pair is intentional — it captures a real refactoring exercise showing:
- What legacy tightly-coupled code looks like
- How clean separation and testability improve it
- Test coverage before and after as validation

This is the kind of work done in production: identify debt, refactor safely, prove it with tests.

---

## Author

**Sri Dosapati** — Full Stack .NET Developer, Auckland NZ  
[LinkedIn](https://www.linkedin.com/in/itsme406/) · [GitHub](https://github.com/sri-dosapati) · [Portfolio](https://sri-dosapati.vercel.app)
