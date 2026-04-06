# Oracle to Azure PostgreSQL Migration

## Overview
- **Category:** Data Modernization
- **Priority:** P2
- **Languages:** SQL/PL/SQL → SQL/Python
- **Repository Name:** appmodlab-oracle-to-azure-postgresql
- **Organization:** EmeaAppGbb

## Objective
This lab teaches participants how to migrate Oracle databases to Azure Database for PostgreSQL with schema conversion, PL/SQL to PL/pgSQL translation, and data migration. It covers using the Azure Database Migration Service and ora2pg for schema conversion, handling Oracle-specific features (sequences, packages, synonyms, materialized views), and adapting application code for PostgreSQL compatibility. This migration reduces licensing costs significantly while moving to a fully managed cloud database.

## Demo Legacy Application
**Business Domain:** Airline loyalty program and frequent flyer management for "SkyReward Airlines"

The Oracle database backs a loyalty program managing 500K+ member records, flight mile accruals, tier status calculations, reward redemptions, and partner earn/burn transactions. It makes heavy use of Oracle-specific features.

### Tech Stack
- Oracle 19c Enterprise Edition
- PL/SQL packages for all business logic (50+ packages)
- Oracle Sequences for ID generation
- Materialized Views for reporting dashboards
- Oracle Scheduler (DBMS_SCHEDULER) for batch jobs
- Oracle Advanced Queuing (AQ) for async processing
- Oracle Text for full-text search on reward catalog
- Oracle Synonyms for schema abstraction
- Java application using Oracle JDBC Thin driver

### Database Schema (Oracle)
- **MEMBERS** (MEMBER_ID NUMBER, FIRST_NAME, LAST_NAME, EMAIL, TIER_STATUS, TOTAL_MILES, QUALIFYING_MILES, JOIN_DATE, EXPIRY_DATE)
- **FLIGHTS** (FLIGHT_ID, MEMBER_ID, FLIGHT_NUMBER, ORIGIN, DESTINATION, TRAVEL_DATE, CLASS, MILES_EARNED, QUALIFYING_MILES_EARNED)
- **REDEMPTIONS** (REDEMPTION_ID, MEMBER_ID, REWARD_ID, MILES_REDEEMED, REDEMPTION_DATE, STATUS, BOOKING_REF)
- **REWARDS** (REWARD_ID, NAME, DESCRIPTION, MILES_REQUIRED, CATEGORY, AVAILABILITY, PARTNER_ID)
- **PARTNER_TRANSACTIONS** (TXN_ID, MEMBER_ID, PARTNER_ID, MILES, TXN_TYPE, TXN_DATE, REFERENCE)
- **TIER_RULES** (RULE_ID, TIER_NAME, MIN_QUALIFYING_MILES, MIN_QUALIFYING_SEGMENTS, BENEFITS)
- Plus Oracle-specific: Sequences, Packages, Materialized Views, Synonyms, AQ queues

### Legacy Anti-Patterns / Oracle-Specific Features
- PL/SQL packages with cursor-heavy logic (100+ cursors)
- Oracle sequences for all ID generation (no UUID)
- Materialized views with complex refresh schedules
- DBMS_SCHEDULER jobs for tier recalculation and mile expiry
- Oracle AQ for async reward fulfillment
- Oracle Text indexes for reward catalog search
- Public/private synonyms for multi-schema access
- CONNECT BY for hierarchical queries (partner referral chains)
- Oracle-specific date functions (ADD_MONTHS, MONTHS_BETWEEN, TRUNC)
- PL/SQL bulk collect and forall for batch operations
- Oracle-specific data types (NUMBER, VARCHAR2, DATE with time)

## Target Architecture
- **Database:** Azure Database for PostgreSQL Flexible Server
- **Migration Tool:** ora2pg for schema conversion + Azure DMS for data
- **ID Generation:** PostgreSQL SERIAL/BIGSERIAL or UUID
- **Business Logic:** PL/pgSQL functions replacing PL/SQL packages
- **Scheduling:** pg_cron extension (replacing DBMS_SCHEDULER)
- **Queuing:** pgmq or Azure Service Bus (replacing Oracle AQ)
- **Full-Text Search:** PostgreSQL tsvector/tsquery (replacing Oracle Text)
- **Materialized Views:** PostgreSQL materialized views with pg_cron refresh
- **Application:** Java with PostgreSQL JDBC driver
- **Monitoring:** Azure Database for PostgreSQL Insights

### Architecture Description
Oracle-specific features are mapped to PostgreSQL equivalents: PL/SQL packages become PL/pgSQL functions grouped by schema, sequences become SERIAL/BIGSERIAL, Oracle AQ becomes pgmq or Azure Service Bus, DBMS_SCHEDULER becomes pg_cron, Oracle Text becomes PostgreSQL full-text search. ora2pg handles the bulk of schema conversion, with manual adjustments for complex PL/SQL logic. The Java application switches from Oracle JDBC to PostgreSQL JDBC with minimal code changes (mostly SQL dialect adjustments).

## Branch Structure
- `main` — Contains the completed lab with full APPMODLAB.md documentation
- `legacy` — Oracle DDL scripts, PL/SQL packages, and sample data (runnable on Oracle XE)
- `solution` — PostgreSQL schema, PL/pgSQL functions, migrated data, and adapted Java app
- `step-1-assessment` — ora2pg assessment report and migration complexity analysis
- `step-2-schema-conversion` — Converted DDL with manual adjustments documented
- `step-3-plsql-migration` — PL/SQL to PL/pgSQL conversion with testing
- `step-4-data-migration` — DMS configuration and data validation scripts
- `step-5-app-adaptation` — Java application changes for PostgreSQL compatibility

## Lab Content Requirements (APPMODLAB.md)
### Learning Objectives
- Assess Oracle databases for PostgreSQL migration complexity using ora2pg
- Convert Oracle schemas including sequences, synonyms, and materialized views
- Translate PL/SQL packages and procedures to PL/pgSQL
- Configure Azure DMS for Oracle-to-PostgreSQL data migration
- Adapt Java application code for PostgreSQL JDBC compatibility

### Prerequisites
- Oracle SQL and PL/SQL experience
- Basic PostgreSQL knowledge
- Docker Desktop (for Oracle XE and PostgreSQL containers)
- Azure subscription
- Java 21 and Maven

### Step-by-Step Instructions Outline
1. **Explore Oracle Database** — Review schema, PL/SQL packages, Oracle-specific features
2. **Run ora2pg Assessment** — Generate migration complexity report
3. **Convert Schema** — Run ora2pg schema conversion, manually fix complex objects
4. **Migrate PL/SQL** — Convert packages to PL/pgSQL functions, handle Oracle-specific syntax
5. **Set Up Azure PostgreSQL** — Provision Flexible Server, configure extensions (pg_cron, pgmq)
6. **Migrate Data** — Configure DMS, run full data migration, validate row counts
7. **Adapt Application** — Switch JDBC driver, fix SQL dialect differences
8. **Validate** — Run application against PostgreSQL, compare results with Oracle

### Estimated Duration
5–7 hours

### Key Concepts Covered
- Oracle to PostgreSQL feature mapping
- PL/SQL to PL/pgSQL translation patterns
- ora2pg toolchain usage
- Azure DMS configuration
- JDBC driver migration

## What the Squad Needs to Build
1. **Legacy App Setup:** Oracle DDL and PL/SQL scripts runnable on Oracle XE (Docker). Include 50+ PL/SQL packages, sequences, materialized views, AQ configuration, and Oracle Text indexes. Include a Java Spring Boot app that exercises all Oracle features. Seed with 10K+ member records.
2. **Modernization Implementation:** Complete PostgreSQL migration with PL/pgSQL functions, pg_cron jobs, full-text search indexes, and adapted Java application. Include ora2pg configuration files and manual conversion documentation.
3. **Lab Documentation:** APPMODLAB.md with Oracle-to-PostgreSQL feature mapping table, PL/SQL translation guide, common conversion issues, and data validation procedures.
4. **Infrastructure as Code:** Bicep templates for Azure Database for PostgreSQL Flexible Server and Azure Database Migration Service.
5. **CI/CD:** GitHub Actions for schema deployment and data validation tests.

## Acceptance Criteria
- [ ] Oracle database runs on Oracle XE with all PL/SQL packages functional
- [ ] Java application works against Oracle with all loyalty program features
- [ ] ora2pg generates assessment report with migration complexity scores
- [ ] Schema converts to PostgreSQL with all objects accounted for
- [ ] PL/pgSQL functions produce identical results to PL/SQL packages
- [ ] Data migration completes with zero data loss (validated by row counts and checksums)
- [ ] Java application works against PostgreSQL with no functional differences
- [ ] APPMODLAB.md follows the template format with complete frontmatter
- [ ] Step-by-step guide is clear and reproducible
- [ ] All code compiles and runs without errors
