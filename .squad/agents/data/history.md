# Project Context

- **Owner:** Marco Antonio Silva
- **Project:** AppModernizationLabs — A GitHub.io retro 8-bit styled gallery website showcasing application modernization labs. Labs are loaded from external repos via APPMODLAB.MD metadata files. GitHub Actions automate lab ingestion and issue-based submissions.
- **Stack:** HTML, CSS, JavaScript, GitHub Actions, GitHub Pages
- **Created:** 2026-03-20

## Learnings

<!-- Append new learnings below. Each entry is something lasting about the project. -->
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
