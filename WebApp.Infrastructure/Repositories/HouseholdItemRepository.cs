using Microsoft.EntityFrameworkCore;
using WebApp.Application.Repositories;
using WebApp.Domain;
using WebApp.Infrastructure.Persistence;

namespace WebApp.Infrastructure.Repositories;

public class HouseholdItemRepository(WebAppDbContext dbContext) : IHouseholdItemRepository
{
    public async Task<IReadOnlyList<HouseholdItem>> GetAllAsync(CancellationToken cancellationToken = default)
        => await dbContext.HouseholdItems
            .AsNoTracking()
            .ToListAsync(cancellationToken);

    public async Task<HouseholdItem?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        => await dbContext.HouseholdItems
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<HouseholdItem> AddAsync(HouseholdItem item, CancellationToken cancellationToken = default)
    {
        dbContext.HouseholdItems.Add(item);
        await dbContext.SaveChangesAsync(cancellationToken);
        return item;
    }

    public async Task UpdateAsync(HouseholdItem item, CancellationToken cancellationToken = default)
    {
        dbContext.HouseholdItems.Update(item);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var rows = await dbContext.HouseholdItems
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync(cancellationToken);
        return rows > 0;
    }
}
