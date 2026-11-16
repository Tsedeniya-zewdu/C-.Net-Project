using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(
            DbContextOptions<ApplicationDBContext> options
        ) : base(options)
        {
        }

        public DbSet<Stoke> Stoke { get; set; }
        public DbSet<Comment> Comment { get; set; }
         public DbSet<Portfolio> Portfolios { get; set; }
         protected override void OnModelCreating(ModelBuilder builder)
{
    base.OnModelCreating(builder);

    builder.Entity<Portfolio>()
        .HasKey(p => new { p.AppUserId, p.StokeId });

    builder.Entity<Portfolio>()
        .HasOne(p => p.AppUser)
        .WithMany(u => u.Portfolios)
        .HasForeignKey(p => p.AppUserId);

    builder.Entity<Portfolio>()
        .HasOne(p => p.Stoke)
        .WithMany(s => s.Portfolios)
        .HasForeignKey(p => p.StokeId);
}
    }
}