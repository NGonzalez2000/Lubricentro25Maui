using System.Collections.ObjectModel;

namespace Lubricentro25.Models.Stock
{
    public partial class Stock : ObservableObject
    {
        public string Id { get; set; }

        [ObservableProperty]
        ObservableCollection<StockItem> items;

        public Stock()
        {
            Id = string.Empty;
            Items = [];
        }
    }
}
