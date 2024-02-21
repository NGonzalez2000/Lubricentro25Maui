using CommunityToolkit.Mvvm.Input;
using Lubricentro25.Pages.Configuration.Views;
using Microsoft.Maui.Controls;
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
    public EmployeeConfigurationViewModel()
    {
        IsEnable = true;
        employeeEditorViewModel = new();
        employees = [];
        employeesList = [];
    }
    
    [RelayCommand]
    async Task NewEmployee()
    {
        IsEnable = false;
        var employee = await EmployeeEditorViewModel.CreateEmployee();
        if (employee != null)
        {
            Employees.Add(employee);
            employeesList.Add(employee);
        }
        IsEnable = true;
    }
    [RelayCommand]
    async Task EditEmployee()
    {
        if (!await CheckIfSelected()) return;
        
        IsEnable = false;
        
        var employee = await EmployeeEditorViewModel.UpdateEmployee(SelectedEmployee!);
        if (employee != null)
        {
            int indx = Employees.IndexOf(SelectedEmployee!);
            if(indx >= 0)
            {
                Employees[indx] = employee;
                SelectedEmployee = employee;
            }
        }
        
        IsEnable = true;
    }
    [RelayCommand]
    async Task DeleteEmployee()
    {
        if (!await CheckIfSelected()) return;

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
