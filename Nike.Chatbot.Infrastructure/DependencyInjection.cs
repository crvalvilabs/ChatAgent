using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nike.Chatbot.Domain.Interfaces;
using Nike.Chatbot.Infrastructure.Api;

namespace Nike.Chatbot.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<ShoeApiClient>(options =>
        {
            options.BaseAddress = new Uri(configuration.GetSection("ApiSettings:BaseUrl").Value!);
            options.DefaultRequestHeaders.Add("Accept", "application/json");
        });
        
        services.AddHttpClient<PriceApiClient>(opt =>
        {
            opt.BaseAddress = new Uri(configuration.GetSection("ApiSettings:BaseUrl").Value!);
            opt.DefaultRequestHeaders.Add("Accept", "application/json");
        });
        
        services.AddHttpClient<UserApiClient>(opt =>
        {
            opt.BaseAddress = new Uri(configuration.GetSection("ApiSettings:BaseUrl").Value!);
            opt.DefaultRequestHeaders.Add("Accept", "application/json");
        });
        
        services.AddTransient<IShoeApiClient>(provider => provider.GetRequiredService<ShoeApiClient>());
        services.AddTransient<IPriceApiClient>(provider => provider.GetRequiredService<PriceApiClient>());
        services.AddTransient<IUserApiClient>(provider => provider.GetRequiredService<UserApiClient>());

        return services;
    }
}