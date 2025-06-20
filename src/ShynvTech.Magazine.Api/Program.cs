using ShynvTech.ServiceDefaults;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add Aspire service defaults
builder.AddServiceDefaults();

// Add services to the container
builder.Services.AddControllers();

// Add OpenAPI services (.NET 9 native OpenAPI)
builder.Services.AddOpenApi("v1", options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Info = new OpenApiInfo
        {
            Title = "ShynvTech Magazine API",
            Version = "v1",
            Description = "API for managing ShynvTech Magazine content and PDF downloads with hierarchical year/month structure",
            Contact = new OpenApiContact
            {
                Name = "ShynvTech Team",
                Email = "support@shynvtech.com"
            },
            License = new OpenApiLicense
            {
                Name = "MIT License"
            }
        };

        // Add server information
        document.Servers = new List<OpenApiServer>
        {
            new OpenApiServer { Url = "/", Description = "Current server" }
        };

        return Task.CompletedTask;
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    // Map OpenAPI endpoint for .NET 9
    app.MapOpenApi("/openapi/v1.json");
}

app.UseHttpsRedirection();

// Map controller routes
app.MapControllers();

// Map Aspire default endpoints
app.MapDefaultEndpoints();

app.Run();
