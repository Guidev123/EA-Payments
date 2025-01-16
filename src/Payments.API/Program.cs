using Payments.API.Configurations;
using Payments.API.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddJwtConfiguration(builder.Configuration);
builder.AddModules();

builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerConfig();

var app = builder.Build();
app.UseSecurity();
app.MapEndpoints();

app.Run();