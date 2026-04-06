# On-Prem Data Warehouse to Azure Synapse

## Overview
- **Category:** Data Modernization
- **Priority:** P2
- **Languages:** SQL/Python
- **Repository Name:** appmodlab-on-prem-data-warehouse-to-azure-synapse
- **Organization:** EmeaAppGbb

## Objective
This lab teaches participants how to migrate an on-premises SQL Server data warehouse to Azure Synapse Analytics. It covers schema migration with distribution and partitioning optimization, data migration using PolyBase or COPY INTO, query performance tuning for MPP architecture, and integration with Azure Data Lake for a modern lakehouse pattern. This enables organizations to eliminate on-premises hardware costs and leverage cloud-scale analytics.

## Demo Legacy Application
**Business Domain:** Telecommunications customer analytics and network performance data warehouse for "NexWave Telecom"

The on-premises data warehouse stores 5 years of CDR (Call Detail Records), network performance metrics, customer behavior analytics, and revenue data. It serves business intelligence dashboards, ad-hoc analyst queries, and monthly regulatory reports.

### Tech Stack
- SQL Server 2016 Enterprise Edition (on-premises, 8 TB database)
- Star schema with 15 fact tables and 25 dimension tables
- Columnstore indexes for analytical queries
- Partitioned tables by month
- SQL Server Agent for ETL scheduling
- SSRS for operational reports
- Power BI Desktop connecting via DirectQuery
- SMP (Symmetric Multi-Processing) architecture — single server

### Database Schema
**Fact Tables:**
- **FactCDR** (DateKey, TimeKey, CallerKey, CalledPartyKey, CellTowerKey, Duration, DataUsageMB, CallType, Revenue, Cost) — 2B+ rows
- **FactNetworkPerformance** (DateKey, TimeKey, CellTowerKey, Latency, Throughput, PacketLoss, Availability, ErrorCount) — 500M+ rows
- **FactBilling** (DateKey, CustomerKey, PlanKey, AmountBilled, AmountPaid, AdjustmentAmount, TaxAmount)
- **FactChurn** (DateKey, CustomerKey, ChurnReason, PredictedProbability, ActualChurn, WinbackOffer)

**Dimension Tables:**
- **DimCustomer** (CustomerKey, CustomerId, Name, Segment, Plan, StartDate, EndDate, IsCurrent) — SCD Type 2
- **DimCellTower** (TowerKey, TowerId, Location, Latitude, Longitude, Technology, Capacity, Region)
- **DimDate/DimTime** (standard date/time dimensions)
- **DimPlan** (PlanKey, PlanId, Name, MonthlyFee, DataCap, VoiceMinutes, Category)

### Legacy Challenges
- 8 TB database on single SMP server — query performance degradation
- Columnstore indexes require careful maintenance (reorganize/rebuild)
- Partitioning by month with manual partition management
- Full table scans on large fact tables for ad-hoc queries
- DirectQuery from Power BI causing concurrency issues
- No separation between serving and transformation workloads
- Manual capacity planning (can't scale compute independently from storage)
- SSRS reports running against DW directly (competing for resources)

## Target Architecture
- **Analytics Engine:** Azure Synapse Analytics (Dedicated SQL Pool)
- **Storage:** Azure Data Lake Storage Gen2 (for raw and staged data)
- **Data Migration:** PolyBase or COPY INTO for bulk data loading
- **Distribution:** Hash distribution on fact tables, replicated for small dimensions
- **Partitioning:** Range partitioning aligned with source
- **Serving Layer:** Synapse Serverless SQL Pool for ad-hoc queries
- **Reporting:** Power BI with Import mode (replacing DirectQuery)
- **Orchestration:** Synapse Pipelines for ongoing ETL
- **Monitoring:** Synapse SQL Analytics + Azure Monitor

### Architecture Description
The SMP data warehouse becomes an MPP (Massively Parallel Processing) system on Synapse Dedicated SQL Pool. Tables are distributed (hash, round-robin, or replicated) based on query patterns. Data is first migrated to ADLS Gen2 in Parquet format, then loaded into Synapse via COPY INTO. Synapse Serverless SQL Pool provides a cost-effective layer for ad-hoc exploration. Power BI switches from DirectQuery to Import mode with incremental refresh. SSRS reports migrate to Power BI paginated reports. Compute can scale independently from storage.

## Branch Structure
- `main` — Contains the completed lab with full APPMODLAB.md documentation
- `legacy` — SQL Server DW DDL, sample data generation scripts, SSRS reports
- `solution` — Synapse DDL with distribution/partitioning, migration scripts, Power BI reports
- `step-1-assessment` — Schema analysis and distribution strategy planning
- `step-2-synapse-provision` — Synapse workspace and dedicated pool provisioning
- `step-3-schema-migration` — DDL conversion with distribution and indexing
- `step-4-data-migration` — PolyBase/COPY INTO data loading from ADLS Gen2
- `step-5-optimization` — Query tuning, statistics, result set caching

## Lab Content Requirements (APPMODLAB.md)
### Learning Objectives
- Analyze SMP data warehouse schemas for MPP distribution strategy
- Choose optimal distribution keys for fact and dimension tables
- Migrate data using PolyBase or COPY INTO via ADLS Gen2
- Tune queries for Synapse MPP architecture
- Configure Power BI with import mode and incremental refresh

### Prerequisites
- SQL Server DBA/DW experience
- Basic Synapse Analytics knowledge
- Azure subscription with Contributor access
- Power BI Desktop installed
- Azure Data Studio

### Step-by-Step Instructions Outline
1. **Analyze Source DW** — Inventory tables, query patterns, data volumes, and dependencies
2. **Design Distribution Strategy** — Choose hash/replicated/round-robin for each table
3. **Provision Synapse** — Create workspace, dedicated SQL pool, ADLS Gen2 storage
4. **Convert Schema** — Create Synapse DDL with DISTRIBUTION and PARTITION clauses
5. **Migrate Data** — Export to Parquet in ADLS Gen2, load via COPY INTO
6. **Tune Performance** — Create statistics, optimize key queries, enable result set caching
7. **Configure Power BI** — Switch to Import mode, set up incremental refresh
8. **Set Up Serverless** — Create serverless views over ADLS Gen2 for ad-hoc exploration
9. **Validate** — Compare query results and performance between source and Synapse

### Estimated Duration
5–7 hours

### Key Concepts Covered
- SMP to MPP migration strategy
- Table distribution design
- PolyBase and COPY INTO data loading
- Synapse query optimization
- Lakehouse architecture

## What the Squad Needs to Build
1. **Legacy App Setup:** SQL Server DW DDL with star schema, sample data generation scripts (create realistic telecom data for 1M+ rows), columnstore indexes, partition functions, and SSRS report definitions. Include Power BI Desktop file with DirectQuery connection.
2. **Modernization Implementation:** Synapse DDL with optimal distribution, ADLS Gen2 staging scripts, COPY INTO migration scripts, performance tuning queries, Synapse Serverless views, and Power BI Import mode reports with incremental refresh.
3. **Lab Documentation:** APPMODLAB.md with distribution strategy decision guide, data migration playbook, performance comparison benchmarks, and Power BI migration walkthrough.
4. **Infrastructure as Code:** Bicep templates for Azure Synapse workspace, Dedicated SQL Pool, ADLS Gen2, and Power BI Embedded (optional).
5. **CI/CD:** GitHub Actions for Synapse DDL deployment and data validation.

## Acceptance Criteria
- [ ] Source DW schema created with star schema and sample data
- [ ] Distribution strategy documented for every table
- [ ] Synapse DDL creates tables with correct distribution and partitioning
- [ ] Data loads successfully via COPY INTO from ADLS Gen2
- [ ] Key analytical queries return correct results on Synapse
- [ ] Power BI report works with Import mode and incremental refresh
- [ ] Synapse Serverless views work for ad-hoc exploration
- [ ] APPMODLAB.md follows the template format with complete frontmatter
- [ ] Step-by-step guide is clear and reproducible
