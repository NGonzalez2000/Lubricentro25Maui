using CommunityToolkit.Mvvm.Input;
using Lubricentro25.Api.Interface;
using Lubricentro25.Controls.PopUps;
using Lubricentro25.Pages.DedicatedPages.BrandPages;
using Lubricentro25.Services.Interfaces;
using System.Collections.ObjectModel;

namespace Lubricentro25.ViewModels.Items;

public partial class BrandViewModel(IBrandEndpoint brandEndpoint, IPopUpService popUpService) : BaseViewModel
{
    [ObservableProperty]
    string nameSearchText = string.Empty;
    [ObservableProperty]
    ObservableCollection<Brand> brands = [];

    [ObservableProperty]
    Brand? fullDetailsBrand;

    List<Brand> _brands = [];
    protected override async Task LoadDataAsync()
    {
        var response = await brandEndpoint.GetALlAsync();
        if (!response.IsSuccessful)
        {
            await popUpService.ShowMessage("No se pudieron cargar las Marcas");
            return;
        }
        _brands = new(response.ResponseContent);
        Brands = new(response.ResponseContent);
    }

    [RelayCommand]
    void FullDetails(Brand? selectedBrand)
    {
        if (selectedBrand is null)
        {
            return;
        }
        FullDetailsBrand = selectedBrand;
        IsSidePanelVisible = true;
    }

    [RelayCommand]
    async Task Create()
    {
        await Shell.Current.GoToAsync(nameof(SingleBrandPage), new Dictionary<string, object>()
        {
            ["Brand"] = new Brand(),
            ["Editing"] = true
        });
    }

    [RelayCommand]
    async Task Update(Brand? selectedBrand)
    {
        if (selectedBrand is null)
        {
            await popUpService.ShowErrorMessage("Seleccione una marca.");
            return;
        }

        await Shell.Current.GoToAsync(nameof(SingleBrandPage), new Dictionary<string, object>()
        {
            ["Brand"] = selectedBrand,
            ["Editing"] = true
        });
    }

    [RelayCommand]
    async Task Delete(Brand? selectedBrand)
    {
        if (selectedBrand is null)
        {
            await popUpService.ShowErrorMessage("Seleccione una Marca.");
            return;
        }

        bool accepted = await popUpService.ShowWarning($"Seguro que desea eliminar la Marca {selectedBrand.Name}?");

        if (!accepted)
        {
            return;
        }

        var response = await brandEndpoint.Delete(selectedBrand.Id);

        if (!response.IsSuccessful)
        {
            await popUpService.ShowErrorMessage(response.ErrorMessage);
            return;
        }

        _brands.Remove(selectedBrand);
        Search();
    }

    [RelayCommand]
    void Search()
    {
        Brands = new(_brands.Where(p => p.Name.Contains(NameSearchText)));
    }
}
