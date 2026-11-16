namespace Application.Abstractions
{
    // Commits all changes staged by repositories in one database transaction.
    // Handlers call SaveChangesAsync once, at the end of the use case.
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken ct = default);
    }
}
