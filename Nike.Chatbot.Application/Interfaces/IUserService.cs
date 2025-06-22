using Nike.Chatbot.Application.Services;
using Nike.Chatbot.Domain.Entities;
using Nike.Chatbot.Domain.ValueObjects;

namespace Nike.Chatbot.Application.Interfaces;

public interface IUserService
{
    Task<SearchResult<User>> SearchUsersAsync(string query);
    Task<SearchResult<OperationResult>> RegisterUserAsync(string userName);
}