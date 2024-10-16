using CommunityToolkit.Mvvm.Input;
using Lubricentro25.Api.Interface;
using Lubricentro25.Models.Collections;
using Lubricentro25.Services.Interfaces;

namespace Lubricentro25.Pages.DedicatedPages.BrandPages;

[QueryProperty(nameof(IsEditingEnable), "Editing")]
[QueryProperty(nameof(Brand), nameof(Brand))]
public partial class SingleBrandViewModel(IPopUpService popUpService, IBrandEndpoint brandEndpoint, IProviderEndpoint providerEndpoint) : ObservableObject
{
    [ObservableProperty]
    Brand brand = null!;

    [ObservableProperty]
    ProviderSelector providerSelector = null!;

    [ObservableProperty]
    bool isEditingEnable;

    public async Task Refresh()
    {
        var response = await providerEndpoint.GetAll();

        if (!response.IsSuccessful)
        {
            await popUpService.ShowErrorMessage(response.ErrorMessage);
            return;
        }
        ProviderSelector = new(response.ResponseContent, Brand.Providers.ToList() ?? []);
    }

    [RelayCommand]
    async Task Accept()
    {
        if (Brand is null) return;

        Brand.Providers = new(ProviderSelector.GetSelectedProviders());

        var response = string.IsNullOrEmpty(Brand.Id) ? await brandEndpoint.Create(Brand) : await brandEndpoint.Update(Brand);

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
