using Lubricentro25.Api.Contracts.Chat;
using Lubricentro25.Models.Chats;
using Mapster;

namespace Lubricentro25.Models.Mapping;

public class ChatMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UserResponse,User>()
            .Map(dest => dest.ImageData, src => src.UserImageData)
            .Map(dest => dest.Name, src => src.UserName)
            .Map(dest => dest.UserId, src => src.Id);

        config.NewConfig<ChatMessageResponse, ChatMessage>()
            .Map(dest => dest.MessageContent, src => src.Message)
            .Map(dest => dest.SenderId, src => src.SenderId);
    }                                          
}
