/* ============================================================
   script.js — App Modernization Labs Gallery
   Handles data loading, filtering, sorting, rendering, and UI
   ============================================================ */

(function () {
  'use strict';

  // ── State ──────────────────────────────────────────────────
  let allLabs = [];
  let filteredLabs = [];

  // ── DOM refs ───────────────────────────────────────────────
  const $ = (sel) => document.querySelector(sel);
  const $$ = (sel) => [...document.querySelectorAll(sel)];

  const galleryGrid = $('#galleryGrid');
  const noResults = $('#noResults');
  const resultsCount = $('#resultsCount');
  const searchInput = $('#searchInput');
  const categoryFilter = $('#categoryFilter');
  const industryFilter = $('#industryFilter');
  const sortSelect = $('#sortSelect');
  const clearFiltersBtn = $('#clearFilters');
  const themeToggle = $('#themeToggle');
  const addDropdownToggle = $('#addDropdownToggle');
  const addDropdownMenu = $('#addDropdownMenu');
  const mobileFilterToggle = $('#mobileFilterToggle');
  const sidebar = $('#sidebar');
  const sidebarOverlay = $('#sidebarOverlay');
  const sidebarClose = $('#sidebarClose');
  const videoModal = $('#videoModal');
  const videoFrame = $('#videoFrame');
  const modalClose = $('#modalClose');

  // Dynamic checkbox containers
  const checkboxContainers = {
    services: $('#servicesFilters'),
    languages: $('#languagesFilters'),
    frameworks: $('#frameworksFilters'),
    modernizationTools: $('#toolsFilters'),
    tags: $('#tagsFilters'),
  };

  // ── Data Loading ───────────────────────────────────────────
  async function loadData() {
    try {
      const res = await fetch('appmodlab.json');
      if (!res.ok) throw new Error(`HTTP ${res.status}`);
      allLabs = await res.json();
      populateCheckboxFilters();
      restoreFiltersFromURL();
      applyFilters();
    } catch (err) {
      console.error('Failed to load lab data:', err);
      galleryGrid.innerHTML = '<p class="load-error">⚠️ Could not load lab data. Please try again later.</p>';
    }
  }

  // ── Checkbox Filter Population ─────────────────────────────
  function collectUnique(key) {
    const set = new Set();
    allLabs.forEach((lab) => {
      const val = lab[key];
      if (Array.isArray(val)) val.forEach((v) => set.add(v));
      else if (val) set.add(val);
    });
    return [...set].sort();
  }

  function populateCheckboxFilters() {
    Object.entries(checkboxContainers).forEach(([key, container]) => {
      const values = collectUnique(key);
      container.innerHTML = values
        .map((v) => {
          const id = `filter-${key}-${v.replace(/[^a-zA-Z0-9]/g, '_')}`;
          return `
          <label class="filter-checkbox" for="${id}">
            <input type="checkbox" id="${id}" value="${v}" data-filter-group="${key}">
            <span>${v}</span>
          </label>`;
        })
        .join('');
    });
  }

  // ── Filtering ──────────────────────────────────────────────
  function getCheckedValues(group) {
    return $$(`input[data-filter-group="${group}"]:checked`).map((cb) => cb.value);
  }

  function applyFilters() {
    const search = searchInput.value.toLowerCase().trim();
    const category = categoryFilter.value;
    const industry = industryFilter.value;
    const checkedServices = getCheckedValues('services');
    const checkedLanguages = getCheckedValues('languages');
    const checkedFrameworks = getCheckedValues('frameworks');
    const checkedTools = getCheckedValues('modernizationTools');
    const checkedTags = getCheckedValues('tags');

    filteredLabs = allLabs.filter((lab) => {
      // Text search — title, description, tags
      if (search) {
        const haystack = [
          lab.title,
          lab.description,
          ...(lab.tags || []),
        ]
          .join(' ')
          .toLowerCase();
        if (!haystack.includes(search)) return false;
      }

      // Category
      if (category && lab.category !== category) return false;

      // Industry
      if (industry && lab.industry !== industry) return false;

      // Checkbox groups — OR within group, AND between groups
      if (checkedServices.length && !checkedServices.some((s) => (lab.services || []).includes(s))) return false;
      if (checkedLanguages.length && !checkedLanguages.some((l) => (lab.languages || []).includes(l))) return false;
      if (checkedFrameworks.length && !checkedFrameworks.some((f) => (lab.frameworks || []).includes(f))) return false;
      if (checkedTools.length && !checkedTools.some((t) => (lab.modernizationTools || []).includes(t))) return false;
      if (checkedTags.length && !checkedTags.some((t) => (lab.tags || []).includes(t))) return false;

      return true;
    });

    applySorting();
    updateURL();
    renderCards(filteredLabs);
  }

  // ── Sorting ────────────────────────────────────────────────
  function applySorting() {
    const sort = sortSelect.value;
    filteredLabs.sort((a, b) => {
      switch (sort) {
        case 'az':
          return a.title.localeCompare(b.title);
        case 'za':
          return b.title.localeCompare(a.title);
        case 'newest':
          return compareVersions(b.version, a.version);
        case 'oldest':
          return compareVersions(a.version, b.version);
        default:
          return 0;
      }
    });
  }

  function compareVersions(a, b) {
    const pa = (a || '0').split('.').map(Number);
    const pb = (b || '0').split('.').map(Number);
    for (let i = 0; i < Math.max(pa.length, pb.length); i++) {
      const diff = (pa[i] || 0) - (pb[i] || 0);
      if (diff !== 0) return diff;
    }
    return 0;
  }

  // ── URL State ──────────────────────────────────────────────
  function updateURL() {
    const params = new URLSearchParams();
    if (searchInput.value) params.set('q', searchInput.value);
    if (categoryFilter.value) params.set('category', categoryFilter.value);
    if (industryFilter.value) params.set('industry', industryFilter.value);
    if (sortSelect.value !== 'az') params.set('sort', sortSelect.value);

    Object.keys(checkboxContainers).forEach((group) => {
      const checked = getCheckedValues(group);
      if (checked.length) params.set(group, checked.join(','));
    });

    const qs = params.toString();
    const url = qs ? `${window.location.pathname}?${qs}` : window.location.pathname;
    history.replaceState(null, '', url);
  }

  function restoreFiltersFromURL() {
    const params = new URLSearchParams(window.location.search);

    if (params.has('q')) searchInput.value = params.get('q');
    if (params.has('category')) categoryFilter.value = params.get('category');
    if (params.has('industry')) industryFilter.value = params.get('industry');
    if (params.has('sort')) sortSelect.value = params.get('sort');

    Object.keys(checkboxContainers).forEach((group) => {
      if (params.has(group)) {
        const values = params.get(group).split(',');
        $$(`input[data-filter-group="${group}"]`).forEach((cb) => {
          cb.checked = values.includes(cb.value);
        });
      }
    });
  }

  // ── Card Rendering ─────────────────────────────────────────
  function renderCards(labs) {
    resultsCount.textContent = `${labs.length} lab${labs.length !== 1 ? 's' : ''} found`;

    if (labs.length === 0) {
      galleryGrid.innerHTML = '';
      noResults.style.display = 'flex';
      return;
    }

    noResults.style.display = 'none';
    galleryGrid.innerHTML = labs.map(cardHTML).join('');
  }

  function truncate(str, max) {
    if (!str) return '';
    return str.length > max ? str.slice(0, max) + '…' : str;
  }

  function cardHTML(lab) {
    const thumbSrc = lab.thumbnail || '';
    const thumbnailMarkup = thumbSrc
      ? `<img class="lab-card-thumbnail" src="${thumbSrc}" alt="${escapeHTML(lab.title)} thumbnail" loading="lazy">`
      : `<div class="lab-card-thumbnail placeholder-thumb">
           <span class="placeholder-icon">🎮</span>
         </div>`;

    const categoryClass = (lab.category || '').replace(/\s+/g, '-').toLowerCase();

    const languageBadges = (lab.languages || [])
      .map((l) => `<span class="badge badge-language">${escapeHTML(l)}</span>`)
      .join('');

    const frameworkBadges = (lab.frameworks || [])
      .map((f) => `<span class="badge badge-framework">${escapeHTML(f)}</span>`)
      .join('');

    const avatars = (lab.authors || [])
      .map(
        (a) =>
          `<img class="avatar" src="https://github.com/${encodeURIComponent(a)}.png?size=32" alt="${escapeHTML(a)}" title="${escapeHTML(a)}" loading="lazy" onerror="this.style.display='none'">`
      )
      .join('');

    const videoBtn = lab.video
      ? `<button class="action-btn btn-pixel video-btn" data-video="${escapeAttr(lab.video)}" title="Watch video">🎬</button>`
      : '';

    const cloneCmd = `git clone ${lab.repoUrl || '#'}.git`;
    const vsCodeUrl = `vscode://vscode.git/clone?url=${encodeURIComponent(lab.repoUrl || '')}`;
    const codespaceUrl = `https://github.com/codespaces/new?repo=${encodeURIComponent((lab.repoOwner || '') + '/' + (lab.repoName || ''))}`;

    return `
    <article class="lab-card" data-title="${escapeAttr(lab.title)}">
      ${thumbnailMarkup}
      <div class="lab-card-body">
        <h3 class="lab-card-title">${escapeHTML(lab.title)}</h3>
        <p class="lab-card-description">${escapeHTML(truncate(lab.description, 140))}</p>
        <div class="lab-card-tags">
          <span class="badge badge-category badge-${categoryClass}">${escapeHTML(lab.category || 'Uncategorized')}</span>
          ${languageBadges}
          ${frameworkBadges}
        </div>
        <div class="avatar-group">${avatars}</div>
      </div>
      <div class="lab-card-footer">
        <div class="lab-card-actions">
          <a class="action-btn btn-pixel" href="${lab.repoUrl ? lab.repoUrl + '/stargazers' : '#'}" target="_blank" rel="noopener" title="Star on GitHub">⭐</a>
          ${videoBtn}
          <a class="action-btn btn-pixel" href="${escapeAttr(lab.repoUrl || '#')}" target="_blank" rel="noopener" title="View on GitHub">👁️ GitHub</a>
          <div class="dropdown start-lab-dropdown">
            <button class="action-btn btn-primary btn-pixel start-lab-toggle" aria-haspopup="true" aria-expanded="false">🚀 Start</button>
            <div class="dropdown-menu start-lab-menu" role="menu">
              <button class="dropdown-item copy-clone" data-clone="${escapeAttr(cloneCmd)}" role="menuitem">📋 Clone</button>
              <a class="dropdown-item" href="${vsCodeUrl}" role="menuitem">💻 Open in VS Code</a>
              <a class="dropdown-item" href="${codespaceUrl}" target="_blank" rel="noopener" role="menuitem">☁️ Open in Codespace</a>
            </div>
          </div>
        </div>
      </div>
    </article>`;
  }

  function escapeHTML(str) {
    const el = document.createElement('span');
    el.textContent = str || '';
    return el.innerHTML;
  }

  function escapeAttr(str) {
    return (str || '').replace(/&/g, '&amp;').replace(/"/g, '&quot;').replace(/'/g, '&#39;').replace(/</g, '&lt;').replace(/>/g, '&gt;');
  }

  // ── Theme Toggle ───────────────────────────────────────────
  function initTheme() {
    const saved = localStorage.getItem('theme');
    const theme = saved || 'dark';
    document.body.setAttribute('data-theme', theme);
    updateThemeIcon(theme);
  }

  function toggleTheme() {
    const current = document.body.getAttribute('data-theme');
    const next = current === 'dark' ? 'light' : 'dark';
    document.body.setAttribute('data-theme', next);
    localStorage.setItem('theme', next);
    updateThemeIcon(next);
  }

  function updateThemeIcon(theme) {
    const icon = themeToggle.querySelector('.theme-icon');
    icon.textContent = theme === 'dark' ? '🌙' : '☀️';
  }

  // ── Add Dropdown ───────────────────────────────────────────
  function toggleAddDropdown(e) {
    e.stopPropagation();
    const open = addDropdownMenu.classList.toggle('open');
    addDropdownToggle.setAttribute('aria-expanded', open);
  }

  // ── Start Lab Dropdown ─────────────────────────────────────
  function handleStartLabToggle(e) {
    const btn = e.target.closest('.start-lab-toggle');
    if (!btn) return;
    e.stopPropagation();

    // Close all other start-lab dropdowns
    $$('.start-lab-menu.open').forEach((m) => {
      if (m !== btn.nextElementSibling) m.classList.remove('open');
    });

    const menu = btn.nextElementSibling;
    const open = menu.classList.toggle('open');
    btn.setAttribute('aria-expanded', open);
  }

  // ── Copy to Clipboard ─────────────────────────────────────
  function handleCopyClone(e) {
    const btn = e.target.closest('.copy-clone');
    if (!btn) return;
    e.preventDefault();

    const cmd = btn.getAttribute('data-clone');
    navigator.clipboard.writeText(cmd).then(
      () => {
        const orig = btn.textContent;
        btn.textContent = '✅ Copied!';
        setTimeout(() => (btn.textContent = orig), 2000);
      },
      () => {
        // Fallback
        const ta = document.createElement('textarea');
        ta.value = cmd;
        document.body.appendChild(ta);
        ta.select();
        document.execCommand('copy');
        document.body.removeChild(ta);
        const orig = btn.textContent;
        btn.textContent = '✅ Copied!';
        setTimeout(() => (btn.textContent = orig), 2000);
      }
    );
  }

  // ── Video Modal ────────────────────────────────────────────
  function openVideoModal(url) {
    // Convert YouTube watch URLs to embed
    let embedUrl = url;
    const ytMatch = url.match(/youtube\.com\/watch\?v=([^&]+)/);
    if (ytMatch) embedUrl = `https://www.youtube.com/embed/${ytMatch[1]}`;
    videoFrame.src = embedUrl;
    videoModal.style.display = 'flex';
    document.body.style.overflow = 'hidden';
  }

  function closeVideoModal() {
    videoFrame.src = '';
    videoModal.style.display = 'none';
    document.body.style.overflow = '';
  }

  // ── Mobile Sidebar ─────────────────────────────────────────
  function openSidebar() {
    sidebar.classList.add('open');
    sidebarOverlay.classList.add('open');
    document.body.style.overflow = 'hidden';
  }

  function closeSidebar() {
    sidebar.classList.remove('open');
    sidebarOverlay.classList.remove('open');
    document.body.style.overflow = '';
  }

  // ── Clear Filters ──────────────────────────────────────────
  function clearAllFilters() {
    searchInput.value = '';
    categoryFilter.value = '';
    industryFilter.value = '';
    sortSelect.value = 'az';
    $$('input[type="checkbox"][data-filter-group]').forEach((cb) => (cb.checked = false));
    applyFilters();
  }

  // ── Close dropdowns on outside click ───────────────────────
  function handleOutsideClick(e) {
    // Add dropdown
    if (!e.target.closest('#addDropdown')) {
      addDropdownMenu.classList.remove('open');
      addDropdownToggle.setAttribute('aria-expanded', 'false');
    }
    // Start lab dropdowns
    if (!e.target.closest('.start-lab-dropdown')) {
      $$('.start-lab-menu.open').forEach((m) => m.classList.remove('open'));
      $$('.start-lab-toggle[aria-expanded="true"]').forEach((b) => b.setAttribute('aria-expanded', 'false'));
    }
  }

  // ── Event Binding ──────────────────────────────────────────
  function bindEvents() {
    // Filters
    searchInput.addEventListener('input', debounce(applyFilters, 250));
    categoryFilter.addEventListener('change', applyFilters);
    industryFilter.addEventListener('change', applyFilters);
    sortSelect.addEventListener('change', applyFilters);
    clearFiltersBtn.addEventListener('click', clearAllFilters);

    // Dynamic checkbox filters — delegate
    Object.values(checkboxContainers).forEach((container) => {
      container.addEventListener('change', applyFilters);
    });

    // Theme
    themeToggle.addEventListener('click', toggleTheme);

    // Add dropdown
    addDropdownToggle.addEventListener('click', toggleAddDropdown);

    // Start Lab dropdown + copy — delegate from grid
    galleryGrid.addEventListener('click', (e) => {
      handleStartLabToggle(e);
      handleCopyClone(e);

      // Video button
      const videoBtn = e.target.closest('.video-btn');
      if (videoBtn) {
        openVideoModal(videoBtn.getAttribute('data-video'));
      }
    });

    // Modal close
    modalClose.addEventListener('click', closeVideoModal);
    videoModal.addEventListener('click', (e) => {
      if (e.target === videoModal) closeVideoModal();
    });

    // Mobile sidebar
    mobileFilterToggle.addEventListener('click', openSidebar);
    sidebarClose.addEventListener('click', closeSidebar);
    sidebarOverlay.addEventListener('click', closeSidebar);

    // Outside click
    document.addEventListener('click', handleOutsideClick);

    // Escape key closes modal / sidebar
    document.addEventListener('keydown', (e) => {
      if (e.key === 'Escape') {
        closeVideoModal();
        closeSidebar();
      }
    });
  }

  // ── Utilities ──────────────────────────────────────────────
  function debounce(fn, ms) {
    let timer;
    return (...args) => {
      clearTimeout(timer);
      timer = setTimeout(() => fn.apply(this, args), ms);
    };
  }

  // ── Init ───────────────────────────────────────────────────
  function init() {
    initTheme();
    bindEvents();
    loadData();
  }

  if (document.readyState === 'loading') {
    document.addEventListener('DOMContentLoaded', init);
  } else {
    init();
  }
})();
