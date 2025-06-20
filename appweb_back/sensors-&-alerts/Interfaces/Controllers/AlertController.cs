using appweb_back.sensors___alerts.Application.Internal.CommandServices;
using appweb_back.sensors___alerts.Application.Internal.QueryServices;
using appweb_back.sensors___alerts.Domain.Model.Entities;
using Microsoft.AspNetCore.Mvc;

namespace appweb_back.sensors___alerts.Interfaces.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlertaController : ControllerBase
{
    private readonly CreateAlertService _createService;
    private readonly GetAlertsService _getService;

    public AlertaController(CreateAlertService createService, GetAlertsService getService)
    {
        _createService = createService;
        _getService = getService;
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
}