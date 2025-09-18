using AutoMapper;
using KOG.ECommerce.Models.Notifications;

namespace KOG.ECommerce.Features.Common.NotificationMessages.DTOs
{
    public class GetAllNotificationMessagesForClientDTO
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime CreatedDate { get; set; }
    }
    public class GetAllNotificationMessagesForClientDTOProfile:Profile
    {
        public GetAllNotificationMessagesForClientDTOProfile()
        {
            CreateMap<NotificationMessage, GetAllNotificationMessagesForClientDTO>();
        }
    }
}
