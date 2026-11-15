using Application.Abstractions;

namespace Infrastructure.Data
{
    // All repositories in one request share the same scoped ApplicationDBContext,
    // so one SaveChangesAsync here commits everything they staged, in one transaction.
    public sealed class UnitOfWork(ApplicationDBContext context) : IUnitOfWork
    {
        public Task<int> SaveChangesAsync(CancellationToken ct = default)
            => context.SaveChangesAsync(ct);
    }
}
