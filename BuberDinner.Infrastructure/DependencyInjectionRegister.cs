using Microsoft.Extensions.DependencyInjection;

namespace BuberDinner.Application;

public static class DependencyInjectionRegister
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        return services;
    }
}