using CommunityToolkit.Mvvm.Input;
using Lubricentro25.Api.Interface;
using Lubricentro25.Pages.DedicatedPages.ProductPages;
using Lubricentro25.Services.Interfaces;
using System.Collections.ObjectModel;

namespace Lubricentro25.ViewModels.Items;

public partial class ProductViewModel(IProductEndpoint productEndpoint, IPopUpService popUpService) : BaseViewModel
{
    private List<Product> productsList = null!;
    [ObservableProperty]
    string productCode = string.Empty;
    [ObservableProperty]
    string productBarcode = string.Empty;
    [ObservableProperty]
    string productDescription = string.Empty;

    [ObservableProperty]
    ObservableCollection<Product> products = null!;

    protected override async Task LoadDataAsync()
    {
        var response = await productEndpoint.GetALlAsync();
        if(!response.IsSuccessful)
        {
            await popUpService.ShowErrorMessage(response.ErrorMessage);
            return;
        }
        productsList = new(response.ResponseContent);
        Products = new(response.ResponseContent);
    }

    [RelayCommand]
    async Task Create()
    {
        await Shell.Current.GoToAsync(nameof(SingleProductPage), new Dictionary<string, object>()
        {
            ["Product"] = new Product(),
            ["Editing"] = true
        });
    }
    [RelayCommand]
    async Task Update(Product? selectedProduct)
    {
        if(selectedProduct is null)
        {
            await popUpService.ShowMessage("Debe seleccionar un producto.");
            return;
        }

        await Shell.Current.GoToAsync(nameof(SingleProductPage), new Dictionary<string, object>()
        {
            ["Product"] = selectedProduct,
            ["Editing"] = true
        });
    }
    [RelayCommand]
    async Task Delete(Product? selectedProduct)
    {
        if (selectedProduct is null)
        {
            await popUpService.ShowMessage("Debe seleccionar un producto.");
            return;
        }

        if (!await popUpService.ShowWarning($"Seguro que desea eliminar el producto {selectedProduct.Code} - {selectedProduct.Description} ?")) return;

        var response = await productEndpoint.Delete(selectedProduct);
        if(!response.IsSuccessful)
        {
            await popUpService.ShowErrorMessage(response.ErrorMessage);
            return;
        }

        productsList.Remove(selectedProduct);
        Search();
    }
    [RelayCommand]
    void Search()
    {
        Products = new(productsList.Where(p =>
        {
            return p.Code.Contains(ProductCode, StringComparison.CurrentCultureIgnoreCase)
            && p.Barcode.Contains(ProductBarcode, StringComparison.CurrentCultureIgnoreCase)
            && p.Description.Contains(ProductDescription, StringComparison.CurrentCultureIgnoreCase);
        }));
    }

}
