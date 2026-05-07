using Microsoft.EntityFrameworkCore;

namespace WebApp.Infrastructure.Persistence;

public static class DbInitializer
{
    public static async Task InitializeAsync(WebAppDbContext dbContext)
    {
        await dbContext.Database.EnsureCreatedAsync();
    }
}
