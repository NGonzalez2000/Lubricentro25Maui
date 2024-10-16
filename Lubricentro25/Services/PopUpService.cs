using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Messaging;
using Lubricentro25.Models.Messages;
using Lubricentro25.Services.Interfaces;

namespace Lubricentro25.Services;

public class PopUpService : IPopUpService
{
    Task? task;
    
    public void ParallelPopupHint()
    {
        if(task is null)
        {
            return;
        }
        WeakReferenceMessenger.Default.Send<PopupProgressBarMessage>(new(false));

        if(task.IsCompleted)
        {
            task.Dispose();
        }
    }

    public async Task ShowErrorMessage(string message)
    {
        await Shell.Current.DisplayAlert("Error", message, "Aceptar");
    }

    public async Task ShowMessage(string message, string title = "")
    {
        await Shell.Current.DisplayAlert(title,message, "Aceptar");
    }

    public void ShowParallelPopUp(Popup PopUp)
    {
        task = Shell.Current.ShowPopupAsync(PopUp);
    }

    public async Task<bool> ShowPopUpAsync(Popup PopUp)
    {
        var resposne = await Shell.Current.ShowPopupAsync(PopUp);
        if(resposne is bool b)
        {
            return b;
        }
        return false;
    }

    public async Task<bool> ShowWarning(string message)
    {
        return await Shell.Current.DisplayAlert("Advertencia", message, "Aceptar", "Cancelar");
    }
}
