using Application.Accounts;
using Application.Accounts.Login;
using Application.Accounts.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Contracts.Accounts;

namespace WebAPI.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController(IMediator mediator) : ControllerBase
    {
        [HttpPost("register")]
        [ProducesResponseType(typeof(NewUserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken ct)
        {
            var user = await mediator.Send(
                new RegisterCommand(request.UserName, request.Email, request.Password), ct);
            return Ok(user);
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(NewUserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken ct)
        {
            var user = await mediator.Send(new LoginCommand(request.UserName, request.Password), ct);
            return Ok(user);
        }
    }
}
