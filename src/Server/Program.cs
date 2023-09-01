using Microsoft.AspNetCore.Builder;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.AddAppServices();

WebApplication app = builder.Build();

app.ConfigureAppRequestPipeline();

await app.SetupAppDatabaseAsync();

app.Run();
