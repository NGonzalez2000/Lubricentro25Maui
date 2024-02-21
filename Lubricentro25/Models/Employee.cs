namespace Lubricentro25.Models;

public partial class Employee: ObservableObject
{
    public Employee(Employee employee)
    {
        FirstName = employee.FirstName;
        LastName = employee.LastName;
        Cuil = employee.Cuil;
        Email = employee.Email;
        Rol = employee.Rol;
        imagePath = employee.imagePath;
    }
    public Employee()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        Cuil = string.Empty;
        Email = string.Empty;
        Rol = string.Empty;
        imagePath = "person.png";
    }
    
    public readonly Guid Id;
    [ObservableProperty]
    string firstName;
    [ObservableProperty]
    string lastName;
    [ObservableProperty]
    string cuil;
    [ObservableProperty]
    string email;
    [ObservableProperty]
    string? imagePath;
    [ObservableProperty]
    string rol;
    public string FullName => $"{FirstName} {LastName}";

}
