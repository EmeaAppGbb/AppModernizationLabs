# .NET Framework with EntLib to .NET 9 with Core DI

## Overview
- **Category:** Code Modernization
- **Priority:** P1
- **Languages:** C#/.NET Framework 4.8 → C#/.NET 9
- **Repository Name:** appmodlab-dotnet-framework-entlib-to-dotnet9-core-di
- **Organization:** EmeaAppGbb

## Objective
This lab focuses on replacing Microsoft Enterprise Library (EntLib) abstractions — including the Logging Application Block, Exception Handling Application Block, Data Access Application Block, and Unity — with native .NET Core dependency injection, configuration, and logging patterns. EntLib was ubiquitous in enterprise .NET applications of the 2008–2015 era and represents one of the most complex migration challenges due to its deep integration with application infrastructure.

## Demo Legacy Application
**Business Domain:** Corporate expense management and approval system for "Alpine Financial Services"

The legacy application is an internal expense reporting system where employees submit expenses, managers approve them, and finance processes reimbursements. It heavily uses Enterprise Library for all cross-cutting concerns.

### Tech Stack
- ASP.NET Web API 2 on .NET Framework 4.8
- Enterprise Library 6.0:
  - Logging Application Block (flat file + database logging)
  - Exception Handling Application Block (exception policies)
  - Data Access Application Block (DAAB) for stored procedure calls
  - Validation Application Block (attribute-based validation)
  - Caching Application Block (in-memory caching)
- Unity 5.x for dependency injection (EntLib's preferred container)
- SQL Server with stored procedures for all data access
- WCF service endpoints for integration with payroll system
- Windows Authentication (Integrated Security)
- XML configuration in web.config (hundreds of lines of EntLib config)

### Key Files/Folders Structure
```
AlpineExpenseManager/
├── AlpineExpenseManager.sln
├── AlpineExpenseManager.WebApi/
│   ├── Web.config                       # Massive EntLib XML configuration
│   ├── Global.asax.cs                   # EntLib bootstrapper
│   ├── App_Start/
│   │   ├── WebApiConfig.cs              # Web API route config
│   │   └── UnityConfig.cs               # Unity + EntLib registration
│   ├── Controllers/
│   │   ├── ExpenseController.cs         # Expense CRUD (uses DAAB)
│   │   ├── ApprovalController.cs        # Approval workflow
│   │   ├── ReimbursementController.cs   # Finance processing
│   │   └── ReportController.cs          # Expense reports
│   └── Filters/
│       └── EntLibExceptionFilter.cs     # Exception handling via EntLib policy
├── AlpineExpenseManager.Core/
│   ├── Services/
│   │   ├── ExpenseService.cs            # Business logic with EntLib logging
│   │   ├── ApprovalService.cs           # Approval rules with exception policies
│   │   └── NotificationService.cs       # WCF client for payroll integration
│   ├── DataAccess/
│   │   ├── ExpenseRepository.cs         # DAAB-based data access
│   │   ├── ApprovalRepository.cs        # Stored procedure calls via DAAB
│   │   └── DatabaseFactory.cs           # EntLib Database factory wrapper
│   ├── Validation/
│   │   └── ExpenseValidators.cs         # EntLib Validation attributes
│   └── Logging/
│       └── LoggingHelper.cs             # Static EntLib LogWriter wrapper
├── AlpineExpenseManager.WcfContracts/
│   ├── IPayrollService.cs               # WCF service contract
│   └── PayrollServiceClient.cs          # Generated WCF proxy
└── AlpineExpenseManager.Tests/
    └── Services/                        # Tests with EntLib test doubles
```

### Database Schema
- **Expenses** (ExpenseId, EmployeeId, Category, Amount, Currency, Description, ReceiptUrl, SubmittedDate, Status)
- **ApprovalWorkflows** (WorkflowId, ExpenseId, ApproverId, Decision, Comments, DecisionDate, Level)
- **Reimbursements** (ReimbursementId, ExpenseId, Amount, ProcessedDate, PayrollBatchId, Status)
- **ExpenseCategories** (CategoryId, Name, MaxAmount, RequiresReceipt, ApprovalThreshold)
- **AuditLog** (LogId, Timestamp, Category, Priority, Message, MachineName, AppDomainName) — EntLib logging table
- Stored procedures for every data operation (sp_InsertExpense, sp_GetExpensesByEmployee, sp_ApproveExpense, etc.)

### Legacy Anti-Patterns Present
- Enterprise Library XML configuration spanning 300+ lines in web.config
- Static LogWriter wrapper class hiding EntLib dependency (untestable)
- Exception handling through named policies (swallowing exceptions silently)
- Data Access Application Block wrapping ADO.NET wrapping stored procedures (3 layers of abstraction)
- Unity container registered via EntLib's UnityContainerExtension
- WCF service references with generated proxy classes
- Validation Application Block attributes mixed with data annotations
- Caching Application Block with no cache invalidation strategy
- Windows Authentication with no claims-based identity

## Target Architecture
- **Target Framework:** .NET 9 with ASP.NET Core Web API
- **Dependency Injection:** Built-in Microsoft.Extensions.DependencyInjection
- **Logging:** Microsoft.Extensions.Logging with Serilog (structured logging)
- **Configuration:** appsettings.json with Options pattern
- **Data Access:** Entity Framework Core 9 (replacing DAAB + stored procedures)
- **Validation:** FluentValidation (replacing EntLib Validation Block)
- **HTTP Client:** HttpClientFactory with Polly resilience (replacing WCF)
- **Caching:** IMemoryCache + IDistributedCache with Azure Redis Cache
- **Exception Handling:** ASP.NET Core middleware with ProblemDetails
- **Authentication:** Azure AD / Entra ID with JWT tokens
- **Hosting:** Azure App Service
- **Monitoring:** Application Insights with Serilog sink

### Architecture Description
Every Enterprise Library block is replaced with its modern .NET Core equivalent. The DAAB stored-procedure-based access is replaced with EF Core and LINQ queries. The Exception Handling Block's policy-based approach is replaced with global exception middleware returning RFC 7807 ProblemDetails. The Logging Block is replaced with the built-in ILogger<T> abstraction backed by Serilog. WCF clients become HttpClient calls with Polly retry policies. Unity is removed entirely in favor of the built-in DI container. All XML configuration moves to strongly-typed Options pattern classes.

## Branch Structure
- `main` — Contains the completed lab with full APPMODLAB.md documentation
- `legacy` — The EntLib-heavy .NET Framework 4.8 application
- `solution` — The fully modernized .NET 9 application
- `step-1-remove-logging-block` — Replace EntLib Logging with ILogger<T> + Serilog
- `step-2-remove-exception-block` — Replace Exception Handling Block with middleware
- `step-3-remove-daab` — Replace Data Access Block with EF Core
- `step-4-remove-unity` — Replace Unity with built-in DI
- `step-5-remove-wcf` — Replace WCF clients with HttpClientFactory
- `step-6-modern-auth-and-deploy` — Entra ID auth + Azure deployment

## Lab Content Requirements (APPMODLAB.md)
### Learning Objectives
- Identify and catalog Enterprise Library dependencies in a legacy codebase
- Replace EntLib Logging Application Block with Microsoft.Extensions.Logging and Serilog
- Replace EntLib Exception Handling policies with ASP.NET Core exception middleware
- Migrate from Data Access Application Block to Entity Framework Core
- Replace Unity container extensions with built-in dependency injection

### Prerequisites
- Strong C# and .NET Framework experience
- Familiarity with Enterprise Library concepts (helpful but not required)
- .NET Framework 4.8 Developer Pack
- .NET 9 SDK
- Visual Studio 2022
- SQL Server LocalDB or Azure SQL Database
- Azure subscription (for deployment and Redis Cache)

### Step-by-Step Instructions Outline
1. **Audit EntLib Usage** — Catalog all Enterprise Library references, configuration blocks, and usage patterns
2. **Replace Logging Block** — Introduce ILogger<T>, configure Serilog, remove LogWriter references
3. **Replace Exception Handling** — Remove exception policies, implement global exception middleware
4. **Replace Data Access Block** — Create EF Core DbContext, map entities, replace stored procedure calls
5. **Replace Validation Block** — Switch from EntLib validation attributes to FluentValidation
6. **Remove Unity** — Register all services in Program.cs using built-in DI
7. **Replace WCF Client** — Implement HttpClientFactory with typed clients and Polly
8. **Modernize Auth** — Replace Windows Auth with Entra ID JWT tokens
9. **Deploy to Azure** — Configure App Service, Redis Cache, Application Insights

### Estimated Duration
5–7 hours

### Key Concepts Covered
- Enterprise Library migration strategies
- Modern .NET logging and configuration patterns
- EF Core adoption from raw ADO.NET
- WCF-to-HTTP migration
- Options pattern and strongly-typed configuration

## What the Squad Needs to Build
1. **Legacy App Setup:** Build a Web API 2 application that uses all five Enterprise Library blocks extensively. The web.config must contain realistic EntLib XML configuration. Include working stored procedures and a WCF service mock. The app must demonstrate real expense submission and approval workflows.
2. **Modernization Implementation:** Replace every EntLib block with its modern equivalent. EF Core with migrations, Serilog structured logging, FluentValidation, HttpClientFactory with Polly, global exception middleware. All business logic must produce identical results.
3. **Lab Documentation:** APPMODLAB.md with a mapping table showing each EntLib block and its modern replacement. Include before/after code comparisons for each block removal step. Document common EntLib migration pitfalls.
4. **Infrastructure as Code:** Bicep templates for Azure App Service, Azure SQL Database, Azure Redis Cache, and Application Insights.
5. **CI/CD:** GitHub Actions workflow for build, test, and deploy with separate stages.

## Acceptance Criteria
- [ ] Legacy app uses all five EntLib blocks with realistic configuration
- [ ] All stored procedures execute correctly in the legacy app
- [ ] Solution has zero Enterprise Library NuGet references
- [ ] Serilog produces structured logs equivalent to EntLib logging output
- [ ] EF Core migrations create schema matching stored procedure expectations
- [ ] FluentValidation rules match EntLib Validation Block behavior
- [ ] Exception middleware returns proper ProblemDetails responses
- [ ] APPMODLAB.md follows the template format with complete frontmatter
- [ ] Step-by-step guide is clear and reproducible
- [ ] All code compiles and runs without errors
