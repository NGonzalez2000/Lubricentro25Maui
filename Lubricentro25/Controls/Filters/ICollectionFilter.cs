namespace Lubricentro25.Controls.Filters
{
    public interface ICollectionFilter<T>
    {
        bool Filter(T item);
    }
}
