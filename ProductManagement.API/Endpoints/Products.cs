using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Application.Products.Commands.CreateProduct;
using ProductManagement.Application.Products.Commands.DeleteProduct;
using ProductManagement.Application.Products.Commands.UpdateProduct;
using ProductManagement.Application.Products.Queries.DTOs;
using ProductManagement.Application.Products.Queries.GetAllProducts;
using ProductManagement.Application.Products.Queries.GetProductById;

namespace ProductManagement.API.Endpoints;

public static class Products
{
    public static void MapProductsEndPoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/products")
            .WithTags("Products");

        group.MapGet("/", GetAllProducts).WithSummary("Get All Products")
            .Produces(200)
            .ProducesValidationProblem();

        group.MapGet("/{id:guid}", GetProductById).WithSummary("Get Product by id")
            .Produces(200)
            .Produces(404);

        group.MapPost("/", CreateProduct).WithSummary("Create product")
            .Produces<Guid>(201)
            .ProducesValidationProblem();

        group.MapPut("/{id:guid}", UpdateProduct).WithSummary("Update product")
            .Produces(204)
            .Produces(400)
            .ProducesValidationProblem();

        group.MapDelete("/{id:guid}", DeleteProduct).WithSummary("Delete product")
            .Produces(204)
            .ProducesValidationProblem()
            .Produces(404);
    }

    /// <summary>
    /// Get all products data
    /// </summary>
    /// <returns>The all products data by the query</returns>
    private static async Task<Results<Ok<IEnumerable<ProductDTO>>, ValidationProblem>> GetAllProducts(
        [AsParameters] GetAllProductsQuery query,
        ISender sender,
        IValidator<GetAllProductsQuery> validator,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(query, cancellationToken);

        if (!validationResult.IsValid) return TypedResults.ValidationProblem(validationResult.ToDictionary());

        return TypedResults.Ok(await sender.Send(query, cancellationToken));
    }

    /// <summary>
    /// Get the product data with the identifier passed from Route
    /// </summary>
    /// <param name="id">The identifier of the product</param>
    /// <returns>return the product data</returns>
    private static async Task<Results<Ok<ProductDTO>, NotFound>> GetProductById(
        [FromRoute] Guid id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetProductByIdQuery(id), cancellationToken);

        return result != null ? TypedResults.Ok(result) : TypedResults.NotFound();
    }

    /// <summary>
    /// Creates a new product.
    /// </summary>
    /// <param name="command">Product data used to create a new product.</param>
    /// <returns>
    /// Returns the identifier of the newly created product.
    /// </returns>
    private static async Task<Results<Created<Guid>, ValidationProblem>> CreateProduct(
        [FromBody] CreateProductCommand command,
        ISender sender,
        IValidator<CreateProductCommand> validator,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid) return TypedResults.ValidationProblem(validationResult.ToDictionary());

        var result = await sender.Send(command, cancellationToken);

        return TypedResults.Created($"/api/products/{result}", result);
    }

    /// <summary>
    /// Update the product
    /// </summary>
    /// <param name="id">The identifier from the product</param>
    /// <param name="command">Product data used to update the product.</param>
    /// <returns>The identifier from the updated product</returns>
    private static async Task<Results<NoContent, BadRequest, NotFound, ValidationProblem>> UpdateProduct(
        [FromRoute] Guid id,
        [FromBody] UpdateProductCommand command,
        ISender sender,
        IValidator<UpdateProductCommand> validator,
        CancellationToken cancellationToken)
    {
        if (id != command.Id) return TypedResults.BadRequest();

        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid) return TypedResults.ValidationProblem(validationResult.ToDictionary());

        var result = await sender.Send(command, cancellationToken);

        return result != Guid.Empty ? TypedResults.NoContent() : TypedResults.NotFound();
    }

    /// <summary>
    /// Delete the product
    /// </summary>
    /// <param name="id">The Identifier of the product</param>
    /// <returns>No content response</returns>
    private static async Task<Results<NoContent, NotFound, ValidationProblem>> DeleteProduct(
        [FromRoute] Guid id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new DeleteProductCommand(id), cancellationToken);

        return result != Guid.Empty ? TypedResults.NoContent() : TypedResults.NotFound();
    }
}