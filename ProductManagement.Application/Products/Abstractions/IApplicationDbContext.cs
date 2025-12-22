using Microsoft.EntityFrameworkCore;
using ProductManagement.Domain.Entities;

namespace ProductManagement.Application.Products.Abstractions;

public interface IApplicationDbContext
{
    DbSet<Product>  Products { get; set; }
}