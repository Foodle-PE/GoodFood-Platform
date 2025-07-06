using appweb_back.Inventory.Domain.Model.ValueObjects;

namespace appweb_back.Inventory.Domain.Model.Aggregates;

public class Product
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; }
    public Quantity Quantity { get; private set; }
    public ExpirationDate ExpirationDate { get; private set; }

    // Constructor requerido por EF Core
    private Product() {}

    // Constructor usado en dominio
    public Product(string name, Quantity quantity, ExpirationDate expirationDate)
    {
        Id = Guid.NewGuid();
        Name = name;
        Quantity = quantity;
        ExpirationDate = expirationDate;
    }

    public void AddStock(Quantity quantity)
    {
        Quantity = Quantity.Add(quantity);
    }
}


