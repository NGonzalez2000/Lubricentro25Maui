using CommunityToolkit.Mvvm.Messaging;
using Lubricentro25.Api.Interface;
using Lubricentro25.Models.Helpers.Interface;
using Lubricentro25.Models.Messages;
using Microsoft.AspNetCore.SignalR.Client;

namespace Lubricentro25.Models.Helpers;

public class ChatConnectionHelper() : IChatConnectionHelper
{
    private HubConnection? connection;

    public async Task Connect(string token)
    {
        connection = new HubConnectionBuilder()
         .WithUrl(new Uri(Preferences.Get("ApiAddress", "") + "/chat"), options => { options.AccessTokenProvider = () => Task.FromResult(token)!; })
         .WithAutomaticReconnect()
         .Build();
        connection.On<string, string>("ReciveMessageAsync", ReciveMessage);

        await connection.StartAsync();
    }

    public async Task<bool> SendMessageAsync(string receptorId, string message)
    {
        if (connection == null)
        {
            await Shell.Current.DisplayAlert("Error", "Se perdio la conexión con el servidor de chat, por favor reinicie el programa", "Aceptar");
            return false;
        }
        try
        {
            await connection.InvokeAsync("SendMessageAsync", receptorId, message);
        }
        catch (Exception)
        {
            await Shell.Current.DisplayAlert("Error", "No se pudo enviar el mensaje", "Aceptar");
            return false;
        }
        return true;
    }

    private void ReciveMessage(string senderId, string message)
    {
        WeakReferenceMessenger.Default.Send(new ReciveChatMessageMessage(message, senderId));
    }
}
