namespace Nike.Chatbot.SemanticKernel.Services;

/// <summary>
/// Interface for memory service to handle message storage and retrieval.
/// This service is used to save and retrieve messages associated with a specific session ID.
/// It is typically used in chat applications to maintain conversation history.
/// Implementations of this interface should provide the actual storage mechanism,
/// which could be in-memory, database, or any other persistent storage.
/// The methods defined here allow for saving a message and retrieving all messages for a given session.
/// </summary>
public interface IMemoryService
{
    /// <summary>
    /// Saves a message associated with a specific session ID.
    /// This method is used to store a message in the memory service.
    /// The session ID is used to group messages together, allowing retrieval of all messages for that session later.
    /// Implementations should handle the actual storage logic, which could involve in-memory storage
    /// or persisting to a database or other storage medium.
    /// The message is typically a string, but could be extended to support more complex message types
    /// if needed in the future.
    /// </summary>
    /// <param name="sessionId">The unique identifier for the session to which the message belongs.</param>
    /// <param name="message">The message content to be saved.</param>
    /// <returns>void</returns>
    void SaveMessage(string sessionId, string message);
    
    /// <summary>
    /// Retrieves all messages associated with a specific session ID.
    /// This method is used to fetch all messages that have been saved for a given session.
    /// It returns a list of messages, allowing the application to reconstruct the conversation history
    /// for that session. The messages are typically returned in the order they were saved,
    /// which is important for maintaining the context of the conversation.
    /// Implementations should ensure that the retrieval logic is efficient and can handle
    /// potentially large numbers of messages if the session has been active for a long time.
    /// </summary>
    /// <param name="sessionId">The unique identifier for the session whose messages are to be retrieved.</param>
    /// <returns>A list of messages associated with the specified session ID.</returns>
    List<string> GetMessages(string sessionId);
}