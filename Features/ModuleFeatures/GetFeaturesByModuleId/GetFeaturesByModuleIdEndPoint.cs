using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.Common.ModuleFeatures.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.ModuleFeatures.GetFeaturesByModuleId
{
    public class GetFeaturesByModuleIdEndPoint : EndpointBase<GetFeaturesByModuleIdRequestViewModel, GetFeaturesByModuleIdResponseViewModel>
    {
        public GetFeaturesByModuleIdEndPoint(EndpointBaseParameters<GetFeaturesByModuleIdRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetFeaturesbyModuleID })]
        public async Task<ActionResult<EndPointResponse<IEnumerable<GetFeaturesByModuleIdResponseViewModel>>>> GetFeaturesByModuleId(
       [FromQuery] GetFeaturesByModuleIdRequestViewModel filter)
        {
            
            var result = await _mediator.Send(filter.MapOne<GetFeaturesByModuleIdQuery>());

            if (!result.IsSuccess || result.Data == null)
            {
                return EndPointResponse<IEnumerable<GetFeaturesByModuleIdResponseViewModel>>.Failure(ErrorCode.NotFound, "No Features found.");
            }

            
            var response = result.Data.MapList<GetFeaturesByModuleIdResponseViewModel>();
            return EndPointResponse<IEnumerable<GetFeaturesByModuleIdResponseViewModel>>.Success(response, "Features filtered successfully");
        }

    }
}
