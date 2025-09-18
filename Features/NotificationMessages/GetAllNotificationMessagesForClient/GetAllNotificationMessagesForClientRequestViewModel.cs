using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.NotificationMessages.Queries;

namespace KOG.ECommerce.Features.NotificationMessages.GetAllNotificationMessagesForClient
{
    public record GetAllNotificationMessagesForClientRequestViewModel(int PageIndex = 1,
        int PageSize = 100);
    public class GetAllNotificationMessagesForClientRequestValidator:AbstractValidator<GetAllNotificationMessagesForClientRequestViewModel>
    {
        public GetAllNotificationMessagesForClientRequestValidator() { }
    }
    public class GetAllNotificationMessagesForClientRequestProfile:Profile
    {
        public GetAllNotificationMessagesForClientRequestProfile()
        {
            CreateMap<GetAllNotificationMessagesForClientRequestViewModel, GetAllNotificationMessagesForClientQuery>();
        }
    }
}
