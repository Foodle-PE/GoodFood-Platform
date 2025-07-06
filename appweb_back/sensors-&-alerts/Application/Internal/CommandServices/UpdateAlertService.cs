using appweb_back.sensors___alerts.Domain.Model.Entities;
using appweb_back.sensors___alerts.Domain.Repositories;

namespace appweb_back.sensors___alerts.Application.Internal.CommandServices;

public class UpdateAlertService
{
    private readonly IAlertRepository _repository;

    public UpdateAlertService(IAlertRepository repository)
    {
        _repository = repository;
    }

    public Task UpdateAsync(Alert alert)
    {
        return _repository.UpdateAsync(alert);
    }
}