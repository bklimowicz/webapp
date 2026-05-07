namespace WebApp.Domain;

public class HouseholdItem
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;

    public int Quantity { get; set; } = 1;
}
