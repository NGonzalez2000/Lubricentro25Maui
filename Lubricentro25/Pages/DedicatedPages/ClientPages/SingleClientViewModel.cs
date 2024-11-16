using CommunityToolkit.Mvvm.Input;
using Lubricentro25.Api;
using Lubricentro25.Api.Interface;
using Lubricentro25.Services.Interfaces;
using System.Linq;

namespace Lubricentro25.Pages.DedicatedPages.ClientPages;

[QueryProperty(nameof(IsEditingEnable),"Editing")]
[QueryProperty(nameof(Client), nameof(Client))]
public partial class SingleClientViewModel(IPopUpService popUpService, ICustomerEndpoint customerEndpoint, IClientTypeEndpoint clientTypeEndpoint,
    ITaxConditionEndpoint taxConditionEndpoint) : ObservableObject
{
    [ObservableProperty]
    bool isEditingEnable;

    [ObservableProperty]
    List<TaxCondition> taxConditions = [];

    [ObservableProperty]
    List<ClientType> clientTypes = [];

    [ObservableProperty]
    TaxCondition? selectedTaxCondition;

    [ObservableProperty]
    ClientType? selectedClientType;

    [ObservableProperty]
    Client? client;

    //partial void OnTaxConditionsChanged(List<TaxCondition> value)
    //{
    //    if (value.Count == 0) return;
    //    if (Client is not null)
    //    {
    //        SelectedTaxCondition = TaxConditions.FirstOrDefault(tx => tx.Id == Client.TaxCondition.Id, value[0]);
    //    }
    //}

    //partial void OnClientTypesChanged(List<ClientType> value)
    //{
    //    if (value.Count == 0) return;
    //    if (Client is not null)
    //    {
    //        SelectedClientType = ClientTypes.FirstOrDefault(ct => ct.Id == Client.ClientType.Id, value.Single(ct => ct.Id == Guid.Empty.ToString()));
    //    }
    //}

    public async Task Refresh()
    {
        var response = await taxConditionEndpoint.GetTaxConditionsAsync();

        if (!response.IsSuccessful)
        {
            await popUpService.ShowErrorMessage(response.ErrorMessage);
            return;
        }
        TaxConditions = new(response.ResponseContent);

        if (TaxConditions.Count == 0) return;
        if (Client is not null)
        {
            SelectedTaxCondition = TaxConditions.FirstOrDefault(tx => tx.Id == Client.TaxCondition.Id, TaxConditions[0]);
        }

        var clientTypeResponse = await clientTypeEndpoint.GetAllAsync();

        if (!clientTypeResponse.IsSuccessful)
        {
            await popUpService.ShowErrorMessage(clientTypeResponse.ErrorMessage);
            return;
        }
        ClientTypes = new(clientTypeResponse.ResponseContent);

        if (ClientTypes.Count == 0) return;
        if (Client is not null)
        {
            SelectedClientType = ClientTypes.FirstOrDefault(ct => ct.Id == Client.ClientType.Id, ClientTypes.Single(ct => ct.Id == Guid.Empty.ToString()));
        }
    }

    [RelayCommand]
    async Task Accept()
    {
        if (Client is null) return;
        if(SelectedTaxCondition is null)
        {
            await popUpService.ShowErrorMessage("No se puede crear un cliente sin Cond. Fiscal");
            return;
        }

        Client.TaxCondition = SelectedTaxCondition;
        if (SelectedClientType is null)
        {
            await popUpService.ShowErrorMessage("No se puede crear un cliente sin Tipo de Cliente");
            return;
        }
        Client.ClientType = SelectedClientType;
        
        
        var response = string.IsNullOrEmpty(Client.Id)? await customerEndpoint.Create(Client) : await customerEndpoint.Update(Client);

        if (!response.IsSuccessful)
        {
            await popUpService.ShowErrorMessage(response.ErrorMessage);
            return;
        }

        await Shell.Current.GoToAsync("..");
    }
    [RelayCommand]
    static async Task Cancel()
    {
        await Shell.Current.GoToAsync("..");
    }
}
