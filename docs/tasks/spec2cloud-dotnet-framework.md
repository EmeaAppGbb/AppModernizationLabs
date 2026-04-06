# Spec2Cloud .NET Framework Modernization

## Overview
- **Category:** Code Modernization
- **Priority:** P2
- **Languages:** C#/.NET Framework 4.8 → C#/.NET 9
- **Repository Name:** appmodlab-spec2cloud-dotnet-framework
- **Organization:** EmeaAppGbb

## Objective
This lab demonstrates the Spec2Cloud methodology for modernizing a .NET Framework application. Participants learn to use Spec2Cloud tooling to reverse-engineer a legacy codebase, generate comprehensive specifications (architecture docs, API contracts, data models), and then use those specs to drive a systematic modernization to .NET 9. The emphasis is on the spec-driven workflow rather than manual code migration — showing how specifications serve as the source of truth for modernization decisions.

## Demo Legacy Application
**Business Domain:** Municipal building permit and inspection management system for "Riverdale City Council"

The legacy application manages the lifecycle of building permits — from application submission through plan review, permit issuance, scheduled inspections, and final certificate of occupancy. It serves city planners, building inspectors, and construction contractors.

### Tech Stack
- ASP.NET Web Forms on .NET Framework 4.8
- ADO.NET with DataSets and DataAdapters (no ORM)
- SQL Server stored procedures for business logic
- ViewState-heavy pages with code-behind files
- Crystal Reports for permit and inspection reports
- SMTP-based email notifications (System.Net.Mail)
- Windows Authentication with role-based access
- Session state stored in SQL Server

### Key Files/Folders Structure
```
RiverdalePermitSystem/
├── RiverdalePermitSystem.sln
├── RiverdalePermitSystem.Web/
│   ├── Web.config
│   ├── Global.asax.cs
│   ├── Default.aspx / Default.aspx.cs
│   ├── Pages/
│   │   ├── PermitApplication.aspx/.cs    # Multi-step wizard with ViewState
│   │   ├── PlanReview.aspx/.cs           # Plan review workflow
│   │   ├── InspectionSchedule.aspx/.cs   # Inspector scheduling grid
│   │   ├── PermitSearch.aspx/.cs         # Search with GridView + ObjectDataSource
│   │   └── Dashboard.aspx/.cs            # Admin dashboard with UpdatePanels
│   ├── UserControls/
│   │   ├── PermitHeader.ascx             # Reusable permit header control
│   │   └── AddressLookup.ascx            # Address autocomplete control
│   ├── Reports/
│   │   ├── PermitReport.rpt              # Crystal Report for permit document
│   │   └── InspectionReport.rpt          # Crystal Report for inspection results
│   └── App_Code/
│       ├── PermitDataAccess.cs           # ADO.NET DataSet-based access
│       ├── InspectionDataAccess.cs       # Stored procedure wrappers
│       └── EmailHelper.cs               # Static SMTP helper
├── Database/
│   ├── Schema.sql                        # Table definitions
│   └── StoredProcedures/                 # 40+ stored procedures
└── RiverdalePermitSystem.Tests/          # Minimal test coverage
```

### Database Schema
- **Permits** (PermitId, ApplicationDate, PropertyAddress, ParcelNumber, PermitType, Status, ApplicantId, ContractorId, EstimatedCost, IssuedDate, ExpirationDate)
- **PlanReviews** (ReviewId, PermitId, ReviewerId, ReviewType, Status, Comments, ReviewDate, DueDate)
- **Inspections** (InspectionId, PermitId, InspectorId, InspectionType, ScheduledDate, Result, Comments, Photos)
- **Applicants** (ApplicantId, Name, Email, Phone, Company, LicenseNumber)
- **Contractors** (ContractorId, CompanyName, LicenseNumber, InsuranceExpiry, Rating)
- **Fees** (FeeId, PermitId, FeeType, Amount, PaidDate, PaymentMethod)

### Legacy Anti-Patterns Present
- Web Forms page lifecycle with heavy ViewState (pages are 200KB+)
- Code-behind files with business logic mixed into event handlers
- ADO.NET DataSets as transport objects across layers
- Stored procedures containing business rules (permit validation, fee calculation)
- Crystal Reports with hard-coded database connections
- Static helper classes for email and data access
- UpdatePanel-based partial postbacks for "AJAX" behavior
- Session state storing complex objects (DataSets in session)
- No separation between UI logic and business logic

## Target Architecture
- **Target Framework:** .NET 9 with ASP.NET Core MVC + Blazor components
- **Data Access:** Entity Framework Core 9 with repository pattern
- **Reporting:** QuestPDF or RDLC for PDF generation (replacing Crystal Reports)
- **Email:** Azure Communication Services
- **Authentication:** Entra ID with role claims
- **Frontend:** Blazor Server components for interactive elements, Razor views for static pages
- **Caching:** Azure Redis Cache for session data
- **Storage:** Azure Blob Storage for inspection photos
- **Hosting:** Azure App Service
- **Search:** Azure AI Search for permit lookup

### Architecture Description
Spec2Cloud analyzes the legacy Web Forms application and generates specifications documenting every page, data flow, stored procedure, and business rule. These specs become the blueprint for rebuilding in ASP.NET Core MVC with Blazor components replacing UpdatePanel interactivity. DataSets are replaced with strongly-typed EF Core entities. Business rules move from stored procedures into domain services. Crystal Reports become QuestPDF-generated documents. The specs ensure nothing is lost in translation.

## Branch Structure
- `main` — Contains the completed lab with full APPMODLAB.md documentation
- `legacy` — The ASP.NET Web Forms application
- `solution` — The fully modernized .NET 9 application
- `step-1-spec-generation` — Spec2Cloud analysis output (architecture docs, API contracts, data models)
- `step-2-spec-review` — Refined specifications with modernization decisions documented
- `step-3-data-layer` — EF Core implementation driven by data model specs
- `step-4-business-logic` — Domain services driven by business rule specs
- `step-5-ui-migration` — MVC + Blazor UI driven by page specs
- `step-6-deploy` — Azure deployment with all supporting services

## Lab Content Requirements (APPMODLAB.md)
### Learning Objectives
- Use Spec2Cloud to reverse-engineer a legacy ASP.NET Web Forms application
- Generate and review architecture specifications, API contracts, and data model documents
- Use specifications as the source of truth to guide modernization decisions
- Rebuild a Web Forms application as ASP.NET Core MVC with Blazor interactivity
- Replace Crystal Reports, DataSets, and stored-procedure logic with modern equivalents

### Prerequisites
- C# and ASP.NET experience
- Basic understanding of Spec2Cloud concepts
- .NET Framework 4.8 Developer Pack and .NET 9 SDK
- Visual Studio 2022
- Azure subscription
- SQL Server LocalDB

### Step-by-Step Instructions Outline
1. **Explore the Legacy App** — Run the Web Forms application, submit a permit, schedule an inspection
2. **Run Spec2Cloud Analysis** — Point Spec2Cloud at the codebase, generate initial specifications
3. **Review Generated Specs** — Examine architecture docs, identify business rules in stored procedures
4. **Make Modernization Decisions** — Annotate specs with target patterns (e.g., "DataSet → EF Core entity")
5. **Build Data Layer from Specs** — Create EF Core DbContext and entities matching data model specs
6. **Build Domain Services from Specs** — Implement business rules documented in the specs
7. **Build UI from Page Specs** — Create MVC views and Blazor components matching page specifications
8. **Replace Reports** — Generate permit PDFs using QuestPDF based on report specs
9. **Deploy and Validate** — Deploy to Azure, run through all workflows

### Estimated Duration
6–8 hours

### Key Concepts Covered
- Spec-driven modernization methodology
- Spec2Cloud toolchain usage
- Web Forms to MVC/Blazor migration patterns
- Specification as documentation and migration guide
- DataSet replacement strategies

## What the Squad Needs to Build
1. **Legacy App Setup:** Build a realistic ASP.NET Web Forms application with multi-step permit wizard, inspection scheduling grid, Crystal Reports, and all described legacy patterns. Include 40+ stored procedures with embedded business rules. Seed data with sample permits in various lifecycle stages.
2. **Modernization Implementation:** Rebuild in ASP.NET Core MVC + Blazor following the Spec2Cloud-generated specifications. All permit workflows must function identically. Replace Crystal Reports with QuestPDF, DataSets with EF Core, stored procedure logic with domain services.
3. **Lab Documentation:** APPMODLAB.md emphasizing the spec-driven workflow — show how specs were generated, how they guided decisions, and how they mapped to implementation. Include spec document examples and before/after architecture diagrams.
4. **Infrastructure as Code:** Bicep templates for Azure App Service, Azure SQL Database, Azure Redis Cache, Azure Blob Storage, and Azure Communication Services.
5. **CI/CD:** GitHub Actions workflow with build, test, and deploy stages.

## Acceptance Criteria
- [ ] Legacy Web Forms app runs with full permit lifecycle workflow
- [ ] Spec2Cloud generates specifications covering all pages, data flows, and business rules
- [ ] Specifications are documented and stored in the `step-1-spec-generation` branch
- [ ] Solution app implements all features documented in the specs
- [ ] Permit PDF reports generate correctly (replacing Crystal Reports)
- [ ] No Web Forms, DataSet, or Crystal Reports dependencies in solution
- [ ] APPMODLAB.md follows the template format with complete frontmatter
- [ ] Step-by-step guide is clear and reproducible
- [ ] All code compiles and runs without errors
