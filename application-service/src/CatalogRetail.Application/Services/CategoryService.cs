using CatalogRetail.Application.DTOs;
using CatalogRetail.Application.Interfaces;
using CatalogRetail.Domain.Entities;
using CatalogRetail.Domain.Interfaces;

namespace CatalogRetail.Application.Services;

public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
{
    public async Task<IEnumerable<CategoryDto>> GetAllAsync()
    {
        var categories = await categoryRepository.GetAllAsync();
        return categories.Select(MapToDto);
    }

    public async Task<CategoryDto?> GetByIdAsync(int id)
    {
        var category = await categoryRepository.GetByIdAsync(id);
        return category is null ? null : MapToDto(category);
    }

    public async Task<CategoryDto> CreateAsync(CreateCategoryDto dto)
    {
        var category = new Category
        {
            Name = dto.Name,
            Description = dto.Description
        };

        var created = await categoryRepository.AddAsync(category);
        return MapToDto(created);
    }

    public async Task<CategoryDto?> UpdateAsync(int id, UpdateCategoryDto dto)
    {
        var category = await categoryRepository.GetByIdAsync(id);
        if (category is null) return null;

        category.Name = dto.Name;
        category.Description = dto.Description;
        category.UpdatedAt = DateTime.UtcNow;

        await categoryRepository.UpdateAsync(category);
        return MapToDto(category);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var category = await categoryRepository.GetByIdAsync(id);
        if (category is null) return false;

        await categoryRepository.DeleteAsync(category);
        return true;
    }

    private static CategoryDto MapToDto(Category category) => new(
        category.Id,
        category.Name,
        category.Description,
        category.CreatedAt,
        category.UpdatedAt
    );
}
