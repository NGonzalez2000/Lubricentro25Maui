using Lubricentro25.Models.Stock;
using CommunityToolkit.Mvvm.Input;
using Lubricentro25.Api.Interface;
using Lubricentro25.Services.Interfaces;
using Lubricentro25.Pages.DedicatedPages.ProductPages;

namespace Lubricentro25.ViewModels.Items;

[QueryProperty(nameof(SearchCode), "StockItemCode")]
public partial class StockViewModel(IStockEndpoint stockEndpoint, IPopUpService popUpService) : BaseViewModel
{
    [ObservableProperty]
    string productCode = string.Empty;
    [ObservableProperty]
    string productBarcode = string.Empty;
    [ObservableProperty]
    string productDescription = string.Empty;

    [ObservableProperty]
    string? searchCode;
    [ObservableProperty]
    StockItem? scrollToProduct;
    
    private bool loaded = false;
    async partial void OnSearchCodeChanged(string? value)
    {
        if (value is null) return;
        if (!loaded)
        {
            await LoadDataAsync();
            loaded = true;
        }
        ScrollToProduct = Stock.Items.FirstOrDefault(p => p.Code == SearchCode);
    }

    [ObservableProperty]
    Stock stock = null!;

    protected override async Task LoadDataAsync()
    {
        if(loaded)
        {
            loaded = false;
            return;
        }
        var resposne = await stockEndpoint.GetStock();

        if(!resposne.IsSuccessful)
        {
            await popUpService.ShowErrorMessage(resposne.ErrorMessage);
            return;
        }

        Stock = resposne.ResponseContent.First();
    }

    [RelayCommand]
    void Search()
    {

    }

    [RelayCommand]
    async Task GoToProduct(StockItem? stockItem)
    {
        if (stockItem is null)
        {
            await popUpService.ShowErrorMessage("Se perdió la referencia al item de stock.");
            return;
        }

        await Shell.Current.GoToAsync($"//Products/Prices?ProductCode={stockItem.Code}");
    }

    [RelayCommand]
    async Task UpdateStockItem(StockItem? stockItem)
    {
        if(stockItem is null)
        {
            await popUpService.ShowErrorMessage("Se perdió la referencia al item de stock.");
            return;
        }

        await Shell.Current.GoToAsync(nameof(SingleStockItemPage), new Dictionary<string, object>
        {
            ["Item"] = stockItem
        });
    }
}
