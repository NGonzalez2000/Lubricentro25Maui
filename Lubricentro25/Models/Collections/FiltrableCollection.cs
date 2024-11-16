using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Lubricentro25.Models.Collections;

public class FilterableCollection<T> : ObservableCollection<T>, INotifyPropertyChanged
{
    private readonly ObservableCollection<T> _originalCollection;
    private Predicate<T>? _filter;

    public FilterableCollection()
    {
        _originalCollection = [];
    }

    public FilterableCollection(IEnumerable<T> values)
    {
        _originalCollection = new(values);
        foreach (T value in values)
        {
            base.Add(value);
        }
    }

    public void SetFilter(Predicate<T> filter)
    {
        _filter = filter;
    }

    public void Refresh()
    {
        ClearItems();
        foreach (var item in _originalCollection.Where(i => _filter?.Invoke(i) ?? true))
        {
            base.Add(item);
        }
        OnPropertyChanged(new PropertyChangedEventArgs(nameof(Count)));
    }

    public new void Add(T item)
    {
        _originalCollection.Add(item);
        if (_filter?.Invoke(item) ?? true)
        {
            base.Add(item);
        }
    }

    public new void Remove(T item)
    {
        _originalCollection.Remove(item);
        base.Remove(item);
        OnPropertyChanged(new PropertyChangedEventArgs(nameof(Count)));
    }

    public new void Clear()
    {
        _originalCollection.Clear();
        base.Clear();
        OnPropertyChanged(new PropertyChangedEventArgs(nameof(Count)));
    }

    public T? Get(Predicate<T> predicate)
    {
        return _originalCollection.FirstOrDefault(predicate.Invoke);
    }
}
