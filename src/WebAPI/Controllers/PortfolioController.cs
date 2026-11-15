using Application.Portfolios.Add;
using Application.Portfolios.GetUserPortfolio;
using Application.Portfolios.Remove;
using Application.Stokes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Extensions;

namespace WebAPI.Controllers
{
    // [Authorize] on the class applies to every action.
    [Route("api/portfolio")]
    [ApiController]
    [Authorize]
    public class PortfolioController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(List<StokeDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserPortfolio(CancellationToken ct)
        {
            var stokes = await mediator.Send(new GetUserPortfolioQuery(User.GetUserId()), ct);
            return Ok(stokes);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Add([FromQuery] string symbol, CancellationToken ct)
        {
            await mediator.Send(new AddToPortfolioCommand(User.GetUserId(), symbol), ct);
            return Created();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Remove([FromQuery] string symbol, CancellationToken ct)
        {
            await mediator.Send(new RemoveFromPortfolioCommand(User.GetUserId(), symbol), ct);
            return NoContent();
        }
    }
}
