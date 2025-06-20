using appweb_back.Inventory.Domain.Model.Commands;
using appweb_back.Inventory.Domain.Model.Entities;
using appweb_back.Inventory.Domain.Model.ValueObjects;
using appweb_back.Inventory.Domain.Repositories;

namespace appweb_back.Inventory.Application.Internal.CommandServices;

public class AddProductToInventoryCommandService
{
    private readonly IProductRepository _repository;

    public AddProductToInventoryCommandService(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(AddProductToInventoryCommand command)
    {
        var product = new Product(
            Guid.NewGuid(),
            command.Name,
            ExpirationDate.Create(command.ExpirationDate),
            Quantity.Create(command.Quantity)
        );

        await _repository.AddAsync(product);
    }
}
