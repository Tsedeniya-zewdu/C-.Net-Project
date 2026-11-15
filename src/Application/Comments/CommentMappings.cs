using Domain.Entities;

namespace Application.Comments
{
    public static class CommentMappings
    {
        // Repositories always load Comment.AppUser together with the comment,
        // so the author's user name is available here.
        public static CommentDto ToCommentDto(this Comment comment) => new()
        {
            Id = comment.Id,
            Title = comment.Title,
            Content = comment.Content,
            CreatedOn = comment.CreatedOn,
            CreatedBy = comment.AppUser?.UserName ?? string.Empty,
            StokeId = comment.StokeId
        };
    }
}
