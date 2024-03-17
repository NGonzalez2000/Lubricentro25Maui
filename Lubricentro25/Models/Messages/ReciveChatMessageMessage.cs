using CommunityToolkit.Mvvm.Messaging.Messages;
using Lubricentro25.Models.Chats;

namespace Lubricentro25.Models.Messages;

public class ReciveChatMessageMessage(string Message, string From) : ValueChangedMessage<ChatMessage>(new(Message,false,From))
{
}
