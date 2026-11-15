using MediatR;

namespace Application.Portfolios.Remove
{
    public sealed record RemoveFromPortfolioCommand(string UserId, string Symbol) : IRequest;
}
