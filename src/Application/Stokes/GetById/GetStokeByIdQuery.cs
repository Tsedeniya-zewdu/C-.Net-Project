using MediatR;

namespace Application.Stokes.GetById
{
    public sealed record GetStokeByIdQuery(int StokeId) : IRequest<StokeDto>;
}
