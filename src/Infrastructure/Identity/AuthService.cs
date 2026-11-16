using Application.Abstractions;
using Application.Accounts;
using Application.Common.Exceptions;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    // Implements IAuthService with ASP.NET Identity.
    // This is the only place that uses UserManager and SignInManager.
    public sealed class AuthService(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        ITokenService tokenService) : IAuthService
    {
        private const string DefaultRole = "User";

        public async Task<NewUserDto> RegisterAsync(string userName, string email, string password)
        {
            var user = new AppUser { UserName = userName, Email = email };

            // CreateAsync hashes the password and checks the password rules.
            var created = await userManager.CreateAsync(user, password);
            if (!created.Succeeded)
            {
                throw new BadRequestException(
                    "Registration failed.",
                    created.Errors.Select(e => e.Description).ToList());
            }

            var roleResult = await userManager.AddToRoleAsync(user, DefaultRole);
            if (!roleResult.Succeeded)
            {
                // Don't leave a user without a role behind.
                await userManager.DeleteAsync(user);
                throw new BadRequestException(
                    "Registration failed.",
                    roleResult.Errors.Select(e => e.Description).ToList());
            }

            return await CreateResponseAsync(user);
        }

        public async Task<NewUserDto> LoginAsync(string userName, string password)
        {
            // Same message for "unknown user" and "wrong password",
            // so attackers cannot find out which user names exist.
            const string invalidLogin = "Invalid username or password.";

            var user = await userManager.FindByNameAsync(userName)
                ?? throw new UnauthorizedException(invalidLogin);

            var result = await signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                throw new UnauthorizedException(invalidLogin);
            }

            return await CreateResponseAsync(user);
        }

        private async Task<NewUserDto> CreateResponseAsync(AppUser user)
        {
            var roles = await userManager.GetRolesAsync(user);

            return new NewUserDto
            {
                UserName = user.UserName!,
                Email = user.Email!,
                Token = tokenService.CreateToken(user, roles)
            };
        }
    }
}
