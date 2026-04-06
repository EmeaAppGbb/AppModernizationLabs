# Squad Decisions

## Active Decisions

### 1. CSS Class Contract & Frontend Architecture

**By:** Data (Frontend Dev)  
**Date:** 2026-03-20  
**Status:** Active  

CSS class names are committed in `index.html` and `script.js` — Sloth targets these exact classes. Theme switching uses `data-theme="dark|light"` on `<body>`. Responsive breakpoints: sidebar overlay on mobile (≤768px), grid columns 3→2→1. Dropdown/modal open state uses `.open` class. No build tools — plain HTML/CSS/JS served directly by GitHub Pages.

### 2. Retro 8-Bit Visual Design System

**By:** Sloth (Designer)  
**Date:** 2026-03-20  
**Status:** Active  

Color palette: NES-inspired deep space blue (`#0f0f23`) dark, parchment (`#f0e6d3`) light, teal primary (`#00d4aa`), hot pink secondary (`#ff6b9d`), golden accent (`#ffd93d`). Typography: `Press Start 2P` (headings), `VT323` (body). Pixel aesthetic via zero border-radius, hard box-shadow, `image-rendering: pixelated`. Theme switching uses `[data-theme="dark"]` on `<body>` with CSS custom properties. CRT scanline overlay, pixel grid background, card entrance animations. Both `.open` and `.show` classes supported for state toggling. Responsive breakpoints: 1024px (3→2 columns), 768px (sidebar overlay + 1 column), 480px (compact). Accessibility: `:focus-visible` outlines, screen-reader utility, print styles.

### 3. SVG Pixel Art Asset Conventions

**By:** Andy (Illustrator)  
**Date:** 2026-03-20  
**Status:** Active  

All pixel art uses `<rect>` elements on a grid (no `<path>` or curves) for authentic 8-bit rendering. `shape-rendering="crispEdges"` on all SVG roots prevents browser anti-aliasing. Theme-aware icons (github, clone, codespace, search, filter, close, arrow-down) use `currentColor` for CSS adaptation. Fixed-color icons: star (golden), sun (golden), moon (white), vscode (blue), category icons (teal). `border-top.svg` tiles at 64x8 via CSS `background-repeat`. OG image at 1200x630 for social sharing. Frontend should reference these paths in HTML/CSS.

### 4. CI/CD Pipeline Architecture

**By:** Chunk (DevOps)  
**Date:** 2026-03-20  
**Status:** Active  

Python over shell/yq for YAML processing — `pyyaml` + `urllib` handle parsing, decoding, and GitHub API cleanly. Staging directory (`_site/`) for Pages deploy since `upload-pages-artifact` lacks file exclusions. `labs.md` is the single source of truth — all lab repos listed as plain URLs, one per line. Three workflows ready: `process-labs` (reads labs.md → builds appmodlab.json), `process-new-lab-issue` (validates submissions → creates PRs), `deploy-pages` (deploys to GitHub Pages). Front-end can rely on auto-generated `appmodlab.json`.

### 5. Application Insights Telemetry Integration

**Agent:** Data (Frontend Dev)  
**Date:** 2026-04-06  
**Status:** Implemented

Created `telemetry.js` as a standalone wrapper around the App Insights JS SDK.
- SDK loaded from Microsoft CDN (`ai.3.gbl.min.js`) — no npm dependency.
- Connection string configured via `window.APP_INSIGHTS_CONNECTION_STRING` (defaults to empty, injected by deployment).
- All telemetry calls guarded; no-op if SDK not loaded or string is empty.
- Tracks 12 custom events: LabCardClick, StartMenuOpen, CloneClick, VSCodeOpen, CodespaceOpen, VideoPlay, ShareClick, StarClick, GitHubOpen, FilterChange, Search, ThemeToggle.
- `script.js` calls `window.telemetry.trackEvent(name, props)` at each interaction point.

**Impact:**
- **Chunk:** Must inject `window.APP_INSIGHTS_CONNECTION_STRING` during deployment (e.g., via CI/CD `<script>` block or Azure Static Web Apps config).
- **All:** New file `telemetry.js` added; script load order in index.html: AI SDK CDN → config → telemetry.js → script.js.

### 6. Blazor Server Dashboard for Telemetry

**Author:** Mikey (Lead/Architect)  
**Date:** 2026-04-06  
**Status:** Implemented

Created a .NET 9 Blazor Server app at `dashboard/` to visualize Application Insights telemetry.
- **App Insights REST API** (not SDK) — keeps dashboard read-only, no instrumentation coupling.
- **Chart.js via JS interop** — matches retro aesthetic; lighter than Blazor-native charting.
- **Same retro 8-bit CSS** — pixel borders, CRT scanlines, Press Start 2P / VT323 fonts, NES palette.
- **Optional YouTube API** — graceful fallback if not configured.
- **Memory cache** (5-min TTL) on all queries.
- **Docker-ready** — multi-stage Dockerfile, port 8080.

**Key Paths:**
- `dashboard/Dashboard.sln`
- `dashboard/src/Dashboard/` (all source)
- `dashboard/Dockerfile`

**Team Impact:**
- Frontend devs (Sloth/Andy): CSS mirrors main gallery — keep palettes in sync.
- Data team: Kusto queries in `AppInsightsService.cs` map to custom events from `script.js`.
- Ops: Dockerfile ready for Azure Container Apps.

**Notes:**
- Targeting net9.0 with TODO markers for net10.0 upgrade.
- Avoid naming Razor loop variables `page` — compiler conflict with `@page` directive.

### 7. AZD Infrastructure for Dashboard Deployment

**Author:** Chunk (DevOps)  
**Date:** 2026-04-06  
**Status:** Implemented

Used Azure Developer CLI (azd) with modular Bicep infrastructure for dashboard deployment.
- **Five Bicep modules** (log-analytics, app-insights, container-registry, container-app-env, container-app) under `infra/modules/`.
- **`azure.yaml`** at repo root defines `dashboard` service pointing to `./dashboard/src/Dashboard`.
- **Container App:** External ingress port 8080, auto-scales 0–3 replicas, 0.5 CPU / 1Gi memory.
- **App Insights:** Shared between gallery website (JS SDK telemetry from Data) and dashboard (API reads).
- **GitHub Actions workflow** (`deploy-dashboard.yml`) handles full provision+deploy pipeline.

**Key Paths:**
| File | Purpose |
|------|---------|
| `azure.yaml` | AZD project definition |
| `infra/main.bicep` | Orchestration, outputs |
| `infra/main.parameters.json` | azd env variable binding |
| `infra/modules/*.bicep` | Individual resource modules |
| `.github/workflows/deploy-dashboard.yml` | CI/CD pipeline |

**Dependencies:**
- Requires `AZURE_CREDENTIALS` secret in GitHub repo settings (service principal JSON).
- Requires `AZURE_ENV_NAME` and `AZURE_LOCATION` as GitHub vars (defaults: `appmodlabs`, `eastus2`).
- Dashboard Dockerfile must exist at `dashboard/Dockerfile`.
- Dashboard source must be at `dashboard/src/Dashboard`.

**Impact on Other Agents:**
- **Data:** `APP_INSIGHTS_CONNECTION_STRING` output is needed to configure the JS SDK on the gallery website.
- **Dashboard:** Reads from App Insights using `AppInsights__ConnectionString` and `AppInsights__AppId` env vars.
- **All:** `azd up` from repo root will provision everything and deploy the dashboard.

### 8. Lab Task Brief Structure and Conventions

**Author:** Mouth (Content/Documentation)  
**Date:** 2026-04-06  
**Status:** Implemented

Created `docs/tasks/` with 35 self-contained Markdown briefs following a standardized template.
- **Unique business domains** — Each lab uses a different, realistic business scenario (insurance, veterinary, pharmaceutical, etc.).
- **Specific legacy app descriptions** — Not generic; detailed stack (e.g., ASP.NET MVC 5, .NET Framework 4.8, Entity Framework 6, EDMX, Unity DI, OWIN auth).
- **Standardized branch structure** — All labs: `main` (docs), `legacy` (starting point), `solution` (reference), `step-N-description` (phases).
- **Consistent file naming** — Kebab-case slugs matching lab names. Repository names: `appmodlab-{slug}` under `EmeaAppGbb`.
- **Five build deliverables** per brief: Legacy App Setup, Modernization Implementation, Lab Documentation (APPMODLAB.md), Infrastructure as Code, CI/CD.

**Impact:**
- **Hands:** Can pick up any task file and know exactly what to build without further clarification.
- **Brain:** Can use task files for Sprint planning and work decomposition.
- **Eyes:** Can use acceptance criteria as review checklist.
- **All agents:** Consistent structure makes cross-lab work predictable.

## Governance

- All meaningful changes require team consensus
- Document architectural decisions here
- Keep history focused on work, decisions focused on direction
