
namespace Lubricentro25.Models;

public partial class BillType : ObservableObject
{
    public string Id { get; set; }

    [ObservableProperty]
    string description;

    public BillType()
    {
        Id = string.Empty;
        Description = string.Empty;
    }

    internal BillType Clone()
    {
        return new()
        {
            Id = Id,
            Description = Description
        };
    }
}
