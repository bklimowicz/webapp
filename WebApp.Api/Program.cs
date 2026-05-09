using WebApp.Api.Endpoints;
using WebApp.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

await app.Services.InitializeDatabaseAsync();

app.UseHttpsRedirection();

app.MapItemEndpoints();

app.Run();

