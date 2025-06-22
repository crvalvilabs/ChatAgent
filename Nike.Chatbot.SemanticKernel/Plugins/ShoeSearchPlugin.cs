using System.ComponentModel;
using Microsoft.SemanticKernel;
using Nike.Chatbot.Application.Interfaces;
using Nike.Chatbot.SemanticKernel.DTOs;

namespace Nike.Chatbot.SemanticKernel.Plugins;

[Description("Plugin for searching Nike shoes by model name or model name and year.")]
public class ShoeSearchPlugin
{
    private readonly INikeService _nikeService;

    public ShoeSearchPlugin(INikeService nikeService)
    {
        _nikeService = nikeService;
    }

    [KernelFunction("search_shoes")]
    [Description("""
        Search for shoes by model nameor model name and year. Returns a list of shoes with model ID, model name, description, and year.
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
                            "ModelId": {
                                "type": "integer",
                                "description": "The unique identifier for the shoe model."
                            },
                            "ModelName": {
                                "type": "string",
                                "description": "The name of the shoe model."
                            },
                            "Description": {
                                "type": "string",
                                "description": "A description of the shoe model."
                            },
                            "Year": {
                                "type": "integer",
                                "description": "The year the shoe model was released."
                            }
                        }
                    }
                }
            },
        }
        """)]
    public async Task<ServiceResponse<Shoes>> SearchShoesAsync(string query, short year)
    {
        var result =  await _nikeService.SearchShoesAsync(query, year);
        var response = new ServiceResponse<Shoes>
        {
            IsSuccess = result.HasResults,
            Message = (result.HasResults ? "Search completed successfully." : "No results found."),
            ObjectsList = result.Objects!.Select(o => new Shoes
            {
                ModelId = o.ModelId,
                ModelName = o.ModelName,
                Description = o.Description,
                Year = o.Year
            }).ToList()
        };
        
        return response;
    }
}