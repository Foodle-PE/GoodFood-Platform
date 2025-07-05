namespace appweb_back.Inventory.Domain.Model.Commands;

public class AddProductToInventoryCommand
{
    public string Name { get; set; }
    public DateTime ExpirationDate { get; set; }
    public int Quantity { get; set; }
}
