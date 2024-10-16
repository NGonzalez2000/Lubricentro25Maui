using CommunityToolkit.Maui.Views;

namespace Lubricentro25.Controls.PopUps;

public partial class CompanySelector : Popup
{
	public CompanySelector(List<Company> companies)
	{
		InitializeComponent();
		companyPicker.ItemsSource = companies;
        companyPicker.ItemDisplayBinding = new Binding(nameof(Company.Name));
	}
    private void Accept_Clicked(object sender, EventArgs e)
    {
        Close(true);
    }
    private void Cancel_Clicked(object sender, EventArgs e)
    {
        Close(false);
    }

    public Company SelectedComapy()
    {
        return (Company)companyPicker.SelectedItem;
    }
}