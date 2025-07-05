using appweb_back.Inventory.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using appweb_back.Profiles.Domain.Model.Aggregates;
using appweb_back.iam.Domain.Model.Aggregates; 
using appweb_back.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;

namespace appweb_back.Shared.Infrastructure.Persistence.EFC.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<User> Users { get; set; } 
    public DbSet<Product> Products { get; set; }// ✅ AÑADIDO

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        base.OnConfiguring(builder);
        builder.AddCreatedUpdatedInterceptor();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // === PROFILES ===
        builder.Entity<Profile>().HasKey(p => p.Id);
        builder.Entity<Profile>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();

        builder.Entity<Profile>().OwnsOne(p => p.Name, n =>
        {
            n.WithOwner().HasForeignKey("Id");
            n.Property(p => p.FirstName).HasColumnName("FirstName");
            n.Property(p => p.LastName).HasColumnName("LastName");
        });

        builder.Entity<Profile>().OwnsOne(p => p.Email, e =>
        {
            e.WithOwner().HasForeignKey("Id");
            e.Property(a => a.Address).HasColumnName("EmailAddress");
        });

        builder.Entity<Profile>().OwnsOne(p => p.Phone, n =>
        {
            n.WithOwner().HasForeignKey("Id");
            n.Property(p => p.Number).HasColumnName("PhoneNumber");
        });

        builder.Entity<Profile>().OwnsOne(p => p.Role, n =>
        {
            n.WithOwner().HasForeignKey("Id");
            n.Property(p => p.Role).HasColumnName("RoleType");
        });

        // === USERS ===
        builder.Entity<User>().HasKey(u => u.Id);
        builder.Entity<User>().Property(u => u.Id).ValueGeneratedOnAdd();
        builder.Entity<User>().Property(u => u.PasswordHash).IsRequired();
        builder.Entity<User>().Property(u => u.Username).IsRequired();
        builder.Entity<User>().Property(u => u.Role).IsRequired();
        
        builder.Entity<Product>().OwnsOne(p => p.Quantity);
        builder.Entity<Product>().OwnsOne(p => p.ExpirationDate);

        // Snake_case + pluralized table names
        builder.UseSnakeCaseWithPluralizedTableNamingConvention();
    }
}