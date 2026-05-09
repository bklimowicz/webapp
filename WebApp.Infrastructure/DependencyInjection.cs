using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Application.Repositories;
using WebApp.Infrastructure.Persistence;
using WebApp.Infrastructure.Repositories;

namespace WebApp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
                               ?? throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");

        services.AddDbContext<WebAppDbContext>(options => options.UseNpgsql(connectionString));
        services.AddScoped<IHouseholdItemRepository, HouseholdItemRepository>();

        return services;
    }

    public static async Task InitializeDatabaseAsync(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<WebAppDbContext>();
        await DbInitializer.InitializeAsync(dbContext);
    }
}
