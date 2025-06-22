using Nike.Chatbot.Domain.Entities;

namespace Nike.Chatbot.Domain.Interfaces;

public interface IPriceApiClient
{
    Task<Price> GetPriceByModel(int modelId);
}