using System.Collections.ObjectModel;

namespace UITests
{
    public partial class MainPage : ContentPage
    {

        private ObservableCollection<Models.Email> _emails;
        public MainPage()
        {
            InitializeComponent();
            _emails = [];
            _emails.Add(new("", "email1", true));
            EmailList.ItemsSource = _emails;
        }

       
    }

}
