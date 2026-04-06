# Team Decisions

## Decision: Switch GitHub Pages to branch-based deployment

**By:** Chunk (DevOps)  
**Date:** 2026-03-20  
**Status:** Accepted

### Context

The previous `deploy-pages.yml` used `actions/deploy-pages` with the GitHub Pages Actions API, requiring `pages: write` and `id-token: write` permissions plus a staging `_site/` directory. The user requested switching to a simpler branch-based deployment model.

### Decision

- Created an orphan `pages` branch containing only publishable site files (`index.html`, `styles.css`, `script.js`, `appmodlab.json`, `assets/`).
- `deploy-pages.yml` now syncs site files from `main` to the `pages` branch via git push (no Pages API involved).
- GitHub Pages should be configured in repo settings to deploy from the `pages` branch, root `/`.
- The `process-labs` → `deploy-pages` chain works automatically via path triggers on `appmodlab.json`.

### Impact

- **Simpler permissions:** Only `contents: write` needed (no `pages:write` or `id-token:write`).
- **No staging directory:** Eliminated the `_site/` workaround.
- **Traceability:** Each pages commit includes the source main commit SHA.
- **Team action needed:** Repo settings must be updated to serve GitHub Pages from the `pages` branch.

---

## Decision: Scope expansion — Agentic Application Enablement

**By:** Marco Antonio Silva (via Copilot)  
**Date:** 2026-04-06  
**Status:** Accepted

### Context

The portal scope is expanding beyond Application Modernization. The project is becoming a hub for agentic application enablement patterns using SQUAD and Spec2Cloud methodologies.

### Decision

- **Project name:** "App Modernization Labs" → "Agentic Application Enablement Labs" / "AgenticAppEnablementLabs"
- **New categories:** Six lab categories now exist:
  - Code Modernization (existing)
  - Infrastructure Modernization (existing)
  - Data Modernization (existing)
  - Agentic Software Development (new — SQUAD-based)
  - Spec-Driven Development (new — Spec2Cloud methodology)
  - Cross-Cutting / End-to-End (new — multi-tool pipelines)
- **Backward compatibility:** GitHub repo name unchanged; APPMODLAB.MD field names preserved
- **Frontend changes:** `<base href>` removed; local dev via `npm run dev`
- **Documentation:** All templates, README, issue templates, and backlog updated to reflect new scope

### Impact

- **Frontend:** Site works locally (file://) and on GitHub Pages; no build step needed
- **Documentation:** Community submission guidance covers all 6 categories
- **Backlog:** 35+ labs across all categories; priority framework (P1/P2/P3) established
- **Team:** Sloth/Andy need category icons for new categories; Chunk's workflows validated for new category values

---

## Decision: Local Development Server via package.json

**By:** Chunk (DevOps)  
**Date:** 2026-04-06  
**Status:** Accepted

### Context

The site previously required GitHub Pages deployment to run because of `<base href>` and had no local dev story. The user requested local testability.

### Decision

- Added `package.json` with `npm run dev` / `npm start` using `npx serve . -l 3000`
- Zero dependencies — `npx` downloads `serve` on demand
- `.gitignore` updated with `node_modules/` and `.env`
- `deploy-pages.yml` updated to sync `package.json` to pages branch

### Impact

- Developers can run `npm run dev` to preview locally at `http://localhost:3000`
- No `npm install` required — `npx` handles everything
- Works in conjunction with removal of `<base href>` to enable local and GitHub Pages deployment
- No changes to CI/CD behavior
