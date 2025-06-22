using Nike.Chatbot.Application.Interfaces;
using Nike.Chatbot.Domain.Entities;
using Nike.Chatbot.Domain.Interfaces;
using Nike.Chatbot.Domain.ValueObjects;

namespace Nike.Chatbot.Application.Services;

internal class UserService : IUserService
{
    private readonly IUserApiClient _userApiClient;
    
    public UserService(IUserApiClient userApiClient)
    {
        _userApiClient = userApiClient;
    }

    public async Task<SearchResult<User>> SearchUsersAsync(string query)
    {
        var result = await _userApiClient.GetUser(query);
        return new SearchResult<User>
        {
            Objects = new List<User> { result },
        };
    }
    
    public async Task<SearchResult<OperationResult>> RegisterUserAsync(string userName)
    {
        var result = await _userApiClient.AddUser(userName);
        return new SearchResult<OperationResult>
        {
            Objects = new List<OperationResult>
            {
                new OperationResult
                {
                    IsSuccess = result,
                    Message = result ? "User session registered successfully." : "Failed to register user session."
                }
            }
        };
    }
} 