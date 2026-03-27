using CatalogRetail.Domain.Entities;

namespace CatalogRetail.Domain.Interfaces;

public interface ICategoryRepository : IRepository<Category>
{
    Task<Category?> GetByNameAsync(string name);
}
