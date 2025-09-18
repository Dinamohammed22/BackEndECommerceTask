using AutoMapper;
using KOG.ECommerce.Features.Common.NotificationMessages.DTOs;

namespace KOG.ECommerce.Features.NotificationMessages.GetAllNotificationMessagesForClient
{
    public record GetAllNotificationMessagesForClientResponseViewModel(string Title, string Body, DateTime CreatedDate);
    public class GetAllNotificationMessagesForClientResponseProfile:Profile
    {
        public GetAllNotificationMessagesForClientResponseProfile()
        {
            CreateMap<GetAllNotificationMessagesForClientDTO, GetAllNotificationMessagesForClientResponseViewModel>();
        }
    }
}
