using appweb_back.sensors___alerts.Domain.Model.Entities;
using appweb_back.sensors___alerts.Interfaces.DTOs;

namespace appweb_back.sensors___alerts.Interfaces.Assemblers;

public static class AlertAssembler
{
    public static Alert ToDomain(AlertRequest request)
    {
        return new Alert
        {
            AlertType = request.AlertType,
            Message = request.Message,
            Severity = request.Severity,
            Date = request.Date,
        };
    }
}