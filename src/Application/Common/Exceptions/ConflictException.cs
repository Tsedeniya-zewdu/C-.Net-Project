namespace Application.Common.Exceptions
{
    /// <summary>The request conflicts with existing data (e.g. a duplicate). Returned as 409.</summary>
    public sealed class ConflictException(string message) : Exception(message);
}
