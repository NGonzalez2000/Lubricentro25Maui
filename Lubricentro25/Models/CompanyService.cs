namespace Lubricentro25.Models;
public partial class CompanyService : ObservableObject
{
    public string Id { get; set; } = string.Empty;
    [ObservableProperty]
    string name = string.Empty;
    [ObservableProperty]
    Company company = new();
}
