namespace Lubricentro25.Models.Stock;

public partial class StockLocation : ObservableObject
{
    public string Id { get; set; }
 
    [ObservableProperty]
    string x;
    
    [ObservableProperty]
    string y;
    
    [ObservableProperty]
    string z;

    public StockLocation()
    {
        Id = string.Empty;
        X = string.Empty;
        Y = string.Empty;
        Z = string.Empty;
    }

    public override string ToString()
    {
        return $"{X} - {Y} - {Z}";
    }
}
