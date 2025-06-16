# Project Structure

This document provides a detailed explanation of the ShynvTech platform's project organization and architecture.

## Root Directory Structure

```
ShynvTech/
├── docs/                           # Documentation
├── src/                            # Source code
├── tests/                          # Test projects (future)
├── scripts/                        # Build and deployment scripts (future)
├── infra/                          # Infrastructure as Code (future)
├── .github/                        # GitHub workflows (future)
├── Directory.Build.props           # Global MSBuild properties
├── Directory.Packages.props        # Central package management
├── ShynvTech.sln                   # Solution file
├── README.md                       # Main readme
├── LICENSE                         # License file
└── .gitignore                      # Git ignore rules
```

## Documentation Structure (`docs/`)

```
docs/
├── README.md                       # Documentation overview
├── getting-started.md              # Quick start guide
├── architecture.md                 # System architecture
├── development-guide.md            # Development guidelines
├── api-documentation.md            # API reference
├── deployment-guide.md             # Deployment instructions
├── project-structure.md            # This file
└── images/                         # Documentation images
    └── A.txt                       # Placeholder
```

## Source Code Structure (`src/`)

### Core Services

```
src/
├── ShynvTech.AppHost/              # .NET Aspire Orchestrator
├── ShynvTech.ServiceDefaults/      # Shared service configuration
├── ShynvTech.Web/                  # Frontend Blazor application
├── ShynvTech.ApiService/           # General API service
├── ShynvTech.Magazine.Api/         # Magazine management API
├── ShynvTech.Events.Api/           # Event management API
├── ShynvTech.Lms.Api/              # Learning Management System API
└── ShynvTech.Content.Api/          # Content management API
```

### Detailed Service Structure

#### ShynvTech.AppHost

The .NET Aspire application host that orchestrates all services.

```
ShynvTech.AppHost/
├── Program.cs                      # Service registration and configuration
├── Properties/
│   └── launchSettings.json         # Launch profiles
├── appsettings.json                # App configuration
├── appsettings.Development.json    # Development overrides
└── ShynvTech.AppHost.csproj        # Project file
```

**Key Responsibilities:**

- Service discovery and registration
- Configuration management
- Health monitoring
- Development dashboard

#### ShynvTech.ServiceDefaults

Shared configuration and extensions for all services.

```
ShynvTech.ServiceDefaults/
├── Extensions.cs                   # Service extension methods
└── ShynvTech.ServiceDefaults.csproj # Project file
```

**Key Features:**

- Common logging configuration
- Health checks setup
- Service discovery configuration
- Telemetry and monitoring setup

#### ShynvTech.Web

The main frontend application built with Blazor Server.

```
ShynvTech.Web/
├── Components/                     # Blazor components
│   ├── App.razor                   # Root application component
│   ├── Routes.razor                # Routing configuration
│   ├── _Imports.razor              # Global using statements
│   ├── Layout/                     # Layout components
│   │   ├── MainLayout.razor        # Main layout
│   │   ├── NavMenu.razor           # Navigation menu
│   │   └── MainLayout.razor.css    # Layout styles
│   └── Pages/                      # Page components
│       ├── Home.razor              # Landing page
│       ├── Error.razor             # Error page
│       └── Weather.razor           # Sample page
├── Properties/
│   └── launchSettings.json         # Launch profiles
├── wwwroot/                        # Static web assets
│   ├── css/                        # Stylesheets
│   ├── js/                         # JavaScript files
│   ├── images/                     # Images
│   └── favicon.ico                 # Site icon
├── Program.cs                      # Application entry point
├── WeatherApiClient.cs             # Example API client
├── appsettings.json                # Configuration
├── appsettings.Development.json    # Development settings
└── ShynvTech.Web.csproj            # Project file
```

**Key Features:**

- Responsive web design with Tailwind CSS
- Server-side Blazor components
- API integration with backend services
- Modern UI with Google Fonts and Font Awesome

#### ShynvTech.Magazine.Api

RESTful API for magazine management functionality.

```
ShynvTech.Magazine.Api/
├── Controllers/                    # API controllers
│   └── MagazinesController.cs      # Magazine CRUD operations
├── Models/                         # Data models (future)
│   ├── Magazine.cs                 # Magazine entity
│   ├── Article.cs                  # Article entity
│   └── Category.cs                 # Category entity
├── Services/                       # Business logic (future)
│   ├── IMagazineService.cs         # Magazine service interface
│   └── MagazineService.cs          # Magazine service implementation
├── Data/                           # Data access (future)
│   ├── MagazineDbContext.cs        # EF Core context
│   └── Repositories/               # Repository pattern
├── Properties/
│   └── launchSettings.json         # Launch profiles
├── Program.cs                      # API configuration
├── appsettings.json                # Configuration
├── appsettings.Development.json    # Development settings
├── ShynvTech.Magazine.Api.csproj   # Project file
└── ShynvTech.Magazine.Api.http     # HTTP test requests
```

**Key Responsibilities:**

- Magazine CRUD operations
- Article management
- File upload and storage
- Search and filtering

#### ShynvTech.Events.Api

RESTful API for event management and registration.

```
ShynvTech.Events.Api/
├── Controllers/                    # API controllers
│   ├── EventsController.cs         # Event CRUD operations
│   └── RegistrationsController.cs  # Event registrations
├── Models/                         # Data models (future)
│   ├── Event.cs                    # Event entity
│   ├── Registration.cs             # Registration entity
│   └── Attendee.cs                 # Attendee entity
├── Services/                       # Business logic (future)
│   ├── IEventService.cs            # Event service interface
│   ├── EventService.cs             # Event service implementation
│   ├── IRegistrationService.cs     # Registration service interface
│   └── RegistrationService.cs      # Registration service implementation
├── Data/                           # Data access (future)
│   └── EventDbContext.cs           # EF Core context
├── Properties/
│   └── launchSettings.json         # Launch profiles
├── Program.cs                      # API configuration
├── appsettings.json                # Configuration
├── appsettings.Development.json    # Development settings
├── ShynvTech.Events.Api.csproj     # Project file
└── ShynvTech.Events.Api.http       # HTTP test requests
```

**Key Responsibilities:**

- Event creation and management
- Registration handling
- Capacity management
- Notification system

#### ShynvTech.Lms.Api

RESTful API for Learning Management System functionality.

```
ShynvTech.Lms.Api/
├── Controllers/                    # API controllers
│   ├── CoursesController.cs        # Course management
│   ├── EnrollmentsController.cs    # Student enrollments
│   ├── AssignmentsController.cs    # Assignment management
│   └── GradesController.cs         # Grade management
├── Models/                         # Data models (future)
│   ├── Course.cs                   # Course entity
│   ├── Module.cs                   # Course module entity
│   ├── Assignment.cs               # Assignment entity
│   ├── Submission.cs               # Assignment submission
│   ├── Enrollment.cs               # Student enrollment
│   └── Grade.cs                    # Grade entity
├── Services/                       # Business logic (future)
│   ├── ICourseService.cs           # Course service interface
│   ├── CourseService.cs            # Course service implementation
│   ├── IEnrollmentService.cs       # Enrollment service interface
│   └── EnrollmentService.cs        # Enrollment service implementation
├── Data/                           # Data access (future)
│   └── LmsDbContext.cs             # EF Core context
├── Properties/
│   └── launchSettings.json         # Launch profiles
├── Program.cs                      # API configuration
├── appsettings.json                # Configuration
├── appsettings.Development.json    # Development settings
├── ShynvTech.Lms.Api.csproj        # Project file
└── ShynvTech.Lms.Api.http          # HTTP test requests
```

**Key Responsibilities:**

- Course catalog management
- Student enrollment tracking
- Assignment and submission handling
- Progress tracking and grading

#### ShynvTech.Content.Api

RESTful API for content management (About Us, Contact Us, etc.).

```
ShynvTech.Content.Api/
├── Controllers/                    # API controllers
│   ├── PagesController.cs          # Static page content
│   ├── ContactController.cs        # Contact form handling
│   └── MediaController.cs          # Media file management
├── Models/                         # Data models (future)
│   ├── Page.cs                     # Page content entity
│   ├── ContactMessage.cs           # Contact form submission
│   └── MediaFile.cs                # Media file entity
├── Services/                       # Business logic (future)
│   ├── IContentService.cs          # Content service interface
│   ├── ContentService.cs           # Content service implementation
│   ├── IContactService.cs          # Contact service interface
│   └── ContactService.cs           # Contact service implementation
├── Data/                           # Data access (future)
│   └── ContentDbContext.cs         # EF Core context
├── Properties/
│   └── launchSettings.json         # Launch profiles
├── Program.cs                      # API configuration
├── appsettings.json                # Configuration
├── appsettings.Development.json    # Development settings
├── ShynvTech.Content.Api.csproj    # Project file
└── ShynvTech.Content.Api.http      # HTTP test requests
```

**Key Responsibilities:**

- Static content management
- Contact form processing
- Media file handling
- SEO content management

#### ShynvTech.ApiService

General-purpose API service for shared functionality.

```
ShynvTech.ApiService/
├── Controllers/                    # API controllers
│   ├── HealthController.cs         # Health checks
│   ├── SearchController.cs         # Global search
│   └── StatsController.cs          # Platform statistics
├── Services/                       # Business logic (future)
│   ├── ISearchService.cs           # Search service interface
│   ├── SearchService.cs            # Search service implementation
│   ├── IStatsService.cs            # Statistics service interface
│   └── StatsService.cs             # Statistics service implementation
├── Properties/
│   └── launchSettings.json         # Launch profiles
├── Program.cs                      # API configuration
├── appsettings.json                # Configuration
├── appsettings.Development.json    # Development settings
└── ShynvTech.ApiService.csproj     # Project file
```

**Key Responsibilities:**

- Cross-platform search functionality
- Platform-wide statistics
- Shared utilities and helpers
- Authentication services (future)

## Configuration Management

### Directory.Build.props

Global MSBuild properties applied to all projects:

```xml
<Project>
  <PropertyGroup>
    <TargetFramework>net9.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
    <TreatWarningsAsErrors>false</TreatWarningsAsErrors>
    <WarningsAsErrors />
    <GenerateDocumentationFile>true</GenerateDocumentationFile>
  </PropertyGroup>

  <PropertyGroup>
    <Product>ShynvTech Platform</Product>
    <Company>ShynvTech</Company>
    <Copyright>Copyright © ShynvTech 2025</Copyright>
    <AssemblyVersion>1.0.0.0</AssemblyVersion>
    <FileVersion>1.0.0.0</FileVersion>
  </PropertyGroup>
</Project>
```

### Directory.Packages.props

Central package version management:

```xml
<Project>
  <PropertyGroup>
    <ManagePackageVersionsCentrally>true</ManagePackageVersionsCentrally>
  </PropertyGroup>

  <ItemGroup>
    <PackageVersion Include="Aspire.Hosting.AppHost" Version="9.1.0" />
    <PackageVersion Include="Microsoft.AspNetCore.Components.WebAssembly.Server" Version="9.0.1" />
    <PackageVersion Include="Microsoft.Extensions.ServiceDiscovery" Version="9.1.0" />
    <!-- Additional packages... -->
  </ItemGroup>
</Project>
```

## Build and Deployment Structure

### Build Configuration

Each project contains standard .NET build artifacts:

```
ProjectName/
├── bin/                            # Compiled binaries
│   ├── Debug/                      # Debug build output
│   └── Release/                    # Release build output
└── obj/                            # Build intermediate files
    ├── project.assets.json         # NuGet restore info
    ├── project.nuget.cache          # NuGet cache
    └── Debug/                      # Debug build intermediates
```

### Launch Settings

Each service has launch profiles for different environments:

```json
{
  "profiles": {
    "http": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "applicationUrl": "http://localhost:5000",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },
    "https": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "applicationUrl": "https://localhost:7000;http://localhost:5000",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  }
}
```

## Future Structure Additions

### Testing Structure (`tests/`)

```
tests/
├── ShynvTech.Web.Tests/            # Web application tests
├── ShynvTech.Magazine.Api.Tests/   # Magazine API tests
├── ShynvTech.Events.Api.Tests/     # Events API tests
├── ShynvTech.Lms.Api.Tests/        # LMS API tests
├── ShynvTech.Content.Api.Tests/    # Content API tests
├── ShynvTech.Integration.Tests/    # Integration tests
└── ShynvTech.Performance.Tests/    # Performance tests
```

### Infrastructure Structure (`infra/`)

```
infra/
├── bicep/                          # Azure Bicep templates
│   ├── main.bicep                  # Main infrastructure
│   ├── modules/                    # Reusable modules
│   └── parameters/                 # Environment parameters
├── terraform/                      # Terraform templates (alternative)
├── kubernetes/                     # Kubernetes manifests
│   ├── deployments/                # Deployment configurations
│   ├── services/                   # Service definitions
│   └── configmaps/                 # Configuration maps
└── docker/                         # Docker configurations
    ├── docker-compose.yml          # Local development
    └── docker-compose.prod.yml     # Production setup
```

### Scripts Structure (`scripts/`)

```
scripts/
├── build.ps1                       # Build script
├── deploy.ps1                      # Deployment script
├── test.ps1                        # Test execution script
├── setup-dev.ps1                   # Development setup
└── ci/                             # CI/CD scripts
    ├── build-images.ps1            # Container image building
    └── deploy-azure.ps1            # Azure deployment
```

## File Naming Conventions

### C# Files

- **Controllers**: `{Entity}Controller.cs` (e.g., `MagazinesController.cs`)
- **Services**: `{Entity}Service.cs` and `I{Entity}Service.cs`
- **Models**: `{Entity}.cs` (e.g., `Magazine.cs`)
- **DTOs**: `{Entity}Dto.cs` or `{Action}{Entity}Request.cs`
- **Extensions**: `{Context}Extensions.cs`

### Blazor Components

- **Pages**: `{PageName}.razor` (e.g., `Home.razor`)
- **Components**: `{ComponentName}.razor` (e.g., `NavMenu.razor`)
- **Layouts**: `{LayoutName}Layout.razor` (e.g., `MainLayout.razor`)

### Configuration Files

- **Settings**: `appsettings.{Environment}.json`
- **Launch**: `launchSettings.json`
- **HTTP Tests**: `{ServiceName}.http`

## Dependencies and References

### Project Dependencies

```
ShynvTech.AppHost
├── ShynvTech.ServiceDefaults
├── ShynvTech.Web
├── ShynvTech.ApiService
├── ShynvTech.Magazine.Api
├── ShynvTech.Events.Api
├── ShynvTech.Lms.Api
└── ShynvTech.Content.Api

ShynvTech.Web
└── ShynvTech.ServiceDefaults

ShynvTech.Magazine.Api
└── ShynvTech.ServiceDefaults

ShynvTech.Events.Api
└── ShynvTech.ServiceDefaults

ShynvTech.Lms.Api
└── ShynvTech.ServiceDefaults

ShynvTech.Content.Api
└── ShynvTech.ServiceDefaults

ShynvTech.ApiService
└── ShynvTech.ServiceDefaults
```

### External Package Dependencies

Key external packages used across the platform:

- **.NET Aspire** - Distributed application framework
- **ASP.NET Core** - Web framework
- **Blazor** - Frontend framework
- **Entity Framework Core** - Data access (future)
- **Serilog** - Structured logging (future)
- **FluentValidation** - Input validation (future)
- **AutoMapper** - Object mapping (future)
- **MediatR** - CQRS pattern (future)

## Best Practices

### Code Organization

1. Group related functionality in folders
2. Use consistent naming conventions
3. Separate concerns with interfaces
4. Keep controllers thin, services thick
5. Use dependency injection consistently

### Project Structure

1. Follow single responsibility principle for projects
2. Keep shared code in ServiceDefaults
3. Use configuration over code where possible
4. Maintain clear separation between layers
5. Document architectural decisions

### File Organization

1. Group similar files in folders
2. Use descriptive file names
3. Keep file sizes reasonable
4. Organize using statements
5. Use regions sparingly and meaningfully

This project structure documentation will be updated as the platform evolves and new components are added.
