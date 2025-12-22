using MediatR;
using ProductManagement.Application.Products.Abstractions.Repositories;
using ProductManagement.Domain.Entities;

namespace ProductManagement.Application.Products.Commands.CreateProduct;

public record CreateProductCommand(string Name, string Description, decimal Price, int Quantity, bool Active)
    : IRequest<Guid>;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly IProductRepository _productRepository;

    public CreateProductHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product(request.Name, request.Description, request.Price, request.Quantity, request.Active);

        await _productRepository.AddAsync(product, cancellationToken);
        await _productRepository.SaveChangesAsync();

        return product.Id;
    }
}