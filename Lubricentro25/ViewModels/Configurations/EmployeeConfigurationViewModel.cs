using CommunityToolkit.Mvvm.Input;
using Lubricentro25.Api.Interface;
using Lubricentro25.Pages.Configuration.Views;
using System.Collections.ObjectModel;

namespace Lubricentro25.ViewModels.Configurations;

public partial class EmployeeConfigurationViewModel : ObservableObject
{
    [ObservableProperty]
    bool isEnable;

    [ObservableProperty]
    ObservableCollection<Employee> employees;
    List<Employee> employeesList;

    [ObservableProperty]
    Employee? selectedEmployee;

    [ObservableProperty]
    EmployeeEditorViewModel employeeEditorViewModel;
    private readonly IEmployeeEndpoint _employeeClient;

    public EmployeeConfigurationViewModel(IEmployeeEndpoint employeeEndpoint, EmployeeEditorViewModel employeeEditorViewModel)
    {
        EmployeeEditorViewModel = employeeEditorViewModel;
        IsEnable = true;
        employees = [];
        employeesList = [];
        _employeeClient = employeeEndpoint;
    }
    
    [RelayCommand]
    private async Task Load()
    {
        var apiResponse = await _employeeClient.GetAllEmployees();
        if(!apiResponse.IsSuccessful)
        {
            await Shell.Current.DisplayAlert("Error", apiResponse.ErrorMessage, "Ok");
            return;
        }
        employeesList = new(apiResponse.ResponseContent);
        Employees = new(employeesList);
    }

    [RelayCommand]
    async Task NewEmployee()
    {
        IsEnable = false;
        var employee = await EmployeeEditorViewModel.NewEmployee();
        IsEnable = true;
        if (employee != null)
        {
            var response = await _employeeClient.CreateEmployee(employee);
            if (!response.IsSuccessful)
            {
                await Shell.Current.DisplayAlert("Error", response.ErrorMessage, "Ok");
                return;
            }
            employeesList.Add(response.ResponseContent.First());
            Search("");
        }
    }
    [RelayCommand]
    async Task EditEmployee()
    {
        if (!await CheckIfSelected()) return;
        
        IsEnable = false;
        
        var employee = await EmployeeEditorViewModel.EditEmployee(SelectedEmployee!);
        IsEnable = true;
        if (employee != null)
        {
            var response = await _employeeClient.UpdateEmployee(employee);
            
            if(!response.IsSuccessful)
            {
                await Shell.Current.DisplayAlert("Error", response.ErrorMessage,"Ok");
                return;
            }
         
            int indx = employeesList.IndexOf(SelectedEmployee!);
            if(indx >= 0)
            {
                employeesList[indx] = new(response.ResponseContent.First());
                Search("");
            }
        }
        
    }
    [RelayCommand]
    async Task DeleteEmployee()
    {
        if (!await CheckIfSelected()) return;

        ;

        if (!await Shell.Current.DisplayAlert("Eliminar Empleado", $"Seguro desea eliminar al empleado: {SelectedEmployee!.FullName} ?", "Aceptar", "Cancelar")) return;
        var response = await _employeeClient.DeleteEmployee(SelectedEmployee.Id);

        if(!response.IsSuccessful)
        {
            await Shell.Current.DisplayAlert("Error", response.ErrorMessage, "Ok");
            return;
        }

        int indx = employeesList.IndexOf(SelectedEmployee);
        if (indx >= 0)
        {
            employeesList.Remove(employeesList[indx]);
            Search("");
        }
        Employees.Remove(SelectedEmployee!);
    }
    [RelayCommand]
    void Search(string fullName)
    {
        Employees = new(employeesList.Where(x => x.FullName.Contains(fullName, StringComparison.OrdinalIgnoreCase)));
    }

    async Task<bool> CheckIfSelected()
    {
        if (SelectedEmployee is null)
        {
            await Shell.Current.DisplayAlert("Empleado nulo", "Seleccione un Empleado.", "Aceptar");
            return false;
        }
        return true;
    }
}
