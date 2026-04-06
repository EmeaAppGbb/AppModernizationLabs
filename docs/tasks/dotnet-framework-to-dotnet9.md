# .NET Framework to .NET 9 Modernization

## Overview
- **Category:** Code Modernization
- **Priority:** P1
- **Languages:** C#/.NET Framework 4.8 → C#/.NET 9
- **Repository Name:** appmodlab-dotnet-framework-to-dotnet9
- **Organization:** EmeaAppGbb

## Objective
This lab guides participants through upgrading a legacy ASP.NET MVC 5 application running on .NET Framework 4.8 to ASP.NET Core on .NET 9. It covers the migration of controllers, views, Entity Framework 6 to EF Core, authentication from OWIN/Katana to ASP.NET Core Identity, and adoption of modern async/await patterns and dependency injection. This is one of the most common modernization scenarios facing enterprises with large .NET portfolios.

## Demo Legacy Application
**Business Domain:** University student enrollment and course management system for "Northwind Technical University"

The legacy application is a full-featured ASP.NET MVC 5 web application that manages student registration, course enrollment, grade tracking, and faculty administration.

### Tech Stack
- ASP.NET MVC 5.2 on .NET Framework 4.8
- Entity Framework 6.4 with Database-First approach (EDMX model)
- OWIN/Katana middleware for authentication (cookie-based, ASP.NET Identity 2.0)
- Unity IoC container for dependency injection
- AutoMapper 6.x for object mapping (pre-static API)
- jQuery 2.x + Bootstrap 3 for frontend
- SQL Server 2016 LocalDB for development
- NLog for logging (configured via web.config)
- Bundling and minification via System.Web.Optimization

### Key Files/Folders Structure
```
NorthwindTechUniversity/
├── NorthwindTechUniversity.sln
├── NorthwindTechUniversity.Web/
│   ├── Web.config                    # Connection strings, app settings, OWIN config
│   ├── Global.asax.cs                # Application startup, route registration
│   ├── Startup.cs                    # OWIN startup (partial class)
│   ├── App_Start/
│   │   ├── RouteConfig.cs            # MVC route table
│   │   ├── BundleConfig.cs           # CSS/JS bundles
│   │   ├── FilterConfig.cs           # Global action filters
│   │   └── UnityConfig.cs            # Unity DI container setup
│   ├── Controllers/
│   │   ├── StudentController.cs      # Student CRUD operations
│   │   ├── CourseController.cs       # Course management
│   │   ├── EnrollmentController.cs   # Enrollment workflows
│   │   ├── GradeController.cs        # Grade entry and reporting
│   │   ├── FacultyController.cs      # Faculty admin
│   │   └── AccountController.cs      # Login/register (Identity 2.0)
│   ├── Models/
│   │   ├── NorthwindTechModel.edmx   # EF6 Database-First model
│   │   ├── ViewModels/               # MVC view models
│   │   └── IdentityModels.cs         # ASP.NET Identity user/role
│   ├── Views/                        # Razor views (ASPX syntax in some)
│   ├── Scripts/                      # jQuery, Bootstrap JS
│   └── Content/                      # CSS, images
├── NorthwindTechUniversity.Data/
│   ├── Repositories/                 # Repository pattern over EF6
│   └── UnitOfWork.cs                 # Unit of Work implementation
└── NorthwindTechUniversity.Tests/
    └── Controllers/                  # Basic controller tests (MSTest)
```

### Database Schema
- **Students** (StudentId, FirstName, LastName, Email, DateOfBirth, EnrollmentDate, ProgramId)
- **Courses** (CourseId, Title, Credits, DepartmentId, MaxCapacity)
- **Enrollments** (EnrollmentId, StudentId, CourseId, Semester, Grade, EnrollmentDate)
- **Faculty** (FacultyId, FirstName, LastName, Email, DepartmentId, HireDate)
- **Departments** (DepartmentId, Name, Budget, HeadFacultyId)
- **Programs** (ProgramId, Name, DepartmentId, RequiredCredits)
- **AspNetUsers/Roles/UserRoles** (ASP.NET Identity 2.0 tables)

### Legacy Anti-Patterns Present
- Database-First EDMX model (generated code, hard to maintain)
- Repository + Unit of Work wrapping EF6 (unnecessary abstraction layer)
- Unity container with XML configuration in web.config
- Synchronous database calls throughout (no async/await)
- Global.asax-based startup with scattered initialization
- Authentication tied to OWIN/Katana middleware pipeline
- Static AutoMapper.Initialize() call in Application_Start
- jQuery-heavy client-side code with inline scripts in views
- Bundling via System.Web.Optimization (not compatible with .NET Core)
- Hard-coded connection strings in web.config transforms
- MSTest v1 tests with limited coverage

## Target Architecture
- **Target Framework:** .NET 9 with ASP.NET Core MVC
- **ORM:** Entity Framework Core 9 with Code-First migrations
- **Authentication:** ASP.NET Core Identity with cookie authentication
- **DI:** Built-in Microsoft.Extensions.DependencyInjection
- **Mapping:** AutoMapper 13.x with profile-based configuration
- **Logging:** Microsoft.Extensions.Logging with Serilog sink
- **Frontend:** Bootstrap 5 + vanilla JS (jQuery removed)
- **Configuration:** appsettings.json with environment-based overrides
- **Database:** Azure SQL Database
- **Hosting:** Azure App Service (Linux)
- **CI/CD:** GitHub Actions with dotnet CLI

### Architecture Description
The monolithic MVC 5 app is migrated to ASP.NET Core MVC on .NET 9, preserving the MVC pattern but modernizing every layer. EF6 Database-First is replaced with EF Core Code-First, removing the EDMX. Unity DI is replaced with the built-in container. OWIN/Katana authentication is replaced with ASP.NET Core Identity middleware. All data access is made async. The application is deployed to Azure App Service with Azure SQL Database, using GitHub Actions for CI/CD.

## Branch Structure
- `main` — Contains the completed lab with full APPMODLAB.md documentation
- `legacy` — The ASP.NET MVC 5 / .NET Framework 4.8 application
- `solution` — The fully migrated .NET 9 application
- `step-1-project-migration` — csproj conversion, SDK-style project, NuGet updates
- `step-2-ef-core-migration` — EDMX to EF Core Code-First with migrations
- `step-3-di-and-config` — Unity to built-in DI, web.config to appsettings.json
- `step-4-auth-migration` — OWIN/Identity 2.0 to ASP.NET Core Identity
- `step-5-async-and-modern` — Async/await adoption, modern C# features
- `step-6-azure-deployment` — Azure App Service + Azure SQL deployment

## Lab Content Requirements (APPMODLAB.md)
### Learning Objectives
- Migrate ASP.NET MVC 5 projects to ASP.NET Core MVC with SDK-style projects
- Convert Entity Framework 6 Database-First (EDMX) to EF Core Code-First
- Replace Unity IoC container with built-in .NET Core dependency injection
- Migrate OWIN/Katana authentication to ASP.NET Core Identity
- Adopt async/await patterns for all data access operations

### Prerequisites
- Familiarity with ASP.NET MVC and C#
- Visual Studio 2022 or VS Code with C# Dev Kit
- .NET Framework 4.8 Developer Pack (for running legacy app)
- .NET 9 SDK
- SQL Server LocalDB or Azure SQL Database
- Azure subscription (for deployment steps)

### Step-by-Step Instructions Outline
1. **Run the Legacy App** — Build and explore the MVC 5 application, understand its structure
2. **Convert Project Files** — Migrate from classic .csproj to SDK-style, update NuGet packages
3. **Migrate Entity Framework** — Replace EDMX with EF Core DbContext and Code-First migrations
4. **Replace Dependency Injection** — Remove Unity, configure built-in DI in Program.cs
5. **Migrate Authentication** — Replace OWIN startup with ASP.NET Core Identity middleware
6. **Modernize Controllers** — Add async/await to all actions, update routing attributes
7. **Update Views and Frontend** — Migrate Razor views, replace jQuery with modern JS, upgrade to Bootstrap 5
8. **Configure for Azure** — Set up appsettings.json, add health checks, deploy to App Service
9. **Validate and Test** — Run all scenarios, compare behavior with legacy application

### Estimated Duration
4–6 hours

### Key Concepts Covered
- .NET Framework to .NET Core migration strategy
- Entity Framework migration patterns
- Dependency injection modernization
- Authentication middleware migration
- Modern C# language features

## What the Squad Needs to Build
1. **Legacy App Setup:** Build a fully functional ASP.NET MVC 5 application with student enrollment workflows, EF6 EDMX model, Unity DI, OWIN auth, and all the described legacy patterns. Must run on .NET Framework 4.8 with LocalDB. Include seed data for at least 50 students, 20 courses, and 100 enrollments.
2. **Modernization Implementation:** Migrate to .NET 9 with EF Core 9, built-in DI, ASP.NET Core Identity, async controllers, Bootstrap 5 frontend. All features must work identically to the legacy version. Include EF Core migrations and data seed.
3. **Lab Documentation:** APPMODLAB.md with detailed migration steps, before/after code comparisons, common pitfalls (e.g., EF6 lazy loading vs EF Core), and troubleshooting tips.
4. **Infrastructure as Code:** Bicep templates for Azure App Service (Linux plan), Azure SQL Database, and Application Insights.
5. **CI/CD:** GitHub Actions workflow for building, testing, and deploying the .NET 9 app to Azure App Service.

## Acceptance Criteria
- [ ] Legacy app runs on .NET Framework 4.8 with all CRUD operations working
- [ ] Solution app runs on .NET 9 with identical functionality
- [ ] EF Core migrations create the same schema as the EDMX model
- [ ] Authentication (login, register, roles) works in the migrated app
- [ ] All controllers use async/await for data access
- [ ] No Unity or System.Web dependencies remain in the solution branch
- [ ] Solution deploys to Azure App Service via Bicep
- [ ] APPMODLAB.md follows the template format with complete frontmatter
- [ ] Step-by-step guide is clear and reproducible
- [ ] All code compiles and runs without errors
