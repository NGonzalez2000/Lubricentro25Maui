using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Lubricentro25.Models;

public partial class Bill : ObservableObject
{
    public string Id { get; set; }

    [ObservableProperty]
    BillType billType;

    [ObservableProperty]
    char afipType;

    [ObservableProperty]
    Client client;

    [ObservableProperty]
    decimal dolarPrice;

    [ObservableProperty]
    ObservableCollection<BillItem> billItems;

    [ObservableProperty]
    decimal addedVat;

    [ObservableProperty]
    decimal subTotalPrice;

    [ObservableProperty]
    decimal totalPrice;

    public Bill()
    {
        Id = string.Empty;
        BillType = new();
        afipType = 'B';
        Client = new();
        BillItems = [];
    }

    public void Reset(Client defaultClient, BillType defaultBillType)
    {
        DolarPrice = (decimal)Preferences.Get(PreferenceTypes.DolarPrice.ToString(), 0d);
        Client = defaultClient.Clone();
        BillType = defaultBillType.Clone();
        BillItems = [];
        SubTotalPrice = 0m;
        AddedVat = 0m;
        AfipType = Preferences.Get(PreferenceTypes.AfipState.ToString(), true) ? 'B' : 'X';
    }

    public void AddItem(BillItem item)
    {
        if(AfipType == 'A')
        {
            item.IsTypeA = true;
            AddedVat += item.AddedVat;
        }
        else
        {
            item.IsTypeA = false;
        }
        SubTotalPrice += item.FinalPrice;

        BillItems.Add(item);
    }

    public void RemoveItem(BillItem item)
    {
        SubTotalPrice -= item.FinalPrice;
        AddedVat -= item.AddedVat;
        BillItems.Remove(item);
    }

    partial void OnAddedVatChanged(decimal value)
    {
        CalculateTotalPrice();
    }

    partial void OnSubTotalPriceChanged(decimal value)
    {
        CalculateTotalPrice();
    }

    private void CalculateTotalPrice()
    {
        TotalPrice = SubTotalPrice + AddedVat;
    }
}
