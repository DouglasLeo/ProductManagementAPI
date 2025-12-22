using FluentValidation;

namespace ProductManagement.Application.Products.Queries.GetAllProducts;

public class GetAllProductsRequestValidator : AbstractValidator<GetAllProductsQuery>
{
    public GetAllProductsRequestValidator()
    {
        RuleFor(x => x.Skip)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Skip must be greater or equal to 0.");

        RuleFor(x => x.Take)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Take must be greater or equal to 1.");

        RuleFor(x => x)
            .Must(x => x.Skip + x.Take <= 1000)
            .WithMessage("The difference between Skip and Take must not exceed 1000.");
    }
}