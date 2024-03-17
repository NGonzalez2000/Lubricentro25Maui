namespace Lubricentro25.Models.Helpers.Interface;

public interface IChatConnectionHelper
{
    string GetUserId();
    void SetUserId(string userId);
    Task Connect(string token);
    Task<bool> SendMessageAsync(string receptorId, string message);
}
