using CommunityToolkit.Maui.Views;
using Lubricentro25.Models.Helpers;
using Newtonsoft.Json.Linq;

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

    private void Button_Clicked(object sender, EventArgs e)
    {
		
		Preferences.Set(nameof(AddressConfigurationHelper.ApiAddress), apiAddressEntry.Text);
        string filePath = Path.Combine(AppContext.BaseDirectory, "Resources/Data/appsettings.json");

        string jsonContent = File.ReadAllText(filePath);

        JObject jsonObject = JObject.Parse(jsonContent);

        jsonObject[AddressConfigurationHelper.SectionName][nameof(AddressConfigurationHelper.ApiAddress)] = apiAddressEntry.Text;

        File.WriteAllText(filePath, jsonObject.ToString());


    }
}