using System.Security.Claims;
using Application.Common.Exceptions;

namespace WebAPI.Extensions
{
    public static class ClaimsExtension
    {
        // Reads the user's id from the JWT ("sub" claim).
        // ASP.NET maps "sub" to ClaimTypes.NameIdentifier when it reads the token.
        public static string GetUserId(this ClaimsPrincipal user)
        {
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? user.FindFirst("sub")?.Value;

            // Tokens created before the "sub" claim was added don't have it.
            return string.IsNullOrEmpty(userId)
                ? throw new UnauthorizedException("Your token has no user id. Please log in again.")
                : userId;
        }
    }
}
