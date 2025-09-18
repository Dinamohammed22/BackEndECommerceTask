using Microsoft.EntityFrameworkCore;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.NotificationMessages.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Notifications;
using System.Linq.Expressions;

namespace KOG.ECommerce.Features.Common.NotificationMessages.Queries
{
    public record GetAllNotificationMessageQuery(
        string? UserName,
        string? UserMobile,
        string? Title,
        DateTime? From,
        DateTime? To,
        int PageIndex = 1,
        int PageSize = 100
    ) : IRequestBase<PagingViewModel<GetAllNotificationMessageDTO>>;
    public class GetAllNotificationMessageQueryHandler : RequestHandlerBase<NotificationMessage, GetAllNotificationMessageQuery, PagingViewModel<GetAllNotificationMessageDTO>>
    {
        public GetAllNotificationMessageQueryHandler(RequestHandlerBaseParameters<NotificationMessage> requestParameters) : base(requestParameters)
        {
        }
        public async override Task<RequestResult<PagingViewModel<GetAllNotificationMessageDTO>>> Handle(GetAllNotificationMessageQuery request, CancellationToken cancellationToken)
        {
            var predicate = PredicateExtensions.PredicateExtensions.Begin<NotificationMessage>(true);

            predicate = predicate
                .And(c => !request.From.HasValue || c.CreatedDate >= request.From)
                .And(c => !request.To.HasValue || c.CreatedDate <= request.To)
                .And(n => string.IsNullOrEmpty(request.UserName) || n.User != null && (n.User.Name.Contains(request.UserName)) )
                .And(n => string.IsNullOrWhiteSpace(request.UserMobile) ||n.User != null &&(n.User.Mobile.Contains(request.UserMobile)))
                .And(n => string.IsNullOrWhiteSpace(request.Title) || n.Title.Contains(request.Title));

            var query = await _repository.Get(predicate)
                .Include(n =>  n.User)
                .OrderByDescending(n =>  n.CreatedDate)
                .Map<GetAllNotificationMessageDTO>()
                .ToPagesAsync(request.PageIndex, request.PageSize);

            return RequestResult<PagingViewModel<GetAllNotificationMessageDTO>>.Success(query);
        }
    }
}
