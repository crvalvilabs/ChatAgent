using System.Net.Http.Json;
using Nike.Chatbot.Domain.Entities;
using Nike.Chatbot.Domain.Interfaces;

namespace Nike.Chatbot.Infrastructure.Api;

internal class PriceApiClient : IPriceApiClient
{
    private readonly HttpClient _httpClient;
    
    public PriceApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Price> GetPriceByModel(int modelId)
    {
        var response = await _httpClient.GetAsync($"/api/GetPriceByModelId/{modelId}");
        if (response.IsSuccessStatusCode)
        {
            var price = await response.Content.ReadFromJsonAsync<Price>();
            return price ?? throw new Exception("Price not found");
        }
        else
        {
            throw new Exception($"Error fetching price: {response.ReasonPhrase}");
        }
    }
}