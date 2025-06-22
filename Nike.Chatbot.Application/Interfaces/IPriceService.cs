using Nike.Chatbot.Domain.Entities;
using Nike.Chatbot.Domain.ValueObjects;

namespace Nike.Chatbot.Application.Interfaces;

public interface IPriceService
{
    Task<SearchResult<Price>> GetPriceAsync(int query);
}