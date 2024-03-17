using Lubricentro25.Api.Contracts.Chat;
using Lubricentro25.Api.Interface;
using Lubricentro25.Models.Chats;
using Lubricentro25.Models.Helpers.Interface;

namespace Lubricentro25.Api.Endpoints;

internal class ChatEndpoint(ILubricentroApiClient apiClient, IChatConnectionHelper connectionHelper) : IChatEndpoint
{
    private readonly ILubricentroApiClient _apiClient = apiClient;
    private readonly IChatConnectionHelper _connectionHelper = connectionHelper;
    public async Task<ApiResponse<List<ChatMessage>>> GetConversationAsync( string receptorId)
    {
        var request = new GetConversationRequest(_connectionHelper.GetUserId(), receptorId);
        return await _apiClient.Post<List<ChatMessage>, List<ChatMessageResponse>>("/chat/getconversation", request);
    }

    public async Task<ApiResponse<User>> GetUsersAsync()
    {
        return await _apiClient.Get<User, UserResponse>("/chat/getusers");
    }
    public Task<bool> SendGroupMessageAsync(string message)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> SendMessageAsync(string receptorId, string message)
    {
        return await _connectionHelper.SendMessageAsync(receptorId, message);
    }
}
