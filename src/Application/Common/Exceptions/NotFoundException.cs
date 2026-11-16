namespace Application.Common.Exceptions
{
    /// <summary>The requested resource does not exist. Returned as 404.</summary>
    public sealed class NotFoundException(string message) : Exception(message);
}
