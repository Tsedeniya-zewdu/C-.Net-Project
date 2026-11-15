using Domain.Interfaces;
using MediatR;

namespace Application.Stokes.GetAll
{
    public sealed class GetAllStokesQueryHandler(IStokeRepository stokeRepository)
        : IRequestHandler<GetAllStokesQuery, List<StokeDto>>
    {
        public async Task<List<StokeDto>> Handle(GetAllStokesQuery request, CancellationToken ct)
        {
            var stokes = await stokeRepository.GetAllAsync(request.Filter, ct);
            return stokes.Select(s => s.ToStokeDto()).ToList();
        }
    }
}
