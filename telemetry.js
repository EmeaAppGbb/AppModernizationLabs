/* ============================================================
   telemetry.js — Application Insights Telemetry
   Non-blocking wrapper: all tracking degrades gracefully
   when App Insights is not configured.
   ============================================================ */

(function () {
  'use strict';

  // Configuration — connection string from deployment env or empty
  const connectionString = (window.APP_INSIGHTS_CONNECTION_STRING || '').trim();

  let appInsights = null;

  /**
   * Initialize Application Insights SDK if a connection string is provided.
   * Called once on page load; silently no-ops if SDK or config is missing.
   */
  function init() {
    if (!connectionString) return;

    try {
      var sdkRef = window.Microsoft &&
        window.Microsoft.ApplicationInsights &&
        window.Microsoft.ApplicationInsights.ApplicationInsights;

      if (!sdkRef) {
        console.warn('[Telemetry] Application Insights SDK not loaded — tracking disabled.');
        return;
      }

      var snippet = new sdkRef({
        config: {
          connectionString: connectionString,
          enableAutoRouteTracking: false, // SPA — we handle page views
          disableFetchTracking: false,
          enableCorsCorrelation: false,
          enableRequestHeaderTracking: false,
          enableResponseHeaderTracking: false,
        },
      });
      snippet.loadAppInsights();
      snippet.trackPageView(); // initial page view
      appInsights = snippet;
    } catch (err) {
      console.warn('[Telemetry] Failed to initialize Application Insights:', err);
    }
  }

  /**
   * Track a named custom event with optional properties.
   * Safe to call even when App Insights is not configured.
   * @param {string} name  — event name, e.g. "LabCardClick"
   * @param {Object} [properties] — key/value properties bag
   */
  function trackEvent(name, properties) {
    if (!appInsights) return;
    try {
      appInsights.trackEvent({ name: name }, properties || {});
    } catch (_) {
      // swallow — telemetry must never break the app
    }
  }

  /**
   * Track a page view (manual).
   * @param {string} [pageName]
   */
  function trackPageView(pageName) {
    if (!appInsights) return;
    try {
      appInsights.trackPageView({ name: pageName || document.title });
    } catch (_) {
      // swallow
    }
  }

  // Expose global telemetry helper
  window.telemetry = {
    init: init,
    trackEvent: trackEvent,
    trackPageView: trackPageView,
  };

  // Auto-init when DOM is ready
  if (document.readyState === 'loading') {
    document.addEventListener('DOMContentLoaded', init);
  } else {
    init();
  }
})();
