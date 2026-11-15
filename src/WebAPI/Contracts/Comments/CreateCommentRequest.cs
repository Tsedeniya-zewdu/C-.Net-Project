namespace WebAPI.Contracts.Comments
{
    public sealed class CreateCommentRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }
}
