using Application.Abstractions;
using Application.Common.Exceptions;
using Domain.Interfaces;
using MediatR;

namespace Application.Stokes.Update
{
    public sealed class UpdateStokeCommandHandler(
        IStokeRepository stokeRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<UpdateStokeCommand, StokeDto>
    {
        public async Task<StokeDto> Handle(UpdateStokeCommand request, CancellationToken ct)
        {
            // Instead of returning null, throw. GlobalExceptionHandler turns this into a 404.
            var stoke = await stokeRepository.GetByIdAsync(request.StokeId, ct)
                ?? throw new NotFoundException($"Stock with id {request.StokeId} was not found.");

            if (await stokeRepository.SymbolExistsAsync(request.Symbol, excludeId: request.StokeId, ct: ct))
            {
                throw new ConflictException($"A stock with symbol '{request.Symbol}' already exists.");
            }

            stoke.Symbol = request.Symbol;
            stoke.CompanyName = request.CompanyName;
            stoke.Purchase = request.Purchase;
            stoke.LastDiv = request.LastDiv;
            stoke.Industry = request.Industry;
            stoke.MarketCap = request.MarketCap;

            // EF tracks the loaded entity, so saving is all that is needed.
            await unitOfWork.SaveChangesAsync(ct);

            return stoke.ToStokeDto();
        }
    }
}
