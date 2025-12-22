using MediatR;
using ProductManagement.Application.Products.Abstractions.Repositories;
using ProductManagement.Application.Products.Queries.DTOs;

namespace ProductManagement.Application.Products.Queries.GetProductById;

public record GetProductByIdQuery(Guid Id) : IRequest<ProductDTO>;

public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ProductDTO?>
{
    private readonly IProductRepository _productRepository;
    
    public GetProductByIdHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductDTO?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.FindByIdAsync(request.Id, cancellationToken);
        
        return ProductDTOMapper.FromEntity(product);
    }
}