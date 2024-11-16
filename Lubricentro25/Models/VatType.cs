namespace Lubricentro25.Models;

public partial class VatType : ObservableObject
{
    public string Id { get; set; }

    [ObservableProperty]
    string description;

    [ObservableProperty]
    decimal aliquota;

    [ObservableProperty]
    int afipCode;

    public VatType()
    {
        Id = string.Empty;
        description = string.Empty;
    }
}
