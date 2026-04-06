# PHP Legacy to Python Flask/FastAPI

## Overview
- **Category:** Code Modernization
- **Priority:** P3
- **Languages:** PHP 5.x → Python 3.12 (Flask/FastAPI)
- **Repository Name:** appmodlab-php-legacy-to-python-flask-fastapi
- **Organization:** EmeaAppGbb

## Objective
This lab guides participants through modernizing a legacy PHP application (procedural PHP 5.x with MySQL) to a modern Python web application using Flask or FastAPI. It covers the paradigm shift from PHP's request-per-process model to Python's ASGI/WSGI patterns, MySQL-to-PostgreSQL migration, and adoption of Python best practices including type hints, Pydantic models, and async database access. This scenario is common in organizations consolidating their tech stack around Python for both web and data science workloads.

## Demo Legacy Application
**Business Domain:** Community event management and ticketing platform for "CityPulse Events"

The legacy PHP application manages event listings, venue management, ticket sales, attendee registration, and event organizer dashboards. It was built with procedural PHP, no framework, and has grown organically over 10 years.

### Tech Stack
- PHP 5.6 (procedural, no framework, no Composer)
- MySQL 5.7 with mysqli extension (some mysql_ deprecated functions)
- Apache with mod_php
- jQuery 1.x for frontend interactions
- PHPMailer for email (outdated version)
- PayPal IPN for payment processing
- File-based sessions
- .htaccess-based routing (URL rewriting)
- PHP include/require for code reuse

### Key Files/Folders Structure
```
citypulse/
├── index.php                      # Homepage with event listing
├── config.php                     # Database credentials (hardcoded)
├── includes/
│   ├── db.php                     # MySQL connection (mysqli_connect)
│   ├── header.php                 # HTML header template
│   ├── footer.php                 # HTML footer
│   ├── functions.php              # 2000-line utility functions file
│   └── auth.php                   # Session-based auth functions
├── events/
│   ├── list.php                   # Event listing with pagination
│   ├── detail.php                 # Event detail (uses $_GET['id'])
│   ├── create.php                 # Create event form + processing
│   ├── edit.php                   # Edit event
│   └── search.php                 # Search with raw SQL LIKE queries
├── tickets/
│   ├── purchase.php               # Ticket purchase flow
│   ├── checkout.php               # PayPal integration
│   ├── confirm.php                # PayPal IPN callback handler
│   └── my-tickets.php             # User's purchased tickets
├── organizers/
│   ├── dashboard.php              # Event organizer dashboard
│   ├── reports.php                # Sales reports (HTML tables)
│   └── settings.php               # Organizer profile settings
├── admin/
│   ├── login.php                  # Admin login
│   ├── events.php                 # Event moderation
│   └── users.php                  # User management
├── uploads/                       # Event images (uploaded via move_uploaded_file)
├── css/                           # Stylesheets
├── js/                            # jQuery scripts
└── .htaccess                      # Apache rewrite rules
```

### Database Schema (MySQL)
- **events** (id, title, description, venue_id, organizer_id, event_date, start_time, end_time, category, max_capacity, price, status, image_path, created_at)
- **venues** (id, name, address, city, capacity, amenities, contact_email, latitude, longitude)
- **tickets** (id, event_id, user_id, ticket_type, price, purchase_date, payment_status, paypal_txn_id, qr_code)
- **users** (id, username, email, password, role, name, phone, created_at) — MD5 hashed passwords
- **organizers** (id, user_id, company_name, description, website, verified, commission_rate)
- **categories** (id, name, slug, icon)
- **reviews** (id, event_id, user_id, rating, comment, created_at)

### Legacy Anti-Patterns Present
- SQL injection everywhere (string interpolation in queries)
- MD5 password hashing (completely insecure)
- Global functions file with 2000+ lines and no namespacing
- register_globals-style $_GET/$_POST access without sanitization
- File-based sessions with no secure cookie settings
- PayPal IPN integration with no signature verification
- move_uploaded_file with no file type or size validation
- Mixed HTML and PHP in every file (no template engine)
- Error display enabled in production (display_errors = On)
- No dependency management (no Composer, libraries copied into project)

## Target Architecture
- **Framework:** Python 3.12 with FastAPI (primary) + Flask alternative
- **ORM:** SQLAlchemy 2.0 with async support (SQLModel for FastAPI)
- **Database:** Azure Database for PostgreSQL (replacing MySQL)
- **Authentication:** OAuth2 with JWT tokens (FastAPI Security)
- **Password Hashing:** bcrypt via passlib
- **Payment:** Stripe API (replacing PayPal IPN)
- **Email:** Azure Communication Services
- **File Storage:** Azure Blob Storage
- **Template Engine:** Jinja2 (for server-rendered pages) or React SPA
- **Hosting:** Azure Container Apps
- **API Documentation:** Auto-generated OpenAPI/Swagger (FastAPI native)

### Architecture Description
The procedural PHP is restructured into a FastAPI application with proper layered architecture: API routes, service layer, repository layer, and Pydantic models. SQLAlchemy 2.0 with async sessions replaces raw MySQL queries. FastAPI's built-in dependency injection provides clean service composition. JWT authentication replaces PHP sessions. Stripe replaces PayPal IPN. Azure Blob Storage replaces filesystem uploads. The FastAPI app auto-generates OpenAPI documentation. A Flask alternative branch demonstrates the same migration using Flask + Flask-SQLAlchemy.

## Branch Structure
- `main` — Contains the completed lab with full APPMODLAB.md documentation
- `legacy` — The PHP application (runnable via Docker with Apache + PHP 5.6 + MySQL)
- `solution` — The FastAPI application (primary)
- `solution-flask` — The Flask alternative
- `step-1-database-migration` — MySQL to PostgreSQL schema and data migration
- `step-2-api-layer` — FastAPI routes and Pydantic models
- `step-3-auth-and-security` — JWT auth, bcrypt passwords, input validation
- `step-4-payment-and-services` — Stripe integration, Azure services
- `step-5-deploy` — Docker + Azure Container Apps

## Lab Content Requirements (APPMODLAB.md)
### Learning Objectives
- Analyze a legacy PHP codebase and plan a Python migration strategy
- Translate procedural PHP patterns to Python OOP with FastAPI/Flask
- Migrate MySQL databases to PostgreSQL with SQLAlchemy
- Implement proper authentication and security (replacing MD5 + sessions)
- Build auto-documented REST APIs with FastAPI

### Prerequisites
- Basic PHP reading knowledge
- Python 3.x experience
- Docker Desktop
- .NET is not required — this is a Python lab
- Azure subscription
- Basic SQL knowledge

### Step-by-Step Instructions Outline
1. **Run the Legacy App** — Docker Compose with PHP 5.6 + MySQL, explore the event platform
2. **Migrate Database** — Convert MySQL schema to PostgreSQL, migrate data
3. **Create FastAPI Project** — Set up project structure with Poetry, configure SQLAlchemy
4. **Build API Routes** — Implement event CRUD, ticket purchase, and user management endpoints
5. **Add Authentication** — JWT tokens with OAuth2 password flow, bcrypt password migration
6. **Integrate Stripe** — Replace PayPal IPN with Stripe Checkout Sessions
7. **Add Azure Services** — Blob Storage for images, Communication Services for email
8. **Deploy** — Containerize with Docker, deploy to Azure Container Apps

### Estimated Duration
4–6 hours

### Key Concepts Covered
- PHP to Python migration patterns
- Procedural to OOP restructuring
- MySQL to PostgreSQL migration
- FastAPI and SQLAlchemy modern patterns
- API-first development with OpenAPI

## What the Squad Needs to Build
1. **Legacy App Setup:** Build a procedural PHP 5.6 application with all described anti-patterns. Provide Docker Compose with Apache, PHP 5.6, and MySQL 5.7. Include seed data with 100+ events, 20 venues, sample ticket purchases.
2. **Modernization Implementation:** Build FastAPI application with SQLAlchemy 2.0 async, Pydantic models, JWT auth, Stripe integration, and proper project structure. Also build a Flask alternative branch. Both must pass the same test suite.
3. **Lab Documentation:** APPMODLAB.md with PHP-to-Python translation guide, security remediation checklist, and framework comparison (FastAPI vs Flask) to help participants choose.
4. **Infrastructure as Code:** Bicep templates for Azure Container Apps, Azure Database for PostgreSQL, Azure Blob Storage, and Azure Communication Services.
5. **CI/CD:** GitHub Actions with pytest, Docker build, and Azure Container Apps deployment.

## Acceptance Criteria
- [ ] Legacy PHP app runs in Docker with all features functional
- [ ] Event listing, ticket purchase, and organizer dashboard work in legacy
- [ ] MySQL to PostgreSQL migration scripts work correctly
- [ ] FastAPI app implements all features with auto-generated OpenAPI docs
- [ ] Flask alternative implements all features
- [ ] JWT authentication replaces PHP sessions
- [ ] Stripe integration replaces PayPal IPN
- [ ] All SQL injection vulnerabilities are remediated
- [ ] APPMODLAB.md follows the template format with complete frontmatter
- [ ] Step-by-step guide is clear and reproducible
- [ ] All code compiles and runs without errors
