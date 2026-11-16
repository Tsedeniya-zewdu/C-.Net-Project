using MediatR;

namespace Application.Stokes.Delete
{
    // IRequest without a type parameter: the handler returns nothing.
    // A record is a short way to write a small immutable class.
    public sealed record DeleteStokeCommand(int StokeId) : IRequest;
}
