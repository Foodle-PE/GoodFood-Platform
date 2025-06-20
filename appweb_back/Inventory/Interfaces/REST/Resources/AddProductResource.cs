namespace appweb_back.Inventory.Interfaces.REST.Resources;

public class AddProductResource
{
    public string Name { get; set; }
    public DateTime ExpirationDate { get; set; }
    public int Quantity { get; set; }
}
