using appweb_back.Inventory.Domain.Model.Entities;
using appweb_back.Inventory.Domain.Repositories;

namespace appweb_back.Inventory.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly List<Product> _products = new();

    public Task AddAsync(Product product)
    {
        _products.Add(product);
        return Task.CompletedTask;
    }

    public Task<IEnumerable<Product>> ListAsync()
    {
        return Task.FromResult<IEnumerable<Product>>(_products);
    }
}

