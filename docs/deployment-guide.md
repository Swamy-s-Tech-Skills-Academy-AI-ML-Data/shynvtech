# Deployment Guide

This guide covers deployment strategies and instructions for the ShynvTech platform.

## Deployment Overview

The ShynvTech platform supports multiple deployment targets:

- **Local Development** - For development and testing
- **Azure Container Apps** - Recommended for production (cloud-native)
- **Azure App Service** - Alternative cloud deployment
- **Docker Containers** - On-premises or cloud containers
- **Kubernetes** - For advanced orchestration needs

## Prerequisites

### General Requirements

- .NET 9 SDK
- Azure CLI (for Azure deployments)
- Docker Desktop (for containerized deployments)
- PowerShell 7+ or Bash

### Azure Requirements

- Active Azure subscription
- Resource group for the deployment
- Container Registry (for container deployments)
- Application Insights (recommended for monitoring)

## Local Development Deployment

### Quick Start

```bash
# Clone the repository
git clone https://github.com/your-org/shynvtech.git
cd shynvtech

# Restore packages
dotnet restore

# Run the application
dotnet run --project src/ShynvTech.AppHost
```

### Environment Configuration

Create environment-specific configuration files:

**appsettings.Development.json** (already exists)
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ShynvTechDev;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```

## Azure Deployment

### Azure Container Apps (Recommended)

Azure Container Apps is the recommended deployment target for the ShynvTech platform.

#### 1. Prepare for Deployment

```bash
# Login to Azure
az login

# Set subscription
az account set --subscription "your-subscription-id"

# Create resource group
az group create --name rg-shynvtech-prod --location eastus2
```

#### 2. Create Infrastructure

Create **infra/main.bicep**:
```bicep
@description('Primary location for all resources')
param location string = resourceGroup().location

@description('Name of the Container Apps environment')
param containerAppsEnvironmentName string = 'cae-shynvtech'

@description('Name of the Container Registry')
param containerRegistryName string = 'crshynvtech${uniqueString(resourceGroup().id)}'

@description('Log Analytics workspace name')
param logAnalyticsWorkspaceName string = 'log-shynvtech'

// Log Analytics Workspace
resource logAnalytics 'Microsoft.OperationalInsights/workspaces@2023-09-01' = {
  name: logAnalyticsWorkspaceName
  location: location
  properties: {
    sku: {
      name: 'PerGB2018'
    }
    retentionInDays: 30
  }
}

// Container Registry
resource containerRegistry 'Microsoft.ContainerRegistry/registries@2023-01-01-preview' = {
  name: containerRegistryName
  location: location
  sku: {
    name: 'Basic'
  }
  properties: {
    adminUserEnabled: true
  }
}

// Container Apps Environment
resource containerAppsEnvironment 'Microsoft.App/managedEnvironments@2023-05-01' = {
  name: containerAppsEnvironmentName
  location: location
  properties: {
    appLogsConfiguration: {
      destination: 'log-analytics'
      logAnalyticsConfiguration: {
        customerId: logAnalytics.properties.customerId
        sharedKey: logAnalytics.listKeys().primarySharedKey
      }
    }
  }
}

// Web App Container
resource webApp 'Microsoft.App/containerApps@2023-05-01' = {
  name: 'ca-shynvtech-web'
  location: location
  properties: {
    managedEnvironmentId: containerAppsEnvironment.id
    configuration: {
      ingress: {
        external: true
        targetPort: 8080
        allowInsecure: false
        traffic: [
          {
            weight: 100
            latestRevision: true
          }
        ]
      }
    }
    template: {
      containers: [
        {
          name: 'shynvtech-web'
          image: '${containerRegistry.properties.loginServer}/shynvtech/web:latest'
          resources: {
            cpu: json('0.5')
            memory: '1Gi'
          }
          env: [
            {
              name: 'ASPNETCORE_ENVIRONMENT'
              value: 'Production'
            }
          ]
        }
      ]
      scale: {
        minReplicas: 1
        maxReplicas: 10
      }
    }
  }
}

// Output the web app URL
output webAppUrl string = 'https://${webApp.properties.configuration.ingress.fqdn}'
output containerRegistryLoginServer string = containerRegistry.properties.loginServer
```

#### 3. Deploy Infrastructure

```bash
# Deploy the infrastructure
az deployment group create \
  --resource-group rg-shynvtech-prod \
  --template-file infra/main.bicep \
  --parameters containerAppsEnvironmentName=cae-shynvtech-prod
```

#### 4. Build and Push Container Images

Create **Dockerfile** for each service:

**src/ShynvTech.Web/Dockerfile**:
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["Directory.Build.props", "."]
COPY ["Directory.Packages.props", "."]
COPY ["src/ShynvTech.Web/ShynvTech.Web.csproj", "src/ShynvTech.Web/"]
COPY ["src/ShynvTech.ServiceDefaults/ShynvTech.ServiceDefaults.csproj", "src/ShynvTech.ServiceDefaults/"]
RUN dotnet restore "src/ShynvTech.Web/ShynvTech.Web.csproj"
COPY . .
WORKDIR "/src/src/ShynvTech.Web"
RUN dotnet build "ShynvTech.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ShynvTech.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShynvTech.Web.dll"]
```

Build and push images:
```bash
# Get the container registry login server
ACR_LOGIN_SERVER=$(az acr show --name crshynvtech --resource-group rg-shynvtech-prod --query loginServer --output tsv)

# Login to ACR
az acr login --name crshynvtech

# Build and push Web app
docker build -t $ACR_LOGIN_SERVER/shynvtech/web:latest -f src/ShynvTech.Web/Dockerfile .
docker push $ACR_LOGIN_SERVER/shynvtech/web:latest

# Build and push API services
docker build -t $ACR_LOGIN_SERVER/shynvtech/magazine-api:latest -f src/ShynvTech.Magazine.Api/Dockerfile .
docker push $ACR_LOGIN_SERVER/shynvtech/magazine-api:latest
```

### Azure App Service Deployment

Alternative deployment using Azure App Service:

```bash
# Create App Service Plan
az appservice plan create \
  --name plan-shynvtech-prod \
  --resource-group rg-shynvtech-prod \
  --sku B1 \
  --is-linux

# Create Web App
az webapp create \
  --resource-group rg-shynvtech-prod \
  --plan plan-shynvtech-prod \
  --name app-shynvtech-web-prod \
  --runtime "DOTNETCORE:9.0"

# Deploy from local folder
dotnet publish src/ShynvTech.Web -c Release -o ./publish
cd publish
zip -r ../deploy.zip .
cd ..

az webapp deploy \
  --resource-group rg-shynvtech-prod \
  --name app-shynvtech-web-prod \
  --src-path deploy.zip \
  --type zip
```

## GitHub Actions CI/CD

### Setup GitHub Actions

Create **.github/workflows/deploy.yml**:

```yaml
name: Deploy to Azure

on:
  push:
    branches: [ main ]
  pull_request:
    branches: [ main ]

env:
  AZURE_WEBAPP_NAME: app-shynvtech-web-prod
  AZURE_WEBAPP_PACKAGE_PATH: './publish'
  DOTNET_VERSION: '9.0.x'

jobs:
  build-and-deploy:
    runs-on: ubuntu-latest
    
    steps:
    - uses: actions/checkout@v4
    
    - name: Setup .NET
      uses: actions/setup-dotnet@v4
      with:
        dotnet-version: ${{ env.DOTNET_VERSION }}
    
    - name: Restore dependencies
      run: dotnet restore
    
    - name: Build
      run: dotnet build --no-restore --configuration Release
    
    - name: Test
      run: dotnet test --no-build --configuration Release --verbosity normal
    
    - name: Publish Web App
      run: dotnet publish src/ShynvTech.Web -c Release -o ${{ env.AZURE_WEBAPP_PACKAGE_PATH }}
    
    - name: Deploy to Azure Web App
      uses: azure/webapps-deploy@v2
      with:
        app-name: ${{ env.AZURE_WEBAPP_NAME }}
        publish-profile: ${{ secrets.AZURE_WEBAPP_PUBLISH_PROFILE }}
        package: ${{ env.AZURE_WEBAPP_PACKAGE_PATH }}
```

### Setup Secrets

Add these secrets to your GitHub repository:

- `AZURE_WEBAPP_PUBLISH_PROFILE` - Download from Azure portal
- `AZURE_SUBSCRIPTION_ID` - Your Azure subscription ID
- `AZURE_CLIENT_ID` - Service principal client ID
- `AZURE_CLIENT_SECRET` - Service principal secret
- `AZURE_TENANT_ID` - Azure tenant ID

## Docker Deployment

### Docker Compose for Local Development

Create **docker-compose.yml**:

```yaml
version: '3.8'

services:
  web:
    build:
      context: .
      dockerfile: src/ShynvTech.Web/Dockerfile
    ports:
      - "8080:8080"
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      - ASPNETCORE_URLS=http://+:8080
    depends_on:
      - magazine-api
      - events-api

  magazine-api:
    build:
      context: .
      dockerfile: src/ShynvTech.Magazine.Api/Dockerfile
    ports:
      - "8081:8080"
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      - ASPNETCORE_URLS=http://+:8080

  events-api:
    build:
      context: .
      dockerfile: src/ShynvTech.Events.Api/Dockerfile
    ports:
      - "8082:8080"
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      - ASPNETCORE_URLS=http://+:8080

  lms-api:
    build:
      context: .
      dockerfile: src/ShynvTech.Lms.Api/Dockerfile
    ports:
      - "8083:8080"
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      - ASPNETCORE_URLS=http://+:8080

  content-api:
    build:
      context: .
      dockerfile: src/ShynvTech.Content.Api/Dockerfile
    ports:
      - "8084:8080"
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      - ASPNETCORE_URLS=http://+:8080

  sqlserver:
    image: mcr.microsoft.com/mssql/server:2022-latest
    ports:
      - "1433:1433"
    environment:
      - ACCEPT_EULA=Y
      - SA_PASSWORD=YourStrong@Passw0rd
      - MSSQL_PID=Developer
    volumes:
      - sqlserver_data:/var/opt/mssql

volumes:
  sqlserver_data:
```

Run with Docker Compose:
```bash
# Build and run all services
docker-compose up --build

# Run in background
docker-compose up -d --build

# Stop all services
docker-compose down
```

## Kubernetes Deployment

### Kubernetes Manifests

Create **k8s/namespace.yaml**:
```yaml
apiVersion: v1
kind: Namespace
metadata:
  name: shynvtech
```

Create **k8s/web-deployment.yaml**:
```yaml
apiVersion: apps/v1
kind: Deployment
metadata:
  name: shynvtech-web
  namespace: shynvtech
spec:
  replicas: 3
  selector:
    matchLabels:
      app: shynvtech-web
  template:
    metadata:
      labels:
        app: shynvtech-web
    spec:
      containers:
      - name: web
        image: your-registry/shynvtech/web:latest
        ports:
        - containerPort: 8080
        env:
        - name: ASPNETCORE_ENVIRONMENT
          value: "Production"
        - name: ASPNETCORE_URLS
          value: "http://+:8080"
        resources:
          requests:
            memory: "256Mi"
            cpu: "250m"
          limits:
            memory: "512Mi"
            cpu: "500m"
---
apiVersion: v1
kind: Service
metadata:
  name: shynvtech-web-service
  namespace: shynvtech
spec:
  selector:
    app: shynvtech-web
  ports:
  - port: 80
    targetPort: 8080
  type: LoadBalancer
```

Deploy to Kubernetes:
```bash
# Apply manifests
kubectl apply -f k8s/

# Check status
kubectl get pods -n shynvtech
kubectl get services -n shynvtech
```

## Environment Configuration

### Production Configuration

Create **appsettings.Production.json**:
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    },
    "ApplicationInsights": {
      "LogLevel": {
        "Default": "Information"
      }
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Server=tcp:sql-shynvtech-prod.database.windows.net,1433;Database=ShynvTechProd;User ID=adminuser;Password={password};Encrypt=true;Connection Timeout=30;"
  },
  "ApplicationInsights": {
    "ConnectionString": "InstrumentationKey=your-app-insights-key"
  }
}
```

### Environment Variables

Key environment variables for production:

```bash
# ASP.NET Core
ASPNETCORE_ENVIRONMENT=Production
ASPNETCORE_URLS=http://+:8080

# Database
ConnectionStrings__DefaultConnection="Server=..."

# Application Insights
ApplicationInsights__ConnectionString="InstrumentationKey=..."

# Custom Settings
Magazine__StorageAccount="https://storageaccount.blob.core.windows.net"
Events__MaxAttendees=1000
```

## Monitoring and Observability

### Application Insights

Configure Application Insights in **Program.cs**:
```csharp
builder.Services.AddApplicationInsightsTelemetry();

// Add custom telemetry
builder.Services.AddSingleton<ITelemetryInitializer, CustomTelemetryInitializer>();
```

### Health Checks

Add health checks for monitoring:
```csharp
builder.Services.AddHealthChecks()
    .AddSqlServer(connectionString)
    .AddAzureBlobStorage(storageConnectionString)
    .AddApplicationInsightsPublisher();

app.MapHealthChecks("/health");
```

### Logging

Configure structured logging:
```csharp
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.ApplicationInsights(services.GetRequiredService<TelemetryConfiguration>(), TelemetryConverter.Traces));
```

## Security Considerations

### SSL/TLS Configuration

```csharp
// In production, use HTTPS only
app.UseHttpsRedirection();
app.UseHsts();

// Security headers
app.Use(async (context, next) =>
{
    context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
    context.Response.Headers.Add("X-Frame-Options", "DENY");
    context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
    context.Response.Headers.Add("Strict-Transport-Security", "max-age=31536000; includeSubDomains");
    await next();
});
```

### API Security

```csharp
// Add authentication and authorization
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["Jwt:Issuer"],
            ValidAudience = configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
        };
    });

builder.Services.AddAuthorization();
```

## Performance Optimization

### Caching

```csharp
// Add Redis cache
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = configuration.GetConnectionString("Redis");
});

// Add in-memory cache
builder.Services.AddMemoryCache();
```

### Database Optimization

```csharp
// Configure Entity Framework for performance
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(connectionString, sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure();
        sqlOptions.CommandTimeout(30);
    });
    
    // Only in development
    if (builder.Environment.IsDevelopment())
    {
        options.EnableSensitiveDataLogging();
        options.EnableDetailedErrors();
    }
});
```

## Deployment Checklist

### Pre-deployment

- [ ] All tests passing
- [ ] Code reviewed and approved
- [ ] Security scan completed
- [ ] Performance testing done
- [ ] Database migrations ready
- [ ] Configuration files updated
- [ ] SSL certificates configured
- [ ] DNS records updated

### Post-deployment

- [ ] Application health checks passing
- [ ] All services responding
- [ ] Database connectivity verified
- [ ] Monitoring dashboards working
- [ ] Log aggregation functioning
- [ ] Backup procedures tested
- [ ] Performance metrics baseline established

## Troubleshooting

### Common Issues

1. **Service Discovery Problems**
   ```bash
   # Check service registration
   kubectl get services
   # Verify DNS resolution
   nslookup service-name
   ```

2. **Database Connection Issues**
   ```bash
   # Test connection
   sqlcmd -S server -U user -P password -Q "SELECT 1"
   ```

3. **Container Image Issues**
   ```bash
   # Check image
   docker inspect image-name
   # View logs
   docker logs container-name
   ```

### Performance Issues

1. **High Memory Usage**
   - Check for memory leaks
   - Review object disposal
   - Monitor garbage collection

2. **Slow Response Times**
   - Enable Application Insights
   - Profile database queries
   - Check network latency

3. **High CPU Usage**
   - Profile application code
   - Check for infinite loops
   - Review async/await usage

---

This deployment guide will be updated as new deployment targets and strategies are added to the platform.
