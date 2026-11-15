using Application.Abstractions;
using Application.Common.Exceptions;
using Domain.Interfaces;
using MediatR;

namespace Application.Comments.Delete
{
    public sealed class DeleteCommentCommandHandler(
        ICommentRepository commentRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<DeleteCommentCommand>
    {
        public async Task Handle(DeleteCommentCommand request, CancellationToken ct)
        {
            var comment = await commentRepository.GetByIdAsync(request.CommentId, ct)
                ?? throw new NotFoundException($"Comment with id {request.CommentId} was not found.");

            // Business rule: only the author may delete a comment.
            if (comment.AppUserId != request.UserId)
            {
                throw new ForbiddenException("You can only delete your own comments.");
            }

            commentRepository.Remove(comment);
            await unitOfWork.SaveChangesAsync(ct);
        }
    }
}
