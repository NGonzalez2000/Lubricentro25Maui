namespace Lubricentro25.Models;

public partial class ClientTypeDiscount : ObservableObject
{
    public string Id { get; set; }
    public string ClientTypeId { get; set; }

    [ObservableProperty]
    string description;

    [ObservableProperty]
    decimal discount;

    public ClientTypeDiscount(ClientType clientType)
    {
        Id = string.Empty;
        ClientTypeId = clientType.Id;
        Description = clientType.Description;
    }

    public ClientTypeDiscount()
    {
        Id = string.Empty;
        ClientTypeId = string.Empty;
        Description = string.Empty;
    }
}
