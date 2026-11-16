using MediatR;

namespace Application.Comments.Update
{
    public sealed class UpdateCommentCommand : IRequest<CommentDto>
    {
        public int CommentId { get; init; }
        public string UserId { get; init; } = string.Empty;
        public string Title { get; init; } = string.Empty;
        public string Content { get; init; } = string.Empty;
    }
}
