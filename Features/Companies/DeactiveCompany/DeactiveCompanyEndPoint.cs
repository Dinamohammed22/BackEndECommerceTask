using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Companies.DeactiveCompany.Commands;
using KOG.ECommerce.Features.Companies.DeactiveCompany.Orchestrators;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Companies.DeactiveCompany
{
    public class DeactiveCompanyEndPoint : EndpointBase<DeactiveCompanyRequestViewModel, DeactiveCompanyResponseViewModel>
    {
        public DeactiveCompanyEndPoint(EndpointBaseParameters<DeactiveCompanyRequestViewModel> parameters) : base(parameters) { }
        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.DeactivateCompany })]
        public async Task<EndPointResponse<DeactiveCompanyResponseViewModel>> DeactiveCompany(DeactiveCompanyRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<DeactiveCompanyOrchestrator>());
            if (result.IsSuccess)
                return EndPointResponse<DeactiveCompanyResponseViewModel>.Success(new DeactiveCompanyResponseViewModel(), "Company Deactivated successfully");
            else
                return EndPointResponse<DeactiveCompanyResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
