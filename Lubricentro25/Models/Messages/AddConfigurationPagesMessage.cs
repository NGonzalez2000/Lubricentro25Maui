using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Lubricentro25.Models.Messages;

public class AddConfigurationPagesMessage(bool value, string policy) : ValueChangedMessage<ConfigurationPageMessage>(new(policy,value))
{
}

public record ConfigurationPageMessage(string Policy, bool IsAllowed);
