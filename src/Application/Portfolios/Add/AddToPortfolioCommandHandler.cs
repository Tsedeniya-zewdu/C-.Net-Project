using Application.Abstractions;
using Application.Common.Exceptions;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Portfolios.Add
{
    public sealed class AddToPortfolioCommandHandler(
        IStokeRepository stokeRepository,
        IPortfolioRepository portfolioRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<AddToPortfolioCommand>
    {
        public async Task Handle(AddToPortfolioCommand request, CancellationToken ct)
        {
            var stoke = await stokeRepository.GetBySymbolAsync(request.Symbol, ct)
                ?? throw new NotFoundException($"Stock '{request.Symbol}' was not found.");

            if (await portfolioRepository.GetAsync(request.UserId, stoke.StokeId, ct) is not null)
            {
                throw new ConflictException($"Stock '{stoke.Symbol}' is already in your portfolio.");
            }

            await portfolioRepository.AddAsync(new Portfolio
            {
                AppUserId = request.UserId,
                StokeId = stoke.StokeId
            }, ct);
            await unitOfWork.SaveChangesAsync(ct);
        }
    }
}
