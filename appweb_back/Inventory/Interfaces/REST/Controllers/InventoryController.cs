using appweb_back.Inventory.Application.Internal.CommandServices;
using appweb_back.Inventory.Application.Internal.QueryServices;
using appweb_back.Inventory.Domain.Model.Queries;
using appweb_back.Inventory.Interfaces.REST.Resources;
using appweb_back.Inventory.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;

namespace appweb_back.Inventory.Interfaces.REST.Controllers;

[ApiController]
[Route("api/inventory")]
public class InventoryController : ControllerBase
{
    private readonly AddProductToInventoryCommandService _commandService;
    private readonly GetAllProductsQueryService _queryService;

    public InventoryController(
        AddProductToInventoryCommandService commandService,
        GetAllProductsQueryService queryService)
    {
        _commandService = commandService;
        _queryService = queryService;
    }

    // POST: api/inventory
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AddProductResource resource)
    {
        var command = ProductMapper.ToCommand(resource);
        await _commandService.ExecuteAsync(command);
        return Ok();
    }

    // GET: api/inventory
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var query = new GetAllProductsQuery();
        var products = await _queryService.HandleAsync(query);

        var resources = products.Select(p => new ProductResource
        {
            Id = p.Id,
            Name = p.Name,
            Quantity = p.Quantity.Value,
            ExpirationDate = p.ExpirationDate.Value
        });

        return Ok(resources);
    }
}