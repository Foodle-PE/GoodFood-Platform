using appweb_back.sensors___alerts.Application.Internal.CommandServices;
using appweb_back.sensors___alerts.Application.Internal.QueryServices;
using appweb_back.sensors___alerts.Domain.Model.Entities;
using appweb_back.sensors___alerts.Interfaces.Assemblers;
using appweb_back.sensors___alerts.Interfaces.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace appweb_back.sensors___alerts.Interfaces.Controllers;

[ApiController]
[Route("api/v1/Alerta")]
public class AlertaController : ControllerBase
{
    private readonly CreateAlertService _createService;
    private readonly GetAlertsService _getService;
    private readonly UpdateAlertService _updateService;
    private readonly ILogger<AlertaController> _logger;

    public AlertaController(CreateAlertService createService, GetAlertsService getService, UpdateAlertService updateService, ILogger<AlertaController> logger)
    {
        _createService = createService;
        _getService = getService;
        _updateService = updateService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var alerts = await _getService.EjecutarAsync();
        return Ok(alerts);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AlertRequest request)
    {
        try
        {
            var alert = AlertAssembler.ToDomain(request);
            await _createService.EjecutarAsync(alert);
            return CreatedAtAction(nameof(Get), new { alert.Id }, alert);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear alerta");
            return StatusCode(500, $"Error interno: {ex.Message}");
        }
    }
    
    [HttpPut("{id}/close")]
    public async Task<IActionResult> CloseAlert(int id)
    {
        var alert = await _getService.FindByIdAsync(id);
        if (alert == null) return NotFound();
        
        await _updateService.UpdateAsync(alert);

        return NoContent();
    }
}