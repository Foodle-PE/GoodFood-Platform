using appweb_back.Inventory.Domain.Model.Commands;
using appweb_back.Inventory.Domain.Model.Aggregates;
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
            command.Name,
            Quantity.Create(command.Quantity),
            ExpirationDate.Create(command.ExpirationDate)
        );

        await _repository.AddAsync(product);
    }
}
