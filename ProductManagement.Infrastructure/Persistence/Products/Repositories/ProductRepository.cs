using Microsoft.EntityFrameworkCore;
using ProductManagement.Application.Products.Abstractions.Repositories;
using ProductManagement.Domain.Entities;

namespace ProductManagement.Infrastructure.Persistence.Products.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<Product> _products;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
        _products = context.Set<Product>();
    }

    public async Task<List<Product>> FindAllAsync(int skip, int take, CancellationToken cancellationToken) =>
        await _products.AsNoTracking().Skip(skip).Take(take).ToListAsync(cancellationToken);

    public async Task<Product?> FindByIdAsync(Guid id, CancellationToken cancellationToken) =>
        await _products.FindAsync(id, cancellationToken);

    public async Task AddAsync(Product entity, CancellationToken cancellationToken) =>
        await _products.AddAsync(entity, cancellationToken);

    public async Task UpdateAsync(Product entity, CancellationToken cancellationToken) =>
        await Task.Run(() => _products.Update(entity), cancellationToken);

    public async Task RemoveAsync(Product entity, CancellationToken cancellationToken) =>
        await Task.Run(() => _products.Remove(entity), cancellationToken);

    public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
}