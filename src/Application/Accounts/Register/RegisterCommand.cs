using MediatR;

namespace Application.Accounts.Register
{
    public sealed record RegisterCommand(string UserName, string Email, string Password) : IRequest<NewUserDto>;
}
