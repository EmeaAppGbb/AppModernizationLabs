# Getting Started with SQUAD

## Overview
- **Category:** Agentic Software Development
- **Priority:** P1
- **Languages:** YAML/Markdown
- **Repository Name:** appmodlab-getting-started-with-squad
- **Organization:** EmeaAppGbb

## Objective
This lab provides a hands-on introduction to SQUAD — the multi-agent team framework for software development. Participants learn to set up a SQUAD team on a repository, configure agents (Brain, Hands, Eyes, Mouth, Ralph), understand agent roles and communication patterns, and run their first agentic development session to deliver a small feature. This is the essential starting point for all SQUAD-based labs.

## Demo Legacy Application
**Business Domain:** Personal task management API for "TaskFlow"

The starting point is a minimal, deliberately incomplete Node.js REST API for personal task management. It has basic CRUD endpoints for tasks but lacks authentication, input validation, error handling, logging, tests, and documentation. This provides the canvas for SQUAD to demonstrate its capabilities.

### Tech Stack
- Node.js 20 with Express.js
- In-memory data store (no database yet)
- Basic REST endpoints (GET, POST, PUT, DELETE for /tasks)
- No tests, no CI/CD, no documentation

### Key Files/Folders Structure
```
taskflow-api/
├── package.json
├── src/
│   ├── index.js                   # Express server entry point
│   ├── routes/
│   │   └── tasks.js               # Task CRUD routes
│   └── data/
│       └── store.js               # In-memory array store
└── README.md                      # Minimal README
```

### What's Intentionally Missing (SQUAD Will Build)
- Input validation
- Error handling middleware
- Authentication
- Logging
- Unit and integration tests
- CI/CD pipeline
- API documentation
- CONTRIBUTING.md
- Issue templates

## Target Architecture
- **Same stack** (Node.js/Express) — the focus is on SQUAD workflow, not tech migration
- Enhanced with proper error handling, validation, logging, tests, and documentation
- SQUAD agents collaborate to plan, implement, review, and document improvements

### Architecture Description
The target is the same TaskFlow API but properly engineered — with input validation (Joi/Zod), centralized error handling middleware, structured logging (Winston/Pino), comprehensive test suite (Jest + Supertest), GitHub Actions CI/CD pipeline, OpenAPI documentation, and full repository documentation (README, CONTRIBUTING, issue templates). All of this is planned and delivered by SQUAD agents.

## Branch Structure
- `main` — Contains the completed lab with full APPMODLAB.md documentation
- `legacy` — The minimal TaskFlow API (starting point)
- `solution` — The SQUAD-enhanced API with all improvements
- `step-1-squad-setup` — SQUAD configuration files added to the repo
- `step-2-first-session` — Results of the first SQUAD planning session
- `step-3-implementation` — Features implemented by SQUAD agents
- `step-4-review-and-ship` — Code reviewed by Eyes, docs by Mouth, shipped

## Lab Content Requirements (APPMODLAB.md)
### Learning Objectives
- Understand SQUAD agent roles (Brain, Hands, Eyes, Mouth, Ralph) and responsibilities
- Set up SQUAD configuration files in a GitHub repository
- Run a SQUAD planning session to decompose work into agent tasks
- Execute a full SQUAD development cycle (plan → implement → review → document → ship)
- Understand inter-agent communication patterns and handoff protocols

### Prerequisites
- GitHub account with Copilot access
- Node.js 20+ installed
- Basic understanding of REST APIs
- Git command-line proficiency
- VS Code with GitHub Copilot extension

### Step-by-Step Instructions Outline
1. **Clone the Starter Repo** — Get the minimal TaskFlow API running locally
2. **Initialize SQUAD** — Create `.squad/` directory with team configuration
3. **Configure Agents** — Define Brain, Hands, Eyes, Mouth roles and capabilities
4. **Run Planning Session** — Brain creates a backlog of improvements
5. **Execute First Task** — Hands implements input validation guided by Brain's plan
6. **Review Implementation** — Eyes reviews the code changes
7. **Document Changes** — Mouth writes/updates documentation
8. **Iterate** — Run 2-3 more tasks through the SQUAD cycle
9. **Retrospective** — Review what SQUAD produced, quality assessment

### Estimated Duration
2–3 hours

### Key Concepts Covered
- Multi-agent software development
- SQUAD team structure and roles
- Agent configuration and customization
- Agentic development workflow
- Human-in-the-loop oversight

## What the Squad Needs to Build
1. **Legacy App Setup:** Build a minimal but functional Express.js REST API for task management with intentional gaps (no validation, no tests, no docs). This is the canvas for SQUAD demonstration.
2. **Modernization Implementation:** Provide the completed API as enhanced by a SQUAD team — with validation, error handling, logging, tests, CI/CD, and documentation. Each improvement should be traceable to a SQUAD agent's work.
3. **Lab Documentation:** APPMODLAB.md with detailed SQUAD setup walkthrough, annotated agent configuration files, screenshots of SQUAD sessions, and explanations of each agent interaction. This is the primary onboarding lab for SQUAD.
4. **Infrastructure as Code:** Not applicable — this lab runs locally.
5. **CI/CD:** GitHub Actions workflow created by SQUAD as part of the lab exercise.

## Acceptance Criteria
- [ ] Minimal TaskFlow API runs and handles basic CRUD operations
- [ ] SQUAD configuration files are documented with inline comments
- [ ] Lab walks through a complete SQUAD development cycle
- [ ] Each agent's contribution is clearly identified and explained
- [ ] Solution branch shows all SQUAD-implemented improvements
- [ ] Participants can replicate the SQUAD setup on their own repositories
- [ ] APPMODLAB.md follows the template format with complete frontmatter
- [ ] Step-by-step guide is clear and reproducible
- [ ] All code compiles and runs without errors
