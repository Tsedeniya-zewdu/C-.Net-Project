using Application.Abstractions;
using Application.Common.Exceptions;
using Domain.Interfaces;
using MediatR;

namespace Application.Comments.Update
{
    public sealed class UpdateCommentCommandHandler(
        ICommentRepository commentRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<UpdateCommentCommand, CommentDto>
    {
        public async Task<CommentDto> Handle(UpdateCommentCommand request, CancellationToken ct)
        {
            var comment = await commentRepository.GetByIdAsync(request.CommentId, ct)
                ?? throw new NotFoundException($"Comment with id {request.CommentId} was not found.");

            // Business rule: only the author may edit a comment.
            if (comment.AppUserId != request.UserId)
            {
                throw new ForbiddenException("You can only edit your own comments.");
            }

            comment.Title = request.Title;
            comment.Content = request.Content;
            await unitOfWork.SaveChangesAsync(ct);

            return comment.ToCommentDto();
        }
    }
}
