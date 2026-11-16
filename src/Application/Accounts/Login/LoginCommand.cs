using MediatR;

namespace Application.Accounts.Login
{
    public sealed record LoginCommand(string UserName, string Password) : IRequest<NewUserDto>;
}
