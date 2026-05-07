namespace WebApp.Application.Contracts;

public record CreateHouseholdItemRequest(string Name, string Location, int? Quantity);
