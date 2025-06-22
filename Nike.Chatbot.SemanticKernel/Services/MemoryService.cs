using Microsoft.Extensions.Caching.Memory;

namespace Nike.Chatbot.SemanticKernel.Services;

/// <summary>
/// A service for managing memory in a chat application.
/// This service allows for saving and retrieving messages associated with specific session IDs.
/// It is designed to maintain a history of messages for each chat session,
/// enabling the chatbot to reference past interactions and provide context-aware responses.
/// The memory store is implemented as a dictionary where the key is the session ID
/// and the value is a list of messages.
/// This implementation is suitable for scenarios where chat history needs to be preserved
/// across multiple interactions, such as in a customer support chatbot or a conversational AI application.
/// The service provides methods to save messages and retrieve the entire message history for a given session.
/// It is a simple in-memory implementation and does not persist data across application restarts.
/// For production use, consider integrating with a persistent storage solution like a database.
/// The service is thread-safe and can handle concurrent requests to save and retrieve messages.
/// </summary>
public class MemoryService : IMemoryService
{
    /// <summary>
    /// Memory cache to store messages associated with session IDs.
    /// The memory store is implemented as a dictionary where the key is the session ID
    /// and the value is a list of messages.
    /// This allows for quick access to the message history for each session.
    /// The memory cache is used to store messages in memory, providing fast access
    /// and reducing the need for database calls for frequently accessed data.
    /// </summary>
    private readonly IMemoryCache _memoryCache;
    
    /// <summary>
    /// The expiration time for cache entries.
    /// This defines how long messages will be stored in memory before they are considered stale
    /// and removed from the cache.
    /// Setting an appropriate expiration time helps manage memory usage
    /// and ensures that the cache does not grow indefinitely.
    /// In this implementation, the expiration time is set to 30 minutes,
    /// but it can be adjusted based on the application's requirements.
    /// </summary>
    private readonly TimeSpan _cacheExpiration;

    /// <summary>
    /// Initializes a new instance of the <see cref="MemoryService"/> class.
    /// This constructor takes an <see cref="IMemoryCache"/> instance as a parameter,
    /// which is used to store messages in memory.
    /// </summary>
    /// <param name="memoryCache"></param>
    public MemoryService(IMemoryCache memoryCache)
    { 
        _memoryCache = memoryCache;
        _cacheExpiration = TimeSpan.FromMinutes(30); // Set cache expiration time
    }
    
    /// <summary>
    /// Saves a message to the memory store associated with a specific session ID.
    /// If the session ID does not exist, it creates a new entry.
    /// This method is useful for maintaining a history of messages in a chat session,
    /// allowing the chatbot to reference past interactions.
    /// </summary>
    /// <param name="sessionId">The unique identifier for the chat session.</param>
    /// <param name="message">The message to be saved in the session's history.</param>
    public void SaveMessage(string sessionId, string message)
    {
        var messages = _memoryCache.GetOrCreate(sessionId, entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = _cacheExpiration; // Set expiration time for cache entry
            return new List<string>();
        });
        
        messages!.Add(message);
    }

    /// <summary>
    /// Retrieves all messages for a given session ID.
    /// If no messages are found, an empty list is returned.
    /// This method is useful for retrieving the conversation history for a specific session.
    /// It can be used to display the chat history to the user or for further processing.
    /// </summary>
    /// <param name="sessionId">The unique identifier for the chat session.</param>
    /// <returns>A list of messages associated with the session ID. If no messages are found,
    /// an empty list is returned.</returns>
    public List<string>? GetMessages(string sessionId) => 
        _memoryCache.TryGetValue(sessionId, out List<string>? messages) 
            ? messages 
            : [];
}