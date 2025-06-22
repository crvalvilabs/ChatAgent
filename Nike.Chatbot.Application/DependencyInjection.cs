using Microsoft.Extensions.DependencyInjection;
using Nike.Chatbot.Application.Interfaces;
using Nike.Chatbot.Application.Services;

namespace Nike.Chatbot.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<INikeService, NikeService>();
        services.AddTransient<IPriceService, PriceService>();
        services.AddTransient<IUserService, UserService>();

        return services;
    }
}