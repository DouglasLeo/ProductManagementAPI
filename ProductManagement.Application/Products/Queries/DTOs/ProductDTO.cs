using ProductManagement.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace ProductManagement.Application.Products.Queries.DTOs;

public class ProductDTO
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public string Description { get; init; } = null!;
    public decimal Price { get; init; }
    public int Quantity { get; init; }
    public bool Active { get; init; }
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public static partial class ProductDTOMapper
{
    public static partial ProductDTO? FromEntity(Product? model);
    public static partial IEnumerable<ProductDTO> FromEntities(IEnumerable<Product> model);
}
