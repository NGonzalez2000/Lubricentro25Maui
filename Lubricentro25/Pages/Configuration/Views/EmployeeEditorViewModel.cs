using CommunityToolkit.Mvvm.Input;

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
    List<string> rols;

    TaskCompletionSource<Employee?>? employeeTaskCompletionSource;
    public EmployeeEditorViewModel()
    {
        IsEnable = false;
        Employee = new();
        Image = ImageSource.FromFile(Employee.ImagePath);
        rols = ["Admin", "Master", "Empleado"];
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
    public Task<Employee?> CreateEmployee()
    {
        IsEnable = true;
        employeeTaskCompletionSource = new();
        return employeeTaskCompletionSource.Task;
    }

    public Task<Employee?> UpdateEmployee(Employee employee)
    {
        IsEnable = true;
        Employee = new(employee); 
        employeeTaskCompletionSource = new();
        return employeeTaskCompletionSource.Task;
    }
}
