# Reverse Engineering Legacy Apps with Spec2Cloud

## Overview
- **Category:** Spec-Driven Development
- **Priority:** P1
- **Languages:** C#/.NET/Java
- **Repository Name:** appmodlab-reverse-engineering-legacy-apps-with-spec2cloud
- **Organization:** EmeaAppGbb

## Objective
This lab demonstrates using Spec2Cloud to reverse-engineer a complex legacy codebase — analyzing its architecture, extracting API contracts, mapping data flows, documenting business rules, and generating comprehensive specifications that serve as the modernization blueprint. Unlike the introductory Spec2Cloud lab, this focuses on deep analysis of a substantial, realistic enterprise codebase with complex patterns that require careful extraction.

## Demo Legacy Application
**Business Domain:** Corporate fleet management and vehicle tracking system for "TransFleet Logistics"

The legacy application manages vehicle fleets for corporate clients — tracking vehicle locations, managing maintenance schedules, processing fuel card transactions, handling driver assignments, and generating regulatory compliance reports (DOT Hours of Service). It's a complex C# application with decades of accumulated business rules.

### Tech Stack
- ASP.NET Web API 2 on .NET Framework 4.7.2
- Entity Framework 6 with Database-First (EDMX)
- WCF services for telematics device communication
- SQL Server 2014 with 150+ tables
- Hangfire for background job processing
- SignalR for real-time vehicle tracking updates
- AutoFac for dependency injection
- Quartz.NET for scheduled maintenance alerts
- SSRS for compliance reports

### Key Files/Folders Structure
```
TransFleet/
├── TransFleet.sln
├── TransFleet.WebApi/
│   ├── Controllers/                 # 25+ API controllers
│   ├── Filters/                     # Action filters for auth, logging
│   └── App_Start/                   # WebAPI config, AutoFac
├── TransFleet.Core/
│   ├── Services/
│   │   ├── VehicleService.cs        # 1200-line service class
│   │   ├── MaintenanceService.cs    # Maintenance scheduling logic
│   │   ├── FuelService.cs           # Fuel card transaction processing
│   │   ├── ComplianceService.cs     # DOT HOS compliance calculations
│   │   ├── DriverService.cs         # Driver assignment and qualification
│   │   └── GeofenceService.cs       # Geofencing rules engine
│   ├── Domain/
│   │   ├── Entities/                # 60+ domain entities
│   │   ├── ValueObjects/            # Value objects (Money, Distance, Duration)
│   │   └── Rules/                   # Business rule classes
│   ├── Integration/
│   │   ├── TelematicsAdapter.cs     # WCF client for GPS devices
│   │   ├── FuelCardAdapter.cs       # WCF client for fuel card processor
│   │   └── DOTReportingAdapter.cs   # Integration with DOT systems
│   └── Specifications/              # Specification pattern implementations
├── TransFleet.Data/
│   ├── TransFleetModel.edmx         # 150+ entity EDMX model
│   ├── Repositories/                # Repository pattern over EF6
│   └── Migrations/                  # 200+ EF migrations
├── TransFleet.WcfServices/
│   ├── TelematicsService.svc        # Vehicle telematics WCF service
│   └── NotificationService.svc      # Push notification WCF service
├── TransFleet.Jobs/
│   ├── MaintenanceAlertJob.cs       # Quartz.NET scheduled jobs
│   ├── ComplianceCheckJob.cs
│   └── DataArchivalJob.cs
└── TransFleet.Tests/
    └── ... (300+ unit tests)
```

### Database Schema (150+ tables, key ones listed)
- **Vehicles** (VehicleId, VIN, Make, Model, Year, LicensePlate, FleetId, Status, OdometerReading, FuelType)
- **Drivers** (DriverId, Name, LicenseNumber, LicenseExpiry, CDLClass, MedicalCertExpiry, Status)
- **MaintenanceSchedules** (ScheduleId, VehicleId, ServiceType, IntervalMiles, IntervalDays, LastServiceDate, NextServiceDate)
- **FuelTransactions** (TransactionId, VehicleId, DriverId, FuelCardId, Gallons, Amount, Location, Date)
- **GPSPositions** (PositionId, VehicleId, Latitude, Longitude, Speed, Heading, Timestamp) — 100M+ rows
- **HOSLogs** (LogId, DriverId, DutyStatus, StartTime, EndTime, Location, Remarks)
- **Geofences** (GeofenceId, Name, Polygon, AlertType, FleetId)
- **WorkOrders** (WorkOrderId, VehicleId, Type, Priority, Status, AssignedTo, CompletedDate)

### Complexity Factors for Reverse Engineering
- 1200-line VehicleService with deeply nested business rules
- Specification pattern implementations scattered across the codebase
- WCF service contracts with complex data contracts
- EF6 EDMX with 150+ entities and implicit relationships
- Quartz.NET job chains with inter-job dependencies
- Geofencing rules engine with spatial calculations
- DOT HOS compliance calculations (federal regulations in code)
- SignalR hubs with complex real-time event routing
- 200+ EF migrations (database evolution history)
- Integration adapters with external system protocols

## Target Architecture
- Spec2Cloud-generated specifications serve as the modernization blueprint
- Target: .NET 9 microservices on Azure Container Apps (guided by specs)
- The focus is on the quality and completeness of reverse-engineered specifications

### Architecture Description
Spec2Cloud performs deep analysis of the TransFleet codebase: static analysis of code structure, dependency mapping, API endpoint extraction, data flow tracing, business rule identification, and integration point cataloging. The output is a comprehensive specification suite: architecture overview, bounded context map, API contracts (OpenAPI), data model with relationships, business rules catalog, integration point specifications, and migration recommendation document.

## Branch Structure
- `main` — Contains the completed lab with full APPMODLAB.md documentation
- `legacy` — The TransFleet application (compilable .NET Framework solution)
- `solution` — Generated specifications + partial implementation guided by specs
- `step-1-static-analysis` — Spec2Cloud static code analysis output
- `step-2-api-extraction` — Extracted API contracts and data contracts
- `step-3-data-modeling` — Reverse-engineered data model from EDMX and migrations
- `step-4-business-rules` — Extracted business rule specifications
- `step-5-integration-mapping` — WCF and external integration specifications
- `step-6-modernization-blueprint` — Complete modernization specification suite

## Lab Content Requirements (APPMODLAB.md)
### Learning Objectives
- Use Spec2Cloud for deep analysis of a complex enterprise codebase
- Extract and document API contracts from legacy controllers and WCF services
- Reverse-engineer data models from Entity Framework EDMX and migrations
- Identify and catalog business rules embedded in service layer code
- Generate a complete modernization specification suite

### Prerequisites
- C# and .NET experience
- Understanding of enterprise patterns (repository, specification, domain services)
- Familiarity with Spec2Cloud basics (Spec2Cloud Introduction lab recommended)
- Visual Studio 2022 with .NET Framework 4.7.2 support

### Step-by-Step Instructions Outline
1. **Explore the Legacy Codebase** — Build and run TransFleet, understand the domain
2. **Run Static Analysis** — Spec2Cloud analyzes code structure, dependencies, and complexity
3. **Extract API Contracts** — Generate OpenAPI specs from Web API controllers and WCF contracts
4. **Reverse-Engineer Data Model** — Analyze EDMX, extract entity relationships, map migrations
5. **Identify Business Rules** — Scan service classes for business rule extraction
6. **Map Integrations** — Document WCF service contracts and external system interfaces
7. **Generate Bounded Contexts** — Spec2Cloud suggests microservice boundaries
8. **Review and Refine** — Human reviews specs, adds domain knowledge, corrects misinterpretations
9. **Create Modernization Blueprint** — Assemble complete specification suite with migration recommendations

### Estimated Duration
5–7 hours

### Key Concepts Covered
- Legacy codebase reverse engineering
- Specification extraction from code
- Business rule identification
- Bounded context discovery
- Modernization blueprint creation

## What the Squad Needs to Build
1. **Legacy App Setup:** Build a substantial C# application with 25+ controllers, 60+ entities, WCF services, EF6 EDMX, Quartz.NET jobs, and complex business rules in the fleet management domain. The application must compile and demonstrate key workflows (vehicle tracking, maintenance scheduling, HOS compliance).
2. **Modernization Implementation:** Complete Spec2Cloud analysis output — architecture specs, OpenAPI contracts, data model docs, business rules catalog, integration specs, and modernization blueprint. Include partial implementation of one microservice guided by the specs.
3. **Lab Documentation:** APPMODLAB.md with detailed reverse engineering walkthrough, spec review and refinement guide, tips for handling complex enterprise codebases, and modernization planning based on specs.
4. **Infrastructure as Code:** Not applicable for this lab (specs are the deliverable).
5. **CI/CD:** GitHub Actions for building the legacy application and validating spec format.

## Acceptance Criteria
- [ ] TransFleet compiles and runs on .NET Framework 4.7.2
- [ ] Key workflows (vehicle tracking, maintenance, HOS) are demonstrable
- [ ] Spec2Cloud generates architecture overview with dependency map
- [ ] API contracts extracted in OpenAPI format for all controllers
- [ ] Data model reverse-engineered from EDMX with entity relationships
- [ ] Business rules catalog identifies rules from service classes
- [ ] Integration points (WCF, external systems) are documented
- [ ] Bounded context map suggests microservice boundaries
- [ ] Modernization blueprint provides actionable migration recommendations
- [ ] APPMODLAB.md follows the template format with complete frontmatter
- [ ] Step-by-step guide is clear and reproducible
