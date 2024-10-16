using CommunityToolkit.Mvvm.Input;

namespace Lubricentro25.Controls.Cards;

public partial class CompanyServiceCard : ContentView
{
    public static readonly BindableProperty CommandProperty =
        BindableProperty.Create(nameof(Command), typeof(IAsyncRelayCommand), typeof(CompanyServiceCard), null, propertyChanged: OnCommandChanged);
  
    public static readonly BindableProperty CommandParameterProperty =
        BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(CompanyServiceCard), null, propertyChanged: OnCommandParameterChanged);

    public static readonly BindableProperty ServiceNameProperty =
        BindableProperty.Create(nameof(ServiceName), typeof(string), typeof(CompanyServiceCard), string.Empty, propertyChanged: OnServiceNameChanged);


    public static readonly BindableProperty SelectedCompanyProperty =
        BindableProperty.Create(nameof(SelectedCompany), typeof(Company), typeof(CompanyServiceCard), null, propertyChanged: OnSelectedCompanyChanged);

    public IAsyncRelayCommand Command
    {
        get { return (IAsyncRelayCommand)GetValue(CommandProperty); }
        set { SetValue(CommandProperty, value); }
    }
    public object CommandParameter
    {
        get { return GetValue(CommandParameterProperty); }
        set { SetValue(CommandParameterProperty, value); }
    }
    public string ServiceName
    {
        get { return (string)GetValue(ServiceNameProperty); }
        set { SetValue(ServiceNameProperty, value); }
    }
    public string SelectedCompany
    {
        get { return (string)GetValue(SelectedCompanyProperty); }
        set { SetValue(SelectedCompanyProperty, value); }
    }
    public CompanyServiceCard()
	{
		InitializeComponent();
	}
    private static void OnCommandChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is CompanyServiceCard csc)
        {
            if (newValue is not IAsyncRelayCommand command)
            {
                csc.selectCompanyButton.Command = null;
                return;
            }
            csc.selectCompanyButton.Command = command;
        }
    }
    private static void OnCommandParameterChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is CompanyServiceCard csc)
        {
            if (newValue is not object obj)
            {
                csc.selectCompanyButton.CommandParameter = null;
                return;
            }
            csc.selectCompanyButton.CommandParameter = obj;
        }
    }
    private static void OnServiceNameChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is CompanyServiceCard csc)
        {
            if (newValue is not string serviceName)
            {
                csc.serviceLabel.Text = "???";
                return;
            }
            csc.serviceLabel.Text = serviceName;
        }
    }
    private static void OnSelectedCompanyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is CompanyServiceCard csc)
        {
            if (newValue is not Company c)
            {
                csc.selectedCompanyLabel.Text = string.Empty;
                return;
            }
            csc.selectedCompanyLabel.Text = c.Name;
        }
    }
}