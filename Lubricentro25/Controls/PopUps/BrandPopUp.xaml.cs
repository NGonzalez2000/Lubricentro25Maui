using CommunityToolkit.Maui.Views;

namespace Lubricentro25.Controls.PopUps;

public partial class BrandPopUp : Popup
{
	public BrandPopUp()
	{
		InitializeComponent();
	}
    private void Accept_Clicked(object sender, EventArgs e)
    {
        Close(true);
    }
    private void Cancel_Clicked(object sender, EventArgs e)
    {
        Close(false);
    }
}