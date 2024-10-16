using System.Collections.ObjectModel;

namespace Lubricentro25.Models;

public partial class Company : ObservableObject
{
    public string Id { get; set; } = "";
    [ObservableProperty]
    string name = "";
    [ObservableProperty]
    string cuil = "";
    [ObservableProperty]
    string email = "";
    [ObservableProperty]
    string clientId = "";
    [ObservableProperty]
    string clientSecret = "";

    [ObservableProperty]
    ObservableCollection<Branch> branches = [];

    public Company Clone()
    {
        return new()
        {
            Id = Id,
            Name = Name,
            Cuil = Cuil,
            Email = Email,
            ClientId = ClientId,
            ClientSecret = ClientSecret,
            Branches = new(Branches)
        };
    }
}
