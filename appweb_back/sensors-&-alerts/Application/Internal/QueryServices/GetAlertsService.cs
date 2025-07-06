using appweb_back.sensors___alerts.Domain.Model.Entities;
using appweb_back.sensors___alerts.Domain.Repositories;

namespace appweb_back.sensors___alerts.Application.Internal.QueryServices;

public class GetAlertsService
{
    private readonly IAlertRepository _repository;

    public GetAlertsService(IAlertRepository repository)
    {
        _repository = repository;
    }

    public Task<List<Alert>> EjecutarAsync()
    {
        return _repository.GetAllAsync();
    }
    
    public Task<Alert?> FindByIdAsync(int id) // ✅ usa el método real del repo
    {
        return _repository.GetByIdAsync(id);
    }
}