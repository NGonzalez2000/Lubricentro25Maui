

using Newtonsoft.Json.Linq;

namespace Lubricentro25.Models;

public partial class SellItem : ObservableObject
{
    public string StockItemId { get; set; }
    public string ProductId { get; set; }
    
    [ObservableProperty]
    string code;
    
    [ObservableProperty]
    string barcode;
    
    [ObservableProperty]
    string description;
    
    [ObservableProperty]
    string brandName;
    
    [ObservableProperty]
    Discount discount;

    [ObservableProperty]
    ClientTypeDiscount? selectedClientDiscount;

    [ObservableProperty]
    VatType vatType;
    
    [ObservableProperty]
    decimal listPrice;
    
    [ObservableProperty]
    decimal markupPercentage;
    
    [ObservableProperty]
    decimal itemsPerPackage;
    
    [ObservableProperty]
    decimal minSellUnit;
    
    [ObservableProperty]
    bool isWholesaler;
    
    [ObservableProperty]
    bool isUsd;
    
    [ObservableProperty]
    decimal quantity;

    [ObservableProperty]
    decimal soldUnits;

    [ObservableProperty]
    decimal sellPrice;

    [ObservableProperty]
    decimal finalPrice;

    public SellItem()
    {
        StockItemId = string.Empty;
        ProductId = string.Empty;
        Code = string.Empty;
        Barcode = string.Empty;
        Description = string.Empty;
        BrandName = string.Empty;
        Discount = null!;
        VatType = null!;
    }

    public SellItem Clone()
    {
        return new SellItem()
        {
            StockItemId = StockItemId,
            ProductId = ProductId,
            Code = Code,
            Barcode = Barcode,
            Description = Description,
            BrandName = BrandName,
            Discount = Discount,
            SelectedClientDiscount = SelectedClientDiscount,
            VatType = VatType,
            ListPrice = ListPrice,
            MarkupPercentage = MarkupPercentage,
            ItemsPerPackage = ItemsPerPackage,
            MinSellUnit = MinSellUnit,
            IsWholesaler = IsWholesaler,
            IsUsd = IsUsd,
            Quantity = Quantity,
            SellPrice = SellPrice,
            FinalPrice = FinalPrice
        };
    }

    partial void OnMinSellUnitChanged(decimal value)
    {
        SoldUnits = value;
    }

    partial void OnDiscountChanged(Discount value)
    {
        if (value is null) return;

        if(value.ClientTypeDiscounts.Count > 0)
        {
            SelectedClientDiscount = value.ClientTypeDiscounts[0];
        }
    }
    partial void OnSelectedClientDiscountChanged(ClientTypeDiscount? value)
    {
        CalculateFinalPrice();
    }

    partial void OnVatTypeChanged(VatType value)
    {
        CalculateFinalPrice();
    }

    private void CalculateFinalPrice()
    {
        if (SelectedClientDiscount is not null)
            FinalPrice = SellPrice * (1 - SelectedClientDiscount.Discount / 100m);

        if(VatType is not null)
            FinalPrice = decimal.Round(FinalPrice * (1 + VatType.Aliquota / 100m), 2);
    }

    public void ChangeDiscountType(ClientType clientType)
    {
        SelectedClientDiscount = Discount.ClientTypeDiscounts.FirstOrDefault(ctd => ctd.ClientTypeId == clientType.Id);
    }


    internal void Refresh()
    {
        SellPrice = ListPrice;
        if (IsUsd)
        {
            SellPrice *= (decimal)Preferences.Get("DolarPrice", 1d);
        }
        SellPrice *= 1 - Discount.FirstDiscount / 100m;
        SellPrice *= 1 - Discount.SecondDiscount / 100m;
        SellPrice *= 1 - Discount.ThirdDiscount / 100m;
        SellPrice *= 1 - Discount.FourthDiscount / 100m;

        SellPrice *= 1 + MarkupPercentage / 100m;


        CalculateFinalPrice();
    }
}
