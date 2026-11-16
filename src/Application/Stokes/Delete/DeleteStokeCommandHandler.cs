using Application.Abstractions;
using Application.Common.Exceptions;
using Domain.Interfaces;
using MediatR;

namespace Application.Stokes.Delete
{
    public sealed class DeleteStokeCommandHandler(
        IStokeRepository stokeRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<DeleteStokeCommand>
    {
        public async Task Handle(DeleteStokeCommand request, CancellationToken ct)
        {
            // GetByIdAsync also loads the stock's comments, so EF can unlink them
            // (set their StokeId to null) before deleting the stock.
            // Portfolio rows are removed by the database (cascade delete).
            var stoke = await stokeRepository.GetByIdAsync(request.StokeId, ct)
                ?? throw new NotFoundException($"Stock with id {request.StokeId} was not found.");

            stokeRepository.Remove(stoke);
            await unitOfWork.SaveChangesAsync(ct);
        }
    }
}
