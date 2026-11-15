using Application.Abstractions;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InfrastructureDI
    {
        // Registers everything that talks to the outside world:
        // the database, ASP.NET Identity storage, repositories and JWT token creation.
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // PostgreSQL database
            services.AddDbContext<ApplicationDBContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            // ASP.NET Identity (users and roles stored in ApplicationDBContext)
            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
            })
            .AddEntityFrameworkStores<ApplicationDBContext>();

            // Repositories: Domain interface -> Infrastructure implementation
            services.AddScoped<IStokeRepository, StokeRepository>();
            services.AddScoped<IcommentRepository, CommentRepository>();
            services.AddScoped<IPortfolioRepository, PortfolioRepository>();

            // Application abstraction -> Infrastructure implementation
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}
