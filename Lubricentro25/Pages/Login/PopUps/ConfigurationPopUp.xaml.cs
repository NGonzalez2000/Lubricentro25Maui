using CommunityToolkit.Maui.Views;
using Lubricentro25.Models.Helpers;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace Lubricentro25.Pages.Login.PopUps;

public partial class ConfigurationPopUp : Popup
{
	public ConfigurationPopUp()
	{
		InitializeComponent();
        Opened += ConfigurationPopUp_Opened;
	}

    private void ConfigurationPopUp_Opened(object? sender, CommunityToolkit.Maui.Core.PopupOpenedEventArgs e)
    {
        apiAddressEntry.Text = Preferences.Get(nameof(AddressConfigurationHelper.ApiAddress), "");
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
		
		Preferences.Set(nameof(AddressConfigurationHelper.ApiAddress), apiAddressEntry.Text);
        //string filePath = Path.Combine(AppContext.BaseDirectory, "Resources/Data/appsettings.json");
        try
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            string customFolderPath = Path.Combine(appDataPath, "Lubricentro25/Resources/Data");

            string filePath = customFolderPath + "/appsettings.json";

            string jsonContent = File.ReadAllText(filePath);

            JObject jsonObject = JObject.Parse(jsonContent);

            jsonObject[AddressConfigurationHelper.SectionName]![nameof(AddressConfigurationHelper.ApiAddress)] = apiAddressEntry.Text;

            File.WriteAllText(filePath, jsonObject.ToString());
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("FATAL ERROR", ex.GetBaseException().Message, "OK");
            throw;
        }
        


    }
}