using FluentValidation;

namespace Application.Stokes.Create
{
    // Runs automatically (through ValidationBehavior) before CreateStokeCommandHandler.
    // If a rule fails, the handler never runs and the client gets a 400 with the errors.
    public sealed class CreateStokeCommandValidator : AbstractValidator<CreateStokeCommand>
    {
        public CreateStokeCommandValidator()
        {
            RuleFor(x => x.Symbol).NotEmpty().MaximumLength(10);
            RuleFor(x => x.CompanyName).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Purchase).GreaterThanOrEqualTo(0);
            RuleFor(x => x.LastDiv).InclusiveBetween(0, 100);
            RuleFor(x => x.Industry).MaximumLength(100);
            RuleFor(x => x.MarketCap).GreaterThanOrEqualTo(0);
        }
    }
}
