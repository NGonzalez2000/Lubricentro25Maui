using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Lubricentro25.Models.Chats;

public partial class Chat : ObservableObject
{
    [ObservableProperty]
    private bool canSendMessage;
    [ObservableProperty]
    private string to = string.Empty;
    [ObservableProperty]
    private ImageSource photo = "person.png";
    [ObservableProperty]
    private ObservableCollection<ChatMessage> messages = [];
    [ObservableProperty]
    private string messageText = string.Empty;
    public string ReceptorId { get; set; } = string.Empty;
    public IAsyncRelayCommand<string>? SendMessageCommand
    {
        get; set;
    }
    
}
