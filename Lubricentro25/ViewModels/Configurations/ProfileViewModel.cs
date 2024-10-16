using CommunityToolkit.Mvvm.Input;
using Lubricentro25.Api.Interface;
using Lubricentro25.Controls.PopUps;
using Lubricentro25.Services.Interfaces;

namespace Lubricentro25.ViewModels.Configurations;

public partial class ProfileViewModel(IEmployeeEndpoint employeeEndpoint, IPopUpService popupService) : BaseViewModel
{
    [ObservableProperty]
    public Employee employee = new();
    protected override async Task LoadDataAsync()
    {
        var response = await employeeEndpoint.GetAllEmployees();
        if(!response.IsSuccessful)
        {
            await popupService.ShowErrorMessage(response.ErrorMessage);
            return;
        }
        try
        {
            Employee = response.ResponseContent.First(x => x.Email == Preferences.Get("CurrentUserEmail", ""));
        }
        catch (Exception)
        {
            Employee = new() { FirstName = "Empleado", LastName="De Prueba", Cuil="20423318806", Role= new() { Name = "Rol de prueba" } };
            await popupService.ShowErrorMessage("Este usuario no es un empleado.");
        }

    }
    [RelayCommand]
    async Task Update()
    {
        Employee emp = new()
        { 
            FirstName = Employee.FirstName,
            LastName = Employee.LastName,
            Cuil = Employee.Cuil
        };
        EmployeePopUp popUp = new() { BindingContext = emp };
        var accepted = await popupService.ShowPopUpAsync(popUp);
        if(!accepted)
        {
            return;
        }

        var response = await employeeEndpoint.UpdateEmployee(emp);
        if(!response.IsSuccessful)
        {
            await popupService.ShowErrorMessage(response.ErrorMessage);
            return;
        }

        Employee = response.ResponseContent.First();
    }
    [RelayCommand]
    async Task UpdateUser()
    {
        Employee emp = new(Employee); 
        EmployeePopUp popup = new() { BindingContext = emp };

        var accepted = await popupService.ShowPopUpAsync(popup);
        if(!accepted)
        {
            return;
        }

        var response = await employeeEndpoint.UpdateEmployee(emp);

        if(!response.IsSuccessful)
        {
            await popupService.ShowErrorMessage(response.ErrorMessage);
            return;
        }
        Employee = new(response.ResponseContent.First());
    }
    [RelayCommand]
    async Task ChangePassword()
    {
        NewPasswordPopUp popUp = new();
        bool accepted = await popupService.ShowPopUpAsync(popUp);
        if(!accepted) { return; }

        string newPassword = popUp.GetPassword();

        var response = await employeeEndpoint.ChangeEmployeePassword(Employee.Email, newPassword);
        if(!response.IsSuccessful)
        {
            await popupService.ShowErrorMessage(response.ErrorMessage);
            return;
        }

        await popupService.ShowMessage("Contraseña cambiada con éxito.");
    }
    [RelayCommand]
    async Task ChangeImage()
    {
        PickOptions options = new()
        {
            PickerTitle = "Seleccione una imagen."
        };

        var file = await PickAndShow(options);
        if (file is null) return;

        Employee.ImageSource = ImageSource.FromFile(file.FullPath);
        var response = await employeeEndpoint.UpdateEmployee(Employee);
        if (!response.IsSuccessful)
        {
            await popupService.ShowErrorMessage(response.ErrorMessage);
        }
    }
    public async Task<FileResult?> PickAndShow(PickOptions options)
    {
        try
        {
            var result = await FilePicker.Default.PickAsync(options);
            if (result != null)
            {
                if (result.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase) ||
                    result.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase))
                {
                    using var stream = await result.OpenReadAsync();
                    Employee.ImageSource = ImageSource.FromStream(() => stream);
                    
                }
            }
            return result;
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Seleccionador de Imagen", ex.GetBaseException().Message, "Aceptar");
        }

        return null;
    }
}
