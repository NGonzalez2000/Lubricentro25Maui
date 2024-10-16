namespace Lubricentro25.Models;

public partial class Address : ObservableObject
{
    public string Id { get; set; }
    [ObservableProperty]
    private string country;
    [ObservableProperty]
    private string state;
    [ObservableProperty]
    private string city;
    [ObservableProperty]
    private string street;
    [ObservableProperty]
    private string postalCode;

    public Address(string id, string country, string state, string city, string street, string postalCode)
    {
        Id = id;
        this.country = country;
        this.state = state;
        this.city = city;
        this.street = street;
        this.postalCode = postalCode;
    }
    public Address(string country, string state, string city, string street, string postalCode)
    {
        Id = string.Empty;
        this.country = country;
        this.state = state;
        this.city = city;
        this.street = street;
        this.postalCode = postalCode;
    }

    public Address()
    {
        Id = string.Empty;
        Country = string.Empty;
        State = string.Empty;
        City = string.Empty;
        Street = string.Empty;
        PostalCode = string.Empty;
    }

    public Address Clone()
        => (Address)MemberwiseClone();
}
