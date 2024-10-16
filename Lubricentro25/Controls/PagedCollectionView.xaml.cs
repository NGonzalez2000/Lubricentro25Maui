using System.Collections;

namespace Lubricentro25.Controls;

public partial class PagedCollectionView : Grid
{
    private readonly int _itemsPerPage = 100;
    private int _currentPage = 1;
    private int _totalPages = 1;
    public PagedCollectionView()
    {
        InitializeComponent();
        contentCollectionView.SelectionChanged += ContentCollectionView_SelectionChanged;
    }

    private void ContentCollectionView_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        SelectedItem = contentCollectionView.SelectedItem;
    }

    public static readonly BindableProperty FullSourceProperty =
       BindableProperty.Create(nameof(FullSource), typeof(IEnumerable), typeof(PagedCollectionView), null, propertyChanged: OnFullSourcePropertyChanged);

    public static readonly BindableProperty ItemTemplateProperty =
       BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), typeof(PagedCollectionView), null);

    public static readonly BindableProperty HeaderTemplateProperty =
        BindableProperty.Create(nameof(Header), typeof(IView), typeof(PagedCollectionView), null);

    public static readonly BindableProperty SelectedItemProperty =
        BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(PagedCollectionView), default, defaultBindingMode: BindingMode.TwoWay, propertyChanged: OnSelectedItemPropertyChanged);

    

    public IEnumerable FullSource
    {
        get { return (IEnumerable)GetValue(FullSourceProperty); }
        set { SetValue(FullSourceProperty, value); }
    }
    public DataTemplate ItemTemplate
    {
        get { return (DataTemplate)GetValue(ItemTemplateProperty); }
        set { SetValue(ItemTemplateProperty, value); }
    }
    public IView Header
    {
        get { return (IView)GetValue(HeaderTemplateProperty); }
        set { SetValue(HeaderTemplateProperty, value); }
    }
    public object SelectedItem
    {
        get { return GetValue(SelectedItemProperty); }
        set { SetValue(SelectedItemProperty, value); }
    }
    private void LoadCollectionView()
    {
        if (FullSource is null)
        {
            return;
        }

        var temp = FullSource.Cast<object>();
        int totalItems = temp.Count();

        _totalPages = totalItems / _itemsPerPage;
        if (totalItems % _itemsPerPage > 0) _totalPages++;
        _currentPage = Math.Min(_totalPages, 1);


        contentCollectionView.ItemTemplate = ItemTemplate;

        if (headerGrid.Children.Count == 0)
        {
            headerGrid.Children.Add(Header);
        }

        totalPagesLabel.Text = _totalPages.ToString();
        LoadPage();
    }
    private void LoadPage()
    {
        currentPageLabel.Text = _currentPage.ToString();
        if (_currentPage == 0)
        {
            contentCollectionView.ItemsSource = Enumerable.Empty<object>();
            return;
        }

        var temp = FullSource.Cast<object>();
        int totalItems = temp.Count(); 
        int index = _itemsPerPage * (_currentPage - 1);
        int stop = (totalItems - index < _itemsPerPage) ? totalItems - index : _itemsPerPage;
        contentCollectionView.ItemsSource = temp.Skip(index).Take(stop);
    }
    private static void OnFullSourcePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is PagedCollectionView pagedCollectionView)
        {
            pagedCollectionView.LoadCollectionView();
        }
    }
    private static void OnSelectedItemPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is PagedCollectionView pcv)
        {
            pcv.contentCollectionView.SelectedItem = newValue;
        }
    }
    private void PrevButton_Clicked(object sender, EventArgs e)
    {
        if (_currentPage <= 1) return;

        _currentPage--;
        LoadPage();
    }
    private void NextButton_Clicked(object sender, EventArgs e)
    {
        if (_currentPage == _totalPages) return;

        _currentPage++;
        LoadPage();
    }
}