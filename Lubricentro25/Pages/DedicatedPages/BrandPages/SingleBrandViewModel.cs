using CommunityToolkit.Mvvm.Input;
using Lubricentro25.Api.Interface;
using Lubricentro25.Models.Collections;
using Lubricentro25.Services.Interfaces;

namespace Lubricentro25.Pages.DedicatedPages.BrandPages;

[QueryProperty(nameof(IsEditingEnable), "Editing")]
[QueryProperty(nameof(Brand), nameof(Brand))]
public partial class SingleBrandViewModel(IPopUpService popUpService,
                                          IBrandEndpoint brandEndpoint,
                                          IProviderEndpoint providerEndpoint,
                                          IClientTypeEndpoint clientTypeEndpoint,
                                          IDiscountEndpoint discountEndpoint) : ObservableObject
{
    List<Discount> deletedDiscounts = [];
    List<ClientType>? clientTypes;

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

        if (Brand.Id == string.Empty) return;

        var discountsResponse = await discountEndpoint.GetBrandDiscounts(Brand);
        if (!discountsResponse.IsSuccessful)
        {
            await popUpService.ShowErrorMessage(discountsResponse.ErrorMessage);
            return;
        }

        Brand.Discounts = new(discountsResponse.ResponseContent.First().Where(d => d.Id != Guid.Empty.ToString()));
        deletedDiscounts.Clear();
    }

    [RelayCommand]
    async Task AddDiscount()
    {
        if(clientTypes is null)
        {
            var response = await clientTypeEndpoint.GetAllAsync();
            if (!response.IsSuccessful)
            {
                await popUpService.ShowErrorMessage(response.ErrorMessage);
                return;
            }
            clientTypes = new(response.ResponseContent.Where(ct => ct.Id != Guid.Empty.ToString()));
        }

        Brand.Discounts.Add(new Discount(clientTypes));
    }

    [RelayCommand]
    void Delete(Discount? discount)
    {
        if (discount is null) return;

        deletedDiscounts.Add(discount);
        Brand.Discounts.Remove(discount);
    }

    [RelayCommand]
    async Task Accept()
    {
        if (Brand is null) return;

        bool goodToGo = true;
        Brand.Providers = new(ProviderSelector.GetSelectedProviders());

        var response = string.IsNullOrEmpty(Brand.Id) ? await brandEndpoint.Create(Brand) : await brandEndpoint.Update(Brand);

        if (!response.IsSuccessful)
        {
            await popUpService.ShowErrorMessage(response.ErrorMessage);
            return;
        }

        if(string.IsNullOrEmpty(Brand.Id))
            Brand.Id = response.ResponseContent.First().Id;

        foreach(Discount discount in deletedDiscounts)
        {
            var deleteResponse = await discountEndpoint.Delete(discount, Brand);
            if(!response.IsSuccessful)
            {
                await popUpService.ShowErrorMessage(response.ErrorMessage);
                goodToGo = false;
            }
        }

        foreach(Discount discount in Brand.Discounts)
        {
            if(!discount.IsValid())
            {
                await popUpService.ShowMessage($"El decuento '{discount.Description}' no es Válido, los valores deben ser entre 0 y 100");
                continue;
            }
            var discountResponse = discount.Id == string.Empty ? await discountEndpoint.Create(discount, Brand) : await discountEndpoint.Update(discount);
            if(!discountResponse.IsSuccessful)
            {
                await popUpService.ShowErrorMessage(discountResponse.ErrorMessage);
                goodToGo = false;
            }
        }
        
        if(goodToGo) await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    static async Task Cancel()
    {
        await Shell.Current.GoToAsync("..");
    }
}
