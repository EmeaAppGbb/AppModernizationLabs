# On-Premises VMs to Azure Container Apps

## Overview
- **Category:** Infrastructure Modernization
- **Priority:** P1
- **Languages:** Docker/Kubernetes/Bicep
- **Repository Name:** appmodlab-on-prem-vms-to-container-apps
- **Organization:** EmeaAppGbb

## Objective
This lab teaches participants how to containerize VM-based workloads and migrate them to Azure Container Apps. It covers the full journey from analyzing VM-hosted applications, creating Dockerfiles, setting up container registries, configuring networking and scaling, and deploying to Azure Container Apps with Dapr integration. This is one of the most impactful infrastructure modernizations, eliminating VM patching overhead and enabling auto-scaling.

## Demo Legacy Application
**Business Domain:** Regional veterinary clinic management system for "PawsCare Veterinary Network"

The legacy system runs as three separate VMs: a web server (IIS hosting an ASP.NET Core 3.1 app), an API server (Node.js Express 14.x), and a background worker (Python 3.8 script for appointment reminders and lab result processing). All three VMs are on the same virtual network with static IP addressing.

### Tech Stack
- **VM 1 — Web Frontend:** Windows Server 2019, IIS 10, ASP.NET Core 3.1 MVC
- **VM 2 — API Server:** Ubuntu 20.04, Node.js 14.x, Express 4.x, MongoDB 4.4
- **VM 3 — Background Worker:** Ubuntu 20.04, Python 3.8, Celery + RabbitMQ, cron jobs
- SQL Server 2019 Standard on VM 1 (shared with web app)
- MongoDB 4.4 on VM 2 (for appointment scheduling and pet records)
- RabbitMQ 3.8 on VM 3 (for message queuing)
- Shared file storage via SMB mount for uploaded documents (X-ray images, lab reports)
- Static IP addressing between VMs, no service discovery

### Key Files/Folders Structure
```
pawscare-system/
├── web-frontend/                     # ASP.NET Core 3.1 MVC (VM 1)
│   ├── PawsCare.Web.csproj
│   ├── Controllers/
│   │   ├── AppointmentController.cs  # Appointment booking
│   │   ├── PatientController.cs      # Pet patient records
│   │   ├── OwnerController.cs        # Pet owner management
│   │   └── DashboardController.cs    # Clinic dashboard
│   ├── Views/                        # Razor views
│   └── appsettings.json              # Hardcoded VM IP addresses
├── api-server/                       # Node.js Express (VM 2)
│   ├── package.json
│   ├── server.js                     # Express app with MongoDB connection
│   ├── routes/
│   │   ├── appointments.js           # Appointment API
│   │   ├── patients.js               # Pet records API
│   │   ├── prescriptions.js          # Prescription management
│   │   └── labResults.js             # Lab result uploads
│   └── models/                       # Mongoose models
├── background-worker/                # Python worker (VM 3)
│   ├── requirements.txt
│   ├── worker.py                     # Celery worker for async tasks
│   ├── tasks/
│   │   ├── reminders.py              # Appointment reminder emails
│   │   ├── lab_processing.py         # Lab result PDF generation
│   │   └── reports.py                # Daily clinic reports
│   └── crontab                       # Cron schedule for daily tasks
└── infrastructure/
    ├── network-diagram.png           # VM network topology
    └── vm-setup-scripts/             # PowerShell/Bash VM provisioning
```

### Infrastructure Configuration
- VM 1: 10.0.1.10 (Windows, 4 vCPU, 8GB RAM)
- VM 2: 10.0.1.20 (Linux, 2 vCPU, 4GB RAM)
- VM 3: 10.0.1.30 (Linux, 2 vCPU, 4GB RAM)
- NSG rules allowing traffic between VMs on specific ports
- SMB share at \\\\10.0.1.10\\documents for shared files
- Manual SSL certificate management on IIS

### Legacy Anti-Patterns Present
- Hardcoded IP addresses in application configuration
- Manual VM patching and OS updates
- No auto-scaling (VMs sized for peak load, wasting resources off-peak)
- SMB file share for inter-service document exchange
- RabbitMQ single instance (no clustering, no persistence)
- Cron-based scheduling with no retry or monitoring
- Manual SSL certificate deployment on IIS
- No health checks or liveness probes
- Monolithic deployment — updating one component requires VM access
- No container orchestration — scaling means cloning VMs

## Target Architecture
- **Container Runtime:** Azure Container Apps (serverless containers)
- **Container Registry:** Azure Container Registry (ACR)
- **Service Communication:** Dapr sidecar for service-to-service invocation
- **Messaging:** Dapr pub/sub with Azure Service Bus (replacing RabbitMQ)
- **State Store:** Dapr state management with Azure Cosmos DB
- **File Storage:** Azure Blob Storage (replacing SMB share)
- **Database:** Azure SQL Database + Azure Cosmos DB for MongoDB API
- **Scaling:** KEDA-based auto-scaling rules
- **Ingress:** Azure Container Apps built-in ingress with TLS
- **Monitoring:** Azure Monitor + Container Apps observability

### Architecture Description
Each VM workload becomes a container in Azure Container Apps. The web frontend, API server, and background worker each get their own Dockerfile and container image stored in ACR. Dapr sidecars handle service discovery (replacing hardcoded IPs), pub/sub messaging (replacing RabbitMQ), and state management. Azure Blob Storage replaces the SMB file share. KEDA auto-scaling replaces static VM sizing. Built-in TLS termination replaces manual IIS certificate management. The background worker's cron jobs become Azure Container Apps jobs with scheduled triggers.

## Branch Structure
- `main` — Contains the completed lab with full APPMODLAB.md documentation
- `legacy` — The three-VM application (runnable via Docker Compose simulating VMs)
- `solution` — The containerized Azure Container Apps deployment
- `step-1-containerize` — Dockerfiles for all three applications
- `step-2-compose` — Docker Compose for local container development
- `step-3-dapr-integration` — Dapr sidecar configuration for service communication
- `step-4-azure-services` — Blob Storage, Service Bus, Cosmos DB integration
- `step-5-container-apps-deploy` — Full Azure Container Apps deployment with Bicep

## Lab Content Requirements (APPMODLAB.md)
### Learning Objectives
- Analyze VM-hosted applications for containerization readiness
- Create optimized multi-stage Dockerfiles for .NET, Node.js, and Python
- Use Dapr for service discovery, pub/sub, and state management in containers
- Configure KEDA auto-scaling rules based on HTTP traffic and queue depth
- Deploy multi-container applications to Azure Container Apps

### Prerequisites
- Docker Desktop installed
- Basic understanding of containers and Docker
- Azure subscription with Contributor access
- Azure CLI installed
- Familiarity with at least one of: .NET, Node.js, or Python

### Step-by-Step Instructions Outline
1. **Explore the Legacy System** — Run the three-VM simulation, understand inter-service communication
2. **Create Dockerfiles** — Build optimized multi-stage Dockerfiles for each service
3. **Local Container Testing** — Docker Compose to verify containers work together
4. **Add Dapr** — Configure Dapr sidecars for service invocation and pub/sub
5. **Migrate to Azure Services** — Replace local MongoDB with Cosmos DB, RabbitMQ with Service Bus
6. **Deploy to Container Apps** — Push images to ACR, deploy Container Apps environment
7. **Configure Scaling** — Set up KEDA rules for auto-scaling
8. **Validate End-to-End** — Book appointments, upload lab results, verify reminders

### Estimated Duration
4–6 hours

### Key Concepts Covered
- VM-to-container migration strategy
- Multi-stage Docker builds
- Dapr building blocks
- Azure Container Apps architecture
- KEDA auto-scaling

## What the Squad Needs to Build
1. **Legacy App Setup:** Build three applications (ASP.NET Core MVC, Node.js Express, Python Celery worker) that communicate via hardcoded IPs. Provide Docker Compose that simulates the VM topology with separate containers on a shared network. Include sample data for pets, appointments, and lab results.
2. **Modernization Implementation:** Containerize all three apps with optimized Dockerfiles. Add Dapr for service communication. Integrate Azure Blob Storage, Service Bus, and Cosmos DB. Deploy to Azure Container Apps with KEDA scaling.
3. **Lab Documentation:** APPMODLAB.md with VM-to-container assessment checklist, Dockerfile best practices, Dapr configuration guide, and architecture before/after diagrams.
4. **Infrastructure as Code:** Bicep templates for ACR, Azure Container Apps environment, Azure Cosmos DB, Azure Service Bus, Azure Blob Storage, and Azure SQL Database.
5. **CI/CD:** GitHub Actions workflows for building all three container images and deploying to Azure Container Apps.

## Acceptance Criteria
- [ ] Legacy system runs via Docker Compose simulating three VMs
- [ ] Appointment booking and lab result upload work across services
- [ ] All three services containerized with multi-stage Dockerfiles
- [ ] Dapr handles service-to-service communication (no hardcoded IPs)
- [ ] Azure Service Bus replaces RabbitMQ for messaging
- [ ] Azure Blob Storage replaces SMB for document storage
- [ ] KEDA auto-scaling configured and tested
- [ ] Solution deploys to Azure Container Apps via Bicep
- [ ] APPMODLAB.md follows the template format with complete frontmatter
- [ ] Step-by-step guide is clear and reproducible
- [ ] All code compiles and runs without errors
