namespace Lubricentro25.Models;

public partial class ClientType : ObservableObject
{
    public string Id { get; set; }

    [ObservableProperty]
    string description;

    [ObservableProperty]
    int order;

    public ClientType()
    {
        Id = string.Empty;
        Description = string.Empty;
    }
}
