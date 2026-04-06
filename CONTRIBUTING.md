# Contributing to Agentic Application Enablement Labs

Thanks for your interest in contributing to Agentic Application Enablement Labs! Whether you're adding a new lab, reporting an issue, or improving the gallery itself, we appreciate your help.

## 🎯 Ways to Contribute

### 1. 🎮 Add a Lab

Share your application modernization lab with the community:

1. Create an `APPMODLAB.MD` file in your repository root (use [LABTemplate.md](LABTemplate.md))
2. Fill in all required metadata fields
3. Submit via the "🎮 Add New Lab" button on the gallery or create an [issue using this template](.github/ISSUE_TEMPLATE/add-lab.yml)
4. Our team will review and add it to the gallery

**Your repository must be public and meet these requirements:**
- ✅ Public GitHub repository
- ✅ `APPMODLAB.MD` file at the repository root
- ✅ Lab title: 40 characters or less
- ✅ Description: 140 characters or less
- ✅ Required fields completed: title, description, authors, category, industry
- ✅ At least one language or technology specified
- ✅ (Recommended) Thumbnail image at 16:9 aspect ratio (1280×720 or 1920×1080)

### 2. 🐛 Report Issues

Found a bug or problem? Create an issue using the [🐛 Report Issue template](.github/ISSUE_TEMPLATE/report-issue.yml).

Include:
- What happened
- What you expected to happen
- Steps to reproduce the issue
- Any relevant links or screenshots

### 3. 💬 Give Feedback

Have suggestions or ideas? Use the [💬 Feedback template](.github/ISSUE_TEMPLATE/feedback.yml) to share:
- Feature requests
- Content suggestions
- Design/UX feedback
- General thoughts

### 4. 🏗️ Improve the Gallery

Help improve the gallery website itself:
1. Fork this repository
2. Create a feature branch (`git checkout -b feature/my-improvement`)
3. Make your changes
4. Test locally (just open `index.html` in a browser)
5. Submit a pull request with a clear description

## 📋 Lab Submission Guidelines

### Required Metadata

Every lab **must** include these fields in `APPMODLAB.MD`:

- **title** — Short, memorable name (40 chars max)
  - ✅ "Modernize .NET App with Aspire"
  - ❌ "A comprehensive guide to modernizing your .NET applications using the Azure Container Apps with Aspire"

- **description** — One-line summary of what the lab covers (140 chars max)
  - ✅ "Step-by-step guide to containerize and modernize legacy .NET apps with Aspire"
  - ❌ "This lab is about taking old .NET apps and making them modern with lots of features..."

- **authors** — GitHub handles of main contributors (at least one)
  - Example: `["octocat", "github-user"]`

- **category** — One required choice:
  - Code Modernization
  - Infra Modernization
  - Data Modernization

- **industry** — One required choice:
  - Cross-Industry
  - Financial Services
  - Healthcare & Life Sciences
  - Manufacturing
  - Retail & Consumer Goods
  - Government & Public Sector
  - Education
  - Energy & Resources
  - Telco & Media
  - Mobility & Automotive

- **languages** — At least one technology/language:
  - .NET, Python, Java, Go, TypeScript, JavaScript, BICEP, Terraform, COBOL

### Optional but Recommended

- **services** — Azure services used (e.g., "Azure Container Apps", "Azure CosmosDB")
- **frameworks** — Frameworks and tools (e.g., "Aspire", "Microsoft Agent Framework")
- **modernizationTools** — Modernization-specific tools (e.g., "Dr Migrate", "GitHub Copilot", "SQUAD")
- **tags** — Custom labels for better discoverability
- **thumbnail** — Lab preview image (16:9 aspect ratio preferred: 1280×720 or 1920×1080)
- **video** — Demo video URL
- **version** — Semantic versioning (e.g., 1.0.0)

## 📝 Code of Conduct

We are committed to providing a welcoming and inspiring community. Please review and follow the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/). Questions? Contact us at [opencode@microsoft.com](mailto:opencode@microsoft.com).

## 🚀 Development Setup

### Running the Gallery Locally

The gallery website is a static site—no build step required!

1. Clone this repository
2. Open `index.html` in your browser
3. Browse and test the gallery locally

### Testing Your Lab Submission

Before submitting your lab:
1. Create `APPMODLAB.MD` in your repository root
2. Validate the YAML frontmatter (use an online YAML validator)
3. Check that all required fields are filled in
4. Verify character limits (title ≤ 40, description ≤ 140)
5. Test any links or resources in your lab repository

## ❓ Questions?

- 📖 See [README.md](README.md) for overview and project details
- 📋 See [LABTemplate.md](LABTemplate.md) for the complete template
- 🎮 Visit the [live gallery](https://EmeaAppGbb.github.io/AppModernizationLabs/)
- 💬 Open an issue to ask questions

Thank you for contributing to Agentic Application Enablement Labs! 🚀
