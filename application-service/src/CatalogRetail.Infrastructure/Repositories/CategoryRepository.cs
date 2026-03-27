using CatalogRetail.Domain.Entities;
using CatalogRetail.Domain.Interfaces;
using CatalogRetail.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CatalogRetail.Infrastructure.Repositories;

public class CategoryRepository(AppDbContext context) : Repository<Category>(context), ICategoryRepository
{
    public async Task<Category?> GetByNameAsync(string name) =>
        await DbSet.FirstOrDefaultAsync(c => c.Name == name);
}
