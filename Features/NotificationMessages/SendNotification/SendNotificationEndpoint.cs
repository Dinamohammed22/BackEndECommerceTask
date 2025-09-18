using Microsoft.AspNetCore.Mvc;
using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Emails.SendEmailToClients.Commands;
using KOG.ECommerce.Features.Emails.SendEmailToClients;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Roboost.Common.Views;
using KOG.ECommerce.Features.NotificationMessages.SendNotification.Orchestrator;
using KOG.ECommerce.Helpers;

namespace KOG.ECommerce.Features.NotificationMessages.SendNotification
{
    public class SendNotificationEndpoint : EndpointBase<SendNotificationRequestViewModel, SendNotificationResponseViewModel>
    {
        public SendNotificationEndpoint(EndpointBaseParameters<SendNotificationRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPost]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.SendNotification })]
        public async Task<EndPointResponse<SendNotificationResponseViewModel>> Post(SendNotificationRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;

            var result = await _mediator.Send(viewModel.MapOne<SendNotificationOrchestrator>());

            if (result.IsSuccess)
                return EndPointResponse<SendNotificationResponseViewModel>.Success(new SendNotificationResponseViewModel(), "Notification Sent To Clients successfully.");
            else
                return EndPointResponse<SendNotificationResponseViewModel>.Failure(result.ErrorCode,result.Message);

        }
    }
}
