using Application.Comments;

namespace Application.Stokes
{
    // What the API returns for a stock. Controllers never return the Stoke entity itself.
    public class StokeDto
    {
        public int StokeId { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public decimal Purchase { get; set; }
        public decimal LastDiv { get; set; }
        public string Industry { get; set; } = string.Empty;
        public long MarketCap { get; set; }
        public List<CommentDto> Comments { get; set; } = new();
    }
}
