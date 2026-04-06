# Ralph Automation

## Overview
- **Category:** Agentic Software Development
- **Priority:** P3
- **Languages:** YAML/GitHub Actions
- **Repository Name:** appmodlab-ralph-automation
- **Organization:** EmeaAppGbb

## Objective
This lab teaches participants how to configure Ralph — SQUAD's autonomous coordinator agent — for continuous work monitoring, idle-watch, and automated backlog processing. Ralph watches for idle periods, monitors team health, processes backlog items autonomously, and escalates blockers. This lab demonstrates the "always-on" agentic development model where Ralph keeps the development pipeline flowing even when humans aren't actively managing it.

## Demo Legacy Application
**Business Domain:** Open-source weather dashboard project for "WeatherLens"

The project is a React + Node.js weather dashboard with a healthy backlog of issues, pending dependency updates, and documentation gaps. Ralph will be configured to autonomously process these items, demonstrating continuous backlog management.

### Tech Stack
- React 18 with TypeScript (frontend)
- Node.js with Express (backend API)
- OpenWeatherMap API integration
- PostgreSQL for user preferences
- GitHub Actions for CI/CD
- Pre-loaded backlog with 20+ issues of varying types

### Key Files/Folders Structure
```
weatherlens/
├── frontend/
│   ├── package.json
│   ├── src/
│   │   ├── App.tsx
│   │   ├── components/              # Weather display components
│   │   ├── hooks/                   # Custom React hooks
│   │   └── services/               # API client services
│   └── tests/
├── backend/
│   ├── package.json
│   ├── src/
│   │   ├── server.ts
│   │   ├── routes/                  # API routes
│   │   └── services/               # Weather data services
│   └── tests/
├── .github/
│   ├── workflows/
│   │   ├── ci.yml
│   │   └── ralph.yml                # Ralph automation workflow (to build)
│   └── ISSUE_TEMPLATE/
├── .squad/
│   ├── team.yml
│   └── agents/
│       └── ralph/                   # Ralph configuration (to customize)
│           ├── config.yml           # Ralph settings
│           ├── schedules.yml        # Work schedule definitions
│           ├── escalation.yml       # Escalation rules
│           └── history.md           # Ralph's activity log
├── docs/
│   └── BACKLOG.md                   # Backlog prioritization
└── docker-compose.yml
```

### Pre-loaded Backlog Items (20+ issues)
- **Dependency updates:** 5 outdated packages with known vulnerabilities
- **Bug fixes:** 3 bugs (timezone display, API error handling, cache invalidation)
- **Documentation:** 4 docs tasks (API docs, setup guide, contributing guide, changelog)
- **Performance:** 2 optimization tasks (bundle size, API response caching)
- **Features:** 3 small features (dark mode, temperature units, favorite locations)
- **Maintenance:** 3 tasks (linting config, test coverage improvement, CI optimization)

## Target Architecture
- Same WeatherLens project with Ralph fully configured
- Ralph automated workflows for different task types
- Escalation rules and human oversight checkpoints
- Activity logging and progress reporting

### Architecture Description
Ralph is configured with work schedules (when to process backlog), task type handlers (how to process different issue types), escalation rules (when to notify humans), and idle-watch triggers (auto-start when pipeline is idle). For dependency updates, Ralph runs `npm audit fix` and creates PRs. For documentation tasks, Ralph delegates to Mouth. For bug fixes and features, Ralph creates a plan with Brain and delegates to Hands with Eyes review. Humans receive daily digests and approve critical changes.

## Branch Structure
- `main` — Contains the completed lab with full APPMODLAB.md documentation
- `legacy` — WeatherLens with pre-loaded backlog and basic SQUAD config
- `solution` — WeatherLens with fully configured Ralph automation
- `step-1-ralph-config` — Basic Ralph configuration with work schedules
- `step-2-task-handlers` — Configure handlers for each task type
- `step-3-idle-watch` — Set up idle-watch triggers and auto-start
- `step-4-escalation` — Define escalation rules and notification channels
- `step-5-demonstration` — Ralph processes 5+ backlog items autonomously

## Lab Content Requirements (APPMODLAB.md)
### Learning Objectives
- Configure Ralph's work schedules and idle-watch triggers
- Define task type handlers for different backlog item categories
- Set up escalation rules with human oversight checkpoints
- Monitor Ralph's activity through logs and reports
- Balance automation autonomy with human control

### Prerequisites
- Completed "Getting Started with SQUAD" lab
- GitHub Actions experience
- React and Node.js familiarity
- Understanding of CI/CD pipelines

### Step-by-Step Instructions Outline
1. **Review Backlog** — Examine the 20+ pre-loaded issues, understand task types
2. **Configure Work Schedules** — Define when Ralph should be active
3. **Define Task Handlers** — Configure how Ralph handles each issue type
4. **Set Up Idle-Watch** — Configure triggers for auto-start when pipeline is idle
5. **Configure Escalation** — Define when Ralph should notify humans
6. **Enable Ralph** — Start Ralph and observe initial backlog processing
7. **Monitor Activity** — Review Ralph's activity log and PR creation
8. **Handle Escalations** — Respond to Ralph's escalation notifications
9. **Review Results** — Assess quality of Ralph's autonomous work
10. **Tune Configuration** — Adjust based on results

### Estimated Duration
3–4 hours

### Key Concepts Covered
- Ralph agent configuration
- Autonomous backlog processing
- Idle-watch and auto-start
- Escalation and human oversight
- Activity monitoring and reporting

## What the Squad Needs to Build
1. **Legacy App Setup:** React + Node.js weather dashboard with functional features, pre-loaded GitHub Issues (20+ covering all task types), and basic SQUAD configuration with Ralph unconfigured.
2. **Modernization Implementation:** Fully configured Ralph with work schedules, task handlers, idle-watch, escalation rules, and GitHub Actions integration. Include examples of Ralph-processed issues (PRs created, docs updated, dependencies fixed).
3. **Lab Documentation:** APPMODLAB.md with Ralph configuration reference, task handler definition guide, escalation rule patterns, and best practices for autonomous agent management.
4. **Infrastructure as Code:** Not applicable — runs on GitHub Actions.
5. **CI/CD:** GitHub Actions workflows for Ralph automation, including scheduled triggers and issue event handlers.

## Acceptance Criteria
- [ ] WeatherLens runs with all existing features
- [ ] 20+ issues pre-loaded covering various task types
- [ ] Ralph configuration defines work schedules and task handlers
- [ ] Idle-watch triggers Ralph when pipeline is idle
- [ ] Ralph processes at least 5 backlog items autonomously
- [ ] Escalation rules notify humans for critical changes
- [ ] Ralph activity log tracks all actions taken
- [ ] APPMODLAB.md follows the template format with complete frontmatter
- [ ] Step-by-step guide is clear and reproducible
- [ ] All code compiles and runs without errors
