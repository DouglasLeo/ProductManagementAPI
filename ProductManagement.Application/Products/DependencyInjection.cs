using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ProductManagement.Application.Products;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services
            .AddValidatorsFromAssembly(assembly)
            .AddMediatR(x => x.RegisterServicesFromAssembly(assembly));

        return services;
    }
}