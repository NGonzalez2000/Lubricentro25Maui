using CommunityToolkit.Mvvm.Input;
using Lubricentro25.Api.Interface;
using Lubricentro25.Pages.DedicatedPages.ConfigurationPages;
using Lubricentro25.Services.Interfaces;
using System.Collections.ObjectModel;

namespace Lubricentro25.ViewModels.Configurations;

public partial class ConfigurationsViewModel(IVatTypeEndpoint vatTypeEndpoint, IBillEndpoint billEndpoint, IPaymentEndpoint paymentEndpoint, IPopUpService popupService) : BaseViewModel
{
    [ObservableProperty]
    ObservableCollection<VatType> vatTypes = null!;

    [ObservableProperty]
    ObservableCollection<BillType> billTypes = null!;

    [ObservableProperty]
    ObservableCollection<PaymentMethod> paymentMethods = null!;
    protected override async Task LoadDataAsync()
    {
        var paymentResponse = await paymentEndpoint.GetPaymentMethodsAsync();
        if(!paymentResponse.IsSuccessful)
        {
            await popupService.ShowErrorMessage(paymentResponse.ErrorMessage);
        }
        else
        {
            PaymentMethods = new(paymentResponse.ResponseContent);
        }

        var vatTypeResponse = await vatTypeEndpoint.GetAllAsync();
        if (!vatTypeResponse.IsSuccessful)
        {
            await popupService.ShowErrorMessage(vatTypeResponse.ErrorMessage);
        }
        else
        {
            VatTypes = new(vatTypeResponse.ResponseContent.OrderBy(vt => vt.Aliquota));
        }

        var billTypeResponse = await billEndpoint.GetBillTypeAsync();
        if (!billTypeResponse.IsSuccessful)
        {
            await popupService.ShowErrorMessage(billTypeResponse.ErrorMessage);
        }
        else
        {
            BillTypes = new(billTypeResponse.ResponseContent);
        }
    }

    [RelayCommand]
    async Task EditVatType(VatType vatType)
    {
        await Shell.Current.GoToAsync(nameof(SingleVatTypePage), new Dictionary<string, object>
        {
            [nameof(VatType)] = vatType
        });
    }

    [RelayCommand]
    async Task EditBillType(BillType billType)
    {
        await Shell.Current.GoToAsync(nameof(SingleBillTypePage), new Dictionary<string, object>
        {
            [nameof(BillType)] = billType
        });
    }

    [RelayCommand]
    async Task EditPaymentMethod(PaymentMethod paymentMethod)
    {
        await Shell.Current.GoToAsync(nameof(SinglePaymentMethodPage), new Dictionary<string, object>
        {
            [nameof(PaymentMethod)] = paymentMethod
        });
    }
}
