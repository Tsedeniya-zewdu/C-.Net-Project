using Application.Stokes;
using MediatR;

namespace Application.Portfolios.GetUserPortfolio
{
    public sealed record GetUserPortfolioQuery(string UserId) : IRequest<List<StokeDto>>;
}
