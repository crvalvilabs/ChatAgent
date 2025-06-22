using HandlebarsDotNet;
using YamlDotNet.RepresentationModel;
using System.Reflection;

namespace Nike.Chatbot.SemanticKernel.Services;

/// <summary>
/// Service responsible for loading and rendering YAML prompts embedded as resources.
/// </summary>
public class PromptService : IPromptService
{
    /// <summary>
    /// Load the specified prompt from the embedded resources.
    /// </summary>
    public async Task<string> LoadPromptAsync(string promptName)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = assembly
            .GetManifestResourceNames()
            .FirstOrDefault(r => r.EndsWith($"{promptName}.yml", StringComparison.OrdinalIgnoreCase));

        if (resourceName is null)
        {
            throw new FileNotFoundException($"Prompt {promptName} not found as embedded resource.");
        }

        await using var stream = assembly.GetManifestResourceStream(resourceName)!;
        using var reader = new StreamReader(stream);
        return await reader.ReadToEndAsync();
    }

    /// <summary>
    /// Render a prompt using Handlebars with the supplied parameters.
    /// </summary>
    public async Task<string> RenderPromptAsync(string promptName, object? parameters = null)
    {
        var yamlContent = await LoadPromptAsync(promptName);

        // Parse YAML to extract the template text
        var yaml = new YamlStream();
        yaml.Load(new StringReader(yamlContent));
        var root = (YamlMappingNode)yaml.Documents[0].RootNode;
        var template = root.Children[new YamlScalarNode("template")].ToString();

        var handlebars = Handlebars.Create();
        var compiled = handlebars.Compile(template);
        return compiled(parameters ?? new { });
    }
}
