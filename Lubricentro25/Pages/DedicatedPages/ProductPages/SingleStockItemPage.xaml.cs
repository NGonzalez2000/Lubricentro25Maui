using CommunityToolkit.Maui.Behaviors;

namespace Lubricentro25.Pages.DedicatedPages.ProductPages;

public partial class SingleStockItemPage : ContentPage
{
	public SingleStockItemPage(SingleStockItemViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;

    }

}