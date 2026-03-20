# App Modernization Labs Backlog

Welcome to the **Application Modernization Labs Backlog**. This document outlines all planned lab exercises designed to help organizations modernize legacy applications, infrastructure, and data platforms.

## About This Backlog

The App Modernization Labs project provides hands-on, production-ready examples of modernizing legacy systems across three key dimensions:

- **Code Modernization**: Updating legacy programming languages and frameworks to modern equivalents
- **Infrastructure Modernization**: Migrating on-premises workloads to cloud-native solutions
- **Data Modernization**: Transforming legacy databases and ETL processes to modern data platforms

Each lab is a complete reference implementation that demonstrates real-world migration patterns and best practices.

## How to Pick Up a Lab

1. **Choose a Lab**: Select a lab from the backlog that interests you or matches your organization's needs
2. **Create the Repository**: Create a new public GitHub repository following the naming pattern: `appmodlab-[lab-slug]`
3. **Add APPMODLAB.MD**: Include an `APPMODLAB.md` file in the repository root that documents:
   - Lab objectives and learning outcomes
   - Prerequisites and setup instructions
   - Step-by-step implementation guide
   - Real-world considerations and gotchas
   - Cleanup/teardown instructions
4. **Submit an Issue**: Open an issue in this repository linking to your lab repo and updating this backlog when your lab is complete
5. **Contributions Welcome**: We accept community contributions! If you've built a modernization lab, we'd love to have it included

## Modernization Labs Backlog

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

---

## Suggested Labs (Community Ideas)

These labs have been suggested by the community but haven't been fully scoped yet. If you're interested in building one of these, please open an issue to discuss scope and approach!

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

---

## Contributing

We welcome contributions from the community! If you've successfully modernized an application or built a reference lab, we'd love to feature it here. Please see [CONTRIBUTING.md](../CONTRIBUTING.md) for guidelines on submitting your lab.

### Questions or Suggestions?

- Open an issue in this repository to discuss a lab idea or ask questions
- Check existing issues to see if others are working on similar modernization scenarios
- Share your experience with existing labs—your feedback helps improve the community resources
