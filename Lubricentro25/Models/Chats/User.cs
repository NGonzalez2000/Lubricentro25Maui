namespace Lubricentro25.Models.Chats;

public partial class User : ObservableObject
{
    [ObservableProperty]
    byte[] imageData = [];
    [ObservableProperty]
    string name = string.Empty;
    public string UserId { get; set; } = string.Empty;
}
