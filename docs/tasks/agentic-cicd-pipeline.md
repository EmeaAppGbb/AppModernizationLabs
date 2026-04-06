# Agentic CI/CD Pipeline

## Overview
- **Category:** Cross-Cutting / End-to-End
- **Priority:** P3
- **Languages:** GitHub Actions/YAML
- **Repository Name:** appmodlab-agentic-cicd-pipeline
- **Organization:** EmeaAppGbb

## Objective
This lab demonstrates setting up GitHub Actions workflows that integrate with SQUAD for automated testing, review, and deployment. It creates an agentic CI/CD pipeline where SQUAD agents participate in the deployment lifecycle — Eyes reviews changes before deployment, Mouth generates release notes, Brain assesses deployment risk, and Ralph monitors post-deployment health. This extends traditional CI/CD with intelligent, agent-driven quality gates.

## Demo Legacy Application
**Business Domain:** SaaS metrics and billing dashboard for "MetricFlow"

The application is a TypeScript full-stack SaaS platform with a React frontend, Node.js backend, and PostgreSQL database. It has a traditional CI/CD pipeline with GitHub Actions for build, test, and deploy — but lacks intelligent quality gates, automated release notes, deployment risk assessment, and post-deployment monitoring.

### Tech Stack
- TypeScript monorepo (Turborepo)
- React 18 frontend (Vite)
- Node.js with Hono backend
- PostgreSQL with Drizzle ORM
- Redis for session management
- Docker Compose for local development
- GitHub Actions for CI/CD (basic: lint → test → build → deploy)
- Vercel for frontend deployment, Fly.io or Azure for backend

### Key Files/Folders Structure
```
metricflow/
├── turbo.json
├── packages/
│   ├── web/                          # React frontend
│   │   ├── package.json
│   │   ├── src/
│   │   └── tests/
│   ├── api/                          # Hono backend
│   │   ├── package.json
│   │   ├── src/
│   │   │   ├── routes/
│   │   │   ├── services/
│   │   │   └── middleware/
│   │   └── tests/
│   └── shared/                       # Shared types
├── .github/
│   ├── workflows/
│   │   ├── ci.yml                    # Basic CI (lint, test, build)
│   │   ├── deploy-staging.yml        # Deploy to staging (basic)
│   │   ├── deploy-production.yml     # Deploy to production (basic)
│   │   └── agentic/                  # EMPTY — SQUAD workflows (to build)
│   │       ├── pre-deploy-review.yml
│   │       ├── risk-assessment.yml
│   │       ├── release-notes.yml
│   │       └── post-deploy-monitor.yml
│   └── CODEOWNERS
├── .squad/
│   ├── team.yml
│   └── agents/
│       └── cicd/                     # CI/CD-specific agent configs
│           ├── risk-assessor.yml     # Brain config for risk assessment
│           ├── release-writer.yml    # Mouth config for release notes
│           └── health-monitor.yml    # Ralph config for post-deploy
├── docker-compose.yml
└── infrastructure/
    ├── Dockerfile.api
    └── bicep/                        # Azure infrastructure
```

### Current CI/CD Pipeline (Before SQUAD)
```
PR Created → Lint → Test → Build → ✅ Merge
Merge to main → Build → Deploy Staging → Manual approval → Deploy Prod
```

### Target CI/CD Pipeline (With SQUAD)
```
PR Created → Lint → Test → Build → Eyes Review → Risk Assessment (Brain) → ✅ Merge
Merge to main → Build → Deploy Staging → Health Check → Release Notes (Mouth)
             → Risk Score Review → Deploy Prod → Post-Deploy Monitor (Ralph)
             → Auto-Rollback if health degrades
```

## Target Architecture
- Same MetricFlow application with agentic CI/CD pipeline
- SQUAD agents integrated into GitHub Actions workflows
- Intelligent quality gates based on agent assessments
- Automated release notes and changelog generation
- Post-deployment health monitoring with auto-rollback capability

### Architecture Description
The CI/CD pipeline is enhanced with four SQUAD-powered stages: (1) **Pre-Deploy Review** — Eyes performs a security and performance review on every PR, adding review comments and a quality score. (2) **Risk Assessment** — Brain analyzes the changeset (files modified, blast radius, database migrations) and assigns a deployment risk score. High-risk deploys require additional human approval. (3) **Release Notes** — Mouth generates markdown release notes from commit messages and PR descriptions, categorizing changes and highlighting breaking changes. (4) **Post-Deploy Monitor** — Ralph watches deployment metrics (error rates, latency, CPU) and triggers auto-rollback if health degrades below thresholds.

## Branch Structure
- `main` — Contains the completed lab with full APPMODLAB.md documentation
- `legacy` — MetricFlow with basic CI/CD (no SQUAD integration)
- `solution` — MetricFlow with full agentic CI/CD pipeline
- `step-1-eyes-review-gate` — Eyes code review as GitHub Actions check
- `step-2-risk-assessment` — Brain risk assessment with scoring
- `step-3-release-notes` — Mouth automated release notes generation
- `step-4-post-deploy-monitor` — Ralph health monitoring and auto-rollback
- `step-5-full-pipeline` — Complete agentic CI/CD pipeline

## Lab Content Requirements (APPMODLAB.md)
### Learning Objectives
- Integrate SQUAD Eyes as a required status check for PRs
- Implement Brain-based deployment risk assessment with scoring
- Automate release notes generation with SQUAD Mouth
- Set up post-deployment health monitoring with Ralph
- Configure auto-rollback based on health metrics

### Prerequisites
- GitHub Actions experience (workflows, actions, environments)
- TypeScript/Node.js development experience
- Understanding of CI/CD concepts (staging, production, rollback)
- Docker Desktop
- Azure subscription (for deployment targets)

### Step-by-Step Instructions Outline
1. **Run MetricFlow** — Start the application, understand the basic CI/CD pipeline
2. **Add Eyes Review Gate** — Create workflow that triggers Eyes on PR, posts review comments
3. **Add Risk Assessment** — Create workflow that Brain analyzes changeset and scores risk
4. **Configure Approval Gates** — High-risk deploys require additional approval
5. **Add Release Notes** — Create workflow that Mouth generates release notes on merge
6. **Add Post-Deploy Monitor** — Create workflow that Ralph monitors health after deploy
7. **Configure Auto-Rollback** — Set health thresholds, trigger rollback on degradation
8. **End-to-End Test** — Push a change through the full agentic pipeline
9. **Test Rollback** — Deploy an intentionally faulty change, verify auto-rollback

### Estimated Duration
4–6 hours

### Key Concepts Covered
- Agentic CI/CD pipeline design
- SQUAD integration with GitHub Actions
- Deployment risk assessment
- Automated release documentation
- Post-deployment health monitoring

## What the Squad Needs to Build
1. **Legacy App Setup:** TypeScript SaaS application (React + Hono) with basic GitHub Actions CI/CD pipeline (lint, test, build, deploy). Include Docker Compose for local development and deployment targets (Azure or container-based).
2. **Modernization Implementation:** Four agentic GitHub Actions workflows: Eyes review gate, Brain risk assessment, Mouth release notes, and Ralph post-deploy monitor. Include auto-rollback mechanism and risk scoring configuration.
3. **Lab Documentation:** APPMODLAB.md with agentic CI/CD design patterns, workflow configuration guide, risk assessment scoring methodology, and auto-rollback setup instructions.
4. **Infrastructure as Code:** Bicep templates for Azure Container Apps (staging + production), Azure Monitor, and Application Insights with alert rules.
5. **CI/CD:** The CI/CD pipeline IS the deliverable — comprehensive GitHub Actions with SQUAD integration.

## Acceptance Criteria
- [ ] MetricFlow runs with basic CI/CD pipeline
- [ ] Eyes review gate runs on PR creation and posts comments
- [ ] Risk assessment scores changes and categorizes risk level
- [ ] High-risk deployments require additional approval
- [ ] Release notes auto-generate on merge to main
- [ ] Post-deploy monitor checks health metrics after deployment
- [ ] Auto-rollback triggers when health degrades
- [ ] Full pipeline processes a change from PR to production
- [ ] APPMODLAB.md follows the template format with complete frontmatter
- [ ] Step-by-step guide is clear and reproducible
- [ ] All code compiles and runs without errors
