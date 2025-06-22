using System.ComponentModel;
using Microsoft.SemanticKernel;
using Nike.Chatbot.Application.Interfaces;
using Nike.Chatbot.SemanticKernel.DTOs;

namespace Nike.Chatbot.SemanticKernel.Plugins;

[Description("Price Search Plugin")]
public class PriceSearchPlugin
{
    private readonly IPriceService _priceService;
    
    public PriceSearchPlugin(IPriceService priceApiClient)
    {
        _priceService = priceApiClient;
    }
    
    [KernelFunction("search_price")]
    [Description("""
        Search for a price by model ID. Returns:
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
                            "PriceId": {
                                "type": "integer",
                                "description": "The unique identifier for the price."
                            },
                            "ModelId": {
                                "type": "integer",
                                "description": "The unique identifier for the shoe model."
                            },
                            "ModelName": {
                                "type": "string",
                                "description": "The name of the shoe model."
                            },
                            "ShoePrice": {
                                "type": "number",
                                "format": "float",
                                "description": "The price of the shoe."
                            },
                            "CreatedAt": {
                                "type": "string",
                                "format": "date-time",
                                "description": "The date and time when the price was created."
                            }
                        }
                    }
                }
            }
                 
                 
        }
        """)]
    public async Task<ServiceResponse<Price>> SearchPriceAsync(int id)
    {
        var result = await _priceService.GetPriceAsync(id);
        var response = new ServiceResponse<Price>
        {
            IsSuccess = result.HasResults,
            Message = result.HasResults ? "Price found successfully." : "Price not found.",
            ObjectsList = result.Objects!.Select(price => new Price
            {
                PriceId = price.PriceId,
                ModelId = price.ModelId,
                ModelName = price.Model!.ModelName,
                ShoePrice = price.ShoePrice,
                CreatedAt = price.CreatedAt
            }).ToList()
        };
        
        return response;
    }
}