using Application.Common.Exceptions;
using Domain.Interfaces;
using MediatR;

namespace Application.Comments.GetById
{
    public sealed class GetCommentByIdQueryHandler(ICommentRepository commentRepository)
        : IRequestHandler<GetCommentByIdQuery, CommentDto>
    {
        public async Task<CommentDto> Handle(GetCommentByIdQuery request, CancellationToken ct)
        {
            var comment = await commentRepository.GetByIdAsync(request.CommentId, ct)
                ?? throw new NotFoundException($"Comment with id {request.CommentId} was not found.");

            return comment.ToCommentDto();
        }
    }
}
