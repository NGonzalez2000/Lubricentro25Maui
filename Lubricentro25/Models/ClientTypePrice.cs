namespace Lubricentro25.Models;

public partial class ClientTypePrice : ObservableObject
{
    public string ClientTypeId { get; set; }
    [ObservableProperty]
    string description;

    [ObservableProperty]
    decimal price;

    [ObservableProperty]
    decimal discount;

    [ObservableProperty]
    decimal discountedPrice;

    [ObservableProperty]
    decimal vatAliquota;

    [ObservableProperty]
    decimal finalPrice;

    public ClientTypePrice(ClientTypeDiscount clientTypeDiscount, decimal Aliquota)
    {
        ClientTypeId = clientTypeDiscount.ClientTypeId;
        Description = clientTypeDiscount.Description;
        Discount = clientTypeDiscount.Discount;
        VatAliquota = Aliquota;
    }

    partial void OnPriceChanged(decimal value)
    {
        DiscountedPrice = decimal.Round(value * (1m - Discount / 100m), 2);
    }

    partial void OnVatAliquotaChanged(decimal value)
    {
        FinalPrice = decimal.Round(DiscountedPrice * (1m + value / 100m), 2);
    }

    partial void OnDiscountedPriceChanged(decimal value)
    {
        FinalPrice = decimal.Round(value * (1m + VatAliquota / 100m), 2);
    }
}
