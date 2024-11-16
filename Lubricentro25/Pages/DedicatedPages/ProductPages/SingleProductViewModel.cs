using CommunityToolkit.Mvvm.Input;
using Lubricentro25.Api.Endpoints;
using Lubricentro25.Api.Interface;
using Lubricentro25.Services.Interfaces;

namespace Lubricentro25.Pages.DedicatedPages.ProductPages;

[QueryProperty(nameof(IsEditingEnable), "Editing")]
[QueryProperty(nameof(Product), nameof(Product))]
public partial class SingleProductViewModel(IPopUpService popUpService, IProductEndpoint productEndpoint, IBrandEndpoint brandEndpoint, IDiscountEndpoint discountEndpoint, IVatTypeEndpoint vatTypeEndpoint) : ObservableObject
{
    [ObservableProperty]
    bool isEditingEnable;

    [ObservableProperty]
    List<Brand> brands = [];

    [ObservableProperty]
    Brand? selectedBrand;

    [ObservableProperty]
    List<Discount> discounts = [];

    [ObservableProperty]
    Discount? selectedDiscount;

    [ObservableProperty]
    List<VatType> vatTypes = [];

    [ObservableProperty]
    VatType? selectedVatType;

    [ObservableProperty]
    Product? product;

    partial void OnBrandsChanged(List<Brand> value)
    {
        if (value.Count == 0) return;
        if (Product is not null)
        {
            SelectedBrand = Brands.FirstOrDefault(b => b.Id == Product.Brand.Id, value[0]);
        }
    }

    partial void OnSelectedDiscountChanged(Discount? value)
    {
        if (value is null || Product is null) return;
        Product.Discount = value;
    }

    partial void OnSelectedVatTypeChanged(VatType? value)
    {
        if (value is null || Product is null) return;
        Product.VatType = value;
    }

    async partial void OnSelectedBrandChanged(Brand? value)
    {
        if(value is null) return;

        var response = await discountEndpoint.GetBrandDiscounts(value);

        if(!response.IsSuccessful)
        {
            await popUpService.ShowErrorMessage(response.ErrorMessage);
            return;
        }

        Discounts = new(response.ResponseContent.First());
        if(Product is not null)
        {
            SelectedDiscount = Discounts.FirstOrDefault(d => d.Id == Product.Discount.Id, Discounts[0]);
        }
    }
    public async Task Refresh()
    {
        var response = await brandEndpoint.GetALlAsync();

        if (!response.IsSuccessful)
        {
            await popUpService.ShowErrorMessage(response.ErrorMessage);
            return;
        }
        Brands = new(response.ResponseContent);

        if (Brands.Count == 0) return;
        if (Product is not null)
        {
            SelectedBrand = Brands.FirstOrDefault(b => b.Id == Product.Brand.Id, Brands[0]);
        }

        var vatTypeResponse = await vatTypeEndpoint.GetAllAsync();
        if (!vatTypeResponse.IsSuccessful)
        {
            await popUpService.ShowErrorMessage(vatTypeResponse.ErrorMessage);
            return;
        }

        VatTypes = new(vatTypeResponse.ResponseContent);

        if (VatTypes.Count > 0) SelectedVatType = VatTypes.FirstOrDefault(v => v.Aliquota == 21m, VatTypes[0]);

    }

    [RelayCommand]
    async Task Accept()
    {
        if (Product is null) return;
        if (SelectedBrand is null)
        {
            await popUpService.ShowErrorMessage("No se puede crear un producto sin Marca");
            return;
        }
        if (SelectedDiscount is null)
        {
            await popUpService.ShowErrorMessage("No se puede crear un producto sin Descuento");
            return;
        }

        if (SelectedVatType is null)
        {
            await popUpService.ShowErrorMessage("No se puede crear un producto sin Tipo de Iva");
            return;
        }
        Product.Brand = SelectedBrand;
        Product.Discount = SelectedDiscount;
        Product.VatType = SelectedVatType;
        var response = string.IsNullOrEmpty(Product.Id) ? await productEndpoint.Create(Product) : await productEndpoint.Update(Product);

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
