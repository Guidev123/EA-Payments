using Payments.API.Configurations;
using Payments.API.Endpoints;
using SharedLib.Tokens.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddJwtConfiguration(builder.Configuration);
builder.Services.AddAuthorization();
builder.AddModules();

builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerConfig();

var app = builder.Build();
app.UseAuthConfiguration();
app.UseSwaggerConfig();
app.MapEndpoints();

app.Run();