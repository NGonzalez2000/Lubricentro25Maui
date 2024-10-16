using System.Collections.ObjectModel;

namespace Lubricentro25.Models.Vehicles;

public partial class VehicleFactory : ObservableObject
{
    public string Id { get; set; }

    [ObservableProperty]
    string name;

    [ObservableProperty]
    ObservableCollection<VehicleModel> models;

    public VehicleFactory()
    {
        Id = string.Empty;
        Name = string.Empty;
        Models = [];
    }

    public VehicleFactory(string name, IEnumerable<VehicleModel> models) : this()
    {
        Name = name;
        Models = new(models);
    }
}
