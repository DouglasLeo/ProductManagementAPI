using ProductManagement.Domain.Entities;

namespace ProductManagement.Application.Products.Abstractions.Repositories;

public interface IProductRepository
{
    Task<List<Product>> FindAllAsync(int skip, int take, CancellationToken cancellationToken);
    
    Task<Product?> FindByIdAsync(Guid id, CancellationToken cancellationToken);
    
    Task AddAsync(Product entity, CancellationToken cancellationToken);
    
    Task UpdateAsync(Product entity, CancellationToken cancellationToken);
    
    Task RemoveAsync(Product entity, CancellationToken cancellationToken);
    
    Task<int> SaveChangesAsync();
}