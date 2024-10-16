using System.Collections.ObjectModel;

namespace UITests;

public class EmailListView : ContentView
{
    public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
            nameof(ItemsSource),
            typeof(ObservableCollection<Models.Email>), 
            typeof(EmailListView),
            new ObservableCollection<Models.Email>());

    public ObservableCollection<Models.Email> ItemsSource
    {
        get => (ObservableCollection<Models.Email>)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }
    public EmailListView()
    {

        DataTemplate ItemTemplate = new(() =>
        {
            var grid = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = GridLength.Star },
                    new ColumnDefinition { Width = 50 }
                },
                Margin = new Thickness(0, 5)
            };

            var entry = new Entry();
            entry.SetBinding(Entry.TextProperty, nameof(Models.Email.Value));
            Grid.SetRow(entry, 0);
            Grid.SetColumn(entry, 0);

            var deleteButton = new Button { Text = "D", HorizontalOptions = LayoutOptions.Start };
            deleteButton.Clicked += (sender, args) =>
            {
                if (sender is Button btn)
                {
                    var item = (Models.Email)btn.BindingContext;
                    ItemsSource.Remove(item);
                }
            };
            Grid.SetRow(deleteButton, 0);
            Grid.SetColumn(deleteButton, 1);

            grid.Children.Add(entry);
            grid.Children.Add(deleteButton);

            var viewCell = new ViewCell { View = grid };
            deleteButton.BindingContext = viewCell.BindingContext;

            return viewCell;
        });

        var listView = new ListView
        {
            ItemTemplate = ItemTemplate
        };

        listView.SetBinding(ListView.ItemsSourceProperty, new Binding(nameof(ItemsSource), source: this));

        var addButton = new Button { Text = "Add Item" };
        addButton.Clicked += (sender, args) =>
        {
            ItemsSource.Add(new("","",false));
        };

        var stackLayout = new StackLayout
        {
            Children = { addButton, listView }
        };

        Content = stackLayout;
    }
}
