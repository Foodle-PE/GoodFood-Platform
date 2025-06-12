using appweb_back.Inventory.Domain.Model.Entities;
using appweb_back.Inventory.Domain.Model.Queries;
using appweb_back.Inventory.Domain.Repositories;

namespace appweb_back.Inventory.Application.Internal.QueryServices;

public class GetAllProductsQueryService
{
    private readonly IProductRepository _repository;

    public GetAllProductsQueryService(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Product>> HandleAsync(GetAllProductsQuery query)
    {
        return await _repository.ListAsync();
    }
}