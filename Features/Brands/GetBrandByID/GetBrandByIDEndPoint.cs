using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Brands.GetBrandByID.Orchestrator;
using KOG.ECommerce.Features.Common.Brands.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Brands.GetBrandByID
{
    public class GetBrandByIDEndPoint : EndpointBase<GetBrandByIDRequestViewModel, GetBrandByIDResponseViewModel>
    {
        public GetBrandByIDEndPoint(EndpointBaseParameters<GetBrandByIDRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetBrandByID })]
        public async Task<EndPointResponse<GetBrandByIDResponseViewModel>> GetBrandByID([FromQuery] GetBrandByIDRequestViewModel viewModel)
        {

            var result = await _mediator.Send(viewModel.MapOne<GetBrandByIdOrchestrator>());

            var response = result.Data.MapOne<GetBrandByIDResponseViewModel>();

            if (result.IsSuccess && result.Data != null)
                return EndPointResponse<GetBrandByIDResponseViewModel>.Success(response, "Get Brand By ID successfully.");
            else
                return EndPointResponse<GetBrandByIDResponseViewModel>.Failure(result.ErrorCode);

        }
    }
}
