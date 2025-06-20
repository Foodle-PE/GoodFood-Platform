using appweb_back.Inventory.Domain.Model.Commands;
using appweb_back.Inventory.Interfaces.REST.Resources;

namespace appweb_back.Inventory.Interfaces.REST.Transform;

public static class ProductMapper
{
    public static AddProductToInventoryCommand ToCommand(AddProductResource resource)
    {
        return new AddProductToInventoryCommand
        {
            Name = resource.Name,
            ExpirationDate = resource.ExpirationDate,
            Quantity = resource.Quantity
        };
    }
}
