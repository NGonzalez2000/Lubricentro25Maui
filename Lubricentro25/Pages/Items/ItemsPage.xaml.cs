using Lubricentro25.ViewModels.Items;

namespace Lubricentro25.Pages.Items;

public partial class ItemsPage : BasePage
{
	public ItemsPage(ProductViewModel vm) : base(vm)
	{
		InitializeComponent();
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        FocusItem();
    }

    void FocusItem()
    {
        if(BindingContext is ProductViewModel vm)
        {
            if (vm.ScrollToProduct is not Product product) return;

            ProductsCollection.ScrollTo(product);
        }
    }
}