using MediatR;
using ProductManagement.Application.Products.Abstractions.Repositories;

namespace ProductManagement.Application.Products.Commands.UpdateProduct;

public record UpdateProductCommand(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    int Quantity,
    bool Active) : IRequest<Guid>;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, Guid>
{
    private readonly IProductRepository _productRepository;

    public UpdateProductHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Guid> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.FindByIdAsync(request.Id, cancellationToken);

        if (product == null) return Guid.Empty;
        
        product.Update(request.Name, request.Description, request.Price, request.Quantity, request.Active);

        await _productRepository.UpdateAsync(product, cancellationToken);
        await _productRepository.SaveChangesAsync();

        return product.Id;
    }
}