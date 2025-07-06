using appweb_back.sensors___alerts.Application.Internal.CommandServices;
using appweb_back.sensors___alerts.Application.Internal.QueryServices;
using appweb_back.sensors___alerts.Domain.Model.Entities;
using Microsoft.AspNetCore.Mvc;

namespace appweb_back.sensors___alerts.Interfaces.Controllers;

[ApiController]
[Route("api/v1/Alerta")]
public class AlertaController : ControllerBase
{
    private readonly CreateAlertService _createService;
    private readonly GetAlertsService _getService;
    private readonly UpdateAlertService _updateService;

    public AlertaController(CreateAlertService createService, GetAlertsService getService, UpdateAlertService updateService)
    {
        _createService = createService;
        _getService = getService;
        _updateService = updateService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var alerts = await _getService.EjecutarAsync();
        return Ok(alerts);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Alert alert)
    {
        await _createService.EjecutarAsync(alert);
        return CreatedAtAction(nameof(Get), new { alert.Id }, alert);
    }
    
    [HttpPut("{id}/close")]
    public async Task<IActionResult> CloseAlert(int id)
    {
        var alert = await _getService.FindByIdAsync(id);
        if (alert == null) return NotFound();

        alert.IsClosed = true;
        await _updateService.UpdateAsync(alert);

        return NoContent();
    }
}