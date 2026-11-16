using Application.Common.Behaviors;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ApplicationDI
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(ApplicationDI).Assembly;

            // Finds every IRequestHandler in this project and registers it.
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(assembly);
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

            // Finds every AbstractValidator in this project and registers it.
            services.AddValidatorsFromAssembly(assembly);

            return services;
        }
    }
}
