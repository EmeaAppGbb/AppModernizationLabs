# Classic ASP to ASP.NET Core

## Overview
- **Category:** Code Modernization
- **Priority:** P2
- **Languages:** VBScript/Classic ASP → C#/ASP.NET Core
- **Repository Name:** appmodlab-classic-asp-to-aspnet-core
- **Organization:** EmeaAppGbb

## Objective
This lab teaches participants how to modernize a legacy Classic ASP website built with VBScript and ADO to ASP.NET Core with Razor Pages or MVC. Classic ASP applications are still found in many enterprises, often running on aging IIS 6/7 servers with no upgrade path. This lab demonstrates a practical migration strategy covering VBScript-to-C# translation, ADO-to-EF Core data access migration, and include-file-based architecture to proper MVC separation.

## Demo Legacy Application
**Business Domain:** Real estate property listing and agent portal for "Summit Realty Group"

The legacy website is a Classic ASP application that manages property listings, agent profiles, client inquiries, appointment scheduling, and property search. It was built around 2003 and has been maintained with incremental patches ever since.

### Tech Stack
- Classic ASP 3.0 with VBScript
- ADO 2.8 (ActiveX Data Objects) with inline SQL queries
- SQL Server 2012 Express
- IIS 6/7 style configuration
- Server-side includes (SSI) for header/footer/navigation
- CDO (Collaboration Data Objects) for email
- FileSystemObject for file uploads (property photos)
- Session variables for authentication (no token-based auth)
- Inline CSS and table-based layout

### Key Files/Folders Structure
```
SummitRealty/
├── default.asp                    # Homepage with featured listings
├── global.asa                     # Application/Session events
├── includes/
│   ├── conn.asp                   # Database connection string (hardcoded)
│   ├── header.asp                 # HTML header with nav
│   ├── footer.asp                 # HTML footer
│   ├── functions.asp              # Utility functions (string helpers, date formatting)
│   └── auth.asp                   # Session-based authentication check
├── listings/
│   ├── search.asp                 # Property search with inline SQL
│   ├── detail.asp                 # Property detail page (uses querystring ID)
│   ├── add.asp                    # Add new listing (agent only)
│   ├── edit.asp                   # Edit listing
│   └── photos.asp                 # Photo upload with FileSystemObject
├── agents/
│   ├── directory.asp              # Agent listing page
│   ├── profile.asp                # Agent profile
│   └── dashboard.asp              # Agent dashboard with stats
├── inquiries/
│   ├── contact.asp                # Contact form with CDO email
│   ├── schedule.asp               # Appointment scheduling
│   └── list.asp                   # Inquiry list (agent view)
├── admin/
│   ├── login.asp                  # Login page (Session-based)
│   ├── users.asp                  # User management
│   └── reports.asp                # Basic reporting
├── images/                        # Property photos
├── css/                           # Inline and external CSS
└── database/
    └── schema.sql                 # Database creation script
```

### Database Schema
- **Properties** (PropertyID, Address, City, State, ZipCode, Price, Bedrooms, Bathrooms, SquareFeet, ListingDate, Status, AgentID, Description, PropertyType)
- **Agents** (AgentID, FirstName, LastName, Email, Phone, LicenseNumber, Bio, PhotoPath, HireDate)
- **Inquiries** (InquiryID, PropertyID, ClientName, ClientEmail, ClientPhone, Message, InquiryDate, Status, AgentID)
- **Appointments** (AppointmentID, PropertyID, AgentID, ClientName, ClientEmail, AppointmentDate, Notes, Status)
- **Users** (UserID, Username, Password, Role, AgentID, LastLogin) — passwords stored in plain text
- **PropertyPhotos** (PhotoID, PropertyID, FilePath, Caption, SortOrder)

### Legacy Anti-Patterns Present
- SQL injection vulnerabilities (string concatenation in queries)
- Passwords stored in plain text in the database
- Session-based authentication with no timeout or CSRF protection
- Inline SQL throughout every ASP page (no data layer)
- Server-side includes as the only code reuse mechanism
- FileSystemObject for file uploads (no size/type validation)
- CDO email with hardcoded SMTP credentials
- QueryString parameters used without sanitization
- No error handling (On Error Resume Next used globally)
- Table-based HTML layout with inline styles
- No separation of concerns — HTML, VBScript, and SQL on the same page

## Target Architecture
- **Framework:** .NET 9 with ASP.NET Core Razor Pages (or MVC)
- **Data Access:** Entity Framework Core 9
- **Authentication:** ASP.NET Core Identity with hashed passwords
- **File Upload:** Azure Blob Storage for property photos
- **Email:** Azure Communication Services (replacing CDO)
- **Search:** Full-text search with Azure AI Search
- **Frontend:** Bootstrap 5, responsive design
- **Hosting:** Azure App Service
- **Security:** HTTPS, CSRF protection, parameterized queries, input validation

### Architecture Description
The spaghetti Classic ASP code is restructured into ASP.NET Core Razor Pages with clean page models separating HTML from logic. All inline SQL becomes EF Core LINQ queries with parameterized inputs. Session-based auth becomes ASP.NET Core Identity with proper password hashing. File uploads move to Azure Blob Storage. The CDO email is replaced with Azure Communication Services. The application gains responsive design, proper error handling, and security best practices.

## Branch Structure
- `main` — Contains the completed lab with full APPMODLAB.md documentation
- `legacy` — The Classic ASP application (runnable via IIS or Docker with ASP image)
- `solution` — The fully modernized ASP.NET Core application
- `step-1-project-setup` — ASP.NET Core project scaffold with page structure
- `step-2-data-layer` — EF Core DbContext, entities, and data migration
- `step-3-page-migration` — Convert ASP pages to Razor Pages with page models
- `step-4-auth-and-security` — ASP.NET Core Identity, CSRF, input validation
- `step-5-azure-services` — Blob Storage for photos, Communication Services for email
- `step-6-deploy` — Azure App Service deployment

## Lab Content Requirements (APPMODLAB.md)
### Learning Objectives
- Analyze a Classic ASP codebase to identify migration scope and risks
- Map VBScript patterns to C# equivalents systematically
- Replace ADO inline SQL with Entity Framework Core
- Implement proper authentication and security (replacing plain-text passwords)
- Migrate file-based operations to Azure cloud services

### Prerequisites
- Basic understanding of Classic ASP / VBScript (reading knowledge sufficient)
- C# and ASP.NET Core experience
- .NET 9 SDK
- Docker Desktop (for running legacy Classic ASP app)
- Azure subscription
- SQL Server LocalDB

### Step-by-Step Instructions Outline
1. **Explore the Legacy App** — Run the Classic ASP site, browse listings, submit an inquiry
2. **Identify Security Issues** — Document SQL injection points, plain-text passwords, missing validation
3. **Create ASP.NET Core Project** — Set up Razor Pages project with folder structure matching ASP pages
4. **Build Data Layer** — Create EF Core entities and DbContext, migrate data from legacy DB
5. **Migrate Pages** — Convert each ASP page to a Razor Page with proper page model
6. **Add Authentication** — Implement ASP.NET Core Identity, migrate user accounts with hashed passwords
7. **Fix Security Issues** — Add CSRF tokens, input validation, parameterized queries
8. **Integrate Azure Services** — Blob Storage for photos, Communication Services for email
9. **Deploy to Azure** — App Service + Azure SQL + Blob Storage

### Estimated Duration
4–6 hours

### Key Concepts Covered
- Classic ASP to ASP.NET Core migration strategy
- VBScript to C# translation patterns
- Security remediation (SQL injection, password storage)
- ADO to Entity Framework Core
- Cloud service integration for file storage and email

## What the Squad Needs to Build
1. **Legacy App Setup:** Build a Classic ASP website that runs on IIS (provide Docker image with IIS + Classic ASP support). Include all described pages with realistic VBScript code, inline SQL, and legacy anti-patterns. Seed database with 50+ property listings, 10 agents, and sample inquiries.
2. **Modernization Implementation:** Build ASP.NET Core Razor Pages application with EF Core, Identity, Azure Blob Storage integration, and responsive Bootstrap 5 UI. All features must work — search, listing CRUD, agent profiles, inquiries, appointments.
3. **Lab Documentation:** APPMODLAB.md with VBScript-to-C# translation guide, security remediation checklist, and before/after code comparisons for each page migration.
4. **Infrastructure as Code:** Bicep templates for Azure App Service, Azure SQL Database, Azure Blob Storage, and Azure Communication Services.
5. **CI/CD:** GitHub Actions workflow for building and deploying the .NET 9 application.

## Acceptance Criteria
- [ ] Legacy Classic ASP site runs in Docker and all pages function
- [ ] Property search, detail view, and inquiry submission work in legacy
- [ ] All ASP pages are migrated to Razor Pages with page models
- [ ] EF Core replaces all inline SQL with parameterized LINQ queries
- [ ] ASP.NET Core Identity replaces session-based auth with hashed passwords
- [ ] Property photos upload to Azure Blob Storage
- [ ] All SQL injection vulnerabilities are remediated in the solution
- [ ] APPMODLAB.md follows the template format with complete frontmatter
- [ ] Step-by-step guide is clear and reproducible
- [ ] All code compiles and runs without errors
