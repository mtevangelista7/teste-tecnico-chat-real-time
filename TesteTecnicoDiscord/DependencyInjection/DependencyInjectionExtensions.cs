using TesteTecnicoDiscord.Application.Interfaces.Services;
using TesteTecnicoDiscord.Application.Interfaces.Services.Generic;
using TesteTecnicoDiscord.Application.Services;
using TesteTecnicoDiscord.Infra.Interfaces;
using TesteTecnicoDiscord.Infra.Interfaces.Generic;
using TesteTecnicoDiscord.Infra.Repositories;
using TesteTecnicoDiscord.Infra.Repositories.Generic;

namespace TesteTecnicoDiscord.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddServiceCollection(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IAuthService, AuthService>();
        serviceCollection.AddScoped<IGuildsService, GuildsService>();
        serviceCollection.AddScoped<IUserService, UserService>();
        serviceCollection.AddScoped<IChannelService, ChannelService>();
        serviceCollection.AddScoped<IMessageService, MessageService>();

        return serviceCollection;
    }

    public static IServiceCollection AddRepositoriesCollection(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IUserRepository, UserRepository>();
        serviceCollection.AddScoped<IGuildsRepository, GuildsRepository>();
        serviceCollection.AddScoped<IChannelRepository, ChannelRepository>();
        serviceCollection.AddScoped<IMessageRepository, MessageRepository>();
        serviceCollection.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));
        serviceCollection.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));

        return serviceCollection;
    }
}