using Nike.Chatbot.Application.Interfaces;
using Nike.Chatbot.Domain.Entities;
using Nike.Chatbot.Domain.Interfaces;
using Nike.Chatbot.Domain.ValueObjects;

namespace Nike.Chatbot.Application.Services;

internal class PriceService : IPriceService
{
    private readonly IPriceApiClient _priceApiClient;
    
public PriceService(IPriceApiClient priceApiClient)
    {
        _priceApiClient = priceApiClient;
    }
    
    public async Task<SearchResult<Price>> GetPriceAsync(int query)
    {
        var result = await _priceApiClient.GetPriceByModel(query);
        return new SearchResult<Price>
        {
            Objects = new List<Price> { result }
        };
    }
}