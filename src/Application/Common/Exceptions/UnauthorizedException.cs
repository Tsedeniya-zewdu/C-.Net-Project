namespace Application.Common.Exceptions
{
    /// <summary>The user could not be authenticated (e.g. wrong password). Returned as 401.</summary>
    public sealed class UnauthorizedException(string message) : Exception(message);
}
