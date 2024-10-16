using CommunityToolkit.Maui.Views;

namespace Lubricentro25.Controls.PopUps;

public partial class ProviderPopUp : Popup
{
    public static readonly BindableProperty TaxConditionsProperty =
            BindableProperty.Create(nameof(TaxConditions), typeof(List<TaxCondition>), typeof(CustomerPopUp), null);

    public List<TaxCondition> TaxConditions
    {
        get { return (List<TaxCondition>)GetValue(TaxConditionsProperty); }
        set { SetValue(TaxConditionsProperty, value); }
    }

    public object GetSelectedTaxCondition()
    {
        return TaxConditionsPicker.SelectedItem;
    }
    public ProviderPopUp(IEnumerable<TaxCondition> _taxConditions)
	{
		InitializeComponent();
        TaxConditions = new(_taxConditions);
        TaxConditionsPicker.ItemsSource = TaxConditions;
        TaxConditionsPicker.SelectedIndex = 0;
    }
    public void SelectTaxCondition(string description)
    {
        int index = TaxConditions.IndexOf(TaxConditions.First(t => t.Description == description));
        TaxConditionsPicker.SelectedIndex = index;
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