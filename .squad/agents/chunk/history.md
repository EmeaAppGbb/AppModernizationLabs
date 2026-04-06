# Project Context

- **Owner:** Marco Antonio Silva
- **Project:** AppModernizationLabs — A GitHub.io retro 8-bit styled gallery website showcasing application modernization labs. Labs are loaded from external repos via APPMODLAB.MD metadata files. GitHub Actions automate lab ingestion and issue-based submissions.
- **Stack:** HTML, CSS, JavaScript, GitHub Actions, GitHub Pages
- **Created:** 2026-03-20

## Team Updates (2026-03-20)

- **Pages deployment strategy accepted:** Chunk's branch-based approach eliminates GitHub Pages API complexity. Team awaits repo settings configuration.
- **Workflow chaining verified:** `process-labs` → `deploy-pages` chain confirmed working via path triggers on `appmodlab.json`.

## Learnings

<!-- Append new learnings below. Each entry is something lasting about the project. -->

- **Pages deployment switched to branch-based model:** Replaced `actions/deploy-pages` (GitHub Pages API) with a `pages` orphan branch strategy. The `deploy-pages.yml` workflow now syncs site files from `main` to the `pages` branch on push. GitHub Pages should be configured to serve from the `pages` branch root. This avoids the `_site` staging directory and `upload-pages-artifact` entirely.
- **Workflow chaining via path triggers:** `process-labs.yml` commits `appmodlab.json` to `main` → `deploy-pages.yml` triggers on `appmodlab.json` path changes → syncs to `pages` branch. No explicit workflow dispatch needed; the path trigger creates automatic chaining.
- **Orphan branch creation gotcha on Windows:** When creating an orphan branch, all tracked files become untracked. Switching back to `main` requires `git checkout -f main` to force-overwrite the untracked files that conflict with main's tracked files.

- **Local dev server added:** Created `package.json` with `npm run dev` / `npm start` scripts using `npx serve . -l 3000`. No dependencies to install — `npx` handles it. `.gitignore` updated with `node_modules/` and `.env`. `deploy-pages.yml` updated to sync `package.json` to the pages branch and trigger on its changes.
- **CI/CD pipeline architecture:** Three workflows established — `process-labs.yml` (reads labs.md → builds appmodlab.json), `process-new-lab-issue.yml` (validates issue submissions → creates PRs), `deploy-pages.yml` (deploys site to GitHub Pages). All use Python for YAML/API processing — no extra tooling needed since `pyyaml` is pip-installable and `urllib` is stdlib.
- **labs.md is the source of truth:** The master lab list lives in `labs.md` at repo root. One GitHub URL per line. The process-labs workflow parses it, fetches APPMODLAB.MD from each repo via GitHub API, and outputs `appmodlab.json`.
- **GitHub Pages deployment uses a staging directory (`_site`):** The `upload-pages-artifact` action doesn't support `exclude`, so we copy files to `_site/` first, omitting `.git`, `.squad`, `.github`, `.copilot`, etc.
- **Issue-based lab submission flow:** Issues labeled `new-lab` trigger validation of the repo URL and APPMODLAB.MD, then auto-create a branch + PR. Invalid submissions get a comment explaining the failure and the issue is closed.
- **AZD infrastructure for dashboard:** Full `azd` setup at repo root — `azure.yaml` + `infra/` with modular Bicep. Five modules: log-analytics, app-insights, container-registry, container-app-env, container-app. `azd up` provisions all Azure resources and deploys the dashboard container.
- **Container App config pattern:** External ingress on port 8080, 0–3 replicas, 0.5 CPU / 1Gi memory. App Insights connection string passed as a Kubernetes-style secret; App ID as a plain env var. ACR admin credentials used for image pulls.
- **Resource naming convention:** `{abbreviation}{envName}-{uniqueSuffix}` where uniqueSuffix is `uniqueString(resourceGroup().id, envName, location)`. Abbreviations defined in `infra/abbreviations.json`. ACR name strips hyphens (Azure requirement).
- **deploy-dashboard.yml workflow:** Triggers on push to `main` when `dashboard/`, `infra/`, or `azure.yaml` change; also supports `workflow_dispatch`. Uses `AZURE_CREDENTIALS` secret (service principal JSON). Steps: checkout → Azure login → azd provision → Docker build+push → azd deploy → summary.

## Team Sync (2026-04-06, Session 4 — Telemetry & Dashboard Batch)

**AZD infrastructure implementation complete:**
- Full `azure.yaml` at repo root defines `dashboard` service pointing to `./dashboard/src/Dashboard`
- Five Bicep modules created in `infra/modules/`: log-analytics, app-insights, container-registry, container-app-env, container-app
- `infra/main.bicep` orchestration with parameter binding via `main.parameters.json`
- Container App configuration: external ingress port 8080, auto-scales 0–3 replicas, 0.5 CPU / 1Gi memory
- App Insights shared between gallery website (JS SDK telemetry from Data) and dashboard (REST API reads from Mikey)
- `deploy-dashboard.yml` GitHub Actions workflow: checkout → Azure login → azd provision → Docker build+push to ACR → azd deploy → summary
- Resource naming follows `{abbreviation}{envName}-{uniqueSuffix}` pattern (abbreviations.json)
- ACR name strips hyphens per Azure requirement
- App Insights connection string passed as Kubernetes-style secret; App ID as plain env var

**Configuration requirements:**
- `AZURE_CREDENTIALS` secret in GitHub (service principal JSON)
- `AZURE_ENV_NAME` GitHub var (default: `appmodlabs`)
- `AZURE_LOCATION` GitHub var (default: `eastus2`)
- Deployment flow: `azd up` from repo root provisions all resources and deploys dashboard

**Integration points:**
- Data: Must provide `APP_INSIGHTS_CONNECTION_STRING` to be injected into gallery site JS during deployment
- Mikey: Dashboard Dockerfile at `dashboard/Dockerfile`, source at `dashboard/src/Dashboard` — fully ready
- All: `azd up` command is the deployment orchestrator for entire dashboard stack
- Dashboard reads App Insights telemetry via REST API (Kusto queries in Mikey's AppInsightsService)

**Handoff status:**
- Infrastructure ready for provision and deployment
- GitHub Actions workflow configured for CI/CD
- All team members notified of deployment flow and integration requirements

## Team Sync (2026-03-20)

**Pipeline readiness verified:**
- Data's `appmodlab.json` will be auto-generated by `process-labs` workflow — no manual updates needed
- Mouth's issue templates and LABTemplate.md guide community submissions; Chunk's validation workflows enforce metadata contracts
- Mouth's CONTRIBUTING.md and README document the full submission workflow — reduces support overhead
- All three workflows tested locally; ready for GitHub Actions deployment
- `labs.md` remains human-editable for quick lab additions without code changes

## Team Sync (2026-04-06, Session 3)

**Scope expansion and local dev implementation complete:**
- Created `package.json` with zero-dependency local dev server (npx serve . -l 3000)
- Updated `deploy-pages.yml` to sync package.json and validate on changes
- `.gitignore` updated with node_modules/ and .env
- Local dev server enables site testing without GitHub Pages (works with Data's base href removal)
- Confirmed backward compatibility — no breaking changes to CI/CD pipeline
- All team members notified of local dev capability and scope expansion

## Team Sync (AZD Infrastructure)

**Azure Developer CLI (azd) infrastructure created for dashboard deployment:**
- Full Bicep module set at `infra/` — Log Analytics, App Insights, ACR, Container Apps Env, Container App
- `azure.yaml` at repo root defines `dashboard` service for `azd up`
- `main.bicep` outputs `APP_INSIGHTS_CONNECTION_STRING`, `APP_INSIGHTS_APP_ID`, `DASHBOARD_URL`, `AZURE_CONTAINER_REGISTRY_ENDPOINT`
- Container App configured: external ingress port 8080, 0-3 replicas, 0.5 CPU / 1Gi memory
- App Insights connection string passed as secret env vars to container; App ID as plain env var
- Resource naming: `{abbreviation}{envName}-{uniqueSuffix}` pattern using abbreviations.json
- GitHub Actions workflow `deploy-dashboard.yml` — triggers on dashboard/ or infra/ changes to main, plus manual dispatch
- Workflow: Azure login → azd provision → Docker build+push to ACR → azd deploy → summary output
- ACR uses admin credentials; Container App pulls via registry secret

