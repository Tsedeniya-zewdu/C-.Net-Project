using Application.Stokes;
using Application.Stokes.Create;
using Application.Stokes.Delete;
using Application.Stokes.GetAll;
using Application.Stokes.GetById;
using Application.Stokes.Update;
using Domain.Filters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Contracts.Stokes;

namespace WebAPI.Controllers
{
    // Thin controller: turn the HTTP request into a command or query, send it
    // through MediatR, and turn the result into an HTTP response. Nothing else.
    // Validation errors and "not found" are handled by ValidationBehavior and
    // GlobalExceptionHandler, so there are no if/else checks here.
    [Route("api/stoke")]
    [ApiController]
    public class StokeController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(List<StokeDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] QueryObjects query, CancellationToken ct)
        {
            var stokes = await mediator.Send(new GetAllStokesQuery(query), ct);
            return Ok(stokes);
        }

        [HttpGet("{stokeId:int}")]
        [ProducesResponseType(typeof(StokeDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] int stokeId, CancellationToken ct)
        {
            var stoke = await mediator.Send(new GetStokeByIdQuery(stokeId), ct);
            return Ok(stoke);
        }

        [HttpPost]
        [ProducesResponseType(typeof(StokeDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Create([FromBody] CreateStokeRequest request, CancellationToken ct)
        {
            var command = new CreateStokeCommand
            {
                Symbol = request.Symbol,
                CompanyName = request.CompanyName,
                Purchase = request.Purchase,
                LastDiv = request.LastDiv,
                Industry = request.Industry,
                MarketCap = request.MarketCap
            };

            var stoke = await mediator.Send(command, ct);
            return CreatedAtAction(nameof(GetById), new { stokeId = stoke.StokeId }, stoke);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(StokeDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStokeRequest request, CancellationToken ct)
        {
            var command = new UpdateStokeCommand
            {
                StokeId = id,
                Symbol = request.Symbol,
                CompanyName = request.CompanyName,
                Purchase = request.Purchase,
                LastDiv = request.LastDiv,
                Industry = request.Industry,
                MarketCap = request.MarketCap
            };

            var stoke = await mediator.Send(command, ct);
            return Ok(stoke);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken ct)
        {
            await mediator.Send(new DeleteStokeCommand(id), ct);
            return NoContent();
        }
    }
}
