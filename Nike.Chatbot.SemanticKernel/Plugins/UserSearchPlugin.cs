using System.ComponentModel;
using Microsoft.SemanticKernel;
using Nike.Chatbot.Application.Interfaces;
using Nike.Chatbot.SemanticKernel.DTOs;

namespace Nike.Chatbot.SemanticKernel.Plugins;

[Description("User Search Plugin")]
public class UserSearchPlugin
{
    private readonly IUserService _userService;
    
    public UserSearchPlugin(IUserService userService)
    {
        _userService = userService;
    }
    
    [KernelFunction("search_users")]
    [Description("""
        Searches for users by their username. Returns a list of users that match the search criteria.
        {
            "type": "object",
            "properties": {
                "IsSuccess": {
                     "type": "boolean",
                     "description": "Indicates if the search was successful."
                 },
                 "Message": {
                     "type": "string",
                     "description": "A message indicating the result of the search."
                 },
                 "ObjectsList": {
                    "type": "array",
                     "items": {
                         "type": "object",
                         "properties": {
                             "UserId": {
                                 "type": "integer",
                                 "description": "The unique identifier for the user."
                             },
                             "UserName": {
                                 "type": "string",
                                 "description": "The username of the user."
                             },
                             "SessionDate": {
                                 "type": "string",
                                 "format": "date-time",
                                 "description": "The date and time of the user's last session."
                             }
                         }
                     }
                 }
        }
        """)]
    public async Task<ServiceResponse<User>> SearchUsersAsync(string userName)
    {
        var response = await _userService.SearchUsersAsync(userName);
        return new ServiceResponse<User>
        {
            IsSuccess = response.HasResults,
            Message = response.HasResults ? "Users found." : "No users found.",
            ObjectsList = response.Objects!.Select(u => new User
            {
                UserId = u.UserId,
                UserName = u.UserName,
                SessionDate = u.SessionDate
            }).ToList()
        };
    }
    
    [KernelFunction("create_user")]
    [Description("""
        Creates a new user with the specified username. Returns the result of the user creation.
        {
            "type": "object",
            "properties": {
                "IsSuccess": {
                     "type": "boolean",
                     "description": "Indicates if the user was created successfully."
                 },
                 "Message": {
                     "type": "string",
                     "description": "A message indicating the result of the user creation."
                 }
            }
        }
        """)]
    public async Task<ServiceResponse<string>> CreateUserAsync(string userName)
    {
        var response = await _userService.RegisterUserAsync(userName);
        return new ServiceResponse<string>
        {
            IsSuccess = Convert.ToBoolean(response.Objects!.Select(b => b.IsSuccess)),
            Message = Convert.ToString(response.Objects!.Select(m => m.Message)),
        };
    }
}