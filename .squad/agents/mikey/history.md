# Project Context

- **Owner:** Marco Antonio Silva
- **Project:** AppModernizationLabs — A GitHub.io retro 8-bit styled gallery website showcasing application modernization labs. Labs are loaded from external repos via APPMODLAB.MD metadata files. GitHub Actions automate lab ingestion and issue-based submissions.
- **Stack:** HTML, CSS, JavaScript, GitHub Actions, GitHub Pages
- **Created:** 2026-03-20

## Learnings

<!-- Append new learnings below. Each entry is something lasting about the project. -->

### 2026-04-06: Backlog Expansion — Agentic Application Enablement

**Scope change:** Project expanded from "App Modernization Labs" (3 categories) to "Agentic Application Enablement Labs" (6 categories). The backlog now covers:

1. **Code Modernization** (10 labs) — existing, unchanged
2. **Infrastructure Modernization** (5 labs) — existing, unchanged
3. **Data Modernization** (5 labs) — existing, unchanged
4. **Agentic Software Development** (7 labs) — NEW, SQUAD-focused
5. **Spec-Driven Development** (5 labs) — NEW, Spec2Cloud-focused
6. **Cross-Cutting / End-to-End** (3 labs) — NEW, multi-tool pipelines

**Architecture decisions:**
- Category values for APPMODLAB.MD are: `Code Modernization`, `Infrastructure Modernization`, `Data Modernization`, `Agentic Software Development`, `Spec-Driven Development`, `Cross-Cutting`
- Backlog table format preserved: Priority | Lab Title | Category | Languages | Description | Status
- Short category labels in tables (`Agentic`, `Spec-Driven`, `Cross-Cutting`) for readability; full names used in APPMODLAB.MD metadata
- Community suggestions split into "Modernization Ideas" and "Agentic Development Ideas" subsections
- P1 labs for new categories focus on "getting started" and foundational workflows; P2/P3 are advanced/specialized
- Cross-Cutting section exists for labs that span multiple categories (e.g., Spec2Cloud → SQUAD → deploy)

## Team Sync (2026-04-06, Session 3)

**Scope expansion and backlog implementation complete:**
- Backlog expanded to 35+ labs across 6 categories (3 new: Agentic, Spec-Driven, Cross-Cutting)
- Priority framework established (P1: foundational, P2: integration, P3: advanced)
- New category structure reflected in backlog tables and metadata
- Community suggestions split into modernization and agentic development ideas
- All team members notified of expanded scope and category decisions
- Pending: Sloth/Andy design category icons/colors for new categories

### Dashboard App — Blazor Server Telemetry Dashboard

**Architecture decisions:**
- Dashboard lives at `dashboard/` in the repo root as a standalone .NET Blazor Server app
- Targets net9.0 (marked with TODO comments to upgrade to net10.0 when GA)
- Structure: `dashboard/Dashboard.sln` → `dashboard/src/Dashboard/` (single project, clean layout)
- Uses Application Insights REST API (`api.applicationinsights.io`) with Kusto queries — reads-only, no SDK dependency
- YouTube Data API v3 integration is optional — service gracefully returns empty results if not configured
- Chart.js via JS interop (`wwwroot/js/charts.js`) with `window.RetroCharts` namespace for all chart rendering
- Retro 8-bit CSS (`wwwroot/css/retro-dashboard.css`) shares the exact same palette and fonts as the main gallery site
- 5-minute TTL memory cache on all App Insights queries to avoid API throttling
- Configuration via `appsettings.json`: `AppInsights:AppId`, `AppInsights:ApiKey`, `YouTube:ApiKey`, `YouTube:ChannelId`
- Dockerized with multi-stage build targeting port 8080

**Key file paths:**
- Solution: `dashboard/Dashboard.sln`
- Main project: `dashboard/src/Dashboard/Dashboard.csproj`
- Services: `dashboard/src/Dashboard/Services/AppInsightsService.cs`, `YouTubeAnalyticsService.cs`
- Models: `dashboard/src/Dashboard/Models/` (5 metric model files)
- Pages: `dashboard/src/Dashboard/Components/Pages/` (Home, PageViews, LabEngagement, UserActivity, VideoMetrics, ShareMetrics)
- Retro CSS: `dashboard/src/Dashboard/wwwroot/css/retro-dashboard.css`
- Chart interop: `dashboard/src/Dashboard/wwwroot/js/charts.js`
- Docker: `dashboard/Dockerfile`, `dashboard/.dockerignore`

**Razor gotcha:** Avoid naming loop variables `page` in Razor files — the Razor compiler mistakes `@page.X` for the `@page` directive. Use `pv`, `item`, etc. instead.

## Team Sync (2026-04-06, Session 4 — Telemetry & Dashboard Batch)

**Blazor dashboard implementation complete:**
- Full .NET 9 Blazor Server dashboard at `dashboard/` with 6 pages: Home, PageViews, LabEngagement, UserActivity, VideoMetrics, ShareMetrics
- Uses Application Insights REST API (`api.applicationinsights.io`) with Kusto queries — read-only, no SDK instrumentation
- Chart.js via JS interop with `window.RetroCharts` namespace — lighter than Blazor-native charting, matches retro aesthetic
- Retro 8-bit CSS (`wwwroot/css/retro-dashboard.css`) mirrors main gallery palette and fonts for visual consistency
- 5-minute TTL memory cache on all App Insights queries to prevent throttling
- Dockerfile (multi-stage) ready for Azure Container Apps, exposes port 8080
- Optional YouTube API integration with graceful fallback if not configured
- Configuration via `appsettings.json`: `AppInsights:AppId`, `AppInsights:ApiKey`, `YouTube:ApiKey`, `YouTube:ChannelId`

**Integration points:**
- Data: Kusto queries in `AppInsightsService.cs` map to custom telemetry events from `telemetry.js` (LabCardClick, VideoPlay, ShareClick, etc.)
- Chunk: Dashboard ready for Azure Container Apps deployment via azd infrastructure
- Frontend (Sloth/Andy): CSS palette must remain in sync with main gallery `styles.css`

**Architecture notes:**
- REST API approach (vs SDK) keeps dashboard read-only and decouples from instrumentation logic
- Memory cache prevents API throttling; 5-min TTL balances freshness vs performance
- Kusto queries capture domain-specific metrics: page views by category, video engagement, share impact, user activity patterns
- Targeting net9.0 with TODO markers for net10.0 upgrade when GA

