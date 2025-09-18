using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Companies.EditCompany.Orchestrators;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Companies.EditCompany
{
    public class EditCompanyEndPoint : EndpointBase<EditCompanyRequestViewModel, EditCompanyResponseViewModel>
    {
        public EditCompanyEndPoint(EndpointBaseParameters<EditCompanyRequestViewModel> dependencyParameters)
            : base(dependencyParameters)
        {
        }

        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.EditCompany })]
        public async Task<EndPointResponse<EditCompanyResponseViewModel>> Put( EditCompanyRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;

            var result = await _mediator.Send(viewModel.MapOne<EditCompanyOrchestrator>());

            if (result.IsSuccess)
                return EndPointResponse<EditCompanyResponseViewModel>.Success(new EditCompanyResponseViewModel(), "Company Updated successfully");
            else
                return EndPointResponse<EditCompanyResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
