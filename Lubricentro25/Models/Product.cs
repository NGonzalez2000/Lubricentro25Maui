using System.Collections.ObjectModel;

namespace Lubricentro25.Models;

public partial class Product : ObservableObject
{
    private bool changedFromCode = false;
    public string Id { get; set; }

    [ObservableProperty]
    string code;

    [ObservableProperty]
    string barcode;

    [ObservableProperty]
    string description;

    [ObservableProperty]
    Brand brand;

    [ObservableProperty]
    Discount discount;

    [ObservableProperty]
    decimal listPrice;

    [ObservableProperty]
    decimal buyPrice;

    [ObservableProperty]
    decimal sellPrice;

    [ObservableProperty]
    decimal markupPercentage;

    [ObservableProperty]
    bool isWholesaler;

    [ObservableProperty]
    bool isUsd;

    [ObservableProperty]
    VatType vatType;

    [ObservableProperty]
    decimal itemsPerPackage;

    [ObservableProperty]
    decimal minSellUnit;

    [ObservableProperty]
    ObservableCollection<ClientTypePrice> priceList;

    [ObservableProperty]
    DateTime lastUpdate;

    public Product()
    {
        PriceList = [];
        Id = string.Empty;
        Code = string.Empty;
        Barcode = string.Empty;
        Description = string.Empty;
        Brand = new();
        ItemsPerPackage = 1;
        MinSellUnit = 1;
        VatType = new();
        Discount = new([]);
    }

    partial void OnListPriceChanged(decimal value)
    {
        decimal initValue = value;

        initValue *= (1m - Discount.FirstDiscount / 100m);
        initValue *= (1m - Discount.SecondDiscount / 100m);
        initValue *= (1m - Discount.ThirdDiscount / 100m);
        initValue *= (1m - Discount.FourthDiscount / 100m);

        BuyPrice = decimal.Round(initValue, 2);
    }

    partial void OnDiscountChanged(Discount value)
    {
        decimal initValue = ListPrice;
        initValue *= (1m - value.FirstDiscount / 100m);
        initValue *= (1m - value.SecondDiscount / 100m);
        initValue *= (1m - value.ThirdDiscount / 100m);
        initValue *= (1m - value.FourthDiscount / 100m);

        List<ClientTypePrice> temp = [];
        foreach(var ctd in value.ClientTypeDiscounts)
        {
            temp.Add(new ClientTypePrice(ctd, VatType is null? 0m : VatType.Aliquota));
        }

        PriceList = new(temp);

        BuyPrice = 0m;
        BuyPrice = decimal.Round(initValue, 2);
    }

    partial void OnBuyPriceChanged(decimal value)
    {
        foreach(var ctp in PriceList)
        {
            ctp.Price = decimal.Round(value * (1m + MarkupPercentage / 100m), 2);
        }
    }

    partial void OnMarkupPercentageChanged(decimal value)
    {
        foreach (var ctp in PriceList)
        {
            ctp.Price = decimal.Round(BuyPrice * (1m + value / 100m), 2);
        }
    }

    partial void OnVatTypeChanged(VatType value)
    {
        foreach(var ctp in PriceList)
        {
            ctp.VatAliquota = value.Aliquota;
        }
    }
}
