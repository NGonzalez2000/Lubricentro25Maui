using CommunityToolkit.Mvvm.Input;
using Lubricentro25.Api.Interface;
using Lubricentro25.Controls.Filters;
using Lubricentro25.Services.Interfaces;

namespace Lubricentro25.Controls.Filters.SellItems;

public partial class SellItemFilterViewModel(IBrandEndpoint brandEndpoint, IPopUpService popUpService) : ObservableObject, ICollectionFilter<SellItem>
{
    [ObservableProperty]
    List<Brand> brands = null!;

    [ObservableProperty]
    Brand? selectedBrand;

    [ObservableProperty]
    string code = string.Empty;

    [ObservableProperty]
    string barcode = string.Empty;

    [ObservableProperty]
    string description = string.Empty;

    public async Task Load()
    {
        var response = await brandEndpoint.GetALlAsync();

        if (!response.IsSuccessful)
        {
            await popUpService.ShowErrorMessage(response.ErrorMessage);
            Brands = [];
            return;
        }

        Brands = new(response.ResponseContent)
        {
            new() { Name = "TODAS LAS MARCAS" }
        };

        SelectedBrand = Brands.First(b => b.Name == "TODAS LAS MARCAS");
    }

    public bool Filter(SellItem item)
    {
        bool ret = true;
        if(SelectedBrand is not null && SelectedBrand.Name != "TODAS LAS MARCAS") ret &= item.BrandName == SelectedBrand.Name;
        if(!string.IsNullOrEmpty(Code)) ret &= item.Code.Contains(Code);
        if(!string.IsNullOrEmpty(Barcode)) ret &= item.Barcode.Contains(Barcode);
        if(!string.IsNullOrEmpty(Description)) ret &= item.Description.Contains(Description);
        return ret;
    }

    [RelayCommand]
    void Clear()
    {
        SelectedBrand = Brands.FirstOrDefault(b => b.Name == "TODAS LAS MARCAS");
        Code = string.Empty;
        Barcode = string.Empty;
        Description = string.Empty;
    }
}
