using MediatR;

namespace Application.Stokes.Create
{
    // A command = "please change something".
    // IRequest<StokeDto> means: the handler for this command returns a StokeDto.
    public sealed class CreateStokeCommand : IRequest<StokeDto>
    {
        public string Symbol { get; init; } = string.Empty;
        public string CompanyName { get; init; } = string.Empty;
        public decimal Purchase { get; init; }
        public decimal LastDiv { get; init; }
        public string Industry { get; init; } = string.Empty;
        public long MarketCap { get; init; }
    }
}
