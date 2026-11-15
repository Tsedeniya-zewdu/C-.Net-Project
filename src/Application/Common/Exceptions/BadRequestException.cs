namespace Application.Common.Exceptions
{
    /// <summary>The request is invalid for a reason found outside FluentValidation
    /// (e.g. Identity rejected the password). Returned as 400.</summary>
    public sealed class BadRequestException(string message, IReadOnlyList<string>? errors = null) : Exception(message)
    {
        public IReadOnlyList<string> Errors { get; } = errors ?? [];
    }
}
