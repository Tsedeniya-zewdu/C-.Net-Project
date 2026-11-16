using FluentValidation;

namespace Application.Stokes.GetAll
{
    public sealed class GetAllStokesQueryValidator : AbstractValidator<GetAllStokesQuery>
    {
        private static readonly string[] SortableFields = ["Symbol", "CompanyName"];

        public GetAllStokesQueryValidator()
        {
            RuleFor(x => x.Filter.PageNumber).GreaterThanOrEqualTo(1);

            // Stops clients from asking for PageSize=100000.
            RuleFor(x => x.Filter.PageSize).InclusiveBetween(1, 100);

            RuleFor(x => x.Filter.SortBy)
                .Must(sortBy => sortBy is null
                    || SortableFields.Contains(sortBy, StringComparer.OrdinalIgnoreCase))
                .WithMessage($"SortBy must be one of: {string.Join(", ", SortableFields)}.");
        }
    }
}
