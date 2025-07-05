using appweb_back.Inventory.Domain.Model.ValueObjects;

namespace appweb_back.Inventory.Domain.Model.Entities;

public class Product
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public ExpirationDate ExpirationDate { get; private set; }
    public Quantity Quantity { get; private set; }

    public Product(Guid id, string name, ExpirationDate expirationDate, Quantity quantity)
    {
        Id = id;
        Name = name;
        ExpirationDate = expirationDate;
        Quantity = quantity;
    }
}
