# Multi-Agent Code Review with SQUAD

## Overview
- **Category:** Agentic Software Development
- **Priority:** P2
- **Languages:** TypeScript/Python
- **Repository Name:** appmodlab-multi-agent-code-review-with-squad
- **Organization:** EmeaAppGbb

## Objective
This lab focuses on setting up a comprehensive multi-agent code review system using SQUAD. It demonstrates configuring reviewer agents with specialized focuses (security, performance, accessibility, API design), implementing rejection protocols and quality gates, and creating a review pipeline that catches issues humans often miss. Participants learn to balance automated review rigor with development velocity.

## Demo Legacy Application
**Business Domain:** Fintech payment processing API for "PayStream"

The codebase is a Python FastAPI payment processing system where code quality is critical — security vulnerabilities, performance issues, or API design mistakes can have significant financial and compliance impact. This makes it an ideal candidate for multi-agent review.

### Tech Stack
- Python 3.12 with FastAPI
- SQLAlchemy 2.0 with PostgreSQL
- Stripe SDK for payment processing
- Redis for rate limiting and caching
- Celery for async task processing
- Pydantic v2 for validation
- pytest for testing
- Docker Compose for local development

### Key Files/Folders Structure
```
paystream/
├── pyproject.toml
├── src/
│   ├── main.py
│   ├── api/
│   │   ├── payments.py              # Payment endpoints
│   │   ├── webhooks.py              # Stripe webhook handlers
│   │   ├── merchants.py             # Merchant management
│   │   └── reports.py               # Transaction reports
│   ├── services/
│   │   ├── payment_service.py       # Payment processing logic
│   │   ├── fraud_detection.py       # Fraud detection rules
│   │   └── notification_service.py  # Notification dispatching
│   ├── models/                      # SQLAlchemy models
│   ├── schemas/                     # Pydantic schemas
│   └── middleware/
│       ├── auth.py                  # JWT authentication
│       ├── rate_limiter.py          # Rate limiting
│       └── audit.py                 # Audit logging
├── tests/
├── .squad/
│   ├── team.yml
│   └── agents/
│       ├── eyes/                    # Base reviewer (to be expanded)
│       └── review-configs/          # Review configuration (to build)
└── docker-compose.yml
```

### Review Focus Areas
1. **Security Review:** PCI DSS compliance, SQL injection, XSS, sensitive data exposure, authentication flaws
2. **Performance Review:** N+1 queries, missing indexes, unbounded queries, cache invalidation, async usage
3. **API Design Review:** REST conventions, error response formats, pagination, versioning, backwards compatibility
4. **Accessibility Review:** API documentation quality, error messages, internationalization readiness

## Target Architecture
- Same PayStream codebase with enhanced SQUAD review configuration
- Four specialized review agents in addition to the base Eyes agent
- Quality gates with configurable severity thresholds
- Review summary and scoring system

### Architecture Description
The base Eyes agent is supplemented with four specialized review agents. Each agent has a focused charter, review checklist, and severity classification. Reviews run in parallel on PR creation. A quality gate aggregates findings — critical security issues block merge, while style suggestions are informational. The review pipeline produces a comprehensive report with categorized findings and an overall quality score. Rejection protocols define clear criteria for what blocks vs. what advises.

## Branch Structure
- `main` — Contains the completed lab with full APPMODLAB.md documentation
- `legacy` — PayStream with basic SQUAD Eyes configuration
- `solution` — PayStream with full multi-agent review pipeline
- `step-1-security-agent` — Security-focused reviewer agent
- `step-2-performance-agent` — Performance-focused reviewer agent
- `step-3-api-design-agent` — API design reviewer agent
- `step-4-quality-gates` — Aggregated quality gates and rejection protocols
- `step-5-demonstration` — Sample PRs reviewed by the full pipeline

## Lab Content Requirements (APPMODLAB.md)
### Learning Objectives
- Configure specialized SQUAD review agents with focused charters
- Define review checklists and severity classifications
- Implement quality gates that aggregate multi-agent findings
- Create rejection protocols with clear merge/block criteria
- Balance review thoroughness with development velocity

### Prerequisites
- Completed "Getting Started with SQUAD" lab
- Python and FastAPI experience
- Understanding of code review best practices
- Familiarity with security concepts (OWASP Top 10)

### Step-by-Step Instructions Outline
1. **Review Base Config** — Understand existing Eyes agent configuration
2. **Create Security Agent** — Define PCI DSS checklist, OWASP rules, sensitive data patterns
3. **Create Performance Agent** — Define N+1 detection, async patterns, caching rules
4. **Create API Design Agent** — Define REST conventions, error format standards, pagination rules
5. **Configure Quality Gates** — Set severity thresholds, define blocking vs. advisory findings
6. **Create Rejection Protocol** — Document what blocks merge and what's informational
7. **Test with Sample PRs** — Submit intentionally flawed PRs, verify agents catch issues
8. **Tune Agent Sensitivity** — Reduce false positives, calibrate severity levels
9. **Document Review Pipeline** — Create guide for interpreting multi-agent review results

### Estimated Duration
3–4 hours

### Key Concepts Covered
- Specialized code review agents
- Quality gate configuration
- Severity classification and thresholds
- Rejection protocols
- False positive management

## What the Squad Needs to Build
1. **Legacy App Setup:** Python FastAPI payment processing API with realistic fintech code — payment flows, webhook handling, fraud detection, and audit logging. Include intentionally flawed sample PRs for review demonstration.
2. **Modernization Implementation:** Four specialized review agents with charters, checklists, and severity classifications. Quality gate configuration that aggregates findings. Sample review reports showing the pipeline in action.
3. **Lab Documentation:** APPMODLAB.md with review agent definition guide, quality gate configuration reference, sample review reports, and best practices for balancing rigor and velocity.
4. **Infrastructure as Code:** Not applicable — review runs locally.
5. **CI/CD:** GitHub Actions that run SQUAD review agents on PR creation as required status checks.

## Acceptance Criteria
- [ ] PayStream API runs with all payment processing features
- [ ] Security agent detects common vulnerabilities (SQL injection, XSS, auth flaws)
- [ ] Performance agent identifies N+1 queries and missing async usage
- [ ] API design agent validates REST conventions and error formats
- [ ] Quality gates correctly block PRs with critical findings
- [ ] Quality gates allow PRs with only advisory findings
- [ ] Sample PRs demonstrate the full review pipeline
- [ ] APPMODLAB.md follows the template format with complete frontmatter
- [ ] Step-by-step guide is clear and reproducible
