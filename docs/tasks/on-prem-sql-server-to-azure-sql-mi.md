# On-Prem SQL Server to Azure SQL Managed Instance

## Overview
- **Category:** Infrastructure Modernization
- **Priority:** P1
- **Languages:** SQL Server/T-SQL
- **Repository Name:** appmodlab-on-prem-sql-server-to-azure-sql-mi
- **Organization:** EmeaAppGbb

## Objective
This lab teaches participants how to migrate on-premises SQL Server databases to Azure SQL Managed Instance with minimal downtime. It covers pre-migration assessment using Azure Migrate and Data Migration Assistant, schema and data migration using Azure Database Migration Service (DMS), handling of SQL Server Agent jobs, linked servers, CLR assemblies, and cross-database queries — features only available in Managed Instance (not Azure SQL Database). This is a critical path for organizations with complex SQL Server environments.

## Demo Legacy Application
**Business Domain:** Regional hospital patient management and billing system for "Lakeview Medical Center"

The legacy system uses a complex SQL Server 2016 environment with multiple databases, cross-database queries, SQL Server Agent jobs, CLR stored procedures, Service Broker messaging, and linked servers to external systems.

### Tech Stack
- SQL Server 2016 SP3 Enterprise Edition
- 4 databases on the same instance
- SQL Server Agent with 25+ scheduled jobs
- CLR assemblies for complex calculations (C# compiled to SQL CLR)
- Service Broker for async messaging between databases
- Linked servers to pharmacy and insurance systems
- SQL Server Reporting Services (SSRS)
- Windows Authentication with AD groups for access control
- Database Mail for notifications
- TDE (Transparent Data Encryption) enabled on patient database

### Key Files/Folders Structure
```
LakeviewMedicalDB/
├── Databases/
│   ├── PatientDB/
│   │   ├── Tables/                      # 80+ patient tables
│   │   ├── Views/                       # Cross-database views
│   │   ├── StoredProcedures/            # 200+ procedures
│   │   ├── Functions/                   # UDFs including CLR functions
│   │   ├── CLRAssemblies/              # C# assemblies for medical calculations
│   │   └── ServiceBroker/              # Message types, contracts, queues
│   ├── BillingDB/
│   │   ├── Tables/                      # Insurance claims, invoices, payments
│   │   ├── StoredProcedures/            # Billing procedures with cross-DB queries
│   │   └── LinkedServerQueries/         # Queries to insurance linked server
│   ├── SchedulingDB/
│   │   ├── Tables/                      # Appointments, rooms, staff schedules
│   │   └── StoredProcedures/
│   └── ReportingDB/
│       ├── Views/                       # Reporting views spanning all DBs
│       └── StoredProcedures/            # Report data procedures
├── SQLAgent/
│   ├── Jobs/
│   │   ├── NightlyBilling.sql           # Nightly billing batch
│   │   ├── InsuranceClaims.sql          # Daily claims submission
│   │   ├── DataArchival.sql             # Monthly data archival
│   │   ├── StatisticsUpdate.sql         # Weekly statistics maintenance
│   │   └── BackupJob.sql               # Full/differential backup schedule
│   └── Alerts/
│       └── DiskSpaceAlert.sql           # Disk space monitoring
├── LinkedServers/
│   ├── PharmacyLink.sql                 # Linked server to pharmacy system
│   └── InsuranceLink.sql               # Linked server to insurance portal
├── SSRS/
│   ├── PatientSummary.rdl              # Patient summary report
│   ├── BillingReport.rdl               # Monthly billing report
│   └── OccupancyReport.rdl            # Ward occupancy report
└── Migration/
    └── assessment-scripts.sql           # Pre-migration assessment queries
```

### Database Schema (PatientDB — Primary)
- **Patients** (PatientId, MRN, FirstName, LastName, DOB, SSN_Encrypted, InsuranceId, PrimaryPhysicianId, AdmitDate, DischargeDate, Status)
- **Encounters** (EncounterId, PatientId, DepartmentId, EncounterType, AdmitDate, DischargeDate, DiagnosisCodes, AttendingPhysicianId)
- **Orders** (OrderId, EncounterId, OrderType, OrderDate, Status, PhysicianId, Priority)
- **Medications** (MedicationId, EncounterId, DrugCode, Dosage, Frequency, StartDate, EndDate, PrescribedBy)
- **LabResults** (ResultId, EncounterId, TestCode, ResultValue, Units, ReferenceRange, ResultDate, AbnormalFlag)
- **BillingCharges** (ChargeId, EncounterId, CPTCode, ICDCode, Amount, InsuranceAmount, PatientAmount, Status) — in BillingDB
- **InsuranceClaims** (ClaimId, EncounterId, InsuranceId, ClaimAmount, Status, SubmitDate, ResponseDate) — in BillingDB

### Legacy Anti-Patterns / Complex Features
- Cross-database queries (SELECT from PatientDB..Patients JOIN BillingDB..BillingCharges)
- CLR assemblies for drug interaction checks and ICD code validation
- Service Broker for async messaging between PatientDB and BillingDB
- Linked servers to external pharmacy and insurance systems
- SQL Server Agent jobs with complex multi-step job chains
- Database Mail for automated notifications on job failures
- TDE with certificate-based encryption
- SSRS reports with embedded SQL queries
- Windows Authentication with AD group-based security
- Filestream for storing medical imaging references

## Target Architecture
- **Database:** Azure SQL Managed Instance (General Purpose or Business Critical)
- **Migration Method:** Azure Database Migration Service (online migration for minimal downtime)
- **Agent Jobs:** SQL Managed Instance Agent (native support)
- **CLR:** SQL Managed Instance CLR support (with PERMISSION_SET adjustments)
- **Linked Servers:** Maintained in Managed Instance (with endpoint configuration)
- **Encryption:** TDE with service-managed keys (or customer-managed via Key Vault)
- **Reporting:** SSRS migrated to Power BI or SSRS on Azure VM
- **Networking:** VNet integration with private endpoints
- **Monitoring:** Azure SQL Analytics + Azure Monitor
- **Backup:** Automated backups with configurable retention

### Architecture Description
Azure SQL Managed Instance provides near-100% compatibility with on-premises SQL Server, making it the ideal target for complex environments. Cross-database queries, CLR assemblies, SQL Agent jobs, and Service Broker all work with minimal or no changes. The migration uses Azure DMS online mode for minimal downtime — initial full backup restore, continuous log shipping, and a final cutover. Linked servers are reconfigured to point to cloud endpoints. TDE certificates are migrated. SSRS reports are migrated to Power BI for modernized reporting.

## Branch Structure
- `main` — Contains the completed lab with full APPMODLAB.md documentation
- `legacy` — SQL scripts to create the full on-premises database environment
- `solution` — Migration scripts, DMS configuration, and post-migration validation
- `step-1-assessment` — DMA assessment reports and compatibility analysis
- `step-2-pre-migration` — Network setup, MI provisioning, TDE certificate export
- `step-3-online-migration` — DMS online migration configuration and monitoring
- `step-4-cutover-validation` — Final cutover, job migration, linked server reconfiguration
- `step-5-post-migration` — Performance tuning, monitoring setup, SSRS migration

## Lab Content Requirements (APPMODLAB.md)
### Learning Objectives
- Assess SQL Server databases for Managed Instance compatibility using DMA
- Configure Azure Database Migration Service for online migration
- Migrate SQL Server Agent jobs, CLR assemblies, and linked servers
- Handle TDE certificate migration for encrypted databases
- Validate migration completeness and tune performance post-migration

### Prerequisites
- SQL Server DBA experience (T-SQL, Agent jobs, backup/restore)
- Azure subscription with Contributor access
- Azure CLI and Azure Data Studio installed
- SQL Server Management Studio (SSMS)
- Basic networking understanding (VNets, NSGs)

### Step-by-Step Instructions Outline
1. **Assess Current Environment** — Run DMA against all four databases, review compatibility issues
2. **Provision Managed Instance** — Create SQL MI via Bicep in a VNet with private endpoint
3. **Prepare TDE Certificates** — Export TDE certificates from source, import to MI
4. **Configure DMS** — Set up Azure Database Migration Service with online migration
5. **Start Migration** — Initial full backup restore + continuous log shipping
6. **Migrate Agent Jobs** — Recreate SQL Agent jobs on Managed Instance
7. **Reconfigure Linked Servers** — Update linked server endpoints for cloud connectivity
8. **Validate CLR and Service Broker** — Test CLR assemblies and Service Broker messaging
9. **Cutover** — Final log restore, switch application connection strings
10. **Post-Migration** — Performance baseline, monitoring, SSRS migration to Power BI

### Estimated Duration
6–8 hours

### Key Concepts Covered
- SQL Managed Instance vs SQL Database comparison
- Online migration with minimal downtime
- DMA assessment and compatibility analysis
- TDE and security migration
- SQL Agent and CLR migration

## What the Squad Needs to Build
1. **Legacy App Setup:** SQL scripts to create four databases with 80+ tables, 200+ stored procedures, CLR assemblies (C# source included), Service Broker configuration, 25+ Agent jobs, linked server definitions, TDE setup, and SSRS report definitions. Include a Docker-based SQL Server for local development.
2. **Modernization Implementation:** Complete migration playbook with DMS configuration, Agent job migration scripts, linked server reconfiguration, TDE certificate migration, and post-migration validation scripts. Include a simple .NET app that exercises cross-database queries to validate the migration.
3. **Lab Documentation:** APPMODLAB.md with detailed DMA assessment walkthrough, DMS step-by-step configuration, compatibility issue resolution guide, and post-migration performance tuning checklist.
4. **Infrastructure as Code:** Bicep templates for Azure SQL Managed Instance (VNet-integrated), Azure Database Migration Service, VNet with subnets and NSGs, and Azure Monitor workspace.
5. **CI/CD:** GitHub Actions workflow for deploying infrastructure and running post-migration validation tests.

## Acceptance Criteria
- [ ] All four databases created with full schema, data, and objects
- [ ] CLR assemblies compile and function correctly
- [ ] SQL Agent jobs execute on schedule
- [ ] Cross-database queries return correct results
- [ ] DMS online migration completes without data loss
- [ ] TDE encryption is preserved after migration
- [ ] Agent jobs run on Managed Instance
- [ ] Linked servers connect to target systems
- [ ] Service Broker messaging works post-migration
- [ ] APPMODLAB.md follows the template format with complete frontmatter
- [ ] Step-by-step guide is clear and reproducible
