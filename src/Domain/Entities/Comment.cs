namespace Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public int? StokeId { get; set; }
        public Stoke? Stoke { get; set; }

        // The user who wrote the comment. Only this user may edit or delete it.
        public string AppUserId { get; set; } = string.Empty;
        public AppUser AppUser { get; set; } = null!;
    }
}
