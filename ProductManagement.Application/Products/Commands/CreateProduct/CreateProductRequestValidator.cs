using FluentValidation;

namespace ProductManagement.Application.Products.Commands.CreateProduct;

public class CreateProductRequestValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductRequestValidator()
    {
        RuleFor(x => x.Name).MinimumLength(3)
            .WithMessage("Name minimal length is 3.")
            .MaximumLength(200).WithMessage("Name maximum length is 200.");

        RuleFor(x => x.Description).MinimumLength(3)
            .WithMessage("Description minimal length is 3.")
            .MaximumLength(2000).WithMessage("Description maximum length is 2000.");

        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than 0.");
    }
}