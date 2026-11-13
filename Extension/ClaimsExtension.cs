using System.Security.Claims;

namespace api.Extension
{
    public static class ClaimsExtension
    {
        public static string GetUserName(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Name)?.Value
                ?? user.FindFirst("name")?.Value
                ?? string.Empty;
        }
    }
}