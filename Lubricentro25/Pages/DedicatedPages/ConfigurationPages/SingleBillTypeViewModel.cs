using CommunityToolkit.Mvvm.Input;
using Lubricentro25.Api.Interface;
using Lubricentro25.Services.Interfaces;

namespace Lubricentro25.Pages.DedicatedPages.ConfigurationPages;

[QueryProperty(nameof(BillType), nameof(BillType))]
public partial class SingleBillTypeViewModel(IBillEndpoint billEndpoint, IPopUpService popUpService) : ObservableObject
{
    [ObservableProperty]
    BillType billType = null!;

    [RelayCommand]
    async Task Accept()
    {
        var resposne = await billEndpoint.UpdateBillTypeAsync(BillType);
        if(!resposne.IsSuccessful)
        {
            await popUpService.ShowErrorMessage(resposne.ErrorMessage);
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
