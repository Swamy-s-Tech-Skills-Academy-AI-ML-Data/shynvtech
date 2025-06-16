var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.ShynvTech_ApiService>("apiservice");

builder.AddProject<Projects.ShynvTech_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService);

builder.Build().Run();
