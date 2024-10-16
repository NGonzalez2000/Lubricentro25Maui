namespace Lubricentro25.Models.Vehicles;

public partial class Vehicle : ObservableObject
{
    public string Id { get; set; }

    [ObservableProperty]
    string plate;
    
    [ObservableProperty]
    string year;

    [ObservableProperty]
    VehicleFactory factory;

    [ObservableProperty]
    VehicleModel model;

    [ObservableProperty]
    VehicleSpecification specification;

    public Vehicle()
    {
        Id = string.Empty;
        Plate = string.Empty;
        Year = string.Empty;
        Factory = new();
        Model = new();
        Specification = new();
    }

    public Vehicle(string plate, string year, VehicleFactory factory, VehicleModel model, VehicleSpecification specification) : this()
    {
        Plate = plate;
        Year = year;
        Factory = factory;
        Model = model;
        Specification = specification;
    }
}
