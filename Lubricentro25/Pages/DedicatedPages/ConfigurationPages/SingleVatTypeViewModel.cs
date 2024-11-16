using CommunityToolkit.Mvvm.Input;
using Lubricentro25.Api.Endpoints;
using Lubricentro25.Api.Interface;
using Lubricentro25.Services.Interfaces;

namespace Lubricentro25.Pages.DedicatedPages.ConfigurationPages;

[QueryProperty(nameof(VatType), nameof(VatType))]
public partial class SingleVatTypeViewModel(IVatTypeEndpoint vatTypeEndpoint, IPopUpService popUpService) : ObservableObject
{
    [ObservableProperty]
    VatType vatType = null!;

    [RelayCommand]
    async Task Accept()
    {
        if (VatType is null) return;
        var response = await vatTypeEndpoint.UpdateVatTypeAsync(VatType);

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
