# Decision: Switch GitHub Pages to branch-based deployment

**By:** Chunk (DevOps)  
**Date:** 2026-03-20  
**Status:** Proposed  

## Context

The previous `deploy-pages.yml` used `actions/deploy-pages` with the GitHub Pages Actions API, requiring `pages: write` and `id-token: write` permissions plus a staging `_site/` directory. The user requested switching to a simpler branch-based deployment model.

## Decision

- Created an orphan `pages` branch containing only publishable site files (`index.html`, `styles.css`, `script.js`, `appmodlab.json`, `assets/`).
- `deploy-pages.yml` now syncs site files from `main` to the `pages` branch via git push (no Pages API involved).
- GitHub Pages should be configured in repo settings to deploy from the `pages` branch, root `/`.
- The `process-labs` → `deploy-pages` chain works automatically via path triggers on `appmodlab.json`.

## Impact

- **Simpler permissions:** Only `contents: write` needed (no `pages:write` or `id-token:write`).
- **No staging directory:** Eliminated the `_site/` workaround.
- **Traceability:** Each pages commit includes the source main commit SHA.
- **Team action needed:** Repo settings must be updated to serve GitHub Pages from the `pages` branch.
