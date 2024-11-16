
using CommunityToolkit.Mvvm.Input;
using Lubricentro25.Api.Endpoints;
using Lubricentro25.Api.Interface;
using Lubricentro25.Pages.DedicatedPages.ClientPages;
using Lubricentro25.Services.Interfaces;
using System.Collections.ObjectModel;

namespace Lubricentro25.ViewModels.Customers;

public partial class ClientTypeViewModel(IClientTypeEndpoint clientTypeEndpoint, IPopUpService popUpService) : BaseViewModel
{
    List<ClientType> _clientTypes = [];

    [ObservableProperty]
    string descriptionSearchText = string.Empty;

    [ObservableProperty]
    ObservableCollection<ClientType> clientTypes = null!;

    protected override async Task LoadDataAsync()
    {
        var response = await clientTypeEndpoint.GetAllAsync();
        if (!response.IsSuccessful)
        {
            await popUpService.ShowErrorMessage(response.ErrorMessage);
            return;
        }

        _clientTypes = new(response.ResponseContent.OrderBy(c => c.Description));
        Search();
    }

    [RelayCommand]
    async Task Create()
    {
        await Shell.Current.GoToAsync(nameof(SingleClientTypePage), new Dictionary<string, object>()
        {
            [nameof(ClientType)] = new ClientType(),
            ["Editing"] = true
        });
    }

    [RelayCommand]
    async Task Update(ClientType? selectedClientType)
    {
        if(selectedClientType is null)
        {
            await popUpService.ShowWarning("Debe seleccionar un Tipo de cliente");
            return;
        }
        await Shell.Current.GoToAsync(nameof(SingleClientTypePage), new Dictionary<string, object>()
        {
            [nameof(ClientType)] = selectedClientType,
            ["Editing"] = true
        });
    }
    
    [RelayCommand]
    async Task Delete(ClientType? selectedClientType)
    {
        if (selectedClientType is null)
        {
            await popUpService.ShowWarning("Debe seleccionar un Tipo de cliente");
            return;
        }

        bool accepted = await popUpService.ShowWarning($"Seguro que desea eliminar el Tipo de Cliente {selectedClientType.Description}?");

        if (!accepted)
        {
            return;
        }

        var response = await clientTypeEndpoint.Delete(selectedClientType);

        if (!response.IsSuccessful)
        {
            await popUpService.ShowErrorMessage(response.ErrorMessage);
            return;
        }

        _clientTypes.Remove(selectedClientType);
        await LoadDataAsync();
    }

    [RelayCommand]
    void Search()
    {
        ClientTypes = new(_clientTypes.Where(c => c.Description.Contains(DescriptionSearchText, StringComparison.CurrentCultureIgnoreCase)));
    }

    [RelayCommand]
    async Task FullDetails(ClientType? selectedClientType)
    {
        if(selectedClientType is null)
        {
            return;
        }
        await Shell.Current.GoToAsync(nameof(SingleClientTypePage), new Dictionary<string, object>()
        {
            [nameof(ClientType)] = selectedClientType,
            ["Editing"] = false
        });
    }
}
