using MediatR;

namespace Application.Portfolios.Add
{
    public sealed record AddToPortfolioCommand(string UserId, string Symbol) : IRequest;
}
