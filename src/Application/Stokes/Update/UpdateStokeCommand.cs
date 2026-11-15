using MediatR;

namespace Application.Stokes.Update
{
    public sealed class UpdateStokeCommand : IRequest<StokeDto>
    {
        public int StokeId { get; init; }
        public string Symbol { get; init; } = string.Empty;
        public string CompanyName { get; init; } = string.Empty;
        public decimal Purchase { get; init; }
        public decimal LastDiv { get; init; }
        public string Industry { get; init; } = string.Empty;
        public long MarketCap { get; init; }
    }
}
