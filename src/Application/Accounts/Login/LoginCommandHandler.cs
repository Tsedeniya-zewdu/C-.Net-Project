using Application.Abstractions;
using MediatR;

namespace Application.Accounts.Login
{
    public sealed class LoginCommandHandler(IAuthService authService)
        : IRequestHandler<LoginCommand, NewUserDto>
    {
        public Task<NewUserDto> Handle(LoginCommand request, CancellationToken ct)
            => authService.LoginAsync(request.UserName, request.Password);
    }
}
