using Domain.Entities;

namespace Application.Abstractions
{
    public interface ITokenService
    {
        string CreateToken(AppUser user, IEnumerable<string> roles);
    }
}
