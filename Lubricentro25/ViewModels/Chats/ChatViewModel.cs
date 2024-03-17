using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Lubricentro25.Api.Interface;
using Lubricentro25.Models.Chats;
using Lubricentro25.Models.Messages;
using Lubricentro25.Services;

namespace Lubricentro25.ViewModels.Chats
{
    public partial class ChatViewModel(IChatEndpoint chatEndpoint) : ObservableObject
    {
        private readonly IChatEndpoint _chatEndpoint = chatEndpoint;
        [ObservableProperty]
        private List<Chat> chats = [];
        private readonly List<Chat> _persistanceChats = [];
        [ObservableProperty]
        private Chat? selectedChat;

        [RelayCommand]
        private async Task Load()
        {
            if (_persistanceChats.Count != 0)
            {
                return;
            }
            var apiResponse = await _chatEndpoint.GetUsersAsync();

            if (!apiResponse.IsSuccess)
            {
                await Shell.Current.DisplayAlert("Error", apiResponse.ErrorMessage, "Ok");
                return;
            }

            foreach(var user in apiResponse.ResponseContent)
            {
                Chat chat = new()
                {
                    Photo = ImageParser.BytesToImageSource(user.ImageData),
                    To = user.Name,
                    ReceptorId = user.UserId
                };
                chat.Messages = new(await GetMessages(chat.ReceptorId));
                _persistanceChats.Add(chat);
            }
            Chats = new(_persistanceChats);
            SelectedChatChanged(null);
            WeakReferenceMessenger.Default.Register<ReciveChatMessageMessage>(this, ReciveMessage);
        }

        [RelayCommand]
        private void Search(string text)
        {
            Chats = _persistanceChats.Where(c => c.To.Contains(text, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        [RelayCommand]
        async Task SendMessage(string messageText)
        {
            await Task.CompletedTask;
            if (SelectedChat is null) return;
            SelectedChat.CanSendMessage = false;
            if(await _chatEndpoint.SendMessageAsync(SelectedChat.ReceptorId, messageText))
            {
                SelectedChat.Messages.Add(new(messageText, true) );
                SelectedChat.MessageText = string.Empty;
            }
            SelectedChat.CanSendMessage = true;
        }
        partial void OnSelectedChatChanging(Chat? oldValue, Chat? newValue)
        {
            if(oldValue  != null)
            {
                oldValue.SendMessageCommand = null;
            }
            if(newValue != null)
            {
                newValue.SendMessageCommand = SendMessageCommand;
            }
        }
        partial void OnSelectedChatChanged(Chat? value)
        {
            if(value != null && value.To == "Nombre de usuario")
            {
                return;
            }
            SelectedChatChanged(value);
        }
        private void SelectedChatChanged(Chat? chat)
        {
            if(chat is null)
            {
                SelectedChat = new()
                {
                    To = "Nombre de usuario",
                    Messages = new(Enumerable.Empty<ChatMessage>()),
                    CanSendMessage = false
                };
                return;
            }
            chat.CanSendMessage = true;
        }
        private void ReciveMessage(object recipient, ReciveChatMessageMessage messageArgs)
        {
            ChatMessage message = messageArgs.Value;
            Chat chat = Chats.First(c => c.ReceptorId == message.SenderId);
            chat.Messages.Add(message);
        }
        private async Task<List<ChatMessage>> GetMessages(string receptorId)
        {
            var response = await _chatEndpoint.GetConversationAsync(receptorId);

            if(!response.IsSuccess)
            {
                await Shell.Current.DisplayAlert("Error", "No se pudieron cargar los mensajes.", "Aceptar");
                return [];
            }

            List<ChatMessage> messages = new(response.ResponseContent.First());

            foreach(var message in messages)
            {
                message.IsMine = message.SenderId != receptorId; 
            }
            
            return messages;
        }
    }
}
