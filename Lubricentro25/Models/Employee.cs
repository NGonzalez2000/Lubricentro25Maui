using Lubricentro25.Services;

namespace Lubricentro25.Models;

public partial class Employee: ObservableObject
{
    private byte[]? data;
    public byte[]? Data
    {
        get => data;
        set
        {
            data = value;
            if(data != null)
            {
                ImageSource = ImageParser.BytesToImageSource(data);
            }
            else
            {
                ImageSource = ImageSource.FromFile("person.png");
            }
        }
    }
    public Employee(Employee employee)
    {
        Id = employee.Id;
        FirstName = employee.FirstName;
        LastName = employee.LastName;
        Cuil = employee.Cuil;
        Email = employee.Email;
        Role = new(employee.Role);
        ImageSource = employee.ImageSource;
    }
    public Employee()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        Cuil = string.Empty;
        Email = string.Empty;
        Role = new();
        ImageSource = ImageSource.FromFile("person.png");
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
    ImageSource imageSource;
    [ObservableProperty]
    Role role;
    public string FullName => $"{FirstName} {LastName}";
    

}
