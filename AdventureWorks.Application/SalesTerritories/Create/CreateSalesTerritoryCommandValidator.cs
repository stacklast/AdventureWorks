using FluentValidation;

namespace AdventureWorks.Application.SalesTerritories.Create;
public class CreateSalesTerritoryCommandValidator : AbstractValidator<CreateSalesTerritoryCommand>
{
    public CreateSalesTerritoryCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(100).WithMessage("Name must be less than 100 characters");

        RuleFor(x => x.CountryRegionCode)
            .NotEmpty().WithMessage("CountryRegionCode is required")
            .Length(2).WithMessage("CountryRegionCode must be exactly 2 characters");

        RuleFor(x => x.Group)
            .NotEmpty().WithMessage("Group is required")
            .MaximumLength(50).WithMessage("Group must be less than 50 characters");

        RuleFor(x => x.SalesYtd)
            .GreaterThanOrEqualTo(0).WithMessage("SalesYtd must be a positive value");

        RuleFor(x => x.SalesLastYear)
            .GreaterThanOrEqualTo(0).WithMessage("SalesLastYear must be a positive value");

        RuleFor(x => x.CostYtd)
            .GreaterThanOrEqualTo(0).WithMessage("CostYtd must be a positive value");

        RuleFor(x => x.CostLastYear)
            .GreaterThanOrEqualTo(0).WithMessage("CostLastYear must be a positive value");

    }
}
