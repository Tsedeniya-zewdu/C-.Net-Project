using MediatR;

namespace Application.Comments.GetAll
{
    public sealed record GetAllCommentsQuery : IRequest<List<CommentDto>>;
}
