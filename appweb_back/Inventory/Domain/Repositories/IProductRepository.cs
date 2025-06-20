using appweb_back.Inventory.Domain.Model.Entities;

namespace appweb_back.Inventory.Domain.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> ListAsync();
    Task AddAsync(Product product);
}
