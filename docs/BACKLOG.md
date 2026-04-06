# Agentic Application Enablement Labs Backlog

Welcome to the **Agentic Application Enablement Labs Backlog**. This document outlines all planned lab exercises designed to help organizations modernize legacy applications, adopt agentic software development workflows, and leverage spec-driven development methodologies.

## About This Backlog

The Agentic Application Enablement Labs project provides hands-on, production-ready examples across five key dimensions:

- **Code Modernization**: Updating legacy programming languages and frameworks to modern equivalents
- **Infrastructure Modernization**: Migrating on-premises workloads to cloud-native solutions
- **Data Modernization**: Transforming legacy databases and ETL processes to modern data platforms
- **Agentic Software Development**: Using SQUAD multi-agent teams to plan, build, review, and ship software with AI agents
- **Spec-Driven Development**: Leveraging Spec2Cloud to reverse-engineer legacy systems, generate specifications, and drive modernization from specs

Each lab is a complete reference implementation that demonstrates real-world patterns and best practices — from traditional modernization to agentic AI-driven development pipelines.

## How to Pick Up a Lab

1. **Choose a Lab**: Select a lab from the backlog that interests you or matches your organization's needs. Labs span five categories: Code Modernization, Infrastructure Modernization, Data Modernization, Agentic Software Development, and Spec-Driven Development.
2. **Create the Repository**: Create a new public GitHub repository following the naming pattern: `appmodlab-[lab-slug]`
3. **Add APPMODLAB.MD**: Include an `APPMODLAB.md` file in the repository root that documents:
   - Lab objectives and learning outcomes
   - Prerequisites and setup instructions
   - Step-by-step implementation guide
   - Real-world considerations and gotchas
   - Cleanup/teardown instructions
   - **Category**: Use one of the following values: `Code Modernization`, `Infrastructure Modernization`, `Data Modernization`, `Agentic Software Development`, `Spec-Driven Development`, or `Cross-Cutting`
4. **Submit an Issue**: Open an issue in this repository linking to your lab repo and updating this backlog when your lab is complete
5. **Contributions Welcome**: We accept community contributions! If you've built a modernization lab or an agentic development workflow, we'd love to have it included

## Labs Backlog

### Code Modernization

| Priority | Lab Title | Category | Languages | Description | Status |
|----------|-----------|----------|-----------|-------------|--------|
| P1 | COBOL to Java Modernization | Code | COBOL → Java | Migrate legacy COBOL mainframe applications to modern Java microservices architecture | 🔴 Not Started |
| P1 | .NET Framework to .NET 9 Modernization | Code | C#/.NET | Upgrade legacy .NET Framework applications to .NET 9 with modern async/await and dependency injection patterns | 🔴 Not Started |
| P1 | .NET Framework with EntLib to .NET 9 with Core DI | Code | C#/.NET | Replace Enterprise Library abstractions with native .NET Core dependency injection and configuration patterns | 🔴 Not Started |
| P2 | Spec2Cloud .NET Framework Modernization | Code | C#/.NET | Use Spec2Cloud methodology and tooling to modernize .NET Framework applications | 🔴 Not Started |
| P2 | Spec2Cloud Monolith to Microservices .NET | Code | C#/.NET | Break down monolithic .NET applications into microservices using Spec2Cloud analysis and refactoring | 🔴 Not Started |
| P1 | Java EE to Spring Boot Modernization | Code | Java | Migrate Java EE and Jakarta EE applications to Spring Boot with modern cloud-native patterns | 🔴 Not Started |
| P2 | Classic ASP to ASP.NET Core | Code | VBScript/C# | Modernize legacy Classic ASP websites to ASP.NET Core with Razor Pages or MVC | 🔴 Not Started |
| P2 | VB6 to .NET 9 Modernization | Code | VB6 → C#/.NET | Migrate Visual Basic 6 desktop applications to .NET 9 WinForms/WPF or web-based alternatives | 🔴 Not Started |
| P3 | PHP Legacy to Python Flask/FastAPI | Code | PHP → Python | Modernize legacy PHP applications to Python with Flask or FastAPI frameworks | 🔴 Not Started |
| P3 | AngularJS to React/Angular Modern | Code | JavaScript | Upgrade legacy AngularJS frontends to modern React or Angular with TypeScript | 🔴 Not Started |

### Infrastructure Modernization

| Priority | Lab Title | Category | Languages | Description | Status |
|----------|-----------|----------|-----------|-------------|--------|
| P1 | On-Premises VMs to Azure Container Apps | Infrastructure | Docker/Kubernetes | Containerize and migrate VM-based workloads to Azure Container Apps | 🔴 Not Started |
| P2 | Windows Services to Azure Functions | Infrastructure | C#/.NET | Convert Windows Services to serverless Azure Functions with appropriate triggers | 🔴 Not Started |
| P1 | IIS to Azure App Service Migration | Infrastructure | IIS/HTTP | Migrate IIS-hosted applications (ASP.NET, Node.js, PHP) to Azure App Service | 🔴 Not Started |
| P1 | On-Prem SQL Server to Azure SQL Managed Instance | Infrastructure | SQL Server | Database migration with minimal downtime using native tools and Azure Database Migration Service | 🔴 Not Started |
| P2 | VMware to Azure VMware Solution | Infrastructure | VMware/Azure | Migrate VMware environments to Azure VMware Solution for hybrid consistency | 🔴 Not Started |

### Data Modernization

| Priority | Lab Title | Category | Languages | Description | Status |
|----------|-----------|----------|-----------|-------------|--------|
| P2 | Oracle to Azure PostgreSQL Migration | Data | SQL/Python | Migrate Oracle databases to Azure Database for PostgreSQL with schema conversion | 🔴 Not Started |
| P2 | Legacy ETL to Azure Data Factory | Data | SQL/Python | Modernize legacy ETL pipelines (SSIS, Informatica, etc.) to Azure Data Factory | 🔴 Not Started |
| P2 | On-Prem Data Warehouse to Azure Synapse | Data | SQL/Python | Migrate on-premises data warehouses to Azure Synapse Analytics | 🔴 Not Started |
| P3 | Legacy Reporting to Power BI | Data | SQL/DAX | Modernize legacy reporting systems (Crystal Reports, SSRS) to Power BI | 🔴 Not Started |
| P3 | MongoDB to Azure Cosmos DB | Data | NoSQL/JavaScript | Migrate MongoDB workloads to Azure Cosmos DB with MongoDB API | 🔴 Not Started |

### Agentic Software Development

| Priority | Lab Title | Category | Languages | Description | Status |
|----------|-----------|----------|-----------|-------------|--------|
| P1 | Getting Started with SQUAD | Agentic | YAML/Markdown | Setting up a SQUAD team on a new repository, configuring agents, and running your first agentic development session | 🔴 Not Started |
| P1 | SQUAD-Driven Feature Development | Agentic | TypeScript/Python | Using SQUAD to plan, implement, test, and ship a feature end-to-end with AI agents | 🔴 Not Started |
| P2 | SQUAD Team Customization | Agentic | YAML/Markdown | Adding custom agents, skills, and ceremonies to a SQUAD team for domain-specific workflows | 🔴 Not Started |
| P2 | SQUAD + GitHub Issues Workflow | Agentic | GitHub Actions/YAML | Connecting SQUAD to GitHub Issues for automated triage, assignment, and delivery | 🔴 Not Started |
| P2 | Multi-Agent Code Review with SQUAD | Agentic | TypeScript/Python | Setting up reviewer agents, rejection protocols, and quality gates in a SQUAD team | 🔴 Not Started |
| P3 | SQUAD Plugin Development | Agentic | TypeScript | Building and publishing plugins to extend SQUAD capabilities and share with the community | 🔴 Not Started |
| P3 | Ralph Automation | Agentic | YAML/GitHub Actions | Configuring Ralph for continuous work monitoring, idle-watch, and automated backlog processing | 🔴 Not Started |

### Spec-Driven Development

| Priority | Lab Title | Category | Languages | Description | Status |
|----------|-----------|----------|-----------|-------------|--------|
| P1 | Spec2Cloud Introduction | Spec-Driven | Markdown/YAML | Understanding spec-driven development methodology and the Spec2Cloud toolchain | 🔴 Not Started |
| P1 | Reverse Engineering Legacy Apps with Spec2Cloud | Spec-Driven | C#/.NET/Java | Analyzing a legacy codebase to generate specifications, then using those specs to guide modernization | 🔴 Not Started |
| P2 | Spec2Cloud + SQUAD Integration | Spec-Driven | TypeScript/Python | Using Spec2Cloud-generated specifications as input for SQUAD-driven development workflows | 🔴 Not Started |
| P2 | Spec-Driven API Modernization | Spec-Driven | OpenAPI/REST | Generating OpenAPI specs from legacy SOAP/REST services and using them to build modern APIs | 🔴 Not Started |
| P3 | Spec-Driven Database Migration | Spec-Driven | SQL/Python | Generating database schemas from legacy systems and using specs to guide migration to modern platforms | 🔴 Not Started |

### Cross-Cutting / End-to-End

| Priority | Lab Title | Category | Languages | Description | Status |
|----------|-----------|----------|-----------|-------------|--------|
| P1 | Legacy App to Agentic Development Pipeline | Cross-Cutting | C#/.NET/Python | Complete journey: reverse-engineer a legacy app with Spec2Cloud, set up SQUAD, modernize with agents | 🔴 Not Started |
| P2 | From Monolith to Microservices with Agents | Cross-Cutting | Java/C#/.NET | Using SQUAD + Spec2Cloud to decompose and modernize a monolithic application into microservices | 🔴 Not Started |
| P3 | Agentic CI/CD Pipeline | Cross-Cutting | GitHub Actions/YAML | Setting up GitHub Actions workflows that integrate with SQUAD for automated testing, review, and deployment | 🔴 Not Started |

---

## Suggested Labs (Community Ideas)

These labs have been suggested by the community but haven't been fully scoped yet. If you're interested in building one of these, please open an issue to discuss scope and approach!

### Modernization Ideas

- **Mainframe CICS to Azure API Management**: Expose mainframe CICS transactions as managed APIs through Azure API Management
- **COBOL to Python with AI-Assisted Translation**: Explore using AI/ML tools to assist in translating COBOL business logic to Python
- **Legacy SOAP to REST API Modernization**: Convert legacy SOAP web services to modern RESTful APIs
- **Struts to Spring MVC/Spring Boot**: Migrate Apache Struts applications to Spring MVC or Spring Boot
- **Ruby on Rails to Node.js/Express**: Modernize Ruby on Rails applications to Node.js with Express
- **Perl Scripts to Python Automation**: Refactor legacy Perl automation scripts to Python with modern tooling
- **Lotus Notes to Microsoft 365**: Migrate Lotus Notes applications and workflows to Microsoft 365 platform
- **Sybase to SQL Server Migration**: Modernize legacy Sybase databases to SQL Server or Azure SQL
- **JUnit 3 Legacy Tests to JUnit 5 with TestNG**: Upgrade legacy Java testing frameworks to modern standards
- **Custom Search Engine to Azure Cognitive Search**: Replace custom search implementations with Azure Cognitive Search

### Agentic Development Ideas

- **SQUAD for Open-Source Maintenance**: Using SQUAD agents to triage issues, review PRs, and maintain open-source projects
- **Agentic Documentation Generation**: Using SQUAD + Spec2Cloud to auto-generate and maintain living documentation from codebases
- **Multi-Repo SQUAD Orchestration**: Coordinating SQUAD teams across multiple repositories for large-scale modernization programs
- **Agentic Security Scanning Pipeline**: Integrating SQUAD with security tools for automated vulnerability detection and remediation
- **Spec2Cloud for Legacy API Discovery**: Using Spec2Cloud to discover and document undocumented legacy APIs and services
- **SQUAD-Driven Technical Debt Reduction**: Using agents to systematically identify, prioritize, and resolve technical debt

---

## Contributing

We welcome contributions from the community! If you've successfully modernized an application, built a reference lab, or created an agentic development workflow, we'd love to feature it here. Please see [CONTRIBUTING.md](../CONTRIBUTING.md) for guidelines on submitting your lab.

### Questions or Suggestions?

- Open an issue in this repository to discuss a lab idea or ask questions
- Check existing issues to see if others are working on similar modernization or agentic development scenarios
- Share your experience with existing labs—your feedback helps improve the community resources
