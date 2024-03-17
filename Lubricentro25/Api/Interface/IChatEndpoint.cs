using Lubricentro25.Models.Chats;

namespace Lubricentro25.Api.Interface;

public interface IChatEndpoint
{
    Task<ApiResponse<User>> GetUsersAsync();
    Task<ApiResponse<List<ChatMessage>>> GetConversationAsync(string receptorId);
    Task<bool> SendMessageAsync(string receptorId, string message);
    Task<bool> SendGroupMessageAsync(string message);
}
