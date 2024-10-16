using Lubricentro25.Models.Collections;
using System.Collections.ObjectModel;

namespace Lubricentro25.Models;

public partial class Provider : ObservableObject
{
    public string Id { get; set; }

    [ObservableProperty]
    string name;

    [ObservableProperty]
    string cuil;

    [ObservableProperty]
    PhoneCollection phoneCollection;

    [ObservableProperty]
    EmailCollection emailCollection;

    [ObservableProperty]
    string fax;

    [ObservableProperty]
    string website;

    [ObservableProperty]
    string observation;

    [ObservableProperty]
    Address address;

    [ObservableProperty]
    TaxCondition taxCondition;

    public Provider()
    {
        Id = string.Empty;
        Name = string.Empty;
        Cuil = string.Empty;
        PhoneCollection = new();
        EmailCollection = new();
        Fax = string.Empty;
        Website = string.Empty;
        Observation = string.Empty;
        Address = new();
        TaxCondition = new();
    }
    public Provider(string id, string name, string cuil, ObservableCollection<Email> emails, ObservableCollection<Phone> phones, string fax, string website, string observation, Address address, TaxCondition taxCondition)
    {
        Id = id;
        Name = name;
        Cuil = cuil;
        PhoneCollection = new() { Phones = phones };
        EmailCollection = new() { Emails = emails };
        Fax = fax;
        Website = website;
        Observation = observation;
        Address = address;
        TaxCondition = taxCondition;
    }

    public Provider Clone()
        => (Provider)MemberwiseClone();

}
