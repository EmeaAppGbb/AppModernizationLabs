/* ============================================================
   Retro Charts — Chart.js interop for the 8-bit dashboard
   Uses the NES color palette for all chart styling
   ============================================================ */

window.RetroCharts = {
  // Shared retro color palette
  colors: {
    primary:   '#00d4aa',
    secondary: '#ff6b9d',
    accent:    '#ffd93d',
    success:   '#2ed573',
    error:     '#ff4757',
    purple:    '#a855f7',
    orange:    '#ff9f43',
    cyan:      '#00cec9',
    grid:      'rgba(74, 74, 138, 0.2)',
    text:      '#a0a0c0',
    white:     '#e8e8e8'
  },

  palette: [
    '#00d4aa', '#ff6b9d', '#ffd93d', '#2ed573', '#a855f7',
    '#ff9f43', '#00cec9', '#ff4757', '#74b9ff', '#fd79a8'
  ],

  // Shared Chart.js defaults for the retro aesthetic
  getDefaults() {
    return {
      responsive: true,
      maintainAspectRatio: false,
      plugins: {
        legend: {
          labels: {
            color: this.colors.text,
            font: { family: "'VT323', monospace", size: 16 },
            padding: 16,
            usePointStyle: true,
            pointStyle: 'rect'
          }
        },
        tooltip: {
          backgroundColor: '#1a1a3e',
          titleColor: this.colors.accent,
          bodyColor: this.colors.white,
          borderColor: this.colors.primary,
          borderWidth: 2,
          titleFont: { family: "'Press Start 2P', cursive", size: 8 },
          bodyFont: { family: "'VT323', monospace", size: 16 },
          padding: 12,
          cornerRadius: 0,
          displayColors: true,
          boxPadding: 4
        }
      }
    };
  },

  getScaleDefaults() {
    return {
      x: {
        ticks: {
          color: this.colors.text,
          font: { family: "'VT323', monospace", size: 14 },
          maxRotation: 45
        },
        grid: { color: this.colors.grid, lineWidth: 1 },
        border: { color: this.colors.grid }
      },
      y: {
        ticks: {
          color: this.colors.text,
          font: { family: "'VT323', monospace", size: 14 }
        },
        grid: { color: this.colors.grid, lineWidth: 1 },
        border: { color: this.colors.grid },
        beginAtZero: true
      }
    };
  },

  // Destroy existing chart on canvas if any
  destroyIfExists(canvasId) {
    const canvas = document.getElementById(canvasId);
    if (!canvas) return null;
    const existing = Chart.getChart(canvas);
    if (existing) existing.destroy();
    return canvas.getContext('2d');
  },

  // ── Line Chart ─────────────────────────────────────────────

  createLineChart(canvasId, labels, data, label) {
    const ctx = this.destroyIfExists(canvasId);
    if (!ctx) return;

    new Chart(ctx, {
      type: 'line',
      data: {
        labels: labels,
        datasets: [{
          label: label,
          data: data,
          borderColor: this.colors.primary,
          backgroundColor: 'rgba(0, 212, 170, 0.1)',
          borderWidth: 3,
          fill: true,
          tension: 0,
          pointBackgroundColor: this.colors.primary,
          pointBorderColor: this.colors.accent,
          pointBorderWidth: 2,
          pointRadius: 4,
          pointHoverRadius: 8,
          pointStyle: 'rect'
        }]
      },
      options: {
        ...this.getDefaults(),
        scales: this.getScaleDefaults()
      }
    });
  },

  // ── Bar Chart ──────────────────────────────────────────────

  createBarChart(canvasId, labels, data, label) {
    const ctx = this.destroyIfExists(canvasId);
    if (!ctx) return;

    const backgroundColors = labels.map((_, i) => this.palette[i % this.palette.length]);
    const borderColors = backgroundColors;

    new Chart(ctx, {
      type: 'bar',
      data: {
        labels: labels,
        datasets: [{
          label: label,
          data: data,
          backgroundColor: backgroundColors.map(c => c + '80'),
          borderColor: borderColors,
          borderWidth: 2,
          borderRadius: 0,
          borderSkipped: false
        }]
      },
      options: {
        ...this.getDefaults(),
        scales: this.getScaleDefaults(),
        plugins: {
          ...this.getDefaults().plugins,
          legend: { display: false }
        }
      }
    });
  },

  // ── Doughnut / Pie Chart ───────────────────────────────────

  createDoughnutChart(canvasId, labels, data, label) {
    const ctx = this.destroyIfExists(canvasId);
    if (!ctx) return;

    const backgroundColors = labels.map((_, i) => this.palette[i % this.palette.length]);

    new Chart(ctx, {
      type: 'doughnut',
      data: {
        labels: labels,
        datasets: [{
          label: label,
          data: data,
          backgroundColor: backgroundColors.map(c => c + 'CC'),
          borderColor: '#1a1a3e',
          borderWidth: 3,
          hoverOffset: 8
        }]
      },
      options: {
        ...this.getDefaults(),
        cutout: '55%',
        plugins: {
          ...this.getDefaults().plugins,
          legend: {
            ...this.getDefaults().plugins.legend,
            position: 'bottom'
          }
        }
      }
    });
  }
};
