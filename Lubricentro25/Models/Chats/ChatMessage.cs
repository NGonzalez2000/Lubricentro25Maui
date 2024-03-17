
namespace Lubricentro25.Models.Chats;

public partial class ChatMessage : ObservableObject
{
    [ObservableProperty]
    private string? senderName;

    [ObservableProperty]
    private string messageContent = string.Empty;
    [ObservableProperty]
    private bool isMine;
    public string SenderId { get; set; } = string.Empty;
    public ChatMessage()
    {
    }
    public ChatMessage(string messageContent, bool isMine, string senderId = "", string? senderName = null)
    {
        SenderId = senderId;
        this.senderName = senderName;
        MessageContent = messageContent;
        IsMine = isMine;
    }
}
