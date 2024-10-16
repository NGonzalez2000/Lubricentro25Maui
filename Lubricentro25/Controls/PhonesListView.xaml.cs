using Lubricentro25.Models.Collections;

namespace Lubricentro25.Controls;

public partial class PhonesListView : ContentView
{
	public PhonesListView()
	{
		InitializeComponent();
	}
    private void Button_Clicked(object sender, EventArgs e)
    {
        if (sender is not Button button) return;
        var commandParameter = button.CommandParameter;

        if (BindingContext is PhoneCollection phoneCollection && commandParameter is Models.Phone phone)
        {
            phoneCollection.DeletePhoneCommand.Execute(phone);
        }
    }
}