# COBOL to Java Modernization

## Overview
- **Category:** Code Modernization
- **Priority:** P1
- **Languages:** COBOL → Java
- **Repository Name:** appmodlab-cobol-to-java
- **Organization:** EmeaAppGbb

## Objective
This lab teaches participants how to migrate a legacy COBOL mainframe batch-processing application to a modern Java microservices architecture running on Azure. It demonstrates systematic decomposition of monolithic COBOL programs into domain-driven Java services, covering data migration from VSAM files to relational databases, COBOL copybook translation, and modern API design. This is critical for organizations with decades of mainframe investment seeking to reduce operational costs and attract modern development talent.

## Demo Legacy Application
**Business Domain:** Regional insurance claims processing system for "Continental Insurance Group"

The legacy application is a COBOL batch-processing system that handles insurance claim submissions, adjudication, and payment authorization. It runs nightly batch cycles on a simulated mainframe environment.

### Tech Stack
- COBOL 85 with embedded SQL (DB2-style)
- VSAM (Virtual Storage Access Method) indexed files for policy holder data
- JCL (Job Control Language) scripts for batch scheduling
- Flat-file interfaces for external partner data exchange
- BMS (Basic Mapping Support) screens for operator interaction (simulated)

### Key Files/Folders Structure
```
src/
├── cobol/
│   ├── CLMPROC.cbl        # Main claims processing program
│   ├── POLYLKUP.cbl       # Policy lookup subroutine
│   ├── ADJUDCTN.cbl       # Adjudication rules engine
│   ├── PYMTAUTH.cbl       # Payment authorization
│   ├── RPTGEN.cbl         # Report generation
│   └── ERRHANDL.cbl       # Error handling routines
├── copybooks/
│   ├── CLMREC.cpy         # Claim record layout
│   ├── POLREC.cpy         # Policy record layout
│   ├── PYMTREC.cpy        # Payment record layout
│   └── ERRCODES.cpy       # Error code definitions
├── jcl/
│   ├── NIGHTRUN.jcl       # Nightly batch job
│   ├── MONTHEND.jcl       # Month-end processing
│   └── RPTJOB.jcl         # Report generation job
└── data/
    ├── policies.dat        # Sample VSAM policy data
    ├── claims.dat          # Sample claims data
    └── partners.dat        # Partner flat-file feed
```

### Database Schema
- VSAM KSDS file for policy records (key: policy-number, 10-digit)
- VSAM RRDS file for claims (relative record numbers)
- Sequential files for payment batches
- Fixed-width record layouts defined in copybooks (80-byte and 132-byte records)

### Legacy Anti-Patterns Present
- Monolithic batch processing — entire nightly cycle is one sequential flow
- GOTO-based control flow in adjudication logic
- Hardcoded business rules embedded in COBOL paragraphs (no externalized config)
- Copybook-based data contracts with no versioning
- Flat-file integration with no error recovery or idempotency
- Implicit data typing through PICTURE clauses (PIC 9(7)V99)
- No unit testing — validation is manual via report inspection
- Tightly coupled I/O — file handling mixed with business logic

## Target Architecture
- **Target Language/Framework:** Java 21 with Spring Boot 3.x
- **Architecture:** Domain-driven microservices with event-driven communication
- **Services:**
  - Claims Service (Spring Boot REST API)
  - Policy Service (Spring Boot REST API)
  - Adjudication Service (Spring Boot with Drools rules engine)
  - Payment Service (Spring Boot with async processing)
  - Reporting Service (Spring Boot with JasperReports)
- **Database:** Azure SQL Database (replacing VSAM files)
- **Messaging:** Azure Service Bus for event-driven communication between services
- **API Gateway:** Azure API Management
- **Container Runtime:** Azure Container Apps
- **CI/CD:** GitHub Actions with Maven build pipeline
- **Monitoring:** Azure Monitor + Application Insights

### Architecture Description
The monolithic COBOL batch program is decomposed into five bounded contexts, each running as an independent Spring Boot microservice in Azure Container Apps. VSAM files are migrated to Azure SQL Database with proper relational schema. Batch processing is replaced by event-driven workflows using Azure Service Bus — when a claim is submitted via REST API, events trigger adjudication and payment workflows asynchronously. The Drools rules engine externalizes business rules that were previously hardcoded in COBOL paragraphs.

## Branch Structure
- `main` — Contains the completed lab with full APPMODLAB.md documentation
- `legacy` — The COBOL application with simulated mainframe environment (GnuCOBOL-based)
- `solution` — The fully modernized Java microservices solution
- `step-1-data-migration` — VSAM-to-SQL schema conversion and data migration scripts
- `step-2-claims-service` — Claims processing extracted to Spring Boot service
- `step-3-adjudication-rules` — Business rules externalized to Drools engine
- `step-4-event-driven` — Azure Service Bus integration for async workflows
- `step-5-containerization` — Docker + Azure Container Apps deployment

## Lab Content Requirements (APPMODLAB.md)
### Learning Objectives
- Understand strategies for decomposing monolithic COBOL programs into microservices
- Translate COBOL copybook data structures to Java POJOs and relational schemas
- Externalize hardcoded business rules using a modern rules engine (Drools)
- Replace batch processing with event-driven architecture using Azure Service Bus
- Deploy Java microservices to Azure Container Apps with CI/CD

### Prerequisites
- Basic understanding of COBOL syntax and mainframe concepts
- Java 21 and Spring Boot experience
- Azure subscription with Contributor access
- Docker Desktop installed
- Maven 3.9+ and JDK 21 installed
- GnuCOBOL compiler (for running the legacy app)

### Step-by-Step Instructions Outline
1. **Explore the Legacy System** — Run the COBOL batch application, understand the data flows and copybook structures
2. **Design the Target Schema** — Map VSAM record layouts to relational tables in Azure SQL
3. **Migrate Data** — Run the data migration scripts to load VSAM data into Azure SQL
4. **Build the Claims Service** — Create Spring Boot REST API for claim submission and retrieval
5. **Build the Policy Service** — Create Spring Boot REST API wrapping policy data access
6. **Externalize Business Rules** — Translate COBOL adjudication paragraphs to Drools rules
7. **Implement Event-Driven Workflow** — Wire Azure Service Bus for claim-to-payment flow
8. **Containerize and Deploy** — Build Docker images and deploy to Azure Container Apps
9. **Validate End-to-End** — Submit claims via API and verify the full processing pipeline

### Estimated Duration
6–8 hours

### Key Concepts Covered
- Mainframe-to-cloud migration patterns
- COBOL data structure translation
- Domain-driven design and bounded contexts
- Event-driven architecture with Azure Service Bus
- Rules engine externalization

## What the Squad Needs to Build
1. **Legacy App Setup:** Build a working COBOL application using GnuCOBOL that compiles and runs a nightly batch cycle processing insurance claims. Include sample data files, JCL-equivalent shell scripts, and copybooks. The app must produce visible output (reports) so participants can verify it works.
2. **Modernization Implementation:** Build five Spring Boot microservices with full REST APIs, Drools rules configuration, Azure Service Bus integration, and Azure SQL Database access. Each service must have unit tests and integration tests. Include Docker Compose for local development.
3. **Lab Documentation:** Write APPMODLAB.md with clear step-by-step instructions that walk through each migration phase. Include screenshots of running applications, API examples with curl commands, and architecture diagrams.
4. **Infrastructure as Code:** Provide Bicep templates for Azure SQL Database, Azure Service Bus namespace, Azure Container Apps environment, and Azure API Management instance.
5. **CI/CD:** GitHub Actions workflows for building each microservice, running tests, building Docker images, and deploying to Azure Container Apps.

## Acceptance Criteria
- [ ] Legacy COBOL app compiles with GnuCOBOL and processes sample claims data
- [ ] Data migration scripts successfully convert VSAM data to Azure SQL tables
- [ ] All five Java microservices start and pass health checks
- [ ] Claims can be submitted via REST API and flow through adjudication to payment
- [ ] Drools rules produce the same adjudication outcomes as the COBOL logic
- [ ] Azure Service Bus events are published and consumed correctly
- [ ] Solution deploys to Azure Container Apps via Bicep templates
- [ ] APPMODLAB.md follows the template format with complete frontmatter
- [ ] Step-by-step guide is clear and reproducible
- [ ] All code compiles and runs without errors
