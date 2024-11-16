using CommunityToolkit.Mvvm.Input;
using Lubricentro25.Api.Interface;
using Lubricentro25.Services.Interfaces;

namespace Lubricentro25.Pages.DedicatedPages.ConfigurationPages;

[QueryProperty(nameof(PaymentMethod), nameof(PaymentMethod))]
public partial class SinglePaymentMethodViewModel(IPaymentEndpoint paymentEndpoint, IPopUpService popUpService) : ObservableObject
{
    [ObservableProperty]
    PaymentMethod paymentMethod = null!;

    [RelayCommand]
    async Task Accept()
    {
        if (PaymentMethod is null) return;
        var resposne = await paymentEndpoint.UpdatePaymentMethodAsync(PaymentMethod);
        if (!resposne.IsSuccessful)
        {
            await popUpService.ShowErrorMessage(resposne.ErrorMessage);
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
