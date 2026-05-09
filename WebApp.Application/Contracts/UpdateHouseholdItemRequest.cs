namespace WebApp.Application.Contracts;

public record UpdateHouseholdItemRequest(string Name, string Location, int? Quantity);
