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

### 9. YouTube Channel SVG Assets

**Author:** Andy (Illustrator / Pixel Artist)  
**Date:** 2026-04-06  
**Status:** Implemented

Created two YouTube-specific SVG assets in `assets/`:

1. **`youtube-profile.svg`** (800×800) — Pixel-circle-bordered gamepad icon with "AL" text. Square canvas that survives YouTube's circle crop. Uses Bresenham algorithm for authentic pixel circle.

2. **`youtube-banner.svg`** (2560×1440) — Full banner with "AGENTIC LABS" + "LEVEL UP YOUR REPOS" in pixel text, 3×3 grid logo, CRT scanline effect, and decorative pixel art (clouds, hearts, gamepads, code brackets, stars). All key content fits the 1546×423 safe zone.

**Design Decisions:**
- **Same NES palette** as all other assets: teal `#00d4aa`, pink `#ff6b9d`, gold `#ffd93d`, dark `#0f0f23`, purple `#4a4a8a`.
- **All `<rect>` elements** — no paths, no curves, consistent with Decision #3 (SVG Pixel Art Asset Conventions).
- **`shape-rendering="crispEdges"`** on both roots.
- **CRT scanlines** implemented as 2px-high rects every 4px at 0.08 opacity — matches Sloth's CSS scanline approach but self-contained in SVG.

**Team Impact:**
- **Sloth/Data:** No CSS changes needed; these are standalone assets.
- **Chunk:** If automating YouTube uploads or Open Graph tags, reference these paths.
- **Mouth:** Can reference these assets in documentation or README when linking the YouTube channel.

### 10. Clickable Badge Tags on Lab Cards

**Author:** Data (Frontend Developer)  
**Date:** 2025-07-15  
**Status:** Implemented

**Decision:**
Made all badge tags on lab cards clickable so they activate the matching sidebar filter:

- **Category badges** → set the `#categoryFilter` dropdown to the badge's value
- **Language badges** → check the matching checkbox in `#languagesFilters`
- **Framework badges** → check the matching checkbox in `#frameworksFilters`

**Implementation:**
1. Added `data-filter-type` and `data-filter-value` attributes to all badges in `cardHTML()`
2. Added a delegated click handler on `galleryGrid` that reads these attributes and activates the correct filter control, then calls `applyFilters()`
3. Added `cursor: pointer` to `.badge` in CSS to signal interactivity
4. Page scrolls to top after filter activation for immediate feedback

**Trade-offs:**
- Language/framework badges **check** the checkbox (additive) rather than clearing other filters first. This lets users build up multi-filter combos by clicking multiple badges. Category uses a dropdown so it naturally replaces.
- Chose event delegation from `galleryGrid` (consistent with existing click handlers) over per-badge listeners, for performance and simplicity.

### 11. Move Layout-Critical Styles to Global CSS

**Agent:** Data (Frontend Dev)  
**Date:** 2025-07-18  
**Status:** Implemented

**Context:**
The dashboard sidebar was rendering on top of the main content instead of beside it. Root cause: Blazor CSS isolation with `::deep .retro-app` compiles to `[b-hash] .retro-app` — a descendant selector. But `.retro-app` is the component's root element (it carries `b-hash` directly), so the selector never matches. This meant `display: flex` and `margin-left: 260px` never applied.

**Decision:**
Moved `.retro-app` and `.retro-main` styles (including their responsive `@media` rules) from `MainLayout.razor.css` to the global `retro-dashboard.css`. Kept only true descendant styles (`.retro-topbar`, `.topbar-title`, `.retro-content`, `.mobile-menu-toggle`) in the scoped CSS where `::deep` works correctly.

**Impact:**
- **Sloth:** Global CSS now defines `.retro-app` and `.retro-main` — keep in sync if changing layout structure.
- **All:** Blazor scoped CSS should not be used for root-element styles in any layout component.

### 12. KQL Queries Must Match Telemetry Event Contract

**Author:** Mikey (Lead/Architect)  
**Date:** 2025-07-16  
**Status:** Active  

**Context:**
Dashboard KQL queries referenced event names (`LabClick`, `ShareLab`, `FilterApplied`, `StartMenuInteraction`) that don't match what `script.js` actually sends (`LabCardClick`, `ShareClick`, `FilterChange`, `StartMenuOpen`). This caused the entire dashboard to show zero custom-event data.

**Decision:**
1. **KQL queries are a contract with `telemetry.js`/`script.js`.** Any change to event names or property keys in the frontend must be reflected in `AppInsightsService.cs` KQL queries, and vice versa.
2. **Event name mapping (current truth):**
   - `LabCardClick` — lab title click (has `labTitle`, `category`)
   - `CloneClick` — clone button (has `repoUrl`)
   - `VSCodeOpen` — VS Code button (has `repoUrl`)
   - `CodespaceOpen` — codespace button (has `repoUrl`)
   - `StarClick` — star button (has `repoUrl`)
   - `StartMenuOpen` — start menu open (has `labTitle`)
   - `ShareClick` — share button (has `labTitle`, `shareMethod`)
   - `FilterChange` — filter applied (has `filterType`, `value`)
   - `Search` — search (has `query`)
   - `VideoPlay` — video play (has `videoUrl`, `labTitle`)
   - `ThemeToggle` — theme switch (has `theme`)
   - `GitHubOpen` — GitHub link (has `repoUrl`)
3. **Future improvement:** Action events (CloneClick, VSCodeOpen, etc.) should include `labTitle` so per-lab action breakdown is possible in the dashboard.

**Impact:**
- **Data (Frontend):** Must keep event names/properties stable, or notify team of changes.
- **Mikey (Dashboard):** Must update KQL queries when event contract changes.
- **All:** The `/status` page now provides diagnostics for configuration issues.

### 13. Migrate App Insights Auth from API Key to Managed Identity (Infrastructure)

**Author:** Chunk (DevOps)  
**Date:** 2026-04-07  
**Status:** Implemented

**Context**

Application Insights API Keys are deprecated (retiring March 2026). The dashboard reads telemetry via the App Insights REST API and was using an API key for authentication. The Bicep `Microsoft.Insights/components/apikeys` resource is create-only and cannot be redeployed, making key rotation fragile.

**Decision**

Replaced API Key auth with Azure AD (Entra ID) Managed Identity:

1. **System-assigned Managed Identity** enabled on the Container App resource.
2. **Monitoring Reader role** (`43d0d8ad-25c7-4714-9337-8ba259a9fe05`) assigned to the Container App's MI, scoped to the App Insights resource — via a Bicep role assignment in `main.bicep`.
3. **Removed all API Key plumbing:** `appInsightsApiKey` parameter, `app-insights-api-key` secret, `AppInsights__ApiKey` env var, `APP_INSIGHTS_API_KEY` from parameters.json and the deploy workflow.
4. **Added `AppInsights__UseManagedIdentity=true`** env var so the app code can detect MI auth is available and use `DefaultAzureCredential` for token acquisition.

**Impact**

- **App code:** Must switch from API Key header (`x-api-key`) to Bearer token auth using `DefaultAzureCredential` (see Decision #14).
- **Secrets:** The `APP_INSIGHTS_API_KEY` GitHub secret can be deleted from repo settings.
- **Operations:** No more secret rotation needed for App Insights API access — MI handles it automatically.

**Files Changed**

| File | Change |
|------|--------|
| `infra/modules/container-app.bicep` | Added MI identity, removed API key param/secret/env, added UseManagedIdentity env var, outputs principalId |
| `infra/main.bicep` | Removed API key param, added Monitoring Reader role assignment on App Insights |
| `infra/main.parameters.json` | Removed `appInsightsApiKey` entry |
| `.github/workflows/deploy-dashboard.yml` | Removed `azd env set APP_INSIGHTS_API_KEY` line |

### 14. Migrate App Insights Auth from API Key to Managed Identity (Application)

**Author:** Mikey (Lead/Architect)  
**Date:** 2026-04-07  
**Status:** Implemented

**Context**

Application Insights API Keys are deprecated and retiring March 2026. The dashboard previously used `x-api-key` header auth with a manually-created API key. This was fragile (the Bicep `Microsoft.Insights/components/apikeys` resource is create-only and can't be redeployed) and required manual key management.

**Decision**

Replace API Key authentication with Azure AD (Entra ID) Managed Identity using `DefaultAzureCredential` from `Azure.Identity`.

- **Token scope:** `https://api.applicationinsights.io/.default`
- **Auth header:** `Authorization: Bearer {token}` (replaces `x-api-key`)
- **Configuration:** Only `AppInsights:AppId` is required; no API key needed
- **Local dev fallback:** `DefaultAzureCredential` tries Managed Identity → Azure CLI → VS Code → env vars

**Impact**

- **Infrastructure:** Must ensure Container App has system-assigned Managed Identity with "Monitoring Reader" role on the App Insights resource (see Decision #13). The `AppInsights__ApiKey` env var is no longer needed in Bicep/deployment.
- **Operations:** No more manual API key creation or rotation. Auth is automatic via Azure AD.
- **appsettings.json:** Empty `ApiKey` field kept for backward compatibility but not used by code.
- **Build:** NuGet restore and Blazor compilation succeed without warnings.

**Files Changed**

| File | Change |
|------|--------|
| `dashboard/Dashboard.csproj` | Added `Azure.Identity` package |
| `dashboard/src/Dashboard/Services/AppInsightsService.cs` | Bearer token auth, removed ApiKey, added `TestTokenAcquisitionAsync` |
| `dashboard/src/Dashboard/Components/Pages/Status.razor` | Managed Identity status rows, token acquisition test |
| `dashboard/src/Dashboard/Components/Pages/Home.razor` | Updated config guide |
| `dashboard/src/Dashboard/Components/Pages/PageViews.razor` | Updated warning banner |
| `dashboard/src/Dashboard/Components/Pages/LabEngagement.razor` | Updated warning banner |
| `dashboard/src/Dashboard/Components/Pages/VideoMetrics.razor` | Updated warning banner |
| `dashboard/src/Dashboard/Components/Pages/UserActivity.razor` | Updated warning banner |
| `dashboard/src/Dashboard/Components/Pages/ShareMetrics.razor` | Updated warning banner |

## Governance

- All meaningful changes require team consensus
- Document architectural decisions here
- Keep history focused on work, decisions focused on direction
