using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.NotificationMessages.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Notifications;

namespace KOG.ECommerce.Features.Common.NotificationMessages.Queries
{
    public record GetAllNotificationMessagesForClientQuery(int PageIndex = 1,
        int PageSize = 100) : IRequestBase<PagingViewModel<GetAllNotificationMessagesForClientDTO>>;
    public class GetAllNotificationMessagesForClientQueryHandler : RequestHandlerBase<NotificationMessage, GetAllNotificationMessagesForClientQuery, PagingViewModel<GetAllNotificationMessagesForClientDTO>>
    {
        public GetAllNotificationMessagesForClientQueryHandler(RequestHandlerBaseParameters<NotificationMessage> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<PagingViewModel<GetAllNotificationMessagesForClientDTO>>> Handle(GetAllNotificationMessagesForClientQuery request, CancellationToken cancellationToken)
        {

            var query = await _repository.Get(not=>not.UserId==_userState.UserID)
                .OrderByDescending(n => n.CreatedDate)
                .Map<GetAllNotificationMessagesForClientDTO>()
                .ToPagesAsync(request.PageIndex, request.PageSize);
            return RequestResult<PagingViewModel<GetAllNotificationMessagesForClientDTO>>.Success(query);
        }
    }
}
