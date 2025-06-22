using Nike.Chatbot.Domain.Entities;
using Nike.Chatbot.Domain.ValueObjects;

namespace Nike.Chatbot.Application.Interfaces;

public interface INikeService
{
    Task<SearchResult<NikeModel>> SearchShoesAsync(string query, short year=0);
}