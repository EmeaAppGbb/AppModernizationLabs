# Spec-Driven API Modernization

## Overview
- **Category:** Spec-Driven Development
- **Priority:** P2
- **Languages:** OpenAPI/REST/SOAP
- **Repository Name:** appmodlab-spec-driven-api-modernization
- **Organization:** EmeaAppGbb

## Objective
This lab teaches participants how to generate OpenAPI specifications from legacy SOAP and REST services and use those specifications to build modern, well-documented REST APIs. It covers WSDL-to-OpenAPI conversion, REST API analysis and contract extraction, specification refinement, and code generation from specs. The spec-driven approach ensures the modernized APIs maintain backward compatibility while adopting modern standards.

## Demo Legacy Application
**Business Domain:** Shipping and logistics integration hub for "CargoLink Express"

The legacy system exposes a mix of SOAP web services (for enterprise clients) and poorly documented REST endpoints (added later for mobile apps) that handle shipment booking, tracking, rate calculation, and carrier management. External partners depend on these APIs.

### Tech Stack
- WCF SOAP services (C#/.NET Framework 4.6.2)
- ASP.NET Web API 1.x REST endpoints (added ad-hoc, no OpenAPI docs)
- WSDL files defining SOAP contracts
- XML request/response for SOAP, JSON for REST
- SQL Server for data
- No API gateway or rate limiting
- API keys passed in custom headers

### Key Files/Folders Structure
```
cargolink-api/
├── CargoLink.sln
├── CargoLink.SoapServices/
│   ├── ShipmentService.svc          # Shipment booking SOAP service
│   ├── TrackingService.svc          # Package tracking SOAP service
│   ├── RateService.svc              # Rate calculation SOAP service
│   ├── CarrierService.svc           # Carrier management SOAP service
│   ├── DataContracts/
│   │   ├── ShipmentRequest.cs       # Complex SOAP data contracts
│   │   ├── TrackingResponse.cs
│   │   └── RateQuote.cs
│   └── WSDL/
│       ├── ShipmentService.wsdl     # WSDL for shipment service
│       ├── TrackingService.wsdl     # WSDL for tracking service
│       └── RateService.wsdl         # WSDL for rate service
├── CargoLink.RestApi/
│   ├── Controllers/
│   │   ├── ShipmentController.cs    # REST shipment endpoints (undocumented)
│   │   ├── TrackingController.cs    # REST tracking endpoints
│   │   └── QuoteController.cs       # REST rate quote endpoints
│   └── Models/
│       ├── ShipmentDto.cs           # Different shape from SOAP contracts!
│       └── TrackingDto.cs           # Inconsistent naming conventions
├── CargoLink.Core/
│   ├── Services/                    # Shared business logic
│   └── Data/                        # EF6 data access
└── tests/
```

### API Inconsistencies (Key Problems)
- SOAP and REST APIs expose the same functionality with different models
- REST endpoints have no documentation — consumers rely on tribal knowledge
- SOAP data contracts use XML serialization with different field names than REST DTOs
- Error responses are inconsistent between SOAP (SOAP faults) and REST (ad-hoc JSON)
- API versioning is absent — breaking changes have historically broken integrations
- Authentication differs: SOAP uses WS-Security, REST uses custom header API keys
- Rate calculation logic is duplicated between SOAP and REST implementations

## Target Architecture
- **API Framework:** ASP.NET Core 9 Web API with OpenAPI-first design
- **API Specification:** OpenAPI 3.1 as the source of truth
- **API Gateway:** Azure API Management for routing, rate limiting, and documentation
- **Authentication:** OAuth2/OIDC via Entra ID (unified for all consumers)
- **Documentation:** Auto-generated from OpenAPI spec (Swagger UI + developer portal)
- **Backward Compatibility:** SOAP-to-REST translation layer in API Management
- **Versioning:** URL-based API versioning (v1, v2)
- **Code Generation:** NSwag or AutoRest for client SDK generation from OpenAPI

### Architecture Description
Spec2Cloud analyzes both SOAP WSDLs and REST endpoints to generate a unified OpenAPI 3.1 specification. This spec becomes the source of truth — resolving inconsistencies between SOAP and REST models, standardizing error responses, and defining a single authentication scheme. The new API is implemented from the OpenAPI spec using code generation for models and manual implementation for business logic. Azure API Management provides a SOAP-to-REST translation policy for legacy SOAP consumers, allowing them to continue calling SOAP endpoints while the backend is unified REST.

## Branch Structure
- `main` — Contains the completed lab with full APPMODLAB.md documentation
- `legacy` — The mixed SOAP/REST API system
- `solution` — The unified OpenAPI-first REST API with APIM configuration
- `step-1-wsdl-analysis` — WSDL parsing and SOAP contract extraction
- `step-2-rest-analysis` — REST endpoint analysis and contract extraction
- `step-3-unified-spec` — Merged and refined OpenAPI 3.1 specification
- `step-4-implementation` — ASP.NET Core API built from spec
- `step-5-apim-setup` — Azure API Management with SOAP translation

## Lab Content Requirements (APPMODLAB.md)
### Learning Objectives
- Extract API contracts from WSDL/SOAP and undocumented REST endpoints
- Generate a unified OpenAPI 3.1 specification from multiple API sources
- Resolve inconsistencies between SOAP and REST data models
- Implement an API from an OpenAPI specification (spec-first development)
- Configure Azure API Management for backward-compatible SOAP translation

### Prerequisites
- C# and ASP.NET experience
- Understanding of SOAP and REST concepts
- Basic OpenAPI/Swagger knowledge
- Azure subscription (for API Management)

### Step-by-Step Instructions Outline
1. **Explore Legacy APIs** — Call SOAP and REST endpoints, observe inconsistencies
2. **Parse WSDLs** — Extract SOAP contracts, map data types
3. **Analyze REST Endpoints** — Document undocumented REST APIs, extract implicit contracts
4. **Generate Unified Spec** — Create OpenAPI 3.1 merging SOAP and REST contracts
5. **Resolve Inconsistencies** — Standardize models, errors, and naming conventions
6. **Implement from Spec** — Build ASP.NET Core API matching the OpenAPI spec
7. **Generate Client SDKs** — Use NSwag to generate TypeScript and C# clients
8. **Configure APIM** — Set up API Management with SOAP-to-REST translation
9. **Validate Backward Compatibility** — Existing SOAP consumers still work

### Estimated Duration
4–6 hours

### Key Concepts Covered
- WSDL-to-OpenAPI conversion
- API specification unification
- OpenAPI-first development
- Azure API Management policies
- SOAP-to-REST backward compatibility

## What the Squad Needs to Build
1. **Legacy App Setup:** WCF SOAP services with WSDL files and ASP.NET Web API REST endpoints with deliberately inconsistent models. Include test data and a Postman/Bruno collection for testing both SOAP and REST APIs.
2. **Modernization Implementation:** Unified OpenAPI 3.1 specification, ASP.NET Core 9 API built from spec, generated client SDKs, and Azure API Management configuration with SOAP translation policies.
3. **Lab Documentation:** APPMODLAB.md with WSDL-to-OpenAPI mapping guide, inconsistency resolution patterns, spec-first implementation walkthrough, and APIM SOAP translation guide.
4. **Infrastructure as Code:** Bicep templates for Azure API Management, Azure App Service (for the API), and Azure SQL Database.
5. **CI/CD:** GitHub Actions with OpenAPI validation, API build/test, and APIM deployment.

## Acceptance Criteria
- [ ] SOAP services respond correctly to SOAP envelope requests
- [ ] REST endpoints respond to HTTP requests
- [ ] Unified OpenAPI 3.1 spec covers all SOAP and REST functionality
- [ ] Inconsistencies between SOAP and REST are resolved in the spec
- [ ] New API implements all endpoints matching the OpenAPI spec
- [ ] Client SDKs generated from spec compile and work
- [ ] APIM SOAP translation allows legacy SOAP consumers to work
- [ ] APPMODLAB.md follows the template format with complete frontmatter
- [ ] Step-by-step guide is clear and reproducible
- [ ] All code compiles and runs without errors
