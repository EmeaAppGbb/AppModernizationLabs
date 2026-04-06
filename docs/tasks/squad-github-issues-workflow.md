# SQUAD + GitHub Issues Workflow

## Overview
- **Category:** Agentic Software Development
- **Priority:** P2
- **Languages:** GitHub Actions/YAML/TypeScript
- **Repository Name:** appmodlab-squad-github-issues-workflow
- **Organization:** EmeaAppGbb

## Objective
This lab teaches participants how to connect SQUAD to GitHub Issues for automated triage, assignment, and delivery. When an issue is created, SQUAD Brain analyzes it, estimates effort, assigns it to the appropriate agent, and kicks off the development cycle. This creates a seamless integration between project management and agentic development — issues become automatically actionable tasks.

## Demo Legacy Application
**Business Domain:** Developer community forum and knowledge base for "DevConnect"

The codebase is a TypeScript monorepo with a Next.js frontend and tRPC backend for a developer Q&A forum (similar to Stack Overflow). The repository has an active issue backlog with various issue types: bugs, features, documentation requests, and performance improvements.

### Tech Stack
- TypeScript monorepo (Turborepo)
- Next.js 14 frontend with App Router
- tRPC backend
- Prisma ORM with PostgreSQL
- Tailwind CSS
- Jest for testing
- GitHub Issues for project management

### Key Files/Folders Structure
```
devconnect/
├── turbo.json
├── packages/
│   ├── web/                         # Next.js frontend
│   │   ├── app/
│   │   ├── components/
│   │   └── tests/
│   ├── api/                         # tRPC backend
│   │   ├── routers/
│   │   ├── services/
│   │   └── tests/
│   └── shared/                      # Shared types and utilities
├── .github/
│   ├── workflows/
│   │   └── ci.yml                   # Existing CI pipeline
│   ├── ISSUE_TEMPLATE/
│   │   ├── bug_report.yml
│   │   ├── feature_request.yml
│   │   └── documentation.yml
│   └── labeler.yml                  # GitHub labeler config
├── .squad/
│   ├── team.yml
│   ├── agents/
│   └── workflows/                   # SQUAD workflow definitions (to build)
│       ├── issue-triage.yml         # Auto-triage new issues
│       ├── issue-to-pr.yml          # Convert issue to implementation PR
│       └── review-cycle.yml         # Review and merge workflow
└── prisma/
    └── schema.prisma
```

### Sample Issues (Pre-loaded in Backlog)
1. **Bug:** "Search results show deleted posts" — requires backend fix + test
2. **Feature:** "Add syntax highlighting to code blocks" — frontend enhancement
3. **Documentation:** "Add API rate limiting docs" — documentation task
4. **Performance:** "Question list page loads slowly with 1000+ posts" — optimization
5. **Bug:** "Markdown preview doesn't escape HTML" — security fix

## Target Architecture
- Same DevConnect codebase with SQUAD-GitHub Issues integration
- GitHub Actions workflows that trigger SQUAD on issue events
- SQUAD workflows that automate issue triage, development, and delivery
- Issue labels, milestones, and project boards managed by SQUAD Brain

### Architecture Description
GitHub Actions listen for issue events (opened, labeled, commented). When an issue is opened, a triage workflow triggers SQUAD Brain to analyze the issue, assign labels, estimate effort, and create a development plan as an issue comment. When an issue is labeled "approved" by a human, SQUAD Hands creates a branch, implements the fix/feature, and opens a PR. SQUAD Eyes reviews the PR. SQUAD Mouth updates documentation. When approved, the PR is merged and the issue is closed automatically.

## Branch Structure
- `main` — Contains the completed lab with full APPMODLAB.md documentation
- `legacy` — DevConnect with standard GitHub Issues (no SQUAD integration)
- `solution` — DevConnect with full SQUAD-GitHub Issues automation
- `step-1-issue-templates` — Enhanced issue templates with SQUAD-parseable sections
- `step-2-triage-workflow` — GitHub Actions + SQUAD Brain for auto-triage
- `step-3-implementation-workflow` — SQUAD Hands auto-implementation from issues
- `step-4-review-workflow` — SQUAD Eyes auto-review on PRs
- `step-5-full-cycle` — End-to-end issue-to-merge demonstration

## Lab Content Requirements (APPMODLAB.md)
### Learning Objectives
- Design GitHub Issue templates that SQUAD agents can parse and act on
- Build GitHub Actions workflows that trigger SQUAD on issue events
- Configure SQUAD Brain for automated issue triage and effort estimation
- Set up SQUAD Hands for automated implementation from issue descriptions
- Implement human-in-the-loop checkpoints in the automated workflow

### Prerequisites
- Completed "Getting Started with SQUAD" lab
- GitHub Actions experience
- TypeScript development experience
- Understanding of GitHub Issues and Projects

### Step-by-Step Instructions Outline
1. **Set Up DevConnect** — Clone, install, run the forum application
2. **Design Issue Templates** — Create structured templates SQUAD can parse
3. **Build Triage Workflow** — GitHub Action that triggers Brain on issue creation
4. **Build Implementation Workflow** — GitHub Action that triggers Hands on "approved" label
5. **Build Review Workflow** — GitHub Action that triggers Eyes on PR creation
6. **Add Human Checkpoints** — Require human approval between triage and implementation
7. **Test with Sample Issues** — Walk through each sample issue through the pipeline
8. **Monitor and Debug** — Review workflow logs, handle edge cases
9. **Document Workflow** — Create runbook for issue-to-delivery pipeline

### Estimated Duration
3–4 hours

### Key Concepts Covered
- GitHub Actions event-driven automation
- SQUAD workflow definitions
- Issue triage automation
- Human-in-the-loop checkpoints
- Issue-to-PR automated pipeline

## What the Squad Needs to Build
1. **Legacy App Setup:** TypeScript monorepo (Next.js + tRPC) developer forum with functional Q&A features. Pre-load GitHub issue templates and 5+ sample issues representing different types (bug, feature, docs, performance).
2. **Modernization Implementation:** GitHub Actions workflows that integrate with SQUAD for issue triage, automated implementation, and review. Include SQUAD workflow YAML definitions, custom event handling, and human approval gates.
3. **Lab Documentation:** APPMODLAB.md showing the complete issue-to-delivery pipeline with workflow diagrams, GitHub Actions configuration guide, and SQUAD workflow reference.
4. **Infrastructure as Code:** Not applicable — runs on GitHub Actions.
5. **CI/CD:** GitHub Actions workflows for SQUAD integration plus existing CI pipeline.

## Acceptance Criteria
- [ ] DevConnect forum runs with all Q&A features
- [ ] Issue templates are structured for SQUAD parsing
- [ ] Triage workflow triggers on issue creation and adds labels/comments
- [ ] Implementation workflow creates branch and PR from approved issues
- [ ] Review workflow triggers SQUAD Eyes on PR creation
- [ ] Human checkpoint prevents auto-implementation without approval
- [ ] At least 3 sample issues are processed through the full pipeline
- [ ] APPMODLAB.md follows the template format with complete frontmatter
- [ ] Step-by-step guide is clear and reproducible
- [ ] All code compiles and runs without errors
