using CommunityToolkit.Mvvm.Input;
using Lubricentro25.Api.Interface;

namespace Lubricentro25.Pages.Configuration.Views;

public partial class EmployeeEditorViewModel : ObservableObject
{
    [ObservableProperty]
    ImageSource image;

    [ObservableProperty]
    Employee employee;
    
    [ObservableProperty]
    bool isBusy;

    [ObservableProperty]
    bool isEnable;

    [ObservableProperty]
    bool isCreating;

    [ObservableProperty]
    int selectedIndex;

    [ObservableProperty]
    List<Role> roles;

    TaskCompletionSource<Employee?>? employeeTaskCompletionSource;
    private readonly IRoleEndpoint _rolesApi;

    public EmployeeEditorViewModel(IRoleEndpoint rolesApi)
    {
        IsEnable = false;
        Employee = new();
        Image = Employee.ImageSource;
        Roles = [];
        _rolesApi = rolesApi;
    }

    [RelayCommand]
    async Task ChangeImage()
    {
        IsBusy = true;
        PickOptions options = new()
        {
            PickerTitle = "Seleccione una imagen."
        };

        var file = await PickAndShow(options);
        if (file is null) return;

        Employee.ImageSource = ImageSource.FromFile(file.FullPath);
        IsBusy = false;
    }
    [RelayCommand]
    void Cancel()
    {
        employeeTaskCompletionSource?.SetResult(null);
        Employee = new();
        IsEnable = false;
    }
    [RelayCommand]
    void Acept()
    {
        employeeTaskCompletionSource?.SetResult(Employee);
        Employee = new();
        IsEnable = false;
    }

    private async Task LoadRoles()
    {
        var response = await _rolesApi.GetAllRoles();
        if (!response.IsSuccessful)
        {
            Roles = [];
            await Shell.Current.DisplayAlert("Error", response.ErrorMessage, "Ok");
            return;
        }

        Roles = new(response.ResponseContent);
    }
    partial void OnSelectedIndexChanged(int value)
    {
        if(value >= 0 && value < Roles.Count)
        {
            Employee.Role = new(Roles[value]);
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
                    Image = ImageSource.FromStream(() => stream);
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

    //esta funcion deberia ser esperada por la pagina
    public async Task<Employee?> NewEmployee()
    {
        await LoadRoles();
        IsEnable = true;
        IsCreating = true;
        Employee = new();
        if (Roles.Count > 0)
        {
            Employee.Role = Roles[0];
            Employee = new(Employee);
            SelectedIndex = 0;
        }
        employeeTaskCompletionSource = new();
        return await employeeTaskCompletionSource.Task;
    }

    public async Task<Employee?> EditEmployee(Employee employee)
    {
        await LoadRoles();
        IsEnable = true;
        IsCreating = false;
        Employee = new(employee); 
        for(int i = 0; i < Roles.Count; i++)
        {
            if (Roles[i].Id == Employee.Role.Id)
            {
                SelectedIndex = i; 
                break;
            }
        }
        employeeTaskCompletionSource = new();
        return await employeeTaskCompletionSource.Task;
    }
}
