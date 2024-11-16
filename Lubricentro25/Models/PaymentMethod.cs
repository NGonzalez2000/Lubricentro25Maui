namespace Lubricentro25.Models;

public partial class PaymentMethod : ObservableObject
{
    public string Id { get; set; }

    [ObservableProperty]
    string description;

    public PaymentMethod()
    {
        Id = string.Empty;
        Description = string.Empty;
    }
}
