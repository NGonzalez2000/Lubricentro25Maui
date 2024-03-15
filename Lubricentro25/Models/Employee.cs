namespace Lubricentro25.Models;

public partial class Employee: ObservableObject
{
    public Employee(Employee employee)
    {
        Id = employee.Id;
        FirstName = employee.FirstName;
        LastName = employee.LastName;
        Cuil = employee.Cuil;
        Email = employee.Email;
        Role = new(employee.Role);
        imagePath = employee.imagePath;
    }
    public Employee()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        Cuil = string.Empty;
        Email = string.Empty;
        Role = new();
        imagePath = "person.png";
    }
    
    public string Id { get; set; } = string.Empty;
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
    Role role;
    public string FullName => $"{FirstName} {LastName}";

}
