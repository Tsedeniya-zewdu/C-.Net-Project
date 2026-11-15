using Application.Common.Exceptions;
using Domain.Interfaces;
using MediatR;

namespace Application.Stokes.GetById
{
    public sealed class GetStokeByIdQueryHandler(IStokeRepository stokeRepository)
        : IRequestHandler<GetStokeByIdQuery, StokeDto>
    {
        public async Task<StokeDto> Handle(GetStokeByIdQuery request, CancellationToken ct)
        {
            var stoke = await stokeRepository.GetByIdAsync(request.StokeId, ct)
                ?? throw new NotFoundException($"Stock with id {request.StokeId} was not found.");

            return stoke.ToStokeDto();
        }
    }
}
