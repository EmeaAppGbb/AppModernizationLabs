# AngularJS to React/Angular Modern

## Overview
- **Category:** Code Modernization
- **Priority:** P3
- **Languages:** JavaScript (AngularJS 1.x) → TypeScript (React 18 / Angular 17)
- **Repository Name:** appmodlab-angularjs-to-react-angular-modern
- **Organization:** EmeaAppGbb

## Objective
This lab demonstrates modernizing a legacy AngularJS 1.x single-page application to either React 18 or Angular 17 with TypeScript. AngularJS reached end of life in January 2022 and is no longer receiving security patches, making migration urgent for enterprises. The lab covers component architecture migration, state management modernization, and the decision framework for choosing between React and Angular as the target.

## Demo Legacy Application
**Business Domain:** Corporate travel booking and expense reconciliation portal for "GlobalTravel Corp"

The legacy AngularJS application allows employees to search flights, book hotels, manage itineraries, submit travel requests for approval, and reconcile travel expenses. It interfaces with a REST API backend.

### Tech Stack
- AngularJS 1.6.x (with UI-Router for routing)
- Bower for package management
- Grunt for build tooling
- jQuery 2.x (mixed usage alongside AngularJS)
- Bootstrap 3 with angular-ui-bootstrap
- Restangular for API calls
- Lodash for utility functions
- Moment.js for date manipulation
- Karma + Jasmine for testing
- No TypeScript — pure JavaScript with JSDoc comments

### Key Files/Folders Structure
```
globaltravel-portal/
├── bower.json                         # Bower dependencies
├── Gruntfile.js                       # Grunt build config
├── app/
│   ├── app.js                         # Main module definition and config
│   ├── app.routes.js                  # UI-Router state definitions
│   ├── components/
│   │   ├── flight-search/
│   │   │   ├── flight-search.controller.js
│   │   │   ├── flight-search.template.html
│   │   │   └── flight-search.service.js
│   │   ├── hotel-booking/
│   │   │   ├── hotel-booking.controller.js
│   │   │   ├── hotel-booking.template.html
│   │   │   └── hotel-booking.service.js
│   │   ├── itinerary/
│   │   │   ├── itinerary.controller.js
│   │   │   ├── itinerary.template.html
│   │   │   └── itinerary.service.js
│   │   ├── travel-request/
│   │   │   ├── travel-request.controller.js
│   │   │   ├── travel-request.template.html
│   │   │   └── travel-request.service.js
│   │   └── expense-reconciliation/
│   │       ├── expense.controller.js
│   │       ├── expense.template.html
│   │       └── expense.service.js
│   ├── directives/
│   │   ├── date-picker.directive.js   # Custom date picker wrapping jQuery plugin
│   │   ├── currency-input.directive.js
│   │   └── approval-status.directive.js
│   ├── filters/
│   │   ├── currency.filter.js
│   │   └── date-format.filter.js
│   ├── services/
│   │   ├── auth.service.js            # $http-based auth with localStorage token
│   │   ├── api.service.js             # Restangular configuration
│   │   └── user.service.js            # User profile management
│   └── assets/
│       ├── css/                       # Custom CSS + Bootstrap overrides
│       └── images/
├── test/
│   └── spec/                          # Karma/Jasmine unit tests
└── api-mock/
    └── server.js                      # Express mock API server
```

### Backend API (Mock Server Included)
The lab includes an Express.js mock API that simulates:
- `/api/flights` — Flight search and booking
- `/api/hotels` — Hotel search and reservation
- `/api/itineraries` — Itinerary CRUD
- `/api/travel-requests` — Approval workflow
- `/api/expenses` — Expense submission and reconciliation
- `/api/auth` — Login/logout with JWT

### Legacy Anti-Patterns Present
- AngularJS $scope-based data binding (not controllerAs syntax everywhere)
- $rootScope events for cross-component communication ($broadcast/$on)
- Bower for package management (deprecated, unmaintained)
- Grunt build pipeline (complex, slow, hard to maintain)
- jQuery plugins wrapped in AngularJS directives
- Restangular wrapping $http (extra abstraction layer)
- Moment.js for all date handling (large bundle size)
- Lodash used for simple operations available natively
- $watch and $watchCollection for change detection
- Two-way data binding causing performance issues on large lists
- No TypeScript — dynamic typing causing runtime errors
- Mixed jQuery and AngularJS DOM manipulation

## Target Architecture
- **Option A (React):** React 18 + TypeScript + Vite + TanStack Router + TanStack Query + Zustand
- **Option B (Angular):** Angular 17 + TypeScript + Angular Router + NgRx + Angular Material
- **Build:** Vite (React) or Angular CLI (Angular)
- **Package Management:** npm/pnpm
- **Testing:** Vitest + Testing Library (React) or Jasmine + TestBed (Angular)
- **Date Handling:** date-fns (replacing Moment.js)
- **API Client:** Axios or fetch with TanStack Query (React) / HttpClient (Angular)
- **Hosting:** Azure Static Web Apps
- **Backend:** Same Express API upgraded to TypeScript, or Azure Functions

### Architecture Description
The AngularJS controller+template pattern is migrated to React functional components with hooks (or Angular standalone components). $scope is replaced with React state/Zustand (or Angular signals/NgRx). Bower+Grunt is replaced with npm+Vite (or Angular CLI). jQuery directives become native React/Angular components. Restangular becomes Axios with TanStack Query (or Angular HttpClient). The app is deployed as a static site on Azure Static Web Apps with the API on Azure Functions.

## Branch Structure
- `main` — Contains the completed lab with full APPMODLAB.md documentation
- `legacy` — The AngularJS 1.6 application with Bower + Grunt
- `solution` — The React 18 + TypeScript application (primary)
- `solution-angular` — The Angular 17 + TypeScript alternative
- `step-1-project-setup` — Vite/Angular CLI project scaffold with TypeScript
- `step-2-component-migration` — AngularJS controllers to React/Angular components
- `step-3-state-management` — $scope/$rootScope to Zustand/NgRx
- `step-4-routing-and-api` — UI-Router to modern router, Restangular to fetch/HttpClient
- `step-5-testing-and-deploy` — Modern testing setup + Azure Static Web Apps

## Lab Content Requirements (APPMODLAB.md)
### Learning Objectives
- Assess an AngularJS application for migration scope and complexity
- Migrate AngularJS controllers and templates to modern React or Angular components
- Replace $scope/$rootScope with modern state management (hooks, signals, stores)
- Modernize the build pipeline from Bower+Grunt to npm+Vite or Angular CLI
- Deploy a modern SPA to Azure Static Web Apps

### Prerequisites
- JavaScript and AngularJS 1.x familiarity
- TypeScript basics
- Node.js 20+ installed
- npm or pnpm
- Azure subscription (for deployment)

### Step-by-Step Instructions Outline
1. **Run the Legacy App** — Install Bower dependencies, run Grunt, explore the travel portal
2. **Analyze Migration Scope** — Inventory controllers, directives, services, and filters
3. **Scaffold Modern Project** — Create React (Vite) or Angular (CLI) project with TypeScript
4. **Migrate Components** — Convert AngularJS controllers to React/Angular components one by one
5. **Migrate State Management** — Replace $scope with hooks/signals, $rootScope with global store
6. **Migrate Routing** — Convert UI-Router states to TanStack Router/Angular Router
7. **Replace Dependencies** — Remove jQuery, Moment.js, Lodash; use modern alternatives
8. **Set Up Testing** — Write component tests with Testing Library or Angular TestBed
9. **Deploy** — Build and deploy to Azure Static Web Apps

### Estimated Duration
5–7 hours

### Key Concepts Covered
- AngularJS to modern framework migration strategy
- Component architecture patterns
- Modern state management
- Build tooling modernization
- TypeScript adoption benefits

## What the Squad Needs to Build
1. **Legacy App Setup:** Build an AngularJS 1.6 application with Bower, Grunt, and all described patterns. Include a working Express mock API. The app must demonstrate flight search, hotel booking, itinerary management, and expense reconciliation.
2. **Modernization Implementation:** Build both React 18 and Angular 17 versions with TypeScript. Both must work with the same mock API. Include proper state management, routing, and testing.
3. **Lab Documentation:** APPMODLAB.md with AngularJS-to-React/Angular migration mapping, framework decision guide, and before/after code comparisons.
4. **Infrastructure as Code:** Bicep templates for Azure Static Web Apps and Azure Functions (for API).
5. **CI/CD:** GitHub Actions for build, test, and deploy for both React and Angular versions.

## Acceptance Criteria
- [ ] AngularJS app runs with Bower+Grunt and all features work
- [ ] Mock API serves all required endpoints
- [ ] React version implements all features with TypeScript
- [ ] Angular version implements all features with TypeScript
- [ ] No AngularJS, Bower, Grunt, jQuery, or Moment.js in solution branches
- [ ] Modern test suites pass for both React and Angular
- [ ] Both versions deploy to Azure Static Web Apps
- [ ] APPMODLAB.md follows the template format with complete frontmatter
- [ ] Step-by-step guide is clear and reproducible
- [ ] All code compiles and runs without errors
