using Microsoft.Extensions.DependencyInjection;
using Nike.Chatbot.SemanticKernel.Services;
using Nike.Chatbot.SemanticKernel.Plugins;

namespace Nike.Chatbot.SemanticKernel;

public static class DependencyInjection
{
    public static IServiceCollection AddSemanticKernel(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddTransient<IMemoryService, MemoryService>();
        services.AddTransient<IPromptService, PromptService>();

        services.AddTransient<ShoeSearchPlugin>();
        services.AddTransient<PriceSearchPlugin>();

        return services;
    }
}