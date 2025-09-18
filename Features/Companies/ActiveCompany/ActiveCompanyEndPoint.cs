using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Companies.ActiveCompany.Orchestrators;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Companies.ActiveCompany
{
    public class ActiveCompanyEndPoint : EndpointBase<ActiveCompanyRequestViewModel, ActiveCompanyResponseViewModel>
    {
        public ActiveCompanyEndPoint(EndpointBaseParameters<ActiveCompanyRequestViewModel> parameters) : base(parameters) { }
        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.ActivateCompany })]
        public async Task<EndPointResponse<ActiveCompanyResponseViewModel>> ActiveCompany(ActiveCompanyRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<ActiveCompanyOrchestrator>());
            if (result.IsSuccess)
                return EndPointResponse<ActiveCompanyResponseViewModel>.Success(new ActiveCompanyResponseViewModel(), "Company Activated successfully.");
            else
                return EndPointResponse<ActiveCompanyResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
