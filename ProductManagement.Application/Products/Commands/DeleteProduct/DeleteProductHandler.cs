using MediatR;
using ProductManagement.Application.Products.Abstractions.Repositories;

namespace ProductManagement.Application.Products.Commands.DeleteProduct;

public record DeleteProductCommand(Guid Id) : IRequest<Guid>;

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, Guid>
{
    private readonly IProductRepository _productRepository;

    public DeleteProductHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Guid> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.FindByIdAsync(request.Id, cancellationToken);

        if (product == null) return Guid.Empty;

        await _productRepository.RemoveAsync(product, cancellationToken);
        await _productRepository.SaveChangesAsync();

        return product.Id;
    }
}