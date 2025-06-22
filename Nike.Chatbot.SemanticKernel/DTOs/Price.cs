namespace Nike.Chatbot.SemanticKernel.DTOs;

public class Price
{
    public int PriceId { get; set; }

    public int ModelId { get; set; }
    
    public string? ModelName { get; set; }
    
    public decimal ShoePrice { get; set; }
    
    public DateTime CreatedAt { get; set; }
}