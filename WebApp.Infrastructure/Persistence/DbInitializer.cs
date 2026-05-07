using Microsoft.EntityFrameworkCore;
using WebApp.Domain;

namespace WebApp.Infrastructure.Persistence;

public static class DbInitializer
{
    public static async Task InitializeAsync(WebAppDbContext dbContext)
    {
        var hasMigrations = dbContext.Database.GetMigrations().Any();
        if (hasMigrations)
        {
            await dbContext.Database.MigrateAsync();
        }
        else
        {
            await dbContext.Database.EnsureCreatedAsync();
        }

        if (!await dbContext.HouseholdItems.AnyAsync())
        {
            dbContext.HouseholdItems.AddRange(
                new HouseholdItem { Name = "Mąka", Location = "Szafka", Quantity = 3 },
                new HouseholdItem { Name = "Powidła", Location = "Spiżarka" },
                new HouseholdItem { Name = "Mleko", Location = "Lodówka" },
                new HouseholdItem { Name = "Chleb", Location = "Zamrażarka" }
            );

            await dbContext.SaveChangesAsync();
        }
    }
}
