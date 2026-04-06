# Spec2Cloud Monolith to Microservices .NET

## Overview
- **Category:** Code Modernization
- **Priority:** P2
- **Languages:** C#/.NET
- **Repository Name:** appmodlab-spec2cloud-monolith-to-microservices-dotnet
- **Organization:** EmeaAppGbb

## Objective
This lab teaches participants how to use Spec2Cloud analysis and refactoring to decompose a monolithic .NET application into microservices. Spec2Cloud identifies bounded contexts, maps data ownership, and generates service boundary specifications that guide the decomposition. Participants learn to analyze coupling, define service contracts, and incrementally extract services — all driven by Spec2Cloud-generated specifications rather than gut instinct.

## Demo Legacy Application
**Business Domain:** Online food ordering and restaurant management platform for "UrbanBites" — a regional food delivery marketplace

The legacy application is a monolithic ASP.NET Core 3.1 application (already on .NET Core but not decomposed) that handles restaurant listings, menu management, order placement, payment processing, delivery tracking, and customer reviews — all in a single deployable unit.

### Tech Stack
- ASP.NET Core 3.1 MVC + Web API (single project)
- Entity Framework Core 3.1 with a single massive DbContext (30+ entities)
- SQL Server with one database for everything
- SignalR for real-time delivery tracking
- Razor views for restaurant admin portal
- REST API for mobile apps
- Hangfire for background job processing (order timeout, delivery assignment)
- SendGrid for email notifications (direct SDK calls)
- Stripe SDK for payment processing (tightly coupled)

### Key Files/Folders Structure
```
UrbanBites/
├── UrbanBites.sln
├── UrbanBites.Web/
│   ├── Program.cs / Startup.cs
│   ├── Controllers/
│   │   ├── RestaurantController.cs       # Restaurant CRUD + menu management
│   │   ├── OrderController.cs            # Order placement + status
│   │   ├── PaymentController.cs          # Stripe payment processing
│   │   ├── DeliveryController.cs         # Delivery assignment + tracking
│   │   ├── ReviewController.cs           # Customer reviews + ratings
│   │   ├── CustomerController.cs         # Customer profiles
│   │   └── AdminController.cs            # Platform admin
│   ├── Services/
│   │   ├── OrderService.cs               # 800-line class handling all order logic
│   │   ├── PaymentService.cs             # Stripe integration
│   │   ├── DeliveryService.cs            # Driver assignment algorithm
│   │   ├── NotificationService.cs        # Email + push notifications
│   │   ├── PricingService.cs             # Dynamic pricing + promotions
│   │   └── ReviewService.cs              # Review moderation
│   ├── Data/
│   │   ├── UrbanBitesDbContext.cs        # Single DbContext with 30+ DbSets
│   │   └── Migrations/                    # 60+ migrations
│   ├── Models/                            # Shared entity models
│   ├── Hubs/
│   │   └── DeliveryHub.cs                # SignalR real-time tracking
│   └── Jobs/
│       ├── OrderTimeoutJob.cs            # Hangfire recurring jobs
│       └── DeliveryAssignmentJob.cs
└── UrbanBites.Tests/                      # Integration tests against full DB
```

### Database Schema (Single Database, 30+ Tables)
- **Restaurants** (Id, Name, Address, Cuisine, Rating, IsActive, OwnerId, CommissionRate)
- **MenuItems** (Id, RestaurantId, Name, Price, Category, IsAvailable, PrepTime)
- **Orders** (Id, CustomerId, RestaurantId, Status, TotalAmount, PlacedAt, DeliveryAddress)
- **OrderItems** (Id, OrderId, MenuItemId, Quantity, Price, SpecialInstructions)
- **Payments** (Id, OrderId, StripePaymentIntentId, Amount, Status, ProcessedAt)
- **Deliveries** (Id, OrderId, DriverId, Status, PickupTime, DeliveryTime, CurrentLocation)
- **Drivers** (Id, Name, VehicleType, IsAvailable, CurrentLocation, Rating)
- **Customers** (Id, Name, Email, Phone, DefaultAddress, LoyaltyPoints)
- **Reviews** (Id, OrderId, CustomerId, RestaurantId, Rating, Comment, IsModerated)
- **Promotions** (Id, Code, DiscountType, Value, ValidFrom, ValidTo, RestaurantId)
- Plus 20 more supporting tables...

### Legacy Anti-Patterns Present
- God DbContext with 30+ DbSets and cross-cutting navigation properties
- 800-line OrderService class doing order placement, validation, pricing, and notification
- Direct Stripe SDK calls in the controller layer
- Shared entity models used across all features (tight coupling)
- Single database for everything — no data ownership boundaries
- Hangfire jobs directly querying the same DbContext
- SignalR hub accessing database directly
- No domain events — services call each other synchronously
- Database transactions spanning multiple aggregate roots
- Integration tests require the full database with all 30+ tables seeded

## Target Architecture
- **Framework:** .NET 9 with ASP.NET Core
- **Architecture:** Microservices with API Gateway
- **Services:**
  - Restaurant Service (menu management, restaurant profiles)
  - Order Service (order lifecycle, saga orchestrator)
  - Payment Service (Stripe integration, payment processing)
  - Delivery Service (driver assignment, real-time tracking)
  - Customer Service (profiles, loyalty, reviews)
  - Notification Service (email, push, SMS)
- **Communication:** Azure Service Bus for async events, gRPC for synchronous calls
- **Data:** Each service owns its own Azure SQL Database
- **API Gateway:** Azure API Management or YARP reverse proxy
- **Container Orchestration:** Azure Container Apps
- **Observability:** Azure Monitor, Application Insights, distributed tracing

### Architecture Description
Spec2Cloud analyzes the monolith and identifies six bounded contexts by mapping entity relationships, service dependencies, and transaction boundaries. Each bounded context becomes an independent microservice with its own database. The OrderService saga orchestrates cross-service workflows using Azure Service Bus. Shared entities are split into service-specific models with anti-corruption layers. The SignalR hub is moved into the Delivery Service. Hangfire jobs become service-internal background workers.

## Branch Structure
- `main` — Contains the completed lab with full APPMODLAB.md documentation
- `legacy` — The monolithic ASP.NET Core 3.1 application
- `solution` — The fully decomposed microservices solution
- `step-1-spec-analysis` — Spec2Cloud analysis output showing bounded contexts and coupling maps
- `step-2-strangler-fig` — API Gateway + first service extraction (Restaurant Service)
- `step-3-order-extraction` — Order Service with saga pattern
- `step-4-payment-delivery` — Payment and Delivery services with event-driven communication
- `step-5-full-decomposition` — All services running independently
- `step-6-observability-deploy` — Distributed tracing, health checks, Azure deployment

## Lab Content Requirements (APPMODLAB.md)
### Learning Objectives
- Use Spec2Cloud to identify bounded contexts and service boundaries in a monolith
- Apply the Strangler Fig pattern to incrementally extract services
- Implement saga orchestration for cross-service workflows
- Split a shared database into service-owned databases with data synchronization
- Set up distributed tracing and observability for microservices

### Prerequisites
- Strong C# and ASP.NET Core experience
- Understanding of microservices concepts (bounded contexts, sagas)
- .NET 9 SDK and Docker Desktop
- Azure subscription with Contributor access
- Basic familiarity with messaging systems

### Step-by-Step Instructions Outline
1. **Run the Monolith** — Explore the application, place an order, observe the full flow
2. **Run Spec2Cloud Analysis** — Generate bounded context maps, coupling analysis, and service specifications
3. **Review Service Boundaries** — Examine Spec2Cloud's decomposition recommendations
4. **Set Up API Gateway** — Implement YARP reverse proxy as the Strangler Fig entry point
5. **Extract Restaurant Service** — First service extraction with its own database
6. **Extract Order Service** — Implement saga orchestrator for order workflow
7. **Extract Payment & Delivery** — Event-driven services with Azure Service Bus
8. **Extract Customer Service** — Profile and review management
9. **Add Observability** — Distributed tracing with Application Insights
10. **Deploy to Azure Container Apps** — Full microservices deployment

### Estimated Duration
8–10 hours

### Key Concepts Covered
- Monolith decomposition strategies
- Spec2Cloud bounded context identification
- Strangler Fig migration pattern
- Saga orchestration
- Database-per-service pattern

## What the Squad Needs to Build
1. **Legacy App Setup:** Build a monolithic ASP.NET Core 3.1 food delivery platform with all described features — restaurant management, ordering, payment (Stripe test mode), delivery tracking (SignalR), and reviews. Single database with 30+ tables and seed data. Include Hangfire dashboard.
2. **Modernization Implementation:** Decompose into six .NET 9 microservices, each with its own database and API. Implement Azure Service Bus for events, saga orchestrator for orders, gRPC for synchronous calls. Include Docker Compose for local development.
3. **Lab Documentation:** APPMODLAB.md showing the Spec2Cloud analysis process, bounded context diagrams, coupling matrices, and step-by-step decomposition guide. Include before/after architecture diagrams.
4. **Infrastructure as Code:** Bicep templates for Azure Container Apps environment, multiple Azure SQL databases, Azure Service Bus, Azure API Management, and Application Insights.
5. **CI/CD:** GitHub Actions workflows for building and deploying all six services independently.

## Acceptance Criteria
- [ ] Monolithic app runs all features in a single deployment
- [ ] Spec2Cloud analysis identifies at least six bounded contexts
- [ ] Each microservice has its own database with proper data ownership
- [ ] Order workflow completes across services via saga orchestration
- [ ] Azure Service Bus events flow correctly between services
- [ ] Real-time delivery tracking works via SignalR in the Delivery Service
- [ ] All services deploy independently to Azure Container Apps
- [ ] APPMODLAB.md follows the template format with complete frontmatter
- [ ] Step-by-step guide is clear and reproducible
- [ ] All code compiles and runs without errors
