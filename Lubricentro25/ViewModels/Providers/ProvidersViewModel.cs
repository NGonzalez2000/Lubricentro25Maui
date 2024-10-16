using CommunityToolkit.Mvvm.Input;
using Lubricentro25.Api.Interface;
using Lubricentro25.Controls.PopUps;
using Lubricentro25.Pages.DedicatedPages.ProviderPages;
using Lubricentro25.Services.Interfaces;
using System.Collections.ObjectModel;

namespace Lubricentro25.ViewModels.Providers;

public partial class ProvidersViewModel(IProviderEndpoint providerEndpoint, IPopUpService popUpService) : BaseViewModel
{
    [ObservableProperty]
    string nameSearchText = string.Empty;
    [ObservableProperty]
    string cuilSearchText = string.Empty;
    [ObservableProperty]
    ObservableCollection<Provider> providers = [];

    [ObservableProperty]
    Provider? fullDetailsProvider;

    List<Provider> _providers = [];
    protected override async Task LoadDataAsync()
    {
        var response = await providerEndpoint.GetAll();
        if(!response.IsSuccessful)
        {
            await popUpService.ShowErrorMessage(response.ErrorMessage); 
            return;
        }

        _providers = new(response.ResponseContent);

        Providers = new(_providers);
    }

    [RelayCommand]
    void FullDetails(Provider? selectedProvider)
    {
        if (selectedProvider is null)
        {
            return;
        }
        FullDetailsProvider = selectedProvider;
        IsSidePanelVisible = true;
    }

    [RelayCommand]
    async Task Create()
    {
        await Shell.Current.GoToAsync(nameof(SingleProviderPage), new Dictionary<string, object>() 
        {
            ["Provider"] = new Provider(),
            ["Editing"] = true
        });
    }

    [RelayCommand]
    async Task Update(Provider? selectedProvider)
    {
        if (selectedProvider is null)
        {
            await popUpService.ShowErrorMessage("Seleccione un proveedor.");
            return;
        }

        await Shell.Current.GoToAsync(nameof(SingleProviderPage), new Dictionary<string, object>()
        {
            ["Provider"] = selectedProvider,
            ["Editing"] = true
        });
    }

    [RelayCommand]
    async Task Delete(Provider? selectedProvider)
    {
        if (selectedProvider is null)
        {
            await popUpService.ShowErrorMessage("Seleccione un proveedor.");
            return;
        }

        bool accepted = await popUpService.ShowWarning($"Seguro que desea eliminar el proveedor {selectedProvider.Name}?");

        if (!accepted)
        {
            return;
        }

        var response = await providerEndpoint.Delete(selectedProvider);

        if (!response.IsSuccessful)
        {
            await popUpService.ShowErrorMessage(response.ErrorMessage);
            return;
        }

        _providers.Remove(selectedProvider);
        Search();
    }

    [RelayCommand]
    void Search()
    {
        Providers = new(_providers.Where(p => p.Name.Contains(NameSearchText) && p.Cuil.Contains(CuilSearchText)));
    }
}
