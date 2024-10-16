using CommunityToolkit.Mvvm.Input;
using Lubricentro25.Api.Interface;
using Lubricentro25.Services.Interfaces;

namespace Lubricentro25.Pages.DedicatedPages.ProviderPages;

[QueryProperty(nameof(IsEditingEnable), "Editing")]
[QueryProperty(nameof(Provider), nameof(Provider))]
public partial class SingleProviderViewModel(IPopUpService popUpService, IProviderEndpoint providerEndpoint, ITaxConditionEndpoint taxConditionEndpoint) : ObservableObject
{
    [ObservableProperty]
    Provider provider = null!;
    
    [ObservableProperty]
    bool isEditingEnable;

    [ObservableProperty]
    List<TaxCondition> taxConditions = [];

    [ObservableProperty]
    TaxCondition? selectedTaxCondition;

    partial void OnTaxConditionsChanged(List<TaxCondition> value)
    {
        if (value.Count == 0) return;
        if (Provider is not null)
        {
            SelectedTaxCondition = TaxConditions.FirstOrDefault(tx => tx.Id == Provider.TaxCondition.Id, value[0]);
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
        if (Provider is not null)
        {
            SelectedTaxCondition = TaxConditions.FirstOrDefault(tx => tx.Id == Provider.TaxCondition.Id, TaxConditions[0]);
        }
    }
    [RelayCommand]
    async Task Accept()
    {
        if (Provider is null) return;
        if (SelectedTaxCondition is null)
        {
            await popUpService.ShowErrorMessage("No se puede crear un cliente sin Cond. Fiscal");
            return;
        }

        Provider.TaxCondition = SelectedTaxCondition;
        var response = string.IsNullOrEmpty(Provider.Id) ? await providerEndpoint.Create(Provider) : await providerEndpoint.Update(Provider);

        if (!response.IsSuccessful)
        {
            await popUpService.ShowErrorMessage(response.ErrorMessage);
            return;
        }

        await Shell.Current.GoToAsync("..");
    }
    [RelayCommand]
    async Task Cancel()
    {
        await Shell.Current.GoToAsync("..");
    }
}
