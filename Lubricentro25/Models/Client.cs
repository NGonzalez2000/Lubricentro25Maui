using Lubricentro25.Models.Collections;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Lubricentro25.Models;

public partial class Client : ObservableObject
{
    public string Id { get; set; }

    [ObservableProperty]
    private Address address;

    [ObservableProperty]
    private TaxCondition taxCondition;

    [ObservableProperty]
    private string clientName;

    [ObservableProperty]
    private string cuil;

    [ObservableProperty]
    EmailCollection emailCollection;

    [ObservableProperty]
    PhoneCollection phoneCollection;

    [ObservableProperty]
    private string observation;
    
    [ObservableProperty]
    private bool hasCheckingAccount;
    
    [ObservableProperty]
    private bool isWholesaler;

    [ObservableProperty]
    private string error = string.Empty;

    [ObservableProperty]
    ICommand? fullDetailsCommand;
    
    public Client()
    {
        Id = string.Empty;
        Address = new();
        TaxCondition = new();
        ClientName = string.Empty;
        Cuil = string.Empty;
        emailCollection = new();
        phoneCollection = new();
        Observation = string.Empty;
    }

    public Client Clone()
    {
        return (Client)MemberwiseClone();
    }
}
