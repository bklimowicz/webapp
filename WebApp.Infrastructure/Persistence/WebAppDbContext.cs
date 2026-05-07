using Microsoft.EntityFrameworkCore;
using WebApp.Domain;

namespace WebApp.Infrastructure.Persistence;

public class WebAppDbContext(DbContextOptions<WebAppDbContext> options) : DbContext(options)
{
    public DbSet<HouseholdItem> HouseholdItems => Set<HouseholdItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<HouseholdItem>(entity =>
        {
            entity.ToTable("HouseholdItems");
            entity.Property(x => x.Name).IsRequired();
            entity.Property(x => x.Location).IsRequired();
            entity.Property(x => x.Quantity).HasDefaultValue(1);
        });
    }
}
