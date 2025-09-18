using AutoMapper;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Notifications;
using KOG.ECommerce.Models.Users;

namespace KOG.ECommerce.Features.Common.NotificationMessages.DTOs
{
    public class GetAllNotificationMessageDTO
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserMobile { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime CreatedDate { get; set; }
    }
    public class GetAllNotificationMessageDTOProfile : Profile
    {
        public GetAllNotificationMessageDTOProfile()
        {
            CreateMap<NotificationMessage, GetAllNotificationMessageDTO>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.UserMobile, opt => opt.MapFrom(src => src.User.Mobile));
        }
    }
}
