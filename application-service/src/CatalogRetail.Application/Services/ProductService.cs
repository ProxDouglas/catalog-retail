using CatalogRetail.Application.DTOs;
using CatalogRetail.Application.Interfaces;
using CatalogRetail.Domain.Entities;
using CatalogRetail.Domain.Interfaces;

namespace CatalogRetail.Application.Services;

public class ProductService(IProductRepository productRepository) : IProductService
{
    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        var products = await productRepository.GetAllAsync();
        return products.Select(MapToDto);
    }

    public async Task<ProductDto?> GetByIdAsync(int id)
    {
        var product = await productRepository.GetByIdAsync(id);
        return product is null ? null : MapToDto(product);
    }

    public async Task<IEnumerable<ProductDto>> GetByCategoryIdAsync(int categoryId)
    {
        var products = await productRepository.GetByCategoryIdAsync(categoryId);
        return products.Select(MapToDto);
    }

    public async Task<IEnumerable<ProductDto>> GetActiveProductsAsync()
    {
        var products = await productRepository.GetActiveProductsAsync();
        return products.Select(MapToDto);
    }

    public async Task<ProductDto> CreateAsync(CreateProductDto dto)
    {
        var product = new Product
        {
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            Stock = dto.Stock,
            ImageUrl = dto.ImageUrl,
            CategoryId = dto.CategoryId
        };

        var created = await productRepository.AddAsync(product);
        return MapToDto(created);
    }

    public async Task<ProductDto?> UpdateAsync(int id, UpdateProductDto dto)
    {
        var product = await productRepository.GetByIdAsync(id);
        if (product is null) return null;

        product.Name = dto.Name;
        product.Description = dto.Description;
        product.Price = dto.Price;
        product.Stock = dto.Stock;
        product.ImageUrl = dto.ImageUrl;
        product.IsActive = dto.IsActive;
        product.CategoryId = dto.CategoryId;
        product.UpdatedAt = DateTime.UtcNow;

        await productRepository.UpdateAsync(product);
        return MapToDto(product);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await productRepository.GetByIdAsync(id);
        if (product is null) return false;

        await productRepository.DeleteAsync(product);
        return true;
    }

    private static ProductDto MapToDto(Product product) => new(
        product.Id,
        product.Name,
        product.Description,
        product.Price,
        product.Stock,
        product.ImageUrl,
        product.IsActive,
        product.CategoryId,
        product.Category?.Name ?? string.Empty,
        product.CreatedAt,
        product.UpdatedAt
    );
}
