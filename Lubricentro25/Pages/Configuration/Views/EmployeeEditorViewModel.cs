using CommunityToolkit.Mvvm.Input;
using Lubricentro25.Api;

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
    List<Role> roles;

    TaskCompletionSource<Employee?>? employeeTaskCompletionSource;
    private readonly ILubricentroApiClient _clientApi;

    public EmployeeEditorViewModel(ILubricentroApiClient clientApi)
    {
        IsEnable = false;
        Employee = new();
        Image = ImageSource.FromFile(Employee.ImagePath);
        Roles = [];
        _clientApi = clientApi;
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

        Employee.ImagePath = file.FullPath;
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
        var response = await _clientApi.GetAllRoles();
        if (response is null)
        {
            Roles = [];
            await Shell.Current.DisplayAlert("Error", "No se pudieron cargar los roles.", "Ok");
            return;
        }

        Roles = new(response);
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
    public async Task<Employee?> CreateEmployee()
    {
        await LoadRoles();
        IsEnable = true;
        employeeTaskCompletionSource = new();
        return await employeeTaskCompletionSource.Task;
    }

    public async Task<Employee?> UpdateEmployee(Employee employee)
    {
        await LoadRoles();
        IsEnable = true;
        Employee = new(employee); 
        employeeTaskCompletionSource = new();
        return await employeeTaskCompletionSource.Task;
    }
}
