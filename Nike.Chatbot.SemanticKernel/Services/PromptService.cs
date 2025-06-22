namespace Nike.Chatbot.SemanticKernel.Services;

public class PromptService : IPromptService
{
    private readonly string _path;
    
    public PromptService()
    {
        
    }
    
    public async Task<string> LoadPromptAsync(string promptName)
    {
        throw new NotImplementedException();
    }

    public async Task<string> RenderPromptAsync(string promptName, object? parameters = null)
    {
        throw new NotImplementedException();
    }
}