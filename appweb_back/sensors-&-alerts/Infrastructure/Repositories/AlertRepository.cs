using appweb_back.sensors___alerts.Domain.Model.Entities;
using appweb_back.sensors___alerts.Domain.Repositories;
using appweb_back.Shared.Infrastructure.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;

namespace appweb_back.sensors___alerts.Infrastructure.Repositories;

public class AlertRepository : IAlertRepository
{
    private readonly AppDbContext _context;

    public AlertRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Alert>> GetAllAsync()
    {
        return await _context.Alerts.ToListAsync();
    }

    public async Task AddAsync(Alert alert)
    {
        await _context.Alerts.AddAsync(alert);
        await _context.SaveChangesAsync();
    }
}