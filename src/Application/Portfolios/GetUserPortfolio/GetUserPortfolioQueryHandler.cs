using Application.Stokes;
using Domain.Interfaces;
using MediatR;

namespace Application.Portfolios.GetUserPortfolio
{
    public sealed class GetUserPortfolioQueryHandler(IPortfolioRepository portfolioRepository)
        : IRequestHandler<GetUserPortfolioQuery, List<StokeDto>>
    {
        public async Task<List<StokeDto>> Handle(GetUserPortfolioQuery request, CancellationToken ct)
        {
            var stokes = await portfolioRepository.GetUserStokesAsync(request.UserId, ct);
            return stokes.Select(s => s.ToStokeDto()).ToList();
        }
    }
}
