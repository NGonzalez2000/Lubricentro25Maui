using CommunityToolkit.Mvvm.Input;
using Lubricentro25.Api.Interface;
using Lubricentro25.Controls.Filters.SellItems;
using Lubricentro25.Models.Collections;
using Lubricentro25.Services.Interfaces;
using Mapster;
namespace Lubricentro25.ViewModels.Billing;

public partial class BillViewModel(ISellEndpoint sellEndpoint,
                                   IVatTypeEndpoint vatTypeEndpoint,
                                   IPopUpService popUpService,
                                   ICustomerEndpoint customerEndpoint,
                                   IBillEndpoint billEndpoint,
                                   SellItemFilterViewModel _filterViewModel) : BaseViewModel
{
    private Client defaultClient = null!;
    private bool loading;

    [ObservableProperty]
    SellItemFilterViewModel filterViewModel = _filterViewModel;
    
    [ObservableProperty]
    FilterableCollection<SellItem> items = null!;

    [ObservableProperty]
    List<VatType> vatTypes = null!;

    [ObservableProperty]
    VatType? selectedVatType;

    [ObservableProperty]
    SellItem? selectedItem;

    [ObservableProperty]
    BillItem billItem = new();

    [ObservableProperty]
    Bill bill = new();

    [ObservableProperty]
    List<BillType> billTypes = null!;

    [ObservableProperty]
    BillType selectedBillType = null!;

    protected override async Task LoadDataAsync()
    {
        loading = true;

        var customerResponse = await customerEndpoint.GetFinalConsumer();
        if (!customerResponse.IsSuccessful)
            return;

        defaultClient = customerResponse.ResponseContent.First();

        var sellResponse = await sellEndpoint.GetSellItemsAsync();
        if(!sellResponse.IsSuccessful)
        {
            await popUpService.ShowErrorMessage(sellResponse.ErrorMessage);
            return;
        }
        Items = [];
        foreach(var item in sellResponse.ResponseContent)
        {
            item.Refresh();
            Items.Add(item);
        }

        var vatResponse = await vatTypeEndpoint.GetAllAsync();
        if (!vatResponse.IsSuccessful) 
        {
            await popUpService.ShowErrorMessage(vatResponse.ErrorMessage);
            return;
        }
        VatTypes = new(vatResponse.ResponseContent);
        SelectedVatType = VatTypes.FirstOrDefault(vt => vt.Aliquota == 21m);

        await FilterViewModel.Load();

        Items.SetFilter(FilterViewModel.Filter);

        var billTypeResponse = await billEndpoint.GetBillTypeAsync();
        if (!billTypeResponse.IsSuccessful)
        {
            await popUpService.ShowErrorMessage(billTypeResponse.ErrorMessage);
            return;
        }

        BillTypes = new(billTypeResponse.ResponseContent.OrderBy(bt => bt.Description));
        if (BillTypes.Count > 0) SelectedBillType = BillTypes[0];

        Bill.Reset(defaultClient, SelectedBillType);

        loading = false;
    }
    partial void OnSelectedItemChanged(SellItem? value)
    {
        if (value is null) return;

        decimal? bonification = value.SelectedClientDiscount?.Discount;
        BillItem = new(value.Code, value.Description, value.MinSellUnit, value.SellPrice, bonification is null? 0m : (decimal)bonification, value.VatType);
        SelectedVatType = VatTypes.FirstOrDefault(vt => vt.Description == BillItem.VatType.Description);
    }

    partial void OnSelectedVatTypeChanged(VatType? value)
    {
        if (value != null)
            BillItem.VatType = value;
    }

    partial void OnSelectedBillTypeChanged(BillType value)
    {
        if (loading) return;
        Bill.BillType = value;
    }

    [RelayCommand]
    void Refresh()
    {
        Items.Refresh();
    }

    [RelayCommand]
    async Task AddBillItem()
    {
        if(BillItem.Quantity == 0m)
        {
            await popUpService.ShowMessage("La cantidad vendida no puede ser 0");
            return;
        }

        if (Items.Get(bi => bi.Code == BillItem.Code) is not SellItem tempSellItem)
        {
            if (string.IsNullOrEmpty(BillItem.Description))
            {
                await popUpService.ShowMessage("Para agregar un producto personalizado\ndebe asignarle una Descripción");
                return;
            }
        }
        // AGREGAR
        Bill.AddItem(BillItem);


        SelectedItem = null;
        BillItem = new();
        SelectedVatType = VatTypes.FirstOrDefault(vt => vt.Aliquota == 21m);
    }

    [RelayCommand]
    void RemoveBillItem(BillItem item)
    {
        Bill.RemoveItem(item);
    }
}
