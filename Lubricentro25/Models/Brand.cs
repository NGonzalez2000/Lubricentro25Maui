
using System.Collections.ObjectModel;

namespace Lubricentro25.Models;

public partial class Brand : ObservableObject
{
    public string Id { get; set; }
    
    [ObservableProperty]
    string name;

    [ObservableProperty]
    ObservableCollection<Provider> providers;

    public Brand()
    {
        Id = "";
        Name = "";
        Providers = [];
    }

    public Brand Clone()
        => (Brand)MemberwiseClone();
}
