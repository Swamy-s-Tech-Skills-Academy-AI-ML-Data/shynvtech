var builder = DistributedApplication.CreateBuilder(args);

// Add API services
var magazineApi = builder.AddProject<Projects.ShyvnTech_Magazine_Api>("magazine-api")
    .WithExternalHttpEndpoints();

var eventsApi = builder.AddProject<Projects.ShyvnTech_Events_Api>("events-api");

var lmsApi = builder.AddProject<Projects.ShyvnTech_Lms_Api>("lms-api");

var contentApi = builder.AddProject<Projects.ShyvnTech_Content_Api>("content-api");

// Add web frontend with references to all API services
builder.AddProject<Projects.ShyvnTech_Web>("shyvntech-web")
    .WithExternalHttpEndpoints()
    .WithReference(magazineApi)
    .WithReference(eventsApi)
    .WithReference(lmsApi)
    .WithReference(contentApi);

builder.Build().Run();
