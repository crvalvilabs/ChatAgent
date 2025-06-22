using Nike.Chatbot.Application.Interfaces;
using Nike.Chatbot.Domain.Entities;
using Nike.Chatbot.Domain.Interfaces;
using Nike.Chatbot.Domain.ValueObjects;

namespace Nike.Chatbot.Application.Services;

internal class NikeService : INikeService
{
    private readonly IShoeApiClient _shoeApiClient;
    
    public NikeService(IShoeApiClient shoeApiClient)
    {
        _shoeApiClient = shoeApiClient;
    }
    
    public async Task<SearchResult<NikeModel>> SearchShoesAsync(string modelName, short year = 0)
    {
        if (year > 0)
        {
            var result = await _shoeApiClient.GetModelByNameYear(modelName, year);
            return new SearchResult<NikeModel>
            {
                Objects = new List<NikeModel> { result }
            };
        }
        
        var shoes = await _shoeApiClient.GetModelByName(modelName);
        return new SearchResult<NikeModel>
        {
            Objects = new List<NikeModel> { shoes }
        };
    }
}