using appweb_back.sensors___alerts.Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace appweb_back.sensors___alerts.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Alert> Alerts { get; set; }
}