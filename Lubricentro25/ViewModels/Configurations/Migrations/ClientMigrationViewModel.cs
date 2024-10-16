using CommunityToolkit.Mvvm.Input;
using Lubricentro25.Api.Interface;
using Lubricentro25.Controls.PopUps;
using Lubricentro25.Services.Interfaces;
using System.Collections.ObjectModel;

namespace Lubricentro25.ViewModels.Configurations.Migrations;

public partial class ClientMigrationViewModel(IMigrationEndpoint _migrationEndpoint, ITaxConditionEndpoint _taxConditionEndpoint, IPopUpService _popUpService) : ObservableObject
{
    private CancellationTokenSource? tokenSource;
    readonly List<TaxCondition> taxConditions = [];

    [ObservableProperty]
    string clientName = string.Empty;
    [ObservableProperty]
    string cuil = string.Empty;

    [ObservableProperty]
    ObservableCollection<Client> newClients = [];

    [ObservableProperty]
    ObservableCollection<Client> existingClients = [];

    IEnumerable<Client> _newClients = [];
    IEnumerable<Client> _existingClients = [];

    [RelayCommand]
    async Task ReadOldDb()
    {
        var response = await _migrationEndpoint.GetClients();

        if(!response.IsSuccessful)
        {
            await _popUpService.ShowErrorMessage(response.ErrorMessage);
            return;
        }

        List<Client> tempNew = [];
        List<Client> tempExisting = [];

        foreach (var oldClient in response.ResponseContent)
        {
            if(int.TryParse(oldClient.Id, out var _))
            {
                tempNew.Add(oldClient);
            }
            else
            {
                tempExisting.Add(oldClient);
            }
        }


        _newClients = tempNew.OrderBy(c => c.ClientName);
        _existingClients = tempExisting.OrderBy(c => c.ClientName);

        Search();
    }

    [RelayCommand]
    private void Search()
    {
        NewClients = new(_newClients.Where(c => c.ClientName.Contains(ClientName) && c.Cuil.Contains(Cuil)));
        ExistingClients = new(_existingClients.Where(c => c.ClientName.Contains(ClientName) && c.Cuil.Contains(Cuil)));
    }

    [RelayCommand]
    async Task Migrate()
    {
        Cuil = string.Empty;
        ClientName = string.Empty;
        Search();


        tokenSource = new();
        int itemsCount = NewClients.Count;
        ProgressPopUp popUp = new(itemsCount, "Creando Clientes", tokenSource);
        _popUpService.ShowParallelPopUp(popUp);

        for (int i = 0; i < itemsCount; i++)
        {
            if(tokenSource.IsCancellationRequested)
            { 
                break;
            }
            var response = await _migrationEndpoint.CreateClients(NewClients[i]);
            if(!response.IsSuccessful)
            {
                NewClients[i].Error = response.ErrorMessage;
            }
            else
            {
                NewClients[i] = response.ResponseContent.First();
                NewClients[i].Error = string.Empty;
            }

            _popUpService.ParallelPopupHint();
        }


        tokenSource = new();
        itemsCount = ExistingClients.Count;
        ProgressPopUp popUp2 = new(itemsCount, "Actualizando Clientes",tokenSource);
        _popUpService.ShowParallelPopUp(popUp2);

        for (int i = 0; i < itemsCount; i++)
        {
            if (tokenSource.IsCancellationRequested)
            {
                break;
            }
            var response = await _migrationEndpoint.UpdateClients(ExistingClients[i]);
            if (!response.IsSuccessful)
            {
                ExistingClients[i].Error = response.ErrorMessage;
            }
            else
            {
                ExistingClients[i] = response.ResponseContent.First();
            }

            _popUpService.ParallelPopupHint();
        }

    }

    [RelayCommand]
    async Task EditClient(Client? selectedClient)
    {
        if (selectedClient is null)
        {
            await _popUpService.ShowErrorMessage("Seleccione un cliente.");
            return;
        }

        if (taxConditions.Count == 0) 
        {
            var response = await _taxConditionEndpoint.GetTaxConditionsAsync();
            if (!response.IsSuccessful)
            {
                await _popUpService.ShowErrorMessage(response.ErrorMessage);
                return;
            }

            taxConditions.AddRange(response.ResponseContent);
        }

        Client editableClient = selectedClient.Clone();


        CustomerPopUp popUp = new(taxConditions)
        {
            BindingContext = editableClient,
        };

        popUp.SelectTaxCondition(editableClient.TaxCondition.Description);

        bool accepted = await _popUpService.ShowPopUpAsync(popUp);

        if (!accepted)
        {
            return;
        }

        var temp = popUp.GetSelectedTaxCondition();

        if (temp is not TaxCondition taxCondition)
        {
            await _popUpService.ShowErrorMessage("No se puede editar un cliente sin Condicion de Cliente");
            return;
        }

        editableClient.TaxCondition = taxCondition;
    }

    [RelayCommand]
    async Task RemoveNew(Client? selectedClient)
    {
        if(selectedClient is null)
        {
            await _popUpService.ShowErrorMessage("Seleccione un cliente.");
            return;
        }

        NewClients.Remove(selectedClient);
    }

    [RelayCommand]
    async Task RemoveExisting(Client? selectedClient)
    {
        if (selectedClient is null)
        {
            await _popUpService.ShowErrorMessage("Seleccione un cliente.");
            return;
        }

        ExistingClients.Remove(selectedClient);
    }
}
