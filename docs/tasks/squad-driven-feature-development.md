# SQUAD-Driven Feature Development

## Overview
- **Category:** Agentic Software Development
- **Priority:** P1
- **Languages:** TypeScript/Python
- **Repository Name:** appmodlab-squad-driven-feature-development
- **Organization:** EmeaAppGbb

## Objective
This lab demonstrates using SQUAD to plan, implement, test, and ship a complete feature end-to-end with AI agents. Unlike the introductory lab, this covers a real-world feature of significant complexity — showing how SQUAD agents collaborate on architecture decisions, implementation across multiple files, comprehensive testing, documentation, and CI/CD pipeline updates. Participants experience the full power of agentic development on a production-grade codebase.

## Demo Legacy Application
**Business Domain:** E-commerce product review and recommendation platform for "ShopSmart"

The starting point is a well-structured but feature-incomplete TypeScript (Node.js) backend and Python ML service for an e-commerce platform. The platform has product catalog and user management but needs a complete review and recommendation system — which SQUAD will build.

### Tech Stack
- TypeScript with Fastify (backend API)
- Python 3.12 with FastAPI (ML recommendation service)
- PostgreSQL for relational data
- Redis for caching
- Docker Compose for local development
- Jest for TypeScript tests, pytest for Python tests
- Existing CI/CD with GitHub Actions

### Key Files/Folders Structure
```
shopsmart/
├── api/                             # TypeScript Fastify backend
│   ├── package.json
│   ├── tsconfig.json
│   ├── src/
│   │   ├── server.ts
│   │   ├── modules/
│   │   │   ├── products/            # Product catalog (complete)
│   │   │   ├── users/               # User management (complete)
│   │   │   ├── orders/              # Order processing (complete)
│   │   │   └── reviews/             # EMPTY — SQUAD will build this
│   │   ├── plugins/                 # Fastify plugins
│   │   └── utils/                   # Shared utilities
│   └── tests/
├── ml-service/                      # Python FastAPI ML service
│   ├── pyproject.toml
│   ├── src/
│   │   ├── main.py
│   │   ├── models/                  # ML model definitions
│   │   └── recommendations/         # EMPTY — SQUAD will build this
│   └── tests/
├── docker-compose.yml
├── .github/workflows/ci.yml
└── .squad/                          # Pre-configured SQUAD team
    ├── team.yml
    └── agents/
```

### Feature to Build (SQUAD's Task)
**Product Review and Recommendation System:**
- Review submission with text, rating, and optional photos
- Review moderation pipeline (automated + manual)
- Sentiment analysis on review text (Python ML service)
- Collaborative filtering recommendations based on review patterns
- "Customers who liked this also liked..." feature
- Review aggregation (average rating, rating distribution, helpful votes)
- Anti-fraud detection for fake reviews

## Target Architecture
- **Review Module:** TypeScript Fastify module with routes, services, and repositories
- **Recommendation Engine:** Python FastAPI service with collaborative filtering
- **Sentiment Analysis:** Python NLP model (pre-trained, fine-tuned)
- **Storage:** PostgreSQL for reviews, Redis for recommendation cache
- **Event-Driven:** Review events published for ML processing
- **Testing:** Jest (API tests) + pytest (ML tests) + integration tests

### Architecture Description
Brain decomposes the feature into work items. Hands implements the TypeScript review module and Python recommendation service. Eyes reviews code for quality, security, and performance. Mouth documents the API endpoints and architecture decisions. The lab shows the full agent collaboration including architecture design, implementation across TypeScript and Python, comprehensive testing, and documentation — demonstrating SQUAD on a real polyglot codebase.

## Branch Structure
- `main` — Contains the completed lab with full APPMODLAB.md documentation
- `legacy` — The ShopSmart platform without reviews/recommendations
- `solution` — Complete platform with SQUAD-built review/recommendation system
- `step-1-brain-planning` — Brain's feature decomposition and architecture decision
- `step-2-hands-api` — Hands implements TypeScript review API module
- `step-3-hands-ml` — Hands implements Python recommendation service
- `step-4-eyes-review` — Eyes reviews all code, identifies issues
- `step-5-mouth-docs` — Mouth creates API documentation and architecture docs

## Lab Content Requirements (APPMODLAB.md)
### Learning Objectives
- Use SQUAD Brain to decompose a complex feature into actionable work items
- Coordinate SQUAD Hands across TypeScript and Python codebases
- Execute SQUAD Eyes code review with actionable feedback
- Generate comprehensive documentation with SQUAD Mouth
- Manage a full SQUAD development cycle from planning to shipping

### Prerequisites
- Completed "Getting Started with SQUAD" lab (or equivalent experience)
- TypeScript and Python development experience
- Docker Desktop installed
- Node.js 20+ and Python 3.12+ installed
- Familiarity with REST API design

### Step-by-Step Instructions Outline
1. **Set Up Environment** — Clone repo, start Docker Compose, verify existing features work
2. **Brief Brain** — Provide feature requirements, receive decomposed work items
3. **Architecture Decision** — Brain proposes architecture, human reviews and approves
4. **Implement Review API** — Hands builds TypeScript review module with tests
5. **Implement ML Service** — Hands builds Python recommendation service with tests
6. **Code Review** — Eyes reviews all implementations, suggests improvements
7. **Fix Review Feedback** — Hands addresses Eyes' feedback
8. **Document** — Mouth generates API docs, README updates, architecture decision records
9. **Integration Testing** — Verify end-to-end review → recommendation flow
10. **Ship** — Merge and deploy via CI/CD

### Estimated Duration
3–4 hours

### Key Concepts Covered
- Feature decomposition with AI agents
- Multi-language SQUAD development
- Agent coordination and handoff
- Code review as an agentic workflow
- Documentation generation

## What the Squad Needs to Build
1. **Legacy App Setup:** Build a TypeScript Fastify + Python FastAPI e-commerce platform with product catalog, users, and orders fully implemented. The reviews and recommendations modules must be empty/stubbed with clear interfaces. Docker Compose with PostgreSQL and Redis. Seed data for products, users, and orders.
2. **Modernization Implementation:** Complete review and recommendation modules as if built by SQUAD agents — TypeScript review API with CRUD, moderation, and aggregation; Python ML service with sentiment analysis and collaborative filtering. Full test suite.
3. **Lab Documentation:** APPMODLAB.md showing the complete SQUAD feature development cycle with annotated agent interactions, decision points, and before/after comparisons. Include SQUAD session transcripts or summaries.
4. **Infrastructure as Code:** Docker Compose for local development. No Azure infrastructure required.
5. **CI/CD:** Updated GitHub Actions workflow covering both TypeScript and Python builds/tests.

## Acceptance Criteria
- [ ] Existing product/user/order features work before SQUAD changes
- [ ] Review API supports full CRUD with moderation pipeline
- [ ] Recommendation engine provides "similar products" based on reviews
- [ ] Sentiment analysis runs on review text
- [ ] TypeScript and Python test suites pass
- [ ] SQUAD agent interactions are documented and reproducible
- [ ] Architecture decision records explain design choices
- [ ] APPMODLAB.md follows the template format with complete frontmatter
- [ ] Step-by-step guide is clear and reproducible
- [ ] All code compiles and runs without errors
