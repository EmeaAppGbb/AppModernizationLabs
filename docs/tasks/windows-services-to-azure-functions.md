# Windows Services to Azure Functions

## Overview
- **Category:** Infrastructure Modernization
- **Priority:** P2
- **Languages:** C#/.NET
- **Repository Name:** appmodlab-windows-services-to-azure-functions
- **Organization:** EmeaAppGbb

## Objective
This lab teaches participants how to convert Windows Services running on on-premises servers to serverless Azure Functions with appropriate triggers (timer, queue, HTTP, blob). It covers decomposing monolithic Windows Service logic into discrete functions, implementing proper retry and poison message handling, and managing state in a serverless context. This migration eliminates server maintenance costs and enables pay-per-execution pricing.

## Demo Legacy Application
**Business Domain:** Financial document processing and compliance reporting system for "Meridian Capital Advisors"

The legacy system consists of three Windows Services that run on a dedicated Windows Server: one for processing incoming financial documents (PDFs, CSVs), one for regulatory compliance report generation, and one for client portfolio valuation updates.

### Tech Stack
- .NET Framework 4.8 Windows Services (ServiceBase-derived)
- TopShelf for service installation and management
- FileSystemWatcher for monitoring incoming document folders
- System.Timers.Timer for scheduled tasks
- ADO.NET with stored procedures for SQL Server access
- SMTP client for email notifications
- Log4Net for logging to file and EventLog
- Configuration via app.config with custom config sections
- Windows Task Scheduler for some auxiliary jobs

### Key Files/Folders Structure
```
MeridianDocProcessor/
├── MeridianDocProcessor.sln
├── Meridian.DocumentProcessor/          # Windows Service 1
│   ├── DocumentProcessorService.cs      # ServiceBase with OnStart/OnStop
│   ├── App.config                       # Connection strings, folder paths
│   ├── FileWatcher/
│   │   ├── IncomingDocWatcher.cs        # FileSystemWatcher for new documents
│   │   ├── PdfParser.cs                 # PDF text extraction
│   │   └── CsvImporter.cs              # CSV financial data import
│   ├── Processing/
│   │   ├── DocumentClassifier.cs        # Classifies document type
│   │   ├── DataExtractor.cs             # Extracts financial data from docs
│   │   └── ComplianceChecker.cs         # Validates against compliance rules
│   └── Notifications/
│       └── EmailNotifier.cs             # SMTP notifications on processing
├── Meridian.ComplianceReporter/          # Windows Service 2
│   ├── ComplianceReporterService.cs     # Timer-based report generation
│   ├── Reports/
│   │   ├── DailyComplianceReport.cs     # Daily EOD compliance report
│   │   ├── WeeklyRiskReport.cs          # Weekly risk exposure summary
│   │   └── MonthlyAuditReport.cs        # Monthly audit trail report
│   └── Exporters/
│       ├── PdfExporter.cs               # Report to PDF
│       └── ExcelExporter.cs             # Report to Excel
├── Meridian.PortfolioValuation/          # Windows Service 3
│   ├── ValuationService.cs              # Timer-based portfolio updates
│   ├── MarketDataFeed/
│   │   ├── PriceFetcher.cs              # HTTP calls to market data provider
│   │   └── FeedParser.cs               # Parse market data responses
│   ├── Calculations/
│   │   ├── PortfolioCalculator.cs       # NAV, returns, risk calculations
│   │   └── BenchmarkComparator.cs       # Compare against benchmarks
│   └── Notifications/
│       └── AlertService.cs              # Alert on threshold breaches
├── Meridian.Shared/
│   ├── Data/
│   │   ├── SqlHelper.cs                 # ADO.NET helper (static class)
│   │   └── StoredProcedures.cs          # SP name constants
│   └── Models/                          # Shared data models
└── Database/
    └── StoredProcedures/                # 30+ stored procedures
```

### Database Schema
- **Documents** (DocumentId, FileName, DocumentType, ReceivedDate, ProcessedDate, Status, ClientId, ExtractedData)
- **ComplianceChecks** (CheckId, DocumentId, RuleId, Result, Details, CheckedDate)
- **ComplianceRules** (RuleId, Name, Category, Expression, Severity, IsActive)
- **Portfolios** (PortfolioId, ClientId, Name, Benchmark, InceptionDate, Currency)
- **Holdings** (HoldingId, PortfolioId, SecurityId, Quantity, CostBasis, AsOfDate)
- **Valuations** (ValuationId, PortfolioId, AsOfDate, NAV, DailyReturn, MTDReturn, YTDReturn)
- **MarketData** (SecurityId, Date, ClosePrice, Volume, Source)
- **Alerts** (AlertId, PortfolioId, AlertType, Message, CreatedDate, AcknowledgedDate)

### Legacy Anti-Patterns Present
- Three monolithic Windows Services with OnStart/OnStop lifecycle
- FileSystemWatcher for event-driven processing (fragile, misses events under load)
- System.Timers.Timer for scheduling (no distributed timer coordination)
- Static SqlHelper class with hardcoded connection strings
- Log4Net with XML configuration sprawl
- No retry logic for failed document processing
- SMTP client instantiated per notification (no connection pooling)
- Shared folder on network drive for incoming documents
- No health monitoring — services fail silently
- Manual installation via sc.exe or TopShelf installer

## Target Architecture
- **Runtime:** Azure Functions v4 on .NET 9 (isolated worker model)
- **Triggers:**
  - Blob trigger for document processing (replacing FileSystemWatcher)
  - Timer trigger for scheduled reports (replacing System.Timers.Timer)
  - Timer trigger for portfolio valuation (replacing System.Timers.Timer)
  - Queue trigger for document classification pipeline
  - HTTP trigger for on-demand report generation
- **Storage:** Azure Blob Storage for documents, Azure Queue Storage for pipeline
- **Database:** Azure SQL Database
- **Monitoring:** Application Insights with Azure Functions monitoring
- **Email:** Azure Communication Services (replacing SMTP)
- **Market Data:** Azure Functions HTTP trigger calling market data APIs
- **Orchestration:** Durable Functions for multi-step document processing

### Architecture Description
Each Windows Service is decomposed into discrete Azure Functions based on trigger type. The FileSystemWatcher becomes a Blob trigger that fires when documents land in a storage container. Timer-based report generation becomes timer-triggered functions. The multi-step document processing pipeline uses Durable Functions for orchestration with automatic retry. Queue triggers decouple classification from extraction. Application Insights replaces Log4Net. The static SqlHelper becomes EF Core with DI. No servers to manage, patch, or monitor.

## Branch Structure
- `main` — Contains the completed lab with full APPMODLAB.md documentation
- `legacy` — The three Windows Services (runnable as console apps for development)
- `solution` — The Azure Functions solution
- `step-1-decompose` — Identify function boundaries from Windows Service logic
- `step-2-blob-functions` — Document processing with Blob + Queue triggers
- `step-3-timer-functions` — Scheduled reports and portfolio valuation
- `step-4-durable-functions` — Orchestration for multi-step document pipeline
- `step-5-deploy` — Azure deployment with all supporting services

## Lab Content Requirements (APPMODLAB.md)
### Learning Objectives
- Decompose Windows Service logic into discrete Azure Functions
- Choose appropriate triggers (blob, timer, queue, HTTP) for each workload
- Implement Durable Functions for multi-step orchestration with retry
- Migrate from file system operations to Azure Blob Storage
- Set up monitoring and alerting with Application Insights

### Prerequisites
- C# and .NET experience
- Basic understanding of Azure Functions concepts
- .NET 9 SDK
- Azure Functions Core Tools v4
- Azure subscription
- Visual Studio 2022 or VS Code with Azure Functions extension

### Step-by-Step Instructions Outline
1. **Explore Windows Services** — Run services as console apps, process sample documents
2. **Map to Functions** — Document which parts become which trigger type
3. **Build Blob-Triggered Functions** — Document upload processing with Blob triggers
4. **Build Queue Pipeline** — Classification → extraction → compliance check via queues
5. **Build Timer Functions** — Scheduled compliance reports and portfolio valuation
6. **Add Durable Orchestration** — Multi-step document processing with retry
7. **Add Monitoring** — Application Insights, alerts, and Function metrics
8. **Deploy** — Azure Functions App + Storage + SQL via Bicep

### Estimated Duration
4–6 hours

### Key Concepts Covered
- Windows Service to Azure Functions migration
- Serverless trigger selection
- Durable Functions orchestration
- Blob Storage event processing
- Serverless monitoring patterns

## What the Squad Needs to Build
1. **Legacy App Setup:** Build three Windows Services using TopShelf that process financial documents, generate compliance reports, and update portfolio valuations. Include sample PDF/CSV documents and stored procedures. Services must be runnable as console apps for development.
2. **Modernization Implementation:** Decompose into Azure Functions with appropriate triggers, implement Durable Functions orchestration, integrate Blob Storage for documents, and replace SMTP with Azure Communication Services. All processing logic must produce identical results.
3. **Lab Documentation:** APPMODLAB.md with Windows Service to Azure Functions mapping guide, trigger selection decision tree, and Durable Functions orchestration patterns.
4. **Infrastructure as Code:** Bicep templates for Azure Functions App (Consumption plan), Azure Storage Account, Azure SQL Database, Application Insights, and Azure Communication Services.
5. **CI/CD:** GitHub Actions for building, testing, and deploying Azure Functions.

## Acceptance Criteria
- [ ] All three Windows Services run as console apps and process sample data
- [ ] Document processing correctly classifies, extracts, and validates
- [ ] Compliance reports generate on schedule
- [ ] Portfolio valuations calculate correctly
- [ ] Azure Functions implement all Windows Service functionality
- [ ] Blob triggers fire on document upload
- [ ] Durable Functions orchestration handles multi-step pipeline
- [ ] Timer functions execute on configured schedules
- [ ] APPMODLAB.md follows the template format with complete frontmatter
- [ ] Step-by-step guide is clear and reproducible
- [ ] All code compiles and runs without errors
