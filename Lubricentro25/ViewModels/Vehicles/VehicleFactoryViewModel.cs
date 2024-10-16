using CommunityToolkit.Mvvm.Input;
using Lubricentro25.Api.Interface;
using Lubricentro25.Models.Vehicles;
using Lubricentro25.Services.Interfaces;
using System.Collections.ObjectModel;

namespace Lubricentro25.ViewModels.Vehicles;

public partial class VehicleFactoryViewModel(IVehicleFactoryEndpoint vehicleFactoryEndpoint, IVehicleModelEndpoint vehicleModelEndpoint, IPopUpService popUpService) : BaseViewModel
{
    [ObservableProperty]
    ObservableCollection<VehicleFactory> vehicleFactories = null!;

    [ObservableProperty]
    VehicleFactory? selecetedVehicleFactory;

    [ObservableProperty]
    string vehicleFactoryName = string.Empty;

    [ObservableProperty]
    string vehicleModelName = string.Empty;

    List<VehicleFactory> factories = [];
    List<VehicleModel> models = [];

    partial void OnSelecetedVehicleFactoryChanged(VehicleFactory? value)
    {
        if(value is not null)
        {
            value.Models = new(value.Models.OrderBy(x => x.Name));
        }
    }

    protected override async Task LoadDataAsync()
    {
        var response = await vehicleFactoryEndpoint.GetALlAsync();
        if (!response.IsSuccessful)
        {
            await popUpService.ShowErrorMessage(response.ErrorMessage);
            return;
        }

        factories = new(response.ResponseContent.OrderBy(c => c.Name));
        VehicleFactories = new(factories);
    }

    [RelayCommand]
    async Task Create()
    {
        string? newName = await Shell.Current.DisplayPromptAsync("NUEVO FABRICANTE", "", "ACEPTAR", "CANCELAR");
        if(string.IsNullOrEmpty(newName)) return;

        VehicleFactory vehicleFactory = new() { Name = newName };
        var response = await vehicleFactoryEndpoint.Create(vehicleFactory);
        if(!response.IsSuccessful)
        {
            await popUpService.ShowErrorMessage(response.ErrorMessage);
            return;
        }

        factories.Add(vehicleFactory);
        Search();
    }

    [RelayCommand]
    async Task Update(VehicleFactory? selectedVehicleFactory)
    {
        if(selectedVehicleFactory is null)
        {
            await popUpService.ShowMessage("DEBE SELECCIONAR UN FABRICANTE.");
            return;
        }
        string? newName = await Shell.Current.DisplayPromptAsync("EDITAR FABRICANTE", "", "ACEPTAR", "CANCELAR",initialValue: selectedVehicleFactory.Name);
        if (string.IsNullOrEmpty(newName)) return;

        VehicleFactory vehicleFactory = new()
        {
            Id = selectedVehicleFactory.Id,
            Name = newName 
        };
        var response = await vehicleFactoryEndpoint.Update(vehicleFactory);
        if (!response.IsSuccessful)
        {
            await popUpService.ShowErrorMessage(response.ErrorMessage);
            return;
        }

        await LoadDataAsync();
    }

    [RelayCommand]
    async Task Delete(VehicleFactory? selectedVehicleFactory)
    {
        if (selectedVehicleFactory is null)
        {
            await popUpService.ShowErrorMessage("Seleccione un fabricante.");
            return;
        }

        bool accepted = await popUpService.ShowWarning($"Seguro que desea eliminar al fabricante {selectedVehicleFactory.Name}?");

        if (!accepted)
        {
            return;
        }

        var response = await vehicleFactoryEndpoint.Delete(selectedVehicleFactory);

        if (!response.IsSuccessful)
        {
            await popUpService.ShowErrorMessage(response.ErrorMessage);
            return;
        }

        await LoadDataAsync();
    }

    [RelayCommand]
    async Task CreateModel()
    {
        if(SelecetedVehicleFactory is null)
        {
            return;
        }

        string? newName = await Shell.Current.DisplayPromptAsync("NUEVO MODELO", "", "ACEPTAR", "CANCELAR");
        if (string.IsNullOrEmpty(newName)) return;

        VehicleModel vehicleModel = new() { Name = newName };

        var response = await vehicleModelEndpoint.Create(SelecetedVehicleFactory, vehicleModel);
        if (!response.IsSuccessful)
        {
            await popUpService.ShowErrorMessage(response.ErrorMessage);
            return;
        }

        SelecetedVehicleFactory.Models.Add(vehicleModel);
        Search();
    }

    [RelayCommand]
    async Task UpdateModel(VehicleModel? selectedVehicleModel)
    {
        if (SelecetedVehicleFactory is null)
        {
            return;
        }
        if (selectedVehicleModel is null)
        {
            await popUpService.ShowMessage("DEBE SELECCIONAR UN MODELO.");
            return;
        }
        string? newName = await Shell.Current.DisplayPromptAsync("EDITAR MODELO", "", "ACEPTAR", "CANCELAR", initialValue: selectedVehicleModel.Name);
        if (string.IsNullOrEmpty(newName)) return;

        VehicleModel vehicleModel = new()
        {
            Id = selectedVehicleModel.Id,
            Name = newName
        };
        var response = await vehicleModelEndpoint.Update(vehicleModel);
        if (!response.IsSuccessful)
        {
            await popUpService.ShowErrorMessage(response.ErrorMessage);
            return;
        }

        await LoadDataAsync();
    }

    [RelayCommand]
    async Task DeleteModel(VehicleModel? selectedVehicleModel)
    {
        if(SelecetedVehicleFactory is null)
        {
            return;
        }
        if (selectedVehicleModel is null)
        {
            await popUpService.ShowErrorMessage("Seleccione un modelo.");
            return;
        }

        bool accepted = await popUpService.ShowWarning($"Seguro que desea eliminar al modelo {selectedVehicleModel.Name}?");

        if (!accepted)
        {
            return;
        }

        var response = await vehicleModelEndpoint.Delete(selectedVehicleModel);

        if (!response.IsSuccessful)
        {
            await popUpService.ShowErrorMessage(response.ErrorMessage);
            return;
        }

        await LoadDataAsync();
    }

    [RelayCommand]
    void Search()
    {
        VehicleFactories = new(factories.Where(f => f.Name.Contains(VehicleFactoryName)));
    }

}
