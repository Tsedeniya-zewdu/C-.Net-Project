using FluentValidation;

namespace Application.Stokes.Update
{
    public sealed class UpdateStokeCommandValidator : AbstractValidator<UpdateStokeCommand>
    {
        public UpdateStokeCommandValidator()
        {
            RuleFor(x => x.StokeId).GreaterThan(0);
            RuleFor(x => x.Symbol).NotEmpty().MaximumLength(10);
            RuleFor(x => x.CompanyName).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Purchase).GreaterThanOrEqualTo(0);
            RuleFor(x => x.LastDiv).InclusiveBetween(0, 100);
            RuleFor(x => x.Industry).MaximumLength(100);
            RuleFor(x => x.MarketCap).GreaterThanOrEqualTo(0);
        }
    }
}
