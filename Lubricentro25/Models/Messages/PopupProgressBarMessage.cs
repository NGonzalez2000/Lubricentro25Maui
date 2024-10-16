using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Lubricentro25.Models.Messages;

internal class PopupProgressBarMessage(bool close) : ValueChangedMessage<bool>(close)
{
}
