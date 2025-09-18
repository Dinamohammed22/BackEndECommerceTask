using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Companies.DeleteCompany.Orchestrator;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Companies.DeleteCompany
{
    public class DeleteCompanyEndPoint : EndpointBase<DeleteCompanyRequestViewModel, DeleteCompanyResponseViewModel>
    {
        public DeleteCompanyEndPoint(EndpointBaseParameters<DeleteCompanyRequestViewModel> dependencyParameters)
            : base(dependencyParameters)
        {
        }

        [HttpDelete]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.DeleteCompany })]
        public async Task<EndPointResponse<DeleteCompanyResponseViewModel>> Delete(DeleteCompanyRequestViewModel viewModel)
        {

            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;

            var result = await _mediator.Send(viewModel.MapOne<DeleteCompanyOrchestrator>());

            if (result.IsSuccess)
                return EndPointResponse<DeleteCompanyResponseViewModel>.Success(new DeleteCompanyResponseViewModel(), "Company Deleted successfully");
            else
                return EndPointResponse<DeleteCompanyResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
