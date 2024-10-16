namespace Lubricentro25.Pages.DedicatedPages.ClientPages;

public partial class SingleClientPage : ContentPage
{
	public SingleClientPage(SingleClientViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
        Loaded += SingleClientPage_Loaded;
	}

    private async void SingleClientPage_Loaded(object? sender, EventArgs e)
    {
        if (BindingContext is SingleClientViewModel viewModel)
        {
            await viewModel.Refresh();
        }
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}