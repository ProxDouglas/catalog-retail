namespace CatalogRetail.Application.DTOs;

public record CategoryDto(
    int Id,
    string Name,
    string? Description,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);

public record CreateCategoryDto(
    string Name,
    string? Description
);

public record UpdateCategoryDto(
    string Name,
    string? Description
);
