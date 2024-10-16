using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Messaging;
using Lubricentro25.Models.Messages;

namespace Lubricentro25.Controls.PopUps;

public partial class ProgressPopUp : Popup
{
	private int _progress;
	private readonly int _total;
    private readonly CancellationTokenSource token;

    public ProgressPopUp(int total, string text, CancellationTokenSource token)
	{
		InitializeComponent();

        _progress = 0;
		_total = total;
        this.token = token;
        ProgressBar.Progress = 0;
		Operation.Text = text;
		ProgressCount.Text = $"{_progress}/{_total}";

		WeakReferenceMessenger.Default.Register<PopupProgressBarMessage>(this, MessageHandler);
	}



	private void Increment()
	{
		if(_total == 0) return;

		_progress++;
        ProgressBar.Progress = (double) _progress/_total;
		ProgressCount.Text = $"{_progress}/{_total}";
	}

	private void MessageHandler(object recipient, PopupProgressBarMessage message )
	{
		Increment();

		if (_progress == _total)
		{
			ShutDown();
		}
	}

    private void ShutDown()
    {
		WeakReferenceMessenger.Default.Unregister<PopupProgressBarMessage>(this);
		Close();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
		token.Cancel();
		Close();
    }
}