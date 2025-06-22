using System.Net.Http.Json;
using Nike.Chatbot.Domain.Entities;
using Nike.Chatbot.Domain.Interfaces;

namespace Nike.Chatbot.Infrastructure.Api;

internal class ShoeApiClient : IShoeApiClient
{
    private readonly HttpClient _httpClient;
    
    public ShoeApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<NikeModel> GetModelByName(string modelName)
    {
        var response = await _httpClient.GetAsync($"/api/GetModelsByName/{modelName}");
        if (response.IsSuccessStatusCode)
        {
            var model = await response.Content.ReadFromJsonAsync<NikeModel>();
            return model ?? throw new Exception("Model not found");
        }
        else
        {
            throw new Exception($"Error fetching model: {response.ReasonPhrase}");
        }
    }

    public async Task<NikeModel> GetModelByNameYear(string modelName, short year)
    {
        var response = await _httpClient.GetAsync($"/api/GetModelsByNameYear/{modelName}/{year}");
        if (response.IsSuccessStatusCode)
        {
            var model = await response.Content.ReadFromJsonAsync<NikeModel>();
            return model ?? throw new Exception("Model not found");
        }
        else
        {
            throw new Exception($"Error fetching model: {response.ReasonPhrase}");
        }
    }
}