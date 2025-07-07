namespace appweb_back.sensors___alerts.Interfaces.DTOs;

public class AlertRequest
{
    public string AlertType { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string Severity { get; set; } = string.Empty;
    public DateTime Date { get; set; }
}