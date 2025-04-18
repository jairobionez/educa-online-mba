
using EducaOnline.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();


builder.Services.AddApiConfig(builder.Configuration);
builder.Services.AddDependencyConfig();
builder.Services.AddIdentityConfig(builder.Configuration);

var app = builder.Build();

app.UseApiConfig(app.Environment);

app.Run();
