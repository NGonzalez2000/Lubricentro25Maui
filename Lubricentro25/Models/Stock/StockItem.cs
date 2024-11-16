namespace Lubricentro25.Models.Stock;

public partial class StockItem : ObservableObject
{
    public string Id { get; set; }

    [ObservableProperty]
    string code;

    [ObservableProperty]
    string barcode;

    [ObservableProperty]
    string description;

    [ObservableProperty]
    string provider;

    [ObservableProperty]
    string brand;

    [ObservableProperty]
    decimal quantity;

    [ObservableProperty]
    int minStock;

    [ObservableProperty]
    int maxStock;

    [ObservableProperty]
    StockLocation location;

    [ObservableProperty]
    StockLocation deposit;

    public StockItem()
    {
        Id = string.Empty;
        Code = string.Empty;
        Barcode = string.Empty;
        Description = string.Empty;
        Provider = string.Empty;
        Brand = string.Empty;
        Location = new();
        Deposit = new();
    }
}
