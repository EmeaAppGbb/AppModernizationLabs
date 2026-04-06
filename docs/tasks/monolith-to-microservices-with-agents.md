# From Monolith to Microservices with Agents

## Overview
- **Category:** Cross-Cutting / End-to-End
- **Priority:** P2
- **Languages:** Java/C#/.NET
- **Repository Name:** appmodlab-monolith-to-microservices-with-agents
- **Organization:** EmeaAppGbb

## Objective
This lab demonstrates using SQUAD and Spec2Cloud together to decompose a monolithic Java application into microservices. Spec2Cloud identifies bounded contexts and service boundaries, SQUAD Brain plans the decomposition strategy, and SQUAD agents execute the migration incrementally using the Strangler Fig pattern. This showcases the agents' ability to handle one of the most complex modernization patterns — breaking apart a tightly coupled monolith.

## Demo Legacy Application
**Business Domain:** Online education and course delivery platform for "EduVerse Academy"

The legacy application is a monolithic Java Spring MVC application that handles everything: course creation, student enrollment, video streaming management, quiz/assessment engine, certificate generation, payment processing, and instructor dashboards. All in a single WAR file deployed on Tomcat.

### Tech Stack
- Java 11 with Spring MVC (not Spring Boot — traditional WAR deployment)
- Tomcat 9 as external servlet container
- JPA/Hibernate with single PostgreSQL database
- JSP pages for server-side rendering
- Apache Tiles for layout templating
- Spring Security with form-based auth
- Quartz Scheduler for batch jobs
- Apache Commons Email for notifications
- Single database with 100+ tables

### Key Files/Folders Structure
```
eduverse-academy/
├── pom.xml                              # Maven WAR project
├── src/main/java/com/eduverse/
│   ├── config/
│   │   ├── AppConfig.java               # Spring Java config (not Boot)
│   │   ├── SecurityConfig.java          # Spring Security form auth
│   │   └── PersistenceConfig.java       # JPA/Hibernate config
│   ├── controller/
│   │   ├── CourseController.java        # Course CRUD + publishing
│   │   ├── EnrollmentController.java    # Student enrollment flow
│   │   ├── AssessmentController.java    # Quiz creation and grading
│   │   ├── VideoController.java         # Video management (upload, stream)
│   │   ├── CertificateController.java   # Certificate generation
│   │   ├── PaymentController.java       # Payment processing
│   │   ├── InstructorController.java    # Instructor dashboard
│   │   └── AdminController.java         # Platform admin
│   ├── service/
│   │   ├── CourseService.java           # Course business logic
│   │   ├── EnrollmentService.java       # Enrollment + prerequisites
│   │   ├── AssessmentService.java       # Grading algorithms
│   │   ├── VideoService.java            # Video processing + transcoding
│   │   ├── CertificateService.java      # PDF certificate generation
│   │   ├── PaymentService.java          # Stripe integration
│   │   ├── ProgressService.java         # Student progress tracking
│   │   └── NotificationService.java     # Email notifications
│   ├── model/                           # 50+ JPA entities
│   ├── repository/                      # Spring Data JPA repositories
│   └── scheduler/
│       ├── EnrollmentReminderJob.java   # Quartz scheduled jobs
│       └── ReportGenerationJob.java
├── src/main/webapp/
│   ├── WEB-INF/
│   │   ├── web.xml                      # Servlet config
│   │   ├── tiles.xml                    # Apache Tiles layout defs
│   │   └── views/                       # JSP pages (30+)
│   └── resources/                       # CSS, JS, images
└── src/test/java/                       # JUnit 4 tests
```

### Database Schema (Single DB, 100+ Tables)
- **courses** (id, title, description, instructor_id, category, price, status, published_date, duration_hours)
- **modules** (id, course_id, title, sort_order, description)
- **lessons** (id, module_id, title, content_type, video_url, duration_minutes, sort_order)
- **enrollments** (id, student_id, course_id, enrolled_date, progress_percent, status, completion_date)
- **assessments** (id, lesson_id, type, title, passing_score, time_limit_minutes)
- **student_answers** (id, assessment_id, student_id, answers_json, score, submitted_date)
- **certificates** (id, enrollment_id, certificate_number, issued_date, template_id)
- **payments** (id, enrollment_id, amount, currency, stripe_payment_id, status, paid_date)
- **videos** (id, lesson_id, original_url, transcoded_urls, duration, status)
- Plus 90+ more tables for progress tracking, notifications, instructor analytics, admin settings...

### Monolith Anti-Patterns
- All 100+ tables in a single database with foreign keys across all domains
- Service classes with cross-domain dependencies (EnrollmentService calls PaymentService calls NotificationService)
- Shared entity models used across all features
- Single transaction spanning enrollment + payment + notification
- JSP views mixing presentation for all features
- Quartz jobs accessing all services directly
- No domain boundaries — any service can query any table
- Single deployment means all features scale together

## Target Architecture
- **Architecture:** Microservices with API Gateway
- **Services (Identified by Spec2Cloud):**
  - Course Catalog Service (Spring Boot 3.x)
  - Enrollment Service (Spring Boot 3.x)
  - Assessment Service (Spring Boot 3.x)
  - Video Service (Spring Boot 3.x + Azure Media Services)
  - Certificate Service (Spring Boot 3.x)
  - Payment Service (Spring Boot 3.x)
  - Notification Service (Spring Boot 3.x)
  - Student Progress Service (Spring Boot 3.x)
- **Communication:** Azure Service Bus for events, gRPC for sync calls
- **API Gateway:** Spring Cloud Gateway or Azure API Management
- **Migration Pattern:** Strangler Fig with incremental service extraction
- **Hosting:** Azure Container Apps

### Architecture Description
Spec2Cloud analyzes the monolith and identifies 8 bounded contexts based on entity ownership, service coupling, and transaction boundaries. SQUAD Brain creates an incremental decomposition plan using the Strangler Fig pattern — starting with the least coupled service (Notification) and ending with the most coupled (Enrollment). Each extraction step is planned, implemented, reviewed, and documented by SQUAD agents. The API Gateway routes traffic between the monolith and extracted services during the migration.

## Branch Structure
- `main` — Contains the completed lab with full APPMODLAB.md documentation
- `legacy` — The monolithic Spring MVC application
- `specs` — Spec2Cloud bounded context analysis and decomposition plan
- `solution` — Fully decomposed microservices
- `step-1-spec2cloud-analysis` — Bounded context identification and coupling analysis
- `step-2-strangler-setup` — API Gateway + first service extraction (Notification)
- `step-3-course-catalog` — Course Catalog Service extraction
- `step-4-payment-service` — Payment Service extraction with saga
- `step-5-full-decomposition` — All services running independently

## Lab Content Requirements (APPMODLAB.md)
### Learning Objectives
- Use Spec2Cloud to identify bounded contexts in a Java monolith
- Plan microservices decomposition using the Strangler Fig pattern
- Coordinate SQUAD agents for incremental service extraction
- Implement saga patterns for cross-service transactions
- Deploy microservices to Azure Container Apps

### Prerequisites
- Strong Java and Spring experience
- Understanding of microservices patterns
- Completed Spec2Cloud and SQUAD introductory labs
- Docker Desktop and JDK 21
- Azure subscription

### Step-by-Step Instructions Outline
1. **Run the Monolith** — Deploy on Tomcat, explore all platform features
2. **Spec2Cloud Analysis** — Identify bounded contexts, map coupling and data ownership
3. **SQUAD Planning** — Brain creates decomposition plan from Spec2Cloud analysis
4. **Set Up Strangler Fig** — Deploy API Gateway alongside monolith
5. **Extract Notification Service** — SQUAD extracts least-coupled service first
6. **Extract Course Catalog** — SQUAD extracts course management with own database
7. **Extract Payment Service** — SQUAD implements saga for enrollment-payment flow
8. **Continue Extraction** — Remaining services extracted incrementally
9. **Final Validation** — All features work across microservices
10. **Deploy to Azure** — Container Apps deployment with all services

### Estimated Duration
8–10 hours (full-day workshop)

### Key Concepts Covered
- Bounded context identification
- Strangler Fig migration pattern
- Saga orchestration for distributed transactions
- Database-per-service decomposition
- Agentic microservices decomposition

## What the Squad Needs to Build
1. **Legacy App Setup:** Build a substantial Java Spring MVC monolith with course delivery, enrollment, assessments, video management, certificates, payments, and notifications. Deploy on Tomcat with PostgreSQL. 100+ tables with seed data including courses, students, and enrollments.
2. **Modernization Implementation:** 8 Spring Boot 3.x microservices extracted using Strangler Fig pattern. Each with its own database, tests, and Docker image. API Gateway routing, Service Bus events, saga orchestrator for enrollment-payment flow.
3. **Lab Documentation:** APPMODLAB.md as a comprehensive workshop guide showing the Spec2Cloud analysis → SQUAD decomposition pipeline. Include bounded context diagrams, coupling matrices, and step-by-step extraction guides.
4. **Infrastructure as Code:** Bicep templates for Azure Container Apps environment, multiple Azure SQL databases, Azure Service Bus, API Management, and Container Registry.
5. **CI/CD:** GitHub Actions for building and deploying all 8 microservices independently.

## Acceptance Criteria
- [ ] Monolith runs on Tomcat with all e-learning features functional
- [ ] Spec2Cloud identifies 8 bounded contexts with coupling analysis
- [ ] SQUAD creates and executes decomposition plan
- [ ] Strangler Fig pattern allows incremental migration
- [ ] Each microservice has its own database with proper data ownership
- [ ] Enrollment-payment flow works via saga pattern
- [ ] All microservices deploy independently to Azure Container Apps
- [ ] APPMODLAB.md follows the template format with complete frontmatter
- [ ] Step-by-step guide is clear and reproducible
- [ ] All code compiles and runs without errors
