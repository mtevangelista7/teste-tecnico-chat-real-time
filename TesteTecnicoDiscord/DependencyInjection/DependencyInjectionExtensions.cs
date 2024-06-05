using TesteTecnicoDiscord.Application.Interfaces.Services;
using TesteTecnicoDiscord.Application.Services;

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
}