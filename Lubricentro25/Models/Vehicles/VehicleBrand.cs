namespace Lubricentro25.Models.Vehicles;

public partial class VehicleModel : ObservableObject
{
    public string Id { get; set; }

    [ObservableProperty]
    string name;

    [ObservableProperty]
    bool isLight;
    public VehicleModel()
    {
        Id = string.Empty;
        Name = string.Empty;
        isLight = true;
    }
    public VehicleModel(string name, bool isLight) : this()
    {
        Name = name;
        IsLight = isLight;
    }
}
