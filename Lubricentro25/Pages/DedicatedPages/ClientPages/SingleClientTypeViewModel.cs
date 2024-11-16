using CommunityToolkit.Mvvm.Input;
using Lubricentro25.Api.Endpoints;
using Lubricentro25.Api.Interface;
using Lubricentro25.Services.Interfaces;

namespace Lubricentro25.Pages.DedicatedPages.ClientPages
{
    [QueryProperty(nameof(IsEditingEnable), "Editing")]
    [QueryProperty(nameof(ClientType), nameof(ClientType))]
    public partial class SingleClientTypeViewModel(IClientTypeEndpoint clientTypeEndpoint, IPopUpService popUpService) : ObservableObject
    {
        [ObservableProperty]
        ClientType clientType = null!;

        [ObservableProperty]
        bool isEditingEnable;

        [RelayCommand]
        async Task Accept()
        {
            if (ClientType is null) return;
            var response = string.IsNullOrEmpty(ClientType.Id) ? await clientTypeEndpoint.Create(ClientType) : await clientTypeEndpoint.Update(ClientType);

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
}
