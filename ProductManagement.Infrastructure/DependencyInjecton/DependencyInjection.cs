using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductManagement.Application.Products.Abstractions;
using ProductManagement.Application.Products.Abstractions.Repositories;
using ProductManagement.Infrastructure.Persistence;
using ProductManagement.Infrastructure.Persistence.Products.Repositories;

namespace ProductManagement.Infrastructure.DependencyInjecton;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"), providerOptions =>
            {
                providerOptions.MigrationsHistoryTable("__ef_migrations_history");
                providerOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
            });
        });

        services.AddScoped<IProductRepository, ProductRepository>();
    }
}