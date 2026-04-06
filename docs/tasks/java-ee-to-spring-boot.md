# Java EE to Spring Boot Modernization

## Overview
- **Category:** Code Modernization
- **Priority:** P1
- **Languages:** Java EE 7 / Jakarta EE 8 → Java 21 / Spring Boot 3.x
- **Repository Name:** appmodlab-java-ee-to-spring-boot
- **Organization:** EmeaAppGbb

## Objective
This lab guides participants through migrating a Java EE application deployed on a traditional application server (WildFly/JBoss) to Spring Boot 3.x with modern cloud-native patterns. It covers migration of EJBs, JPA, JSF, JMS, and JAAS to their Spring equivalents, demonstrating how to shed the application server dependency and embrace embedded, self-contained microservices running on Azure.

## Demo Legacy Application
**Business Domain:** Wholesale pharmaceutical distribution and inventory management for "MedFlow Distributors"

The legacy application manages pharmaceutical product catalogs, warehouse inventory, customer orders (hospitals and pharmacies), lot tracking for regulatory compliance, and supplier purchase orders. It runs on WildFly 26 application server.

### Tech Stack
- Java EE 7 on WildFly 26 (JBoss EAP compatible)
- EJB 3.2 (Stateless Session Beans for business logic, Singleton for caching)
- JPA 2.1 with Hibernate 5.x (persistence.xml configuration)
- JSF 2.3 with PrimeFaces 12 for UI
- JMS 2.0 with ActiveMQ for async order processing
- JAAS for authentication with LDAP realm
- CDI (Contexts and Dependency Injection) 1.2
- Bean Validation 1.1 (Hibernate Validator)
- JAX-RS 2.1 for REST endpoints
- JDBC DataSource configured in standalone.xml
- PostgreSQL 14 database

### Key Files/Folders Structure
```
medflow-distributor/
├── pom.xml                              # Maven with Java EE BOM
├── src/main/java/com/medflow/
│   ├── ejb/
│   │   ├── ProductCatalogBean.java      # @Stateless EJB for product operations
│   │   ├── InventoryBean.java           # @Stateless EJB for stock management
│   │   ├── OrderProcessingBean.java     # @Stateless EJB with JMS integration
│   │   ├── PurchaseOrderBean.java       # @Stateless EJB for supplier orders
│   │   └── CacheManagerBean.java        # @Singleton EJB for product cache
│   ├── entity/
│   │   ├── Product.java                 # JPA entity with @NamedQueries
│   │   ├── Inventory.java               # Stock levels with lot tracking
│   │   ├── Order.java                   # Customer order with line items
│   │   ├── PurchaseOrder.java           # Supplier purchase orders
│   │   └── LotTracking.java            # Regulatory lot/batch tracking
│   ├── rest/
│   │   ├── ProductResource.java         # JAX-RS endpoints
│   │   ├── OrderResource.java           # Order REST API
│   │   └── JaxRsApplication.java        # @ApplicationPath config
│   ├── jms/
│   │   ├── OrderMessageProducer.java    # JMS message producer
│   │   └── OrderMessageConsumer.java    # @MessageDriven bean
│   ├── jsf/
│   │   ├── ProductSearchBean.java       # @ManagedBean / @ViewScoped
│   │   ├── OrderWizardBean.java         # Multi-step order wizard
│   │   └── DashboardBean.java           # Admin dashboard backing bean
│   └── security/
│       └── LdapLoginModule.java         # JAAS login module
├── src/main/resources/
│   ├── META-INF/persistence.xml         # JPA configuration
│   └── META-INF/ejb-jar.xml            # EJB deployment descriptor
├── src/main/webapp/
│   ├── WEB-INF/
│   │   ├── web.xml                      # Servlet configuration
│   │   ├── faces-config.xml             # JSF navigation rules
│   │   └── beans.xml                    # CDI configuration
│   └── pages/
│       ├── productSearch.xhtml          # PrimeFaces DataTable with lazy loading
│       ├── orderWizard.xhtml            # Multi-step wizard
│       └── dashboard.xhtml              # PrimeFaces charts
└── src/test/java/                       # Arquillian-based integration tests
```

### Database Schema (PostgreSQL)
- **products** (id, sku, name, manufacturer, category, unit_price, requires_cold_chain, dea_schedule)
- **inventory** (id, product_id, warehouse_id, lot_number, quantity, expiry_date, received_date)
- **orders** (id, customer_id, order_date, status, total_amount, shipping_address, priority)
- **order_items** (id, order_id, product_id, lot_number, quantity, unit_price)
- **purchase_orders** (id, supplier_id, status, order_date, expected_delivery, total_amount)
- **lot_tracking** (id, product_id, lot_number, manufacturer_date, expiry_date, quantity_received, quantity_distributed)
- **customers** (id, name, type, license_number, address, credit_limit)
- **suppliers** (id, name, contact_email, lead_time_days, rating)

### Legacy Anti-Patterns Present
- EJB container dependency for transaction management and lifecycle
- JPA NamedQueries defined as annotations (hard to maintain, no compile-time checking)
- JSF view-scoped beans with complex conversation state
- JMS message-driven beans tightly coupled to ActiveMQ
- JAAS login module with LDAP realm configured in WildFly standalone.xml
- persistence.xml with WildFly-specific datasource JNDI name
- Arquillian tests requiring running WildFly instance
- CDI interceptors for cross-cutting concerns (logging, auditing)
- PrimeFaces component library lock-in for UI
- WAR deployment to external application server

## Target Architecture
- **Framework:** Java 21 with Spring Boot 3.3
- **Data Access:** Spring Data JPA with Hibernate 6
- **Messaging:** Spring for Apache Kafka or Azure Service Bus (replacing JMS/ActiveMQ)
- **Security:** Spring Security with OAuth2/OIDC (Entra ID)
- **UI:** Thymeleaf + htmx for server-side rendered UI (replacing JSF)
- **Validation:** Spring Validation with Hibernate Validator 8
- **REST:** Spring Web MVC (replacing JAX-RS)
- **Caching:** Spring Cache with Caffeine (replacing Singleton EJB)
- **Database:** Azure Database for PostgreSQL
- **Hosting:** Azure Container Apps
- **Build:** Maven with Spring Boot plugin (fat JAR)

### Architecture Description
The application server dependency is eliminated by moving to Spring Boot's embedded Tomcat. EJBs become Spring `@Service` classes with `@Transactional`. JPA configuration moves from persistence.xml to application.yml. JSF + PrimeFaces is replaced with Thymeleaf templates and htmx for interactivity. JMS message-driven beans become Spring Kafka listeners or Azure Service Bus consumers. JAAS authentication is replaced with Spring Security OAuth2 backed by Entra ID. The application deploys as a self-contained fat JAR in a Docker container on Azure Container Apps.

## Branch Structure
- `main` — Contains the completed lab with full APPMODLAB.md documentation
- `legacy` — The Java EE application deployable to WildFly
- `solution` — The fully migrated Spring Boot 3.x application
- `step-1-spring-boot-scaffold` — Spring Boot project setup, EJB to @Service migration
- `step-2-jpa-migration` — persistence.xml to Spring Data JPA with application.yml
- `step-3-jsf-to-thymeleaf` — JSF/PrimeFaces to Thymeleaf + htmx
- `step-4-messaging-migration` — JMS to Spring messaging with Azure Service Bus
- `step-5-security-migration` — JAAS to Spring Security with OAuth2
- `step-6-containerize-deploy` — Docker + Azure Container Apps

## Lab Content Requirements (APPMODLAB.md)
### Learning Objectives
- Migrate EJB business logic to Spring @Service components with @Transactional
- Convert JPA persistence.xml configuration to Spring Boot auto-configuration
- Replace JSF/PrimeFaces UI with Thymeleaf and htmx
- Migrate JMS message-driven beans to Spring messaging abstractions
- Replace JAAS authentication with Spring Security OAuth2

### Prerequisites
- Strong Java and Java EE experience
- Maven proficiency
- Docker Desktop installed
- JDK 21 installed
- Azure subscription
- Basic PostgreSQL knowledge

### Step-by-Step Instructions Outline
1. **Run the Legacy App** — Deploy to WildFly, explore product catalog and order workflows
2. **Create Spring Boot Project** — Scaffold Spring Boot 3.x, migrate EJBs to @Service classes
3. **Migrate JPA** — Replace persistence.xml with application.yml, convert NamedQueries to Spring Data repositories
4. **Migrate UI** — Replace JSF pages with Thymeleaf templates, add htmx for interactivity
5. **Migrate Messaging** — Replace JMS producers/consumers with Spring messaging
6. **Migrate Security** — Replace JAAS with Spring Security, configure Entra ID OAuth2
7. **Add Caching** — Replace Singleton EJB cache with Spring Cache + Caffeine
8. **Containerize** — Create Dockerfile, build fat JAR, deploy to Azure Container Apps
9. **Validate** — Run full order workflow, compare with legacy application behavior

### Estimated Duration
6–8 hours

### Key Concepts Covered
- Java EE to Spring Boot migration patterns
- Application server elimination
- JSF to modern server-side rendering
- JMS to cloud messaging migration
- OAuth2/OIDC authentication

## What the Squad Needs to Build
1. **Legacy App Setup:** Build a Java EE 7 application deployable to WildFly 26 with EJBs, JPA, JSF/PrimeFaces UI, JMS messaging, and JAAS security. Include a Docker Compose setup for WildFly + PostgreSQL + ActiveMQ. Seed data with pharmaceutical products, inventory lots, and sample orders.
2. **Modernization Implementation:** Migrate to Spring Boot 3.3 with Spring Data JPA, Thymeleaf, Spring Kafka or Azure Service Bus, and Spring Security OAuth2. All business workflows must be preserved. Package as a fat JAR with embedded Tomcat.
3. **Lab Documentation:** APPMODLAB.md with Java EE to Spring mapping table, step-by-step migration guide, and common migration pitfalls (e.g., CDI scope differences, transaction propagation differences).
4. **Infrastructure as Code:** Bicep templates for Azure Container Apps, Azure Database for PostgreSQL, Azure Service Bus, and Azure Active Directory app registration.
5. **CI/CD:** GitHub Actions with Maven build, test, Docker image push, and Azure Container Apps deployment.

## Acceptance Criteria
- [ ] Legacy app deploys to WildFly and runs all business workflows
- [ ] Product catalog search, order placement, and lot tracking work in legacy app
- [ ] Solution runs as standalone Spring Boot application (no external app server)
- [ ] All EJBs are replaced with Spring @Service classes
- [ ] JPA works with Spring Boot auto-configuration (no persistence.xml)
- [ ] UI works with Thymeleaf + htmx (no JSF dependencies)
- [ ] Messaging works via Spring messaging abstractions
- [ ] Authentication works via Spring Security OAuth2
- [ ] APPMODLAB.md follows the template format with complete frontmatter
- [ ] Step-by-step guide is clear and reproducible
- [ ] All code compiles and runs without errors
