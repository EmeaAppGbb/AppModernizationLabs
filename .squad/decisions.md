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

## Governance

- All meaningful changes require team consensus
- Document architectural decisions here
- Keep history focused on work, decisions focused on direction
