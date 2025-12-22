using MediatR;
using ProductManagement.Application.Products.Abstractions.Repositories;
using ProductManagement.Application.Products.Queries.DTOs;

namespace ProductManagement.Application.Products.Queries.GetAllProducts;

public record GetAllProductsQuery(int Skip = 0, int Take = 100) : IRequest<IEnumerable<ProductDTO>>;

public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDTO>>
{
    private readonly IProductRepository _productRepository;

    public GetAllProductsHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<ProductDTO>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        => ProductDTOMapper.FromEntities(
            await _productRepository.FindAllAsync(request.Skip, request.Take, cancellationToken));
}