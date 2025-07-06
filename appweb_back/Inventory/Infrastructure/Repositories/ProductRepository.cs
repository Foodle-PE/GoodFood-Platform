using appweb_back.Inventory.Domain.Model.Aggregates;
using appweb_back.Inventory.Domain.Repositories;
using appweb_back.Shared.Infrastructure.Persistence.EFC.Configuration; // Asegúrate que este namespace sea correcto y contenga AppDbContext
using Microsoft.EntityFrameworkCore;

namespace appweb_back.Inventory.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync(); // Guarda en la BD
        }

        public async Task<IEnumerable<Product>> ListAsync()
        {
            return await _context.Products.ToListAsync();
        }
    }
}