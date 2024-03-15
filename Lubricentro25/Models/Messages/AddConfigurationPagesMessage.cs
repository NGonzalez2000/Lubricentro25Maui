using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Lubricentro25.Models.Messages;

public class AddConfigurationPagesMessage(bool value) : ValueChangedMessage<bool>(value)
{
}
