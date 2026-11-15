using Application.Abstractions;
using Application.Common.Exceptions;
using Domain.Interfaces;
using MediatR;

namespace Application.Portfolios.Remove
{
    public sealed class RemoveFromPortfolioCommandHandler(
        IStokeRepository stokeRepository,
        IPortfolioRepository portfolioRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<RemoveFromPortfolioCommand>
    {
        public async Task Handle(RemoveFromPortfolioCommand request, CancellationToken ct)
        {
            var stoke = await stokeRepository.GetBySymbolAsync(request.Symbol, ct)
                ?? throw new NotFoundException($"Stock '{request.Symbol}' was not found.");

            var portfolio = await portfolioRepository.GetAsync(request.UserId, stoke.StokeId, ct)
                ?? throw new NotFoundException($"Stock '{stoke.Symbol}' is not in your portfolio.");

            portfolioRepository.Remove(portfolio);
            await unitOfWork.SaveChangesAsync(ct);
        }
    }
}
