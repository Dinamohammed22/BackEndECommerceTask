using Microsoft.AspNetCore.Mvc;
using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.NotificationMessages.DTOs;
using KOG.ECommerce.Features.Common.NotificationMessages.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.NotificationMessages.GetAllNotificationMessages
{
    public class GetAllNotificationMessageEndPoint : EndpointBase<GetAllNotificationMessageRequestViewModel, GetAllNotificationMessageResponseViewModel>
    {
        public GetAllNotificationMessageEndPoint(EndpointBaseParameters<GetAllNotificationMessageRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpGet]
        //[TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetAllNotificationMessages })]
        public async Task<EndPointResponse<PagingViewModel<GetAllNotificationMessageResponseViewModel>>> GetAllNotificationMessages([FromQuery] GetAllNotificationMessageRequestViewModel viewModel)
        {
            var result = await _mediator.Send(viewModel.MapOne<GetAllNotificationMessageQuery>());

            var response = result.Data.MapPage<GetAllNotificationMessageDTO, GetAllNotificationMessageResponseViewModel>();

            if (result.IsSuccess && result.Data != null)
                return EndPointResponse<PagingViewModel<GetAllNotificationMessageResponseViewModel>>.Success(response, "Get All Notfications successfully.");
            else
                return EndPointResponse<PagingViewModel<GetAllNotificationMessageResponseViewModel>>.Failure(result.ErrorCode);

        }
    }
}
