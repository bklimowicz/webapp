using WebApp.Application.Contracts;
using WebApp.Application.Repositories;
using WebApp.Domain;

namespace WebApp.Api.Endpoints;

public static class ItemEndpoints
{
    public static void MapItemEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/items");

        group.MapGet("/", GetAll);
        group.MapGet("/{id:int}", GetById);
        group.MapPost("/", Create);
        group.MapPut("/{id:int}", Update);
        group.MapDelete("/{id:int}", Delete);
    }

    private static async Task<IResult> GetAll(IHouseholdItemRepository repository, CancellationToken cancellationToken)
    {
        var items = await repository.GetAllAsync(cancellationToken);
        var response = items.Select(x => new HouseholdItemResponse(x.Id, x.Name, x.Location, x.Quantity));
        return Results.Ok(response);
    }

    private static async Task<IResult> GetById(int id, IHouseholdItemRepository repository, CancellationToken cancellationToken)
    {
        var item = await repository.GetByIdAsync(id, cancellationToken);
        return item is null
            ? Results.NotFound()
            : Results.Ok(new HouseholdItemResponse(item.Id, item.Name, item.Location, item.Quantity));
    }

    private static async Task<IResult> Create(CreateHouseholdItemRequest request, IHouseholdItemRepository repository, CancellationToken cancellationToken)
    {
        var errors = ValidateItemFields(request.Name, request.Location, request.Quantity);
        if (errors.Count > 0)
            return Results.ValidationProblem(errors);

        var item = new HouseholdItem
        {
            Name = request.Name.Trim(),
            Location = request.Location.Trim(),
            Quantity = request.Quantity ?? 1
        };

        await repository.AddAsync(item, cancellationToken);
        return Results.Created($"/api/items/{item.Id}", new HouseholdItemResponse(item.Id, item.Name, item.Location, item.Quantity));
    }

    private static async Task<IResult> Update(int id, UpdateHouseholdItemRequest request, IHouseholdItemRepository repository, CancellationToken cancellationToken)
    {
        var errors = ValidateItemFields(request.Name, request.Location, request.Quantity);
        if (errors.Count > 0)
            return Results.ValidationProblem(errors);

        var item = await repository.GetByIdAsync(id, cancellationToken);
        if (item is null)
            return Results.NotFound();

        item.Name = request.Name.Trim();
        item.Location = request.Location.Trim();
        item.Quantity = request.Quantity ?? 1;

        await repository.UpdateAsync(item, cancellationToken);
        return Results.Ok(new HouseholdItemResponse(item.Id, item.Name, item.Location, item.Quantity));
    }

    private static async Task<IResult> Delete(int id, IHouseholdItemRepository repository, CancellationToken cancellationToken)
    {
        var deleted = await repository.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static Dictionary<string, string[]> ValidateItemFields(string? name, string? location, int? quantity)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(name))
            errors["name"] = ["Name is required."];

        if (string.IsNullOrWhiteSpace(location))
            errors["location"] = ["Location is required."];

        if (quantity is < 1)
            errors["quantity"] = ["Quantity must be greater than 0."];

        return errors;
    }
}
