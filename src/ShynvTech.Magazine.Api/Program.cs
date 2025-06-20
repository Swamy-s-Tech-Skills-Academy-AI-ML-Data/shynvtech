using ShynvTech.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

// Add Aspire service defaults
builder.AddServiceDefaults();

// Add services to the container
builder.Services.AddControllers();

// Add OpenAPI/Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "ShynvTech Magazine API",
        Version = "v1",
        Description = "API for managing ShynvTech Magazine content and PDF downloads",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "ShynvTech Team",
            Email = "support@shynvtech.com"
        }
    });

    // Include XML comments if available
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    // Enable Swagger UI
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShynvTech Magazine API v1");
        c.RoutePrefix = "swagger"; // Swagger UI at /swagger
        c.DocumentTitle = "ShynvTech Magazine API";
        c.DefaultModelsExpandDepth(-1); // Hide models section by default
    });

    // Also map OpenAPI endpoint
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Map controller routes
app.MapControllers();

// Map Aspire default endpoints
app.MapDefaultEndpoints();

app.Run();
