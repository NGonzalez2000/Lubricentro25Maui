using CommunityToolkit.Mvvm.Input;
using Lubricentro25.Api.Interface;
using Lubricentro25.Controls.PopUps;
using Lubricentro25.Pages.DedicatedPages.ClientPages;
using Lubricentro25.Services.Interfaces;
using System.Collections.ObjectModel;

namespace Lubricentro25.ViewModels.Customers;

public partial class CustomersViewModel(ICustomerEndpoint _customerEndpoint, ITaxConditionEndpoint _taxConditionEndpoint, IPopUpService _popUpService) : BaseViewModel
{
    [ObservableProperty]
    string nameSearchText = string.Empty;

    [ObservableProperty]
    string cuilSearchText = string.Empty;

    [ObservableProperty]
    Client? fullDetailsClient;

    [ObservableProperty]
    ObservableCollection<Client> clients = [];

    List<Client> _clients = [];

    protected override async Task LoadDataAsync()
    {
        var response = await _customerEndpoint.GetAll();
        if (!response.IsSuccessful)
        {
            await _popUpService.ShowErrorMessage(response.ErrorMessage);
            return;
        }

        _clients = new(response.ResponseContent.OrderBy(c => c.ClientName));
        foreach (var client in _clients)
        {
            client.FullDetailsCommand = FullDetailsCommand;
        }
        Search();
    }

    [RelayCommand]
    void FullDetails(Client? selectedClient)
    {
        if(selectedClient is null)
        {
            return;
        }
        FullDetailsClient = selectedClient;
        IsSidePanelVisible = true;
    }

    [RelayCommand]
    async Task Create()
    {
        var response = await _taxConditionEndpoint.GetTaxConditionsAsync();

        if(!response.IsSuccessful)
        {
            await _popUpService.ShowErrorMessage(response.ErrorMessage);
            return;
        }

        await Shell.Current.GoToAsync($"{nameof(SingleClientPage)}?Editing=true", 
            new Dictionary<string, object>
            { ["Client"] = new Client(), ["TaxConditions"] = response.ResponseContent.ToList() });
        return;
    }

    [RelayCommand]
    async Task Update(Client? selectedClient)
    {
        if(selectedClient is null)
        {
            await _popUpService.ShowErrorMessage("Seleccione un cliente.");
            return;
        }

        Client editableClient = selectedClient.Clone();


        await Shell.Current.GoToAsync($"{nameof(SingleClientPage)}?Editing=true",
            new Dictionary<string, object>
            { ["Client"] = editableClient });
    }

    [RelayCommand]
    async Task Delete(Client? selectedClient)
    {
        if (selectedClient is null)
        {
            await _popUpService.ShowErrorMessage("Seleccione un cliente.");
            return;
        }

        bool accepted = await _popUpService.ShowWarning($"Seguro que desea eliminar el cliente {selectedClient.ClientName}?");

        if (!accepted)
        {
            return;
        }

        var response = await _customerEndpoint.Delete(selectedClient);

        if (!response.IsSuccessful)
        {
            await _popUpService.ShowErrorMessage(response.ErrorMessage);
            return;
        }

        _clients.Remove(selectedClient);
        Search();
    }

    [RelayCommand]
    void Search()
    {
        Clients = new(_clients.Where(c => c.ClientName.Contains(NameSearchText) && c.Cuil.Contains(CuilSearchText)));
    }

}

