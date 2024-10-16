using CommunityToolkit.Mvvm.Input;
using Lubricentro25.Api;
using Lubricentro25.Api.Interface;
using Lubricentro25.Services.Interfaces;

namespace Lubricentro25.Pages.DedicatedPages.ClientPages;

[QueryProperty(nameof(IsEditingEnable),"Editing")]
[QueryProperty(nameof(Client), nameof(Client))]
public partial class SingleClientViewModel(IPopUpService popUpService, ICustomerEndpoint customerEndpoint, ITaxConditionEndpoint taxConditionEndpoint) : ObservableObject
{
    [ObservableProperty]
    bool isEditingEnable;

    [ObservableProperty]
    List<TaxCondition> taxConditions = [];

    [ObservableProperty]
    TaxCondition? selectedTaxCondition;

    [ObservableProperty]
    Client? client;

    partial void OnTaxConditionsChanged(List<TaxCondition> value)
    {
        if (value.Count == 0) return;
        if(Client is not null)
        {
            SelectedTaxCondition = TaxConditions.FirstOrDefault(tx => tx.Id == Client.TaxCondition.Id, value[0]);
        }
    }

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
