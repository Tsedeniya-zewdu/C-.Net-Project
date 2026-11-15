using Application.Abstractions;
using Application.Common.Exceptions;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Stokes.Create
{
    // A handler holds the business rules for exactly one use case.
    // It knows nothing about HTTP (no IActionResult) or EF Core (no DbContext),
    // only about Domain interfaces and Application abstractions.
    public sealed class CreateStokeCommandHandler(
        IStokeRepository stokeRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<CreateStokeCommand, StokeDto>
    {
        public async Task<StokeDto> Handle(CreateStokeCommand request, CancellationToken ct)
        {
            // Business rule: stock symbols are unique.
            if (await stokeRepository.SymbolExistsAsync(request.Symbol, ct: ct))
            {
                throw new ConflictException($"A stock with symbol '{request.Symbol}' already exists.");
            }

            var stoke = new Stoke
            {
                Symbol = request.Symbol,
                CompanyName = request.CompanyName,
                Purchase = request.Purchase,
                LastDiv = request.LastDiv,
                Industry = request.Industry,
                MarketCap = request.MarketCap
            };

            await stokeRepository.AddAsync(stoke, ct);
            await unitOfWork.SaveChangesAsync(ct);

            return stoke.ToStokeDto();
        }
    }
}
