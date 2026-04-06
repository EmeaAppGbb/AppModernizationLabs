# Legacy App to Agentic Development Pipeline

## Overview
- **Category:** Cross-Cutting / End-to-End
- **Priority:** P1
- **Languages:** C#/.NET/Python
- **Repository Name:** appmodlab-legacy-app-to-agentic-development-pipeline
- **Organization:** EmeaAppGbb

## Objective
This is the capstone end-to-end lab that demonstrates the complete journey: reverse-engineer a legacy application with Spec2Cloud, set up a SQUAD team, and use agents to plan, implement, review, and deliver the modernized application. It combines all the skills from previous labs into a single cohesive workflow — showing the full power of agentic application enablement from legacy analysis to cloud-deployed modern application.

## Demo Legacy Application
**Business Domain:** Commercial property insurance quoting and policy management for "Keystone Insurance"

The legacy application handles commercial property insurance — from initial quote generation through underwriting, policy issuance, endorsements, and renewal. It encodes 15 years of insurance business rules, actuarial calculations, and regulatory compliance requirements.

### Tech Stack
- ASP.NET MVC 4 on .NET Framework 4.6.1
- Entity Framework 5 with Database-First
- WCF services for integration with reinsurance partners
- Crystal Reports for policy documents
- SQL Server 2012 with 200+ stored procedures
- jQuery UI for frontend interactions
- Windows Authentication
- MSMQ for async message processing

### Key Files/Folders Structure
```
KeystoneInsurance/
├── KeystoneInsurance.sln
├── KeystoneInsurance.Web/
│   ├── Controllers/                 # 15+ MVC controllers
│   ├── Views/                       # Razor views with jQuery UI
│   └── App_Start/
├── KeystoneInsurance.Core/
│   ├── Services/
│   │   ├── QuotingEngine.cs         # 2000+ lines of quoting logic
│   │   ├── UnderwritingService.cs   # Risk assessment algorithms
│   │   ├── PremiumCalculator.cs     # Actuarial premium calculations
│   │   ├── PolicyService.cs         # Policy lifecycle management
│   │   └── ComplianceService.cs     # Regulatory compliance checks
│   ├── Domain/
│   │   ├── Entities/                # 80+ domain entities
│   │   └── Rules/                   # Business rule implementations
│   └── Integration/
│       ├── ReinsuranceClient.cs     # WCF client for reinsurers
│       └── RegulatoryReporter.cs    # Regulatory submission client
├── KeystoneInsurance.Data/
│   ├── KeystoneModel.edmx           # 100+ entity EDMX
│   └── StoredProcedures/            # 200+ stored procedures
├── KeystoneInsurance.Reports/
│   ├── PolicyDocument.rpt           # Crystal Report for policies
│   ├── QuoteLetter.rpt              # Quote letter template
│   └── RenewalNotice.rpt            # Renewal notice
├── KeystoneInsurance.Messaging/
│   └── MessageHandlers/             # MSMQ message processors
└── KeystoneInsurance.Tests/
    └── ... (100+ tests)
```

### Database Schema (200+ tables, key areas)
- **Quotes** (QuoteId, ClientId, PropertyAddress, PropertyValue, ConstructionType, OccupancyType, Premium, Status, ExpirationDate)
- **Policies** (PolicyId, QuoteId, PolicyNumber, EffectiveDate, ExpirationDate, Premium, Deductible, CoverageLimit, Status)
- **Underwriting** (UWId, QuoteId, RiskScore, CatastropheZone, LossHistory, ApprovalStatus, UnderwriterId)
- **Claims** — referenced but managed by separate system
- **RateFactors** (FactorId, FactorType, Value, EffectiveDate, ExpirationDate, StateCode)
- **CoverageOptions** (OptionId, CoverageType, Description, BaseRate, Limits)

### Complexity Factors
- 2000+ line QuotingEngine with nested conditionals for state-specific rules
- Actuarial premium calculations with 40+ rating factors
- Regulatory compliance varying by state (50 state variations)
- WCF integration with reinsurance partners (complex XML contracts)
- Crystal Reports with embedded SQL and sub-reports
- MSMQ message handlers for async policy issuance
- EDMX with 100+ entities and complex inheritance hierarchies

## Target Architecture
- **Backend:** .NET 9 with ASP.NET Core Web API
- **Rules Engine:** Drools or custom rules engine (externalizing business rules)
- **Database:** Azure SQL Database with EF Core 9
- **Messaging:** Azure Service Bus (replacing MSMQ)
- **Documents:** QuestPDF for policy documents (replacing Crystal Reports)
- **Integration:** HttpClient with Polly (replacing WCF)
- **Frontend:** Blazor Server or React
- **Hosting:** Azure Container Apps
- **All built by SQUAD agents guided by Spec2Cloud specifications**

### Architecture Description
This is the complete pipeline: (1) Spec2Cloud analyzes the legacy codebase and generates comprehensive specifications — architecture docs, API contracts, data models, business rule catalog, and integration specs. (2) SQUAD Brain uses these specs to create a prioritized backlog and development plan. (3) SQUAD Hands implements the modernized application following the specs, with multiple agents handling different services. (4) SQUAD Eyes reviews against specifications. (5) SQUAD Mouth generates documentation. The result demonstrates how the combined Spec2Cloud + SQUAD approach can modernize a complex enterprise application.

## Branch Structure
- `main` — Contains the completed lab with full APPMODLAB.md documentation
- `legacy` — The ASP.NET MVC 4 insurance application
- `specs` — Spec2Cloud-generated specifications
- `solution` — The SQUAD-built modernized application
- `step-1-spec2cloud-analysis` — Run Spec2Cloud, generate specs
- `step-2-squad-setup` — Configure SQUAD, Brain creates plan from specs
- `step-3-core-services` — Hands builds core quoting and policy services
- `step-4-integrations` — Hands builds Service Bus messaging and API integrations
- `step-5-review-and-docs` — Eyes reviews, Mouth documents, final validation

## Lab Content Requirements (APPMODLAB.md)
### Learning Objectives
- Execute the full Spec2Cloud → SQUAD pipeline on a complex enterprise app
- Use Spec2Cloud to reverse-engineer insurance domain business rules
- Configure SQUAD to work from formal specifications
- Coordinate multi-agent implementation of a domain-rich application
- Validate modernized application against Spec2Cloud specifications

### Prerequisites
- Completed Spec2Cloud Introduction and Getting Started with SQUAD labs
- C# and .NET experience
- Azure subscription with Contributor access
- Visual Studio 2022
- Docker Desktop

### Step-by-Step Instructions Outline
1. **Explore the Legacy App** — Run the insurance application, generate quotes, issue policies
2. **Run Spec2Cloud** — Analyze the codebase, generate full specification suite
3. **Review Specifications** — Examine architecture, business rules, API contracts
4. **Configure SQUAD** — Set up SQUAD with spec-aware agents
5. **Brain Planning** — Brain decomposes specs into development plan
6. **Hands: Core Services** — Implement quoting engine and policy service
7. **Hands: Integrations** — Implement Service Bus messaging and partner APIs
8. **Hands: Frontend** — Build Blazor or React UI matching legacy functionality
9. **Eyes Review** — Spec-based code review across all modules
10. **Mouth Documentation** — Generate comprehensive lab documentation
11. **Deploy and Validate** — Deploy to Azure, validate against specs

### Estimated Duration
8–12 hours (full-day workshop)

### Key Concepts Covered
- End-to-end agentic modernization pipeline
- Spec2Cloud + SQUAD integration workflow
- Complex domain modernization (insurance)
- Multi-service cloud deployment
- Specification-based validation

## What the Squad Needs to Build
1. **Legacy App Setup:** Build a comprehensive ASP.NET MVC 4 insurance application with realistic quoting, underwriting, and policy management. Include 200+ stored procedures, Crystal Reports, WCF service, and MSMQ handling. Must be runnable with seed data.
2. **Modernization Implementation:** Spec2Cloud analysis output + SQUAD-built .NET 9 application with all functionality migrated. Include externalized business rules, Azure Service Bus integration, QuestPDF documents, and full test suite.
3. **Lab Documentation:** APPMODLAB.md as a comprehensive workshop guide — this is the capstone lab and should reference/build on all previous labs. Include timing guides, facilitator notes, and troubleshooting sections.
4. **Infrastructure as Code:** Bicep templates for the full Azure deployment: Container Apps, SQL Database, Service Bus, Blob Storage, Redis Cache, Application Insights.
5. **CI/CD:** GitHub Actions for building, testing, and deploying the complete modernized application.

## Acceptance Criteria
- [ ] Legacy insurance app runs with quote generation, underwriting, and policy issuance
- [ ] Spec2Cloud generates complete specification suite
- [ ] SQUAD Brain creates actionable development plan from specs
- [ ] Modernized application implements all core business workflows
- [ ] Premium calculations match between legacy and modern (validated)
- [ ] Azure Service Bus replaces MSMQ for async processing
- [ ] Policy documents generate via QuestPDF
- [ ] Solution deploys to Azure Container Apps
- [ ] APPMODLAB.md follows the template format with complete frontmatter
- [ ] Step-by-step guide is clear and reproducible
- [ ] All code compiles and runs without errors
