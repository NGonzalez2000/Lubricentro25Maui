using CommunityToolkit.Mvvm.Input;
using Lubricentro25.Api.Interface;
using Lubricentro25.Pages.DedicatedPages.ProductPages;
using Lubricentro25.Services.Interfaces;
using System.Collections.ObjectModel;

namespace Lubricentro25.ViewModels.Items;

[QueryProperty(nameof(SearchCode), "ProductCode")]
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

    [ObservableProperty]
    string? searchCode;
    [ObservableProperty]
    Product? scrollToProduct;

    private bool loaded = false;
    async partial void OnSearchCodeChanged(string? value)
    {
        if (value is null) return;
        if(!loaded)
        {
            await LoadDataAsync();
            loaded = true;
        }
        ScrollToProduct = Products.FirstOrDefault(p => p.Code == SearchCode);
    }

    protected override async Task LoadDataAsync()
    {
        if(loaded)
        {
            loaded = false;
            return;
        }
        var response = await productEndpoint.GetAllAsync();
        if(!response.IsSuccessful)
        {
            await popUpService.ShowErrorMessage(response.ErrorMessage);
            return;
        }
        productsList = new(response.ResponseContent);
        Products = new(response.ResponseContent);

        loaded = true;
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

    [RelayCommand]
    async Task SeeStock(Product? product)
    {
        if (product is null)
        {
            await popUpService.ShowErrorMessage("Se perdió la referencia al Articulo.");
            return;
        }

        await Shell.Current.GoToAsync($"//Products/Stock?StockItemCode={product.Code}");
    }

    [RelayCommand]
    async Task Details(Product? product)
    {
        if (product is null)
        {
            await popUpService.ShowWarning("Se perdió la referencia del producto");
            return;
        }
        await Shell.Current.GoToAsync($"{nameof(SingleProductPage)}?Editing=false", new Dictionary<string, object>
        {
            ["Product"] = product
        });

    }

}
