# Spec-Driven Database Migration

## Overview
- **Category:** Spec-Driven Development
- **Priority:** P3
- **Languages:** SQL/Python
- **Repository Name:** appmodlab-spec-driven-database-migration
- **Organization:** EmeaAppGbb

## Objective
This lab demonstrates using Spec2Cloud to generate database specifications from legacy systems and using those specs to guide migration to modern platforms. It covers reverse-engineering database schemas, documenting implicit business rules in stored procedures, generating migration specifications, and executing a spec-driven migration with validation. The spec-driven approach ensures nothing is lost during migration — every table, constraint, trigger, and stored procedure is accounted for.

## Demo Legacy Application
**Business Domain:** Agricultural supply chain and crop management for "GreenHarvest Cooperative"

The legacy database manages crop planting schedules, fertilizer and pesticide inventory, harvest tracking, commodity pricing, and cooperative member accounts. It runs on SQL Server 2012 with extensive stored procedures, triggers, and computed columns that encode decades of agricultural business knowledge.

### Tech Stack
- SQL Server 2012 Standard Edition
- 120+ tables with complex relationships
- 80+ stored procedures containing business logic
- 30+ triggers for data integrity and auditing
- 20+ user-defined functions
- Computed columns with complex formulas
- SQL Server Agent jobs for data synchronization
- Linked servers to commodity pricing feeds

### Key Files/Folders Structure
```
greenharvest-db/
├── Schema/
│   ├── Tables/
│   │   ├── CropManagement/          # 30+ crop-related tables
│   │   ├── Inventory/               # 25+ inventory tables
│   │   ├── Members/                 # 15+ member/account tables
│   │   ├── Trading/                 # 20+ commodity trading tables
│   │   └── Reference/              # 30+ reference/lookup tables
│   ├── Views/                       # 40+ views (some materialized via indexed views)
│   ├── StoredProcedures/
│   │   ├── CropPlanning/           # Crop rotation and planting procedures
│   │   ├── Inventory/              # Stock management procedures
│   │   ├── Pricing/                # Price calculation procedures
│   │   ├── Settlement/             # Cooperative settlement procedures
│   │   └── Reporting/              # Report data procedures
│   ├── Functions/
│   │   ├── Scalar/                 # Yield calculations, unit conversions
│   │   └── TableValued/            # Complex filtering functions
│   ├── Triggers/
│   │   ├── AuditTriggers/          # Track all changes to key tables
│   │   ├── IntegrityTriggers/      # Cross-table business rule enforcement
│   │   └── SyncTriggers/           # Data synchronization between schemas
│   └── Types/
│       └── UserDefinedTypes/       # Table types for SP parameters
├── Data/
│   ├── SeedData/                   # Reference data (crop types, chemicals, regions)
│   └── SampleData/                 # Sample transactional data
├── Specs/                          # EMPTY — Spec2Cloud output goes here
│   ├── schema-spec/
│   ├── business-rules/
│   ├── migration-plan/
│   └── validation/
└── Migration/
    ├── Scripts/                    # Migration scripts (to be generated from specs)
    └── Validation/                 # Data validation queries
```

### Database Schema (Key Areas)
**Crop Management:**
- **CropTypes** (CropTypeId, Name, GrowingSeason, DaysToMaturity, MinTemp, MaxTemp, WaterRequirement)
- **Fields** (FieldId, MemberId, AcreAge, SoilType, IrrigationType, GPSBoundary, CurrentCrop)
- **PlantingSchedules** (ScheduleId, FieldId, CropTypeId, PlantDate, ExpectedHarvestDate, SeedVariety, RowSpacing)
- **Harvests** (HarvestId, FieldId, CropTypeId, HarvestDate, YieldBushels, MoistureContent, GradeCode)

**Business Logic in DB:**
- Stored procedures calculate optimal crop rotation based on soil type and previous crops
- Triggers enforce that planting dates fall within the crop's growing season
- Computed columns calculate yield-per-acre from harvest data
- UDFs convert between measurement units (bushels, tonnes, hundredweight)
- Settlement procedures calculate cooperative member payments based on complex grade/quality matrices

### Legacy Challenges
- Business rules encoded in 80+ stored procedures (not in application code)
- Triggers with implicit dependencies on execution order
- Computed columns with complex formulas that reference other tables via UDFs
- Indexed views for performance that complicate schema changes
- User-defined types for stored procedure parameters
- MERGE statements for complex upsert logic
- Cross-database references to commodity pricing database
- Temporal data patterns implemented manually (before temporal tables)

## Target Architecture
- **Database:** Azure Database for PostgreSQL (or Azure SQL Database — spec recommends)
- **Business Logic:** Extracted to application-layer Python services (from stored procedures)
- **Triggers:** Replaced with application-layer event handlers or database-level rules
- **Computed Columns:** Replaced with application-calculated fields or PostgreSQL generated columns
- **Scheduling:** Azure Functions or pg_cron (replacing SQL Agent jobs)
- **Validation:** Spec-generated validation suite ensures migration completeness

### Architecture Description
Spec2Cloud reverse-engineers the entire database: schema structure, stored procedure logic, trigger behavior, computed column formulas, and UDF implementations. It generates a comprehensive specification that documents every database object and the business rules it encodes. The migration spec includes: target schema DDL, stored procedure to Python service mapping, trigger replacement strategy, data migration scripts, and validation queries. The spec serves as both the migration plan and the acceptance criteria.

## Branch Structure
- `main` — Contains the completed lab with full APPMODLAB.md documentation
- `legacy` — Complete SQL Server database scripts
- `solution` — Target PostgreSQL schema + Python services + validation suite
- `step-1-schema-analysis` — Spec2Cloud schema reverse engineering
- `step-2-business-rule-extraction` — Stored procedure and trigger analysis
- `step-3-migration-spec` — Generated migration specification
- `step-4-target-implementation` — PostgreSQL schema + Python services from spec
- `step-5-validation` — Data and logic validation against spec

## Lab Content Requirements (APPMODLAB.md)
### Learning Objectives
- Use Spec2Cloud to reverse-engineer complex SQL Server database schemas
- Extract business rules from stored procedures, triggers, and computed columns
- Generate migration specifications that serve as both plan and acceptance criteria
- Execute a spec-driven database migration to PostgreSQL
- Validate migration completeness using spec-generated test cases

### Prerequisites
- SQL Server experience (T-SQL, stored procedures, triggers)
- Basic PostgreSQL knowledge
- Python experience (for extracted business logic)
- Docker Desktop (for SQL Server and PostgreSQL containers)

### Step-by-Step Instructions Outline
1. **Explore Legacy Database** — Review schema, run key stored procedures, understand business rules
2. **Run Schema Analysis** — Spec2Cloud extracts table definitions, relationships, and constraints
3. **Extract Business Rules** — Spec2Cloud analyzes stored procedures, triggers, and UDFs
4. **Generate Migration Spec** — Review the complete specification with target recommendations
5. **Create Target Schema** — Build PostgreSQL schema from specification
6. **Extract Business Logic** — Implement Python services replacing stored procedures
7. **Migrate Data** — Execute data migration scripts
8. **Replace Triggers** — Implement event handlers or PostgreSQL triggers
9. **Validate** — Run spec-generated validation queries to confirm completeness

### Estimated Duration
5–7 hours

### Key Concepts Covered
- Database reverse engineering with Spec2Cloud
- Business rule extraction from stored procedures
- Spec-driven database migration planning
- Stored procedure to application code extraction
- Migration validation strategies

## What the Squad Needs to Build
1. **Legacy App Setup:** Complete SQL Server database with 120+ tables, 80+ stored procedures, 30+ triggers, computed columns, UDFs, and indexed views. Include seed and sample data for the agricultural domain. Provide Docker-based SQL Server.
2. **Modernization Implementation:** PostgreSQL target schema, Python services replacing stored procedure business logic, data migration scripts, and validation suite. All generated from Spec2Cloud specifications.
3. **Lab Documentation:** APPMODLAB.md with database reverse engineering walkthrough, business rule extraction guide, migration spec format reference, and validation methodology.
4. **Infrastructure as Code:** Bicep templates for Azure Database for PostgreSQL and Azure Functions (for scheduled tasks).
5. **CI/CD:** GitHub Actions for schema deployment, migration execution, and validation test suite.

## Acceptance Criteria
- [ ] SQL Server database created with 120+ tables and all objects
- [ ] Stored procedures execute correctly with sample data
- [ ] Triggers fire and enforce business rules
- [ ] Spec2Cloud generates complete schema specification
- [ ] Business rules extracted and documented from stored procedures
- [ ] PostgreSQL schema created from specification
- [ ] Python services replicate stored procedure logic
- [ ] Data migration completes with validation queries passing
- [ ] APPMODLAB.md follows the template format with complete frontmatter
- [ ] Step-by-step guide is clear and reproducible
