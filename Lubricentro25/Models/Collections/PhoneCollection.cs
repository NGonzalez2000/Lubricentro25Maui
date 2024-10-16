using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Lubricentro25.Models.Collections;

public partial class PhoneCollection : ObservableObject
{
    [ObservableProperty]
    bool hasPhoneNotificationEnable;

    [ObservableProperty]
    ObservableCollection<Phone> phones;

    public PhoneCollection()
    {
        phones = [];
    }

    [RelayCommand]
    void NewPhone()
    {
        Phones.Add(new Phone(Guid.Empty.ToString(),"54" , "", true));
    }
    [RelayCommand]
    void DeletePhone(Phone email)
    {
        Phones.Remove(email);
    }
    public override string ToString()
    {
        string ret = string.Empty;
        foreach (var email in Phones)
        {
            ret += string.IsNullOrEmpty(ret) ? email.Value : $"\n{email.Value}";
        }
        return ret;
    }
}
