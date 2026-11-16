using FluentValidation;

namespace Application.Accounts.Register
{
    public sealed class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();

            // The detailed password rules (digit, uppercase, ...) are enforced by
            // ASP.NET Identity, configured in Infrastructure/DependencyInjection.cs.
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
