# VMware to Azure VMware Solution

## Overview
- **Category:** Infrastructure Modernization
- **Priority:** P2
- **Languages:** VMware/Azure/PowerShell
- **Repository Name:** appmodlab-vmware-to-azure-vmware-solution
- **Organization:** EmeaAppGbb

## Objective
This lab teaches participants how to migrate VMware environments to Azure VMware Solution (AVS) for hybrid consistency. It covers VMware HCX for live migration, NSX-T networking configuration, vSAN storage, and integration with Azure native services. AVS allows organizations to run their VMware workloads natively on Azure without re-platforming, providing a fast path to cloud with minimal disruption while enabling gradual modernization.

## Demo Legacy Application
**Business Domain:** Retail chain point-of-sale and inventory management for "Harbor Retail Group"

The legacy VMware environment runs a three-tier retail application across multiple VMs: web tier (2 VMs behind load balancer), application tier (2 VMs with business logic), and database tier (1 VM with SQL Server). The environment includes vSphere HA, DRS, and vMotion configured.

### Tech Stack
- VMware vSphere 7.0 with vCenter Server
- 5 VMs across 3 tiers
- vSphere HA and DRS enabled
- vMotion for live VM migration
- NSX-V for network virtualization (micro-segmentation)
- vSAN for shared storage
- Windows Server 2019 for web and app tiers
- SQL Server 2019 Standard on Windows Server 2019
- ASP.NET MVC application on IIS
- Active Directory domain-joined VMs

### Key Files/Folders Structure
```
harbor-retail-vmware/
├── vmware-config/
│   ├── vcenter-inventory.json          # vCenter VM inventory export
│   ├── network-topology.json           # NSX-V network configuration
│   ├── resource-pools.json             # Resource pool definitions
│   ├── drs-rules.json                  # DRS affinity/anti-affinity rules
│   └── ha-config.json                  # HA cluster configuration
├── vm-specs/
│   ├── web-tier/
│   │   ├── web01-spec.json             # VM1: 4 vCPU, 8GB RAM, 100GB disk
│   │   └── web02-spec.json             # VM2: 4 vCPU, 8GB RAM, 100GB disk
│   ├── app-tier/
│   │   ├── app01-spec.json             # VM3: 8 vCPU, 16GB RAM, 200GB disk
│   │   └── app02-spec.json             # VM4: 8 vCPU, 16GB RAM, 200GB disk
│   └── db-tier/
│       └── db01-spec.json              # VM5: 16 vCPU, 64GB RAM, 500GB disk
├── application/
│   ├── HarborRetail.Web/               # ASP.NET MVC frontend
│   ├── HarborRetail.Api/               # Web API business logic
│   └── HarborRetail.Database/          # SQL Server database project
├── networking/
│   ├── nsx-v-config/                   # NSX-V distributed firewall rules
│   ├── load-balancer/                  # NSX load balancer config
│   └── dns-records.json                # Internal DNS entries
├── scripts/
│   ├── powercli/
│   │   ├── export-inventory.ps1        # PowerCLI inventory export
│   │   ├── assess-compatibility.ps1    # HCX compatibility check
│   │   └── migration-runbook.ps1       # Migration automation script
│   └── terraform/
│       └── avs-provision.tf            # AVS private cloud provisioning
└── documentation/
    ├── runbook.md                      # Migration runbook
    └── rollback-plan.md               # Rollback procedures
```

### VM Inventory
| VM Name | OS | Role | vCPU | RAM | Disk | Network |
|---------|-----|------|------|-----|------|---------|
| WEB01 | Windows 2019 | IIS Web Server | 4 | 8 GB | 100 GB | Web-Segment |
| WEB02 | Windows 2019 | IIS Web Server | 4 | 8 GB | 100 GB | Web-Segment |
| APP01 | Windows 2019 | API Server | 8 | 16 GB | 200 GB | App-Segment |
| APP02 | Windows 2019 | API Server | 8 | 16 GB | 200 GB | App-Segment |
| DB01 | Windows 2019 | SQL Server 2019 | 16 | 64 GB | 500 GB | DB-Segment |

### Legacy Anti-Patterns / Challenges
- NSX-V (deprecated) needs migration to NSX-T on AVS
- vSAN storage configuration must map to AVS storage policies
- Custom DRS rules may not translate directly
- Network micro-segmentation rules need NSX-T equivalents
- Active Directory dependency for VM authentication
- SQL Server Always On not configured (single instance)
- Manual VM scaling (no auto-scaling)
- On-premises backup solution (Veeam or similar)
- No hybrid connectivity to Azure services
- Performance baselines not documented

## Target Architecture
- **Platform:** Azure VMware Solution (AVS) private cloud
- **Migration Tool:** VMware HCX for live workload migration
- **Networking:** NSX-T for micro-segmentation and load balancing
- **Storage:** vSAN on AVS with storage policies
- **Connectivity:** ExpressRoute Global Reach for AVS-to-Azure connectivity
- **Identity:** Azure AD + AD DS extended to AVS
- **Backup:** Azure Backup for AVS
- **Monitoring:** Azure Monitor + vRealize Operations (optional)
- **Integration:** Azure native services via ExpressRoute (Azure SQL, Storage, etc.)
- **DNS:** Azure Private DNS zones

### Architecture Description
The on-premises VMware environment is migrated to an AVS private cloud using HCX for live migration with no downtime. NSX-V security policies are recreated in NSX-T on AVS. vSAN storage policies map to AVS storage. ExpressRoute Global Reach provides connectivity between AVS and Azure native services, enabling future modernization. VMs continue to run unchanged on AVS while the organization can gradually adopt Azure native services. Azure Backup replaces on-premises backup. Azure Monitor provides cloud-native observability.

## Branch Structure
- `main` — Contains the completed lab with full APPMODLAB.md documentation
- `legacy` — VMware configuration exports, application source, and PowerCLI scripts
- `solution` — AVS deployment, HCX configuration, NSX-T policies, and Azure integrations
- `step-1-assessment` — Compatibility assessment and migration planning
- `step-2-avs-provision` — AVS private cloud provisioning and networking
- `step-3-hcx-setup` — HCX connector deployment and site pairing
- `step-4-migration` — VM migration waves with HCX
- `step-5-post-migration` — NSX-T policies, Azure integration, backup, monitoring

## Lab Content Requirements (APPMODLAB.md)
### Learning Objectives
- Assess VMware environments for Azure VMware Solution compatibility
- Provision and configure an AVS private cloud
- Set up VMware HCX for workload migration
- Migrate VMs with minimal downtime using HCX vMotion or bulk migration
- Configure NSX-T networking and integrate with Azure native services

### Prerequisites
- VMware vSphere administration experience
- Azure subscription with AVS quota approved (requires quota request)
- Azure CLI and PowerCLI installed
- Basic networking knowledge (BGP, ExpressRoute)
- VMware HCX license (included with AVS)

### Step-by-Step Instructions Outline
1. **Assess VMware Environment** — Export inventory, check HCX compatibility, document network topology
2. **Provision AVS Private Cloud** — Deploy AVS via Bicep/Terraform, configure ExpressRoute
3. **Set Up HCX** — Deploy HCX connector, configure site pairing, create network profiles
4. **Configure Networking** — Set up NSX-T segments, recreate firewall rules, configure DNS
5. **Plan Migration Waves** — Group VMs by dependency, define migration order
6. **Execute Migration** — Migrate VMs using HCX (vMotion for zero-downtime or bulk migration)
7. **Validate** — Test application functionality, verify network connectivity
8. **Post-Migration** — Configure Azure Backup, set up monitoring, document Azure integrations
9. **Azure Integration** — Connect to Azure native services via ExpressRoute

### Estimated Duration
6–8 hours (plus AVS provisioning time, which can take 3-4 hours)

### Key Concepts Covered
- Azure VMware Solution architecture
- VMware HCX migration methods
- NSX-T networking on AVS
- ExpressRoute Global Reach
- Hybrid cloud with Azure native services

## What the Squad Needs to Build
1. **Legacy App Setup:** Provide a three-tier ASP.NET retail application source code, SQL Server database project, VMware configuration exports (JSON/XML), and PowerCLI scripts for environment documentation. Since AVS requires actual VMware infrastructure, provide detailed simulation scripts and mock data.
2. **Modernization Implementation:** Complete AVS deployment automation (Bicep/Terraform), HCX configuration scripts, NSX-T policy definitions, Azure Backup configuration, and monitoring setup. Include validation scripts that verify post-migration functionality.
3. **Lab Documentation:** APPMODLAB.md with VMware-to-AVS migration playbook, HCX configuration walkthrough, NSX-V to NSX-T mapping guide, and Azure integration patterns.
4. **Infrastructure as Code:** Bicep/Terraform for AVS private cloud, ExpressRoute circuit, VNet with gateway, NSG rules, Azure Backup vault, and Azure Monitor workspace.
5. **CI/CD:** GitHub Actions for infrastructure deployment and post-migration validation tests.

## Acceptance Criteria
- [ ] VMware environment is fully documented with inventory exports
- [ ] AVS private cloud provisions successfully via IaC
- [ ] HCX site pairing and network profiles are configured
- [ ] VM migration completes with minimal/zero downtime
- [ ] Application functions correctly on AVS
- [ ] NSX-T firewall rules replicate NSX-V policies
- [ ] Azure Backup is configured for AVS VMs
- [ ] ExpressRoute connectivity to Azure services is verified
- [ ] APPMODLAB.md follows the template format with complete frontmatter
- [ ] Step-by-step guide is clear and reproducible
