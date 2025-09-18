using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.Emails.SendEmailToCompanies.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Emails.SendEmailToCompanies
{
    public class SendEmailToCompaniesEndpoint : EndpointBase<SendEmailToCompaniesRequestViewModel, SendEmailToCompaniesResponseViewModel>
    {
        public SendEmailToCompaniesEndpoint(EndpointBaseParameters<SendEmailToCompaniesRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPost]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.SendEmailToCompanies })]
        public async Task<EndPointResponse<SendEmailToCompaniesResponseViewModel>> Post(SendEmailToCompaniesRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;

            var result = await _mediator.Send(viewModel.MapOne<SendEmailToCompaniesCommand>());

            if (result.IsSuccess)
                return EndPointResponse<SendEmailToCompaniesResponseViewModel>.Success(new SendEmailToCompaniesResponseViewModel(), "Email Sent To Company successfully");
            else
                return EndPointResponse<SendEmailToCompaniesResponseViewModel>.Failure(result.ErrorCode);

        }
    }
}
