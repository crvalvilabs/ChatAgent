using System.Net.Http.Json;
using Nike.Chatbot.Domain.Entities;
using Nike.Chatbot.Domain.Interfaces;

namespace Nike.Chatbot.Infrastructure.Api;

public class UserApiClient : IUserApiClient
{
    private readonly HttpClient _httpClient;
    
    public UserApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> AddUser(string userName)
    {
        var user = new User(userName);
        
        var response = await _httpClient.PostAsJsonAsync("/api/AddUser/", user);
        if (response.IsSuccessStatusCode)
        {
            return response.Content.ReadFromJsonAsync<bool>().Result;
        }
        else
        {
            throw new Exception($"Error adding user: {response.ReasonPhrase}");
        }
    }

    public async Task<User> GetUser(string userName)
    {
        var response = await _httpClient.GetAsync($"/api/GetUserByUserName/{userName}");
        if (response.IsSuccessStatusCode)
        {
            var user = await response.Content.ReadFromJsonAsync<User>();
            return user ?? throw new Exception("User not found");
        }
        else
        {
            throw new Exception($"Error fetching user: {response.ReasonPhrase}");
        }
    }
}