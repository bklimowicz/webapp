using Microsoft.EntityFrameworkCore;
using WebApp.Application.Contracts;
using WebApp.Domain;
using WebApp.Infrastructure;
using WebApp.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<WebAppDbContext>();
    await DbInitializer.InitializeAsync(dbContext);
}

app.UseHttpsRedirection();

app.MapGet("/items", async (WebAppDbContext dbContext) =>
{
    var items = await dbContext.HouseholdItems
        .AsNoTracking()
        .Select(item => new HouseholdItemResponse(item.Id, item.Name, item.Location, item.Quantity))
        .ToListAsync();

    return Results.Ok(items);
});

app.MapPost("/items", async (CreateHouseholdItemRequest request, WebAppDbContext dbContext) =>
{
    var item = new HouseholdItem
    {
        Name = request.Name,
        Location = request.Location,
        Quantity = request.Quantity ?? 1
    };

    dbContext.HouseholdItems.Add(item);
    await dbContext.SaveChangesAsync();

    var response = new HouseholdItemResponse(item.Id, item.Name, item.Location, item.Quantity);
    return Results.Created($"/items/{item.Id}", response);
});

app.Run();
