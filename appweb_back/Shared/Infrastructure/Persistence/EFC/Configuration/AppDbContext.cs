using appweb_back.Inventory.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using appweb_back.Profiles.Domain.Model.Aggregates;
using appweb_back.sensors___alerts.Domain.Model.Entities;
using appweb_back.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
    
namespace appweb_back.Shared.Infrastructure.Persistence.EFC.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Alert> Alerts { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        base.OnConfiguring(builder);
        // Enable Audit Fields Interceptors
        builder.AddCreatedUpdatedInterceptor();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        
        //Profiles ContextAdd 
        builder.Entity<Profile>().HasKey(p=>p.Id);
        builder.Entity<Profile>().Property(p=>p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Profile>().OwnsOne(p => p.Name, n =>
        {
            n.WithOwner().HasForeignKey("Id");
            n.Property(p => p.FirstName).HasColumnName("FirstName");
            n.Property(p => p.LastName).HasColumnName("LastName");
        });
        
        builder.Entity<Profile>().OwnsOne(p => p.Email,
            e =>
            {
                e.WithOwner().HasForeignKey("Id");
                e.Property(a => a.Address).HasColumnName("EmailAddress");
            });

        builder.Entity<Profile>().OwnsOne(p => p.Phone,
            n =>
            {
                n.WithOwner().HasForeignKey("Id");
                n.Property(p=>p.Number).HasColumnName("PhoneNumber");
            });
        builder.Entity<Profile>().OwnsOne(p => p.Role,
            n =>
            {
                n.WithOwner().HasForeignKey("Id");
                n.Property(p=>p.Role).HasColumnName("RoleType");
            });
        builder.Entity<Product>().OwnsOne(p => p.Quantity, q =>
        {
            q.WithOwner().HasForeignKey("Id");
            q.Property(q => q.Value).HasColumnName("quantity_value");
        });

        builder.Entity<Product>().OwnsOne(p => p.ExpirationDate, d =>
        {
            d.WithOwner().HasForeignKey("Id");
            d.Property(d => d.Value).HasColumnName("expiration_date");
        });
        
        
       
        // Apply SnakeCase Naming Convention
        builder.UseSnakeCaseWithPluralizedTableNamingConvention();   
    }
}