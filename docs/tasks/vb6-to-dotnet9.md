# VB6 to .NET 9 Modernization

## Overview
- **Category:** Code Modernization
- **Priority:** P2
- **Languages:** Visual Basic 6 → C#/.NET 9
- **Repository Name:** appmodlab-vb6-to-dotnet9
- **Organization:** EmeaAppGbb

## Objective
This lab demonstrates migrating a Visual Basic 6 desktop application to .NET 9, targeting either WinForms/WPF for desktop or Blazor for a web-based alternative. VB6 applications remain prevalent in manufacturing, finance, and government sectors, often running critical business processes. This lab covers COM interop elimination, ADO/DAO-to-EF Core migration, ActiveX control replacement, and the decision framework for choosing between desktop and web modernization targets.

## Demo Legacy Application
**Business Domain:** Small manufacturing company inventory and work order tracking system for "Precision Parts Manufacturing"

The legacy VB6 application manages raw material inventory, production work orders, finished goods tracking, quality control inspections, and shipping manifests. It's been the backbone of shop floor operations since 2001.

### Tech Stack
- Visual Basic 6 SP6 with Windows Common Controls
- ADO 2.8 for database access (ADODB.Connection, ADODB.Recordset)
- Microsoft Access 2003 database (MDB file) — single file on network share
- Crystal Reports 8.5 for printing work orders and shipping labels
- MSFlexGrid for data display
- CommonDialog control for file operations
- Windows API calls (user32.dll, kernel32.dll) for printer and system info
- Registry-based application settings
- Single-user with file-locking for concurrent access

### Key Files/Folders Structure
```
PrecisionParts/
├── PrecisionParts.vbp                # VB6 project file
├── PrecisionParts.vbw                # VB6 workspace
├── Forms/
│   ├── frmMain.frm                   # MDI parent form with menu bar
│   ├── frmInventory.frm              # Raw material inventory grid
│   ├── frmWorkOrder.frm              # Work order entry/editing
│   ├── frmQualityCheck.frm           # QC inspection form
│   ├── frmShipping.frm               # Shipping manifest
│   ├── frmPartLookup.frm             # Part number search dialog
│   ├── frmReports.frm                # Report selection and preview
│   └── frmLogin.frm                  # Simple login form
├── Modules/
│   ├── modDatabase.bas               # Global ADO connection and helper functions
│   ├── modGlobals.bas                # Global variables and constants
│   ├── modPrinting.bas               # Windows API printing functions
│   └── modUtilities.bas              # String/date/number utilities
├── Classes/
│   ├── clsWorkOrder.cls              # Work order business logic
│   ├── clsInventory.cls              # Inventory calculations (reorder points)
│   └── clsPart.cls                   # Part number validation
├── Reports/
│   ├── rptWorkOrder.rpt              # Crystal Report for work order printout
│   ├── rptShippingLabel.rpt          # Shipping label template
│   └── rptInventoryStatus.rpt        # Inventory status report
├── Database/
│   └── PrecisionParts.mdb            # Access database file
└── Resources/
    └── icons/                        # Toolbar icons (BMP format)
```

### Database Schema (MS Access)
- **Parts** (PartNumber, Description, Material, UnitOfMeasure, Weight, DrawingNumber, RevisionLevel)
- **RawMaterials** (MaterialID, Description, Supplier, QuantityOnHand, ReorderPoint, UnitCost, Location)
- **WorkOrders** (WorkOrderID, PartNumber, Quantity, DueDate, StartDate, Status, Priority, CustomerPO)
- **QualityChecks** (CheckID, WorkOrderID, Inspector, CheckDate, Result, Measurements, Notes)
- **ShippingManifests** (ManifestID, WorkOrderID, ShipDate, Carrier, TrackingNumber, Weight, Boxes)
- **Customers** (CustomerID, CompanyName, Contact, Phone, Address, PaymentTerms)
- **Users** (UserID, Username, Password, Role) — plain text passwords

### Legacy Anti-Patterns Present
- Global variables in .bas modules (modGlobals.bas with 50+ public variables)
- GoTo-based error handling (On Error GoTo ErrHandler)
- ADO Recordset used as both data access and business object
- File-based Access database on network share (concurrency via file locking)
- Windows API calls for printer management (P/Invoke-style)
- Registry-based settings (SaveSetting/GetSetting)
- ActiveX controls (MSFlexGrid, CommonDialog) with no modern equivalent
- Crystal Reports 8.5 dependency (unsupported version)
- No unit testing capability
- Single-threaded UI with DoEvents calls for responsiveness
- Hardcoded connection strings and file paths

## Target Architecture
- **Option A (Desktop):** .NET 9 WPF application with MVVM pattern
- **Option B (Web):** .NET 9 Blazor Server application
- **Data Access:** Entity Framework Core 9 with SQL Server (replacing Access MDB)
- **Reporting:** QuestPDF for PDF generation
- **Configuration:** appsettings.json (replacing registry)
- **Authentication:** ASP.NET Core Identity (for Blazor) or Windows Auth (for WPF)
- **Database:** Azure SQL Database (replacing Access MDB)
- **Desktop Deployment:** MSIX packaging or ClickOnce (for WPF option)
- **Web Hosting:** Azure App Service (for Blazor option)

### Architecture Description
The VB6 forms-over-data application is restructured with proper separation of concerns. Global variables become injected services. ADO Recordsets become EF Core entities with repository pattern. MSFlexGrid is replaced with WPF DataGrid (or Blazor table components). Crystal Reports become QuestPDF-generated PDFs. The Access database migrates to SQL Server / Azure SQL. The lab presents both desktop (WPF) and web (Blazor) modernization paths, letting participants choose based on their scenario.

## Branch Structure
- `main` — Contains the completed lab with full APPMODLAB.md documentation
- `legacy` — The VB6 application (source code + compiled EXE + Access database)
- `solution` — The fully modernized .NET 9 application (Blazor version as primary)
- `solution-wpf` — Alternative WPF desktop version
- `step-1-database-migration` — Access MDB to SQL Server migration scripts
- `step-2-domain-model` — EF Core entities and DbContext (replacing ADO/global modules)
- `step-3-ui-migration` — Blazor/WPF UI replacing VB6 forms
- `step-4-reports-and-printing` — QuestPDF replacing Crystal Reports
- `step-5-deploy` — Azure deployment (App Service for Blazor, MSIX for WPF)

## Lab Content Requirements (APPMODLAB.md)
### Learning Objectives
- Assess a VB6 application for modernization scope and complexity
- Migrate Microsoft Access database to SQL Server / Azure SQL
- Translate VB6 forms and business logic to C# with proper OOP patterns
- Replace global state with dependency injection
- Choose between desktop (WPF) and web (Blazor) modernization targets

### Prerequisites
- Basic VB6 reading knowledge (no VB6 IDE needed — source is reviewed, not compiled)
- C# and .NET experience
- .NET 9 SDK
- SQL Server LocalDB or Azure SQL
- Visual Studio 2022
- Azure subscription (for web deployment option)

### Step-by-Step Instructions Outline
1. **Analyze the Legacy App** — Review VB6 source code, run the compiled EXE, understand business workflows
2. **Migrate Database** — Convert Access MDB to SQL Server using migration scripts
3. **Create Domain Model** — Build EF Core entities from Access schema, implement repository pattern
4. **Build UI Shell** — Create Blazor (or WPF) application shell matching VB6 MDI layout
5. **Migrate Forms** — Convert each VB6 form to a Blazor component (or WPF view)
6. **Replace Reports** — Build QuestPDF templates for work orders and shipping labels
7. **Add Authentication** — Implement ASP.NET Core Identity (replacing plain-text login)
8. **Deploy** — Deploy Blazor to Azure App Service (or package WPF as MSIX)

### Estimated Duration
5–7 hours

### Key Concepts Covered
- VB6 to C# translation patterns
- Access to SQL Server database migration
- Desktop vs. web modernization decision framework
- Global state elimination with DI
- ActiveX control replacement strategies

## What the Squad Needs to Build
1. **Legacy App Setup:** Provide VB6 source code and a pre-compiled EXE that runs on Windows. Include the Access MDB with seed data (100+ parts, 50+ work orders, sample QC records). Document the VB6 app with screenshots for participants who can't run VB6.
2. **Modernization Implementation:** Build both Blazor Server and WPF versions of the application. Both must implement all features — inventory management, work order tracking, QC inspection, shipping, and reporting. Blazor is the primary solution; WPF is the alternative branch.
3. **Lab Documentation:** APPMODLAB.md with VB6-to-C# mapping guide, decision framework for choosing desktop vs. web, and step-by-step migration for both targets.
4. **Infrastructure as Code:** Bicep templates for Azure App Service (Blazor), Azure SQL Database, and Azure Blob Storage (for any file attachments).
5. **CI/CD:** GitHub Actions workflow for building and deploying the Blazor application.

## Acceptance Criteria
- [ ] VB6 EXE runs on Windows and demonstrates all business workflows
- [ ] Access database contains realistic seed data
- [ ] SQL Server migration scripts convert Access schema and data correctly
- [ ] Blazor application implements all VB6 form features
- [ ] WPF alternative implements all VB6 form features
- [ ] QuestPDF reports match Crystal Reports output format
- [ ] No VB6, COM, or ActiveX dependencies in solution branches
- [ ] APPMODLAB.md follows the template format with complete frontmatter
- [ ] Step-by-step guide is clear and reproducible
- [ ] All code compiles and runs without errors
