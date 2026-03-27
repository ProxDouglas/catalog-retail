using CatalogRetail.Domain.Entities;
using CatalogRetail.Domain.Interfaces;
using CatalogRetail.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CatalogRetail.Infrastructure.Repositories;

public class ProductRepository(AppDbContext context) : Repository<Product>(context), IProductRepository
{
    public async Task<IEnumerable<Product>> GetByCategoryIdAsync(int categoryId) =>
        await DbSet
            .Include(p => p.Category)
            .Where(p => p.CategoryId == categoryId)
            .ToListAsync();

    public async Task<IEnumerable<Product>> GetActiveProductsAsync() =>
        await DbSet
            .Include(p => p.Category)
            .Where(p => p.IsActive)
            .ToListAsync();

    public new async Task<IEnumerable<Product>> GetAllAsync() =>
        await DbSet
            .Include(p => p.Category)
            .ToListAsync();

    public new async Task<Product?> GetByIdAsync(int id) =>
        await DbSet
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);
}
