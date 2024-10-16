
namespace Lubricentro25.Models;

public partial class Phone : ObservableObject
{
    public string Id { get; set; }

    [ObservableProperty]
    private string nationalId;

    [ObservableProperty]
    private string value;

    [ObservableProperty]
    private bool isActive;

    public Phone(string id, string nationalId, string value, bool isActive)
    {
        Id = id;
        NationalId = nationalId;
        Value = value;
        IsActive = isActive;
    }
}
