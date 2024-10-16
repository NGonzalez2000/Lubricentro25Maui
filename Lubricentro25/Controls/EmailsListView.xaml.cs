using Lubricentro25.Models.Collections;

namespace Lubricentro25.Controls;

public partial class EmailsListView : ContentView
{
    public EmailsListView()
    {
        InitializeComponent();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        if (sender is not Button button) return;
        var commandParameter = button.CommandParameter;

        if(BindingContext is EmailCollection emailCollection && commandParameter is Models.Email email)
        {
            emailCollection.DeleteEmailCommand.Execute(email);
        }
    }
}