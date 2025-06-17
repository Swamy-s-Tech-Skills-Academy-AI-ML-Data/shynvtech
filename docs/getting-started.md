# Getting Started with ShynvTech Platform

This guide will help you set up and run the ShynvTech platform - a **tech innovation collective** designed for software professionals, community builders, and continuous learners who want to showcase their expertise and build meaningful connections.

## Who This Platform Is For

- **Azure Cloud Architects** and **DevOps Engineers**
- **Community Leaders** organizing meetups and events
- **Mentors** running spotlight sessions and learning programs
- **Technical Speakers** sharing knowledge and expertise
- **AI/ML Enthusiasts** exploring LangChain and Azure OpenAI
- **Software Professionals** building personal brands and networks

## Prerequisites

Before you begin, ensure you have the following installed:

### Required Software

- **.NET 9 SDK** (Preview or later)

  - Download from: https://dotnet.microsoft.com/download/dotnet/9.0
  - Verify installation: `dotnet --version`

- **Visual Studio 2025** (recommended) or **Visual Studio Code**

  - Visual Studio 2025 with .NET Aspire workload
  - VS Code with C# Dev Kit extension

- **Git** for version control
  - Download from: https://git-scm.com/

### Optional Tools

- **Docker Desktop** (for containerization in future)
- **Azure CLI** (for Azure deployment)
- **PowerShell 7+** (for cross-platform scripts)

## Quick Start

### 1. Clone the Repository

```bash
git clone <repository-url>
cd shynvtech
```

### 2. Restore Dependencies

```bash
dotnet restore
```

### 3. Build the Solution

```bash
dotnet build
```

### 4. Run the Application

```bash
dotnet run --project src/ShynvTech.AppHost
```

The Aspire dashboard will automatically open in your browser, typically at:

- **Dashboard**: `https://localhost:17123` (or similar)
- **Web Frontend**: Auto-assigned port (check dashboard)

## Development Workflow

### Running Individual Services

You can run individual services for debugging:

```bash
# Run only the web frontend
dotnet run --project src/ShynvTech.Web

# Run only the magazine API
dotnet run --project src/ShynvTech.Magazine.Api

# Run only the events API
dotnet run --project src/ShynvTech.Events.Api
```

### Hot Reload

The application supports hot reload for both frontend and backend changes:

- **Blazor pages**: Automatically refresh on save
- **API controllers**: Restart automatically
- **CSS/JS**: Updates immediately

### Debugging

#### Visual Studio 2025

1. Open `ShynvTech.sln`
2. Set `ShynvTech.AppHost` as startup project
3. Press F5 to start debugging

#### Visual Studio Code

1. Open the project folder
2. Use the built-in terminal: `dotnet run --project src/ShynvTech.AppHost`
3. Attach debugger as needed

## Project Structure Overview

```
shynvtech/
├── docs/                          # Documentation
├── src/                           # Source code
│   ├── ShynvTech.AppHost/        # Aspire orchestration
│   ├── ShynvTech.Web/            # Frontend (Blazor)
│   ├── ShynvTech.Magazine.Api/   # Magazine service
│   ├── ShynvTech.Events.Api/     # Events service
│   ├── ShynvTech.Lms.Api/        # LMS service
│   ├── ShynvTech.Content.Api/    # Content service
│   ├── ShynvTech.ApiService/     # General API service
│   └── ShynvTech.ServiceDefaults/ # Shared configurations
├── Directory.Build.props          # Global build settings
├── Directory.Packages.props       # Centralized package versions
└── ShynvTech.sln                 # Solution file
```

## Common Tasks

### Adding a New Service

1. Create new Web API project:

   ```bash
   dotnet new webapi -n ShynvTech.NewService.Api -o src/ShynvTech.NewService.Api
   ```

2. Add to solution:

   ```bash
   dotnet sln add src/ShynvTech.NewService.Api
   ```

3. Add ServiceDefaults reference:

   ```bash
   cd src/ShynvTech.NewService.Api
   dotnet add reference ../ShynvTech.ServiceDefaults
   ```

4. Register in AppHost (see [Development Guide](development-guide.md))

### Adding Packages

Update `Directory.Packages.props` to add new packages:

```xml
<PackageVersion Include="NewPackage" Version="1.0.0" />
```

Then reference in project files without version:

```xml
<PackageReference Include="NewPackage" />
```

## Troubleshooting

### Common Issues

**Build Errors**

- Ensure .NET 9 SDK is installed
- Run `dotnet clean` followed by `dotnet restore`
- Check for conflicting package versions

**Port Conflicts**

- Aspire automatically assigns ports
- Check the dashboard for actual port assignments
- Close other applications using common ports

**Service Discovery Issues**

- Ensure all services reference ServiceDefaults
- Check service names in AppHost configuration
- Verify services are registered correctly

### Getting Help

1. Check the [Development Guide](development-guide.md) for detailed instructions
2. Review [Architecture Documentation](architecture.md) for system overview
3. Consult [API Documentation](api-documentation.md) for service details

## Next Steps

1. **Explore the Landing Page**: Visit the web frontend to see the beautiful landing page
2. **Check the Dashboard**: Monitor all services in the Aspire dashboard
3. **Review the Code**: Start with `src/ShynvTech.Web/Components/Pages/Home.razor`
4. **Add Features**: Begin implementing specific functionality for each service

Happy coding! 🚀
