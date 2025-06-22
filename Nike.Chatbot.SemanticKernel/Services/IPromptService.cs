namespace Nike.Chatbot.SemanticKernel.Services;

public interface IPromptService
{
    Task<string> LoadPromptAsync(string promptName);
    
    Task<string> RenderPromptAsync(string promptName, object? parameters = null);
}