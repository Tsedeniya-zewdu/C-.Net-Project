using Domain.Filters;
using MediatR;

namespace Application.Stokes.GetAll
{
    // A query = "please read something". Queries never change data.
    public sealed record GetAllStokesQuery(QueryObjects Filter) : IRequest<List<StokeDto>>;
}
