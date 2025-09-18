using Microsoft.AspNetCore.Mvc;
using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.NotificationMessages.DTOs;
using KOG.ECommerce.Features.Common.NotificationMessages.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.NotificationMessages.GetAllNotificationMessagesForClient
{
    public class GetAllNotificationMessagesForClientEndPoint : EndpointBase<GetAllNotificationMessagesForClientRequestViewModel, GetAllNotificationMessagesForClientResponseViewModel>
    {
        public GetAllNotificationMessagesForClientEndPoint(EndpointBaseParameters<GetAllNotificationMessagesForClientRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetAllNotificationMessagesForClient })]
        public async Task<EndPointResponse<PagingViewModel<GetAllNotificationMessagesForClientResponseViewModel>>> GetAllNotificationMessagesForClient([FromQuery] GetAllNotificationMessagesForClientRequestViewModel viewModel)
        {
            var result = await _mediator.Send(viewModel.MapOne<GetAllNotificationMessagesForClientQuery>());

            var response = result.Data.MapPage<GetAllNotificationMessagesForClientDTO, GetAllNotificationMessagesForClientResponseViewModel>();

            if (result.IsSuccess && result.Data != null)
                return EndPointResponse<PagingViewModel<GetAllNotificationMessagesForClientResponseViewModel>>.Success(response, "Get All Notfications successfully.");
            else
                return EndPointResponse<PagingViewModel<GetAllNotificationMessagesForClientResponseViewModel>>.Failure(result.ErrorCode);

        }
    }
}
