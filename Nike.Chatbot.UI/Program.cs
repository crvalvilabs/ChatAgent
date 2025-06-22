using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using System.Linq;
using Nike.Chatbot.Application;
using Nike.Chatbot.Infrastructure;
using Nike.Chatbot.SemanticKernel;
using Nike.Chatbot.SemanticKernel.DTOs;
using Nike.Chatbot.SemanticKernel.Plugins;
using Nike.Chatbot.SemanticKernel.Services;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

var services = new ServiceCollection();
services.AddSingleton<IConfiguration>(configuration);
services.AddInfrastructure(configuration);
services.AddApplication();
services.AddSemanticKernel();

var provider = services.BuildServiceProvider();
var promptService = provider.GetRequiredService<IPromptService>();
var shoePlugin = provider.GetRequiredService<ShoeSearchPlugin>();
var pricePlugin = provider.GetRequiredService<PriceSearchPlugin>();

var kernel = Kernel.CreateBuilder().Build();
var shoesFunctions = kernel.ImportPluginFromObject(shoePlugin, nameof(ShoeSearchPlugin));
var priceFunctions = kernel.ImportPluginFromObject(pricePlugin, nameof(PriceSearchPlugin));

Console.WriteLine("Ingrese el nombre del modelo:");
string? modelName = Console.ReadLine();
Console.WriteLine("Ingrese el año (opcional, Enter para omitir):");
string? yearText = Console.ReadLine();
short year = short.TryParse(yearText, out var y) ? y : (short)0;

var shoeArgs = new KernelArguments
{
    ["query"] = modelName ?? string.Empty,
    ["year"] = year
};
var shoeResult = await kernel.InvokeAsync(shoesFunctions["search_shoes"], shoeArgs);
var shoeResponse = shoeResult.GetValue<ServiceResponse<Shoes>>();

bool found = shoeResponse?.IsSuccess == true && shoeResponse.ObjectsList?.Any() == true;
string output;

if (found)
{
    var shoe = shoeResponse!.ObjectsList!.First();
    var priceArgs = new KernelArguments { ["id"] = shoe.ModelId };
    var priceResult = await kernel.InvokeAsync(priceFunctions["search_price"], priceArgs);
    var priceResponse = priceResult.GetValue<ServiceResponse<Price>>();
    decimal price = priceResponse?.ObjectsList?.First().ShoePrice ?? 0m;

    var templateName = year > 0 ? "search-by-name-year" : "search-by-name";
    output = await promptService.RenderPromptAsync(templateName, new
    {
        modelName = shoe.ModelName,
        modelFound = true,
        modelYear = shoe.Year,
        modelDescription = shoe.Description,
        precioModelo = price
    });
}
else
{
    var templateName = year > 0 ? "search-by-name-year" : "search-by-name";
    output = await promptService.RenderPromptAsync(templateName, new
    {
        modelName = modelName,
        modelFound = false,
        modelYear = year,
        modelDescription = string.Empty,
        precioModelo = string.Empty
    });
}

Console.WriteLine(output);
