namespace WebAPI.Contracts.Stokes
{
    // The JSON body the client sends. Contracts belong to the API layer:
    // the controller copies them into an Application command.
    public sealed class CreateStokeRequest
    {
        public string Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public decimal Purchase { get; set; }
        public decimal LastDiv { get; set; }
        public string Industry { get; set; } = string.Empty;
        public long MarketCap { get; set; }
    }
}
