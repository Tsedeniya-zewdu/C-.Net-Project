using Domain.Interfaces;
using MediatR;

namespace Application.Comments.GetAll
{
    public sealed class GetAllCommentsQueryHandler(ICommentRepository commentRepository)
        : IRequestHandler<GetAllCommentsQuery, List<CommentDto>>
    {
        public async Task<List<CommentDto>> Handle(GetAllCommentsQuery request, CancellationToken ct)
        {
            var comments = await commentRepository.GetAllAsync(ct);
            return comments.Select(c => c.ToCommentDto()).ToList();
        }
    }
}
