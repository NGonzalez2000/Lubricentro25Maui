using CommunityToolkit.Mvvm.Input;

namespace Lubricentro25.Controls.Filters;

public class FilterView : ContentView
{
    private readonly Grid mainGrid;
    private readonly Button filterButton;
    private readonly Button clearButton;

    public readonly BindableProperty MainContentProperty =
        BindableProperty.Create(nameof(MainContent), typeof(IView), typeof(FilterView), null, propertyChanged: OnMainContentChanged);

    public static readonly BindableProperty CommandProperty =
       BindableProperty.Create(nameof(Command), typeof(IRelayCommand), typeof(FilterView), null, propertyChanged: OnCommandChanged);


    public IView MainContent
    {
        get => (IView)GetValue(MainContentProperty);
        set => SetValue(MainContentProperty, value);
    }

    public IRelayCommand Command
    {
        get { return (IRelayCommand)GetValue(CommandProperty); }
        set { SetValue(CommandProperty, value); }
    }
    static void OnMainContentChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is FilterView page && newValue is IView view)
        {
            page.mainGrid.Add(page.MainContent, 0, 0);
        }
    }
    private static void OnCommandChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is FilterView page && newValue is IRelayCommand command)
        {
            page.filterButton.Command = command;
        }
    }
    public FilterView()
    {
        mainGrid = [];
        mainGrid.ColumnSpacing = 20d;
        mainGrid.ColumnDefinitions.Add(new ColumnDefinition(GridLength.Star));
        mainGrid.ColumnDefinitions.Add(new ColumnDefinition(100f));

        mainGrid.RowDefinitions.Add(new RowDefinition(GridLength.Auto));
        mainGrid.RowDefinitions.Add(new RowDefinition(2f));

        clearButton = new Button()
        {
            Text = "Limpiar"
        };

        clearButton.SetBinding(Button.CommandProperty, "ClearCommand");

        filterButton = new Button
        {
            Text = "Filtrar"
        };

        VerticalStackLayout buttonsLayout = [clearButton, filterButton];
        buttonsLayout.Spacing = 20d;
        Grid.SetColumn(buttonsLayout, 1);
        mainGrid.Children.Add(buttonsLayout);

        BoxView boxView = new() { HeightRequest = 2f };
        Grid.SetRow(boxView, 1);
        Grid.SetColumnSpan(boxView, 2);
        mainGrid.Children.Add(boxView);


        Content = mainGrid;
    }
}
