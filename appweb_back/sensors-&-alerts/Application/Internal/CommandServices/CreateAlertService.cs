using appweb_back.sensors___alerts.Domain.Model.Entities;
using appweb_back.sensors___alerts.Domain.Repositories;

namespace appweb_back.sensors___alerts.Application.Internal.CommandServices;

public class CreateAlertService
{
    private readonly IAlertRepository _repository;

    public CreateAlertService(IAlertRepository repository)
    {
        _repository = repository;
    }

    public Task EjecutarAsync(Alert alert)
    {
        return _repository.AddAsync(alert);
    }
}