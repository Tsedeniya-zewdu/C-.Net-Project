using MediatR;

namespace Application.Comments.GetById
{
    public sealed record GetCommentByIdQuery(int CommentId) : IRequest<CommentDto>;
}
