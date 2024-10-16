using CommunityToolkit.Maui.Views;

namespace Lubricentro25.Controls.PopUps;

public partial class NewPasswordPopUp : Popup
{
	public NewPasswordPopUp()
	{
		InitializeComponent();
	}
    private bool VerifyPassword()
    {
        if (entryPassword.Text.Length < 8)
        {
            labelError.Text = "La contraseña debe tener al menos 8 caracteres.";
            return false;
        }
        if(entryVerification.Text != entryPassword.Text)
        {
            labelError.Text = "Las contraseñas no coiniciden.";
            return false;
        }
        return true;
    }
    private void Accept_Clicked(object sender, EventArgs e)
    {
        if (VerifyPassword())
            Close(true);
    }

    private void Cancel_Clicked(object sender, EventArgs e)
    {
        Close(false);
    }
    public string GetPassword()
    {
        return entryPassword.Text;
    }
}