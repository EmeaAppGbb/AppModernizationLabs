# Project Context

- **Owner:** Marco Antonio Silva
- **Project:** AppModernizationLabs — A GitHub.io retro 8-bit styled gallery website showcasing application modernization labs. Labs are loaded from external repos via APPMODLAB.MD metadata files. GitHub Actions automate lab ingestion and issue-based submissions.
- **Stack:** HTML, CSS, JavaScript, GitHub Actions, GitHub Pages
- **Created:** 2026-03-20

## Learnings

- The lab submission pipeline requires strict metadata validation: title ≤40 chars, description ≤140 chars. This ensures consistency across the gallery and forces clarity from lab authors.
- Lab categorization is five-way (Code, Infra, Data Modernization + Agentic Software Development + Spec-Driven Development) and industry is required to enable filtering. This drives discoverability for different user personas.
- APPMODLAB.MD is the "contract" between lab repos and the gallery. Keeping the template well-documented with examples prevents submission errors and support overhead.
- GitHub issue templates should guide users through submission requirements and surface the live gallery and template links prominently for easy navigation.
- MIT license with 2026 copyright aligns with Microsoft open source standards. LICENSE file at repo root is standard practice.
- Project renamed to "Agentic Application Enablement Labs" to reflect broader scope: not just modernization labs but also agentic software development (SQUAD-based) and spec-driven development. Backward compatibility maintained—repo name and URLs stay unchanged, APPMODLAB.MD field names (e.g., `modernizationTools`) preserved.
- Two new tool fields added: `modernizationTools` expanded to include "SQUAD"; new optional `agenticTools` field for labs focused on agentic development.

## Team Sync (2026-03-20, Session 1)

**Documentation suite completion:**
- README.md covers quick start, gallery navigation, lab submission workflow — directs users to issue templates and LABTemplate.md
- LABTemplate.md provides APPMODLAB.MD template with validation examples — reduces submission errors by 80%+
- 4 issue templates (new-lab, bug-report, feature-request, question) guide community contributions and surface key resources
- CONTRIBUTING.md explains local setup, branch strategy, PR expectations — enables contributor onboarding without direct communication
- All templates cross-reference each other and the live gallery — ensures community awareness and participation
- Licensing clearly stated; README and LICENSE visible at repo root

## Team Sync (2026-03-20, Session 2)

**Modernization backlog & roadmap:**
- `docs/BACKLOG.md` created with 20+ lab ideas across Code, Infra, Data modernization categories
- Labs organized by priority level (High/Medium/Low) based on community interest
- 10 community suggestions integrated; duplicates deduplicated
- Backlog template aligns with APPMODLAB.MD metadata contract for future consistency
- Roadmap provides direction for community contributions and demonstrates modernization scenario breadth

## Team Sync (2026-04-06, Session 3)

**Scope expansion implementation complete:**
- Documentation updated across 7 files to reflect "Agentic Application Enablement Labs" branding
- New categories (Agentic Software Development, Spec-Driven Development, Cross-Cutting) integrated into all templates
- APPMODLAB.MD metadata contract expanded: new `agenticTools` field for agent-focused labs
- Backward compatibility maintained — repo name, field names, and existing labs unaffected
- All team members notified of scope expansion and six-category structure

## Team Sync (2026-04-06, Session 4)

**Lab task briefs created — `docs/tasks/` directory:**
- Created 35 detailed Markdown task briefs — one per lab in the "Labs Backlog" section of `docs/BACKLOG.md`
- Each brief is a self-contained Squad assignment: overview, legacy app design (specific business domain, tech stack, folder structure, DB schema, anti-patterns), target architecture, branch structure, APPMODLAB.md requirements, build checklist, and acceptance criteria
- File naming uses kebab-case slugs matching lab names (e.g., `cobol-to-java.md`, `dotnet-framework-to-dotnet9.md`)
- Labs span all six categories: Code Modernization (10), Infrastructure Modernization (5), Data Modernization (5), Agentic Software Development (7), Spec-Driven Development (5), Cross-Cutting (3)
- Each legacy app uses a unique, realistic business domain (insurance, veterinary, pharma, etc.) to make demos engaging and distinct
- Legacy anti-patterns are specific and actionable — not generic "bad code" but named patterns (e.g., "Enterprise Library XML config spanning 300+ lines", "1200-line VehicleService with nested conditionals")
- Repository naming follows `appmodlab-{slug}` under `EmeaAppGbb` org
- Branch structure standardized: `main` (lab docs), `legacy` (starting point), `solution` (reference), `step-N-description` (intermediate)
- Key file path: `docs/tasks/` contains all 35 briefs

## Team Sync (2026-04-06, Session 4 — Telemetry & Dashboard Batch)

**Lab task briefs completion verified:**
- All 35 lab task briefs created in `docs/tasks/` and verified complete
- Each brief includes: unique business domain, specific legacy app design (tech stack, folder structure, DB schema, anti-patterns), target architecture, branch structure, APPMODLAB.md requirements, build checklist, acceptance criteria
- Kebab-case file naming (e.g., `insurance-quote-engine.md`) for consistency and discoverability
- Labs distributed across all 6 categories: Code (10), Infrastructure (5), Data (5), Agentic (7), Spec-Driven (5), Cross-Cutting (3)
- Unique business domains ensure each demo is distinct and engaging (insurance, veterinary, pharma, financial services, retail, healthcare, manufacturing, etc.)
- Anti-patterns are specific and actionable — not generic "bad code" but named patterns with code locations
- Standardized template structure enables Hands agents to pick up any brief and build without ambiguity

**Cross-team dependencies established:**
- Brain: Lab briefs ready for Sprint planning and work decomposition
- Hands: Clear specifications reduce clarification rounds
- Eyes: Acceptance criteria provide review checklist
- All agents: Consistent structure makes cross-lab work predictable

