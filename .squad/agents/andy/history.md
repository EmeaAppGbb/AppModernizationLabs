# Project Context

- **Owner:** Marco Antonio Silva
- **Project:** AppModernizationLabs — A GitHub.io retro 8-bit styled gallery website showcasing application modernization labs. Labs are loaded from external repos via APPMODLAB.MD metadata files. GitHub Actions automate lab ingestion and issue-based submissions.
- **Stack:** HTML, CSS, JavaScript, GitHub Actions, GitHub Pages
- **Created:** 2026-03-20

## Learnings

<!-- Append new learnings below. Each entry is something lasting about the project. -->

- **2026-03-20**: Created complete pixel art SVG asset library (20 files) in `assets/` directory. All SVGs use `viewBox` for scalability and `shape-rendering="crispEdges"` for pixel-perfect rendering. Built with `<rect>` elements on pixel grids for authentic 8-bit aesthetic. NES-inspired palette: teal `#00d4aa`, hot pink `#ff6b9d`, golden `#ffd93d`, dark `#0f0f23`, light `#e8e8e8`, border `#4a4a8a`. Icons using `currentColor` for theme adaptability: github, clone, codespace, search, filter, close, arrow-down. Category icons (code/infra/data) use teal. Star icon is golden. Sun/moon theme toggles use yellow/white respectively. VS Code icon uses blue tones. Decorative border-top.svg tiles horizontally via CSS `background-repeat`. Game-over.svg features a sad pixel robot with "GAME OVER" text for empty states. OG image is 1200x630 with full "APP MOD LABS" pixel text and "LEVEL UP YOUR APPS" tagline.

## Team Sync (2026-03-20)

**Cross-agent deliverables confirmed:**
- Sloth's CSS targeting finalized — `currentColor` icons automatically inherit Sloth's theme color properties, fixed-color assets align with committed palette
- Data's responsive grid and state classes (`.open`) confirmed working with asset scaling and hover effects
- All 20 SVG assets ready for immediate HTML integration — no further iteration needed
- Asset naming conventions stable for long-term gallery maintenance and future contributor clarity
