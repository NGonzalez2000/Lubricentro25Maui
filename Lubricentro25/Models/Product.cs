namespace Lubricentro25.Models;

public partial class Product : ObservableObject
{
    public string Id { get; set; }

    [ObservableProperty]
    string code;

    [ObservableProperty]
    string barcode;

    [ObservableProperty]
    string description;

    [ObservableProperty]
    Provider provider;

    [ObservableProperty]
    Brand brand;

    [ObservableProperty]
    decimal listPrice;

    [ObservableProperty]
    decimal sellPrice;

    [ObservableProperty]
    decimal markupPercentage;

    [ObservableProperty]
    bool isWholesaler;

    [ObservableProperty]
    bool isUsd;

    public Product()
    {
        Id = string.Empty;
        Code = string.Empty;
        Barcode = string.Empty;
        Description = string.Empty;
        Provider = new();
        Brand = new();
    }
}
