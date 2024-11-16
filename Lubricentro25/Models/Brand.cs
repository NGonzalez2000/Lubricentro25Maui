
using System.Collections.ObjectModel;

namespace Lubricentro25.Models;

public partial class Brand : ObservableObject
{
    public string Id { get; set; }
    
    [ObservableProperty]
    string name;

    [ObservableProperty]
    ObservableCollection<Provider> providers;

    [ObservableProperty]
    ObservableCollection<Discount> discounts;

    public Brand()
    {
        Id = "";
        Name = "";
        Providers = [];
        Discounts = [];
    }

    public Brand Clone()
        => (Brand)MemberwiseClone();
}
