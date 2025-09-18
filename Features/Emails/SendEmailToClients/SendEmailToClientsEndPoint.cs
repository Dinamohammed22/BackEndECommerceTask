using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.Emails.SendEmailToClients.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Emails.SendEmailToClients
{
    public class SendEmailToClientsEndPoint : EndpointBase<SendEmailToClientsRequestViewModel, SendEmailToClientsResponseViewModel>
    {
        public SendEmailToClientsEndPoint(EndpointBaseParameters<SendEmailToClientsRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPost]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.SendEmailToClients })]
        public async Task<EndPointResponse<SendEmailToClientsResponseViewModel>> Post(SendEmailToClientsRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;

            var result = await _mediator.Send(viewModel.MapOne<SendEmailToClientsCommand>());

            if (result.IsSuccess)
                return EndPointResponse<SendEmailToClientsResponseViewModel>.Success(new SendEmailToClientsResponseViewModel(), "Email Sent To Clients successfully.");
            else
                return EndPointResponse<SendEmailToClientsResponseViewModel>.Failure(result.ErrorCode);

        }
    }
}
