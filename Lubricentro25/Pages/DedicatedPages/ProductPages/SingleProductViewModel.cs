using CommunityToolkit.Mvvm.Input;
using Lubricentro25.Api.Endpoints;
using Lubricentro25.Api.Interface;
using Lubricentro25.Services.Interfaces;

namespace Lubricentro25.Pages.DedicatedPages.ProductPages;

[QueryProperty(nameof(IsEditingEnable), "Editing")]
[QueryProperty(nameof(Product), nameof(Product))]
public partial class SingleProductViewModel(IPopUpService popUpService, IProductEndpoint productEndpoint, IBrandEndpoint brandEndpoint) : ObservableObject
{
    [ObservableProperty]
    bool isEditingEnable;

    [ObservableProperty]
    List<Brand> brands = [];

    [ObservableProperty]
    Brand? selectedBrand;

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
        Product.Brand = SelectedBrand;
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
