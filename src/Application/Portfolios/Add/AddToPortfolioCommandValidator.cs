using FluentValidation;

namespace Application.Portfolios.Add
{
    public sealed class AddToPortfolioCommandValidator : AbstractValidator<AddToPortfolioCommand>
    {
        public AddToPortfolioCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.Symbol).NotEmpty().MaximumLength(10);
        }
    }
}
