using AutoMapper;
using KOG.ECommerce.Features.Common.NotificationMessages.DTOs;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.NotificationMessages.GetAllNotificationMessages
{
    public class GetAllNotificationMessageResponseViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserMobile { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime CreatedDate { get; set; }
    }
    public class GetAllNotificationMessageResponseProfile : Profile
    {
        public GetAllNotificationMessageResponseProfile()
        {
            CreateMap<GetAllNotificationMessageDTO, GetAllNotificationMessageResponseViewModel>();
        }
    }
}
