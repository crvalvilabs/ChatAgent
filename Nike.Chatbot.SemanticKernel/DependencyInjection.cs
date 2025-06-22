using Microsoft.Extensions.DependencyInjection;
using Nike.Chatbot.SemanticKernel.Services;

namespace Nike.Chatbot.SemanticKernel;

public static class DependencyInjection
{
    public static IServiceCollection AddSemanticKernel(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddTransient<IMemoryService, MemoryService>();
        return services;
    }
}