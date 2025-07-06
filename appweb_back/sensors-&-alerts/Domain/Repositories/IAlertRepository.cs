using appweb_back.sensors___alerts.Domain.Model.Entities;

namespace appweb_back.sensors___alerts.Domain.Repositories;

public interface IAlertRepository
{
    Task<List<Alert>> GetAllAsync();
    Task AddAsync(Alert alert);
    Task UpdateAsync(Alert alert);
    
    Task<Alert?> GetByIdAsync(int id);
}