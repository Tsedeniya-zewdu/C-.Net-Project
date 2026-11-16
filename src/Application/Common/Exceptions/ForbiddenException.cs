namespace Application.Common.Exceptions
{
    /// <summary>The user is logged in but not allowed to do this. Returned as 403.</summary>
    public sealed class ForbiddenException(string message) : Exception(message);
}
