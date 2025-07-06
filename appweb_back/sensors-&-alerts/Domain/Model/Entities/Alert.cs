using System.ComponentModel.DataAnnotations;

namespace appweb_back.sensors___alerts.Domain.Model.Entities;

public class Alert
{
    [Key]
    public int Id { get; set; }
    
    public string AlertType { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string Severity { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public bool IsClosed { get; set; } = false; // 👈 NUEVO
}