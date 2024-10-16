namespace Lubricentro25.Models.Vehicles;

public partial class VehicleSpecification : ObservableObject
{
    public string Id { get; set; }
    
    [ObservableProperty]
    string specification;

    public VehicleSpecification()
    {
        Id = string.Empty;
        Specification = string.Empty;
    }
    public VehicleSpecification(string specification) : this()
    {
        Specification = specification;
    }
}
