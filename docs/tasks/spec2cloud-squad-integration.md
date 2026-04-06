# Spec2Cloud + SQUAD Integration

## Overview
- **Category:** Spec-Driven Development
- **Priority:** P2
- **Languages:** TypeScript/Python
- **Repository Name:** appmodlab-spec2cloud-squad-integration
- **Organization:** EmeaAppGbb

## Objective
This lab demonstrates the powerful combination of Spec2Cloud and SQUAD — using Spec2Cloud-generated specifications as input for SQUAD-driven development workflows. Specs become the source of truth that Brain uses for planning, Hands uses for implementation, Eyes uses for review criteria, and Mouth uses for documentation. This integration shows how AI agents can be more effective when guided by formal specifications rather than ad-hoc instructions.

## Demo Legacy Application
**Business Domain:** Property management platform for "UrbanNest Properties"

The starting point is a Spec2Cloud-analyzed Ruby on Rails 5 property management application. The analysis has already generated comprehensive specifications. SQUAD will use these specs to build the modernized TypeScript/Python replacement from scratch.

### Tech Stack (Legacy — Already Analyzed)
- Ruby on Rails 5.2 (legacy, already analyzed by Spec2Cloud)
- PostgreSQL 12
- Sidekiq for background jobs
- ActionCable for WebSocket updates

### Tech Stack (Target — SQUAD Will Build)
- TypeScript with NestJS (backend API)
- Python with FastAPI (property analytics service)
- PostgreSQL 16
- Redis for caching and pub/sub
- React 18 frontend

### Key Files/Folders Structure
```
urbannest/
├── specs/                           # Spec2Cloud-generated specifications
│   ├── architecture/
│   │   ├── system-overview.md       # Architecture overview with diagrams
│   │   ├── bounded-contexts.md      # Service boundary definitions
│   │   └── tech-stack-decisions.md  # ADRs for technology choices
│   ├── api/
│   │   ├── properties-api.yaml      # OpenAPI for properties service
│   │   ├── tenants-api.yaml         # OpenAPI for tenants service
│   │   ├── leases-api.yaml          # OpenAPI for lease management
│   │   └── maintenance-api.yaml     # OpenAPI for maintenance requests
│   ├── data/
│   │   ├── entity-model.md          # Entity relationships
│   │   ├── migration-plan.md        # Data migration strategy
│   │   └── schema.sql               # Target PostgreSQL schema
│   ├── business-rules/
│   │   ├── lease-calculations.md    # Lease pricing and renewal rules
│   │   ├── maintenance-workflow.md  # Maintenance request lifecycle
│   │   └── tenant-screening.md     # Tenant screening criteria
│   └── integration/
│       ├── payment-gateway.md       # Stripe integration spec
│       └── notification-service.md  # Email/SMS notification spec
├── legacy/                          # Rails 5 app (reference only)
├── api/                             # EMPTY — NestJS backend (SQUAD builds)
├── analytics/                       # EMPTY — Python analytics (SQUAD builds)
├── frontend/                        # EMPTY — React frontend (SQUAD builds)
├── .squad/
│   ├── team.yml                     # SQUAD configured for spec-driven work
│   └── agents/
│       ├── brain/
│       │   └── context.md           # Brain uses specs for planning
│       └── ...
└── docker-compose.yml
```

### Pre-Generated Specifications (Spec2Cloud Output)
1. **Architecture Overview** — System components, service boundaries, communication patterns
2. **API Contracts** — OpenAPI 3.1 specs for 4 service APIs (50+ endpoints)
3. **Data Model** — Entity relationships, PostgreSQL schema, migration strategy
4. **Business Rules** — Lease calculations, maintenance workflows, tenant screening
5. **Integration Specs** — Payment gateway and notification service contracts

## Target Architecture
- **Backend:** NestJS TypeScript monorepo with service modules
- **Analytics:** Python FastAPI service for property analytics and ML predictions
- **Frontend:** React 18 with TypeScript
- **Database:** PostgreSQL 16
- **Caching:** Redis
- **Hosting:** Azure Container Apps
- All implementation guided by Spec2Cloud specifications

### Architecture Description
SQUAD Brain reads the Spec2Cloud specifications to create work items that directly reference spec sections. Hands implements code that conforms to the OpenAPI contracts and data model specs. Eyes reviews code against the specifications — checking that API responses match the contract, business rules match the spec, and data models match the schema. Mouth generates documentation that references and extends the specifications. This demonstrates a spec-first development workflow where every decision is traceable to a specification.

## Branch Structure
- `main` — Contains the completed lab with full APPMODLAB.md documentation
- `legacy` — Spec2Cloud specs + Rails 5 reference app
- `solution` — SQUAD-built TypeScript/Python app conforming to specs
- `step-1-spec-review` — Brain analyzes specs, creates work items
- `step-2-api-implementation` — Hands builds NestJS API matching OpenAPI contracts
- `step-3-business-logic` — Hands implements business rules from specs
- `step-4-analytics-service` — Hands builds Python analytics service
- `step-5-spec-validation` — Eyes validates implementation against specs

## Lab Content Requirements (APPMODLAB.md)
### Learning Objectives
- Use Spec2Cloud specifications as input for SQUAD planning
- Implement APIs that conform to OpenAPI contracts generated by Spec2Cloud
- Validate code against formal specifications during review
- Maintain traceability between specs and implementation
- Demonstrate the spec → plan → code → review → docs pipeline

### Prerequisites
- Completed Spec2Cloud Introduction lab
- Completed Getting Started with SQUAD lab
- TypeScript and Python experience
- Understanding of OpenAPI specifications

### Step-by-Step Instructions Outline
1. **Review Specifications** — Explore the Spec2Cloud-generated specs for UrbanNest
2. **Configure SQUAD** — Point Brain at specs directory, configure spec-aware review
3. **Brain Planning** — Brain decomposes specs into actionable work items
4. **Implement APIs** — Hands builds NestJS endpoints matching OpenAPI contracts
5. **Implement Business Rules** — Hands codes lease calculations matching rule specs
6. **Build Analytics Service** — Hands creates Python analytics matching integration spec
7. **Spec-Based Review** — Eyes validates implementation against specifications
8. **Document Deviations** — Mouth documents any deviations from specs with rationale
9. **Validate Conformance** — Run spec conformance tests (OpenAPI validator)

### Estimated Duration
4–6 hours

### Key Concepts Covered
- Spec-driven agentic development
- Specification as planning input
- OpenAPI contract conformance
- Spec-based code review
- Traceability between specs and code

## What the Squad Needs to Build
1. **Legacy App Setup:** Pre-generated Spec2Cloud specifications for a property management platform (architecture, API contracts, data model, business rules, integrations). Include a Rails 5 reference app for context.
2. **Modernization Implementation:** TypeScript NestJS API and Python FastAPI analytics service built by following the specifications. Implementation must conform to OpenAPI contracts and business rule specs. Include conformance tests.
3. **Lab Documentation:** APPMODLAB.md demonstrating the full Spec2Cloud + SQUAD workflow — how specs feed into planning, implementation, review, and documentation. Include spec-to-code traceability examples.
4. **Infrastructure as Code:** Docker Compose for local development. Bicep for Azure Container Apps deployment.
5. **CI/CD:** GitHub Actions with OpenAPI conformance validation and standard CI.

## Acceptance Criteria
- [ ] Spec2Cloud specifications are comprehensive and well-structured
- [ ] SQUAD Brain successfully decomposes specs into work items
- [ ] NestJS API conforms to OpenAPI contracts (validated by tooling)
- [ ] Business rules implementation matches specification
- [ ] Python analytics service matches integration specification
- [ ] Eyes review references specifications in feedback
- [ ] Spec-to-code traceability is maintained
- [ ] APPMODLAB.md follows the template format with complete frontmatter
- [ ] Step-by-step guide is clear and reproducible
- [ ] All code compiles and runs without errors
