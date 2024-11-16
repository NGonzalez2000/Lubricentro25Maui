using System.Collections.ObjectModel;

namespace Lubricentro25.Models;

public partial class Discount : ObservableObject
{
    public string Id { get; set; }
    [ObservableProperty]
    string description;
    [ObservableProperty]
    decimal firstDiscount;
    [ObservableProperty]
    decimal secondDiscount;
    [ObservableProperty]
    decimal thirdDiscount;
    [ObservableProperty]
    decimal fourthDiscount;
    [ObservableProperty]
    ObservableCollection<ClientTypeDiscount> clientTypeDiscounts;
    public Discount(IEnumerable<ClientType> clientTypes)
    {
        Id = string.Empty;
        description = string.Empty;
        ClientTypeDiscounts = [];

        foreach(ClientType clientType in clientTypes)
        {
            ClientTypeDiscounts.Add(new ClientTypeDiscount(clientType));
        }
    }

    public Discount()
    {
        Id = string.Empty;
        description = string.Empty;
        ClientTypeDiscounts = [];
    }
    public bool IsValid()
    {
        bool ret = true;
        ret &= FirstDiscount >= 0 && FirstDiscount <= 100;
        ret &= SecondDiscount >= 0 && SecondDiscount <= 100;
        ret &= ThirdDiscount >= 0 && ThirdDiscount <= 100;
        ret &= FourthDiscount >= 0 && FourthDiscount <= 100;
        return ret;
    }
    public override string ToString()
    {
        return $"{Description} {FirstDiscount}%+{SecondDiscount}%+{ThirdDiscount}%+{FourthDiscount}%";
    }
}
