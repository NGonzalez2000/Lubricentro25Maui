using CommunityToolkit.Maui.Behaviors;
using Lubricentro25.ViewModels;

namespace Lubricentro25.Pages;

public abstract class BasePage : ContentPage
{
    private readonly Grid mainGrid;
    private readonly Grid topRow;

    public readonly BindableProperty MainContentProperty =
        BindableProperty.Create(nameof(MainContent), typeof(IView), typeof(BasePage), null, propertyChanged: OnMainContentChanged);

    public readonly BindableProperty ButtonsProperty =
        BindableProperty.Create(nameof(Buttons), typeof(IView), typeof(BasePage), null, propertyChanged: OnButtonsChanged);

    public readonly BindableProperty FilterProperty =
        BindableProperty.Create(nameof(Filter), typeof(IView), typeof(BasePage), null, propertyChanged: OnFilterChanged);

    public IView MainContent
    {
        get => (IView)GetValue(MainContentProperty);
        set => SetValue(MainContentProperty, value);
    }
    public IView Buttons
    {
        get => (IView)GetValue(ButtonsProperty);
        set => SetValue(ButtonsProperty, value);
    }
    public IView Filter
    {
        get => (IView)GetValue(FilterProperty);
        set => SetValue(FilterProperty, value);
    }
    public BasePage(BaseViewModel bvm)
    {
        BindingContext = bvm;
        Behaviors.Add(new EventToCommandBehavior() { EventName = nameof(Appearing), Command = bvm.LoadCommand });
        Behaviors.Add(new EventToCommandBehavior() { EventName = nameof(Disappearing), Command = bvm.CloseCommand });
        // Create side panel layout

        mainGrid = [];
        mainGrid.RowDefinitions.Add(new RowDefinition(GridLength.Auto));
        mainGrid.RowDefinitions.Add(new RowDefinition(GridLength.Star));

        topRow = [];
        topRow.ColumnDefinitions.Add(new ColumnDefinition(GridLength.Star));
        topRow.ColumnDefinitions.Add(new ColumnDefinition(GridLength.Star));
        mainGrid.Add(topRow);

        Content = mainGrid;
    }
    static void OnMainContentChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is BasePage page)
        {
            page.mainGrid.Add(page.MainContent, 0, 1);
        }
    }
    static void OnButtonsChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is BasePage page)
        {
            page.topRow.Add(page.Buttons, 0,0);
        }
    }
    static void OnFilterChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is BasePage page)
        {
            page.topRow.Add(page.Filter, 1, 0);
        }
    }
}