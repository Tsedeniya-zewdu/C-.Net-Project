using Application.Accounts;

namespace Application.Abstractions
{
    // Hides ASP.NET Identity (UserManager / SignInManager) from the Application layer.
    public interface IAuthService
    {
        Task<NewUserDto> RegisterAsync(string userName, string email, string password);
        Task<NewUserDto> LoginAsync(string userName, string password);
    }
}
