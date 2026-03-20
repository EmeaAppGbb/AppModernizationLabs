# Project Context

- **Owner:** Marco Antonio Silva
- **Project:** AppModernizationLabs — A GitHub.io retro 8-bit styled gallery website showcasing application modernization labs. Labs are loaded from external repos via APPMODLAB.MD metadata files. GitHub Actions automate lab ingestion and issue-based submissions.
- **Stack:** HTML, CSS, JavaScript, GitHub Actions, GitHub Pages
- **Created:** 2026-03-20

## Learnings

<!-- Append new learnings below. Each entry is something lasting about the project. -->
- Built `styles.css` with NES-inspired 8-bit retro aesthetic. Uses `Press Start 2P` + `VT323` Google Fonts, pixel borders (hard box-shadow, no blur), CRT scanline overlay, and CSS custom properties for light/dark theming.
- Data's CSS class contract requires `.open` for dropdown/sidebar/modal state toggling. Both `.open` and `.show` are supported for compatibility.
- Responsive grid: 3 columns → 2 (≤1024px) → 1 (≤768px). Sidebar becomes a fixed overlay on mobile.
- Color palette: primary `#00d4aa`, secondary `#ff6b9d`, accent `#ffd93d`. Dark bg `#0f0f23`, light bg `#f0e6d3`.

## Team Sync (2026-03-20)

**Cross-agent integration complete:**
- Data's HTML class contract implemented — `.header`, `.sidebar`, `.gallery-grid`, `.lab-card`, `.badge-*`, `.dropdown`, `.modal`, `.footer` styled
- Andy's SVG assets (20 files) — theme-aware `currentColor` icons automatically adapt to light/dark via CSS `color` property; fixed-color icons align with Sloth's NES palette
- Responsive breakpoints align with mobile UX: sidebar overlay ≤768px, grid 1 column ≤768px, compact sizing ≤480px
- CSS custom properties enable instant theme swaps and maintainability for future contributors
