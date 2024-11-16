


namespace Lubricentro25.Models;

public partial class BillItem : ObservableObject
{
    [ObservableProperty]
    string code;

    [ObservableProperty]
    string description;

    [ObservableProperty]
    decimal quantity;

    [ObservableProperty]
    decimal sellPrice;

    [ObservableProperty]
    decimal unitPrice;

    [ObservableProperty]
    decimal bonification;

    [ObservableProperty]
    VatType vatType;

    [ObservableProperty]
    decimal addedVat;
    
    [ObservableProperty]
    decimal finalPrice;

    [ObservableProperty]
    bool isTypeA;

    public BillItem()
    {
        Code = string.Empty;
        Description = string.Empty;
        VatType = vatType;
        IsTypeA = false;
    }

    public BillItem(string code, string description, decimal quantity, decimal sellPrice, decimal bonification, VatType vatType)
    {
        VatType = vatType;
        Code = code;
        Description = description;
        Quantity = quantity;
        SellPrice = sellPrice;
        Bonification = bonification;
    }

    partial void OnSellPriceChanged(decimal value)
    {
        CalculateUnitPrice();
    }

    partial void OnBonificationChanged(decimal value)
    {
        CalculateUnitPrice();
    }

    partial void OnIsTypeAChanged(bool value)
    {
        CalculateUnitPrice();
    }

    partial void OnUnitPriceChanged(decimal value)
    {
        CalculateFinalPrice();
    }

    partial void OnQuantityChanged(decimal value)
    {
        CalculateFinalPrice();
    }
    private void CalculateUnitPrice()
    {
        UnitPrice = SellPrice * (1 - Bonification / 100m);
        if (!IsTypeA) UnitPrice *= (1 + VatType.Aliquota / 100m);

        UnitPrice = decimal.Round(UnitPrice, 2);
    }
    private void CalculateFinalPrice()
    {
        FinalPrice = UnitPrice * Quantity;

        if(IsTypeA)
        {
            AddedVat = FinalPrice * VatType.Aliquota / 100m;
        }

        FinalPrice = decimal.Round(FinalPrice, 2);
    }

    
}
