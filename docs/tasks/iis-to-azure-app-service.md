# IIS to Azure App Service Migration

## Overview
- **Category:** Infrastructure Modernization
- **Priority:** P1
- **Languages:** IIS/HTTP/C#/.NET
- **Repository Name:** appmodlab-iis-to-azure-app-service
- **Organization:** EmeaAppGbb

## Objective
This lab guides participants through migrating IIS-hosted applications to Azure App Service. It covers migrating IIS-specific features (URL rewrite rules, custom handlers, application pools), handling configuration transformation, SSL certificate migration, and leveraging App Service features like deployment slots, auto-scaling, and managed identity. This is the most common Azure migration path for .NET web workloads and one of the fastest wins for cloud adoption.

## Demo Legacy Application
**Business Domain:** Employee self-service HR portal for "Cascade Human Resources" — a mid-sized company's intranet application

The legacy system is an ASP.NET Core 6.0 application hosted on IIS 10 on Windows Server 2019, with IIS-specific configurations including URL rewrite rules, virtual directories, custom error pages, HTTPS redirect, and Windows Authentication via IIS.

### Tech Stack
- ASP.NET Core 6.0 hosted on IIS 10 (in-process hosting model)
- IIS URL Rewrite Module 2.1 (complex rewrite rules)
- IIS Application Request Routing (ARR) for reverse proxy
- Windows Authentication (Negotiate/NTLM via IIS)
- IIS Virtual Directories pointing to file shares
- Custom IIS HTTP modules for request logging
- SQL Server 2019 on a separate server
- SMTP relay server for email notifications
- SSL certificate bound to IIS site (PFX file)
- IIS application pool with specific identity and recycling settings
- Web.config transforms for environment-specific settings

### Key Files/Folders Structure
```
CascadeHRPortal/
├── CascadeHRPortal.sln
├── CascadeHRPortal.Web/
│   ├── CascadeHRPortal.Web.csproj
│   ├── Program.cs
│   ├── web.config                       # IIS-specific settings
│   ├── Controllers/
│   │   ├── LeaveController.cs           # Leave request management
│   │   ├── TimesheetController.cs       # Timesheet submission
│   │   ├── PayslipController.cs         # Payslip viewing
│   │   ├── ProfileController.cs         # Employee profile management
│   │   ├── BenefitsController.cs        # Benefits enrollment
│   │   └── DirectoryController.cs       # Employee directory search
│   ├── Views/                           # Razor views
│   ├── wwwroot/                         # Static assets
│   └── appsettings.json                 # Connection strings, SMTP config
├── CascadeHRPortal.Services/
│   ├── LeaveService.cs
│   ├── TimesheetService.cs
│   ├── PayrollIntegration.cs            # Calls external payroll API
│   └── DocumentService.cs              # Reads from IIS virtual directory
├── IIS-Config/
│   ├── applicationHost.config           # IIS site configuration
│   ├── urlrewrite.config               # URL rewrite rules (20+ rules)
│   ├── web.config.transform.staging    # Config transform for staging
│   ├── web.config.transform.prod       # Config transform for production
│   └── setup-iis.ps1                   # PowerShell IIS setup script
└── Database/
    └── schema.sql                       # HR database schema
```

### IIS Configuration Details
- URL Rewrite rules: legacy URL redirects, API versioning rewrites, trailing slash normalization
- Application pool: .NET CLR No Managed Code, Enable 32-bit: False, recycling at 2:00 AM daily
- Virtual directories: /documents → \\\\fileserver\\hr-documents, /payslips → \\\\fileserver\\payslips
- Windows Authentication enabled, Anonymous disabled
- Custom 403/404/500 error pages
- Request logging via custom HTTP module
- SSL certificate from internal CA bound to port 443
- ARR proxy rule forwarding /api/payroll/* to internal payroll service

### Database Schema
- **Employees** (EmployeeId, FirstName, LastName, Email, Department, Manager, HireDate, Status)
- **LeaveRequests** (RequestId, EmployeeId, LeaveType, StartDate, EndDate, Status, ApproverId, Notes)
- **Timesheets** (TimesheetId, EmployeeId, WeekStartDate, Hours, ProjectCode, Status, ApprovedBy)
- **Benefits** (BenefitId, EmployeeId, PlanType, CoverageLevel, EffectiveDate, EndDate)
- **Documents** (DocumentId, EmployeeId, DocumentType, FilePath, UploadDate, Category)

### Legacy Anti-Patterns Present
- IIS-specific URL rewrite rules not portable to other hosts
- Virtual directories pointing to network file shares
- Windows Authentication tightly coupled to IIS/Active Directory
- Config transforms for environment management (web.config.transform)
- Custom HTTP module for logging (IIS pipeline dependency)
- ARR proxy rules for routing to internal services
- Manual SSL certificate management with PFX files
- Application pool identity used for SQL Server trusted connection
- No health check endpoints
- No deployment automation — manual IIS publish via Web Deploy

## Target Architecture
- **Hosting:** Azure App Service (Linux or Windows plan)
- **Authentication:** Entra ID with Easy Auth (replacing Windows Auth)
- **File Storage:** Azure Blob Storage (replacing virtual directories/file shares)
- **SSL:** App Service Managed Certificates or Azure Key Vault
- **URL Handling:** ASP.NET Core middleware (replacing IIS URL Rewrite)
- **Deployment:** Deployment slots for zero-downtime deployments
- **Scaling:** Auto-scale rules based on CPU and request count
- **Monitoring:** Application Insights
- **Configuration:** Azure App Configuration + Key Vault references
- **Proxy:** Azure Application Gateway or Front Door (replacing ARR)
- **Database:** Azure SQL Database with Managed Identity

### Architecture Description
The IIS-hosted application moves to Azure App Service with minimal code changes but significant infrastructure modernization. IIS URL rewrite rules are converted to ASP.NET Core middleware. Windows Authentication is replaced with Entra ID using App Service Easy Auth. Virtual directories are replaced with Azure Blob Storage access. Config transforms become App Service configuration and Key Vault references. SSL is handled by App Service managed certificates. The ARR proxy becomes Azure Application Gateway. Deployment slots enable blue-green deployments replacing manual Web Deploy.

## Branch Structure
- `main` — Contains the completed lab with full APPMODLAB.md documentation
- `legacy` — The IIS-hosted application with all IIS configurations
- `solution` — The Azure App Service-hosted application
- `step-1-iis-assessment` — IIS configuration audit and migration plan
- `step-2-url-rewrite-migration` — Convert IIS rewrite rules to ASP.NET Core middleware
- `step-3-auth-migration` — Windows Auth to Entra ID Easy Auth
- `step-4-storage-migration` — Virtual directories to Azure Blob Storage
- `step-5-app-service-deploy` — App Service deployment with slots and scaling

## Lab Content Requirements (APPMODLAB.md)
### Learning Objectives
- Audit IIS configurations and assess migration complexity
- Convert IIS URL Rewrite rules to ASP.NET Core middleware
- Migrate Windows Authentication to Entra ID with Easy Auth
- Replace IIS virtual directories with Azure Blob Storage
- Deploy to App Service with deployment slots and auto-scaling

### Prerequisites
- ASP.NET Core and IIS administration experience
- Azure subscription with Contributor access
- Azure CLI installed
- .NET 9 SDK (solution targets .NET 9)
- Basic understanding of Entra ID / Azure AD

### Step-by-Step Instructions Outline
1. **Audit IIS Configuration** — Catalog URL rewrite rules, virtual dirs, auth settings, handlers
2. **Convert URL Rewrites** — Translate IIS rewrite rules to ASP.NET Core RewriteMiddleware
3. **Migrate Authentication** — Configure Entra ID app registration, enable Easy Auth
4. **Migrate File Access** — Replace virtual directory access with Azure Blob Storage SDK
5. **Configure App Service** — Create App Service plan, configure settings from App Configuration
6. **Set Up Deployment Slots** — Create staging slot, configure slot-specific settings
7. **Configure SSL** — Set up managed certificate or Key Vault integration
8. **Configure Auto-Scaling** — Set scale rules based on CPU and requests
9. **Deploy and Validate** — Deploy via GitHub Actions, test all HR portal features

### Estimated Duration
3–5 hours

### Key Concepts Covered
- IIS to App Service migration patterns
- URL rewrite rule conversion
- Authentication migration (Windows Auth to Entra ID)
- Deployment slots and blue-green deployment
- App Service auto-scaling

## What the Squad Needs to Build
1. **Legacy App Setup:** Build an ASP.NET Core 6.0 application with IIS-specific configurations including URL rewrite rules, virtual directories, Windows Auth, custom error pages, and ARR proxy rules. Include PowerShell scripts for IIS setup and Docker-based IIS simulation.
2. **Modernization Implementation:** Migrate to Azure App Service with Entra ID auth, Blob Storage, App Configuration, deployment slots, and auto-scaling. Convert all IIS dependencies to portable ASP.NET Core equivalents.
3. **Lab Documentation:** APPMODLAB.md with IIS feature-to-App Service mapping table, URL rewrite conversion guide, and authentication migration walkthrough.
4. **Infrastructure as Code:** Bicep templates for Azure App Service (with deployment slots), Azure SQL Database, Azure Blob Storage, Azure App Configuration, Azure Key Vault, and Application Insights.
5. **CI/CD:** GitHub Actions workflow with build, deploy to staging slot, swap to production.

## Acceptance Criteria
- [ ] Legacy app runs on IIS with all configurations active
- [ ] URL rewrite rules function correctly in both IIS and App Service
- [ ] Authentication works with Entra ID (replacing Windows Auth)
- [ ] File access works via Blob Storage (replacing virtual directories)
- [ ] Deployment slots enable zero-downtime deployment
- [ ] Auto-scaling rules are configured and tested
- [ ] SSL is handled by managed certificates
- [ ] APPMODLAB.md follows the template format with complete frontmatter
- [ ] Step-by-step guide is clear and reproducible
- [ ] All code compiles and runs without errors
