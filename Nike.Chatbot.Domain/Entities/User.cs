namespace Nike.Chatbot.Domain.Entities;

public class User
{
    public int UserId { get; private set; }
    
    public string? UserName { get; private set; }
    
    public DateTime SessionDate { get; private set; }

    public User(string userName)
    {
        ChangeUserName(userName);
    }
    
    public User(int userId, string userName, DateTime sessionDate)
    {
        userId = userId;
        ChangeUserName(userName);
        SessionDate = sessionDate;
    }
    
    private void ChangeUserName(string userName)
    {
        if (string.IsNullOrWhiteSpace(userName))
        {
            throw new ArgumentException("User name cannot be null or empty.");
        }
        UserName = userName;
    }
}