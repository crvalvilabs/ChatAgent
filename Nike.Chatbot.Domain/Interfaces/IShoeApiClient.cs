using Nike.Chatbot.Domain.Entities;

namespace Nike.Chatbot.Domain.Interfaces;

public interface IShoeApiClient
{
    Task<NikeModel> GetModelByName(string modelName);
    
    Task<NikeModel> GetModelByNameYear(string modelName, short year);
}