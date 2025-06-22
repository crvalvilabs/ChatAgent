namespace Nike.Chatbot.Domain.ValueObjects;

public class SearchResult <T> where T : class
{
    public IEnumerable<T>? Objects { get; set; }
    public bool HasResults => Objects!.Any(); 
}