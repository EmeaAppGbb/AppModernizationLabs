# Project Context

- **Owner:** Marco Antonio Silva
- **Project:** AppModernizationLabs — A GitHub.io retro 8-bit styled gallery website showcasing application modernization labs. Labs are loaded from external repos via APPMODLAB.MD metadata files. GitHub Actions automate lab ingestion and issue-based submissions.
- **Stack:** HTML, CSS, JavaScript, GitHub Actions, GitHub Pages
- **Created:** 2026-03-20

## Learnings

<!-- Append new learnings below. Each entry is something lasting about the project. -->
- **Rename:** Project renamed from "App Modernization Labs" to "Agentic Application Enablement Labs". Short form in header: "AGENTIC LABS". Tagline: "Level Up Your Repos 🎮".
- **Blazor CSS isolation pitfall:** In `MainLayout.razor.css`, `::deep .retro-app` compiles to `[b-hash] .retro-app` — a descendant selector. But `.retro-app` IS the component's root element (it carries the `b-hash` attribute), so the selector never matches itself. Layout-critical styles for the root element and its structural children (`.retro-app`, `.retro-main`) must go in global CSS (`retro-dashboard.css`), not in scoped CSS. Only true descendants (`.retro-topbar`, `.topbar-title`, `.retro-content`, `.mobile-menu-toggle`) work correctly with `::deep`.
- **Telemetry:** `telemetry.js` wraps Azure Application Insights SDK (`@microsoft/applicationinsights-web` via CDN `ai.3.gbl.min.js`). Exposes `window.telemetry.trackEvent(name, props)` globally; gracefully no-ops when connection string is empty.
- **App Insights config:** Connection string is set via `window.APP_INSIGHTS_CONNECTION_STRING` — a deployment-time variable. Placeholder in `index.html` allows injection during CI/CD.
- **Telemetry events tracked:** LabCardClick, StartMenuOpen, CloneClick, VSCodeOpen, CodespaceOpen, VideoPlay, ShareClick, StarClick, GitHubOpen, FilterChange, Search, ThemeToggle — all fire from `script.js` delegated click/change handlers.
- **Non-blocking pattern:** All telemetry calls guard on `if (window.telemetry)` so the site fully works even if SDK fails to load or connection string is absent.
- **Card data attributes:** Lab cards carry `data-title` and `data-category` attributes for telemetry context without re-querying data.
- **Script load order:** index.html loads: AI SDK CDN → inline config → telemetry.js → script.js.
- `<base href>` tag removed from index.html — it broke local development and wasn't needed since all asset URLs are relative.
- Category filter now includes 5 options: Code Modernization, Infra Modernization, Data Modernization, Agentic Software Development, Spec-Driven Development.
- Sidebar label "Modernization Tools" renamed to "Enablement Tools" to match new project scope.
- GitHub repo URLs (EmeaAppGbb/AppModernizationLabs) kept as-is — repo not renamed on GitHub yet. OG URLs also unchanged.
- **2026-03-20:** Built `index.html`, `script.js`, and sample `appmodlab.json` at repo root.
- Architecture: Single-page static site; JS IIFE pattern; no build tooling needed.
- CSS class contract shared with Sloth (styles.css author): `.header`, `.sidebar`, `.gallery-grid`, `.lab-card`, `.badge-*`, `.dropdown`, `.modal`, `.footer`, etc.
- Filters use AND logic between groups, OR within checkbox groups. URL query params sync for shareable links.
- Theme stored in `localStorage`; `data-theme` attribute on `<body>` drives dark/light mode.
- `appmodlab.json` is the data source; fetched at runtime. Sample has 4 entries for dev testing.
- Google Fonts: `Press Start 2P` (headings), `VT323` (body text).
- `<base href="/AppModernizationLabs/">` set for GitHub Pages compatibility.
- Key files: `/index.html`, `/script.js`, `/styles.css` (Sloth), `/appmodlab.json`.

## Team Sync (2026-03-20, Session 1)

**Cross-agent dependencies confirmed:**
- Sloth styling targets Data's committed CSS class names — integration verified
- Andy's SVG assets (20 files, `assets/` directory) — currentColor icons ready for theme switching, fixed-color assets aligned with Sloth's NES palette
- Chunk's CI/CD (`process-labs.yml`, `process-new-lab-issue.yml`, `deploy-pages.yml`) — will auto-generate `appmodlab.json` from `labs.md`
- Mouth's documentation (README, LABTemplate, issue templates) — guides community lab submissions, validated by Chunk's workflows
- All teams synchronized on retro 8-bit design system, responsive breakpoints, theming strategy, and asset conventions

## Team Sync (2026-03-20, Session 2)

**Sample dataset expansion for improved testing:**
- `appmodlab.json` expanded from 4 to 14 labs covering all modernization categories (Code, Infra, Data)
- Labs span diverse industries: Financial Services, Retail, Healthcare, Manufacturing, Media, SaaS, Energy, Public Sector
- Tech stack representation: Node.js, Python, Go, Java, C#, TypeScript, React, Vue, AWS/Azure/GCP
- All entries enforce APPMODLAB.MD metadata contract (title ≤40, description ≤140, required fields)
- Enhanced sample dataset validates frontend filtering logic across diverse scenarios

## Team Sync (2026-04-06, Session 3)

**Scope expansion implementation complete:**
- Project renamed to "Agentic Application Enablement Labs" across all user-facing content
- Removal of `<base href>` enables both local file:// development and GitHub Pages deployment
- New categories (Agentic Software Development, Spec-Driven Development) integrated into filter UI
- Base href removal confirmed working with Chunk's local dev server (npm run dev)
- All team members notified of scope expansion and category structure decisions

## Team Sync (2026-04-06, Session 4 — Telemetry & Dashboard Batch)

**Application Insights telemetry integration complete:**
- Created `telemetry.js` wrapper around App Insights JS SDK (CDN `ai.3.gbl.min.js`, no npm dependency)
- Tracks 12 custom events: LabCardClick, StartMenuOpen, CloneClick, VSCodeOpen, CodespaceOpen, VideoPlay, ShareClick, StarClick, GitHubOpen, FilterChange, Search, ThemeToggle
- Connection string via `window.APP_INSIGHTS_CONNECTION_STRING` (injection by Chunk during deployment)
- Script load order: AI SDK CDN → config → telemetry.js → script.js
- All telemetry calls guard on `if (window.telemetry)` — non-blocking if SDK fails
- Frontend fixes verified: Start dropdown, Share modal, Video modal, clickable titles, light mode sidebar
- Handoff: Chunk must inject real connection string at deployment (default empty string for development)

**Dashboard readiness:**
- Mikey's Blazor dashboard reads App Insights REST API (read-only, no SDK coupling)
- Kusto queries in AppInsightsService map to custom events from telemetry tracking
- Chart.js interop for retro-styled metrics visualization
- Memory cache (5-min TTL) prevents API throttling
- All team members notified of telemetry structure and dashboard integration points

