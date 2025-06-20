namespace appweb_back.Inventory.Interfaces.REST.Resources;

public class ProductResource
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public DateTime ExpirationDate { get; set; }
}