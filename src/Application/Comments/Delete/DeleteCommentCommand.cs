using MediatR;

namespace Application.Comments.Delete
{
    public sealed record DeleteCommentCommand(int CommentId, string UserId) : IRequest;
}
