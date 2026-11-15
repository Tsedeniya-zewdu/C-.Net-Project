namespace Domain.Entities
{
    // Join table for the many-to-many relationship between users and stocks.
    public class Portfolio
    {
        public string AppUserId { get; set; } = string.Empty;
        public int StokeId { get; set; }

        public AppUser AppUser { get; set; } = null!;
        public Stoke Stoke { get; set; } = null!;
    }
}
