using CommunityToolkit.Mvvm.Input;
using Lubricentro25.Icons;

namespace Lubricentro25.Controls;

public class ConfigurationFrame : ContentView
{
	private Label title;
	private VerticalStackLayout vst;

    public static readonly BindableProperty TitleProperty =
		BindableProperty.Create(nameof(Title), typeof(string), typeof(ConfigurationFrame), propertyChanged: OnTitlePropertyChanged);

	public static readonly BindableProperty CollectionProperty =
		BindableProperty.Create(nameof(Collection), typeof(IView), typeof(ConfigurationFrame), propertyChanged: OnCollectionPropertyChanged);

	public string Title
	{
		get { return (string)GetValue(TitleProperty); }
		set { SetValue(TitleProperty, value); }
	}

	public IView Collection
	{
        get { return (IView)GetValue(CollectionProperty); }
        set { SetValue(CollectionProperty, value); }
    }

    private static void OnTitlePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is ConfigurationFrame configFrame && newValue is string str)
        {
            configFrame.title.Text = str;
        }
    }

    private static void OnCollectionPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is ConfigurationFrame configFrame && newValue is IView collection)
        {
            configFrame.vst.Children.Add(collection);
        }
    }
    public ConfigurationFrame()
	{
        title = new()
        {
            VerticalOptions = LayoutOptions.Center
        };

		//button = new()
		//{
		//	Style = (Style?)Application.Current!.Resources["IconButton"],
		//	Text = IconFont.Add,
		//	TextColor = Colors.LightGreen,
		//	HorizontalOptions = LayoutOptions.End,
		//	VerticalOptions = LayoutOptions.Center,
		//	Margin = new Thickness(0, 0, 0, 5)
		//};

		//Grid.SetColumn(title, 0);
		//Grid.SetColumn(button, 1);
		//Grid grid = [];
		//grid.Children.Add(title);
		//grid.Children.Add(button);

		vst = new()
		{
			Children =
			{
				title,
				new BoxView() { HeightRequest = 2, Color = Colors.Gray, HorizontalOptions = LayoutOptions.Fill}
			}
		};
		Frame outterFrame = new()
		{
			Content = vst
		};

		Content = outterFrame;
	}
}