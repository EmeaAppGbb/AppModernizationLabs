# Legacy ETL to Azure Data Factory

## Overview
- **Category:** Data Modernization
- **Priority:** P2
- **Languages:** SQL/Python/SSIS
- **Repository Name:** appmodlab-legacy-etl-to-azure-data-factory
- **Organization:** EmeaAppGbb

## Objective
This lab guides participants through modernizing legacy ETL pipelines (SSIS packages) to Azure Data Factory (ADF). It covers converting SSIS data flows to ADF pipelines and mapping data flows, migrating complex transformations, handling incremental loads, implementing proper error handling and monitoring, and leveraging ADF's integration runtime for hybrid connectivity. This is critical for organizations modernizing their data platforms to Azure.

## Demo Legacy Application
**Business Domain:** National retail analytics data warehouse ETL for "Brightfield Retail Analytics"

The legacy ETL system uses 30+ SSIS packages that extract data from 5 source systems (POS, inventory, CRM, e-commerce, supplier portal), transform it, and load it into a SQL Server data warehouse nightly.

### Tech Stack
- SQL Server Integration Services (SSIS) 2016
- SSIS Package Store on SQL Server
- SQL Server Agent for scheduling
- SQL Server 2016 data warehouse (destination)
- Source systems: SQL Server, Oracle, flat files (CSV), REST APIs, Excel
- SSIS Script Tasks (C#) for custom transformations
- SSIS connection managers with Windows Authentication
- SSISDB catalog for deployment and execution

### Key Files/Folders Structure
```
BrightfieldETL/
├── SSIS-Packages/
│   ├── Master/
│   │   └── MasterOrchestrator.dtsx        # Master package orchestrating all ETL
│   ├── Extract/
│   │   ├── Extract_POS_Sales.dtsx          # POS system extraction
│   │   ├── Extract_Inventory.dtsx          # Inventory system extraction
│   │   ├── Extract_CRM_Customers.dtsx      # CRM data extraction
│   │   ├── Extract_Ecommerce_Orders.dtsx   # E-commerce REST API extraction
│   │   └── Extract_Supplier_Files.dtsx     # CSV file import from FTP
│   ├── Transform/
│   │   ├── Transform_SalesFacts.dtsx       # Sales fact table transformations
│   │   ├── Transform_InventoryFacts.dtsx   # Inventory fact transformations
│   │   ├── Transform_CustomerDim.dtsx      # Customer SCD Type 2
│   │   ├── Transform_ProductDim.dtsx       # Product dimension processing
│   │   ├── Transform_TimeDim.dtsx          # Time dimension maintenance
│   │   └── Transform_DataQuality.dtsx      # Data quality rules and cleansing
│   ├── Load/
│   │   ├── Load_FactSales.dtsx             # Fact table bulk load
│   │   ├── Load_FactInventory.dtsx         # Inventory fact load
│   │   ├── Load_Dimensions.dtsx            # Dimension table loads
│   │   └── Load_Aggregations.dtsx          # Pre-computed aggregations
│   └── Utility/
│       ├── Audit_Framework.dtsx            # ETL audit logging
│       └── ErrorHandling.dtsx              # Error row routing
├── SQL/
│   ├── StagingTables/                      # Staging area DDL
│   ├── DataWarehouse/                      # DW schema (star schema)
│   ├── StoredProcedures/                   # Pre/post-load procedures
│   └── IncrementalLoad/                    # Change tracking queries
├── Config/
│   ├── Connections.dtsConfig               # SSIS package configurations
│   └── Environment.xml                     # SSISDB environment variables
└── Documentation/
    ├── DataFlowDiagram.vsd                 # Visio data flow diagrams
    └── SourceToTargetMapping.xlsx          # Column mapping spreadsheet
```

### Data Warehouse Schema (Star Schema)
- **FactSales** (DateKey, StoreKey, ProductKey, CustomerKey, Quantity, Revenue, Discount, Cost, TransactionId)
- **FactInventory** (DateKey, StoreKey, ProductKey, QuantityOnHand, QuantityOnOrder, DaysOfSupply)
- **DimCustomer** (CustomerKey, CustomerId, Name, Email, Segment, LoyaltyTier, StartDate, EndDate, IsCurrent) — SCD Type 2
- **DimProduct** (ProductKey, ProductId, Name, Category, SubCategory, Brand, UnitPrice, StartDate, EndDate, IsCurrent)
- **DimStore** (StoreKey, StoreId, Name, Address, City, State, Region, Format)
- **DimDate** (DateKey, Date, DayOfWeek, Month, Quarter, Year, IsHoliday, FiscalPeriod)
- **Staging** tables mirror source system structures

### Legacy Anti-Patterns
- Monolithic master package orchestrating 30+ packages sequentially
- SSIS Script Tasks with complex C# code (hard to maintain, no unit testing)
- Flat file connections to FTP shares with hardcoded paths
- Windows Authentication making cloud migration harder
- SSIS package configurations in XML files (environment-specific)
- No incremental load for several sources (full reload nightly)
- Error rows written to flat files with no automated alerting
- SCD Type 2 implemented with custom SSIS components (not standard)
- No data quality framework — quality rules embedded in packages
- Sequential execution — no parallelism between independent data flows

## Target Architecture
- **ETL Platform:** Azure Data Factory with Mapping Data Flows
- **Orchestration:** ADF pipelines with dependency chains and parallel execution
- **Storage:** Azure Data Lake Storage Gen2 (staging area)
- **Data Warehouse:** Azure Synapse Analytics (or Azure SQL Database)
- **Transformations:** ADF Mapping Data Flows for visual transformations, Databricks for complex logic
- **Incremental Load:** ADF Change Data Capture or watermark patterns
- **Monitoring:** ADF Monitor + Azure Monitor alerts
- **Source Connectivity:** Self-hosted Integration Runtime for on-premises sources
- **File Ingestion:** ADF Copy Activity with SFTP connector (replacing FTP flat files)
- **Data Quality:** Great Expectations or ADF data flow assertions

### Architecture Description
SSIS packages are decomposed into ADF pipelines with proper dependency management and parallel execution. SSIS data flows become ADF Mapping Data Flows for visual transformations. Complex C# Script Tasks become Azure Databricks notebooks or Azure Functions. The master orchestrator becomes an ADF pipeline with Execute Pipeline activities and dependency conditions. Staging moves to ADLS Gen2 in Parquet format. SCD Type 2 uses ADF's built-in SCD transformation. Data quality rules become data flow assertions with alerting.

## Branch Structure
- `main` — Contains the completed lab with full APPMODLAB.md documentation
- `legacy` — SSIS packages (DTSX files), SQL scripts, configuration files
- `solution` — ADF pipeline definitions (ARM/Bicep), data flow definitions, Databricks notebooks
- `step-1-assessment` — SSIS package inventory and migration complexity analysis
- `step-2-infrastructure` — ADF, ADLS Gen2, Synapse provisioning
- `step-3-extract-migration` — Convert extraction packages to ADF Copy Activities
- `step-4-transform-migration` — Convert transformations to Mapping Data Flows
- `step-5-orchestration` — Master pipeline with dependency management and monitoring

## Lab Content Requirements (APPMODLAB.md)
### Learning Objectives
- Assess SSIS packages for ADF migration complexity
- Convert SSIS data flows to ADF Mapping Data Flows
- Implement incremental load patterns in ADF
- Handle SCD Type 2 dimensions in ADF
- Set up monitoring, alerting, and error handling for ADF pipelines

### Prerequisites
- SSIS development experience
- Basic Azure Data Factory knowledge
- Azure subscription with Contributor access
- SQL Server with SSISDB (for legacy packages)
- Azure Data Studio or SSMS

### Step-by-Step Instructions Outline
1. **Inventory SSIS Packages** — Catalog all packages, data flows, and dependencies
2. **Provision Azure Resources** — ADF, ADLS Gen2, Azure Synapse, Self-hosted IR
3. **Migrate Extractions** — Convert SSIS extract packages to ADF Copy Activities
4. **Migrate Transformations** — Convert data flows to ADF Mapping Data Flows
5. **Implement SCD Type 2** — Use ADF's Alter Row transformation for dimension processing
6. **Build Orchestration** — Create master pipeline with dependencies and parallel execution
7. **Add Incremental Loads** — Implement watermark-based incremental extraction
8. **Set Up Monitoring** — Configure alerts, diagnostic logging, and ADF Monitor dashboards
9. **Validate** — Compare ADF output with SSIS output for data accuracy

### Estimated Duration
6–8 hours

### Key Concepts Covered
- SSIS to ADF migration patterns
- Mapping Data Flow transformations
- Incremental load strategies
- SCD Type 2 in ADF
- Pipeline monitoring and alerting

## What the Squad Needs to Build
1. **Legacy App Setup:** SSIS packages (DTSX) demonstrating all described patterns — source extraction, transformations, SCD Type 2, data quality, and master orchestration. Include SQL scripts for staging tables, DW schema, and sample source data. Provide Docker-based SQL Server with SSISDB.
2. **Modernization Implementation:** Complete ADF pipeline definitions with Mapping Data Flows, Copy Activities, incremental load patterns, SCD Type 2 processing, and monitoring. Include Databricks notebooks for complex transformations. Export as ARM templates.
3. **Lab Documentation:** APPMODLAB.md with SSIS-to-ADF component mapping, Mapping Data Flow transformation guide, incremental load pattern documentation, and data validation procedures.
4. **Infrastructure as Code:** Bicep templates for Azure Data Factory, ADLS Gen2, Azure Synapse Analytics, Azure Databricks workspace, and Self-hosted Integration Runtime VM.
5. **CI/CD:** GitHub Actions for ADF ARM template deployment and pipeline trigger management.

## Acceptance Criteria
- [ ] SSIS packages run on SQL Server and produce correct DW output
- [ ] All source extractions converted to ADF Copy Activities
- [ ] Mapping Data Flows produce identical results to SSIS data flows
- [ ] SCD Type 2 processing works correctly in ADF
- [ ] Incremental loads extract only changed data
- [ ] Master pipeline orchestrates all activities with proper dependencies
- [ ] Monitoring alerts fire on pipeline failures
- [ ] APPMODLAB.md follows the template format with complete frontmatter
- [ ] Step-by-step guide is clear and reproducible
