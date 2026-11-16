using Application.Comments;
using Domain.Entities;

namespace Application.Stokes
{
    public static class StokeMappings
    {
        public static StokeDto ToStokeDto(this Stoke stoke) => new()
        {
            StokeId = stoke.StokeId,
            Symbol = stoke.Symbol,
            CompanyName = stoke.CompanyName,
            Purchase = stoke.Purchase,
            LastDiv = stoke.LastDiv,
            Industry = stoke.Industry,
            MarketCap = stoke.MarketCap,
            Comments = stoke.Comments.Select(c => c.ToCommentDto()).ToList()
        };
    }
}
