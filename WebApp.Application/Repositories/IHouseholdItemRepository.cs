using WebApp.Domain;

namespace WebApp.Application.Repositories;

public interface IHouseholdItemRepository
{
    Task<IReadOnlyList<HouseholdItem>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<HouseholdItem?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<HouseholdItem> AddAsync(HouseholdItem item, CancellationToken cancellationToken = default);
    Task UpdateAsync(HouseholdItem item, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
