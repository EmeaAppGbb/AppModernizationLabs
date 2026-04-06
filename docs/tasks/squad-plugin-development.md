# SQUAD Plugin Development

## Overview
- **Category:** Agentic Software Development
- **Priority:** P3
- **Languages:** TypeScript
- **Repository Name:** appmodlab-squad-plugin-development
- **Organization:** EmeaAppGbb

## Objective
This lab teaches participants how to build and publish plugins that extend SQUAD capabilities. Plugins can add new skills to agents, integrate with external tools, define custom ceremonies, or provide domain-specific review rules. Participants learn the SQUAD plugin API, packaging standards, and distribution through the SQUAD plugin registry. This enables the community to share and reuse specialized SQUAD extensions.

## Demo Legacy Application
**Business Domain:** SQUAD plugin development itself — the lab builds three plugins

The lab doesn't modernize a legacy app. Instead, it uses a sample TypeScript project as a testbed for demonstrating three plugins built during the lab: a Jira integration plugin, a database migration safety checker plugin, and a changelog generator plugin.

### Tech Stack
- TypeScript 5.x for plugin development
- SQUAD Plugin SDK
- Jest for testing
- npm for package management and publishing
- GitHub Actions for CI/CD

### Key Files/Folders Structure
```
squad-plugins-lab/
├── plugins/
│   ├── jira-integration/             # Plugin 1: Jira sync
│   │   ├── package.json
│   │   ├── tsconfig.json
│   │   ├── src/
│   │   │   ├── index.ts              # Plugin entry point
│   │   │   ├── jira-client.ts        # Jira API client
│   │   │   ├── issue-sync.ts         # Issue sync logic
│   │   │   └── config.ts             # Plugin configuration schema
│   │   └── tests/
│   ├── db-migration-checker/          # Plugin 2: Migration safety
│   │   ├── package.json
│   │   ├── src/
│   │   │   ├── index.ts
│   │   │   ├── migration-parser.ts   # Parse migration files
│   │   │   ├── safety-rules.ts       # Destructive change detection
│   │   │   └── reporter.ts           # Safety report generator
│   │   └── tests/
│   └── changelog-generator/          # Plugin 3: Changelog
│       ├── package.json
│       ├── src/
│       │   ├── index.ts
│       │   ├── commit-parser.ts      # Conventional commit parser
│       │   ├── changelog-writer.ts   # Markdown changelog writer
│       │   └── version-bumper.ts     # Semantic version calculator
│       └── tests/
├── test-project/                     # Sample project for testing plugins
│   ├── package.json
│   ├── .squad/
│   │   └── team.yml                  # SQUAD config using plugins
│   ├── prisma/
│   │   └── migrations/              # Sample DB migrations (for plugin 2)
│   └── src/
└── docs/
    ├── plugin-sdk-reference.md       # SDK API documentation
    └── publishing-guide.md           # npm publishing guide
```

## Target Architecture
- Three functional SQUAD plugins: Jira integration, DB migration checker, changelog generator
- Each plugin follows SQUAD plugin SDK patterns
- Plugins are tested, packaged, and ready for npm publishing
- Test project demonstrates plugin usage in a real SQUAD configuration

### Architecture Description
Each plugin exports a standard SQUAD plugin interface: an `init` function for configuration, event hooks for lifecycle integration, and skill definitions for agent capabilities. The Jira plugin adds a skill that syncs GitHub Issues to Jira. The DB migration checker adds a review skill that Eyes uses to check migration safety. The changelog generator adds a ceremony that Mouth runs before releases. Plugins are packaged as npm modules with proper peer dependencies on the SQUAD SDK.

## Branch Structure
- `main` — Contains the completed lab with full APPMODLAB.md documentation
- `legacy` — Empty plugin scaffolds with SQUAD SDK configured
- `solution` — Three completed, tested plugins with documentation
- `step-1-sdk-setup` — SQUAD Plugin SDK setup and hello-world plugin
- `step-2-jira-plugin` — Jira integration plugin implementation
- `step-3-db-checker-plugin` — Database migration safety checker
- `step-4-changelog-plugin` — Changelog generator plugin
- `step-5-testing-publishing` — Test suite, packaging, and npm publishing

## Lab Content Requirements (APPMODLAB.md)
### Learning Objectives
- Understand the SQUAD plugin architecture and SDK
- Build a plugin that adds skills to SQUAD agents
- Build a plugin that integrates SQUAD with external tools (Jira)
- Build a plugin that defines custom review rules
- Package and publish SQUAD plugins to npm

### Prerequisites
- Strong TypeScript development experience
- Familiarity with SQUAD concepts (agents, skills, ceremonies)
- npm publishing experience (or npm account for publishing)
- Basic Jira API knowledge (for plugin 1)

### Step-by-Step Instructions Outline
1. **Set Up Plugin SDK** — Install SQUAD SDK, create plugin scaffold
2. **Build Hello World Plugin** — Minimal plugin that adds a greeting skill
3. **Build Jira Plugin** — Implement issue sync, bidirectional status updates
4. **Build DB Migration Checker** — Parse migration files, detect destructive changes
5. **Build Changelog Generator** — Parse commits, generate changelog, calculate version
6. **Write Tests** — Unit tests for each plugin with mocked SQUAD context
7. **Package Plugins** — Configure npm packaging, peer dependencies, entry points
8. **Test in Sample Project** — Configure SQUAD to use plugins, run development cycle
9. **Publish** — Publish to npm (or private registry)

### Estimated Duration
4–6 hours

### Key Concepts Covered
- SQUAD plugin SDK and API
- Plugin lifecycle hooks
- Skill definition for agents
- External tool integration
- npm packaging and publishing

## What the Squad Needs to Build
1. **Legacy App Setup:** Plugin scaffolds with SQUAD SDK dependencies, TypeScript configuration, test setup, and a sample project for integration testing.
2. **Modernization Implementation:** Three fully implemented plugins with comprehensive test suites. Each plugin should demonstrate a different plugin capability: external integration (Jira), review skill (DB migration), and ceremony (changelog).
3. **Lab Documentation:** APPMODLAB.md serving as both a tutorial and SDK reference — plugin architecture overview, API reference, configuration schema, event hooks, and publishing guide.
4. **Infrastructure as Code:** Not applicable.
5. **CI/CD:** GitHub Actions for linting, testing, building, and publishing all three plugins.

## Acceptance Criteria
- [ ] SQUAD Plugin SDK is documented with API reference
- [ ] Jira plugin syncs issues bidirectionally (or simulated with mock)
- [ ] DB migration checker detects destructive changes (DROP TABLE, column removal)
- [ ] Changelog generator produces correct Markdown from conventional commits
- [ ] All three plugins pass unit and integration tests
- [ ] Plugins install and configure via SQUAD team.yml
- [ ] Sample project demonstrates all three plugins in a SQUAD session
- [ ] APPMODLAB.md follows the template format with complete frontmatter
- [ ] Step-by-step guide is clear and reproducible
- [ ] All code compiles and runs without errors
