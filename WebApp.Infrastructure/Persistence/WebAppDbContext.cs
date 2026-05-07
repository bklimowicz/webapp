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

            entity.HasData(
                new HouseholdItem { Id = 1, Name = "Mąka", Location = "Szafka", Quantity = 3 },
                new HouseholdItem { Id = 2, Name = "Powidła", Location = "Spiżarka" },
                new HouseholdItem { Id = 3, Name = "Mleko", Location = "Lodówka" },
                new HouseholdItem { Id = 4, Name = "Chleb", Location = "Zamrażarka" }
            );
        });
    }
}
