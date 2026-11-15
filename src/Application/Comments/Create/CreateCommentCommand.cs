using MediatR;

namespace Application.Comments.Create
{
    public sealed class CreateCommentCommand : IRequest<CommentDto>
    {
        public int StokeId { get; init; }

        // Taken from the logged-in user's JWT by the controller, never from the request body.
        public string UserId { get; init; } = string.Empty;

        public string Title { get; init; } = string.Empty;
        public string Content { get; init; } = string.Empty;
    }
}
