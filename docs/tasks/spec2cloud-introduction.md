# Spec2Cloud Introduction

## Overview
- **Category:** Spec-Driven Development
- **Priority:** P1
- **Languages:** Markdown/YAML
- **Repository Name:** appmodlab-spec2cloud-introduction
- **Organization:** EmeaAppGbb

## Objective
This lab introduces the Spec2Cloud methodology and toolchain — a spec-driven development approach where specifications (architecture docs, API contracts, data models) are the primary artifacts that drive code generation and modernization. Participants learn the philosophy of spec-driven development, the Spec2Cloud workflow, and how to create specifications that serve as both documentation and executable blueprints. This is the foundational lab for all Spec2Cloud-related exercises.

## Demo Legacy Application
**Business Domain:** Simple library management system for "OpenShelf Library"

The demo is intentionally simple to keep the focus on the Spec2Cloud methodology rather than application complexity. It's a small Node.js + SQLite library management app with book catalog, member management, and loan tracking.

### Tech Stack
- Node.js 18 with Express.js
- SQLite for data storage
- EJS templates for server-side rendering
- No framework beyond Express
- Minimal but functional CRUD operations

### Key Files/Folders Structure
```
openshelf-library/
├── package.json
├── src/
│   ├── app.js                     # Express app
│   ├── routes/
│   │   ├── books.js               # Book catalog routes
│   │   ├── members.js             # Member management
│   │   └── loans.js               # Book loan tracking
│   ├── models/
│   │   ├── book.js                # Book model (SQLite queries)
│   │   ├── member.js              # Member model
│   │   └── loan.js                # Loan model
│   └── views/                     # EJS templates
├── database/
│   ├── schema.sql                 # SQLite schema
│   └── seed.sql                   # Sample data
└── specs/                         # EMPTY — Spec2Cloud output will go here
    ├── architecture/              # Architecture specifications
    ├── api/                       # API contract specifications
    ├── data/                      # Data model specifications
    └── decisions/                 # Architecture decision records
```

### Database Schema (SQLite)
- **books** (id, title, author, isbn, genre, published_year, available_copies, total_copies)
- **members** (id, name, email, phone, membership_date, status)
- **loans** (id, book_id, member_id, loan_date, due_date, return_date, status)

## Target Architecture
- Same library app rebuilt from Spec2Cloud-generated specifications
- Target: Node.js 20 with Fastify (chosen based on spec recommendations)
- PostgreSQL (chosen based on spec analysis of data patterns)
- The target is secondary — the specs are the primary deliverable

### Architecture Description
Spec2Cloud analyzes the simple library app and generates comprehensive specifications: an architecture document describing the system's components and dependencies, API contracts in OpenAPI format, data model specifications with entity relationships, and architecture decision records (ADRs) for modernization choices. These specs then drive the rebuilding of the app with a modern stack — demonstrating the spec-to-code pipeline.

## Branch Structure
- `main` — Contains the completed lab with full APPMODLAB.md documentation
- `legacy` — The simple Node.js/SQLite library app
- `solution` — The spec-driven rebuilt app
- `step-1-spec2cloud-setup` — Install and configure Spec2Cloud toolchain
- `step-2-analysis` — Run Spec2Cloud analysis on the legacy app
- `step-3-spec-generation` — Review and refine generated specifications
- `step-4-spec-to-code` — Build new app from specifications
- `step-5-validation` — Validate implementation against specs

## Lab Content Requirements (APPMODLAB.md)
### Learning Objectives
- Understand the spec-driven development philosophy and its advantages
- Install and configure the Spec2Cloud toolchain
- Run Spec2Cloud analysis on a legacy codebase
- Generate architecture specifications, API contracts, and data model docs
- Use specifications to drive a modernization implementation

### Prerequisites
- Basic Node.js development experience
- Understanding of REST API design
- Markdown proficiency (specs are Markdown-based)
- Git command-line proficiency

### Step-by-Step Instructions Outline
1. **Understand Spec-Driven Development** — Read the philosophy section, understand why specs matter
2. **Explore the Legacy App** — Run the library app, understand its features
3. **Install Spec2Cloud** — Set up the toolchain and configure for the project
4. **Run Analysis** — Point Spec2Cloud at the codebase, observe the analysis
5. **Review Architecture Spec** — Examine the generated architecture document
6. **Review API Contracts** — Examine generated OpenAPI specifications
7. **Review Data Models** — Examine entity relationship specifications
8. **Refine Specs** — Make modernization decisions, annotate specs
9. **Build from Specs** — Use specs to guide implementation of the new app
10. **Validate Against Specs** — Verify the implementation matches the specifications

### Estimated Duration
2–3 hours

### Key Concepts Covered
- Spec-driven development methodology
- Spec2Cloud toolchain overview
- Architecture specification formats
- API contract generation
- Spec-to-code workflow

## What the Squad Needs to Build
1. **Legacy App Setup:** Simple Node.js/SQLite library management app that's easy to understand but demonstrates enough complexity for meaningful spec generation. Include seed data.
2. **Modernization Implementation:** Rebuilt app from Spec2Cloud specs targeting Fastify + PostgreSQL. Include the generated specifications as artifacts in the `specs/` directory.
3. **Lab Documentation:** APPMODLAB.md serving as an introduction to Spec2Cloud — heavy on concepts, methodology explanation, and toolchain walkthrough. This is the "textbook" lab for Spec2Cloud.
4. **Infrastructure as Code:** Not applicable — runs locally.
5. **CI/CD:** Minimal GitHub Actions for build and test.

## Acceptance Criteria
- [ ] Legacy library app runs with all CRUD features
- [ ] Spec2Cloud generates architecture specification document
- [ ] Spec2Cloud generates OpenAPI contract from existing routes
- [ ] Spec2Cloud generates data model specification from schema
- [ ] Specifications are stored in the `specs/` directory
- [ ] New app is built following the generated specifications
- [ ] Validation confirms implementation matches specs
- [ ] APPMODLAB.md follows the template format with complete frontmatter
- [ ] Step-by-step guide is clear and reproducible
- [ ] All code compiles and runs without errors
