using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.NotificationMessages.Queries;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.NotificationMessages.GetAllNotificationMessages
{
    public record GetAllNotificationMessageRequestViewModel(
        string? UserName,
        string? UserMobile,
        string? Title,
        DateTime? From,
        DateTime? To,
        int PageIndex = 1,
        int PageSize = 100
    );
    public class GetAllNotificationMessageRequestValidator : AbstractValidator<GetAllNotificationMessageRequestViewModel>
    {
        public GetAllNotificationMessageRequestValidator()
        {
        }
    }
    public class GetAllNotificationMessageRequestProfile : Profile
    {
        public GetAllNotificationMessageRequestProfile()
        {
            CreateMap<GetAllNotificationMessageRequestViewModel, GetAllNotificationMessageQuery>();
        }
    }
}
