namespace appweb_back.sensors___alerts.Domain.Model.Commands;

public record CreateAlertCommand(string AlertType,string Message,string AlertSeverity, string Date);