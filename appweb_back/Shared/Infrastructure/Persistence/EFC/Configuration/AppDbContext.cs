using appweb_back.Inventory.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace appweb_back.Shared.Infrastructure.Persistence.EFC.Configuration;

public class AppDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().OwnsOne(p => p.Quantity);
        modelBuilder.Entity<Product>().OwnsOne(p => p.ExpirationDate);
    }
}