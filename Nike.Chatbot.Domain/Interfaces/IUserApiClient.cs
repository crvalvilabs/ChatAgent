using Nike.Chatbot.Domain.Entities;

namespace Nike.Chatbot.Domain.Interfaces;

public interface IUserApiClient
{
    Task<bool> AddUser(string userName);
    
    Task<User> GetUser(string userName);
}