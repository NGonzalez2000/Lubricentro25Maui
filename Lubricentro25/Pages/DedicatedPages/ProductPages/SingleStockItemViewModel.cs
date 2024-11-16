using CommunityToolkit.Maui.Behaviors;
using CommunityToolkit.Mvvm.Input;
using Lubricentro25.Api.Interface;
using Lubricentro25.Models.Stock;
using Lubricentro25.Services.Interfaces;
using System.Collections.ObjectModel;

namespace Lubricentro25.Pages.DedicatedPages.ProductPages;

[QueryProperty(nameof(Item), "Item")]
public partial class SingleStockItemViewModel(IStockEndpoint stockEndpoint, IPopUpService popUpService) : ObservableObject
{
    [ObservableProperty]
    StockItem item = null!;

    [ObservableProperty]
    bool isStockEntryValid;

    [ObservableProperty]
    bool isMinStockEntryValid;

    [ObservableProperty]
    bool isMaxStockEntryValid;


    [ObservableProperty]
    bool isFormValid;

    partial void OnIsStockEntryValidChanged(bool value)
    {
        CheckValidationState();
    }
    partial void OnIsMinStockEntryValidChanged(bool value)
    {
        CheckValidationState();
    }
    partial void OnIsMaxStockEntryValidChanged(bool value)
    {
        CheckValidationState();
    }
    private void CheckValidationState()
    {
        IsFormValid = IsStockEntryValid && IsMinStockEntryValid && IsMaxStockEntryValid;
    }
    [RelayCommand]
    async Task Accept()
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    async Task Cancel()
    {
        await Shell.Current.GoToAsync("..");
    }
}
