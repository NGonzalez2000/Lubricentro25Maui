using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Lubricentro25.Models.Collections;

public partial class EmailCollection : ObservableObject
{
    [ObservableProperty]
    bool hasEmailNotificationEnable;

    [ObservableProperty]
    ObservableCollection<Email> emails;
    public EmailCollection()
    {
        emails = [];
    }

    [RelayCommand]
    void NewEmail()
    {
        Emails.Add(new Email(Guid.Empty.ToString(), "", true));
    }
    [RelayCommand]
    void DeleteEmail(Email email)
    {
        Emails.Remove(email);
    }
    public override string ToString()
    {
        string ret = string.Empty;
        foreach (var email in Emails)
        {
            ret += string.IsNullOrEmpty(ret) ? email.Value : $"\n{email.Value}";
        }
        return ret;
    }
}
