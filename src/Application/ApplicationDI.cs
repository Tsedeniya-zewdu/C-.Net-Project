using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ApplicationDI
    {
        // Registers the Application layer's services.
        // Empty for now: in Phase 2 this is where MediatR and FluentValidation get registered.
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            return services;
        }
    }
}
