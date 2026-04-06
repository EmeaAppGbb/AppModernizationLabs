# SQUAD Team Customization

## Overview
- **Category:** Agentic Software Development
- **Priority:** P2
- **Languages:** YAML/Markdown
- **Repository Name:** appmodlab-squad-team-customization
- **Organization:** EmeaAppGbb

## Objective
This lab teaches participants how to customize a SQUAD team by adding custom agents, defining specialized skills, configuring ceremonies (retrospectives, standups), and adapting agent behavior for domain-specific workflows. It demonstrates that SQUAD is not a rigid framework but a flexible platform that can be tailored to an organization's unique development culture, tech stack, and quality requirements.

## Demo Legacy Application
**Business Domain:** Healthcare appointment scheduling SaaS for "MedBook"

The codebase is a Go-based microservices backend for healthcare scheduling. It requires domain-specific agents: a HIPAA compliance reviewer, a healthcare terminology validator, and a test data anonymizer — none of which are standard SQUAD agents.

### Tech Stack
- Go 1.22 with Gin web framework
- gRPC for inter-service communication
- PostgreSQL with pgx driver
- Protocol Buffers for service contracts
- Kubernetes deployment manifests
- Existing SQUAD base configuration

### Key Files/Folders Structure
```
medbook/
├── cmd/
│   ├── appointment-service/         # Appointment microservice
│   ├── provider-service/            # Healthcare provider service
│   └── patient-service/             # Patient management service
├── internal/
│   ├── appointment/                 # Appointment domain logic
│   ├── provider/                    # Provider domain logic
│   └── patient/                     # Patient domain logic
├── proto/                           # Protocol Buffer definitions
├── k8s/                             # Kubernetes manifests
├── .squad/
│   ├── team.yml                     # Base SQUAD config (to be customized)
│   └── agents/
│       ├── brain/                   # Brain agent (standard)
│       ├── hands/                   # Hands agent (standard)
│       ├── eyes/                    # Eyes agent (standard)
│       └── mouth/                   # Mouth agent (standard)
└── tests/
```

### Customization Goals
- Add a **HIPAA Compliance Agent** that reviews code for PHI handling violations
- Add a **Domain Terminology Agent** that validates healthcare terminology usage
- Add a **Data Anonymizer Agent** that generates/validates anonymized test data
- Configure custom ceremonies (weekly security review, sprint planning)
- Define custom skills (Go-specific linting rules, HIPAA checklists)

## Target Architecture
- Same MedBook codebase with enhanced SQUAD configuration
- Three custom agents integrated into the SQUAD workflow
- Custom ceremonies and skill definitions
- Domain-specific quality gates in the review process

### Architecture Description
The standard SQUAD team is extended with three domain-specific agents. The HIPAA Compliance Agent runs during code review (alongside Eyes) and checks for PHI exposure, encryption compliance, and audit logging. The Domain Terminology Agent validates that medical terms, codes (ICD-10, CPT), and field names follow healthcare standards. The Data Anonymizer Agent ensures test fixtures use properly anonymized patient data. Custom ceremonies include a weekly HIPAA compliance review and a sprint planning session that includes compliance considerations.

## Branch Structure
- `main` — Contains the completed lab with full APPMODLAB.md documentation
- `legacy` — MedBook codebase with standard SQUAD configuration
- `solution` — MedBook with customized SQUAD team (3 custom agents, ceremonies, skills)
- `step-1-custom-agent-definition` — Define custom agent charters and capabilities
- `step-2-agent-implementation` — Implement custom agent configurations
- `step-3-ceremonies` — Define and configure custom ceremonies
- `step-4-skills-and-gates` — Add domain-specific skills and quality gates
- `step-5-integration-test` — Run customized SQUAD on a real development task

## Lab Content Requirements (APPMODLAB.md)
### Learning Objectives
- Define custom SQUAD agents with specialized roles and skills
- Configure agent charters with domain-specific review criteria
- Set up custom ceremonies (standups, retrospectives, compliance reviews)
- Create domain-specific quality gates in the SQUAD workflow
- Test and validate custom agent behavior

### Prerequisites
- Completed "Getting Started with SQUAD" lab
- Basic Go language familiarity (for understanding the codebase)
- Understanding of YAML configuration
- GitHub Copilot access

### Step-by-Step Instructions Outline
1. **Review Base SQUAD Config** — Understand the standard agent definitions
2. **Define HIPAA Agent** — Create charter, skills, review checklist, and integration points
3. **Define Terminology Agent** — Create domain vocabulary, validation rules
4. **Define Anonymizer Agent** — Create anonymization rules, test data patterns
5. **Configure Ceremonies** — Set up weekly security review, sprint planning
6. **Add Quality Gates** — Require HIPAA agent approval before merge
7. **Test Custom Agents** — Run SQUAD on a feature that touches patient data
8. **Iterate** — Refine agent behavior based on results
9. **Document** — Create custom agent documentation and templates for other teams

### Estimated Duration
3–4 hours

### Key Concepts Covered
- SQUAD agent architecture and extensibility
- Custom agent definition and charter writing
- Domain-specific quality gates
- SQUAD ceremony configuration
- Agent skill definitions

## What the Squad Needs to Build
1. **Legacy App Setup:** Go microservices codebase with appointment scheduling features and standard SQUAD configuration. Include realistic healthcare domain code with PHI handling, ICD-10 codes, and patient data.
2. **Modernization Implementation:** Customized SQUAD configuration with three additional agents, custom ceremonies, domain-specific skills, and quality gates. Include example SQUAD sessions showing custom agents in action.
3. **Lab Documentation:** APPMODLAB.md as a comprehensive guide to SQUAD customization — agent definition schema, ceremony configuration reference, skill definition patterns, and best practices for domain-specific agents.
4. **Infrastructure as Code:** Not applicable — SQUAD runs locally.
5. **CI/CD:** GitHub Actions that enforce custom quality gates (HIPAA compliance check as a required status check).

## Acceptance Criteria
- [ ] Base MedBook codebase runs with standard SQUAD
- [ ] HIPAA Compliance Agent defined with review checklist and charter
- [ ] Domain Terminology Agent validates healthcare terminology
- [ ] Data Anonymizer Agent generates anonymized test data
- [ ] Custom ceremonies are configured and documented
- [ ] Quality gates require custom agent approval
- [ ] Custom agents produce meaningful output when run on healthcare code
- [ ] APPMODLAB.md follows the template format with complete frontmatter
- [ ] Step-by-step guide is clear and reproducible
- [ ] Configuration files are well-documented with inline comments
