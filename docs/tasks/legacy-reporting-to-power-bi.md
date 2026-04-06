# Legacy Reporting to Power BI

## Overview
- **Category:** Data Modernization
- **Priority:** P3
- **Languages:** SQL/DAX/M
- **Repository Name:** appmodlab-legacy-reporting-to-power-bi
- **Organization:** EmeaAppGbb

## Objective
This lab demonstrates modernizing legacy reporting systems (Crystal Reports and SQL Server Reporting Services) to Power BI. It covers converting report designs, translating report formulas to DAX, migrating data sources, implementing row-level security, and setting up automated refresh schedules. This enables self-service analytics, mobile access, and real-time dashboards — replacing static, IT-dependent report generation.

## Demo Legacy Application
**Business Domain:** Hospital operations and clinical quality reporting for "Clearwater Health System"

The legacy reporting system produces 40+ reports used by hospital administrators, clinical staff, and regulatory compliance officers. Reports include patient census, surgical outcomes, readmission rates, revenue cycle metrics, and regulatory submissions.

### Tech Stack
- Crystal Reports 2016 (20 reports) connected to Oracle 12c
- SQL Server Reporting Services 2016 (20 reports) connected to SQL Server 2016
- Reports distributed via email subscriptions and shared folders
- Crystal Reports Runtime on client PCs for interactive viewing
- SSRS Report Server with folder-based security
- Parameterized reports with cascading dropdowns
- Sub-reports and drill-through for detail navigation
- Scheduled snapshots for month-end report archival

### Key Files/Folders Structure
```
ClearwaterReporting/
├── CrystalReports/
│   ├── PatientCensus.rpt              # Real-time patient census by ward
│   ├── SurgicalOutcomes.rpt           # Surgical outcomes with sub-reports
│   ├── ReadmissionRates.rpt           # 30-day readmission analysis
│   ├── PhysicianProductivity.rpt      # Provider productivity metrics
│   ├── ORUtilization.rpt              # Operating room utilization
│   ├── InfectionControl.rpt           # HAI (Hospital-Acquired Infection) tracking
│   └── ... (14 more reports)
├── SSRS-Reports/
│   ├── RevenueCycle/
│   │   ├── DailyRevenue.rdl           # Daily revenue dashboard
│   │   ├── ClaimsDenials.rdl          # Claims denial analysis
│   │   ├── AgingReport.rdl            # Accounts receivable aging
│   │   └── PayerMix.rdl               # Payer mix analysis
│   ├── Clinical/
│   │   ├── QualityMetrics.rdl         # CMS quality measures
│   │   ├── MedicationErrors.rdl       # Medication error tracking
│   │   └── StaffingRatios.rdl         # Nurse-to-patient ratios
│   ├── Regulatory/
│   │   ├── CMSSubmission.rdl          # CMS regulatory submission
│   │   └── JointCommission.rdl        # Joint Commission compliance
│   └── ... (11 more reports)
├── DataSources/
│   ├── OracleConnection.config        # Oracle connection for Crystal
│   └── SQLServerConnection.config     # SQL Server connection for SSRS
├── SQL/
│   ├── StoredProcedures/              # Report stored procedures
│   ├── Views/                         # Reporting views
│   └── Functions/                     # UDFs for calculations
└── Subscriptions/
    └── SubscriptionConfig.xml         # Email distribution schedules
```

### Report Characteristics
- Crystal Reports use complex formulas (WhilePrintingRecords, RunningTotal)
- SSRS reports use complex expressions and custom code blocks
- Sub-reports for drill-through (e.g., patient census → individual patient detail)
- Cross-tab/matrix reports for physician productivity
- Cascading parameters (select department → filter by physician)
- Conditional formatting based on thresholds (red/yellow/green)
- Report snapshots archived monthly for regulatory compliance

### Legacy Anti-Patterns
- Crystal Reports Runtime dependency on client PCs
- Mixed report platforms (Crystal + SSRS) with different skill requirements
- Stored procedures embedding business logic that should be in a semantic model
- Email-based distribution with no interactivity
- No self-service — every report change requires IT involvement
- Cascading parameters causing slow report rendering
- Sub-report queries executing N+1 database calls
- No mobile-friendly report access
- Static PDF exports with no drill-down capability
- Row-level security implemented in stored procedures per report

## Target Architecture
- **Analytics Platform:** Power BI Service (Premium or Pro)
- **Semantic Model:** Power BI dataset with DAX measures (single source of truth)
- **Reports:** Power BI reports and paginated reports (for pixel-perfect regulatory)
- **Data Source:** Azure SQL Database or Azure Synapse (consolidated from Oracle + SQL Server)
- **Row-Level Security:** Power BI RLS with DAX filters
- **Distribution:** Power BI App workspace with role-based access
- **Mobile:** Power BI mobile app for tablet/phone access
- **Refresh:** Scheduled refresh with incremental refresh for large datasets
- **Embedding:** Power BI Embedded for intranet portal integration
- **Governance:** Microsoft Purview for data lineage and governance

### Architecture Description
Crystal Reports and SSRS reports are consolidated into a single Power BI workspace. A unified semantic model (Power BI dataset) replaces scattered stored procedures with DAX measures. Crystal Reports formulas are translated to DAX. SSRS paginated reports (for regulatory submissions) are migrated to Power BI paginated reports. Row-level security is centralized in the dataset. Distribution shifts from email subscriptions to Power BI App with push notifications. Interactive dashboards replace static PDF exports.

## Branch Structure
- `main` — Contains the completed lab with full APPMODLAB.md documentation
- `legacy` — Crystal Reports (.rpt), SSRS reports (.rdl), stored procedures, sample data
- `solution` — Power BI Desktop files (.pbix), paginated reports (.rdl for PBI), DAX measures
- `step-1-assessment` — Report inventory, usage analysis, and conversion priority
- `step-2-semantic-model` — Power BI dataset with DAX measures replacing stored procs
- `step-3-report-conversion` — Crystal/SSRS reports converted to Power BI
- `step-4-rls-and-distribution` — Row-level security and Power BI App setup
- `step-5-paginated-reports` — Regulatory reports as Power BI paginated reports

## Lab Content Requirements (APPMODLAB.md)
### Learning Objectives
- Assess legacy reports for Power BI migration complexity and priority
- Build a unified semantic model with DAX measures
- Convert Crystal Reports formulas and SSRS expressions to DAX
- Implement row-level security in Power BI datasets
- Set up Power BI Apps for enterprise report distribution

### Prerequisites
- Basic Crystal Reports or SSRS familiarity (reading knowledge)
- Power BI Desktop experience
- DAX fundamentals
- Azure subscription (for Power BI Service)
- SQL Server and/or Oracle (for legacy report data sources)

### Step-by-Step Instructions Outline
1. **Inventory Reports** — Catalog all Crystal and SSRS reports, assess usage and complexity
2. **Consolidate Data Sources** — Create Azure SQL views replacing report-specific stored procedures
3. **Build Semantic Model** — Create Power BI dataset connecting to Azure SQL, define DAX measures
4. **Convert Key Reports** — Migrate top 10 reports from Crystal/SSRS to Power BI visuals
5. **Implement RLS** — Define row-level security roles and DAX filters in the dataset
6. **Migrate Paginated Reports** — Convert regulatory reports to Power BI paginated reports
7. **Set Up Distribution** — Create Power BI App workspace, configure access and subscriptions
8. **Configure Refresh** — Set up scheduled and incremental refresh
9. **Validate** — Compare Power BI output with legacy reports for accuracy

### Estimated Duration
4–6 hours

### Key Concepts Covered
- Legacy report migration strategy
- DAX measure development
- Crystal Reports formula translation
- Power BI row-level security
- Enterprise report distribution

## What the Squad Needs to Build
1. **Legacy App Setup:** Provide Crystal Reports (.rpt) and SSRS reports (.rdl) with sample data in SQL Server. Include stored procedures used by the reports. Provide screenshots of rendered reports for comparison. Crystal Reports viewer not required — screenshots and .rpt source are sufficient.
2. **Modernization Implementation:** Power BI Desktop files (.pbix) with semantic model, DAX measures, report pages, RLS configuration, and paginated reports for regulatory use. Include a data source consolidation script.
3. **Lab Documentation:** APPMODLAB.md with Crystal-to-Power BI formula translation guide, SSRS-to-Power BI expression mapping, RLS implementation guide, and report distribution setup.
4. **Infrastructure as Code:** Bicep templates for Azure SQL Database (data source) and Power BI Embedded capacity (optional).
5. **CI/CD:** GitHub Actions for database schema deployment and Power BI dataset refresh trigger.

## Acceptance Criteria
- [ ] Legacy Crystal and SSRS reports render with sample data (screenshots provided)
- [ ] Power BI semantic model connects to Azure SQL with correct relationships
- [ ] DAX measures produce identical results to report stored procedures
- [ ] Top 10 reports migrated to interactive Power BI visuals
- [ ] Regulatory reports available as Power BI paginated reports
- [ ] Row-level security restricts data by department/role
- [ ] Power BI App configured with appropriate workspace roles
- [ ] APPMODLAB.md follows the template format with complete frontmatter
- [ ] Step-by-step guide is clear and reproducible
