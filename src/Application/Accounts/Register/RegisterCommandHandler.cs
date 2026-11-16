using Application.Abstractions;
using MediatR;

namespace Application.Accounts.Register
{
    // Identity does the real work. The handler only depends on the IAuthService
    // abstraction, so Application never references ASP.NET Identity directly.
    public sealed class RegisterCommandHandler(IAuthService authService)
        : IRequestHandler<RegisterCommand, NewUserDto>
    {
        public Task<NewUserDto> Handle(RegisterCommand request, CancellationToken ct)
            => authService.RegisterAsync(request.UserName, request.Email, request.Password);
    }
}
