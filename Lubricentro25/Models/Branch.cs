namespace Lubricentro25.Models;

public partial class Branch : ObservableObject
{
    public string Id { get; set; }

    [ObservableProperty]
    string name;

    [ObservableProperty]
    string pointOfSale;

    [ObservableProperty]
    Address address;

    public Branch()
    {
        Id = string.Empty;
        Name = string.Empty;
        PointOfSale = string.Empty;
        Address = new();
    }

    public Branch Clone()
    {
        return (Branch)MemberwiseClone();
    }

    public override string ToString()
    {
        return $"{Name} - {PointOfSale}";
    }
}
