namespace Lubricentro25.Models.Helpers.Interface;

public interface IChatConnectionHelper
{
    Task Connect(string token);
    Task<bool> SendMessageAsync(string receptorId, string message);
}
