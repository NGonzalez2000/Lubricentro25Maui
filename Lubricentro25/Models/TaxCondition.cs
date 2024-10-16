namespace Lubricentro25.Models;

public partial class TaxCondition : ObservableObject
{
    public string Id { get; set; }
    [ObservableProperty]
    private string description;
    [ObservableProperty]
    private char type;
    [ObservableProperty]
    private bool vat;

    public TaxCondition()
    {
        Id = string.Empty;
        Description = string.Empty;
        Type = 'A' ;
        Vat = false;
    }
    public TaxCondition(string id, string taxConditionDescription, char taxConditionType, bool taxConditionVAT)
    {
        Id = id;
        description = taxConditionDescription;
        type = taxConditionType;
        vat = taxConditionVAT;
    }

    public TaxCondition(string taxConditionDescription, char taxConditionType, bool taxConditionVAT)
    {
        Id = string.Empty;
        description = taxConditionDescription;
        type = taxConditionType;
        vat = taxConditionVAT;
    }

    public TaxCondition Clone()
        => (TaxCondition)MemberwiseClone();
    
}
