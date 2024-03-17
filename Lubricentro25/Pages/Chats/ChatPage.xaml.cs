using CommunityToolkit.Maui.Behaviors;
using Lubricentro25.ViewModels.Chats;

namespace Lubricentro25.Pages.Chats;

public partial class ChatPage : ContentPage
{
	public ChatPage(ChatViewModel bindingContext)
	{
		InitializeComponent();
		BindingContext = bindingContext;
        Behaviors.Add(new EventToCommandBehavior() { EventName = nameof(Loaded), Command = bindingContext.LoadCommand });
    }

}