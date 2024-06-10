using TesteTecnicoDiscord.Application.Interfaces.Services;
using TesteTecnicoDiscord.Application.Services;
using TesteTecnicoDiscord.Infra.Interfaces;
using TesteTecnicoDiscord.Infra.Repositories;

namespace TesteTecnicoDiscord.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddServiceCollection(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IAuthService, AuthService>();
        serviceCollection.AddScoped<IGuildsService, GuildsService>();
        serviceCollection.AddScoped<IUserService, UserService>();

        return serviceCollection;
    }

    public static IServiceCollection AddRepositoriesCollection(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IUserRepository, UserRepository>();
        return serviceCollection;
    }
}