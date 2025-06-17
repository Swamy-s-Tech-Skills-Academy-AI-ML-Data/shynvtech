# Development Guide

This guide provides comprehensive information for developers working on the ShynvTech platform.

## Development Environment Setup

### IDE Configuration

#### Visual Studio 2025

1. Install the **.NET Aspire workload** during VS installation
2. Enable **Hot Reload** for faster development cycles
3. Install recommended extensions:
   - ASP.NET and web development
   - Azure development
   - Git tools

#### Visual Studio Code

1. Install the **C# Dev Kit** extension
2. Install the **.NET Extension Pack**
3. Recommended extensions:
   - C# for Visual Studio Code
   - .NET Core Test Explorer
   - Blazor WASM Debugging

### Development Workflow

#### 1. Starting the Application

```bash
# Navigate to the solution root
cd d:\STSAAIMLDT\shynvtech

# Restore packages
dotnet restore

# Run the AppHost (starts all services)
dotnet run --project src/ShynvTech.AppHost
```

#### 2. Development Workflow

1. **Make changes** to any service
2. **Hot reload** will automatically apply changes
3. **Test** using the Aspire dashboard
4. **Commit** changes with meaningful messages

#### 3. Testing Individual Services

```bash
# Test Magazine API
dotnet run --project src/ShynvTech.Magazine.Api

# Test Events API
dotnet run --project src/ShynvTech.Events.Api

# Test LMS API
dotnet run --project src/ShynvTech.Lms.Api

# Test Content API
dotnet run --project src/ShynvTech.Content.Api

# Test Web Frontend
dotnet run --project src/ShynvTech.Web
```

## Coding Standards

### C# Coding Guidelines

#### Naming Conventions

- **Classes**: PascalCase (`UserService`)
- **Methods**: PascalCase (`GetUserById`)
- **Properties**: PascalCase (`UserName`)
- **Fields**: camelCase with underscore (`_userRepository`)
- **Constants**: PascalCase (`MaxRetryCount`)
- **Interfaces**: PascalCase with 'I' prefix (`IUserService`)

#### Code Structure

```csharp
// Example service structure
namespace ShynvTech.ServiceName.Services
{
    public interface IExampleService
    {
        Task<ExampleDto> GetExampleAsync(int id);
        Task<IEnumerable<ExampleDto>> GetAllExamplesAsync();
        Task<ExampleDto> CreateExampleAsync(CreateExampleRequest request);
        Task<ExampleDto> UpdateExampleAsync(int id, UpdateExampleRequest request);
        Task DeleteExampleAsync(int id);
    }

    public class ExampleService : IExampleService
    {
        private readonly ILogger<ExampleService> _logger;
        private readonly IExampleRepository _repository;

        public ExampleService(
            ILogger<ExampleService> logger,
            IExampleRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<ExampleDto> GetExampleAsync(int id)
        {
            // Implementation
        }
    }
}
```

### API Development Guidelines

#### Controller Structure

```csharp
[ApiController]
[Route("api/[controller]")]
public class ExamplesController : ControllerBase
{
    private readonly IExampleService _exampleService;
    private readonly ILogger<ExamplesController> _logger;

    public ExamplesController(
        IExampleService exampleService,
        ILogger<ExamplesController> logger)
    {
        _exampleService = exampleService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ExampleDto>>> GetAll()
    {
        var examples = await _exampleService.GetAllExamplesAsync();
        return Ok(examples);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ExampleDto>> GetById(int id)
    {
        var example = await _exampleService.GetExampleAsync(id);
        if (example == null)
            return NotFound();

        return Ok(example);
    }

    [HttpPost]
    public async Task<ActionResult<ExampleDto>> Create(CreateExampleRequest request)
    {
        var example = await _exampleService.CreateExampleAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = example.Id }, example);
    }
}
```

#### Error Handling

```csharp
// Global error handling middleware
public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var response = exception switch
        {
            NotFoundException => (StatusCodes.Status404NotFound, "Not found"),
            ValidationException => (StatusCodes.Status400BadRequest, "Validation error"),
            _ => (StatusCodes.Status500InternalServerError, "Internal server error")
        };

        context.Response.StatusCode = response.Item1;
        await context.Response.WriteAsync(JsonSerializer.Serialize(new { error = response.Item2 }));
    }
}
```

### Frontend Development (Blazor)

#### Component Structure

```razor
@* Example Blazor component *@
@page "/examples"
@inject IExampleService ExampleService
@inject IJSRuntime JSRuntime

<PageTitle>Examples</PageTitle>

<div class="container mx-auto px-4 py-8">
    <h1 class="text-3xl font-bold mb-6">Examples</h1>

    @if (examples == null)
    {
        <div class="flex justify-center">
            <div class="animate-spin rounded-full h-32 w-32 border-b-2 border-blue-500"></div>
        </div>
    }
    else
    {
        <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
            @foreach (var example in examples)
            {
                <ExampleCard Example="example" OnDelete="HandleDelete" />
            }
        </div>
    }
</div>

@code {
    private List<ExampleDto>? examples;

    protected override async Task OnInitializedAsync()
    {
        examples = await ExampleService.GetAllExamplesAsync();
    }

    private async Task HandleDelete(int id)
    {
        await ExampleService.DeleteExampleAsync(id);
        examples = await ExampleService.GetAllExamplesAsync();
        StateHasChanged();
    }
}
```

#### CSS with Tailwind

```css
/* Custom styles in wwwroot/css/app.css */
@tailwind base;
@tailwind components;
@tailwind utilities;

@layer components {
  .btn-primary {
    @apply bg-blue-500 hover:bg-blue-600 text-white font-bold py-2 px-4 rounded transition duration-200;
  }

  .card {
    @apply bg-white rounded-lg shadow-md p-6 hover:shadow-lg transition duration-200;
  }

  .form-input {
    @apply border border-gray-300 rounded-md px-3 py-2 focus:outline-none focus:ring-2 focus:ring-blue-500;
  }
}
```

## Database Development

### Entity Framework Setup (Future)

```csharp
// Example DbContext
public class ExampleDbContext : DbContext
{
    public ExampleDbContext(DbContextOptions<ExampleDbContext> options) : base(options)
    {
    }

    public DbSet<Example> Examples { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Example>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        });
    }
}
```

### Migration Commands

```bash
# Add migration
dotnet ef migrations add InitialCreate --project src/ShynvTech.ServiceName

# Update database
dotnet ef database update --project src/ShynvTech.ServiceName

# Remove last migration
dotnet ef migrations remove --project src/ShynvTech.ServiceName
```

## Testing Guidelines

### Unit Testing

```csharp
[TestClass]
public class ExampleServiceTests
{
    private readonly Mock<IExampleRepository> _mockRepository;
    private readonly Mock<ILogger<ExampleService>> _mockLogger;
    private readonly ExampleService _service;

    public ExampleServiceTests()
    {
        _mockRepository = new Mock<IExampleRepository>();
        _mockLogger = new Mock<ILogger<ExampleService>>();
        _service = new ExampleService(_mockLogger.Object, _mockRepository.Object);
    }

    [TestMethod]
    public async Task GetExampleAsync_ValidId_ReturnsExample()
    {
        // Arrange
        var expectedExample = new ExampleDto { Id = 1, Name = "Test" };
        _mockRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(expectedExample);

        // Act
        var result = await _service.GetExampleAsync(1);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(expectedExample.Id, result.Id);
        Assert.AreEqual(expectedExample.Name, result.Name);
    }
}
```

### Integration Testing

```csharp
[TestClass]
public class ExampleControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public ExampleControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    [TestMethod]
    public async Task GetExamples_ReturnsSuccessStatusCode()
    {
        // Act
        var response = await _client.GetAsync("/api/examples");

        // Assert
        response.EnsureSuccessStatusCode();
    }
}
```

## Debugging and Troubleshooting

### Common Issues

#### 1. Service Discovery Issues

```bash
# Check if services are registered correctly
# Look at the Aspire dashboard for service status
# Verify service URLs in launchSettings.json
```

#### 2. Hot Reload Not Working

```bash
# Restart the AppHost
dotnet run --project src/ShynvTech.AppHost

# Clear bin/obj folders
dotnet clean && dotnet build
```

#### 3. Package Restore Issues

```bash
# Clear NuGet cache
dotnet nuget locals all --clear

# Restore packages
dotnet restore
```

### Logging and Monitoring

#### Structured Logging

```csharp
_logger.LogInformation("Processing request for user {UserId} at {Timestamp}",
    userId, DateTime.UtcNow);

_logger.LogError(exception, "Failed to process request for user {UserId}", userId);
```

#### Performance Monitoring

```csharp
// Using Activity for tracing
using var activity = ActivitySource.StartActivity("ExampleOperation");
activity?.SetTag("user.id", userId);
activity?.SetTag("operation.type", "get");
```

## Git Workflow

### Branch Strategy

```bash
# Main branch for production-ready code
main

# Development branch for ongoing work
develop

# Feature branches for new features
feature/magazine-crud
feature/event-registration

# Hotfix branches for urgent fixes
hotfix/login-bug
```

### Commit Messages

```bash
# Good commit messages
feat: add magazine CRUD operations
fix: resolve null reference in user service
docs: update API documentation
refactor: simplify event validation logic
test: add unit tests for LMS service
```

### Pull Request Process

1. Create feature branch from `develop`
2. Implement changes with tests
3. Update documentation if needed
4. Create pull request to `develop`
5. Request code review
6. Merge after approval

## Performance Optimization

### API Performance

- Use async/await for I/O operations
- Implement caching where appropriate
- Use pagination for large datasets
- Optimize database queries

### Frontend Performance

- Lazy load components when possible
- Optimize image sizes and formats
- Use Blazor's virtualization for large lists
- Minimize JavaScript interop calls

### Memory Management

- Dispose of resources properly
- Use object pooling for frequently created objects
- Monitor memory usage in production

## Security Best Practices

### API Security

- Validate all input data
- Use HTTPS everywhere
- Implement proper authentication/authorization
- Sanitize user inputs
- Use parameterized queries

### Frontend Security

- Validate data on both client and server
- Escape user-generated content
- Use secure cookies
- Implement CSRF protection

This development guide will be updated as the platform evolves and new practices are established.
