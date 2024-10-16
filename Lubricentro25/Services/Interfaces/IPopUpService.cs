using CommunityToolkit.Maui.Views;

namespace Lubricentro25.Services.Interfaces;

public interface IPopUpService
{
    Task ShowErrorMessage(string message);
    Task ShowMessage(string message, string title = "");
    Task<bool> ShowWarning(string message);
    Task<bool> ShowPopUpAsync(Popup PopUp);
    void ShowParallelPopUp(Popup PopUp);
    void ParallelPopupHint();
}
